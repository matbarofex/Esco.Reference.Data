namespace ESCO.Reference.Data.App
{    
    class Config
    {   
        public static string url = "https://apids.primary.com.ar/";
        public static string cache = "no-cache";

        private static string APIv1 = url + "prd-ro/v1";
        private static string APIv2 = url + "prd-ro/v2";

        public class Header
        {
            public static string key = "Ocp-Apim-Subscription-Key";
            public static string cache = "Cache-Control";
            public static string xversion = "X-Version";
        }        

        //Filters
        public static string FilterType         = "?$filter=type eq {1}";
        public static string FilterTypeStr      = "?$filter=type eq '{1}'";
        public static string FilterSource       = "?$filter=source eq {1}";
        public static string FilterSourceStr    = "?$filter=source eq '{1}'";
        public static string FilterBoth         = "?$filter=type eq {1} and source eq {2}";
        public static string FilterId           = "?$filter=indexof(id, '{1}') ne -1";

        public static string Depositary         = "?$filter=type eq 'MF' & $select=FundCustodianId,FundCustodianName & $count=true & apply=groupby((FundCustodianId))";         //Retorna la lista de Sociedades Depositarias
        public static string Managment          = "?$filter=type eq 'MF' & $select=FundManagerId,FundManagerName & $count=true & apply=groupby((FundManagerId))";               //Retorna la lista de Sociedades Administradoras 
        public static string RentType           = "?$filter=type eq 'MF' & $select=RentTypeId,RentTypeName & $count=true & apply=groupby((RentTypeId))";                        //Retorna la lista de Tipos de Renta    
        public static string Region             = "?$filter=type eq 'MF' & $select=RegionId,RegionName & $count=true & apply=groupby((RegionId))";                              //Retorna la lista de Regiones 
        public static string Currency           = "?$select=Currency & $count=true & apply=groupby((Currency))";                                                                //Retorna la lista de Monedas    
        public static string Country            = "?$select=Country & $count=true & apply=groupby((Country))";                                                                  //Retorna la lista de Países    
        public static string Issuer             = "?$select=Issuer & $count=true & apply=groupby((Issuer))";                                                                    //Retorna la lista de Issuer    
        public static string Horizon            = "?$filter=type eq 'MF' & $select=HorizonId,HorizonName & $count=true & apply=groupby((HorizonId))";                           //Retorna la lista de Horizon 
        public static string FundType           = "?$filter=type eq 'MF' & $select=FundTypeId,FundTypeName & $count=true & apply=groupby((FundTypeId))";                        //Retorna la lista de Tipos de Fondos 
        public static string Benchmark          = "?$filter=type eq 'MF' & $select=FundBenchmarkId,FundBenchmarkName & $count=true & apply=groupby((FundBenchmarkId))";         //Retorna la lista de Benchmarks 
        public static string RDTypes            = "?$select=type & $count=true & apply=groupby((type))";                                                                    //Retorna la lista de Benchmarks 
        public static string Markets            = "?$select=MarketId & $count=true & apply=groupby((MarketId))";                                     //Retorna la lista de Benchmarks 


        #region Schemas
        public static string WorkingSchema  = APIv2 + "/api/Schemas/working-schema";               //Devuelve el schema de trabajo actual.
        public static string Schemas        = APIv2 + "/api/Schemas";                              //Devuelve la lista completa de esquemas.
        public static string SchemasId      = APIv2 + "/api/Schemas/";                             //Devuelve un esquema con un id específico.
        public static string PromoteSchema  = APIv2 + "/api/Schemas/is-promote-schema-running";    //Verifica si la tarea de promover un schema se está ejecutando
        #endregion

        #region Fields
        public static string Fields = APIv2 + "/api/schema/{0}/Fields";      //Devuelve la lista completa de fields.
        public static string Field  = APIv2 + "/api/schema/{0}/Fields/{1}";  //Devuelve un field con un id específico.
        #endregion

        #region Instruments
        public static string InstrumentsSuggestedFields             = APIv2 + "/api/schema/{0}/Instruments/suggested-fields";    //Obtiene una lista de campos sugeridos.
        public static string InstrumentsTodayUpdated                = APIv2 + "/api/schema/{0}/Instruments/today-updated";       //Retorna la lista de instrumentos actualizados en el día.                
        public static string InstrumentsTodayAdded                  = APIv2 + "/api/schema/{0}/Instruments/today-added";         //Retorna la lista de instrumentos dados de alta en el día.
        public static string InstrumentsTodayRemoved                = APIv2 + "/api/schema/{0}/Instruments/today-removed";       //Retorna la lista de instrumentos dados de baja en el día.
        public static string InstrumentsReport                      = APIv2 + "/api/schema/{0}/Instruments/report";              //Retorna un reporte resumido de instrumentos.
        public static string Instrument                             = APIv2 + "/api/schema/{0}/Instruments/{1}";                 //Retorna una instrumento por id.        
        public static string Instruments                            = APIv2 + "/api/schema/{0}/Instruments";                     //Retorna una lista de instrumentos.              
        #endregion

        #region ReferenceDatas
        public static string TodayUpdated           = APIv2 + "/api/schema/{0}/ReferenceDatas/today-updated";                //Retorna la lista de instrumentos actualizados en el día.
        public static string TodayAdded             = APIv2 + "/api/schema/{0}/ReferenceDatas/today-added";                  //Retorna la lista de instrumentos dados de alta en el día.
        public static string TodayRemoved           = APIv2 + "/api/schema/{0}/ReferenceDatas/today-removed";                //Retorna la lista de instrumentos dados de baja en el día.
        public static string ReferenceDatas         = APIv2 + "/api/schema/{0}/ReferenceDatas";                              //Retorna la lista de instrumentos.        
        public static string Specification          = APIv2 + "/api/schema/{0}/ReferenceDatas/specification";                //Retorna una especificación del estado actual.
        #endregion

        #region Reports
        public static string Reports    = APIv2 + "/api/schema/{0}/Reports";                 //Devuelve la lista completa de reportes.
        public static string Report     = APIv2 + "/api/schema/{0}/Reports/{1}";             //Devuelve un reporte con un id específico.
        #endregion

        #region Types
        public static string SourceFieldTypes       = APIv2 + "/api/Types/source-field-types";       //Devuelve los posibles tipos de datos de los orígenes.
        public static string PropertyControlTypes   = APIv2 + "/api/Types/property-control-types";   //Devuelve los tipos de control de las propiedades de los instrumentos.
        public static string StateControlTypes      = APIv2 + "/api/Types/state-control-types";      //Devuelve los tipos de control del estado de un instrumento.
        public static string InstrumentTypes        = APIv2 + "/api/Types/instrument-types";         //Devuelve los tipos de instrumentos.
        public static string PropertyOriginTypes    = APIv2 + "/api/Types/property-origin-types";    //Devuelve los tipos de origen para las propiedades de los instrumentos.
        public static string SourceTypes            = APIv2 + "/api/Types/source-types";             //Devuelve los tipos de origen.        
        #endregion

        #region Mappings
        public static string Mapping    = APIv2 + "/api/schema/{0}/Mappings/{1}";            //Devuelve un mapping para un id específico.
        public static string Mappings   = APIv2 + "/api/schema/{0}/Mappings";                //Devuelve una lista de mappings.
        #endregion

        #region SourceFields
        public static string SourceFields   = APIv2 + "/api/schema/{0}/SourceFields";        //Devuelve la lista completa de source fields.
        public static string SourceField    = APIv2 + "/api/schema/{0}/SourceFields/{1}";    //Devuelve un source field con un id específico.
        #endregion

        #region StatusReports
        public static string ProcessesStatus = APIv2 + "/api/StatusReports/processes-status";    //Devuelve el estado de los procesos.
        #endregion

        #region Derivatives
        public static string Derivatives = APIv1 + "/api/Derivatives";     //Retorna una lista de derivados
        #endregion

        #region Funds
        public static string Fund           = APIv1 + "/api/Funds/{0}";    //Retorna un fondo por id        
        public static string Funds          = APIv1 + "/api/Funds";        //Retorna una lista de fondos
        #endregion       

        #region Securities
        public static string Securitie  = APIv1 + "/api/Securities/{0}";   //Retorna un titulo valor por id        
        public static string Securities = APIv1 + "/api/Securities";       //Retorna una lista de títulos valores
        #endregion 

        #region OData
        public static string OData = APIv2 + "/api/schema/2/refdata";      //Retorna una lista de instrumentos filtrados con OData
        #endregion 
    }
}
