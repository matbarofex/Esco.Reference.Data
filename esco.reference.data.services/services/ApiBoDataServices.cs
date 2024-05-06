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
        /// <param name="key">(Required) Suscription key del usuario. Requerido para poder operar en la API (Solicitar habilitación de la suscripción despues de la creación de cuenta).</param>                              
        /// <param name="host">(Optional) Dirección url de la API Reference Data. Si es null toma el valor por defecto: https://api.anywhereportfolio.com.ar/ </param>
        /// <returns></returns>
        public ApiBoServices(string key, string host = null)
        {
            httpClient = new ApiBoHttpClient(key, host);
        }

        /// <summary>
        /// Cambiar la Suscription Key del usuario.
        /// </summary>
        /// <param name="key">(Required) Suscription key del usuario. Requerido para poder operar en la API (Solicitar habilitación de la suscripción despues de la creación de cuenta).</param>                        
        /// <returns></returns>
        public void ChangeSuscriptionKey(string key)
        {
            httpClient.ChangeKey(key);
        }

        #region Currencies
        public async Task<CurrenciesToResponse> GetCurrencies()
        {
            try
            {
                // Obtener los datos de las monedas
                var currenciesData = await httpClient.GetCurrencies(Url.Currencies);

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
            // Aquí puedes implementar tus reglas para determinar la moneda de referencia
            // Para este ejemplo, lo dejo con valores fijos
            switch (currency)
            {
                case "USD D":
                    return null;
                case "USD C":
                    return "EXT";
                default:
                    return currency;
            }
        }

        private Dictionary<string, string> GetMarketInfo(string currency)
        {
            // Lógica para determinar la información del mercado
            // Aquí puedes implementar tus reglas para proporcionar información específica del mercado
            // Para este ejemplo, solo agregamos información para USD D
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