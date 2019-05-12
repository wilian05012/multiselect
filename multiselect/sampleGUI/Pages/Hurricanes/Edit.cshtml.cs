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
        private IEnumerable<County> AvailableCounties { get;  } 

        public EditModel(SampleDbContext context) {
            _context = context;
            AvailableCounties = _context.Counties.ToArray();
        }

        [BindProperty]
        [UIHint("Hurricane")]
        public HurricaneViewModel Hurricane { get; set; }

        public async Task<IActionResult> OnGetAsync(int id) {
            Hurricane = (HurricaneViewModel)(await _context.Hurricanes.FindAsync(id));
            if(Hurricane is null) {
                return NotFound();
            } else {
                return Page();
            }
        }
    }
}