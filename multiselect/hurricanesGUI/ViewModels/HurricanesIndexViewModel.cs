using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using hurricanesGUI.Models;

namespace hurricanesGUI.ViewModels {
    public class HurricanesIndexViewModel {
        public IEnumerable<HurricaneViewModel> Hurricanes { get; set; }
        [UIHint("Hurricane")]
        public HurricaneViewModel NewHurricane { get; set; }

        public IEnumerable<CountyViewModel> AvailableCounties { get; }
        public HurricanesIndexViewModel(IEnumerable<CountyViewModel> availableCounties) {
            AvailableCounties = availableCounties;
        }
    }
}