using ESCO.Reference.Data.Model;

using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

using static ESCO.Reference.Data.Config.Config;

namespace ESCO.Reference.Data.Services
{
    class ReferenceDataHttpClient
    {
        private static HttpClient httpClient;

        public ReferenceDataHttpClient(string key, string baseUrl = null)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl ?? Http.url)
            };

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(Http.json));
            httpClient.DefaultRequestHeaders.Add(Header.cache, Http.cache);
            httpClient.DefaultRequestHeaders.Add(Header.key, key);
        }


        public JsonSerializerOptions Options() => new()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true
        };

        public void ChangeKey(string key)
        {
            httpClient.DefaultRequestHeaders.Remove(Header.key);
            httpClient.DefaultRequestHeaders.Add(Header.key, key);
        }

        #region Schemas
        //Devuelve el mapping que tiene un schema.
        public async Task<Mappings> GetMapping(string url) => await httpClient.GetFromJsonAsync<Mappings>(url, Options());
        #endregion

        #region ReferenceDatas
        //Retorna la lista de instrumentos financieros.
        public async Task<ReferenceDatas> GetReferenceData(string url) => await httpClient.GetFromJsonAsync<ReferenceDatas>(url, Options());

        public async Task<Prices> GetPrices(string url) => await httpClient.GetFromJsonAsync<Prices>(url, Options());
        public async Task<Price> GetPrice(string url) => await httpClient.GetFromJsonAsync<Price>(url, Options());

        public async Task<HttpResponseMessage> GetAsync(string url) => await httpClient.GetAsync(url);

        public async Task<Stream> GetAsStream(string url) => await httpClient.GetStreamAsync(url);

        public async Task<string> GetAsString(string url) => await httpClient.GetStringAsync(url);

        //Retorna una especificación del estado actual.        
        public async Task<Specification> GetSpecification(string url) => await httpClient.GetFromJsonAsync<Specification>(url, Options());
        #endregion     

        #region ESCO
        // Retorna la lista de Sociedades Depositarias o Custodia de Fondos
        public async Task<CustodiansList> GetCustodians(string url) => await httpClient.GetFromJsonAsync<CustodiansList>(url, Options());

        // Retorna la lista de Sociedades Administradoras de Fondos
        public async Task<ManagmentsList> GetManagements(string url) => await httpClient.GetFromJsonAsync<ManagmentsList>(url, Options());
        // Retorna la lista de Tipos de Rentas
        public async Task<RentsList> GetRentTypes(string url) => await httpClient.GetFromJsonAsync<RentsList>(url, Options());

        // Retorna la lista de Regiones
        public async Task<RegionsList> GetRegions(string url) => await httpClient.GetFromJsonAsync<RegionsList>(url, Options());

        // Retorna la lista de Monedas
        public async Task<CurrencysList> GetCurrencys(string url) => await httpClient.GetFromJsonAsync<CurrencysList>(url, Options());

        // Retorna la lista de Países
        public async Task<CountrysList> GetCountrys(string url) => await httpClient.GetFromJsonAsync<CountrysList>(url, Options());

        // Retorna la lista de Issuers
        public async Task<IssuersList> GetIssuers(string url) => await httpClient.GetFromJsonAsync<IssuersList>(url, Options());

        // Retorna la lista de Horizons
        public async Task<HorizonsList> GetHorizons(string url) => await httpClient.GetFromJsonAsync<HorizonsList>(url, Options());

        // Retorna la lista de Tipos de Fondos
        public async Task<FundTypesList> GetFundTypes(string url) => await httpClient.GetFromJsonAsync<FundTypesList>(url, Options());

        // Retorna la lista de Issuers
        public async Task<BenchmarksList> GetBenchmarks(string url) => await httpClient.GetFromJsonAsync<BenchmarksList>(url, Options());

        // Retorna la lista de Mercados para los Instrumentos financieros
        public async Task<MarketsList> GetMarkets(string url) => await httpClient.GetFromJsonAsync<MarketsList>(url, Options());
        #endregion 

        #region Reports    
        //Devuelve la lista  de Campos.
        public async Task<Reports> GetReports(string url) => await httpClient.GetFromJsonAsync<Reports>(url, Options());
        #endregion
    }
}