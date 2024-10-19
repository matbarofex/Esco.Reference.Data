using ESCO.Reference.Data.Model;
using ESCO.Reference.Data.Services.Contracts;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

using static ESCO.Reference.Data.Config.Config;

namespace ESCO.Reference.Data.Services
{
    /// <summary>
    /// Servicios Reference Datas Conector que se integra con el Servicio Primary Information Reference.
    /// </summary>   
    public class ApiBoServices : IApiBoServices
    {
        private readonly ApiBoHttpClient httpClient;

        /// <summary>
        /// Inicialización del servicio API de Reference Datas.
        /// </summary>                        
        /// <param name="host">(Optional) Dirección url de la API Reference Data. Si es null toma el valor por defecto: https://api.anywhereportfolio.com.ar/ </param>
        /// <returns></returns>
        public ApiBoServices(string host = null)
        {
            httpClient = new ApiBoHttpClient(host);
        }

        #region Currencies
        public async Task<CurrenciesToResponse> Currencies()
        {
            try
            {
                // Obtener los datos de las monedas
                var currenciesData = await httpClient.Currencies(Url.Currencies);

                // Transformar los datos en la estructura requerida
                var processedCurrencies = currenciesData.Value.Where(currency =>
                {
                    var currencyDescription = currency.CurrencyDescription;
                    // Aquí puedes agregar las descripciones de las monedas que quieres incluir en la respuesta
                    return currencyDescription.Equals("Pesos") ||
                           currencyDescription.Equals("Dólar UY") ||
                           currencyDescription.Equals("Dólar MEP") ||
                           currencyDescription.Equals("Dólar Cable") ||
                           currencyDescription.Equals("Dólar MtR");
                }).Select(currency =>
                {
                    var processedCurrency = new CurrencyInfo
                    {
                        CurrencyDescription = currency.CurrencyDescription,
                        Currency = currency.Currency,
                        MarketDataCurrency = GetMarketDataCurrency(currency.Currency),
                        ReferenceDataCurrency = GetReferenceDataCurrency(currency.Currency),
                        Market = GetMarketInfo(currency.Currency)
                    };
                    return processedCurrency;
                }).ToList();

                return new CurrenciesToResponse { Value = processedCurrencies };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las monedas: {ex.Message}");
                throw;
            }
        }

        private string GetReferenceDataCurrency(string currency)
        {
            // Lógica para determinar la moneda de referencia
            switch (currency)
            {
                case "USD D":
                    return "";
                case "USD C":
                    return "EXT";
                default:
                    return currency;
            }
        }
        private string GetMarketDataCurrency(string currency)
        {
            // Lógica para determinar la moneda del mercado
            switch (currency)
            {
                case "USD G":
                    return "USD-G";
                case "ARS":
                    return "ARS";
                case "USD D":
                    return "MEP";
                case "USD C":
                    return "CCL";
                case "USD UY":
                    return "USD-UY";
                default:
                    return currency;
            }
        }

        private Dictionary<string, string> GetMarketInfo(string currency)
        {
            // Lógica para determinar la información del mercado
            if (currency == "USD D")
            {
                return new Dictionary<string, string>
        {
                    { "BYMA", "USD" },
                    { "MTR", "USD D" },
                    { "CAFCI", "USD" }
        };
            }
            return new Dictionary<string, string>();
        }
        #endregion
    }
}