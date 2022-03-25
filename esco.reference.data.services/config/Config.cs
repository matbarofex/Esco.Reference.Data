using System;
namespace ESCO.Reference.Data.Config
{
    class Config
    {
        public static class Http
        {
            public const string url = "https://apids.primary.com.ar/";
            public const string cache = "no-cache";
            public const string json = "application/json";

            public const string v1 = "prd-ro/v1";
            public const string v2 = "prd-ro/v2";
            public const string v3 = "prd-ro/v3";
        }

        public static class Schema
        {
            public const string v2 = "2";
            public const string v3 = "schema-000";
            public const string v4 = "schema-001";
        }

        public class Header
        {
            public const string key = "Ocp-Apim-Subscription-Key";
            public const string cache = "Cache-Control";
            public const string xversion = "X-Version";
        }

        public class Url
        {
            #region Filters
            //Generals Filters
            public const string FilterId = "?$filter=indexof(name, '{1}') ne -1";
            public const string FilterIdStr = " and indexof(name, '{2}') ne -1";
            public const string FilterTypeStr = " and type eq '{2}'";
            public const string FilterPages = "&$top=500&$skip={0}";

            //Filters ReferenceData
            public const string FilterAll = "?$filter=type ne null";
            public const string FilterUpdated = "?$filter=updated ge {1} and active eq true";
            public const string FilterAdded = "?$filter=date eq '{1}'";
            public const string FilterRemoved = "?$filter=updated ge {1} and active eq false";

            //Filters OData
            public const string FilterType = "indexof(type, '{0}') ne -1";
            public const string FilterName = "indexof(name, '{0}') ne -1";
            public const string FilterCurrency = "indexof(currency, '{0}') ne -1";
            public const string FilterMarketId = "indexof(marketId, '{0}') ne -1";
            public const string FilterCountry = "indexof(country, '{0}') ne -1";

            public const string FilterOpts = "?$filter=type eq 'OPT' or type eq 'OOF'";
            public const string FilterADRS = "?$filter=text eq 'A.D.R.S (ACCIONES)'&orderby name";
            public const string FilterPrivadas = "?$filter=text eq 'ACCIONES PRIVADAS'&orderby name";
            public const string FilterPymes = "?$filter=text eq 'ACCIONES PYMES'&orderby name";

            #endregion

            #region Schemas
            public const string Mapping = Http.v3 + "/api/Schemas/{0}/field-mapping";                      //Devuelve el mapping que tiene un schema.  
            #endregion

            #region ReferenceDatas
            public const string Specification = Http.v3 + "/api/Schemas/{0}/Data/specification";           //Retorna una especificación del estado actual.      
            public const string ReferenceData = Http.v3 + "/api/Schemas/{0}/Data/by-odata";                //Retorna la lista de instrumentos.        
            #endregion

            #region Reports
            public const string FieldsReports = Http.v3 + "/api/Schemas/{0}/Actions/fields-for-reports";   //Obtiene la lista completa de campos para los reportes
            public const string Fields = Http.v3 + "/api/Schemas/{0}/Actions/fields";                      //Obtiene la lista completa de campos.
            #endregion

            #region OData
            public const string Consolidated = Http.v3 + "/api/Schemas/{0}/Data/consolidated-by-odata";    //Retorna una lista de instrumentos consolidados filtrados con OData
            public const string ODataCSV = Http.v3 + "/api/Schemas/{0}/Data/csv/by-odata";                 //Obtener instrumentos filtrados en un csv.
            public const string ODataReports = Http.v3 + "/api/Schemas/{0}/Data/by-odata-for-reports";     //Obtener instrumentos filtrados por una query OData sin validar el estado del schema.
            #endregion

