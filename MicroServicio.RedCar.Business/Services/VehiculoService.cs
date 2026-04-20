using MicroServicio.RedCar.Business.DTOs.Vehiculo;
using MicroServicio.RedCar.Business.Exceptions;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.Business.Mappers;
using MicroServicio.RedCar.Business.Validators;
using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoDataService _vehiculoDataService;

        public VehiculoService(IVehiculoDataService vehiculoDataService)
        {
            _vehiculoDataService = vehiculoDataService;
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<VehiculoResponse> CrearAsync(CrearVehiculoRequest request, CancellationToken cancellationToken = default)
        {
            var errors = VehiculoValidator.ValidarCreacion(request);

            if (errors.Any())
                throw new ValidationException("La solicitud de creación del vehículo es inválida.", errors);

            var existentePorCodigo = await _vehiculoDataService
                .ObtenerPorCodigoAsync(request.codigo_interno_vehiculo, cancellationToken);

            if (existentePorCodigo is not null)
                throw new ValidationException("Ya existe un vehículo con el código interno indicado.");

            var existentePorPlaca = await _vehiculoDataService
                .ObtenerPorPlacaAsync(request.placa_vehiculo, cancellationToken);

            if (existentePorPlaca is not null)
                throw new ValidationException("Ya existe un vehículo con la placa indicada.");

            var dataModel = VehiculoBusinessMapper.ToDataModel(request);

            var creado = await _vehiculoDataService.CrearAsync(dataModel, cancellationToken);

            return VehiculoBusinessMapper.ToResponse(creado);
        }

        public async Task<VehiculoResponse> ActualizarAsync(ActualizarVehiculoRequest request, CancellationToken cancellationToken = default)
        {
            var errors = VehiculoValidator.ValidarActualizacion(request);

            if (errors.Any())
                throw new ValidationException("La solicitud de actualización del vehículo es inválida.", errors);

            var existente = await _vehiculoDataService.ObtenerPorIdAsync(request.id_vehiculo, cancellationToken);

            if (existente is null)
                throw new NotFoundException("No se encontró el vehículo solicitado.");

            var porCodigo = await _vehiculoDataService
                .ObtenerPorCodigoAsync(request.codigo_interno_vehiculo, cancellationToken);

            if (porCodigo is not null && porCodigo.id_vehiculo != request.id_vehiculo)
                throw new ValidationException("Ya existe otro vehículo con el código interno indicado.");

            var porPlaca = await _vehiculoDataService
                .ObtenerPorPlacaAsync(request.placa_vehiculo, cancellationToken);

            if (porPlaca is not null && porPlaca.id_vehiculo != request.id_vehiculo)
                throw new ValidationException("Ya existe otro vehículo con la placa indicada.");

            var dataModel = VehiculoBusinessMapper.ToDataModel(request);

            // =========================
            // PRESERVAR DATOS ORIGINALES
            // =========================
            dataModel.vehiculo_guid = existente.vehiculo_guid;
            dataModel.fecha_registro_utc = existente.fecha_registro_utc;
            dataModel.creado_por_usuario = existente.creado_por_usuario;
            dataModel.row_version = existente.row_version;
            dataModel.es_eliminado = existente.es_eliminado;

            // =========================
            // CONTROL DE INHABILITACIÓN
            // =========================
            if (request.estado_vehiculo == "ACT")
            {
                dataModel.fecha_inhabilitacion_utc = null;
                dataModel.motivo_inhabilitacion = null;
            }
            else if (request.estado_vehiculo == "INA" && existente.estado_vehiculo == "INA")
            {
                dataModel.fecha_inhabilitacion_utc = existente.fecha_inhabilitacion_utc ?? dataModel.fecha_inhabilitacion_utc;
            }

            var actualizado = await _vehiculoDataService.ActualizarAsync(dataModel, cancellationToken);

            if (actualizado is null)
                throw new NotFoundException("No se pudo actualizar el vehículo porque no existe.");

            return VehiculoBusinessMapper.ToResponse(actualizado);
        }

        public async Task EliminarLogicoAsync(int id_vehiculo, string usuario, string? motivo, CancellationToken cancellationToken = default)
        {
            if (id_vehiculo <= 0)
                throw new ValidationException("El id_vehiculo es inválido.");

            if (string.IsNullOrWhiteSpace(usuario))
                throw new ValidationException("El usuario es obligatorio para la eliminación lógica.");

            var eliminado = await _vehiculoDataService.EliminarLogicoAsync(id_vehiculo, usuario, motivo, cancellationToken);

            if (!eliminado)
                throw new NotFoundException("No se encontró el vehículo para eliminación lógica.");
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<VehiculoResponse> ObtenerPorIdAsync(int id_vehiculo, CancellationToken cancellationToken = default)
        {
            if (id_vehiculo <= 0)
                throw new ValidationException("El id_vehiculo es inválido.");

            var vehiculo = await _vehiculoDataService.ObtenerPorIdAsync(id_vehiculo, cancellationToken);

            if (vehiculo is null)
                throw new NotFoundException("No se encontró el vehículo solicitado.");

            return VehiculoBusinessMapper.ToResponse(vehiculo);
        }

        public async Task<VehiculoResponse> ObtenerPorGuidAsync(Guid vehiculo_guid, CancellationToken cancellationToken = default)
        {
            if (vehiculo_guid == Guid.Empty)
                throw new ValidationException("El vehiculo_guid es obligatorio.");

            var vehiculo = await _vehiculoDataService.ObtenerPorGuidAsync(vehiculo_guid, cancellationToken);

            if (vehiculo is null)
                throw new NotFoundException("No se encontró el vehículo solicitado.");

            return VehiculoBusinessMapper.ToResponse(vehiculo);
        }

        public async Task<VehiculoResponse> ObtenerPorCodigoAsync(string codigo_interno_vehiculo, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(codigo_interno_vehiculo))
                throw new ValidationException("El código interno del vehículo es obligatorio.");

            var vehiculo = await _vehiculoDataService.ObtenerPorCodigoAsync(codigo_interno_vehiculo, cancellationToken);

            if (vehiculo is null)
                throw new NotFoundException("No se encontró el vehículo con el código indicado.");

            return VehiculoBusinessMapper.ToResponse(vehiculo);
        }

        public async Task<VehiculoResponse> ObtenerPorPlacaAsync(string placa_vehiculo, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(placa_vehiculo))
                throw new ValidationException("La placa del vehículo es obligatoria.");

            var vehiculo = await _vehiculoDataService.ObtenerPorPlacaAsync(placa_vehiculo, cancellationToken);

            if (vehiculo is null)
                throw new NotFoundException("No se encontró el vehículo con la placa indicada.");

            return VehiculoBusinessMapper.ToResponse(vehiculo);
        }

        public async Task<IReadOnlyList<VehiculoResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var vehiculos = await _vehiculoDataService.ObtenerTodosAsync(cancellationToken);

            return vehiculos
                .Select(VehiculoBusinessMapper.ToResponse)
                .ToList();
        }

        public async Task<DataPagedResult<VehiculoResponse>> BuscarAsync(VehiculoFiltroRequest request, CancellationToken cancellationToken = default)
        {
            var errors = VehiculoValidator.ValidarFiltro(request);

            if (errors.Any())
                throw new ValidationException("La solicitud de búsqueda de vehículos es inválida.", errors);

            var filtro = new VehiculoFiltroDataModel
            {
                codigo_interno_vehiculo = request.codigo_interno_vehiculo,
                placa_vehiculo = request.placa_vehiculo,
                modelo_vehiculo = request.modelo_vehiculo,
                tipo_combustible = request.tipo_combustible,
                tipo_transmision = request.tipo_transmision,
                id_marca_vehiculo = request.id_marca_vehiculo,
                id_categoria_vehiculo = request.id_categoria_vehiculo,
                localizacion_actual = request.localizacion_actual,
                estado_vehiculo = request.estado_vehiculo,
                precio_min = request.precio_base_dia_min,
                precio_max = request.precio_base_dia_max,
                PageNumber = request.page_number,
                PageSize = request.page_size
            };

            var result = await _vehiculoDataService.BuscarAsync(filtro, cancellationToken);

            return new DataPagedResult<VehiculoResponse>
            {
                Items = result.Items
                    .Select(VehiculoBusinessMapper.ToResponse)
                    .ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords
            };
        }

        // =========================
        // DISPONIBILIDAD
        // =========================
        public async Task<IReadOnlyList<VehiculoResponse>> ObtenerDisponiblesAsync(
            int id_localizacion_recogida,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            CancellationToken cancellationToken = default)
        {
            if (id_localizacion_recogida <= 0)
                throw new ValidationException("La localización de recogida es inválida.");

            if (fecha_hora_recogida >= fecha_hora_devolucion)
                throw new ValidationException("La fecha/hora de devolución debe ser mayor a la fecha/hora de recogida.");

            var vehiculos = await _vehiculoDataService.ObtenerDisponiblesAsync(
                id_localizacion_recogida,
                fecha_hora_recogida,
                fecha_hora_devolucion,
                cancellationToken);

            return vehiculos
                .Select(VehiculoBusinessMapper.ToResponse)
                .ToList();
        }

        public async Task<IReadOnlyList<VehiculoResponse>> ObtenerDisponiblesPorCategoriaAsync(
            int id_localizacion_recogida,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            int id_categoria_vehiculo,
            CancellationToken cancellationToken = default)
        {
            if (id_localizacion_recogida <= 0)
                throw new ValidationException("La localización de recogida es inválida.");

            if (id_categoria_vehiculo <= 0)
                throw new ValidationException("La categoría de vehículo es inválida.");

            if (fecha_hora_recogida >= fecha_hora_devolucion)
                throw new ValidationException("La fecha/hora de devolución debe ser mayor a la fecha/hora de recogida.");

            var vehiculos = await _vehiculoDataService.ObtenerDisponiblesPorCategoriaAsync(
                id_localizacion_recogida,
                fecha_hora_recogida,
                fecha_hora_devolucion,
                id_categoria_vehiculo,
                cancellationToken);

            return vehiculos
                .Select(VehiculoBusinessMapper.ToResponse)
                .ToList();
        }

        public async Task<bool> EstaDisponibleAsync(
            int id_vehiculo,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            CancellationToken cancellationToken = default)
        {
            if (id_vehiculo <= 0)
                throw new ValidationException("El id_vehiculo es inválido.");

            if (fecha_hora_recogida >= fecha_hora_devolucion)
                throw new ValidationException("La fecha/hora de devolución debe ser mayor a la fecha/hora de recogida.");

            var vehiculo = await _vehiculoDataService.ObtenerPorIdAsync(id_vehiculo, cancellationToken);

            if (vehiculo is null)
                throw new NotFoundException("No se encontró el vehículo solicitado.");

            return await _vehiculoDataService.EstaDisponibleAsync(
                id_vehiculo,
                fecha_hora_recogida,
                fecha_hora_devolucion,
                cancellationToken);
        }
    }
}