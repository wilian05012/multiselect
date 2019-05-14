using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DAL = sampledbDAL;

namespace sampleGUI.ViewModel {
    public class HurricaneViewModel {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        [DisplayName("Saffir-Simpson category")]
        public int SaffirSimpsonCategory { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [DisplayName("Storm name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Landfall date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime LandFall { get; set; }

        [DisplayName("Affected counties")]
        public ICollection<int> AffecttedCountiesId { get; set; }

        public HurricaneViewModel() {
            LandFall = new DateTime(year: 1970, month: 01, day: 01);
            SaffirSimpsonCategory = 1;
        }

        public static explicit operator HurricaneViewModel(DAL.Hurricane hurricane) {
            return hurricane is null ? null : new HurricaneViewModel() {
                Id = hurricane.Id,
                SaffirSimpsonCategory = hurricane.SaffirSimpsonCat,
                Name = hurricane.Name,
                LandFall = hurricane.LandfallDate,
                AffecttedCountiesId = hurricane.Affectations is null ? null : hurricane.Affectations.Select(affection => affection.CountyId).ToList()
            };
        }

        public static explicit operator DAL.Hurricane(HurricaneViewModel hurricane) {
            return hurricane is null ? null : new DAL.Hurricane() {
                Id = hurricane.Id,
                Name = hurricane.Name,
                SaffirSimpsonCat = hurricane.SaffirSimpsonCategory,
                LandfallDate = hurricane.LandFall,
                Affectations = hurricane.AffecttedCountiesId is null ? null : hurricane.AffecttedCountiesId.Select(countyId => new DAL.Affectation() { HurricaneId = hurricane.Id, CountyId = countyId }).ToList()
            };
        }
    }
}
