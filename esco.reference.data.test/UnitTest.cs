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

        private const string subscriptionKey2 = "Basic wsprimaryreference:PrimaryReference1";
        private readonly ApiBoServices services2 = new(subscriptionKey2);


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

        #endregion

        #region ESCO
        
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
        public void GetReferenceDataTypes()
        {
            ReferenceDataTypes result = services.GetReferenceDataTypes();
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

            Assert.IsTrue(result.Count != 0);
        }
        #endregion

        #region Currecies
        [TestMethod]
        [TestCategory("Currencies")]
        public void GetCurrencies()
        {
            CurrenciesToResponse result = services2.GetCurrencies().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

        }
        #endregion
    }
}