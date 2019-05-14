using System;
using System.Collections.Generic;

namespace sampledbDAL {
    public partial class Hurricane {
        public Hurricane() {
            Affectations = new HashSet<Affectation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LandfallDate { get; set; }
        public int SaffirSimpsonCat { get; set; }

        public virtual ICollection<Affectation> Affectations { get; set; }
    }
}
