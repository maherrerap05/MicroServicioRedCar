using MicroServicio.RedCar.Business.DTOs.Cliente;
using MicroServicio.RedCar.Business.Exceptions;
using MicroServicio.RedCar.Business.Interfaces;
using MicroServicio.RedCar.Business.Mappers;
using MicroServicio.RedCar.Business.Validators;
using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.Business.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteDataService _clienteDataService;

        public ClienteService(IClienteDataService clienteDataService)
        {
            _clienteDataService = clienteDataService;
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<ClienteResponse> CrearAsync(CrearClienteRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ClienteValidator.ValidarCreacion(request);

            if (errors.Any())
                throw new ValidationException("La solicitud de creación de cliente es inválida.", errors);

            var existentePorIdentificacion = await _clienteDataService
                .ObtenerPorIdentificacionAsync(request.numero_identificacion, cancellationToken);

            if (existentePorIdentificacion is not null)
                throw new ValidationException("Ya existe un cliente con el número de identificación indicado.");

            var existentePorCorreo = await _clienteDataService
                .ObtenerPorCorreoAsync(request.correo, cancellationToken);

            if (existentePorCorreo is not null)
                throw new ValidationException("Ya existe un cliente con el correo indicado.");

            var dataModel = ClienteBusinessMapper.ToDataModel(request);

            var creado = await _clienteDataService.CrearAsync(dataModel, cancellationToken);

            return ClienteBusinessMapper.ToResponse(creado);
        }

        public async Task<ClienteResponse> ActualizarAsync(ActualizarClienteRequest request, CancellationToken cancellationToken = default)
        {
            var errors = ClienteValidator.ValidarActualizacion(request);

            if (errors.Any())
                throw new ValidationException("La solicitud de actualización de cliente es inválida.", errors);

            var existente = await _clienteDataService.ObtenerPorIdAsync(request.id_cliente, cancellationToken);

            if (existente is null)
                throw new NotFoundException("No se encontró el cliente solicitado.");

            var porIdentificacion = await _clienteDataService
                .ObtenerPorIdentificacionAsync(request.numero_identificacion, cancellationToken);

            if (porIdentificacion is not null && porIdentificacion.id_cliente != request.id_cliente)
                throw new ValidationException("Ya existe otro cliente con el número de identificación indicado.");

            var porCorreo = await _clienteDataService
                .ObtenerPorCorreoAsync(request.correo, cancellationToken);

            if (porCorreo is not null && porCorreo.id_cliente != request.id_cliente)
                throw new ValidationException("Ya existe otro cliente con el correo indicado.");

            var dataModel = ClienteBusinessMapper.ToDataModel(request);

            // =========================
            // PRESERVAR DATOS ORIGINALES
            // =========================
            dataModel.cliente_guid = existente.cliente_guid;
            dataModel.fecha_registro_utc = existente.fecha_registro_utc;
            dataModel.creado_por_usuario = existente.creado_por_usuario;
            dataModel.row_version = existente.row_version;

            // =========================
            // PRESERVAR / CONTROLAR ESTADO INTERNO
            // =========================
            dataModel.es_eliminado = existente.es_eliminado;

            // Si el cliente ya estaba eliminado, mantenemos su fecha de inhabilitación
            // salvo que el mapper ya la gestione explícitamente.
            if (existente.es_eliminado)
            {
                dataModel.fecha_inhabilitacion_utc = existente.fecha_inhabilitacion_utc;
            }

            var actualizado = await _clienteDataService.ActualizarAsync(dataModel, cancellationToken);

            if (actualizado is null)
                throw new NotFoundException("No se pudo actualizar el cliente porque no existe.");

            return ClienteBusinessMapper.ToResponse(actualizado);
        }

        public async Task EliminarLogicoAsync(int id_cliente, string usuario, string? motivo, CancellationToken cancellationToken = default)
        {
            if (id_cliente <= 0)
                throw new ValidationException("El id_cliente es inválido.");

            if (string.IsNullOrWhiteSpace(usuario))
                throw new ValidationException("El usuario es obligatorio para la eliminación lógica.");

            var eliminado = await _clienteDataService.EliminarLogicoAsync(id_cliente, usuario, motivo, cancellationToken);

            if (!eliminado)
                throw new NotFoundException("No se encontró el cliente para eliminación lógica.");
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<ClienteResponse> ObtenerPorIdAsync(int id_cliente, CancellationToken cancellationToken = default)
        {
            if (id_cliente <= 0)
                throw new ValidationException("El id_cliente es inválido.");

            var cliente = await _clienteDataService.ObtenerPorIdAsync(id_cliente, cancellationToken);

            if (cliente is null)
                throw new NotFoundException("No se encontró el cliente solicitado.");

            return ClienteBusinessMapper.ToResponse(cliente);
        }

        public async Task<ClienteResponse> ObtenerPorGuidAsync(Guid cliente_guid, CancellationToken cancellationToken = default)
        {
            if (cliente_guid == Guid.Empty)
                throw new ValidationException("El cliente_guid es obligatorio.");

            var cliente = await _clienteDataService.ObtenerPorGuidAsync(cliente_guid, cancellationToken);

            if (cliente is null)
                throw new NotFoundException("No se encontró el cliente solicitado.");

            return ClienteBusinessMapper.ToResponse(cliente);
        }

        public async Task<ClienteResponse> ObtenerPorIdentificacionAsync(string numero_identificacion, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(numero_identificacion))
                throw new ValidationException("El número de identificación es obligatorio.");

            var cliente = await _clienteDataService.ObtenerPorIdentificacionAsync(numero_identificacion, cancellationToken);

            if (cliente is null)
                throw new NotFoundException("No se encontró el cliente con la identificación indicada.");

            return ClienteBusinessMapper.ToResponse(cliente);
        }

        public async Task<ClienteResponse> ObtenerPorCorreoAsync(string correo, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new ValidationException("El correo es obligatorio.");

            var cliente = await _clienteDataService.ObtenerPorCorreoAsync(correo, cancellationToken);

            if (cliente is null)
                throw new NotFoundException("No se encontró el cliente con el correo indicado.");

            return ClienteBusinessMapper.ToResponse(cliente);
        }

        public async Task<IReadOnlyList<ClienteResponse>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var clientes = await _clienteDataService.ObtenerTodosAsync(cancellationToken);

            return clientes
                .Select(ClienteBusinessMapper.ToResponse)
                .ToList();
        }

        public async Task<DataPagedResult<ClienteResponse>> BuscarAsync(ClienteFiltroRequest request, CancellationToken cancellationToken = default)
        {
            if (request is null)
                throw new ValidationException("La solicitud de búsqueda no puede ser nula.");

            if (request.page_number <= 0)
                throw new ValidationException("El page_number debe ser mayor a cero.");

            if (request.page_size <= 0)
                throw new ValidationException("El page_size debe ser mayor a cero.");

            var filtro = new ClienteFiltroDataModel
            {
                tipo_identificacion = request.tipo_identificacion,
                numero_identificacion = request.numero_identificacion,
                razon_social = request.razon_social,
                nombres = request.nombres,
                apellidos = request.apellidos,
                correo = request.correo,
                telefono = request.telefono,
                estado = request.estado,
                PageNumber = request.page_number,
                PageSize = request.page_size
            };

            var result = await _clienteDataService.BuscarAsync(filtro, cancellationToken);

            return new DataPagedResult<ClienteResponse>
            {
                Items = result.Items
                    .Select(ClienteBusinessMapper.ToResponse)
                    .ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalRecords
            };
        }
    }
}