using ESCO.Reference.Data.Model;
using ESCO.Reference.Data.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private readonly ApiBoServices services2 = new();


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
            string result = services.GetReferenceDataAsString(null, "FT").Result;
            Console.Write(JsonSerializer.Serialize(result, options));

            Assert.IsTrue(result != string.Empty);
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceData()
        {
            var date = DateTime.Parse("03-08-2024");
            ReferenceDatas result = services.GetReferenceData(null, "XLINKD").Result;
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
        public void GetReferenceDataUSAWithTreasuries()
        {
            // Arrange
            string country = "USA";
            
            // Act
            ReferenceDatas result = services.GetReferenceDataByCountry(country).Result;
            
            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal USA instruments with treasuries: {result.totalCount}");
            
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, "Should return USA instruments");
            
            // Verificar que todos los instrumentos retornados son de USA
            var allFromUSA = result.data.All(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == country);
            
            Assert.IsTrue(allFromUSA, $"All instruments should be from {country}");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataWithTreasuriesFalse_ShouldNotIncludeUSA()
        {
            // Arrange
            bool treasuries = false;
            
            // Act
            ReferenceDatas result = services.GetReferenceData(null, null, null, treasuries).Result;
            
            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal instruments (treasuries=false): {result.totalCount}");
            
            Assert.IsNotNull(result, "Result should not be null");
            
            // Verificar que NO hay instrumentos de USA
            var hasUSAInstruments = result.data != null && result.data.Any(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");
            
            Assert.IsFalse(hasUSAInstruments, "Should NOT return USA instruments when treasuries=false");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataWithTreasuriesTrue_ShouldIncludeUSA()
        {
            // Arrange
            bool treasuries = true;
            
            // Act
            ReferenceDatas result = services.GetReferenceData(null, null, null, treasuries).Result;
            
            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal instruments (treasuries=true): {result.totalCount}");
            
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, "Should return instruments");
            
            // Verificar que SÍ hay instrumentos de USA
            var hasUSAInstruments = result.data.Any(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");
            
            Assert.IsTrue(hasUSAInstruments, "Should include USA instruments when treasuries=true");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataWithoutTreasuriesParameter_ShouldNotIncludeUSA()
        {
            // Arrange & Act - llamada sin parámetro treasuries (valor por defecto: false)
            ReferenceDatas result = services.GetReferenceData().Result;
            
            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal instruments (default): {result.totalCount}");
            
            Assert.IsNotNull(result, "Result should not be null");
            
            // Verificar que NO hay instrumentos de USA (comportamiento por defecto)
            var hasUSAInstruments = result.data != null && result.data.Any(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");
            
            Assert.IsFalse(hasUSAInstruments, "Should NOT return USA instruments by default (treasuries not specified)");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void CompareTreasuriesParameter_CountDifference()
        {
            // Arrange & Act
            ReferenceDatas resultWithoutTreasuries = services.GetReferenceData(null, null, null, false).Result;
            ReferenceDatas resultWithTreasuries = services.GetReferenceData(null, null, null, true).Result;
            
            // Assert
            Console.WriteLine($"Instruments WITHOUT treasuries: {resultWithoutTreasuries.totalCount}");
            Console.WriteLine($"Instruments WITH treasuries: {resultWithTreasuries.totalCount}");
            
            int difference = (resultWithTreasuries.totalCount ?? 0) - (resultWithoutTreasuries.totalCount ?? 0);
            Console.WriteLine($"Difference (USA instruments): {difference}");
            
            Assert.IsTrue(resultWithTreasuries.totalCount > resultWithoutTreasuries.totalCount, 
                "Should have more instruments when treasuries=true (includes USA)");
            Assert.IsTrue(difference > 0, "The difference should be the count of USA instruments");
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
        public void Currencies()
        {
            CurrenciesToResponse result = services2.Currencies().Result;
            string strult = JsonSerializer.Serialize(result, options);
            Console.Write(strult);

        }
        #endregion

        #region ReferenceData GO Tests
        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataWithTreasuriesTrue_ShouldIncludeGOInstruments()
        {
            // Arrange
            bool treasuries = true;
            
            // Act
            ReferenceDatas result = services.GetReferenceData(null, null, null, treasuries).Result;
            
            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal instruments (treasuries=true): {result.totalCount}");
            
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, "Should return instruments");
            
            // Verificar que SÍ hay instrumentos de tipo GO
            var hasGOInstruments = result.data.Any(d =>
                !string.IsNullOrEmpty(d.type) &&
                d.type == "GO");
            
            Assert.IsTrue(hasGOInstruments, "Should include GO instruments when treasuries=true");
            
            // Contar instrumentos GO
            int goCount = result.data.Count(d =>
                !string.IsNullOrEmpty(d.type) &&
                d.type == "GO");
            
            Console.WriteLine($"Total GO instruments: {goCount}");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataWithTreasuriesFalse_ShouldStillIncludeGOInstruments()
        {
            // Arrange
            bool treasuries = false;
            
            // Act
            ReferenceDatas result = services.GetReferenceData(null, null, null, treasuries).Result;
            
            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal instruments (treasuries=false): {result.totalCount}");
            
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, "Should return instruments");
            
            // Verificar que TAMBIÉN hay instrumentos de tipo GO (no son de USA)
            var hasGOInstruments = result.data.Any(d =>
                !string.IsNullOrEmpty(d.type) &&
                d.type == "GO");
            
            Assert.IsTrue(hasGOInstruments, "Should include GO instruments when treasuries=false (non-USA GO instruments)");
            
            // Contar instrumentos GO
            int goCount = result.data.Count(d =>
                !string.IsNullOrEmpty(d.type) &&
                d.type == "GO");
            
            Console.WriteLine($"Total GO instruments (non-USA): {goCount}");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataByTypeGO_WithTreasuriesTrue()
        {
            // Arrange
            string type = "GO";
            bool treasuries = true;
            
            // Act
            ReferenceDatas result = services.GetReferenceData(null, type, null, treasuries).Result;
            
            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal GO instruments (treasuries=true): {result.totalCount}");
            
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, "Should return GO instruments");
            
            // Verificar que TODOS los instrumentos son tipo GO
            var allAreGO = result.data.All(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.type) &&
                d.type == type);
            
            Assert.IsTrue(allAreGO, "All instruments should be of type GO");
            
            // Verificar que hay instrumentos de USA (treasuries)
            var hasUSAInstruments = result.data.Any(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");
            
            Assert.IsTrue(hasUSAInstruments, "Should include USA GO instruments when treasuries=true");
            
            Console.WriteLine($"USA GO instruments: {result.data.Count(d => d.fields?.country == "USA")}");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataByTypeGO_WithTreasuriesFalse()
        {
            // Arrange
            string type = "GO";
            bool treasuries = false;
            
            // Act
            ReferenceDatas result = services.GetReferenceData(null, type, null, treasuries).Result;
            
            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal GO instruments (treasuries=false): {result.totalCount}");
            
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, "Should return GO instruments");
            
            // Verificar que TODOS los instrumentos son tipo GO
            var allAreGO = result.data.All(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.type) &&
                d.type == type);
            
            Assert.IsTrue(allAreGO, "All instruments should be of type GO");
            
            // Verificar que NO hay instrumentos de USA
            var hasUSAInstruments = result.data.Any(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");
            
            Assert.IsFalse(hasUSAInstruments, "Should NOT include USA GO instruments when treasuries=false");
            
            Console.WriteLine($"Non-USA GO instruments: {result.data.Count}");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void CompareGOInstruments_TreasuriesParameter()
        {
            // Arrange
            string type = "GO";
            
            // Act
            ReferenceDatas resultWithoutTreasuries = services.GetReferenceData(null, type, null, false).Result;
            ReferenceDatas resultWithTreasuries = services.GetReferenceData(null, type, null, true).Result;
            
            // Assert
            Console.WriteLine($"GO instruments WITHOUT treasuries: {resultWithoutTreasuries.totalCount}");
            Console.WriteLine($"GO instruments WITH treasuries: {resultWithTreasuries.totalCount}");
            
            int difference = (resultWithTreasuries.totalCount ?? 0) - (resultWithoutTreasuries.totalCount ?? 0);
            Console.WriteLine($"Difference (USA GO instruments): {difference}");
            
            Assert.IsTrue(resultWithTreasuries.totalCount > resultWithoutTreasuries.totalCount, 
                "Should have more GO instruments when treasuries=true (includes USA treasuries)");
            
            // Verificar que la diferencia son instrumentos GO de USA
            var usaGOCount = resultWithTreasuries.data.Count(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.type) &&
                d.type == "GO" &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");
            
            Console.WriteLine($"USA GO instruments in treasuries=true result: {usaGOCount}");
            Assert.AreEqual(difference, usaGOCount, "The difference should equal the count of USA GO instruments");
            
            // Verificar que NO hay USA en treasuries=false
            var nonTreasuriesHasUSA = resultWithoutTreasuries.data.Any(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");
            
            Assert.IsFalse(nonTreasuriesHasUSA, "Result without treasuries should not contain USA instruments");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataByCountryUSA_ShouldOnlyReturnGOType()
        {
            // Arrange
            string country = "USA";
            
            // Act
            ReferenceDatas result = services.GetReferenceDataByCountry(country).Result;
            
            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal USA instruments: {result.totalCount}");
            
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, "Should return USA instruments");
            
            // Verificar que TODOS los instrumentos de USA son tipo GO
            var allUSAInstrumentsAreGO = result.data.All(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.type) &&
                d.type == "GO");
            
            Assert.IsTrue(allUSAInstrumentsAreGO, "All USA instruments should be of type GO (treasuries)");
            
            // Verificar que todos son de USA
            var allFromUSA = result.data.All(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == country);
            
            Assert.IsTrue(allFromUSA, $"All instruments should be from {country}");
            
            Console.WriteLine($"Total USA GO instruments: {result.data.Count}");
        }
        #endregion
    }
}