using System;
using System.IO;
using System.Threading.Tasks;
using ESCO.Reference.Data.Model;
using Pathoschild.Http.Client;

namespace ESCO.Reference.Data.Services
{
    class ReferenceDataHttpClient
    {
        private string _baseUrl;
        private string _key;

        public ReferenceDataHttpClient(
            string key,
            string baseUrl = null)
        {
            _key = key;
            _baseUrl = baseUrl ?? Config.url;
        }

        public void changeKey(string key)
        {            
            _key = key;         
        }        

        #region Schemas        

        //Devuelve la lista completa de esquemas.
        public async Task<Schemas> getSchemas()
        {
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + Config.Schemas)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Schemas>();
                return rest;
            }
        }

        #endregion

        #region OData

        //Retorna la lista de instrumentos filtrados con OData.
        public async Task<ODataList> getODataReferenceData(string query, string schema)
        {
            string url = String.Format(Config.OData + query, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<ODataList>();
                return rest;
            }
        }

        //Retorna la lista de instrumentos filtrados por Id
        public async Task<ODataList> getODataReferenceDataById(string id, string schema)
        {
            string url = String.Format(Config.OData + Config.FilterId, schema, id);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<ODataList>();
                return rest;
            }
        }

        //Retorna la lista de instrumentos filtrados por campos específicos (puede incluirse cadenas de búsqueda parcial).
        public async Task<ODataList> searchODataReferenceData(
            string type,
            string currency,
            string symbol,
            string market,
            string country,
            string schema)
        {
            string url = API.getUrlOData(type, currency, symbol, market, country, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<ODataList>();
                return rest;
            }
        }

        #endregion

        #region ReferenceDatas

        //Retorna la lista de instrumentos financieros.
        public async Task<ReferenceDatas> getReferenceData(string cfg, string type, string schema)
        {
            string url = (type != null) ?
                String.Format(cfg + Config.FilterTypeStr, schema, type) :
                String.Format(cfg, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<ReferenceDatas>();
                return rest;
            }
        }        

        //Retorna los instrumentos financieros que contengan una cadena de búsqueda como parte del id.
        public async Task<ReferenceDatas> searchReferenceData(string cfg, string id, string schema)
        {
            string url = String.Format(cfg + Config.FilterId, schema, id);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<ReferenceDatas>();
                return rest;
            }
        }

        //Retorna una especificación del estado actual.        
        public async Task<Specification> getSpecification(string schema)
        {
            string url = String.Format(Config.Specification, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Specification>();
                return rest;
            }
        }
        #endregion     

        #region ESCO

        // Retorna la lista de Sociedades Depositarias o Custodia de Fondos
        public async Task<CustodiansList> getCustodians(string schema)
        {
            string url = String.Format(Config.OData + Config.Depositary, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<CustodiansList>();
                return rest;
            }
        }

        // Retorna la lista de Sociedades Administradoras de Fondos
        public async Task<ManagmentsList> getManagements(string schema)
        {
            string url = String.Format(Config.OData + Config.Managment, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<ManagmentsList>();
                return rest;
            }
        }

        // Retorna la lista de Tipos de Rentas
        public async Task<RentsList> getRentTypes(string schema)
        {
            string url = String.Format(Config.OData + Config.RentType, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<RentsList>();
                return rest;
            }
        }

        // Retorna la lista de Regiones
        public async Task<RegionsList> getRegions(string schema)
        {
            string url = String.Format(Config.OData + Config.Region, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<RegionsList>();
                return rest;
            }
        }

        // Retorna la lista de Monedas
        public async Task<CurrencysList> getCurrencys(string schema)
        {
            string url = String.Format(Config.OData + Config.Currency, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<CurrencysList>();
                return rest;
            }
        }

        // Retorna la lista de Países
        public async Task<CountrysList> getCountrys(string schema)
        {
            string url = String.Format(Config.OData + Config.Country, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<CountrysList>();
                return rest;
            }
        }

        // Retorna la lista de Issuers
        public async Task<IssuersList> getIssuers(string schema)
        {
            string url = String.Format(Config.OData + Config.Issuer, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<IssuersList>();
                return rest;
            }
        }

        // Retorna la lista de Horizons
        public async Task<HorizonsList> getHorizons(string schema)
        {
            string url = String.Format(Config.OData + Config.Horizon, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<HorizonsList>();
                return rest;
            }
        }

        // Retorna la lista de Tipos de Fondos
        public async Task<FundTypesList> getFundTypes(string schema)
        {
            string url = String.Format(Config.OData + Config.FundType, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<FundTypesList>();
                return rest;
            }
        }

        // Retorna la lista de Issuers
        public async Task<BenchmarksList> getBenchmarks(string schema)
        {
            string url = String.Format(Config.OData + Config.Benchmark, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<BenchmarksList>();
                return rest;
            }
        }

        // Retorna la lista de Tipos de Instrumentos financieros
        public async Task<ReferenceDataTypesList> getReferenceDataTypes(string schema)
        {
            string url = String.Format(Config.OData + Config.RDTypes, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<ReferenceDataTypesList>();
                return rest;
            }
        }

        // Retorna la lista de Símbolos (UnderlyingSymbol) de Instrumentos financieros
        public async Task<ReferenceDataSymbolsList> getReferenceDataSymbols(string schema)
        {
            string url = String.Format(Config.OData + Config.RDSymbols, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<ReferenceDataSymbolsList>();
                return rest;
            }
        }

        // Retorna la lista de Mercados para los Instrumentos financieros
        public async Task<MarketsList> getMarkets(string schema)
        {
            string url = String.Format(Config.OData + Config.Markets, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<MarketsList>();
                return rest;
            }
        }
        #endregion 

        #region Reports        

        //Devuelve la lista completa de Reportes.
        public async Task<Reports> getReports(string schema)
        {
            string url = String.Format(Config.Reports, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Reports>();
                return rest;
            }
        }

        //Devuelve un reporte con un id específico.
        public async Task<Report> getReport(string id, string schema)
        {
            string url = String.Format(Config.Report, schema, id);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Report>();
                return rest;
            }
        }

        //Retorna un reporte resumido de instrumentos.
        public async Task<string> getInstrumentsReport(string schema)
        {
            string url = String.Format(Config.InstrumentsReport, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).AsString();                  
                return rest;
            }
        }
        #endregion
    }
}