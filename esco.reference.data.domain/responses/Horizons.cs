using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Horizons : List<Horizon> { }
    public class HorizonsList
    {
        public List<HorizonFields> data { get; set; }
    }

    public class HorizonFields
    {
        public Horizon fields { get; set; }
    }

    public class Horizon
    {
        public string horizonId { get; set; }
        public string horizonName { get; set; }
    }
}
