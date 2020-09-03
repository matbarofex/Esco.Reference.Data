using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESCO.Reference.Data.Model;
using ESCO.Reference.Data.Services.Contracts;

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
        /// <param name="version">(Optional) Definir version demo o de producción de la Api (parametros aceptados: "demo", "ro", si es null toma la version de producción por defecto).</param>                
        /// <param name="host">(Optional) Dirección url de la API Reference Data. Si es null toma el valor por defecto: https://apids.primary.com.ar/ </param>
        /// <returns></returns>
        public ReferenceDataServices(string key, string version = null, string host = null)
        {
            _httpClient = new ReferenceDataHttpClient(key, version, host);
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
            Schema _schema = await _httpClient.getSchema();
            return _schema.id;
        }

        private async Task<string> getSourceType(string value, bool type)
        {           
            try
            {
                Int32.Parse(value);                
            }
            catch
            {
                Types _type = (type)? await _httpClient.getInstrumentTypes(): 
                    await _httpClient.getSourceTypes();
                if (_type.Count > 0)
                {
                    TypeField types = _type.Where(s => s.description == value).FirstOrDefault();
                    value = (types != null) ? types.code.ToString() : value;
                }
            }
            return value;
        }
        private async Task<string> getSourceReport(string value)
        {
            try
            {
                int intValue = Int32.Parse(value);
                Types _type = await _httpClient.getSourceTypes();
                if (_type.Count > 0)
                {
                    TypeField types = _type.Where(s => s.code == intValue).FirstOrDefault();
                    value = (types != null) ? types.description : value;
                }
            }
            catch
            {
                //
            }
            return value;
        }

        private async Task<string> getMarketsTypes(string value)
        {
            try
            {
                int intValue = Int32.Parse(value);
                Types _markets = await _httpClient.getSourceTypes();
                if (_markets.Count > 0)
                {
                    TypeField markets = _markets.Where(s => s.code == intValue).FirstOrDefault();
                    value = (markets != null) ? markets.description : value;
                }
            }
            catch
            {
                //
            }
            return value;
        }

        private async Task<string> getTypes(string value, string schema)
        {
            try
            {
                int intValue = Int32.Parse(value);
                ReferenceDataTypes _type = await getReferenceDataTypes(schema);
                if (_type.Count > 0)
                {
                    ReferenceDataType types = _type.Where(s => s.id == value).FirstOrDefault();
                    value = (types != null) ? types.type : value;
                }
            }
            catch
            {
                //
            }
            return value;
        }

        #region Schemas
        /// <summary>
        /// Devuelve el schema de trabajo actual.
        /// </summary>
        /// <param></param>
        /// <returns>Schema object result</returns>
        public async Task<Schema> getSchema()
        {
            try
            {
                return await _httpClient.getSchema();
            }
            catch (Exception e)
            {
                throw e;
            }
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

        /// <summary>
        /// Devuelve un esquema con un id específico.
        /// </summary>
        /// <param name="id">(Optional) Id del esquema. Si es null devuelve el esquema activo</param>
        /// <returns>Schema object Result.</returns>
        public async Task<Schema> getSchemaId(string id = null)
        {
            try
            {
                if (id == null)
                {
                    Schema schema = await _httpClient.getSchema();
                    id = (schema != null) ? schema.id : "0";
                }

                return await _httpClient.getSchemaId(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Verifica si la tarea de promover un schema se está ejecutando
        /// </summary>
        /// <param></param>
        /// <returns>PromoteSchema object Result.</returns>
        public async Task<PromoteSchema> getPromoteSchema()
        {
            try
            {
                return await _httpClient.getPromoteSchema();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion       

        #region Fields
        /// <summary>
        /// Devuelve la lista completa de fields.
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>FieldsList object Result.</returns>
        public async Task<FieldsList> getFields(string schema = null)
        {
            try
            {
                schema = (schema == null) ? await getSchemaActive() : schema;
                return await _httpClient.getFields(schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Devuelve un field con un id específico.
        /// </summary>
        /// <param name="id">(Required) Id del Field a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Field object Result.</returns>
        public async Task<Field> getField(string id, string schema = null)
        {
            try
            {
                schema = (schema == null) ? await getSchemaActive() : schema;
                return await _httpClient.getField(id, schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Instruments

        /// <summary>
        /// Obtiene una lista de campos sugeridos.
        /// </summary>        
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>SuggestedFields object Result.</returns>
        public async Task<SuggestedFields> getInstrumentsSuggestedFields(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                return await _httpClient.getInstrumentsSuggestedFields(schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna la lista de instrumentos actualizados en el día.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="source">(Optional) Filtrar por mercado (source). Valores permitidos: "ROFEX", "CAFCI", "BYMA". Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Instruments object Result.</returns>
        public async Task<Instruments> getInstrumentsTodayUpdated(string type = null, string source = null, string schema = null) 
        {
            return await _httpGetInstruments(Config.InstrumentsTodayUpdated, type, source, schema);
        }

        /// <summary>
        /// Retorna los instrumentos actualizados en el día que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos actualizados en el día a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Instruments object Result.</returns>
        public async Task<Instruments> searchInstrumentsTodayUpdated(string id, string schema = null)
        {
            return await _httpSearchInstruments(Config.InstrumentsTodayUpdated, id, schema);
        }

        /// <summary>
        /// Retorna la lista de instrumentos dados de alta en el día.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="source">(Optional) Filtrar por mercado (source). Valores permitidos: "ROFEX", "CAFCI", "BYMA". Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Instruments object Result.</returns>
        public async Task<Instruments> getInstrumentsTodayAdded(string type = null, string source = null, string schema = null)
        {
            return await _httpGetInstruments(Config.InstrumentsTodayAdded, type, source, schema);
        }

        /// <summary>
        /// Retorna los instrumentos dados de alta en el día que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos dados de alta en el día a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Instruments object Result.</returns>
        public async Task<Instruments> searchInstrumentsTodayAdded(string id, string schema = null)
        {
            return await _httpSearchInstruments(Config.InstrumentsTodayAdded, id, schema);
        }

        /// <summary>
        /// Retorna la lista de instrumentos dados de baja en el día.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="source">(Optional) Filtrar por mercado (source). Valores permitidos: "ROFEX", "CAFCI", "BYMA". Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Instruments object Result.</returns>
        public async Task<Instruments> getInstrumentsTodayRemoved(string type = null, string source = null, string schema = null)
        {
            return await _httpGetInstruments(Config.InstrumentsTodayRemoved, type, source, schema);
        }

        /// <summary>
        /// Retorna los instrumentos dados de baja en el día que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos dados de baja en el día a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Instruments object Result.</returns>
        public async Task<Instruments> searchInstrumentsTodayRemoved(string id, string schema = null)
        {
            return await _httpSearchInstruments(Config.InstrumentsTodayRemoved, id, schema);
        }

        /// <summary>
        /// Retorna un reporte resumido de instrumentos.
        /// </summary>
        /// <param name="source">(Optional) Filtrar por tipo de mercado (source). Valores permitidos: "ROFEX", "CAFCI", "BYMA". Si es null devuelve la lista completa.</param>      
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>InstrumentsReport object Result.</returns>
        public async Task<InstrumentsReport> getInstrumentsReport(string source = null, string schema = null)
        {
            Response result = new Response();
            try
            {
                schema = schema ?? await getSchemaActive();
                source = (source != null) ? await getSourceReport(source) : source;
                return await _httpClient.getInstrumentsReport(source, schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna los instrumentos del reporte resumido contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">">(Requeried) Cadena de búsqueda del Id de los Instrumentos del reporte resumido a filtrar</param>      
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>InstrumentsReport object Result.</returns>
        public async Task<InstrumentsReport> searchInstrumentsReport(string id, string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                return await _httpClient.searchInstrumentsReport(id, schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna una instrumento por id.
        /// </summary>
        /// <param name="id">(Requeried) Id del Instrumento a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Instrument object Result.</returns>
        public async Task<Instrument> getInstrument(string id, string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                return await _httpClient.getInstrument(id, schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna los instrumentos que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Instruments object Result.</returns>
        public async Task<Instruments> searchInstruments(string id, string schema = null)
        {
            return await _httpSearchInstruments(Config.Instruments, id, schema);
        }

        /// <summary>
        /// Retorna una lista de instrumentos.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve todos los tipos de Instrumentos.</param>
        /// <param name="source">(Optional) Filtrar por mercado (source). Valores permitidos: "ROFEX", "CAFCI", "BYMA". Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Instruments object Result.</returns>
        public async Task<Instruments> getInstruments(string type = null, string source = null, string schema = null)
        {
            return await _httpGetInstruments(Config.Instruments, type, source, schema);
        }

        private async Task<Instruments> _httpGetInstruments(string cfg, string type, string source, string schema)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                type = (type != null) ? await getSourceType(type, true) : type;
                source = (source != null) ? await getSourceType(source, false) : source;
                return await _httpClient.getInstruments(cfg, type, source, schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<Instruments> _httpSearchInstruments(string cfg, string id, string schema)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                return await _httpClient.searchInstruments(cfg, id, schema);
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
            return await _httpReferenceDatas(Config.TodayUpdated, type, schema, false);
        }

        /// <summary>
        /// Retorna la lista de instrumentos actualizados en el día que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos actualizados en el día a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> searchReferenceDataTodayUpdated(string id, string schema = null)
        {
            return await _httpReferenceDatas(Config.TodayUpdated, id, schema, true);
        }

        /// <summary>
        /// Retorna la lista de instrumentos dados de alta en el día.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> getReferenceDataTodayAdded(string type = null, string schema = null)
        {
            return await _httpReferenceDatas(Config.TodayAdded, type, schema, false);
        }

        /// <summary>
        /// Retorna la lista de instrumentos dados de alta en el día que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos dados de alta en el día a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> searchReferenceDataTodayAdded(string id, string schema = null)
        {
            return await _httpReferenceDatas(Config.TodayAdded, id, schema, true);
        }

        /// <summary>
        /// Retorna la lista de instrumentos dados de baja en el día.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> getReferenceDataTodayRemoved(string type = null, string schema = null)
        {
            return await _httpReferenceDatas(Config.TodayRemoved, type, schema, false);
        }

        /// <summary>
        /// Retorna la lista de instrumentos dados de baja en el día que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos dados de baja en el día a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> searchReferenceDataTodayRemoved(string id, string schema = null)
        {
            return await _httpReferenceDatas(Config.TodayRemoved, id, schema, true);
        }

        /// <summary>
        /// Retorna la lista de instrumentos financieros.
        /// </summary>
        /// <param name="type">(Optional) Filtrar por Id del tipo de Instrumentos. Si es null devuelve la lista completa.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>        
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> getReferenceDatas(string type = null, string schema = null)
        {
            return await _httpReferenceDatas(Config.ReferenceDatas, type, schema, false);
        }

        /// <summary>
        /// Retorna los Instrumentos financieros que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Instrumentos financieros a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>        
        /// <returns>ReferenceDatas object Result.</returns>
        public async Task<ReferenceDatas> searchReferenceDatas(string id, string schema = null)
        {
            return await _httpReferenceDatas(Config.ReferenceDatas, id, schema, true);
        }

        private async Task<ReferenceDatas> _httpReferenceDatas(string cfg, string str, string schema, bool search)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                str = (!search) ? await getTypes(str, schema) : str;
                return (search) ?
                    await _httpClient.searchReferenceDatas(cfg, str, schema) :
                    await _httpClient.getReferenceDatas(cfg, str, schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna una especificación del estado actual.
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

        #region Reports        
        /// <summary>
        /// Devuelve la lista completa de reportes.
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Reports object Result.</returns>
        public async Task<Reports> getReports(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                return await _httpClient.getReports(schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Devuelve un reporte con un id específico.
        /// </summary>
        /// <param name="id">(Requeried) Id del Reporte a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Report object Result.</returns>
        public async Task<Report> getReport(string id, string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                return await _httpClient.getReport(id, schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Types

        /// <summary>
        /// Devuelve los posibles tipos de datos de los orígenes.
        /// </summary>
        /// <returns>Types object Result.</returns>
        public async Task<Types> getSourceFieldTypes()
        {
            try
            {
                return await _httpClient.getSourceFieldTypes();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Devuelve los tipos de control de las propiedades de los instrumentos
        /// </summary>       
        /// <returns>Types object Result.</returns>
        public async Task<Types> getPropertyControlTypes()
        {
            try
            {
                return await _httpClient.getPropertyControlTypes();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Devuelve los tipos de control del estado de un instrumento
        /// </summary>       
        /// <returns>Types object Result.</returns>
        public async Task<Types> getStateControlTypes()
        {
            try
            {
                return await _httpClient.getStateControlTypes();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Devuelve los tipos de instrumentos
        /// </summary>       
        /// <returns>Types object Result.</returns>
        public async Task<Types> getInstrumentTypes()
        {
            try
            {
                return await _httpClient.getInstrumentTypes();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Devuelve los tipos de origen para las propiedades de los instrumentos
        /// </summary>       
        /// <returns>object Result.</returns>
        public async Task<Types> getPropertyOriginTypes()
        {
            try
            {
                return await _httpClient.getPropertyOriginTypes();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Devuelve los tipos de origen
        /// </summary>       
        /// <returns>Types object Result.</returns>
        public async Task<Types> getSourceTypes()
        {
            try
            {
                return await _httpClient.getSourceTypes();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Mappings        
        /// <summary>
        /// Devuelve un mapping para un id específico.
        /// </summary>
        /// <param name="id">(Requeried) Id del Mapping a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Mapping object Result.</returns>
        public async Task<Mapping> getMapping(string id = null, string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                return await _httpClient.getMapping(id, schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Devuelve una lista de mappings.
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>Mappings object Result.</returns>
        public async Task<Mappings> getMappings(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                return await _httpClient.getMappings(schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region SourceFields        
        /// <summary>
        /// Devuelve la lista completa de source fields.
        /// </summary>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>SourceFields object Result.</returns>
        public async Task<SourceFields> getSourceFields(string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                return await _httpClient.getSourceFields(schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Devuelve un source field con un id específico.
        /// </summary>
        /// <param name="id">(Requeried) Id del Source Field a filtrar.</param>
        /// <param name="schema">(Optional) Id del esquema de devolución de la información. Si es null se toma por defecto el esquema activo.</param>
        /// <returns>SourceField object Result.</returns>
        public async Task<SourceField> getSourceField(string id, string schema = null)
        {
            try
            {
                schema = schema ?? await getSchemaActive();
                return await _httpClient.getSourceField(id, schema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region StatusReports        
        /// <summary>
        /// Devuelve el estado de los procesos.
        /// </summary>
        /// <returns>Status object Result.</returns>
        public async Task<Status> getStatusProcesses()
        {
            try
            {
                return await _httpClient.getStatusProcesses();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Derivatives        
        /// <summary>
        /// Retorna una lista de derivados
        /// </summary>
        /// <param name="marketSegmentId">(Optional) Id del segmento de mercado (Ej: "DDA", "MATBA", puede incluirse una cadena de búsqueda parcial)</param>
        /// <param name="underlyingSymbol">(Optional) Símbolo del Derivado (Ej: "Indice Novillo Pesos", puede incluirse una cadena de búsqueda parcial).</param>
        /// <returns>Derivatives object Result.</returns>
        public async Task<Derivatives> getDerivatives(string marketSegmentId = null, string underlyingSymbol = null)
        {
            try
            {
                return await _httpClient.getDerivatives(marketSegmentId, underlyingSymbol);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna una lista de derivados que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Derivados a filtrar.</param>        
        /// <returns>Instruments object Result.</returns>
        public async Task<Derivatives> searchDerivatives(string id)
        {
            try
            {
                return await _httpClient.searchDerivatives(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna una lista de Segmentos de mercado de derivados (MarketSegmentId).
        /// </summary>       
        /// <returns>MarketSegments object Result.</returns>
        public async Task<MarketSegments> getMarketSegments()
        {
            try
            {
                MarketSegments markets = new MarketSegments();
                Derivatives rest = await _httpClient.getDerivatives(null, null);
                if (rest != null && rest.Count > 0)
                {
                    DerivativesList restMarkets = new DerivativesList();
                    restMarkets.value = rest
                        .GroupBy(x => x.marketSegmentId)
                        .Select(x => x.First())
                        .OrderBy(x => x.marketSegmentId)
                        .ToList();
                    for (int i = 0; i < restMarkets.value.Count; i++)
                    {
                        MarketSegment market = new MarketSegment();
                        market.marketSegmentId = restMarkets.value[i].marketSegmentId;
                        markets.Add(market);
                    }
                }
                return markets;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna una lista de Símbolos de derivados (underlyingSymbol).
        /// </summary>       
        /// <returns>MarketSegments object Result.</returns>
        public async Task<UnderlyingSymbols> getUnderlyingSymbols()
        {
            try
            {
                UnderlyingSymbols symbols = new UnderlyingSymbols();
                Derivatives rest = await _httpClient.getDerivatives(null, null);
                if (rest != null && rest.Count > 0)
                {
                    DerivativesList restMarkets = new DerivativesList();
                    restMarkets.value = rest
                        .GroupBy(x => x.underlyingSymbol)
                        .Select(x => x.First())
                        .OrderBy(x => x.underlyingSymbol)
                        .ToList();
                    for (int i = 0; i < restMarkets.value.Count; i++)
                    {
                        UnderlyingSymbol symbol = new UnderlyingSymbol();
                        symbol.underlyingSymbol = restMarkets.value[i].underlyingSymbol;
                        symbols.Add(symbol);
                    }
                }
                return symbols;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Funds        
        /// <summary>
        /// Retorna un fondo por id
        /// </summary>
        /// <param name="id">(Requeried) Id del Fondo a filtrar.</param>
        /// <returns>Fund object Result.</returns>
        public async Task<Fund> getFund(string id)
        {
            try
            {
                return await _httpClient.getFund(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna una lista de fondos filtrado por campos específicos
        /// </summary>
        /// <param name="managment">(Optional) Filtar por Id de la Sociedad de Administración</param>
        /// <param name="depositary">(Optional) Filtar por Id de la Sociedad Depositaria</param>
        /// <param name="currency">(Optional) Filtar por Moneda (Ejemplo: "ARS", "USD")</param>
        /// <param name="rentType">(Optional) Filtar por Id del Tipo de Renta</param>
        /// <returns>Funds object Result.</returns>
        public async Task<Funds> getFunds(
            string managment = null,
            string depositary = null,
            string currency = null,
            string rentType = null)
        {
            try
            {
                return await _httpClient.getFunds(managment, depositary, currency, rentType);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna una lista de fondos que contengan una cadena de búsqueda como parte del id.
        /// </summary>
        /// <param name="id">(Requeried) Cadena de búsqueda del Id de los Fondos a filtrar. Si es null devuelve todos los Fondos</param>
        /// <returns>Funds object Result.</returns>
        public async Task<Funds> searchFunds(string id)
        {
            try
            {
                return await _httpClient.searchFunds(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion      

        #region Securities        
        /// <summary>
        /// Retorna un titulo valor por id
        /// </summary>
        /// <param name="id">(Requeried) Id del título valor a filtrar.</param>
        /// <returns>Securitie object Result.</returns>
        public async Task<Securitie> getSecuritie(string id)
        {
            try
            {
                return await _httpClient.getSecuritie(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Retorna una lista de títulos valores
        /// </summary>
        /// <param name="id">(Optional) Cadena de búsqueda de del Id de los títulos valores a filtrar. Si es null devuelve todos los títulos valores</param>
        /// <returns>Securities object Result.</returns>
        public async Task<Securities> getSecurities(string id = null)
        {
            try
            {
                return await _httpClient.getSecurities(id);
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
        public async Task<ODataObject> getODataReferenceDatas(string query = null, string schema = null)
        {
            try
            {
                schema = schema ?? "2";
                query = query ?? String.Empty;
                ODataList list = await _httpClient.getODataReferenceDatas(query, schema);

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
        public async Task<ODataObject> getODataReferenceDatasById(string id, string schema = null)
        {
            try
            {
                schema = schema ?? "2";
                ODataList list = await _httpClient.getODataReferenceDatasById(id, schema);

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
        public async Task<ODataObject> searchODataReferenceDatas(
            string type = null,
            string currency = null,
            string symbol = null,
            string market = null,
            string country = null,
            string schema = null)
        {
            try
            {
                schema = schema ?? "2";
                type = await getTypes(type, schema);
                market = await getMarketsTypes(market);

                ODataList list = await _httpClient.searchODataReferenceDatas(
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
                schema = schema ?? "2";
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
                schema = schema ?? "2";
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
                schema = schema ?? "2";
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
                schema = schema ?? "2";
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
                schema = schema ?? "2";
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
                schema = schema ?? "2";
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
                schema = schema ?? "2";
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
                schema = schema ?? "2";
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
                schema = schema ?? "2";
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
                schema = schema ?? "2";
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
                schema = schema ?? "2";
                
                ReferenceDataTypes types = new ReferenceDataTypes();
                ReferenceDataTypes list = new ReferenceDataTypes();
                ReferenceDataTypesList rest = await _httpClient.getReferenceDataTypes(schema);

                Types instr = await _httpClient.getInstrumentTypes();

                if (rest != null && rest.value.Count > 0)
                {
                    rest.value
                        .GroupBy(x => x.type)
                        .Select(x => x.First())
                        .ToList()
                        .ForEach(x => list.Add(x));

                    for (int i = 0; i < list.Count; i++)
                    {
                        TypeField id = instr.Find(x => x.description == list[i].type);
                        list[i].id = (id != null) ? id.code.ToString() : String.Empty;
                    }
                    list.OrderBy(x => Int32.Parse(x.id)).ToList().ForEach(x => types.Add(x));
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
                schema = schema ?? "2";

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
                schema = schema ?? "2";
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
    }
}