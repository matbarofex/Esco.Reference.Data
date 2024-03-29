﻿using ESCO.Reference.Data.Model;
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
                _ = _services.GetMapping().Result;
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
                _ = services.GetMapping().Result;
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
                _ = _services.GetMapping().Result;
            });

            Console.Write(ex.InnerException.Message);
            Assert.AreEqual(expected, ex.InnerException.Message);
        }
        #endregion

        #region Schemas       
        [TestMethod]
        [TestCategory("Schemas")]
        public void GetMapping()
        {
            Mappings result = services.GetMapping().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsNotNull(result);
        }
        #endregion

        #region OData
        [TestMethod]
        [TestCategory("OData")]
        public void GetODataReferenceDataTop()
        {
            string query = "?$filter=type eq 'MF'&$select=symbol,currency,cnvCode,country&$top=355&$skip=0";
            ReferenceDatas result = services.GetReferenceDataByOData(query).Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("OData")]
        public void GetODataReferenceData()
        {
            string query = "?$filter=type eq 'MF'&$select=symbol,currency,cnvCode,country";
            ReferenceDatas result = services.GetReferenceDataByOData(query).Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("OData")]
        public void GetByODataAsString()
        {
            //string query = "?$filter=type eq 'FUT'";
            string query = "?$top=500&$skip=500";
            string result = services.GetByODataAsString(query).Result;
            var json = JsonSerializer.Deserialize<ReferenceDatas>(result);
            Console.Write(JsonSerializer.Serialize(json, options));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("OData")]
        public void GetByODataAsOnlyString()
        {
            string query = "?$filter=type eq 'FUT'";
            string result = services.GetByODataAsString(query).Result;            
            Console.Write(result);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("OData")]
        public void GetODataReferenceDataNull()
        {
            string expected = "Response status code does not indicate success: 404 (Resource Not Found).";
            string query = "top=6&$filter=type eq 'MF'";

            var ex = Assert.ThrowsException<AggregateException>(() =>
            {
                _ = services.GetReferenceDataByOData(query).Result;
            });

            Console.Write(ex.InnerException.Message);
            Assert.AreEqual(expected, ex.InnerException.Message);
        }

        [TestMethod]
        [TestCategory("OData")]
        public void GetODataConsolidate()
        {
            string query = "?$filter=type eq 'CS'";
            ReferenceDatas result = services.GetConsolidatedByOData(query).Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("OData")]
        public void GetConsolidatedAsString()
        {
            string query = "?$filter=type eq 'CS'";
            string result = services.GetConsolidatedAsString(query).Result;
            var json = JsonSerializer.Deserialize<ReferenceDatas>(result);
            Console.Write(JsonSerializer.Serialize(json, options));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("OData")]
        public void GetODataConsolidateNull()
        {
            string expected = "Response status code does not indicate success: 404 (Resource Not Found).";
            string query = "top=6&$filter=type eq 'MF'";

            var ex = Assert.ThrowsException<AggregateException>(() =>
            {
                _ = services.GetConsolidatedByOData(query).Result;
            });

            Console.Write(ex.InnerException.Message);
            Assert.AreEqual(expected, ex.InnerException.Message);
        }

        [TestMethod]
        [TestCategory("OData")]
        public void GetODataCSV()
        {
            string query = "?$filter=type eq 'FUT'";
            Stream result = services.GetCSVByOData(query).Result;
            StreamReader stream = new(result);
            Console.Write(stream.ReadToEnd());

            Assert.IsTrue(result.CanRead);
        }

        [TestMethod]
        [TestCategory("OData")]
        public void SaveDataCSV()
        {
            string query = "?$filter=type eq 'FUT'";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            bool export = services.SaveCSVByOData(path, "report", query).Result;
            Assert.IsTrue(export);
        }
        #endregion

        #region ByTypes
        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetFondos()
        {
            Fondos result = services.GetFondos().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetCedears()
        {
            Cedears result = services.GetCedears().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetAcciones()
        {
            Acciones result = services.GetAcciones().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetAccionesADRS()
        {
            Acciones result = services.GetAccionesADRS("schema-001").Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetAccionesPrivadas()
        {
            Acciones result = services.GetAccionesPrivadas().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetAccionesPYMES()
        {
            Acciones result = services.GetAccionesPYMES().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetObligaciones()
        {
            Obligaciones result = services.GetObligaciones().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }


        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetTitulos()
        {
            Titulos result = services.GetTitulos().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetFuturos()
        {
            Futuros result = services.GetFuturos().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetFuturosOTC()
        {
            FuturosOTC result = services.GetFuturosOTC().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetOpciones()
        {
            Opciones result = services.GetOpciones().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetOpcionesMTR()
        {
            OpcionesMTR result = services.GetOpcionesMTR().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }


        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetOpcionesOTC()
        {
            OpcionesOTC result = services.GetOpcionesOTC().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetPases()
        {
            Pases result = services.GetPases().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }
        
        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetCauciones()
        {
            Cauciones result = services.GetCauciones().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetPlazos()
        {
            Plazos result = services.GetPlazos().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetPrestamos()
        {
            Prestamos result = services.GetPrestamosValores().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }

        [TestMethod]
        [TestCategory("ByTypes")]
        public void GetIndices()
        {
            Indices result = services.GetIndices().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result.data.Count != 0);
        }
        #endregion

        #region Precios

        [TestMethod]
        [TestCategory("Precios")]
        public void GetPriceAsString()
        {
            string result = services.GetPriceAsString("COME-0003-C-CT-ARS").Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result != string.Empty);
        }

        [TestMethod]
        [TestCategory("Precios")]
        public void GetPrice()
        {
            Price result = services.GetPrice("COME-0003-C-CT-ARS").Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Precios")]
        public void GetPricesAsString()
        {
            string result = services.GetPricesAsString().Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result != string.Empty);
        }

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
            var date = DateTime.Parse("03-13-2023");
            string result = services.GetReferenceDataAsString(null, "OOFOTC").Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result != string.Empty);
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceData()
        {
            var date = DateTime.Parse("03-27-2023");
            ReferenceDatas result = services.GetReferenceData(null, "OOFOTC").Result;
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
