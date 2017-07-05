using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EfRepository.Manager
{
    /// <summary>
    /// Interfaz que define el estado de respuesta de peticiones a la capa de logica de negocios.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public interface IManagerResponse<TObject>
    {
        /// <summary>
        /// Detalle de respuesta
        /// </summary>
        string Response { get; set; }
        /// <summary>
        /// Si la respuesta define una transaccion satisfactoria
        /// </summary>
        bool IsSucess { get; set; }

        /// <summary>
        /// Encapsula la excepcion principal si es que se genera
        /// </summary>
        Exception Exception { get; set; }

        /// <summary>
        /// Generico que permite devolver objetos con información agregada desde la logica de negocios
        /// </summary>
        TObject ObjectResponse { get; set; }
    }
}