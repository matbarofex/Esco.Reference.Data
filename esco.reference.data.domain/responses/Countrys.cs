using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Countrys : List<string> { }

    public class CountrysList
    {
        public List<CountryFields> data { get; set; }
    }

    public class CountryFields
    {
        public Country fields { get; set; }
    }

    public class Country
    {
        public string country { get; set; }
    }
}
