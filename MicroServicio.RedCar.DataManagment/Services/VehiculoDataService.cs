using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Mappers;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Services
{
    public class VehiculoDataService : IVehiculoDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehiculoDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<VehiculoDataModel?> ObtenerPorIdAsync(int id_vehiculo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.VehiculoRepository.ObtenerPorIdAsync(id_vehiculo, cancellationToken);

            return entity is null
                ? null
                : VehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<VehiculoDataModel?> ObtenerPorGuidAsync(Guid vehiculo_guid, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.VehiculoRepository.ObtenerPorGuidAsync(vehiculo_guid, cancellationToken);

            return entity is null
                ? null
                : VehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<VehiculoDataModel?> ObtenerPorCodigoAsync(string codigo_interno_vehiculo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.VehiculoRepository.ObtenerPorCodigoAsync(codigo_interno_vehiculo, cancellationToken);

            return entity is null
                ? null
                : VehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<VehiculoDataModel?> ObtenerPorPlacaAsync(string placa_vehiculo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.VehiculoRepository.ObtenerPorPlacaAsync(placa_vehiculo, cancellationToken);

            return entity is null
                ? null
                : VehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<IReadOnlyList<VehiculoDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.VehiculoRepository.ObtenerTodosAsync(cancellationToken);

            return entities
                .Select(VehiculoDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<DataPagedResult<VehiculoDataModel>> BuscarAsync(VehiculoFiltroDataModel filtro, CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.VehiculoQueryRepository.BuscarAsync(
                filtro.codigo_interno_vehiculo,
                filtro.placa_vehiculo,
                filtro.modelo_vehiculo,
                filtro.tipo_combustible,
                filtro.tipo_transmision,
                filtro.id_marca_vehiculo,
                filtro.id_categoria_vehiculo,
                filtro.localizacion_actual,
                filtro.estado_vehiculo,
                filtro.precio_min,
                filtro.precio_max,
                filtro.PageNumber,
                filtro.PageSize,
                cancellationToken);

            return new DataPagedResult<VehiculoDataModel>
            {
                Items = result.Items
                    .Select(VehiculoDataMapper.ToDataModel)
                    .ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords
            };
        }

        public async Task<IReadOnlyList<VehiculoDataModel>> ObtenerDisponiblesAsync(
            int id_localizacion_recogida,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.VehiculoQueryRepository.ObtenerDisponiblesAsync(
                id_localizacion_recogida,
                fecha_hora_recogida,
                fecha_hora_devolucion,
                cancellationToken);

            return entities
                .Select(VehiculoDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<IReadOnlyList<VehiculoDataModel>> ObtenerDisponiblesPorCategoriaAsync(
            int id_localizacion_recogida,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            int id_categoria_vehiculo,
            CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.VehiculoQueryRepository.ObtenerDisponiblesPorCategoriaAsync(
                id_localizacion_recogida,
                fecha_hora_recogida,
                fecha_hora_devolucion,
                id_categoria_vehiculo,
                cancellationToken);

            return entities
                .Select(VehiculoDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<bool> EstaDisponibleAsync(
            int id_vehiculo,
            DateTime fecha_hora_recogida,
            DateTime fecha_hora_devolucion,
            CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.VehiculoQueryRepository.EstaDisponibleAsync(
                id_vehiculo,
                fecha_hora_recogida,
                fecha_hora_devolucion,
                cancellationToken);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<VehiculoDataModel> CrearAsync(VehiculoDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = VehiculoDataMapper.ToEntity(model);

            await _unitOfWork.VehiculoRepository.AgregarAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return VehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<VehiculoDataModel?> ActualizarAsync(VehiculoDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.VehiculoRepository.ObtenerParaActualizarAsync(model.id_vehiculo, cancellationToken);

            if (entity is null)
                return null;

            entity.codigo_interno_vehiculo = model.codigo_interno_vehiculo;
            entity.placa_vehiculo = model.placa_vehiculo;
            entity.modelo_vehiculo = model.modelo_vehiculo;
            entity.anio_fabricacion = model.anio_fabricacion;
            entity.color_vehiculo = model.color_vehiculo;
            entity.tipo_combustible = model.tipo_combustible;
            entity.tipo_transmision = model.tipo_transmision;
            entity.capacidad_pasajeros = model.capacidad_pasajeros;
            entity.capacidad_maletas = model.capacidad_maletas;
            entity.numero_puertas = model.numero_puertas;
            entity.aire_acondicionado = model.aire_acondicionado;
            entity.localizacion_actual = model.localizacion_actual;
            entity.precio_base_dia = model.precio_base_dia;
            entity.kilometraje_actual = model.kilometraje_actual;
            entity.observaciones_generales = model.observaciones_generales;
            entity.imagen_referencial_url = model.imagen_referencial_url;
            entity.id_marca_vehiculo = model.id_marca_vehiculo;
            entity.id_categoria_vehiculo = model.id_categoria_vehiculo;

            entity.estado_vehiculo = model.estado_vehiculo;
            entity.es_eliminado = model.es_eliminado;

            entity.origen_registro = model.origen_registro;

            entity.modificado_por_usuario = model.modificado_por_usuario;
            entity.fecha_modificacion_utc = model.fecha_modificacion_utc;
            entity.modificado_desde_ip = model.modificado_desde_ip;

            entity.fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc;
            entity.motivo_inhabilitacion = model.motivo_inhabilitacion;

            _unitOfWork.VehiculoRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return VehiculoDataMapper.ToDataModel(entity);
        }

        public async Task<bool> ActualizarLocalizacionAsync(
            int id_vehiculo,
            int id_localizacion,
            string modificado_por_usuario,
            CancellationToken cancellationToken = default)
        {
            var resultado = await _unitOfWork.VehiculoRepository.ActualizarLocalizacionAsync(
                id_vehiculo,
                id_localizacion,
                modificado_por_usuario,
                cancellationToken);

            if (!resultado)
                return false;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        public async Task<bool> EliminarLogicoAsync(int id_vehiculo, string usuario, string? motivo, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.VehiculoRepository.ObtenerParaActualizarAsync(id_vehiculo, cancellationToken);

            if (entity is null)
                return false;

            entity.estado_vehiculo = "INA";
            entity.es_eliminado = true;
            entity.fecha_inhabilitacion_utc = DateTime.UtcNow;
            entity.fecha_modificacion_utc = DateTime.UtcNow;
            entity.modificado_por_usuario = usuario;
            entity.motivo_inhabilitacion = motivo;

            _unitOfWork.VehiculoRepository.Actualizar(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorCodigoAsync(string codigo_interno_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.VehiculoRepository.ExistePorCodigoAsync(codigo_interno_vehiculo, cancellationToken);
        }

        public async Task<bool> ExistePorPlacaAsync(string placa_vehiculo, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.VehiculoRepository.ExistePorPlacaAsync(placa_vehiculo, cancellationToken);
        }
    }
}