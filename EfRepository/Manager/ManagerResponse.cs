using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EfRepository.Manager
{

    /// <summary>
    /// Clase concreta que envia respuesta de las operaciones en capa de logica de negocios
    /// </summary>
    public class ManagerResponse<TObject> : IManagerResponse<TObject>
    {
        /// <summary>
        /// Detalle de respuesta
        /// </summary>
       public string Response { get; set; }
        /// <summary>
        /// Si la respuesta define una transaccion satisfactoria
        /// </summary>
        public bool IsSucess { get; set; }

        /// <summary>
        /// Encapsula la excepcion principal si es que se genera
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Generico que permite devolver objetos con información agregada desde la logica de negocios
        /// </summary>
        public TObject ObjectResponse { get; set; }
    }
}