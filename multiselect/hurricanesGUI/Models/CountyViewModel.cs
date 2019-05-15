using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using hurricanesDAL;

namespace hurricanesGUI.Models {
    public class CountyViewModel {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public ICollection<int> HurricanesId { get; set; }

        public static explicit operator CountyViewModel(County county) {
            return county is null ? null : new CountyViewModel() {
                Id = county.Id,
                Name = county.Name,
                HurricanesId = county.Hurricanes.Select(hurricane => hurricane.Id).ToList()
            };
        }

        public static explicit operator County(CountyViewModel county) {
            return county is null ? null : new County() {
                Id = county.Id,
                Name = county.Name,
                Hurricanes = county.HurricanesId.Select(hurricaneId => new Hurricane() { Id = hurricaneId}).ToList()
            };
        }
    }
}