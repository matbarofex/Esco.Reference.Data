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
    public interface IApiBoServices
    {
        void ChangeSuscriptionKey(string key);              //Cambiar la Suscription Key del usuario

        #region Cuerrencies
        Task<CurrenciesToResponse> GetCurrencies();                      // Retorna la lista de Monedas de Referencedata
        #endregion
    }
}
