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
        Task<Currencys> GetCurrencys(string schema = null);                        // Retorna la lista de Monedas 
        ReferenceDataTypes GetReferenceDataTypes(string schema = null);            // Retorna la lista de tipos de instrumentos financieros
        #endregion
    }
}
