using System;

namespace ESCO.Reference.Data.Model
{
    public class SuscriptionKeyEmpty : Exception
    {
        public SuscriptionKeyEmpty() : base("Suscripction Key is empty") {
        }
    }

    public class ResponseBase : Exception
    {
        public Exception exception { get; set; }
        public string result { get; set; }
    }
}