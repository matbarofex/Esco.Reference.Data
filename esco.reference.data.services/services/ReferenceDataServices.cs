using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ESCO.Reference.Data.Model;
using ESCO.Reference.Data.Services.Contracts;
using Newtonsoft.Json;

namespace ESCO.Reference.Data.Services
{
    /// <summary>
    /// Servicios Reference Datas Conector que se integra con el Servicio PMYDS - Reference Data de Primary .
    /// </summary>   
    public class ReferenceDataServices : IReferenceDataServices
    {
        private ReferenceDataHttpClient _httpClient;

        /// <summary>
        /// Inicialización del servicio API de Reference Datas.
        /// </summary>
        /// <param name="key">(Required) Suscription key del usuario. Requerido para poder operar en la API (Solicitar habilitación de la suscripción despues de la creación de cuenta).</param>                              
        /// <param name="host">(Optional) Dirección url de la API Reference Data. Si es null toma el valor por defecto: https://apids.primary.com.ar/ </param>
        /// <returns></returns>
        public ReferenceDataServices(string key, string host = null)
        {
            _httpClient = new ReferenceDataHttpClient(key, host);
        }

        /// <summary>
        /// Cambiar la Suscription Key del usuario.
        /// </summary>
        /// <param name="key">(Required) Suscription key del usuario. Requerido para poder operar en la API (Solicitar habilitación de la suscripción despues de la creación de cuenta).</param>                        
        /// <returns></returns>
        public void changeSuscriptionKey(string key)
        {
            _httpClient.changeKey(key);
        }

        private async Task<string> getSchemaActive()
        {
            Schemas schemas = await getSchemas();
            Schema schema = schemas.Find(match: x => x.active == true);
            return (schema != null) ? schema.id : "2";
        }

        #region Schemas

        /// <summary>
        /// Devuelve el esquema activo actual.
        /// </summary>
        /// <param></param>
        /// <returns>Schemas object Result.</returns>
        public async Task<Schema> getSchema()
        {
            Schemas schemas = await getSchemas();
            return schemas.Find(match: x => x.active == true);
        }

