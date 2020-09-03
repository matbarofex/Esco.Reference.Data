using ESCO.Reference.Data.Model;
using System.Threading.Tasks;

namespace ESCO.Reference.Data.Services.Contracts
{
    /// <summary>
    /// Interface Reference Datas Services
    /// </summary>  
    public interface IReferenceDataServices
    {
        void changeSuscriptionKey(string key);      //Cambiar la Suscription Key del usuario

        #region Schemas
        Task<Schema> getSchema();                   //Devuelve el schema de trabajo actual.
        Task<Schemas> getSchemas();                 //Devuelve la lista completa de esquemas.
        Task<Schema> getSchemaId(string id = null); //Devuelve un esquema con un id específico.
        Task<PromoteSchema> getPromoteSchema();     //Verifica si la tarea de promover un schema se está ejecutando        
        #endregion        

        #region Fields
        Task<FieldsList> getFields(string schema);              //Devuelve la lista completa de fields.
        Task<Field> getField(string id, string schema);         //Devuelve un field con un id específico.
        #endregion

        #region Instruments
        Task<SuggestedFields> getInstrumentsSuggestedFields(string schema);                                 //Obtiene una lista de campos sugeridos.
        Task<Instruments> getInstrumentsTodayUpdated(string type, string source, string schema);            //Retorna la lista de instruments actualizados en el día.
        Task<Instruments> getInstrumentsTodayAdded(string type, string source, string schema);              //Retorna la lista de instruments dados de alta en el día.
        Task<Instruments> getInstrumentsTodayRemoved(string type, string source, string schema);            //Retorna la lista de instruments dados de baja en el día.
        Task<InstrumentsReport> getInstrumentsReport(string source, string schema);                               //Retorna un reporte resumido de instruments.
        Task<InstrumentsReport> searchInstrumentsReport(string id, string schema = null);                   // Retorna los instrumentos del reporte resumido contengan una cadena de búsqueda como parte del id.
        Task<Instrument> getInstrument(string id, string schema);                                           //Retorna una instrument por id.
        Task<Instruments> getInstruments(string type, string source, string schema);                        //Retorna una lista de instruments.  
        Task<Instruments> searchInstruments(string id, string schema);                                      // Retorna instrumentos que contenga al menos una parte del id específicado.
        #endregion

        #region ReferenceDatas
        Task<ReferenceDatas> getReferenceDataTodayUpdated(string type, string schema);  //Retorna la lista de instruments actualizados en el día.      
        Task<ReferenceDatas> searchReferenceDataTodayUpdated(string id, string schema); //Retorna la lista de instrumentos actualizados en el día que contengan una cadena de búsqueda como parte del id.
        Task<ReferenceDatas> getReferenceDataTodayAdded(string type, string schema);    //Retorna la lista de instrumentos dados de alta en el día.
        Task<ReferenceDatas> searchReferenceDataTodayAdded(string id, string schema);   //Retorna la lista de instrumentos dados de alta en el día que contengan una cadena de búsqueda como parte del id.
        Task<ReferenceDatas> getReferenceDataTodayRemoved(string type, string schema);  //Retorna la lista de instrumentos dados de baja en el día.
        Task<ReferenceDatas> searchReferenceDataTodayRemoved(string id, string schema); //Retorna la lista de instrumentos dados de baja en el día que contengan una cadena de búsqueda como parte del id.
        Task<ReferenceDatas> getReferenceDatas(string type, string schema);             //Retorna la lista de instrumentos.
        Task<Specification> getReferenceDataSpecification(string schema);               //Retorna una especificación del estado actual.
        #endregion

        #region Reports
        Task<Reports> getReports(string schema);                     //Devuelve la lista completa de reportes.
        Task<Report> getReport(string id, string schema);        //Devuelve un reporte con un id específico.
        #endregion

