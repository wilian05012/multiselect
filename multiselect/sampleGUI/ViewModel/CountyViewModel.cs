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
            return county is null ? null : new CountyViewModel() {
                Id = county.Id,
                Name = county.Name,
                HurricanesId = county.Affectations is null ? null : county.Affectations.Select(affection => affection.HurricaneId).ToList()
            };
        }

        public static explicit operator DAL.County(CountyViewModel county) {
            return county is null ? null : new DAL.County() {
                Id = county.Id,
                Name = county.Name,
                Affectations = county.HurricanesId is null ? null : county.HurricanesId.Select(hurricaneId => new DAL.Affectation() { CountyId = county.Id, HurricaneId = hurricaneId }).ToList()
            };
        }
    }
}
