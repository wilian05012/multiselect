using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DAL = sampledbDAL;

namespace sampleGUI.ViewModel {
    public class CountyViewModel {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public ICollection<int> HurricanesId { get; set; }

        public static explicit operator CountyViewModel(DAL.County county) {
            return new CountyViewModel() {
                Id = county.Id,
                Name = county.Name,
                HurricanesId = county.Affections.Select(affection => affection.HurricaneId).ToList()
            };
        }

        public static explicit operator DAL.County(CountyViewModel county) {
            return new DAL.County() {
                Id = county.Id,
                Name = county.Name,
                Affections = county.HurricanesId.Select(hurricaneId => new DAL.Affection() { CountyId = county.Id, HurricaneId = hurricaneId }).ToList()
            };
        }
    }
}
