using System;
using System.Collections.Generic;

namespace sampledbDAL {
    public partial class Affection {
        public int HurricaneId { get; set; }
        public int CountyId { get; set; }

        public virtual County County { get; set; }
        public virtual Hurricane Hurricane { get; set; }
    }
}