        /// <summary>
        /// Devuelve la lista completa de esquemas.
        /// </summary>
        /// <param></param>
        /// <returns>Schemas object Result.</returns>
        public async Task<Schemas> getSchemas()
        {
            try
            {
                return await _httpClient.getSchemas();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region OData

        /// <summary>
        /// Retorna la lista de instrumentos financieros filtrados con Query en formato OData.
        /// </summary>
        /// <param name="query">(Optional) Query de filtrado en formato OData. Diccionario de campos disponible con el método getReferenceDataSpecification("2"). (Ejemplo de consulta:"?$top=5&$filter=type eq 'MF'&$select=Currency,Symbol,UnderlyingSymbol" </param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ODataObject> getODataReferenceData(string query = null, string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                query = query ?? String.Empty;
                ODataList list = await _httpClient.getODataReferenceData(query, schema);

                return (list != null) ? list.value : new ODataObject();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de instrumentos financieros filtrados por Id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda de los Instrumentos por Id.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>OData object Result.</returns>
        public async Task<ODataObject> getODataReferenceDataById(string id, string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                ODataList list = await _httpClient.getODataReferenceDataById(id, schema);

                return (list != null) ? list.value : new ODataObject();
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de instrumentos financieros filtrados por campos específicos (puede incluirse cadenas de búsqueda parcial).
        /// </summary>
        /// <param name="type">(Optional) Filtrar por tipo de Instrumentos financiero (Ej: "MF","FUT", "OPC", puede incluirse una cadena de búsqueda parcial.</param>
        /// <param name="currency">(Optional) Filtrar por tipo de Moneda. (Ej: "ARS", puede incluirse una cadena de búsqueda parcial)</param>
        /// <param name="symbol">(Optional) Filtrar por símbolo de Instrumentos (Ej: "AULA", puede incluirse una cadena de búsqueda parcial).</param>        
        /// <param name="market">(Optional) Filtrar por Tipo de Mercado. (Ej "ROFX", "BYMA", puede incluirse una cadena de búsqueda parcial)</param>       
        /// <param name="country">(Optional) Filtrar por nombre de País (Ej: "ARG", puede incluirse una cadena de búsqueda parcial).</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema "2".</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ODataObject> searchODataReferenceData(
            string type = null,
            string currency = null,
            string symbol = null,
            string market = null,
            string country = null,
            string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();

                ODataList list = await _httpClient.searchODataReferenceData(
                    type,
                    currency,
                    symbol,
                    market,
                    country,
                    schema);

                return (list != null) ? list.value : new ODataObject();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region ReferenceDatas        

        /// <summary>
        /// Retorna la lista de instrumentos actualizados en el día.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> getReferenceDataTodayUpdated(string type = null, string schema = null)
        {
            return await _httpReferenceData(Config.TodayUpdated, type, schema, false);
        }

        /// <summary>
        /// Retorna la lista de instrumentos actualizados en el día que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos actualizados en el día a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> searchReferenceDataTodayUpdated(string id, string schema = null)
        {
            return await _httpReferenceData(Config.TodayUpdated, id, schema, true);
        }

        /// <summary>
        /// Retorna la lista de instrumentos dados de alta en el día.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> getReferenceDataTodayAdded(string type = null, string schema = null)
        {
            return await _httpReferenceData(Config.TodayAdded, type, schema, false);
        }

        /// <summary>
        /// Retorna la lista de instrumentos dados de alta en el día que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos dados de alta en el día a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> searchReferenceDataTodayAdded(string id, string schema = null)
        {
            return await _httpReferenceData(Config.TodayAdded, id, schema, true);
        }

        /// <summary>
        /// Retorna la lista de instrumentos dados de baja en el día.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> getReferenceDataTodayRemoved(string type = null, string schema = null)
        {
            return await _httpReferenceData(Config.TodayRemoved, type, schema, false);
        }

        /// <summary>
        /// Retorna la lista de instrumentos dados de baja en el día que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos dados de baja en el día a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> searchReferenceDataTodayRemoved(string id, string schema = null)
        {
            return await _httpReferenceData(Config.TodayRemoved, id, schema, true);
        }

        /// <summary>
        /// Retorna la lista de instrumentos financieros.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>        
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> getReferenceData(string type = null, string schema = null)
        {
            return await _httpReferenceData(Config.ReferenceDatas, type, schema, false);
        }

        /// <summary>
        /// Retorna los Instrumentos financieros que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos financieros a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>        
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> searchReferenceData(string id, string schema = null)
        {
            return await _httpReferenceData(Config.ReferenceDatas, id, schema, true);
        }

        private async Task<ReferenceDatas> _httpReferenceData(string cfg, string str, string schema, bool search)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                //str = (!search) ? await getTypes(str, schema) : str;
                return (search) ?
                    await _httpClient.searchReferenceData(cfg, str, schema) :
                    await _httpClient.getReferenceData(cfg, str, schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna una especificación del estado actual de los modelos de datos Reference Data
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Specification object Result.</returns>
        public async Task<Specification> getReferenceDataSpecification(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                return await _httpClient.getSpecification(schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region ESCO

        /// <summary>
        /// Retorna la lista de Sociedades Depositarias o Custodia de Fondos
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Depositary object Result.</returns>
        public async Task<Custodians> getCustodians(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                Custodians custodians = new Custodians();
                CustodiansList rest = await _httpClient.getCustodians(schema);
                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.FundCustodianId)
                        .Select(x => x.First())
                        .OrderBy(x => Int32.Parse(x.FundCustodianId))
                        .ToList<Custodian>()
                        .ForEach(x => custodians.Add(x));
                }
                return custodians;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de Sociedades Administradoras de Fondos  
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Managers object Result.</returns>
        public async Task<Managments> getManagements(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                Managments managments = new Managments();
                ManagmentsList rest = await _httpClient.getManagements(schema);
                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.FundManagerId)
                        .Select(x => x.First())
                        .OrderBy(x => Int32.Parse(x.FundManagerId))
                        .ToList()
                        .ForEach(x => managments.Add(x));
                }
                return managments;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de Tipos de Rentas
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Rents object Result.</returns>
        public async Task<Rents> getRentTypes(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                Rents rents = new Rents();
                RentsList rest = await _httpClient.getRentTypes(schema);
                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.RentTypeId)
                        .Select(x => x.First())
                        .OrderBy(x => Int32.Parse(x.RentTypeId))
                        .ToList()
                        .ForEach(x => rents.Add(x));
                }
                return rents;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de Regiones
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Regions object Result.</returns>
        public async Task<Regions> getRegions(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                Regions regions = new Regions();
                RegionsList rest = await _httpClient.getRegions(schema);
                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.RegionId)
                        .Select(x => x.First())
                        .OrderBy(x => Int32.Parse(x.RegionId))
                        .ToList()
                        .ForEach(x => regions.Add(x));
                }
                return regions;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de Monedas  
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Currencys object Result.</returns>
        public async Task<Currencys> getCurrencys(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                Currencys currencys = new Currencys();
                CurrencysList rest = await _httpClient.getCurrencys(schema);
                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.Currency)
                        .Select(x => x.First())
                        .ToList()
                        .ForEach(x => currencys.Add(x));
                }
                return currencys;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de Países  
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Countrys object Result.</returns>
        public async Task<Countrys> getCountrys(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                Countrys countrys = new Countrys();
                CountrysList rest = await _httpClient.getCountrys(schema);
                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.Country)
                        .Select(x => x.First())
                        .OrderBy(x => x.Country)
                        .ToList()
                        .ForEach(x => countrys.Add(x));
                }
                return countrys;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de Issuers 
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Issuers object Result.</returns>
        public async Task<Issuers> getIssuers(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                Issuers issuers = new Issuers();
                IssuersList rest = await _httpClient.getIssuers(schema);
                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.Issuer)
                        .Select(x => x.First())
                        .OrderBy(x => x.Issuer)
                        .ToList()
                        .ForEach(x => issuers.Add(x));
                }
                return issuers;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de Horizons
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Horizons object Result.</returns>
        public async Task<Horizons> getHorizons(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                Horizons horizons = new Horizons();
                HorizonsList rest = await _httpClient.getHorizons(schema);
                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.HorizonId)
                        .Select(x => x.First())
                        .OrderBy(x => Int32.Parse(x.HorizonId))
                        .ToList()
                        .ForEach(x => horizons.Add(x));
                }
                return horizons;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de Tipos de Fondos
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>FundTypes object Result.</returns>
        public async Task<FundTypes> getFundTypes(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                FundTypes types = new FundTypes();
                FundTypesList rest = await _httpClient.getFundTypes(schema);
                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.FundTypeId)
                        .Select(x => x.First())
                        .OrderBy(x => Int32.Parse(x.FundTypeId))
                        .ToList()
                        .ForEach(x => types.Add(x));
                }
                return types;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de Benchmarks
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Benchmarks object Result.</returns>
        public async Task<Benchmarks> getBenchmarks(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                Benchmarks benchmarks = new Benchmarks();
                BenchmarksList rest = await _httpClient.getBenchmarks(schema);
                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.FundBenchmarkId)
                        .Select(x => x.First())
                        .OrderBy(x => Int32.Parse(x.FundBenchmarkId))
                        .ToList()
                        .ForEach(x => benchmarks.Add(x));
                }
                return benchmarks;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de Tipos de Instrumentos financieros
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDataTypes object Result.</returns>
        public async Task<ReferenceDataTypes> getReferenceDataTypes(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                Specification specs = await getReferenceDataSpecification(schema);

                ReferenceDataTypes types = new ReferenceDataTypes();
                for (int i = 0; i < specs.instrumentTypes.Count; i++)
                {
                    ReferenceDataType type = new ReferenceDataType();
                    type.id = i.ToString();
                    type.type = specs.instrumentTypes[i];
                    types.Add(type);
                }
                return types;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de Símbolos (UnderlyingSymbol) de Instrumentos financieros
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDataTypes object Result.</returns>
        public async Task<ReferenceDataSymbols> getReferenceDataSymbols(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();

                ReferenceDataSymbols types = new ReferenceDataSymbols();
                ReferenceDataSymbolsList rest = await _httpClient.getReferenceDataSymbols(schema);

                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.UnderlyingSymbol)
                        .Select(x => x.First())
                        .OrderBy(x => x.UnderlyingSymbol)
                        .ToList()
                        .ForEach(x => types.Add(x));
                }
                return types;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de Mercados para los Instrumentos financieros
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Markets object Result.</returns>
        public async Task<Markets> getMarkets(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                Markets markets = new Markets();
                MarketsList rest = await _httpClient.getMarkets(schema);
                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.MarketId)
                        .Select(x => x.First())
                        .OrderBy(x => x.MarketId)
                        .ToList()
                        .ForEach(x => markets.Add(x));
                }
                return markets;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion 

        #region Reports
        /// <summary>
        /// Devuelve la lista completa de reportes.
        /// </summary>        
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Reports object Result.</returns>
        public async Task<Reports> getReports(string schema = null)
        {
            schema = schema ?? await getSchemaActive();
            return await _httpClient.getReports(schema);
        }

        /// <summary>
        /// Devuelve un reporte con un id específico.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id del Reporte a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Report object Result.</returns>
        public async Task<Report> getReport(string id, string schema = null)
        {
            schema = schema ?? await getSchemaActive();
            return await _httpClient.getReport(id, schema);
        }

        /// <summary>
        /// Retorna un reporte resumido de instrumentos.
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Stream object Result.</returns>
        public async Task<string> getInstrumentsReport(string schema = null)
        {
            schema = schema ?? await getSchemaActive();
            return  await _httpClient.getInstrumentsReport(schema);
        }

        #endregion
    }
}