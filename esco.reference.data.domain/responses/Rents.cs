using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Rents : List<Rent> { }
    public class RentsList
    {
        public List<RentFields> data { get; set; }
    }

    public class RentFields
    {
        public Rent fields { get; set; }
    }

    public class Rent
    {
        public string rentTypeId { get; set; }
        public string rentTypeName { get; set; }
    }
}
