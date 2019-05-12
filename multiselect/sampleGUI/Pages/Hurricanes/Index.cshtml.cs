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
    public class IndexModel : PageModel {
        private readonly SampleDbContext _context;
        public IEnumerable<HurricaneViewModel> Hurricanes { get; set; }
        public IndexModel(SampleDbContext context) {
            _context = context;
            AvailableCounties = _context.Counties.ToArray();
            NewHurricane = new HurricaneViewModel();
        }

        [BindProperty]
        [UIHint("Hurricane")]
        public HurricaneViewModel NewHurricane { get; set; }

        public IEnumerable<County> AvailableCounties { get; } 

        public async Task<IActionResult> OnGetAsync() {
            Hurricanes = await _context.Hurricanes
                .Include(hurricane => hurricane.Affections)
                .Select(hurricane => (HurricaneViewModel)hurricane)
                .ToArrayAsync();
                

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id) {
            Hurricane hurricane = await _context.Hurricanes.FindAsync(id);
            if(!(hurricane is null)) {
                _context.Hurricanes.Remove(hurricane);
                await _context.SaveChangesAsync();
            }

            return Page();
        }
    }
}