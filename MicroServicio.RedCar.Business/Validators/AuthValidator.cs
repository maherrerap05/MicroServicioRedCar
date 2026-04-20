using System.Collections.Generic;
using MicroServicio.RedCar.Business.DTOs.Auth;

namespace MicroServicio.RedCar.Business.Validators
{
    public static class AuthValidator
    {
        public static IReadOnlyCollection<string> ValidarLogin(LoginRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de login no puede ser nula.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(request.UserName))
                errors.Add("El nombre de usuario es obligatorio.");

            if (request.UserName != null && request.UserName.Trim().Length > 50)
                errors.Add("El nombre de usuario no puede exceder 50 caracteres.");

            if (string.IsNullOrWhiteSpace(request.Password))
                errors.Add("La contraseña es obligatoria.");

            if (request.Password != null && request.Password.Trim().Length > 500)
                errors.Add("La contraseña no puede exceder 500 caracteres.");

            return errors;
        }
    }
}