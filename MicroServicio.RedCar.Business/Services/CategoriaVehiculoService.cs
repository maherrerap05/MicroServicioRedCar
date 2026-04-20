using MicroServicio.RedCar.Business.DTOs.CategoriaVehiculo;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.Business.Mappers;
using MicroServicio.RedCar.Business.Validators;
using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MicroServicio.RedCar.Business.Services
{
    public class CategoriaVehiculoService : ICategoriaVehiculoService
    {
        private readonly ICategoriaVehiculoDataService _dataService;

        public CategoriaVehiculoService(ICategoriaVehiculoDataService dataService)
        {
            _dataService = dataService;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<CategoriaVehiculoResponse?> ObtenerPorIdAsync(int id)
        {
            var model = await _dataService.ObtenerPorIdAsync(id);
            return model is null ? null : CategoriaVehiculoBusinessMapper.ToResponse(model);
        }

        public async Task<CategoriaVehiculoResponse?> ObtenerPorGuidAsync(Guid guid)
        {
            var model = await _dataService.ObtenerPorGuidAsync(guid);
            return model is null ? null : CategoriaVehiculoBusinessMapper.ToResponse(model);
        }

        public async Task<CategoriaVehiculoResponse?> ObtenerPorCodigoAsync(string codigo)
        {
            var model = await _dataService.ObtenerPorCodigoAsync(codigo);
            return model is null ? null : CategoriaVehiculoBusinessMapper.ToResponse(model);
        }

        public async Task<IReadOnlyList<CategoriaVehiculoResponse>> ObtenerTodosAsync()
        {
            var list = await _dataService.ObtenerTodosAsync();
            return list.Select(CategoriaVehiculoBusinessMapper.ToResponse).ToList();
        }

        public async Task<DataPagedResult<CategoriaVehiculoResponse>> BuscarAsync(CategoriaVehiculoFiltroRequest filtro)
        {
            var errors = CategoriaVehiculoValidator.ValidarFiltro(filtro);
            if (errors.Any())
                throw new ArgumentException(string.Join(" | ", errors));

            var data = await _dataService.ObtenerTodosAsync();

            var query = data.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.codigo_categoria_vehiculo))
                query = query.Where(x => x.codigo_categoria_vehiculo.Contains(filtro.codigo_categoria_vehiculo));

            if (!string.IsNullOrWhiteSpace(filtro.nombre_categoria_vehiculo))
                query = query.Where(x => x.nombre_categoria_vehiculo.Contains(filtro.nombre_categoria_vehiculo));

            if (!string.IsNullOrWhiteSpace(filtro.estado_categoria_vehiculo))
                query = query.Where(x => x.estado_categoria_vehiculo == filtro.estado_categoria_vehiculo);

            var total = query.Count();

            var items = query
                .OrderBy(x => x.id_categoria_vehiculo)
                .Skip((filtro.page_number - 1) * filtro.page_size)
                .Take(filtro.page_size)
                .ToList();

            return new DataPagedResult<CategoriaVehiculoResponse>
            {
                Items = items.Select(CategoriaVehiculoBusinessMapper.ToResponse).ToList(),
                PageNumber = filtro.page_number,
                PageSize = filtro.page_size,
                TotalRecords = total
            };
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<CategoriaVehiculoResponse> CrearAsync(CrearCategoriaVehiculoRequest request)
        {
            var errors = CategoriaVehiculoValidator.ValidarCreacion(request);
            if (errors.Any())
                throw new ArgumentException(string.Join(" | ", errors));

            if (await _dataService.ExistePorCodigoAsync(request.codigo_categoria_vehiculo))
                throw new InvalidOperationException("Ya existe una categoría con ese código.");

            if (await _dataService.ExistePorNombreAsync(request.nombre_categoria_vehiculo))
                throw new InvalidOperationException("Ya existe una categoría con ese nombre.");

            var model = CategoriaVehiculoBusinessMapper.ToDataModel(request);

            var created = await _dataService.CrearAsync(model);

            return CategoriaVehiculoBusinessMapper.ToResponse(created);
        }

        public async Task<CategoriaVehiculoResponse?> ActualizarAsync(ActualizarCategoriaVehiculoRequest request)
        {
            var errors = CategoriaVehiculoValidator.ValidarActualizacion(request);
            if (errors.Any())
                throw new ArgumentException(string.Join(" | ", errors));

            var model = CategoriaVehiculoBusinessMapper.ToDataModel(request);

            var updated = await _dataService.ActualizarAsync(model);

            return updated is null ? null : CategoriaVehiculoBusinessMapper.ToResponse(updated);
        }

        public async Task<bool> EliminarAsync(int id, string usuario, string? motivo)
        {
            return await _dataService.EliminarLogicoAsync(id, usuario, motivo);
        }
    }
}