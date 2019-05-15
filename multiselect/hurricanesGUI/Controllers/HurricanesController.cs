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

        [HttpGet]
        public async Task<ActionResult> Delete(int id) {
            Hurricane hurricane = await _context.Hurricanes.FindAsync(id);
            if(hurricane is null) {
                return new HttpNotFoundResult("Unable to find the selected storm");
            } else {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Edit(int id) {
            Hurricane hurricane = await _context.Hurricanes.FindAsync(id);
            if(hurricane is null) {
                return new HttpNotFoundResult("Unable to find the selected storm");
            } else {
                await _context.Entry(hurricane).Collection(h => h.AffectedCounties).LoadAsync();
                return View((HurricaneViewModel)hurricane);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(HurricaneViewModel hurricane) {
            if(ModelState.IsValid) {
                Hurricane h = (Hurricane)hurricane;
                _context.Entry(h).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            } else {
                return View(hurricane);
            }
        }
    }
}