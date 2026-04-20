using System;
using System.Collections.Generic;
using MicroServicio.RedCar.Business.DTOs.Vehiculo;

namespace MicroServicio.RedCar.Business.Validators
{
    public static class VehiculoValidator
    {
        public static IReadOnlyCollection<string> ValidarCreacion(CrearVehiculoRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de creación del vehículo no puede ser nula.");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(request.codigo_interno_vehiculo))
                errors.Add("El código interno del vehículo es obligatorio.");

            if (request.codigo_interno_vehiculo != null && request.codigo_interno_vehiculo.Trim().Length > 20)
                errors.Add("El código interno del vehículo no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.placa_vehiculo))
                errors.Add("La placa del vehículo es obligatoria.");

            if (request.placa_vehiculo != null && request.placa_vehiculo.Trim().Length > 15)
                errors.Add("La placa del vehículo no puede exceder 15 caracteres.");

            if (string.IsNullOrWhiteSpace(request.modelo_vehiculo))
                errors.Add("El modelo del vehículo es obligatorio.");

            if (request.modelo_vehiculo != null && request.modelo_vehiculo.Trim().Length > 50)
                errors.Add("El modelo del vehículo no puede exceder 50 caracteres.");

            if (request.anio_fabricacion <= 0)
                errors.Add("El año de fabricación es obligatorio.");

            if (request.anio_fabricacion < 1900)
                errors.Add("El año de fabricación no puede ser menor a 1900.");

            if (request.anio_fabricacion > DateTime.UtcNow.Year + 1)
                errors.Add("El año de fabricación no puede ser mayor al año siguiente del actual.");

            if (string.IsNullOrWhiteSpace(request.color_vehiculo))
                errors.Add("El color del vehículo es obligatorio.");

            if (request.color_vehiculo != null && request.color_vehiculo.Trim().Length > 30)
                errors.Add("El color del vehículo no puede exceder 30 caracteres.");

            if (string.IsNullOrWhiteSpace(request.tipo_combustible))
                errors.Add("El tipo de combustible es obligatorio.");

            if (request.tipo_combustible != null && request.tipo_combustible.Trim().Length > 20)
                errors.Add("El tipo de combustible no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.tipo_transmision))
                errors.Add("El tipo de transmisión es obligatorio.");

            if (request.tipo_transmision != null && request.tipo_transmision.Trim().Length > 20)
                errors.Add("El tipo de transmisión no puede exceder 20 caracteres.");

            if (request.capacidad_pasajeros <= 0)
                errors.Add("La capacidad de pasajeros debe ser mayor que cero.");

            if (request.numero_puertas <= 0)
                errors.Add("El número de puertas debe ser mayor que cero.");

            if (request.localizacion_actual <= 0)
                errors.Add("La localización actual es obligatoria.");

            if (request.precio_base_dia <= 0)
                errors.Add("El precio base por día debe ser mayor que cero.");

            if (request.kilometraje_actual < 0)
                errors.Add("El kilometraje actual no puede ser negativo.");

            if (!string.IsNullOrWhiteSpace(request.observaciones_generales) &&
                request.observaciones_generales.Trim().Length > 300)
            {
                errors.Add("Las observaciones generales no pueden exceder 300 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.imagen_referencial_url) &&
                request.imagen_referencial_url.Trim().Length > 300)
            {
                errors.Add("La imagen referencial URL no puede exceder 300 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.estado_vehiculo))
                errors.Add("El estado del vehículo es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_vehiculo) &&
                request.estado_vehiculo != "ACT" &&
                request.estado_vehiculo != "INA")
            {
                errors.Add("El estado del vehículo debe ser ACT o INA.");
            }

            if (request.id_marca_vehiculo <= 0)
                errors.Add("La marca del vehículo es obligatoria.");

            if (request.id_categoria_vehiculo <= 0)
                errors.Add("La categoría del vehículo es obligatoria.");

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

        public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarVehiculoRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de actualización del vehículo no puede ser nula.");
                return errors;
            }

            if (request.id_vehiculo <= 0)
                errors.Add("El id del vehículo es inválido.");

            if (string.IsNullOrWhiteSpace(request.codigo_interno_vehiculo))
                errors.Add("El código interno del vehículo es obligatorio.");

            if (request.codigo_interno_vehiculo != null && request.codigo_interno_vehiculo.Trim().Length > 20)
                errors.Add("El código interno del vehículo no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.placa_vehiculo))
                errors.Add("La placa del vehículo es obligatoria.");

            if (request.placa_vehiculo != null && request.placa_vehiculo.Trim().Length > 15)
                errors.Add("La placa del vehículo no puede exceder 15 caracteres.");

            if (string.IsNullOrWhiteSpace(request.modelo_vehiculo))
                errors.Add("El modelo del vehículo es obligatorio.");

            if (request.modelo_vehiculo != null && request.modelo_vehiculo.Trim().Length > 50)
                errors.Add("El modelo del vehículo no puede exceder 50 caracteres.");

            if (request.anio_fabricacion <= 0)
                errors.Add("El año de fabricación es obligatorio.");

            if (request.anio_fabricacion < 1900)
                errors.Add("El año de fabricación no puede ser menor a 1900.");

            if (request.anio_fabricacion > DateTime.UtcNow.Year + 1)
                errors.Add("El año de fabricación no puede ser mayor al año siguiente del actual.");

            if (string.IsNullOrWhiteSpace(request.color_vehiculo))
                errors.Add("El color del vehículo es obligatorio.");

            if (request.color_vehiculo != null && request.color_vehiculo.Trim().Length > 30)
                errors.Add("El color del vehículo no puede exceder 30 caracteres.");

            if (string.IsNullOrWhiteSpace(request.tipo_combustible))
                errors.Add("El tipo de combustible es obligatorio.");

            if (request.tipo_combustible != null && request.tipo_combustible.Trim().Length > 20)
                errors.Add("El tipo de combustible no puede exceder 20 caracteres.");

            if (string.IsNullOrWhiteSpace(request.tipo_transmision))
                errors.Add("El tipo de transmisión es obligatorio.");

            if (request.tipo_transmision != null && request.tipo_transmision.Trim().Length > 20)
                errors.Add("El tipo de transmisión no puede exceder 20 caracteres.");

            if (request.capacidad_pasajeros <= 0)
                errors.Add("La capacidad de pasajeros debe ser mayor que cero.");

            if (request.numero_puertas <= 0)
                errors.Add("El número de puertas debe ser mayor que cero.");

            if (request.localizacion_actual <= 0)
                errors.Add("La localización actual es obligatoria.");

            if (request.precio_base_dia <= 0)
                errors.Add("El precio base por día debe ser mayor que cero.");

            if (request.kilometraje_actual < 0)
                errors.Add("El kilometraje actual no puede ser negativo.");

            if (!string.IsNullOrWhiteSpace(request.observaciones_generales) &&
                request.observaciones_generales.Trim().Length > 300)
            {
                errors.Add("Las observaciones generales no pueden exceder 300 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.imagen_referencial_url) &&
                request.imagen_referencial_url.Trim().Length > 300)
            {
                errors.Add("La imagen referencial URL no puede exceder 300 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.estado_vehiculo))
                errors.Add("El estado del vehículo es obligatorio.");

            if (!string.IsNullOrWhiteSpace(request.estado_vehiculo) &&
                request.estado_vehiculo != "ACT" &&
                request.estado_vehiculo != "INA")
            {
                errors.Add("El estado del vehículo debe ser ACT o INA.");
            }

            if (request.id_marca_vehiculo <= 0)
                errors.Add("La marca del vehículo es obligatoria.");

            if (request.id_categoria_vehiculo <= 0)
                errors.Add("La categoría del vehículo es obligatoria.");

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

            if (request.estado_vehiculo == "INA")
            {
                if (string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                    errors.Add("El motivo de inhabilitación es obligatorio cuando el estado es INA.");

                if (!string.IsNullOrWhiteSpace(request.motivo_inhabilitacion) &&
                    request.motivo_inhabilitacion.Trim().Length > 200)
                {
                    errors.Add("El motivo de inhabilitación no puede exceder 200 caracteres.");
                }
            }

            if (request.estado_vehiculo == "ACT" && !string.IsNullOrWhiteSpace(request.motivo_inhabilitacion))
                errors.Add("No debe enviarse motivo de inhabilitación cuando el estado es ACT.");

            return errors;
        }

        public static IReadOnlyCollection<string> ValidarFiltro(VehiculoFiltroRequest request)
        {
            var errors = new List<string>();

            if (request == null)
            {
                errors.Add("La solicitud de filtro no puede ser nula.");
                return errors;
            }

            if (!string.IsNullOrWhiteSpace(request.codigo_interno_vehiculo) &&
                request.codigo_interno_vehiculo.Trim().Length > 20)
            {
                errors.Add("El código interno del vehículo no puede exceder 20 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.placa_vehiculo) &&
                request.placa_vehiculo.Trim().Length > 15)
            {
                errors.Add("La placa del vehículo no puede exceder 15 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.modelo_vehiculo) &&
                request.modelo_vehiculo.Trim().Length > 50)
            {
                errors.Add("El modelo del vehículo no puede exceder 50 caracteres.");
            }

            if (request.anio_fabricacion.HasValue)
            {
                if (request.anio_fabricacion.Value < 1900)
                    errors.Add("El año de fabricación no puede ser menor a 1900.");

                if (request.anio_fabricacion.Value > DateTime.UtcNow.Year + 1)
                    errors.Add("El año de fabricación no puede ser mayor al año siguiente del actual.");
            }

            if (!string.IsNullOrWhiteSpace(request.color_vehiculo) &&
                request.color_vehiculo.Trim().Length > 30)
            {
                errors.Add("El color del vehículo no puede exceder 30 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.tipo_combustible) &&
                request.tipo_combustible.Trim().Length > 20)
            {
                errors.Add("El tipo de combustible no puede exceder 20 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(request.tipo_transmision) &&
                request.tipo_transmision.Trim().Length > 20)
            {
                errors.Add("El tipo de transmisión no puede exceder 20 caracteres.");
            }

            if (request.capacidad_pasajeros.HasValue && request.capacidad_pasajeros.Value <= 0)
                errors.Add("La capacidad de pasajeros debe ser mayor que cero.");

            if (request.capacidad_maletas.HasValue && request.capacidad_maletas.Value < 0)
                errors.Add("La capacidad de maletas no puede ser negativa.");

            if (request.numero_puertas.HasValue && request.numero_puertas.Value <= 0)
                errors.Add("El número de puertas debe ser mayor que cero.");

            if (request.localizacion_actual.HasValue && request.localizacion_actual.Value <= 0)
                errors.Add("La localización actual debe ser mayor que cero.");

            if (request.precio_base_dia_min.HasValue && request.precio_base_dia_min.Value <= 0)
                errors.Add("El precio base por día mínimo debe ser mayor que cero.");

            if (request.precio_base_dia_max.HasValue && request.precio_base_dia_max.Value <= 0)
                errors.Add("El precio base por día máximo debe ser mayor que cero.");

            if (request.precio_base_dia_min.HasValue && request.precio_base_dia_max.HasValue &&
                request.precio_base_dia_min.Value > request.precio_base_dia_max.Value)
            {
                errors.Add("El precio base por día mínimo no puede ser mayor que el máximo.");
            }

            if (request.kilometraje_actual_min.HasValue && request.kilometraje_actual_min.Value < 0)
                errors.Add("El kilometraje actual mínimo no puede ser negativo.");

            if (request.kilometraje_actual_max.HasValue && request.kilometraje_actual_max.Value < 0)
                errors.Add("El kilometraje actual máximo no puede ser negativo.");

            if (request.kilometraje_actual_min.HasValue && request.kilometraje_actual_max.HasValue &&
                request.kilometraje_actual_min.Value > request.kilometraje_actual_max.Value)
            {
                errors.Add("El kilometraje actual mínimo no puede ser mayor que el máximo.");
            }

            if (!string.IsNullOrWhiteSpace(request.estado_vehiculo) &&
                request.estado_vehiculo != "ACT" &&
                request.estado_vehiculo != "INA")
            {
                errors.Add("El estado del filtro debe ser ACT o INA.");
            }

            if (request.id_marca_vehiculo.HasValue && request.id_marca_vehiculo.Value <= 0)
                errors.Add("La marca del vehículo debe ser mayor que cero.");

            if (request.id_categoria_vehiculo.HasValue && request.id_categoria_vehiculo.Value <= 0)
                errors.Add("La categoría del vehículo debe ser mayor que cero.");

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