namespace ESCO.Reference.Data.App
{    
    class Config
    {   

        public static class Http
        {
            public const string url = "https://apids.primary.com.ar/";
            public const string v1 = url + "prd-ro/v1";
            public const string v2 = url + "prd-ro/v2";
            public const string v3 = url + "prd-ro/v3";
        }

        //Filters
        #region Filters
        //Generals Filters
        public const string FilterId = "?$filter=indexof(name, '{name}') ne -1";
        public const string FilterIdStr = " and indexof(name, '{name}') ne -1";
        public const string FilterTypeStr = " and type eq '{type}'";
        public const string FilterReports = "?$filter=type eq '{0}'";

        //Filters ReferenceData
        public const string FilterAll = "?$filter=type ne null";
        public const string FilterUpdated = "?$filter=updated ge {updated} and active eq true";
        public const string FilterAdded = "?$filter=date eq '{date}'";
        public const string FilterRemoved = "?$filter=updated ge {updated} and active eq false";

        //Filters OData
        public const string FilterType = "?$filter=indexof(type, 'type') ne -1";
        public const string FilterName = " and indexof(name, 'name') ne -1";
        public const string FilterCurrency = " and indexof(currency, 'currency') ne -1";
        public const string FilterMarketId = " and indexof(marketId, 'market') ne -1";
        public const string FilterCountry = " and indexof(country, 'country') ne -1";
        public const string FilterSearch = FilterType + FilterName + FilterCurrency + FilterMarketId + FilterCountry;

        public const string FilterOpts = "?$filter=type eq 'OPT' or type eq 'OOF'";
        public const string FilterADRS = "?$filter=text eq 'A.D.R.S (ACCIONES)'&orderby name";
        public const string FilterPrivadas = "?$filter=text eq 'ACCIONES PRIVADAS'&orderby name";
        public const string FilterPymes = "?$filter=text eq 'ACCIONES PYMES'&orderby name";

        #endregion

        #region Schemas
        public const string Mapping = Http.v3 + "/api/Schemas/schema-00x/field-mapping";                      //Devuelve el mapping que tiene un schema.  
        #endregion

        #region ReferenceDatas
        public const string Specification = Http.v3 + "/api/Schemas/schema-00x/Data/specification";           //Retorna una especificación del estado actual.      
        public const string ReferenceData = Http.v3 + "/api/Schemas/schema-00x/Data/by-odata";                //Retorna la lista de instrumentos.        
        #endregion

        #region Reports
        public const string FieldsReports = Http.v3 + "/api/Schemas/schema-00x/Actions/fields-for-reports";   //Obtiene la lista completa de campos para los reportes
        public const string Fields = Http.v3 + "/api/Schemas/schema-00x/Actions/fields";                      //Obtiene la lista completa de campos.
        #endregion

        #region OData
        public const string Consolidated = Http.v3 + "/api/Schemas/schema-00x/Data/consolidated-by-odata";    //Retorna una lista de instrumentos consolidados filtrados con OData
        public const string ODataCSV = Http.v3 + "/api/Schemas/schema-00x/Data/csv/by-odata";                 //Obtener instrumentos filtrados en un csv.
        public const string ODataReports = Http.v3 + "/api/Schemas/schema-00x/Data/by-odata-for-reports";     //Obtener instrumentos filtrados por una query OData sin validar el estado del schema.
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

        #region TypesRD
        //Set Url
        public static string SetUrl(string value)
        {
            return string.Format(FilterReports, value);
        }
        public static class Types
        {
            public const string Fondos = "MF";         //MF es estándar fix y se refiere a FONDOS COMUNES DE INVERSIÓN
            public const string Cedears = "CD";        //CD es estándar fix y se refiere a CEDEARS
            public const string Acciones = "CS";       //Líderes CS //CS es estándar fix y se refiere a ACCIONES
                                                       //Acciones Pymes CS CS es estándar fix y se refiere a ACCIONES PYMES
                                                       //A.D.R.S(Acciones) //CS Incluido en Type CS
            public const string Obligaciones = "CORP"; //CORP es estándar fix y se refiere a Obligaciones Negociables
            public const string Titulos = "GO";        //Títulos Públicos > Bonos GO GO es estándar fix y se refiere a BONOS EXTERNOS, TITULOS PUBLICOS, BONOS CONSOLIDADOS
                                                       //Títulos Públicos > Letras GO //Incluido en Type GO y se refiere a LETRAS, LETRAS TESORO NACIONAL
                                                       //Especies de Fideicomisos GO // Incluido en Type GO y se refiere a TITULOS DE DEUDA, CERTIF. PARTICIP.
            public const string Futuros = "FUT";       //FUT es estándar fix y se refiere a FUTUROS
            public const string Opciones = "OPT";      //OPT y OOF son estándar fix y se refiere a OPCIONES
            public const string OpcionesF = "OOF";     //OPT y OOF son estándar fix y se refiere a OPCIONES
            public const string Pases = "BUYSELL";     //BUYSELL es estándar fix y se refiere a PASES
            public const string Cauciones = "REPO";    //REPO es estándar fix y se refiere a CAUCIONES
            public const string STN = "STN";           //Acciones Privadas
            public const string T = "T";               //Plazo por Lotes
            public const string TERM = "TERM";         //Préstamos de Valores
            public const string Indices = "XLINKD";    //Índices XLINKD
        }

        public static class TypesDesc
        {
            public const string Fondos = "MF es estándar fix y se refiere a FONDOS COMUNES DE INVERSIÓN";
            public const string Cedears = "CD es estándar fix y se refiere a CEDEARS";
            public const string Acciones = "Líderes CS //CS es estándar fix y se refiere a ACCIONES / Acciones Pymes CS CS es estándar fix y se refiere a ACCIONES PYMES / A.D.R.S(Acciones) //CS Incluido en Type CS";
            public const string Obligaciones = "CORP es estándar fix y se refiere a Obligaciones Negociables";
            public const string Titulos = "Títulos Públicos > Bonos GO GO es estándar fix y se refiere a BONOS EXTERNOS, TITULOS PUBLICOS, BONOS CONSOLIDADOS / Títulos Públicos > Letras GO / Incluido en Type GO y se refiere a LETRAS, LETRAS TESORO NACIONAL / Especies de Fideicomisos GO / Incluido en Type GO y se refiere a TITULOS DE DEUDA, CERTIF. PARTICIP.";
            public const string Futuros = "FUT es estándar fix y se refiere a FUTUROS";
            public const string Opciones = "OPT y OOF son estándar fix y se refiere a OPCIONES";
            public const string OpcionesF = "OPT y OOF son estándar fix y se refiere a OPCIONES";
            public const string Pases = "BUYSELL es estándar fix y se refiere a PASES";
            public const string Cauciones = "REPO es estándar fix y se refiere a CAUCIONES";
            public const string STN = "Acciones Privadas";
            public const string T = "Plazo por Lotes";
            public const string TERM = "Préstamos de Valores";
            public const string Indices = "Índices XLINKD";
        }
        #endregion
    }
}
