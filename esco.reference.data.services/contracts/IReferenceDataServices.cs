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

        #endregion

        #region OData
        Task<ODataObject> getODataReferenceData(string query, string schema);  // Retorna la lista de instrumentos filtrados con OData.
        Task<ODataObject> getODataReferenceDataById(string id, string schema); // Retorna la lista de instrumentos filtrados por Id.
        Task<ODataObject> searchODataReferenceData(string type, string currency, string symbol, string market, string country, string schema); // Retorna la lista de instrumentos filtrados por campos específicos (puede incluirse cadenas de búsqueda parcial).
        #endregion

        #region ReferenceDatas
        Task<ReferenceDatas> getReferenceDataTodayUpdated(string type, string schema);  //Retorna la lista de instruments actualizados en el día.      
        Task<ReferenceDatas> searchReferenceDataTodayUpdated(string id, string schema); //Retorna la lista de instrumentos actualizados en el día que contengan una cadena de búsqueda como parte del id.
        Task<ReferenceDatas> getReferenceDataTodayAdded(string type, string schema);    //Retorna la lista de instrumentos dados de alta en el día.
        Task<ReferenceDatas> searchReferenceDataTodayAdded(string id, string schema);   //Retorna la lista de instrumentos dados de alta en el día que contengan una cadena de búsqueda como parte del id.
        Task<ReferenceDatas> getReferenceDataTodayRemoved(string type, string schema);  //Retorna la lista de instrumentos dados de baja en el día.
        Task<ReferenceDatas> searchReferenceDataTodayRemoved(string id, string schema); //Retorna la lista de instrumentos dados de baja en el día que contengan una cadena de búsqueda como parte del id.
        Task<ReferenceDatas> getReferenceData(string type, string schema);             //Retorna la lista de instrumentos.
        Task<Specification> getReferenceDataSpecification(string schema);               //Retorna una especificación del estado actual.
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
        //Task<ReferenceDataTypes> getReferenceDataTypes(string schema);      // Retorna la lista de tipos de instrumentos financieros
        Task<ReferenceDataSymbols> getReferenceDataSymbols(string schema);  // Retorna la lista de Símbolos (UnderlyingSymbol) de Instrumentos financieros
        Task<Markets> getMarkets(string schema);                            // Retorna la lista de Mercados para los instrumentos financieros
        #endregion

        #region Reportes

        Task<Reports> getReports(string schema);                   //Devuelve la lista completa de reportes.
        Task<Report> getReport(string id, string schema);          //Devuelve un reporte con un id específico.
        Task<string> getInstrumentsReport(string schema);          //Retorna un reporte resumido de instrumentos.

        #endregion
    }
}
