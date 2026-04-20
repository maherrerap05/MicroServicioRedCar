using System.Collections.Generic;
using MicroServicio.RedCar.Business.DTOs.CategoriaVehiculo;

namespace MicroServicio.RedCar.Business.Validators
{
    public static class CategoriaVehiculoValidator
    {
        public static IReadOnlyCollection<string> ValidarCreacion(CrearCategoriaVehiculoRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de creación de la categoría de vehículo no puede ser nula.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(request.codigo_categoria_vehiculo))
                errors.Add("El código de la categoría de vehículo es obligatorio.");

            if (request.codigo_categoria_vehiculo != null && request.codigo_categoria_vehiculo.Trim().Length > 20)
                errors.Add("El código de la categoría de vehículo no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.nombre_categoria_vehiculo))
                errors.Add("El nombre de la categoría de vehículo es obligatorio.");

            if (request.nombre_categoria_vehiculo != null && request.nombre_categoria_vehiculo.Trim().Length > 100)
                errors.Add("El nombre de la categoría de vehículo no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.descripcion_categoria_vehiculo) &&
                request.descripcion_categoria_vehiculo.Trim().Length > 250)
            {
                errors.Add("La descripción de la categoría de vehículo no puede exceder 250 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.estado_categoria_vehiculo))
                errors.Add("El estado de la categoría de vehículo es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_categoria_vehiculo) &&
                request.estado_categoria_vehiculo != "ACT" &&
                request.estado_categoria_vehiculo != "INA")
            {
                errors.Add("El estado de la categoría de vehículo debe ser ACT o INA.");
            }

            if (string.IsNullOrWhiteSpace(request.creado_por_usuario))
                errors.Add("El usuario creador es obligatorio.");

            if (request.creado_por_usuario != null && request.creado_por_usuario.Trim().Length > 100)
                errors.Add("El usuario creador no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.modificado_desde_ip) &&
                request.modificado_desde_ip.Trim().Length > 45)
            {
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.origen_registro))
                errors.Add("El origen del registro es obligatorio.");

            if (request.origen_registro != null && request.origen_registro.Trim().Length > 20)
                errors.Add("El origen del registro no puede exceder 20 caracteres.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarCategoriaVehiculoRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de actualización de la categoría de vehículo no puede ser nula.");
                return errors;
            }

            if (request.id_categoria_vehiculo <= 0)
                errors.Add("El id de la categoría de vehículo es inválido.");

            if (string.IsNullOrWhiteSpace(request.codigo_categoria_vehiculo))
                errors.Add("El código de la categoría de vehículo es obligatorio.");

            if (request.codigo_categoria_vehiculo != null && request.codigo_categoria_vehiculo.Trim().Length > 20)
                errors.Add("El código de la categoría de vehículo no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.nombre_categoria_vehiculo))
                errors.Add("El nombre de la categoría de vehículo es obligatorio.");

            if (request.nombre_categoria_vehiculo != null && request.nombre_categoria_vehiculo.Trim().Length > 100)
                errors.Add("El nombre de la categoría de vehículo no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.descripcion_categoria_vehiculo) &&
                request.descripcion_categoria_vehiculo.Trim().Length > 250)
            {
                errors.Add("La descripción de la categoría de vehículo no puede exceder 250 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.estado_categoria_vehiculo))
                errors.Add("El estado de la categoría de vehículo es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_categoria_vehiculo) &&
                request.estado_categoria_vehiculo != "ACT" &&
                request.estado_categoria_vehiculo != "INA")
            {
                errors.Add("El estado de la categoría de vehículo debe ser ACT o INA.");
            }

            if (string.IsNullOrWhiteSpace(request.modificado_por_usuario))
                errors.Add("El usuario modificador es obligatorio.");

            if (request.modificado_por_usuario != null && request.modificado_por_usuario.Trim().Length > 100)
                errors.Add("El usuario modificador no puede exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(request.modificado_desde_ip) &&
                request.modificado_desde_ip.Trim().Length > 45)
            {
                errors.Add("La IP de modificación no puede exceder 45 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.origen_registro))
                errors.Add("El origen del registro es obligatorio.");

            if (request.origen_registro != null && request.origen_registro.Trim().Length > 20)
                errors.Add("El origen del registro no puede exceder 20 caracteres.");

            if (request.estado_categoria_vehiculo == "INA")
            {
                if (string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                    errors.Add("El motivo de inhabilitación es obligatorio cuando el estado es INA.");

                if (!string.IsNullOrWhiteSpace(request.motivo_inhabilitacion) &&
                    request.motivo_inhabilitacion.Trim().Length > 200)
                {
                    errors.Add("El motivo de inhabilitación no puede exceder 200 caracteres.");
                }
            }

            if (request.estado_categoria_vehiculo == "ACT" && !string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                errors.Add("No debe enviarse motivo de inhabilitación cuando el estado es ACT.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarFiltro(CategoriaVehiculoFiltroRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de filtro no puede ser nula.");
                return errors;
            }

            if (!string.IsNullOrWhiteSpace(request.codigo_categoria_vehiculo) &&
                request.codigo_categoria_vehiculo.Trim().Length > 20)
            {
                errors.Add("El código de la categoría de vehículo no puede exceder 20 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.nombre_categoria_vehiculo) &&
                request.nombre_categoria_vehiculo.Trim().Length > 100)
            {
                errors.Add("El nombre de la categoría de vehículo no puede exceder 100 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.descripcion_categoria_vehiculo) &&
                request.descripcion_categoria_vehiculo.Trim().Length > 250)
            {
                errors.Add("La descripción de la categoría de vehículo no puede exceder 250 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.estado_categoria_vehiculo) &&
                request.estado_categoria_vehiculo != "ACT" &&
                request.estado_categoria_vehiculo != "INA")
            {
                errors.Add("El estado del filtro debe ser ACT o INA.");
            }

            if (!string.IsNullOrWhiteSpace(request.origen_registro) &&
                request.origen_registro.Trim().Length > 20)
            {
                errors.Add("El origen del registro no puede exceder 20 caracteres.");
            }

            if (request.page_number <= 0)
                errors.Add("El número de página debe ser mayor que cero.");

            if (request.page_size <= 0)
                errors.Add("El tamaño de página debe ser mayor que cero.");

            if (request.page_size > 100)
                errors.Add("El tamaño de página no puede ser mayor a 100.");

            return errors;
        }
    }
}