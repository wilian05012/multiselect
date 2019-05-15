using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using sampledbDAL;
using sampleGUI.ViewModel;

namespace sampleGUI.Pages.Hurricanes {
    public class EditModel : PageModel {
        private readonly SampleDbContext _context;
        public IEnumerable<County> AvailableCounties { get;  } 

        public EditModel(SampleDbContext context) {
            _context = context;
            AvailableCounties = _context.Counties.ToArray();
        }

        [BindProperty]
        [UIHint("Hurricane")]
        public HurricaneViewModel Hurricane { get; set; }

        public async Task<IActionResult> OnGetAsync(int id) {
            Hurricane = (HurricaneViewModel)(await _context.Hurricanes.Include(h => h.Affectations).FirstOrDefaultAsync(h => h.Id == id));
            if(Hurricane is null) {
                return NotFound();
            } else {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync() {
            if(ModelState.IsValid) {
                Hurricane hurricane = (Hurricane)Hurricane;
                _context.Entry(hurricane).State = EntityState.Modified;

                IEnumerable<int> selectedCountiesId = hurricane.Affectations.Select(a => a.CountyId).ToArray();

                await _context.SaveChangesAsync();
                _context.Entry(hurricane).State = EntityState.Detached;


                Hurricane h = await _context.Hurricanes.FindAsync(Hurricane.Id);
                await _context.Entry(h).Collection(hc => hc.Affectations).LoadAsync();
                h.Affectations.Clear();
                foreach(int countyId in selectedCountiesId) {
                    h.Affectations.Add(new Affectation() { CountyId = countyId, HurricaneId = Hurricane.Id });
                }
                
                await _context.SaveChangesAsync();

                return Redirect("/Hurricanes/Index");
            } else {
                return Page();
            }
        }
    }
}