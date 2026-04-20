using System;
using System.Collections.Generic;

namespace MicroServicio.RedCar.Business.Exceptions
{
    /// <summary>
    /// Excepción utilizada cuando una solicitud no cumple con las validaciones de negocio.
    /// Permite manejar múltiples errores de validación.
    /// </summary>
    public class ValidationException : BusinessException
    {
        /// <summary>
        /// Lista de errores de validación encontrados.
        /// </summary>
        public IReadOnlyCollection<string> Errors { get; }

        public ValidationException(string message, IReadOnlyCollection<string>? errors = null)
            : base(message)
        {
            Errors = errors ?? Array.Empty<string>();
        }
    }
}