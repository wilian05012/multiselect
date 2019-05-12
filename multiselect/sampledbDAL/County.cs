using System;
using System.Collections.Generic;

namespace sampledbDAL {
    public partial class County {
        public County() {
            Affections = new HashSet<Affection>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Affection> Affections { get; set; }
    }
}
