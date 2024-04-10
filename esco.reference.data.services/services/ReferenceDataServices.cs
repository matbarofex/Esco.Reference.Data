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

        #region OData
        
        /// <summary>
        /// Retorna la lista de instrumentos financieros consolidados como string, filtrados con Query en formato OData.
        /// </summary>
        /// <param name="query">(Optional) Query de filtrado en formato OData. Diccionario de campos disponible con el método getReferenceDataSpecification(). (Ejemplo de consulta:"?$top=5 & $filter=type eq 'MF' & $select=currency,name,region" </param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>String Result.</returns>
        public async Task<string> GetConsolidatedAsString(string query = null, string schema = null) =>
            JsonSerializer.Serialize(await GetAsReferenceData(SetUrl(Url.Consolidated + (query ?? Url.FilterAll), schema ?? Schema.actual)), httpClient.Options());


        /// <summary>
        /// Retorna la lista de instrumentos financieros filtrados en un CSV con Query en formato OData.
        /// </summary>
        /// <param name="query">(Optional) Query de filtrado en formato OData. Diccionario de campos disponible con el método getReferenceDataSpecification(). (Ejemplo de consulta:"?$top=5 & $filter=type eq 'MF' & $select=currency,name,region" </param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<Stream> GetCSVByOData(string query = null, string schema = null) =>
            await httpClient.GetAsStream(SetUrl(Url.ODataCSV + (query ?? string.Empty), schema ?? Schema.actual));


        /// <summary>
        /// Retorna la lista de instrumentos financieros en un CSV (compactado en archivo ZIP) filtrados con Query en formato OData.
        /// </summary>
        /// <param name="filePath">(Required) Ruta del directorio donde se guarda el archivo exportado a formato .csv </param>
        /// <param name="fileName">(Required) Nombre del archivo donde se guarda la exportación en formato .csv </param>
        /// <param name="query">(Optional) Query de filtrado en formato OData. Diccionario de campos disponible con el método getReferenceDataSpecification(). (Ejemplo de consulta:"?$top=5 & $filter=type eq 'MF' & $select=currency,name,region" </param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<bool> SaveCSVByOData(string filePath, string fileName, string query = null, string schema = null)
        {
            DirectoryInfo info = new(filePath);
            if (!info.Exists) info.Create();

            try
            {
                using FileStream fileStream = new(Path.Combine(filePath, fileName + ".zip"), FileMode.Create, FileAccess.Write);
                Stream stream = await GetCSVByOData(query, schema);
                await stream.CopyToAsync(fileStream);

                fileStream.Dispose();
                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ReferenceDatasTypes
        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Fondos Comunes de Inversion (MF).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Fondos json.</returns>
        public async Task<Fondos> GetFondos(string schema = null) =>
             JsonSerializer.Deserialize<Fondos>(await GetAsString(Types.Fondos, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Cedears (CD).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Cedears json.</returns>
        public async Task<Cedears> GetCedears(string schema = null) =>
            JsonSerializer.Deserialize<Cedears>(await GetAsString(Types.Cedears, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Acciones (CS).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Acciones.</returns>
        public async Task<Acciones> GetAcciones(string schema = null) =>
            JsonSerializer.Deserialize<Acciones>(await GetAsString(Types.Acciones, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Acciones A.D.R.S.
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Acciones.</returns>
        public async Task<Acciones> GetAccionesADRS(string schema = null) =>
            JsonSerializer.Deserialize<Acciones>(await GetAsString(null, schema, Url.FilterADRS));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Acciones Privadas
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Acciones.</returns>
        public async Task<Acciones> GetAccionesPrivadas(string schema = null) =>
            JsonSerializer.Deserialize<Acciones>(await GetAsString(null, schema, Url.FilterPrivadas));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Acciones PYMES
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Acciones.</returns>
        public async Task<Acciones> GetAccionesPYMES(string schema = null) =>
            JsonSerializer.Deserialize<Acciones>(await GetAsString(null, schema, Url.FilterPymes));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Obligaciones (CORP).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Obligaciones.</returns>
        public async Task<Obligaciones> GetObligaciones(string schema = null) =>
            JsonSerializer.Deserialize<Obligaciones>(await GetAsString(Types.Obligaciones, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Títulos Públicos (GO).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Titulos.</returns>
        public async Task<Titulos> GetTitulos(string schema = null) =>
            JsonSerializer.Deserialize<Titulos>(await GetAsString(Types.Titulos, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Futuros (FUT).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Futuros.</returns>
        public async Task<Futuros> GetFuturos(string schema = null) =>
            JsonSerializer.Deserialize<Futuros>(await GetAsString(Types.Futuros, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Futuros OTC (FUTOTC).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo FuturosOTC json.</returns>
        public async Task<FuturosOTC> GetFuturosOTC(string schema = null) =>
             JsonSerializer.Deserialize<FuturosOTC>(await GetAsString(Types.FuturosOTC, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Opciones de ByMA (OPT).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Opciones.</returns>
        public async Task<Opciones> GetOpciones(string schema = null) =>
            JsonSerializer.Deserialize<Opciones>(await GetAsString(Types.Opciones, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Opciones de MatbaRofex (OOF).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo OpcionesMTR.</returns>
        public async Task<OpcionesMTR> GetOpcionesMTR(string schema = null) =>
            JsonSerializer.Deserialize<OpcionesMTR>(await GetAsString(Types.OpcionesMTR, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo OpcionesOTC (OOFOTC).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Opciones OTC.</returns>
        public async Task<OpcionesOTC> GetOpcionesOTC(string schema = null) =>
            JsonSerializer.Deserialize<OpcionesOTC>(await GetAsString(Types.OpcionesOTC, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Pases (BUYSELL).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Pases.</returns>
        public async Task<Pases> GetPases(string schema = null) =>
            JsonSerializer.Deserialize<Pases>(await GetAsString(Types.Pases, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Cauciones (REPO).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Cauciones.</returns>
        public async Task<Cauciones> GetCauciones(string schema = null) =>
            JsonSerializer.Deserialize<Cauciones>(await GetAsString(Types.Cauciones, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Plazos por Lotes.
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Plazos.</returns>
        public async Task<Plazos> GetPlazos(string schema = null) =>
            JsonSerializer.Deserialize<Plazos>(await GetAsString(Types.T, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Plazos por Lotes.
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Plazos.</returns>
        public async Task<Prestamos> GetPrestamosValores(string schema = null) =>
            JsonSerializer.Deserialize<Prestamos>(await GetAsString(Types.TERM, schema));

        /// <summary>
        /// Retorna la lista de instrumentos financieros de tipo Indice (XLINKD).
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// </summary>     
        /// <returns>Modelo de datos json de tipo Indices.</returns>
        public async Task<Indices> GetIndices(string schema = null) =>
            JsonSerializer.Deserialize<Indices>(await GetAsString(Types.Indices, schema));
        #endregion

        #region Prices
      
        /// <summary>
        /// Retorna los campos de precios actualizados de un instrumento financiero como una cadena.
        /// </summary>
        /// <param name="name">(Requeried) Filtrar por nombre de Instrumento.</param>    
        /// <returns>string</returns>
        public async Task<string> GetPriceAsString(string name = null)
        {
            return JsonSerializer.Serialize(await httpClient.GetPrice(string.Format(Url.PriceByInstrument, name)), httpClient.Options());
        }

        /// <summary>
        /// Retorna los campos de precios actualizados de un instrumento financiero dado.
        /// </summary>
        /// <param name="name">(Requeried) Filtrar por nombre de Instrumentos.</param>      
        /// <returns>Price</returns>
        public async Task<Price> GetPrice(string name = null) =>
            await httpClient.GetPrice(string.Format(Url.PriceByInstrument, name));


        /// <summary>
        /// Retorna la lista de campos de precios actualizados de los instrumentos financieros como una cadena.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por tipo de Instrumentos.</param>  
        /// <returns>string</returns>
        public async Task<string> GetPricesAsString(string type = null) => 
            JsonSerializer.Serialize(await GetAsPrices(type), httpClient.Options());

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