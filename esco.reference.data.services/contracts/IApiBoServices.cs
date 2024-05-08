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
        #region Cuerrencies
        Task<CurrenciesToResponse> Currencies();                      // Retorna la lista de Monedas de Referencedata
        #endregion
    }
}
