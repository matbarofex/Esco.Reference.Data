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

        #region Schemas
        Task<Mappings> GetMapping(string schema = null);    //Devuelve el mapping que tiene un schema.
        #endregion

        #region OData
        Task<ReferenceDatas> GetReferenceDataByOData(string query = null, string schema = null);                  // Retorna la lista de instrumentos filtrados con OData.        
        Task<string> GetByODataAsString(string query, string schema);                                             // Retorna la lista de instrumentos financieros como string, filtrados con Query en formato OData.
        Task<ReferenceDatas> GetConsolidatedByOData(string query = null, string schema = null);                   // Retorna la lista de instrumentos financieros consolidados filtrados con Query en formato OData.
        Task<string> GetConsolidatedAsString(string query = null, string schema = null);                          // Retorna la lista de instrumentos financieros consolidados como string filtrados con Query en formato OData.
        Task<Stream> GetCSVByOData(string query = null, string schema = null);                                    // Retorna la lista de instrumentos financieros filtrados en un CSV con Query en formato OData.
        Task<bool> SaveCSVByOData(string filePath, string fileName, string query = null, string schema = null);   // Retorna la lista de instrumentos financieros en un CSV (compactado en archivo ZIP) filtrados con Query en formato OData.
        #endregion

        #region Prices
        Task<string> GetPriceAsString(string name);              //Retorna el precio actualizado de un  instrumentos financiero como una cadena.
        Task<Price> GetPrice(string name);                       //Retorna el precio actualizado de un  instrumentos financiero.

        Task<string> GetPricesAsString(string type = null);      //Retorna la lista de campos de precios actualizados de los instrumentos financieros como una cadena.
        Task<List<Price>> GetPrices(string type = null);         //Retorna la lista de campos de precios actualizados de los instrumentos financieros.
        #endregion

        #region ReferenceData
        Task<string> GetUpdatedAsString(string type = null, string schema = null);                              //Retorna la lista de instruments actualizados en el día como cadena string.      
        Task<ReferenceDatas> GetReferenceData(DateTime? date, string type = null, string schema = null);        //Retorna la lista de instrumentos.
        Task<string> GetReferenceDataAsString(DateTime? date = null, string type = null, string schema = null); //Retorna la lista de instrumentos como una cadena.
        Task<ReferenceDatas> SearchReferenceData(string type = null, string name = null, string currency = null, string market = null, string country = null, string schema = null); //Retorna la lista de instrumentos financieros filtrados por campos específicos.
        Task<ReferenceDatas> SearchReferenceDataById(string id = null, string schema = null);                   //Retorna la lista de instrumentos financieros filtrados por el identificador.
        Task<Specification> GetReferenceDataSpecification(string schema = null);                                //Retorna una especificación del estado actual.
        #endregion

        #region ReferenceDatasTypes
        Task<Fondos> GetFondos(string schema = null);       
        Task<Cedears> GetCedears(string schema = null);
        Task<Acciones> GetAcciones(string schema = null);
        Task<Acciones> GetAccionesADRS(string schema = null);
        Task<Acciones> GetAccionesPrivadas(string schema = null);
        Task<Acciones> GetAccionesPYMES(string schema = null);
        Task<Obligaciones> GetObligaciones(string schema = null);
        Task<Titulos> GetTitulos(string schema = null);        
        Task<Futuros> GetFuturos(string schema = null); 
        Task<ReferenceDatas> GetOpciones(string schema = null);
        Task<Pases> GetPases(string schema = null);
        Task<Cauciones> GetCauciones(string schema = null);
        Task<Plazos> GetPlazos(string schema = null);
        Task<Prestamos> GetPrestamosValores(string schema = null);
        Task<Indices> GetIndices(string schema = null);
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
