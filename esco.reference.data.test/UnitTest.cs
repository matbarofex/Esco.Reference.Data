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
        public void GetReferenceDataUSAWithUSA()
        {
            // Arrange
            string country = "USA";
            
            // Act
            ReferenceDatas result = services.GetReferenceDataByCountry(country).Result;
            
            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal USA instruments with USA: {result.totalCount}");
            
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
        public void GetReferenceDataWithUSAFalse_ShouldNotIncludeUSA()
        {
            // Arrange
            bool usa = false;
            
            // Act
            ReferenceDatas result = services.GetReferenceData(null, null, null, usa).Result;
            
            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal instruments (usa=false): {result.totalCount}");
            
            Assert.IsNotNull(result, "Result should not be null");
            
            // Verificar que NO hay instrumentos de USA
            var hasUSAInstruments = result.data != null && result.data.Any(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");
            
            Assert.IsFalse(hasUSAInstruments, "Should NOT return USA instruments when usa=false");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataWithUSATrue_ShouldIncludeUSA()
        {
            // Arrange
            bool usa = true;
            
            // Act
            ReferenceDatas result = services.GetReferenceData(null, null, null, usa).Result;
            
            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal instruments (usa=true): {result.totalCount}");
            
            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, "Should return instruments");
            
            // Verificar que SÍ hay instrumentos de USA
            var hasUSAInstruments = result.data.Any(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");
            
            Assert.IsTrue(hasUSAInstruments, "Should include USA instruments when usa=true");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataWithoutUSAParameter_ShouldNotIncludeUSA()
        {
            // Arrange & Act - llamada sin parámetro usa (valor por defecto: false)
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
            
            Assert.IsFalse(hasUSAInstruments, "Should NOT return USA instruments by default (usa not specified)");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void CompareUSAParameter_CountDifference()
        {
            // Arrange & Act
            ReferenceDatas resultWithoutUSA = services.GetReferenceData(null, null, null, false).Result;
            ReferenceDatas resultWithUSA = services.GetReferenceData(null, null, null, true).Result;
            
            // Assert
            Console.WriteLine($"Instruments WITHOUT USA: {resultWithoutUSA.totalCount}");
            Console.WriteLine($"Instruments WITH USA: {resultWithUSA.totalCount}");
            
            int difference = (resultWithUSA.totalCount ?? 0) - (resultWithoutUSA.totalCount ?? 0);
            Console.WriteLine($"Difference (USA instruments): {difference}");
            
            Assert.IsTrue(resultWithUSA.totalCount > resultWithoutUSA.totalCount, 
                "Should have more instruments when usa=true (includes USA)");
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

        #region ReferenceData USA Tests
        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataWithUSATrue_ShouldIncludeUSAInstruments()
        {
            // Arrange
            bool usa = true;

            // Act
            ReferenceDatas result = services.GetReferenceData(null, null, null, usa).Result;

            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal instruments (usa=true): {result.totalCount}");

            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, "Should return instruments");

            // Verificar que hay instrumentos de USA
            var hasUSAInstruments = result.data.Any(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");

            Assert.IsTrue(hasUSAInstruments, "Should include USA instruments when usa=true");

            // Mostrar tipos de instrumentos en USA
            var usaInstrumentTypes = result.data
                .Where(d => d.fields != null && d.fields.country == "USA")
                .Select(d => d.type)
                .Distinct()
                .ToList();

            Console.WriteLine($"Total USA instruments: {result.data.Count(d => d.fields?.country == "USA")}");
            Console.WriteLine($"USA instrument types: {string.Join(", ", usaInstrumentTypes)}");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataWithUSAFalse_ShouldExcludeUSAInstruments()
        {
            // Arrange
            bool usa = false;

            // Act
            ReferenceDatas result = services.GetReferenceData(null, null, null, usa).Result;

            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal instruments (usa=false): {result.totalCount}");

            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, "Should return instruments");

            // Verificar que NO hay instrumentos de USA
            var hasUSAInstruments = result.data.Any(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");

            Assert.IsFalse(hasUSAInstruments, "Should NOT include USA instruments when usa=false");

            Console.WriteLine($"Total non-USA instruments: {result.data.Count}");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataByType_WithUSATrue_ShouldReturnOnlyRequestedType()
        {
            // Arrange
            string type = "GO";
            bool usa = true;

            // Act
            ReferenceDatas result = services.GetReferenceData(null, type, null, usa).Result;

            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal instruments of type {type} (usa=true): {result.totalCount}");

            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, $"Should return {type} instruments");

            // Verificar que TODOS los instrumentos son del tipo solicitado
            var allAreRequestedType = result.data.All(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.type) &&
                d.type == type);

            Assert.IsTrue(allAreRequestedType, $"All instruments should be of type {type}");

            // Mostrar instrumentos de USA
            var usaCount = result.data.Count(d => d.fields?.country == "USA");
            Console.WriteLine($"USA {type} instruments: {usaCount}");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataByType_WithUSAFalse_ShouldExcludeUSAInstruments()
        {
            // Arrange
            string type = "GO";
            bool usa = false;

            // Act
            ReferenceDatas result = services.GetReferenceData(null, type, null, usa).Result;

            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal {type} instruments (usa=false): {result.totalCount}");

            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, $"Should return {type} instruments");

            // Verificar que TODOS los instrumentos son del tipo solicitado
            var allAreRequestedType = result.data.All(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.type) &&
                d.type == type);

            Assert.IsTrue(allAreRequestedType, $"All instruments should be of type {type}");

            // Verificar que NO hay instrumentos de USA
            var hasUSAInstruments = result.data.Any(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");

            Assert.IsFalse(hasUSAInstruments, $"Should NOT include USA {type} instruments when usa=false");

            Console.WriteLine($"Non-USA {type} instruments: {result.data.Count}");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void CompareGOInstruments_USAParameter()
        {
            // Arrange
            string type = "GO";
            
            // Act
            ReferenceDatas resultWithoutUSA = services.GetReferenceData(null, type, null, false).Result;
            ReferenceDatas resultWithUSA = services.GetReferenceData(null, type, null, true).Result;
            
            // Assert
            Console.WriteLine($"GO instruments WITHOUT USA: {resultWithoutUSA.totalCount}");
            Console.WriteLine($"GO instruments WITH USA: {resultWithUSA.totalCount}");
            
            int difference = (resultWithUSA.totalCount ?? 0) - (resultWithoutUSA.totalCount ?? 0);
            Console.WriteLine($"Difference (USA GO instruments): {difference}");
            
            Assert.IsTrue(resultWithUSA.totalCount > resultWithoutUSA.totalCount, 
                "Should have more GO instruments when usa=true (includes USA GO instruments)");
            
            // Verificar que la diferencia son instrumentos GO de USA
            var usaGOCount = resultWithUSA.data.Count(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.type) &&
                d.type == "GO" &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");
            
            Console.WriteLine($"USA GO instruments in usa=true result: {usaGOCount}");
            Assert.AreEqual(difference, usaGOCount, "The difference should equal the count of USA GO instruments");
            
            // Verificar que NO hay USA en usa=false
            var nonUSAHasUSA = resultWithoutUSA.data.Any(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == "USA");
            
            Assert.IsFalse(nonUSAHasUSA, "Result without USA should not contain USA instruments");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataUSACommonStock_ShouldReturnUSACSInstruments()
        {
            // Arrange
            string country = "USA";
            string type = "CS"; // Common Stock

            // Act
            ReferenceDatas result = services.GetReferenceDataByCountryAndType(country, type).Result;

            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal USA CS instruments: {result.totalCount}");

            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, "Should return USA CS instruments");

            // Verificar que todos son de USA y tipo CS
            var allUSACS = result.data.All(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == country &&
                d.type == type);

            Assert.IsTrue(allUSACS, $"All instruments should be from {country} and type {type}");

            Console.WriteLine($"Total USA CS instruments: {result.data.Count}");
        }

        [TestMethod]
        [TestCategory("ReferenceData")]
        public void GetReferenceDataUSAETF_ShouldReturnUSAETFInstruments()
        {
            // Arrange
            string country = "USA";
            string type = "ETF"; // Exchange Traded Fund

            // Act
            ReferenceDatas result = services.GetReferenceDataByCountryAndType(country, type).Result;

            // Assert
            string resultJson = JsonSerializer.Serialize(result, options);
            Console.Write(resultJson);
            Console.WriteLine($"\nTotal USA ETF instruments: {result.totalCount}");

            Assert.IsNotNull(result, "Result should not be null");
            Assert.IsTrue(result.data.Count > 0, "Should return USA ETF instruments");

            // Verificar que todos son de USA y tipo ETF
            var allUSAETF = result.data.All(d =>
                d.fields != null &&
                !string.IsNullOrEmpty(d.fields.country) &&
                d.fields.country == country &&
                d.type == type);

            Assert.IsTrue(allUSAETF, $"All instruments should be from {country} and type {type}");

            Console.WriteLine($"Total USA ETF instruments: {result.data.Count}");
        }
        #endregion
    }
}