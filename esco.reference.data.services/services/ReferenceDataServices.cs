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
        /// Retorna la lista de instruments actualizados en el día como cadena string
        /// </summary>
        /// <param name="type">(Optional) Filtrar por tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>string</returns>
        public async Task<string> GetUpdatedAsString(string type = null, string schema = null) =>
            await httpClient.GetAsString(GetUrl(Url.FilterUpdated, type, schema));

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
        /// Retorna la lista de instrumentos financieros filtrados por campos específicos (puede incluirse cadenas de búsqueda parcial).
        /// </summary>
        /// <param name="type">(Optional) Filtrar por tipo de Instrumentos financiero (Ej: "MF","FUT", "OPC", puede incluirse una cadena de búsqueda parcial.</param>
        /// <param name="name">(Optional) Filtrar por nombre de Instrumentos (Ej: "ALUA", puede incluirse una cadena de búsqueda parcial).</param> 
        /// <param name="currency">(Optional) Filtrar por tipo de Moneda. (Ej: "ARS", puede incluirse una cadena de búsqueda parcial)</param>
        /// <param name="market">(Optional) Filtrar por Tipo de Mercado. (Ej "ROFX", "BYMA", puede incluirse una cadena de búsqueda parcial)</param>       
        /// <param name="country">(Optional) Filtrar por nombre de País (Ej: "ARG", puede incluirse una cadena de búsqueda parcial).</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo por defecto.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> SearchReferenceData(
            string type = null,
            string name = null,
            string currency = null,
            string market = null,
            string country = null,
            string schema = null) =>
            await GetAsReferenceData(GetUrlOData(Url.ReferenceData, type, name, currency, market, country, schema ?? Schema.actual));

        /// <summary>
        /// Retorna los Instrumentos financieros que contengan una cadena de búsqueda como parte del identificador (puede incluirse cadenas de búsqueda parcial).
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos financieros a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma el activo por defecto.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> SearchReferenceDataById(string id = null, string schema = null) =>
            await GetAsReferenceData(GetUrl(null, id, schema ?? Schema.actual, true));


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

        /// <summary>
        /// Retorna una especificación del estado actual de los modelos de datos Reference Data
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Specification json.</returns>
        public async Task<Specification> GetReferenceDataSpecification(string schema = null)
        {
            try
            {
                return await httpClient.GetSpecification(SetUrl(Url.Specification, schema ?? Schema.actual));
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ESCO
        /// <summary>
        /// Retorna la lista de Sociedades Depositarias o Custodia de Fondos
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Custodians object Result.</returns>
        public async Task<Custodians> GetCustodians(string schema = null)
        {
            try
            {
                Custodians custodians = new();
                CustodiansList rest =  JsonSerializer.Deserialize<CustodiansList>(await GetAsString(null, schema, Url.Custodian));
                if (rest != null && rest.data.Count > 0)
                {
                    rest.data
                        .GroupBy(x => x.fields.fundCustodianId)
                        .Select(x => x.First())
                        .OrderBy(x => int.Parse(x.fields.fundCustodianId))
                        .ToList()
                        .ForEach(x => custodians.Add(x.fields));
                }
                return custodians;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna la lista de Sociedades Administradoras de Fondos  
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Managers object Result.</returns>
        public async Task<Managments> GetManagements(string schema = null)
        {
            try
            {
                Managments managments = new();
                ManagmentsList rest = JsonSerializer.Deserialize<ManagmentsList>(await GetAsString(null, schema, Url.Managment));
                if (rest != null && rest.data.Count > 0)
                {
                    rest.data
                        .GroupBy(x => x.fields.fundManagerId)
                        .Select(x => x.First())
                        .OrderBy(x => int.Parse(x.fields.fundManagerId))
                        .ToList()
                        .ForEach(x => managments.Add(x.fields));
                }
                return managments;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna la lista de Tipos de Rentas
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Rents object Result.</returns>
        public async Task<Rents> GetRentTypes(string schema = null)
        {
            try
            {
                Rents rents = new();
                RentsList rest = JsonSerializer.Deserialize<RentsList>(await GetAsString(null, schema, Url.RentType));
                if (rest != null && rest.data.Count > 0)
                {
                    rest.data
                        .GroupBy(x => x.fields.rentTypeId)
                        .Select(x => x.First())
                        .OrderBy(x => int.Parse(x.fields.rentTypeId))
                        .ToList()
                        .ForEach(x => rents.Add(x.fields));
                }
                return rents;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna la lista de Regiones
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Regions object Result.</returns>
        public async Task<Regions> GetRegions(string schema = null)
        {
            try
            {
                Regions regions = new();
                RegionsList rest = JsonSerializer.Deserialize<RegionsList>(await GetAsString(null, schema, Url.Region));
                if (rest != null && rest.data.Count > 0)
                {
                    rest.data
                        .GroupBy(x => x.fields.regionId)
                        .Select(x => x.First())
                        .OrderBy(x => Int32.Parse(x.fields.regionId))
                        .ToList()
                        .ForEach(x => regions.Add(x.fields));
                }
                return regions;
            }
            catch
            {
                throw;
            }
        }

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
        /// Retorna la lista de Países  
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Countrys object Result.</returns>
        public async Task<Countrys> GetCountrys(string schema = null)
        {
            try
            {
                Countrys countrys = new();
                CountrysList rest = JsonSerializer.Deserialize<CountrysList>(await GetAsString(null, schema, Url.Country));
                if (rest != null && rest.data.Count > 0)
                {
                    rest.data
                        .GroupBy(x => x.fields.country)
                        .Select(x => x.First())
                        .OrderBy(x => x.fields.country)
                        .ToList()
                        .ForEach(x => countrys.Add(x.fields.country));
                }
                return countrys;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna la lista de Issuers 
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Issuers object Result.</returns>
        public async Task<Issuers> GetIssuers(string schema = null)
        {
            try
            {
                Issuers issuers = new();
                IssuersList rest = JsonSerializer.Deserialize<IssuersList>(await GetAsString(null, schema, Url.Issuer));
                if (rest != null && rest.data.Count > 0)
                {
                    rest.data
                        .GroupBy(x => x.fields.issuer)
                        .Select(x => x.First())
                        .OrderBy(x => x.fields.issuer)
                        .ToList()
                        .ForEach(x => issuers.Add(x.fields.issuer));
                }
                return issuers;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna la lista de Horizons
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Horizons object Result.</returns>
        public async Task<Horizons> GetHorizons(string schema = null)
        {
            try
            {
                Horizons horizons = new();
                HorizonsList rest = JsonSerializer.Deserialize<HorizonsList>(await GetAsString(null, schema, Url.Horizon));
                if (rest != null && rest.data.Count > 0)
                {
                    rest.data
                        .GroupBy(x => x.fields.horizonId)
                        .Select(x => x.First())
                        .OrderBy(x => int.Parse(x.fields.horizonId))
                        .ToList()
                        .ForEach(x => horizons.Add(x.fields));
                }
                return horizons;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna la lista de Tipos de Fondos
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>FundTypes object Result.</returns>
        public async Task<FundTypes> GetFundTypes(string schema = null)
        {
            try
            {
                FundTypes types = new();
                FundTypesList rest = JsonSerializer.Deserialize<FundTypesList>(await GetAsString(null, schema, Url.FundType));
                if (rest != null && rest.data.Count > 0)
                {
                    rest.data
                        .GroupBy(x => x.fields.fundTypeId)
                        .Select(x => x.First())
                        .OrderBy(x => int.Parse(x.fields.fundTypeId))
                        .ToList()
                        .ForEach(x => types.Add(x.fields));
                }
                return types;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna la lista de Benchmarks
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Benchmarks object Result.</returns>
        public async Task<Benchmarks> GetBenchmarks(string schema = null)
        {
            try
            {
                Benchmarks benchmarks = new();
                BenchmarksList rest = JsonSerializer.Deserialize<BenchmarksList>(await GetAsString(null, schema, Url.Benchmark));
                if (rest != null && rest.data.Count > 0)
                {
                    rest.data
                        .GroupBy(x => x.fields.fundBenchmarkId)
                        .Select(x => x.First())
                        .OrderBy(x => int.Parse(x.fields.fundBenchmarkId))
                        .ToList()
                        .ForEach(x => benchmarks.Add(x.fields));
                }
                return benchmarks;
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

        /// <summary>
        /// Retorna la lista de Mercados para los Instrumentos financieros
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Markets object Result.</returns>
        public async Task<Markets> GetMarkets(string schema = null)
        {
            try
            {
                Markets markets = new();
                MarketsList rest = JsonSerializer.Deserialize<MarketsList>(await GetAsString(null, schema, Url.Markets));
                if (rest != null && rest.data.Count > 0)
                {
                    rest.data
                        .GroupBy(x => x.fields.marketId)
                        .Select(x => x.First())
                        .OrderBy(x => x.fields.marketId)
                        .ToList()
                        .ForEach(x => markets.Add(x.fields.marketId));
                }
                return markets;
            }
            catch
            {
                throw;
            }
        }
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