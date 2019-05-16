using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using hurricanesDAL;

namespace hurricanesGUI.Models {
    public class HurricaneViewModel {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Storm name")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Category")]
        [Range(1, 5)]
        public int SaffirSimpsonCategory { get; set; }

        [Required]
        [DisplayName("Landfall date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime LandfallDate { get; set; }

        public ICollection<int> AffectedCountiesId { get; set; }

        public static explicit operator HurricaneViewModel(Hurricane hurricane) {
            return hurricane is null ? null : new HurricaneViewModel() {
                Id = hurricane.Id,
                Name = hurricane.Name,
                SaffirSimpsonCategory = hurricane.SaffirSimpsonCat,
                LandfallDate = hurricane.LandfallDate,
                AffectedCountiesId = hurricane.AffectedCounties.Select(county => county.Id).ToList()
            };
        }

        public static explicit operator Hurricane(HurricaneViewModel hurricane) {
            return hurricane is null ? null : new Hurricane() {
                Id = hurricane.Id,
                Name = hurricane.Name,
                SaffirSimpsonCat = hurricane.SaffirSimpsonCategory,
                LandfallDate = hurricane.LandfallDate,
                AffectedCounties = hurricane.AffectedCountiesId.Select(countyId => new County() { Id = countyId }).ToList()
            };
        }

        public HurricaneViewModel() {
            LandfallDate = new DateTime(year: 1970, month: 01, day: 01);
            AffectedCountiesId = new List<int>();
        }
    }
}