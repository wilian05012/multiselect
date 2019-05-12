using System;
using System.Collections.Generic;

namespace sampledbDAL {
    public partial class Hurricane {
        public Hurricane() {
            Affections = new HashSet<Affection>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LandfallDate { get; set; }
        public int SaffirSimpsonCat { get; set; }

        public virtual ICollection<Affection> Affections { get; set; }
    }
}
