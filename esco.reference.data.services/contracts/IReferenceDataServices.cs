using ESCO.Reference.Data.Model;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ESCO.Reference.Data.Services.Contracts
{
    /// <summary>
    /// Interface Reference Datas Services
    /// </summary>  
    public interface IReferenceDataServices
    {
        void ChangeSuscriptionKey(string key);              //Cambiar la Suscription Key del usuario
        void PaginatedMode(bool paginated = true);          //Habilitación del paginado de registros (por defecto es false: trae todos los registros sin paginar)

        #region Prices
        Task<List<Price>> GetPrices(string type = null);         //Retorna la lista de campos de precios actualizados de los instrumentos financieros.
        #endregion

        #region ReferenceData
        Task<ReferenceDatas> GetReferenceData(DateTime? date, string type = null, string schema = null);        //Retorna la lista de instrumentos.
        Task<string> GetReferenceDataAsString(DateTime? date = null, string type = null, string schema = null); //Retorna la lista de instrumentos como una cadena.
        #endregion

        #region ESCO
        Task<Custodians> GetCustodians(string schema = null);                      // Retorna la lista de Sociedades Depositarias  
        Task<Managments> GetManagements(string schema = null);                     // Retorna la lista de Sociedades Administradoras
        Task<Rents> GetRentTypes(string schema = null);                            // Retorna la lista de Tipos de Rentas      
        Task<Regions> GetRegions(string schema = null);                            // Retorna la lista de Regiones      
        Task<Currencys> GetCurrencys(string schema = null);                        // Retorna la lista de Monedas      
        Task<Countrys> GetCountrys(string schema = null);                          // Retorna la lista de Países      
        Task<Issuers> GetIssuers(string schema = null);                            // Retorna la lista de Issuers  
        Task<Horizons> GetHorizons(string schema = null);                          // Retorna la lista de Horizon 
        Task<FundTypes> GetFundTypes(string schema = null);                        // Retorna la lista de Tipos de Fondos
        Task<Benchmarks> GetBenchmarks(string schema = null);                      // Retorna la lista de Benchmarks
        ReferenceDataTypes GetReferenceDataTypes(string schema = null);            // Retorna la lista de tipos de instrumentos financieros
        Task<Markets> GetMarkets(string schema = null);                            // Retorna la lista de Mercados para los instrumentos financieros
        #endregion

        #region Reportes
        Task<Reports> GetFieldsReports(string schema = null);      //Devuelve la lista de campos para reportes.
        Task<Reports> GetFields(string schema = null);             //Devuelve la lista completa de campos.
        #endregion
    }
}
