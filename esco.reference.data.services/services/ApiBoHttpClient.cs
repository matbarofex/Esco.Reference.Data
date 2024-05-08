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
    class ApiBoHttpClient
    {
        private static HttpClient httpClient;

        public ApiBoHttpClient(string key, string baseUrl = null)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl ?? Http.urlAnywhereportfolio)
            };

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(Http.json));
            httpClient.DefaultRequestHeaders.Add(Header.authorization, key);
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

        #region Currencies
        public async Task<CurrenciesToResponse> Currencies(string url) => await httpClient.GetFromJsonAsync<CurrenciesToResponse>(url, Options());
        #endregion
    }
}