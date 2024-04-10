using ESCO.Reference.Data.Model;
using ESCO.Reference.Data.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace esco.reference.data.test
{
    [TestClass]
    public class ReferenceDataTest
    {
        private const string subscriptionKey = "6VMzeCB2BqQucS6wXSMtkmRLv2IdzSI0Tl";
        private readonly ReferenceDataServices services = new(subscriptionKey);


        private readonly JsonSerializerOptions options = new()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        };

        #region API
        [TestMethod]
        [TestCategory("API")]
        public void SubscriptionKey()
        {
            string expected = "Response status code does not indicate success: 401 (Access Denied).";
            ReferenceDataServices _services = new(subscriptionKey + "000");

            var ex = Assert.ThrowsException<AggregateException>(() =>
            {
                
            });

            Console.Write(ex.InnerException.Message);
            Assert.AreEqual(expected, ex.InnerException.Message);
        }

        [TestMethod]
        [TestCategory("API")]
        public void ChangeKey()
        {
            string expected = "Response status code does not indicate success: 401 (Access Denied).";
            services.ChangeSuscriptionKey("****");

            var ex = Assert.ThrowsException<AggregateException>(() =>
            {
                
            });

            Console.Write(ex.InnerException.Message);
            Assert.AreEqual(expected, ex.InnerException.Message);
        }

        [TestMethod]
        [TestCategory("API")]
        public void APIHost()
        {
            string expected = "Response status code does not indicate success: 404 (Not Found).";
            string _host = "https://i.primary.com.ar/";
            ReferenceDataServices _services = new(subscriptionKey, _host);

            var ex = Assert.ThrowsException<AggregateException>(() =>
            {
                
            });

            Console.Write(ex.InnerException.Message);
            Assert.AreEqual(expected, ex.InnerException.Message);
        }
        #endregion

        #region Precios

        [TestMethod]
        [TestCategory("Precios")]
        public void GetPrices()
        {
            List<Price> result = services.GetPrices().Result;
            Console.Write(JsonSerializer.Serialize(result, options));
            Console.Write("Count: " + result.Count);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Precios")]
        public void GetPricesbyType()
        {
            var result = services.GetPrices("CD").Result;            
            Console.Write(JsonSerializer.Serialize(result, options));
            Console.Write("Count: " + result.Count);

            Assert.IsNotNull(result);
        }

        #endregion

        #region ReferenceDatas

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetUpdatedAsString()
        {
            string result = services.GetUpdatedAsString().Result;
            var json = JsonSerializer.Deserialize<ReferenceDatas>(result);
            Console.Write(JsonSerializer.Serialize(json, options));

            Assert.IsNotNull(result);
        }


        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataAsString()
        {
            var date = DateTime.Parse("03-08-2024");
            string result = services.GetReferenceDataAsString(null, "FUT").Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result != string.Empty);
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceData()
        {
            var date = DateTime.Parse("03-08-2024");
            ReferenceDatas result = services.GetReferenceData(null, "FUT").Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataNull()
        {
            ReferenceDatas result = services.GetReferenceData(null, "*****").Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count == 0);
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void SearchReferenceData()
        {
            ReferenceDatas result = services.SearchReferenceData("FUT", null, "USD", null, "ARG").Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void SearchReferenceDataNull()
        {
            ReferenceDatas result = services.SearchReferenceData("***").Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count == 0);
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void SearchReferenceDataParam()
        {
            ReferenceDatas result = services.SearchReferenceData("FUT", "***").Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.data.Count == 0);
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataSpecification()
        {
            Specification result = services.GetReferenceDataSpecification().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void SearchReferenceDataById()
        {
            ReferenceDatas result = services.SearchReferenceDataById("ALUA").Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void SearchReferenceDataByIdNull()
        {
            ReferenceDatas result = services.SearchReferenceDataById("***").Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count == 0);
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataSpecificationNull()
        {
            string expected = "Response status code does not indicate success: 500 (Internal Server Error).";
            var ex = Assert.ThrowsException<AggregateException>(() =>
            {
                _ = services.GetReferenceDataSpecification("***").Result;
            });

            Console.Write(ex.InnerException.Message);
            Assert.AreEqual(expected, ex.InnerException.Message);
        }

        #endregion

        #region ESCO
        [TestMethod]
        [TestCategory("ESCO")]
        public void GetCustodians()
        {
            Custodians result = services.GetCustodians().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }

        [TestMethod]
        [TestCategory("ESCO")]
        public void GetCustodiansSchema()
        {
            string expected = "Response status code does not indicate success: 500 (Internal Server Error).";
            var ex = Assert.ThrowsException<AggregateException>(() =>
            {
                _ = services.GetCustodians("**").Result;
            });

            Console.Write(ex.InnerException.Message);
            Assert.AreEqual(expected, ex.InnerException.Message);
        }

        [TestMethod]
        [TestCategory("ESCO")]
        public void GetManagements()
        {
            Managments result = services.GetManagements().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }

        [TestMethod]
        [TestCategory("ESCO")]
        public void GetRentTypes()
        {
            Rents result = services.GetRentTypes().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }

        [TestMethod]
        [TestCategory("ESCO")]
        public void GetRegions()
        {
            Regions result = services.GetRegions().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }

        [TestMethod]
        [TestCategory("ESCO")]
        public void GetCurrencys()
        {
            Currencys result = services.GetCurrencys().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }

        [TestMethod]
        [TestCategory("ESCO")]
        public void GetCountrys()
        {
            Countrys result = services.GetCountrys().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }

        [TestMethod]
        [TestCategory("ESCO")]
        public void GetIssuers()
        {
            Issuers result = services.GetIssuers().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }

        [TestMethod]
        [TestCategory("ESCO")]
        public void GetHorizons()
        {
            Horizons result = services.GetHorizons().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }

        [TestMethod]
        [TestCategory("ESCO")]
        public void GetFundTypes()
        {
            FundTypes result = services.GetFundTypes().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }

        [TestMethod]
        [TestCategory("ESCO")]
        public void GetBenchmarks()
        {
            Benchmarks result = services.GetBenchmarks().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }

        [TestMethod]
        [TestCategory("ESCO")]
        public void GetReferenceDataTypes()
        {
            ReferenceDataTypes result = services.GetReferenceDataTypes();
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }

        [TestMethod]
        [TestCategory("ESCO")]
        public void GetMarkets()
        {
            Markets result = services.GetMarkets().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }

        #endregion

        #region Reportes
        [TestMethod]
        [TestCategory("Fields")]
        public void GetReports()
        {
            Reports result = services.GetFieldsReports().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.fields.Count != 0);
        }

        [TestMethod]
        [TestCategory("Fields")]
        public void GetReport()
        {
            Reports result = services.GetFields().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.fields.Count != 0);
        }


        #endregion

    }
}
