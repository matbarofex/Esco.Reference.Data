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
    public class ReferenceDataServices : IReferenceDataServices
    {
        private readonly ReferenceDataHttpClient httpClient;
        private bool _paginated;

        /// <summary>
        /// Inicialización del servicio API de Reference Datas.
        /// </summary>
        /// <param name="key">(Required) Suscription key del usuario. Requerido para poder operar en la API (Solicitar habilitación de la suscripción despues de la creación de cuenta).</param>                              
        /// <param name="host">(Optional) Dirección url de la API Reference Data. Si es null toma el valor por defecto: https://apids.primary.com.ar/ </param>
        /// <param name="paginated">(Optional) Habilitación del paginado de registros (hasta 500 por página) de cada endpoints (por defecto: false) </param>
        /// <returns></returns>
        public ReferenceDataServices(string key, string host = null, bool paginated = false)
        {
            httpClient = new ReferenceDataHttpClient(key, host);
            _paginated = paginated;
        }

        /// <summary>
        /// Habilitación del paginado de registros (por defecto es false: trae todos los registros sin paginar)
        /// </summary>
        /// <param name="paginated">(Optional) Habilitación del paginado de registros (hasta 500 por página) de cada endpoints (por defecto: false) </param>
        /// <returns></returns>
        public void PaginatedMode(bool paginated = true)
        {
            _paginated = paginated;
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

        #region Prices
        /// <summary>
        /// Retorna la lista de campos de precios actualizados de los instrumentos financieros.
        /// </summary>   
        /// <param name="type">(Optional) Filtrar por tipo de Instrumentos.</param> 
        /// <returns>List<Price></returns>
        public async Task<List<Price>> GetPrices(string type = null) =>
            await GetAsPrices(type); 

        public async Task<List<Price>> GetPricesInParallell(string url, string type = null)
        {
            var skip = 0;
            var count = -1;
            var list = new List<Price>();

            while (count != 0)
            {
                string pages = SetUrl(url + Url.FilterSkip, skip.ToString());
                var prices = await httpClient.GetPrices(pages);
                count = prices.Count;
                if (count != 0)
                {
                    list.AddRange(prices);
                    skip += 300;
                }
            }
            return list;
        }

        private async Task<List<Price>> GetAsPrices(string type = null)
        {
            try
            {
                var typestr = (type != null) ? string.Format(Url.FilterTypeStr, null, null, type) : string.Empty;
                var url = Url.Prices + Url.FilterAll + typestr;
                if (_paginated)
                {
                    return await httpClient.GetPrices(url);
                }
                return await GetPricesInParallell(url, type);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ReferenceDatas 

        /// <summary>
        /// Retorna la lista de instrumentos financieros.
        /// </summary>
        /// <param name="date">(Optional) Filtrar por Fecha de actualizacion de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>        
        /// <returns>ReferenceDatas json.</returns>
        public async Task<ReferenceDatas> GetReferenceData(DateTime? date = null, string type = null, string schema = null)
        {
            string cfg = (date != null) ? Url.FilterDated : null;
            return await GetAsReferenceData(GetUrl(cfg, type, schema, false, date));
        }


        /// <summary>
        /// Retorna la lista de instrumentos financieros como una cadena.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>        
        /// <returns>string</returns>
        public async Task<string> GetReferenceDataAsString(DateTime? date = null, string type = null, string schema = null) =>
            await GetAsString(type, schema, null, date);

        private async Task<string> GetAsString(string type = null, string schema = null, string cfg = null, DateTime? date = null)
        {
            try
            {
                cfg = (date != null) ? Url.FilterDated : cfg;
                var result = await GetAsReferenceData(GetUrl(cfg, type, schema, false, date));
                var serializedResult = JsonSerializer.Serialize(result, httpClient.Options());

                return serializedResult;
            }
            catch
            {
                throw;
            }
        }

        private async Task<int> GetBatches(string url)
        {
            ReferenceDatas response = await httpClient.GetReferenceData(url);
            return (int)Math.Ceiling((double)response.totalCount / 500);
        }

        public async Task<IEnumerable<ReferenceData>> GetInParallelInWithBatches(string url)
        {
            var skip = 0;
            var tasks = new List<Task<ReferenceDatas>>();
            int numberOfBatches = await GetBatches(url);

            for (int i = 0; i < numberOfBatches + 1; i++)
            {
                string pages = SetUrl(url + Url.FilterPages, skip.ToString());
                tasks.Add(httpClient.GetReferenceData(pages));
                skip += 500;
            }

            return (await Task.WhenAll(tasks)).SelectMany(u => u.data);
        }

        private async Task<ReferenceDatas> GetAsReferenceData(string url)
        {
            try
            {
                if (_paginated || url.Contains("top") || url.Contains("skip"))
                {
                    return await httpClient.GetReferenceData(url);
                }
                var data = await GetInParallelInWithBatches(url);

                return new()
                {
                    data = data.ToList(),
                    totalCount = data.Count() 
                };
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ESCO
        /// <summary>
        /// Retorna la lista de Monedas  
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Currencys object Result.</returns>
        public async Task<Currencys> GetCurrencys(string schema = null)
        {
            try
            {
                Currencys currencys = new();
                CurrencysList rest = JsonSerializer.Deserialize<CurrencysList>(await GetAsString(null, schema, Url.Currency));
                if (rest != null && rest.data.Count > 0)
                {
                    rest.data
                        .GroupBy(x => x.fields.currency)
                        .Select(x => x.First())
                        .ToList()
                        .ForEach(x => currencys.Add(x.fields.currency));
                }
                return currencys;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna la lista de Tipos de Instrumentos financieros
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDataTypes object Result.</returns>
        public ReferenceDataTypes GetReferenceDataTypes(string schema = null) => TypesList.List;

        #endregion 

        #region Reports
        /// <summary>
        /// Devuelve la lista completa de campos para los reportes
        /// </summary>        
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Reports object Result.</returns>
        public async Task<Reports> GetFieldsReports(string schema = null) =>
            await httpClient.GetReports(SetUrl(Url.FieldsReports, schema ?? Schema.actual));

        /// <summary>
        /// Devuelve la lista completa de campos
        /// </summary>        
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Reports object Result.</returns>
        public async Task<Reports> GetFields(string schema = null) =>
            await httpClient.GetReports(SetUrl(Url.Fields, schema ?? Schema.actual));
        #endregion
    }
}