        #region Types
        Task<Types> getSourceFieldTypes();        //Devuelve los posibles tipos de datos de los orígenes.
        Task<Types> getPropertyControlTypes();    //Devuelve los tipos de control de las propiedades de los instrumentos.
        Task<Types> getStateControlTypes();       //Devuelve los tipos de control del estado de un instrumento.
        Task<Types> getInstrumentTypes();         //Devuelve los tipos de instrumentos.
        Task<Types> getPropertyOriginTypes();     //Devuelve los tipos de origen para las propiedades de los instrumentos.
        Task<Types> getSourceTypes();             //Devuelve los tipos de origen.
        #endregion

        #region Mappings
        Task<Mappings> getMappings(string schema);                    //Devuelve una lista de mappings.
        Task<Mapping> getMapping(string id, string schema);       //Devuelve un mapping para un id específico.
        #endregion

        #region SourceFields
        Task<SourceFields> getSourceFields(string schema);                //Devuelve la lista completa de source fields.
        Task<SourceField> getSourceField(string id, string schema);   //Devuelve un source field con un id específico.
        #endregion

        #region StatusReports
        Task<Status> getStatusProcesses();                            //Devuelve el estado de los procesos.
        #endregion

        #region Derivatives
        Task<Derivatives> getDerivatives(string marketSegmentId, string underlyingSymbol);  //Retorna una lista de derivados
        Task<Derivatives> searchDerivatives(string id);                                     //Retorna una lista de derivados que contengan una cadena de búsqueda como parte del id.
        Task<MarketSegments> getMarketSegments();                                           // Retorna una lista de Segmentos de mercado de derivados (MarketSegmentId).
        Task<UnderlyingSymbols> getUnderlyingSymbols();                                     // Retorna una lista de Símbolos de derivados (underlyingSymbol).
        #endregion

        #region Funds
        Task<Fund> getFund(string id);        //Retorna un fondo por id        
        Task<Funds> getFunds(string managment, string depositary, string currency, string rentType);  //Retorna una lista de fondos
        Task<Funds> searchFunds(string id);    //Retorna una lista de fondos
        #endregion     

        #region Securities
        Task<Securitie> getSecuritie(string id);   //Retorna un titulo valor por id        
        Task<Securities> getSecurities(string id);  //Retorna una lista de títulos valores
        #endregion

        #region OData
        Task<ODataObject> getODataReferenceDatas(string query, string schema);  // Retorna la lista de instrumentos filtrados con OData.
        Task<ODataObject> getODataReferenceDatasById(string id, string schema); // Retorna la lista de instrumentos filtrados por Id.
        Task<ODataObject> searchODataReferenceDatas(string type, string currency, string symbol, string market, string country, string schema); // Retorna la lista de instrumentos filtrados por campos específicos (puede incluirse cadenas de búsqueda parcial).
        #endregion

        #region ESCO
        Task<Custodians> getCustodians(string schema);                      // Retorna la lista de Sociedades Depositarias  
        Task<Managments> getManagements(string schema);                     // Retorna la lista de Sociedades Administradoras
        Task<Rents> getRentTypes(string schema);                            // Retorna la lista de Tipos de Rentas      
        Task<Regions> getRegions(string schema);                            // Retorna la lista de Regiones      
        Task<Currencys> getCurrencys(string schema);                        // Retorna la lista de Monedas      
        Task<Countrys> getCountrys(string schema);                          // Retorna la lista de Países      
        Task<Issuers> getIssuers(string schema);                            // Retorna la lista de Issuers  
        Task<Horizons> getHorizons(string schema);                          // Retorna la lista de Horizon 
        Task<FundTypes> getFundTypes(string schema);                        // Retorna la lista de Tipos de Fondos
        Task<Benchmarks> getBenchmarks(string schema);                      // Retorna la lista de Benchmarks
        Task<ReferenceDataTypes> getReferenceDataTypes(string schema);      // Retorna la lista de tipos de instrumentos financieros
        Task<ReferenceDataSymbols> getReferenceDataSymbols(string schema);  // Retorna la lista de Símbolos (UnderlyingSymbol) de Instrumentos financieros
        Task<Markets> getMarkets(string schema);                            // Retorna la lista de Mercados para los instrumentos financieros
        #endregion
    }
}
