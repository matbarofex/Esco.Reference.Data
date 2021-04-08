using System;

namespace ESCO.Reference.Data.Services
{
    class API
    {
        public static string ver
        {  
            get
            {
                return "prd-ro";
            }
        }        

        public static string v1
        {
            get { return "/v1"; }
        }

        public static string v2
        {
            get { return "/v2"; }
        }

        //Format Url with Filters
        public static string getUrl(string cfg, string type, string source, string schema)
        {
            string url = String.Format(cfg, schema);
            if ((type != null) && (source != null))
            {
                url = String.Format(cfg + Config.FilterBoth, schema, type, source);
            }
            else
            {
                url = (type != null) ? String.Format(cfg + Config.FilterType, schema, type) : url;
                url = (source != null) ? String.Format(cfg + Config.FilterSource, schema, source) : url;
            }
            return url;
        }

        public static string getUrlDerivatives(string market, string symbol)
        {
            string url = Config.Derivatives;
            string empty = String.Empty;
            string and = " and ";
            string filter = "?$filter=";

            string urlStr = (market != null) ? filter + String.Format(Config.FilterMarket, market) : empty;

            string urlSymbol = String.Format(Config.FilterSymbol, symbol);
            string urlSymbolFilter = (symbol != null) ? filter + urlSymbol : empty;
            string urlSymbolAnd = (symbol != null) ? and + urlSymbol : empty;

            urlStr = urlStr + ((urlStr == empty) ? urlSymbolFilter : urlSymbolAnd);

            return url + urlStr;
        }

        public static string getUrlFunds(string management, string despositary, string currency, string rent)
        {
            string url = Config.Funds;            
            string empty = String.Empty;
            string and = " and ";
            string filter = "?$filter=";

            string urlManagment = getUrlStr(Config.FilterManagment, Config.FilterManagmentStr, management);
            string urlStr = (management != null) ? filter + urlManagment : empty;

            string urlDespositary = getUrlStr(Config.FilterDepositary, Config.FilterDepositaryStr, despositary);
            string urlDespositaryFilter = (despositary != null) ? filter  + urlDespositary : empty;
            string urlDespositaryAnd = (despositary != null) ? and + urlDespositary : empty;

            string urlCurrency = String.Format(Config.FilterCurrencyCode, currency);
            string urlCurrencyFilter = (currency != null) ? filter + urlCurrency : empty;
            string urlCurrencyAnd = (currency != null) ? and + urlCurrency : empty;

            string urlRent = getUrlStr(Config.FilterRent, Config.FilterRentStr, rent);
            string urlRentFilter = (rent != null) ? filter + urlRent : empty;
            string urlRentAnd = (rent != null) ? and + urlRent : empty;

            urlStr = urlStr + ((urlStr == empty) ? urlDespositaryFilter : urlDespositaryAnd);
            urlStr = urlStr + ((urlStr == empty) ? urlCurrencyFilter : urlCurrencyAnd);
            urlStr = urlStr + ((urlStr == empty) ? urlRentFilter : urlRentAnd);
           
            return url + urlStr;
        }

        private static string getUrlStr(string cfg, string cfgstr, string value)
        {
            try
            {
                Int32.Parse(value);
                value = String.Format(cfg, value);
            }
            catch
            {
                value = String.Format(cfgstr, value);
            }
            return value;
        }

        public static string getUrlOData(
            string type,
            string currency,
            string symbol,
            string market,
            string country,
            string schema)
        {
            string url = String.Format(Config.OData, schema);
            string empty = String.Empty;
            string and = " and ";
            string filter = "?$filter=";

            string urlStr = (type != null) ? filter + String.Format(Config.FilterTypeSearch, type) : empty;

            string urlCurrency = String.Format(Config.FilterCurrency, currency);
            string urlCurrencyFilter = (currency != null) ? filter + urlCurrency : empty;
            string urlCurrencyAnd = (currency != null) ? and + urlCurrency : empty;

            string urlSymbol = String.Format(Config.FilterUnderSymbol, symbol);
            string urlSymbolFilter = (symbol != null) ? filter + urlSymbol : empty;
            string urlSymbolAnd = (symbol != null) ? and + urlSymbol : empty;

            string urlMarket = String.Format(Config.FilterMarketId, market);
            string urlMarketFilter = (market != null) ? filter + urlMarket : empty;
            string urlMarketAnd = (market != null) ? and + urlMarket : empty;

            string urlCountry = String.Format(Config.FilterCountry, country);
            string urlCountryFilter = (country != null) ? filter + urlCountry : empty;
            string urlCountryAnd = (country != null) ? and + urlCountry : empty;
            
            urlStr = urlStr + ((urlStr == empty) ? urlCurrencyFilter : urlCurrencyAnd);
            urlStr = urlStr + ((urlStr == empty) ? urlSymbolFilter : urlSymbolAnd);
            urlStr = urlStr + ((urlStr == empty) ? urlMarketFilter : urlMarketAnd);
            urlStr = urlStr + ((urlStr == empty) ? urlCountryFilter : urlCountryAnd);

            return url + urlStr;
        }
    }
}
