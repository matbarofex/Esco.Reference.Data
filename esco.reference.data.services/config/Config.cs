using System;

namespace ESCO.Reference.Data.Services
{
    class Config
    {   
        public static string url = "https://apids.primary.com.ar/";
        public static string cache = "no-cache";

        public class Header
        {
            public static string key = "Ocp-Apim-Subscription-Key";
            public static string cache = "Cache-Control";
            public static string xversion = "X-Version";
        }

        #region Filters

        //Generals Filters
        public static string FilterId           = "?$filter=indexof(id, '{1}') ne -1";
        public static string FilterType         = "?$filter=type eq {1}";
        public static string FilterTypeStr      = "?$filter=type eq '{1}'";
        public static string FilterSource       = "?$filter=source eq {1}";
        public static string FilterSourceStr    = "?$filter=source eq '{1}'";
        public static string FilterBoth         = "?$filter=type eq {1} and source eq {2}";
        public static string FilterBothStr      = "?$filter=type eq '{1}' and source eq '{2}'";

        //Filters Derivatives
        public static string FilterMarket       = "indexof(marketSegmentId, '{0}') ne -1";
        public static string FilterSymbol       = "indexof(underlyingSymbol, '{0}') ne -1";

        //Filters Funds
        public static string FilterManagment    = "managementSocietyId eq '{0}'";
        public static string FilterDepositary   = "despositarySocietyId eq '{0}'";
        public static string FilterCurrencyCode = "indexof(currencyCode, '{0}') ne -1";
        public static string FilterRent         = "rentTypeId eq '{0}'";
        public static string FilterManagmentStr = "indexof(managementSocietyName, '{0}') ne -1";
        public static string FilterDepositaryStr= "indexof(despositarySocietyName, '{0}') ne -1";        
        public static string FilterRentStr      = "indexof(rentTypeName, '{0}') ne -1";

        //Filters OData
        public static string FilterTypeSearch   = "indexof(type, '{0}') ne -1";
        public static string FilterCurrency     = "indexof(Currency, '{0}') ne -1";
        public static string FilterUnderSymbol  = "indexof(UnderlyingSymbol, '{0}') ne -1";        
        public static string FilterMarketId     = "indexof(MarketId, '{0}') ne -1";
        public static string FilterCountry      = "indexof(Country, '{0}') ne -1";

        #endregion

        #region Schemas
        public static string WorkingSchema  = API.v2 + "/api/Schemas/working-schema";               //Devuelve el schema de trabajo actual.
        public static string Schemas        = API.v2 + "/api/Schemas";                              //Devuelve la lista completa de esquemas.
        public static string SchemasId      = API.v2 + "/api/Schemas/";                             //Devuelve un esquema con un id específico.
        public static string PromoteSchema  = API.v2 + "/api/Schemas/is-promote-schema-running";    //Verifica si la tarea de promover un schema se está ejecutando
        #endregion

        #region Fields
        public static string Fields = API.v2 + "/api/schema/{0}/Fields";      //Devuelve la lista completa de fields.
        public static string Field  = API.v2 + "/api/schema/{0}/Fields/{1}";  //Devuelve un field con un id específico.
        #endregion

        #region Instruments
        public static string InstrumentsSuggestedFields             = API.v2 + "/api/schema/{0}/Instruments/suggested-fields";    //Obtiene una lista de campos sugeridos.
        public static string InstrumentsTodayUpdated                = API.v2 + "/api/schema/{0}/Instruments/today-updated";       //Retorna la lista de instrumentos actualizados en el día.                
        public static string InstrumentsTodayAdded                  = API.v2 + "/api/schema/{0}/Instruments/today-added";         //Retorna la lista de instrumentos dados de alta en el día.
        public static string InstrumentsTodayRemoved                = API.v2 + "/api/schema/{0}/Instruments/today-removed";       //Retorna la lista de instrumentos dados de baja en el día.
        public static string InstrumentsReport                      = API.v2 + "/api/schema/{0}/Instruments/report";              //Retorna un reporte resumido de instrumentos.
        public static string Instrument                             = API.v2 + "/api/schema/{0}/Instruments/{1}";                 //Retorna una instrumento por id.        
        public static string Instruments                            = API.v2 + "/api/schema/{0}/Instruments";                     //Retorna una lista de instrumentos.              
        #endregion

        #region ReferenceDatas
        public static string TodayUpdated           = API.v2 + "/api/schema/{0}/ReferenceDatas/today-updated";                //Retorna la lista de instrumentos actualizados en el día.
        public static string TodayAdded             = API.v2 + "/api/schema/{0}/ReferenceDatas/today-added";                  //Retorna la lista de instrumentos dados de alta en el día.
        public static string TodayRemoved           = API.v2 + "/api/schema/{0}/ReferenceDatas/today-removed";                //Retorna la lista de instrumentos dados de baja en el día.
        public static string ReferenceDatas         = API.v2 + "/api/schema/{0}/ReferenceDatas";                              //Retorna la lista de instrumentos.        
        public static string Specification          = API.v2 + "/api/schema/{0}/ReferenceDatas/specification";                //Retorna una especificación del estado actual.
        #endregion

