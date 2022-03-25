using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Managments : List<Managment> { }

    public class ManagmentsList
    {
        public List<ManagmentFields> data { get; set; }
    }

    public class ManagmentFields
    {
        public Managment fields { get; set; }
    }

    public class Managment
    {
        public string fundManagerId { get; set; }
        public string fundManagerName { get; set; }
    }
}
