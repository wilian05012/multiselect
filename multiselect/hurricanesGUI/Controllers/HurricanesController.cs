using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using hurricanesDAL;
using hurricanesGUI.Models;
using hurricanesGUI.ViewModels;

namespace hurricanesGUI.Controllers {
    public class HurricanesController : Controller {
        private readonly SampleDbEntities _context;

        public HurricanesController() {
            _context = new SampleDbEntities();
        }

        [HttpGet]
        public ActionResult Index() {
            IEnumerable<CountyViewModel> availableCounties = _context.Counties
                .ToArray()
                .Select(county => (CountyViewModel)county)
                .ToArray();

            HurricanesIndexViewModel viewModel = new HurricanesIndexViewModel(availableCounties) {
                Hurricanes = _context.Hurricanes
                                .Include(h => h.AffectedCounties)
                                .ToArray()
                                .Select(h => (HurricaneViewModel)h)
                                .ToArray(),
                NewHurricane = new HurricaneViewModel()
            };

            return View(viewModel);
        }

        public ActionResult AddNew(HurricaneViewModel NewHurricane) {
            if (ModelState.IsValid) {
                Hurricane newHurricane = (Hurricane)NewHurricane;
                newHurricane.AffectedCounties.Clear();
                _context.Hurricanes.Add(newHurricane);
                foreach (int countyId in NewHurricane.AffectedCountiesId) {
                    newHurricane.AffectedCounties.Add(_context.Counties.Find(countyId));
                }
                
                _context.SaveChanges();

                return RedirectToAction("Index");
            } else {
                IEnumerable<CountyViewModel> availableCounties = _context.Counties
                                                                    .ToArray()
                                                                    .Select(county => (CountyViewModel)county)
                                                                    .ToArray();

                HurricanesIndexViewModel viewModel = new HurricanesIndexViewModel(availableCounties) {
                    Hurricanes = _context.Hurricanes
                                    .Include(h => h.AffectedCounties)
                                    .ToArray()
                                    .Select(h => (HurricaneViewModel)h)
                                    .ToArray(),
                    NewHurricane = NewHurricane
                };
                return View("Index", viewModel);
            }

        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id) {
            Hurricane hurricane = await _context.Hurricanes.FindAsync(id);
            if(hurricane is null) {
                return new HttpNotFoundResult("Unable to find the selected storm");
            } else {
                _context.Hurricanes.Remove(hurricane);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Edit(int id) {
            Hurricane hurricane = await _context.Hurricanes.FindAsync(id);
            if(hurricane is null) {
                return new HttpNotFoundResult("Unable to find the selected storm");
            } else {
                await _context.Entry(hurricane).Collection(h => h.AffectedCounties).LoadAsync();
                ViewData["AvailableCounties"] = _context.Counties
                                                    .ToArray()
                                                    .Select(county => (CountyViewModel)county)
                                                    .ToArray();
                ViewData["NamePrefix"] = "";
                return View((HurricaneViewModel)hurricane);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(HurricaneViewModel hurricane) {
            if(ModelState.IsValid) {
                Hurricane h = await _context.Hurricanes.FindAsync(hurricane.Id);
                await _context.Entry(h).Collection(hu => hu.AffectedCounties).LoadAsync();

                h.Id = hurricane.Id;
                h.Name = hurricane.Name;
                h.SaffirSimpsonCat = hurricane.SaffirSimpsonCategory;
                h.LandfallDate = hurricane.LandfallDate;

                foreach(int countyId in hurricane.AffectedCountiesId) {
                    if(!h.AffectedCounties.Any(county => county.Id == countyId)) {
                        h.AffectedCounties.Add(_context.Counties.Find(countyId));
                    }
                }

                foreach(County county in h.AffectedCounties.ToArray()) {
                    if(!hurricane.AffectedCountiesId.Any(countyId => countyId == county.Id)) {
                        h.AffectedCounties.Remove(county);
                    }
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            } else {
                return View(hurricane);
            }
        }
    }
}