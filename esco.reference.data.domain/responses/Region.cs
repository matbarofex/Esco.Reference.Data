using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Regions : List<Region> { }

    public class RegionsList
    {
        public List<RegionFields> data { get; set; }
    }

    public class RegionFields
    {
        public Region fields { get; set; }
    }

    public class Region
    {
        public string regionId { get; set; }
        public string regionName { get; set; }
    }
}
