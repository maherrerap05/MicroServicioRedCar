using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Mappers;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Services
{
    public class ReservaDataService : IReservaDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservaDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<ReservaDataModel?> ObtenerPorIdAsync(int id_reserva, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ReservaRepository.ObtenerPorIdAsync(id_reserva, cancellationToken);

            return entity is null
                ? null
                : ReservaDataMapper.ToDataModel(entity);
        }

        public async Task<ReservaDataModel?> ObtenerPorGuidAsync(Guid guid_reserva, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ReservaRepository.ObtenerPorGuidAsync(guid_reserva, cancellationToken);

            return entity is null
                ? null
                : ReservaDataMapper.ToDataModel(entity);
        }

        public async Task<ReservaDataModel?> ObtenerPorCodigoAsync(string codigo_reserva, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ReservaRepository.ObtenerPorCodigoAsync(codigo_reserva, cancellationToken);

            return entity is null
                ? null
                : ReservaDataMapper.ToDataModel(entity);
        }

        public async Task<IReadOnlyList<ReservaDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.ReservaRepository.ObtenerTodosAsync(cancellationToken);

            return entities
                .Select(ReservaDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<DataPagedResult<ReservaDataModel>> BuscarAsync(ReservaFiltroDataModel filtro, CancellationToken cancellationToken = default)
        {
            var result = await _unitOfWork.ReservaQueryRepository.BuscarAsync(
                filtro.codigo_reserva,
                filtro.id_cliente,
                filtro.id_vehiculo,
                filtro.id_localizacion_recogida,
                filtro.id_localizacion_devolucion,
                filtro.estado_reserva,
                filtro.origen_canal_reserva,
                filtro.fecha_inicio_desde,
                filtro.fecha_inicio_hasta,
                filtro.fecha_fin_desde,
                filtro.fecha_fin_hasta,
                filtro.total_min,
                filtro.total_max,
                filtro.PageNumber,
                filtro.PageSize,
                cancellationToken);

            return new DataPagedResult<ReservaDataModel>
            {
                Items = result.Items
                    .Select(ReservaDataMapper.ToDataModel)
                    .ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords
            };
        }

        public async Task<IReadOnlyList<ReservaDataModel>> ObtenerHistorialPorClienteAsync(int id_cliente, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.ReservaQueryRepository.ObtenerHistorialPorClienteAsync(id_cliente, cancellationToken);

            return entities
                .Select(ReservaDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<IReadOnlyList<ReservaDataModel>> ObtenerReservasActivasAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.ReservaQueryRepository.ObtenerReservasActivasAsync(cancellationToken);

            return entities
                .Select(ReservaDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<IReadOnlyList<ReservaDataModel>> ObtenerReservasPorVehiculoAsync(int id_vehiculo, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.ReservaQueryRepository.ObtenerReservasPorVehiculoAsync(id_vehiculo, cancellationToken);

            return entities
                .Select(ReservaDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<IReadOnlyList<ReservaExtraDataModel>> ObtenerExtrasPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.ReservaExtraRepository.ObtenerPorReservaAsync(id_reserva, cancellationToken);

            return entities
                .Select(ReservaExtraDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<IReadOnlyList<ReservaConductorDataModel>> ObtenerConductoresPorReservaAsync(int id_reserva, CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.ReservaConductorRepository.ObtenerPorReservaAsync(id_reserva, cancellationToken);

            return entities
                .Select(ReservaConductorDataMapper.ToDataModel)
                .ToList();
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<ReservaDataModel> CrearAsync(
            ReservaDataModel model,
            IReadOnlyList<ReservaExtraDataModel>? extras = null,
            IReadOnlyList<ReservaConductorDataModel>? conductores = null,
            CancellationToken cancellationToken = default)
        {
            var entity = ReservaDataMapper.ToEntity(model);

            await _unitOfWork.ReservaRepository.AgregarAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (extras is not null)
            {
                foreach (var extra in extras)
                {
                    var extraEntity = ReservaExtraDataMapper.ToEntity(extra);
                    extraEntity.id_reserva = entity.id_reserva;

                    await _unitOfWork.ReservaExtraRepository.AgregarAsync(extraEntity, cancellationToken);
                }
            }

            if (conductores is not null)
            {
                foreach (var conductor in conductores)
                {
                    var conductorEntity = ReservaConductorDataMapper.ToEntity(conductor);
                    conductorEntity.id_reserva = entity.id_reserva;

                    await _unitOfWork.ReservaConductorRepository.AgregarAsync(conductorEntity, cancellationToken);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ReservaDataMapper.ToDataModel(entity);
        }

        public async Task<ReservaDataModel?> ActualizarAsync(
            ReservaDataModel model,
            IReadOnlyList<ReservaExtraDataModel>? extras = null,
            IReadOnlyList<ReservaConductorDataModel>? conductores = null,
            CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ReservaRepository.ObtenerParaActualizarAsync(model.id_reserva, cancellationToken);

            if (entity is null)
                return null;

            // =========================
            // DATOS PRINCIPALES
            // =========================
            entity.codigo_reserva = model.codigo_reserva;

            entity.id_cliente = model.id_cliente;
            entity.id_vehiculo = model.id_vehiculo;
            entity.id_localizacion_recogida = model.id_localizacion_recogida;
            entity.id_localizacion_devolucion = model.id_localizacion_devolucion;

            entity.fecha_reserva_utc = model.fecha_reserva_utc;
            entity.fecha_inicio = model.fecha_inicio;
            entity.fecha_fin = model.fecha_fin;

            entity.fecha_recogida = model.fecha_recogida;
            entity.hora_recogida = model.hora_recogida;
            entity.fecha_devolucion = model.fecha_devolucion;
            entity.hora_devolucion = model.hora_devolucion;
            entity.fecha_hora_recogida = model.fecha_hora_recogida;
            entity.fecha_hora_devolucion = model.fecha_hora_devolucion;

            entity.cantidad_dias_reserva = model.cantidad_dias_reserva;

            entity.subtotal_reserva = model.subtotal_reserva;
            entity.valor_iva = model.valor_iva;
            entity.total_reserva = model.total_reserva;

            entity.observaciones_reserva = model.observaciones_reserva;
            entity.origen_canal_reserva = model.origen_canal_reserva;

            entity.estado_reserva = model.estado_reserva;
            entity.fecha_confirmacion_utc = model.fecha_confirmacion_utc;
            entity.fecha_cancelacion_utc = model.fecha_cancelacion_utc;
            entity.motivo_cancelacion = model.motivo_cancelacion;

            entity.es_eliminado = model.es_eliminado;
            entity.fecha_inhabilitacion_utc = model.fecha_inhabilitacion_utc;
            entity.motivo_inhabilitacion = model.motivo_inhabilitacion;

            entity.modificado_por_usuario = model.modificado_por_usuario;
            entity.fecha_modificacion_utc = model.fecha_modificacion_utc;
            entity.modificacion_ip = model.modificacion_ip;

            entity.servicio_origen = model.servicio_origen;

            // EF Core detecta los cambios automáticamente — no se llama a Actualizar()
            // porque la entidad ya está siendo tracked por el contexto.

            // =========================
            // SINCRONIZAR EXTRAS
            // =========================
            if (extras is not null)
            {
                // Incluye eliminados para poder reactivarlos sin violar UQ_RES_X_XTRAS_RESERVA_EXTRA
                var existentes = await _unitOfWork.ReservaExtraRepository
                    .ObtenerTodosPorReservaAsync(model.id_reserva, cancellationToken);

                // Marcar como inactivos los extras que ya no vienen en el request
                foreach (var existente in existentes.Where(x => !x.es_eliminado))
                {
                    var recibido = extras.FirstOrDefault(x => x.id_extra == existente.id_extra);

                    if (recibido is null)
                    {
                        existente.estado_reserva_extra = "INA";
                        existente.es_eliminado = true;
                        existente.fecha_inhabilitacion_utc = DateTime.UtcNow;
                        existente.fecha_modificacion_utc = DateTime.UtcNow;
                        existente.modificado_por_usuario = model.modificado_por_usuario;
                        existente.motivo_inhabilitacion = "Extra removido de la reserva.";

                        _unitOfWork.ReservaExtraRepository.Actualizar(existente);
                        continue;
                    }

                    existente.cantidad = recibido.cantidad;
                    existente.valor_unitario_extra = recibido.valor_unitario_extra;
                    existente.subtotal_extra = recibido.subtotal_extra;
                    existente.estado_reserva_extra = recibido.estado_reserva_extra;
                    existente.es_eliminado = recibido.es_eliminado;
                    existente.fecha_inhabilitacion_utc = recibido.fecha_inhabilitacion_utc;
                    existente.motivo_inhabilitacion = recibido.motivo_inhabilitacion;
                    existente.modificado_por_usuario = model.modificado_por_usuario;
                    existente.fecha_modificacion_utc = recibido.fecha_modificacion_utc;
                    existente.modificado_desde_ip = recibido.modificado_desde_ip;
                    existente.origen_registro = recibido.origen_registro;

                    _unitOfWork.ReservaExtraRepository.Actualizar(existente);
                }

                // Insertar nuevos o reactivar eliminados
                foreach (var extraNuevo in extras)
                {
                    var existente = existentes.FirstOrDefault(x => x.id_extra == extraNuevo.id_extra);

                    // Ya fue procesado arriba como activo
                    if (existente is not null && !existente.es_eliminado)
                        continue;

                    // Reactivar extra eliminado en lugar de insertar duplicado
                    if (existente is not null && existente.es_eliminado)
                    {
                        existente.cantidad = extraNuevo.cantidad;
                        existente.valor_unitario_extra = extraNuevo.valor_unitario_extra;
                        existente.subtotal_extra = extraNuevo.subtotal_extra;
                        existente.estado_reserva_extra = extraNuevo.estado_reserva_extra;
                        existente.es_eliminado = false;
                        existente.fecha_inhabilitacion_utc = null;
                        existente.motivo_inhabilitacion = null;
                        existente.modificado_por_usuario = model.modificado_por_usuario;
                        existente.fecha_modificacion_utc = DateTime.UtcNow;
                        existente.modificado_desde_ip = extraNuevo.modificado_desde_ip;
                        existente.origen_registro = extraNuevo.origen_registro;

                        _unitOfWork.ReservaExtraRepository.Actualizar(existente);
                        continue;
                    }

                    // Extra completamente nuevo — insertar
                    var extraEntity = ReservaExtraDataMapper.ToEntity(extraNuevo);
                    extraEntity.id_reserva = model.id_reserva;

                    await _unitOfWork.ReservaExtraRepository.AgregarAsync(extraEntity, cancellationToken);
                }
            }

            // =========================
            // SINCRONIZAR CONDUCTORES
            // =========================
            if (conductores is not null)
            {
                // Incluye eliminados para poder reactivarlos sin violar UQ_RES_X_CON_RESERVA_CONDUCTOR
                var existentes = await _unitOfWork.ReservaConductorRepository
                    .ObtenerTodosPorReservaAsync(model.id_reserva, cancellationToken);

                // Marcar como inactivos los conductores que ya no vienen en el request
                foreach (var existente in existentes.Where(x => !x.es_eliminado))
                {
                    var recibido = conductores.FirstOrDefault(x => x.id_conductor == existente.id_conductor);

                    if (recibido is null)
                    {
                        existente.estado_reserva_conductor = "INA";
                        existente.es_eliminado = true;
                        existente.fecha_inhabilitacion_utc = DateTime.UtcNow;
                        existente.fecha_modificacion_utc = DateTime.UtcNow;
                        existente.modificado_por_usuario = model.modificado_por_usuario;
                        existente.motivo_inhabilitacion = "Conductor removido de la reserva.";

                        _unitOfWork.ReservaConductorRepository.Actualizar(existente);
                        continue;
                    }

                    existente.tipo_conductor = recibido.tipo_conductor;
                    existente.es_principal = recibido.es_principal;
                    existente.fecha_asignacion_utc = recibido.fecha_asignacion_utc;
                    existente.estado_reserva_conductor = recibido.estado_reserva_conductor;
                    existente.es_eliminado = recibido.es_eliminado;
                    existente.fecha_inhabilitacion_utc = recibido.fecha_inhabilitacion_utc;
                    existente.motivo_inhabilitacion = recibido.motivo_inhabilitacion;
                    existente.modificado_por_usuario = model.modificado_por_usuario;
                    existente.fecha_modificacion_utc = recibido.fecha_modificacion_utc;
                    existente.modificado_desde_ip = recibido.modificado_desde_ip;
                    existente.origen_registro = recibido.origen_registro;

                    _unitOfWork.ReservaConductorRepository.Actualizar(existente);
                }

                // Insertar nuevos o reactivar eliminados
                foreach (var conductorNuevo in conductores)
                {
                    var existente = existentes.FirstOrDefault(x => x.id_conductor == conductorNuevo.id_conductor);

                    // Ya fue procesado arriba como activo
                    if (existente is not null && !existente.es_eliminado)
                        continue;

                    // Reactivar conductor eliminado en lugar de insertar duplicado
                    if (existente is not null && existente.es_eliminado)
                    {
                        existente.tipo_conductor = conductorNuevo.tipo_conductor;
                        existente.es_principal = conductorNuevo.es_principal;
                        existente.fecha_asignacion_utc = conductorNuevo.fecha_asignacion_utc;
                        existente.estado_reserva_conductor = conductorNuevo.estado_reserva_conductor;
                        existente.es_eliminado = false;
                        existente.fecha_inhabilitacion_utc = null;
                        existente.motivo_inhabilitacion = null;
                        existente.modificado_por_usuario = model.modificado_por_usuario;
                        existente.fecha_modificacion_utc = DateTime.UtcNow;
                        existente.modificado_desde_ip = conductorNuevo.modificado_desde_ip;
                        existente.origen_registro = conductorNuevo.origen_registro;

                        _unitOfWork.ReservaConductorRepository.Actualizar(existente);
                        continue;
                    }

                    // Conductor completamente nuevo — insertar
                    var conductorEntity = ReservaConductorDataMapper.ToEntity(conductorNuevo);
                    conductorEntity.id_reserva = model.id_reserva;

                    await _unitOfWork.ReservaConductorRepository.AgregarAsync(conductorEntity, cancellationToken);
                }
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ReservaDataMapper.ToDataModel(entity);
        }

        public async Task<bool> EliminarLogicoAsync(
            int id_reserva,
            string usuario,
            string? motivo,
            CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.ReservaRepository.ObtenerParaActualizarAsync(id_reserva, cancellationToken);

            if (entity is null)
                return false;

            // CORRECCIÓN: se usa estado CAN en lugar de INA porque el constraint
            // CHK_RESERVAS_ESTADO solo permite: PEN, CON, CAN, EXP, FIN, EMI.
            // Los constraints CHK_RESERVAS_CANCELACION_MOTIVO_COHERENTE y
            // CHK_RESERVAS_CANCELACION_FECHA_COHERENTE exigen que cuando el estado
            // es CAN, motivo_cancelacion y fecha_cancelacion_utc no sean nulos.
            entity.estado_reserva = "CAN";
            entity.motivo_cancelacion = motivo;
            entity.fecha_cancelacion_utc = DateTime.UtcNow;
            entity.es_eliminado = true;
            entity.fecha_inhabilitacion_utc = DateTime.UtcNow;
            entity.fecha_modificacion_utc = DateTime.UtcNow;
            entity.modificado_por_usuario = usuario;
            entity.motivo_inhabilitacion = motivo;

            // EF Core detecta los cambios automáticamente — no se llama a Actualizar()

            var extras = await _unitOfWork.ReservaExtraRepository.ObtenerPorReservaAsync(id_reserva, cancellationToken);
            foreach (var extra in extras)
            {
                extra.estado_reserva_extra = "INA";
                extra.es_eliminado = true;
                extra.fecha_inhabilitacion_utc = DateTime.UtcNow;
                extra.fecha_modificacion_utc = DateTime.UtcNow;
                extra.modificado_por_usuario = usuario;
                extra.motivo_inhabilitacion = motivo;

                _unitOfWork.ReservaExtraRepository.Actualizar(extra);
            }

            var conductores = await _unitOfWork.ReservaConductorRepository.ObtenerPorReservaAsync(id_reserva, cancellationToken);
            foreach (var conductor in conductores)
            {
                conductor.estado_reserva_conductor = "INA";
                conductor.es_eliminado = true;
                conductor.fecha_inhabilitacion_utc = DateTime.UtcNow;
                conductor.fecha_modificacion_utc = DateTime.UtcNow;
                conductor.modificado_por_usuario = usuario;
                conductor.motivo_inhabilitacion = motivo;

                _unitOfWork.ReservaConductorRepository.Actualizar(conductor);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task AprobarConductoresYExtrasAsync(
            int id_reserva,
            string modificado_por_usuario,
            CancellationToken cancellationToken = default)
        {
            var conductores = await _unitOfWork.ReservaConductorRepository
                .ObtenerPorReservaAsync(id_reserva, cancellationToken);

            foreach (var conductor in conductores)
            {
                conductor.estado_reserva_conductor = "APR";
                conductor.modificado_por_usuario = modificado_por_usuario;
                conductor.fecha_modificacion_utc = DateTime.UtcNow;

                _unitOfWork.ReservaConductorRepository.Actualizar(conductor);
            }

            var extras = await _unitOfWork.ReservaExtraRepository
                .ObtenerPorReservaAsync(id_reserva, cancellationToken);

            foreach (var extra in extras)
            {
                extra.estado_reserva_extra = "APR";
                extra.modificado_por_usuario = modificado_por_usuario;
                extra.fecha_modificacion_utc = DateTime.UtcNow;

                _unitOfWork.ReservaExtraRepository.Actualizar(extra);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        // =========================
        // VALIDACIONES
        // =========================
        public async Task<bool> ExistePorCodigoAsync(string codigo_reserva, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ReservaRepository.ExistePorCodigoAsync(codigo_reserva, cancellationToken);
        }
    }
}