        #region Reports
        public static string Reports    = API.v2 + "/api/schema/{0}/Reports";                 //Devuelve la lista completa de reportes.
        public static string Report     = API.v2 + "/api/schema/{0}/Reports/{1}";             //Devuelve un reporte con un id específico.
        #endregion

        #region Types
        public static string SourceFieldTypes       = API.v2 + "/api/Types/source-field-types";       //Devuelve los posibles tipos de datos de los orígenes.
        public static string PropertyControlTypes   = API.v2 + "/api/Types/property-control-types";   //Devuelve los tipos de control de las propiedades de los instrumentos.
        public static string StateControlTypes      = API.v2 + "/api/Types/state-control-types";      //Devuelve los tipos de control del estado de un instrumento.
        public static string InstrumentTypes        = API.v2 + "/api/Types/instrument-types";         //Devuelve los tipos de instrumentos.
        public static string PropertyOriginTypes    = API.v2 + "/api/Types/property-origin-types";    //Devuelve los tipos de origen para las propiedades de los instrumentos.
        public static string SourceTypes            = API.v2 + "/api/Types/source-types";             //Devuelve los tipos de origen.        
        #endregion

        #region Mappings
        public static string Mapping    = API.v2 + "/api/schema/{0}/Mappings/{1}";            //Devuelve un mapping para un id específico.
        public static string Mappings   = API.v2 + "/api/schema/{0}/Mappings";                //Devuelve una lista de mappings.
        #endregion

        #region SourceFields
        public static string SourceFields   = API.v2 + "/api/schema/{0}/SourceFields";        //Devuelve la lista completa de source fields.
        public static string SourceField    = API.v2 + "/api/schema/{0}/SourceFields/{1}";    //Devuelve un source field con un id específico.
        #endregion

        #region StatusReports
        public static string ProcessesStatus = API.v2 + "/api/StatusReports/processes-status";    //Devuelve el estado de los procesos.
        #endregion

        #region Derivatives
        public static string Derivatives        = API.v1 + "/api/Derivatives";   
        #endregion

        #region Funds
        public static string Fund           = API.v1 + "/api/Funds/{0}";    //Retorna un fondo por id        
        public static string Funds          = API.v1 + "/api/Funds";        //Retorna una lista de fondos
        #endregion       

        #region Securities
        public static string Securitie  = API.v1 + "/api/Securities/{0}";   //Retorna un titulo valor por id        
        public static string Securities = API.v1 + "/api/Securities";       //Retorna una lista de títulos valores
        #endregion 

        #region OData
        public static string OData = API.v2 + "/api/schema/{0}/refdata";    //Retorna una lista de instrumentos filtrados con OData
        #endregion

        #region ESCO
        public static string Depositary     = "?$filter=type eq 'MF' & $select=FundCustodianId,FundCustodianName & $count=true & apply=groupby((FundCustodianId))";     //Retorna la lista de Sociedades Depositarias
        public static string Managment      = "?$filter=type eq 'MF' & $select=FundManagerId,FundManagerName & $count=true & apply=groupby((FundManagerId))";           //Retorna la lista de Sociedades Administradoras 
        public static string RentType       = "?$filter=type eq 'MF' & $select=RentTypeId,RentTypeName & $count=true & apply=groupby((RentTypeId))";                    //Retorna la lista de Tipos de Renta    
        public static string Region         = "?$filter=type eq 'MF' & $select=RegionId,RegionName & $count=true & apply=groupby((RegionId))";                          //Retorna la lista de Regiones 
        public static string Currency       = "?$select=Currency & $count=true & apply=groupby((Currency))";                                                            //Retorna la lista de Monedas    
        public static string Country        = "?$select=Country & $count=true & apply=groupby((Country))";                                                              //Retorna la lista de Países    
        public static string Issuer         = "?$select=Issuer & $count=true & apply=groupby((Issuer))";                                                                //Retorna la lista de Issuer    
        public static string Horizon        = "?$filter=type eq 'MF' & $select=HorizonId,HorizonName & $count=true & apply=groupby((HorizonId))";                       //Retorna la lista de Horizon 
        public static string FundType       = "?$filter=type eq 'MF' & $select=FundTypeId,FundTypeName & $count=true & apply=groupby((FundTypeId))";                    //Retorna la lista de Tipos de Fondos 
        public static string Benchmark      = "?$filter=type eq 'MF' & $select=FundBenchmarkId,FundBenchmarkName & $count=true & apply=groupby((FundBenchmarkId))";     //Retorna la lista de Benchmarks 
        public static string RDTypes        = "?$select=type & $count=true & apply=groupby((type))";                                                                    //Retorna la lista de Tipos de Reference Data 
        public static string RDSymbols      = "?$select=UnderlyingSymbol & $count=true & apply=groupby((UnderlyingSymbol))";                                            //Retorna la lista de Símbolos (UnderlyingSymbol) de Instrumentos financieros
        public static string Markets        = "?$select=MarketId & $count=true & apply=groupby((MarketId))";                                                            //Retorna la lista de Mercados para los Instrumentos financieros
        #endregion
    }
}