            #region ESCO
            public const string Custodian = "?$filter=type eq 'MF'&$select=fundCustodianId,fundCustodianName&apply=groupby((fundCustodianId))";        //Retorna la lista de Sociedades Depositarias
            public const string Managment = "?$filter=type eq 'MF' & $select=fundManagerId,fundManagerName&apply=groupby((fundManagerId))";            //Retorna la lista de Sociedades Administradoras 
            public const string RentType = "?$filter=type eq 'MF' & $select=rentTypeId,rentTypeName&apply=groupby((rentTypeId))";                      //Retorna la lista de Tipos de Renta    
            public const string Region = "?$filter=type eq 'MF' & $select=regionId,regionName&apply=groupby((regionId))";                              //Retorna la lista de Regiones 
            public const string Currency = "?$select=currency&apply=groupby((currency))";                                                              //Retorna la lista de Monedas    
            public const string Country = "?$select=country&apply=groupby((country))";                                                                 //Retorna la lista de Países    
            public const string Issuer = "?$select=issuer&apply=groupby((issuer))";                                                                    //Retorna la lista de Issuer    
            public const string Horizon = "?$filter=type eq 'MF' & $select=horizonId,horizonName&apply=groupby((horizonId))";                          //Retorna la lista de Horizon 
            public const string FundType = "?$filter=type eq 'MF' & $select=fundTypeId,fundTypeName&apply=groupby((fundTypeId))";                      //Retorna la lista de Tipos de Fondos 
            public const string Benchmark = "?$filter=type eq 'MF' & $select=fundBenchmarkId,fundBenchmarkName&apply=groupby((fundBenchmarkId))";      //Retorna la lista de Benchmarks                                             //Retorna la lista de Símbolos (UnderlyingSymbol) de Instrumentos financieros
            public const string Markets = "?$select=marketId&apply=groupby((marketId))";                                                               //Retorna la lista de Mercados para los Instrumentos financieros
            #endregion
        }

        #region Functions
        //Set Url
        public static string SetUrl(
            string url,
            string value1,
            string value2 = null,
            string value3 = null,
            string value4 = null,
            string value5 = null,
            string value6 = null)
        {
            return string.Format(url, value1, value2, value3, value4, value5, value6);
        }

        //Format Url
        public static string GetUrl(string cfg, string typeorid, string schema, bool search = false)
        {
            schema ??= Schema.v3;
            string format = (cfg == Url.FilterAdded) ? "d/MM/yyyy" : "yyyy-MM-d";
            cfg = (cfg == null) ? Url.ReferenceData + Url.FilterAll : Url.ReferenceData + cfg;
            string date = DateTime.Now.ToString(format);
            return (search) ?
                    SetUrl(cfg + Url.FilterIdStr, schema, date, typeorid) :
                    ((typeorid != null) ?
                        SetUrl(cfg + Url.FilterTypeStr, schema, date, typeorid) :
                        SetUrl(cfg, schema, date));
        }

        //Format OData Url
        public static string GetUrlOData(
            string urlodata,
            string type,
            string name,
            string currency,
            string market,
            string country,
            string schema)
        {
            string url = SetUrl(urlodata, schema);
            string empty = string.Empty;
            string and = " and ";
            string filter = "?$filter=";

            string urlStr = (type != null) ? filter + SetUrl(Url.FilterType, type) : empty;

            string urlName = SetUrl(Url.FilterName, name);
            string urlNameFilter = (name != null) ? filter + urlName : empty;
            string urlNameAnd = (name != null) ? and + urlName : empty;

            string urlCurrency = SetUrl(Url.FilterCurrency, currency);
            string urlCurrencyFilter = (currency != null) ? filter + urlCurrency : empty;
            string urlCurrencyAnd = (currency != null) ? and + urlCurrency : empty;

            string urlMarket = SetUrl(Url.FilterMarketId, market);
            string urlMarketFilter = (market != null) ? filter + urlMarket : empty;
            string urlMarketAnd = (market != null) ? and + urlMarket : empty;

            string urlCountry = SetUrl(Url.FilterCountry, country);
            string urlCountryFilter = (country != null) ? filter + urlCountry : empty;
            string urlCountryAnd = (country != null) ? and + urlCountry : empty;

            urlStr += ((urlStr == empty) ? urlNameFilter : urlNameAnd);
            urlStr += ((urlStr == empty) ? urlCurrencyFilter : urlCurrencyAnd);
            urlStr += ((urlStr == empty) ? urlMarketFilter : urlMarketAnd);
            urlStr += ((urlStr == empty) ? urlCountryFilter : urlCountryAnd);

            return url + urlStr;
        }
        #endregion

    }
}
