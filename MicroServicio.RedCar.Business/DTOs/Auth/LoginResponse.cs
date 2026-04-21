using System;
using System.Collections.Generic;

namespace MicroServicio.RedCar.Business.DTOs.Auth
{
    public class LoginResponse
    {
        public string UserName { get; set; } = null!;
        public string Correo { get; set; } = null!;

        public bool Activo { get; set; }

        // AGREGADO: id_cliente para restricción de acceso en endpoints de reservas.
        // Es nullable porque usuarios con rol ADMIN o VENDEDOR no tienen cliente asociado.
        public int? IdCliente { get; set; }

        public IReadOnlyCollection<string> Roles { get; set; } = Array.Empty<string>();

        public string Token { get; set; } = null!;
        public DateTime ExpirationUtc { get; set; }
    }
}