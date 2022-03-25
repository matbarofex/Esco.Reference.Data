using System.Collections.Generic;

namespace ESCO.Reference.Data.Model
{
    public class Mappings
    {
        public FieldsMapping fieldsMapping { get; set; }
    }

    public class FieldsMapping
    {
        public List<string> BUYSELL { get; set; }
        public List<string> CD { get; set; }
        public List<string> CORP { get; set; }
        public List<string> CS { get; set; }
        public List<string> FUT { get; set; }
        public List<string> GO { get; set; }
        public List<string> MF { get; set; }
        public List<string> OOF { get; set; }
        public List<string> OPT { get; set; }
        public List<string> REPO { get; set; }
        public List<string> STN { get; set; }
        public List<string> T { get; set; }
        public List<string> TERM { get; set; }
        public List<string> XLINKD { get; set; }        
    }
}
