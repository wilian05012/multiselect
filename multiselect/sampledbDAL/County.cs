using System;
using System.Collections.Generic;

namespace sampledbDAL {
    public partial class County {
        public County() {
            Affectations = new HashSet<Affectation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Affectation> Affectations { get; set; }
    }
}
