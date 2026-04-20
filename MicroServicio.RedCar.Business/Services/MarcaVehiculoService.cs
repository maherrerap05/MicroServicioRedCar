using MicroServicio.RedCar.Business.DTOs.MarcaVehiculo;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.Business.Mappers;
using MicroServicio.RedCar.Business.Validators;
using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Services
{
    public class MarcaVehiculoService : IMarcaVehiculoService
    {
        private readonly IMarcaVehiculoDataService _dataService;

        public MarcaVehiculoService(IMarcaVehiculoDataService dataService)
        {
            _dataService = dataService;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<MarcaVehiculoResponse?> ObtenerPorIdAsync(int id)
        {
            var model = await _dataService.ObtenerPorIdAsync(id);
            return model is null ? null : MarcaVehiculoBusinessMapper.ToResponse(model);
        }

        public async Task<MarcaVehiculoResponse?> ObtenerPorGuidAsync(Guid guid)
        {
            var model = await _dataService.ObtenerPorGuidAsync(guid);
            return model is null ? null : MarcaVehiculoBusinessMapper.ToResponse(model);
        }

        public async Task<MarcaVehiculoResponse?> ObtenerPorCodigoAsync(string codigo)
        {
            var model = await _dataService.ObtenerPorCodigoAsync(codigo);
            return model is null ? null : MarcaVehiculoBusinessMapper.ToResponse(model);
        }

        public async Task<IReadOnlyList<MarcaVehiculoResponse>> ObtenerTodosAsync()
        {
            var list = await _dataService.ObtenerTodosAsync();
            return list.Select(MarcaVehiculoBusinessMapper.ToResponse).ToList();
        }

        public async Task<DataPagedResult<MarcaVehiculoResponse>> BuscarAsync(MarcaVehiculoFiltroRequest filtro)
        {
            var errors = MarcaVehiculoValidator.ValidarFiltro(filtro);
            if (errors.Any())
                throw new ArgumentException(string.Join(" | ", errors));

            var data = await _dataService.ObtenerTodosAsync();

            var query = data.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.codigo_marca_vehiculo))
                query = query.Where(x => x.codigo_marca_vehiculo.Contains(filtro.codigo_marca_vehiculo));

            if (!string.IsNullOrWhiteSpace(filtro.nombre_marca_vehiculo))
                query = query.Where(x => x.nombre_marca_vehiculo.Contains(filtro.nombre_marca_vehiculo));

            if (!string.IsNullOrWhiteSpace(filtro.estado_marca_vehiculo))
                query = query.Where(x => x.estado_marca_vehiculo == filtro.estado_marca_vehiculo);

            var total = query.Count();

            var items = query
                .OrderBy(x => x.id_marca_vehiculo)
                .Skip((filtro.page_number - 1) * filtro.page_size)
                .Take(filtro.page_size)
                .ToList();

            return new DataPagedResult<MarcaVehiculoResponse>
            {
                Items = items.Select(MarcaVehiculoBusinessMapper.ToResponse).ToList(),
                PageNumber = filtro.page_number,
                PageSize = filtro.page_size,
                TotalRecords = total
            };
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<MarcaVehiculoResponse> CrearAsync(CrearMarcaVehiculoRequest request)
        {
            var errors = MarcaVehiculoValidator.ValidarCreacion(request);
            if (errors.Any())
                throw new ArgumentException(string.Join(" | ", errors));

            if (await _dataService.ExistePorCodigoAsync(request.codigo_marca_vehiculo))
                throw new InvalidOperationException("Ya existe una marca con ese código.");

            if (await _dataService.ExistePorNombreAsync(request.nombre_marca_vehiculo))
                throw new InvalidOperationException("Ya existe una marca con ese nombre.");

            var model = MarcaVehiculoBusinessMapper.ToDataModel(request);

            var created = await _dataService.CrearAsync(model);

            return MarcaVehiculoBusinessMapper.ToResponse(created);
        }

        public async Task<MarcaVehiculoResponse?> ActualizarAsync(ActualizarMarcaVehiculoRequest request)
        {
            var errors = MarcaVehiculoValidator.ValidarActualizacion(request);
            if (errors.Any())
                throw new ArgumentException(string.Join(" | ", errors));

            var model = MarcaVehiculoBusinessMapper.ToDataModel(request);

            var updated = await _dataService.ActualizarAsync(model);

            return updated is null ? null : MarcaVehiculoBusinessMapper.ToResponse(updated);
        }

        public async Task<bool> EliminarAsync(int id, string usuario, string? motivo)
        {
            return await _dataService.EliminarLogicoAsync(id, usuario, motivo);
        }
    }
}