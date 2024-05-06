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

        #region ReferenceDatas
        //Retorna la lista de instrumentos financieros.
        public async Task<ReferenceDatas> GetReferenceData(string url) => await httpClient.GetFromJsonAsync<ReferenceDatas>(url, Options());

        public async Task<Prices> GetPrices(string url) => await httpClient.GetFromJsonAsync<Prices>(url, Options());
        #endregion
    }
}