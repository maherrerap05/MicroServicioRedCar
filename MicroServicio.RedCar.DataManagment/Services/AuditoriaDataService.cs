using MicroServicio.RedCar.DataManagement.Interfaces;
using MicroServicio.RedCar.DataManagement.Mappers;
using MicroServicio.RedCar.DataManagement.Models;

namespace MicroServicio.RedCar.DataManagement.Services
{
    public class AuditoriaDataService : IAuditoriaDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditoriaDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // =========================
        // CONSULTAS
        // =========================
        public async Task<IReadOnlyList<AuditoriaDataModel>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _unitOfWork.AuditoriaRepository.ObtenerTodosAsync(cancellationToken);

            return entities
                .Select(AuditoriaDataMapper.ToDataModel)
                .ToList();
        }

        public async Task<AuditoriaDataModel?> ObtenerPorIdAsync(long id_auditoria, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.AuditoriaRepository.ObtenerPorIdAsync(id_auditoria, cancellationToken);

            return entity is null
                ? null
                : AuditoriaDataMapper.ToDataModel(entity);
        }

        public async Task<AuditoriaDataModel?> ObtenerPorGuidAsync(Guid auditoria_guid, CancellationToken cancellationToken = default)
        {
            var entity = await _unitOfWork.AuditoriaRepository.ObtenerPorGuidAsync(auditoria_guid, cancellationToken);

            return entity is null
                ? null
                : AuditoriaDataMapper.ToDataModel(entity);
        }

        // =========================
        // COMANDOS
        // =========================
        public async Task<AuditoriaDataModel> RegistrarAsync(AuditoriaDataModel model, CancellationToken cancellationToken = default)
        {
            var entity = AuditoriaDataMapper.ToEntity(model);

            await _unitOfWork.AuditoriaRepository.AgregarAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return AuditoriaDataMapper.ToDataModel(entity);
        }
    }
}