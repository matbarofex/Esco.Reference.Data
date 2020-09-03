using System;
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
            string ver,
            string baseUrl = null)
        {
            API.ver = ver;
            _key = key;
            _baseUrl = baseUrl ?? Config.url;
        }

        public void changeKey(string key)
        {            
            _key = key;         
        }        

        #region Schemas        

        //Devuelve el schema de trabajo actual.
        public async Task<Schema> getSchema()
        {
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + Config.WorkingSchema)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Schema>();
                return rest;
            }
        }

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

        //Devuelve un esquema con un id específico.
        public async Task<Schema> getSchemaId(string id)
        {
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + Config.SchemasId + id)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Schema>();
                return rest;
            }
        }

        //Verifica si la tarea de promover un schema se está ejecutando
        public async Task<PromoteSchema> getPromoteSchema()
        {
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + Config.PromoteSchema)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<PromoteSchema>();
                return rest;
            }
        }

        #endregion

        #region Fields
        //Devuelve la lista completa de fields.
        public async Task<FieldsList> getFields(string schema)
        {
            string url = String.Format(Config.Fields, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<FieldsList>();
                return rest;
            }
        }

        //Devuelve un field con un id específico.
        public async Task<Field> getField(string id, string schema)
        {
            string url = String.Format(Config.Field, schema, id);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Field>();
                return rest;
            }
        }
        #endregion

        #region Instruments
        //Obtiene una lista de campos sugeridos.
        public async Task<SuggestedFields> getInstrumentsSuggestedFields(string schema)
        {
            string url = String.Format(Config.InstrumentsSuggestedFields, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<SuggestedFields>();
                return rest;
            }
        }        

        //Retorna un reporte resumido de instruments.
        public async Task<InstrumentsReport> getInstrumentsReport(string source, string schema)
        {
            string url = (source != null) ?
                String.Format(Config.InstrumentsReport + Config.FilterSourceStr, schema, source) :
                String.Format(Config.InstrumentsReport, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<InstrumentsReport>();
                return rest;
            }
        }

        public async Task<InstrumentsReport> searchInstrumentsReport(string id, string schema)
        {
            string url = String.Format(Config.InstrumentsReport + Config.FilterId, schema, id);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<InstrumentsReport>();
                return rest;
            }
        }

        //Retorna una instrument por id.
        public async Task<Instrument> getInstrument(string id, string schema)
        {
            string url = String.Format(Config.Instrument, schema, id);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Instrument>();
                return rest;
            }
        }

        //Retorna una lista de instruments.
        public async Task<Instruments> getInstruments(string cfg, string type, string source, string schema)
        {
            string url = API.getUrl(cfg, type, source, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Instruments>();
                return rest;
            }
        }

        //Retorna los instrumentos que contengan una cadena de búsqueda como parte del id.
        public async Task<Instruments> searchInstruments(string cfg, string id, string schema)
        {
            string url = String.Format(cfg + Config.FilterId, schema, id);         
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Instruments>();
                return rest;
            }
        }

        #endregion

        #region ReferenceDatas

        //Retorna la lista de instrumentos financieros.
        public async Task<ReferenceDatas> getReferenceDatas(string cfg, string type, string schema)
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
        public async Task<ReferenceDatas> searchReferenceDatas(string cfg, string id, string schema)
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

        #region Reports
        //Devuelve la lista completa de reportes.
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
        #endregion

        #region Types        

        //Devuelve los posibles tipos de datos de los orígenes
        public async Task<Types> getSourceFieldTypes()
        {            
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + Config.SourceFieldTypes)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Types>();
                return rest;
            }
        }

        //Devuelve los tipos de control de las propiedades de los instrumentos. Task<string> getPropertyControlTypes();
        public async Task<Types> getPropertyControlTypes()
        {
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + Config.PropertyControlTypes)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Types>();
                return rest;
            }
        }

        //Devuelve los tipos de control del estado de un instrumento
        public async Task<Types> getStateControlTypes()
        {
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + Config.StateControlTypes)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Types>();
                return rest;
            }
        }

        //Devuelve los tipos de instrumentos
        public async Task<Types> getInstrumentTypes()
        {
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + Config.InstrumentTypes)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Types>();
                return rest;
            }
        }

        //Devuelve los tipos de origen para las propiedades de los instrumentos
        public async Task<Types> getPropertyOriginTypes()
        {
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + Config.PropertyOriginTypes)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Types>();
                return rest;
            }
        }

        //Devuelve los tipos de origen
        public async Task<Types> getSourceTypes()
        {
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + Config.SourceTypes)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Types>();
                return rest;
            }
        }

        #endregion

        #region Mappings
        //Devuelve un mapping para un id específico.
        public async Task<Mapping> getMapping(string id, string schema)
        {
            string url = String.Format(Config.Mapping, schema, id);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Mapping>();
                return rest;
            }
        }

        //Devuelve una lista de mappings.
        public async Task<Mappings> getMappings(string schema)
        {
            string url = String.Format(Config.Mappings, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Mappings>();
                return rest;
            }
        }
        #endregion

        #region SourceFields
        //Devuelve la lista completa de source fields.
        public async Task<SourceFields> getSourceFields(string schema)
        {
            string url = String.Format(Config.SourceFields, schema);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<SourceFields>();
                return rest;
            }
        }
        //Devuelve un source field con un id específico.
        public async Task<SourceField> getSourceField(string id, string schema)
        {
            string url = String.Format(Config.SourceField, schema, id);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<SourceField>();
                return rest;
            }
        }
        #endregion

        #region StatusReports
        //Devuelve el estado de los procesos.
        public async Task<Status> getStatusProcesses()
        {
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + Config.ProcessesStatus)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Status>();
                return rest;
            }
        }
        #endregion

        #region Derivatives
        //Retorna una lista de derivados
        public async Task<Derivatives> getDerivatives(string market, string symbol)
        {
            string url = API.getUrlDerivatives(market, symbol);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Derivatives>();
                return rest;
            }
        }

        //Retorna la lista de derivados que contengan una cadena de búsqueda como parte del id.
        public async Task<Derivatives> searchDerivatives(string id)
        {
            string url = String.Format(Config.Derivatives + Config.FilterId, null, id);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Derivatives>();
                return rest;
            }
        }              
        #endregion

        #region Funds
        //Retorna un fondo por id
        public async Task<Fund> getFund(string id)
        {
            string url = String.Format(Config.Fund, id);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Fund>();
                return rest;
            }
        }        

        //Retorna una lista de fondos
        public async Task<Funds> getFunds(string managment, string depositary, string currency, string rent)
        {
            string url = API.getUrlFunds(managment, depositary, currency, rent);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Funds>();
                return rest;
            }
        }

        //Retorna una lista de fondos filtrado por id
        public async Task<Funds> searchFunds(string id)
        {
            string url = String.Format(Config.Funds + Config.FilterId, null, id);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Funds>();
                return rest;
            }
        }
        #endregion      

        #region Securities
        //Retorna un titulo valor por id
        public async Task<Securitie> getSecuritie(string id)
        {
            string url = String.Format(Config.Securitie, id);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Securitie>();
                return rest;
            }
        }

        //Retorna una lista de títulos valores
        public async Task<Securities> getSecurities(string id)
        {
            string url = (id != null) ?
                String.Format(Config.Securities + Config.FilterId, null, id) :
                String.Format(Config.Securities);
            using (var client = new FluentClient(_baseUrl))
            {
                var rest = await client.GetAsync(API.ver + url)
                    .WithHeader(Config.Header.cache, Config.cache)
                    .WithHeader(Config.Header.key, _key).As<Securities>();
                return rest;
            }
        }
        #endregion

        #region OData

        //Retorna la lista de instrumentos filtrados con OData.
        public async Task<ODataList> getODataReferenceDatas(string query, string schema)
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
        public async Task<ODataList> getODataReferenceDatasById(string id, string schema)
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
        public async Task<ODataList> searchODataReferenceDatas(
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
    }
}