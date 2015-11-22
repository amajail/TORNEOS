using FNHMVC.CommandProcessor.Command;
using TORNEOS.Model.Commands;
using TORNEOS.Data.Repositories;
using TORNEOS.Data.Infrastructure;

namespace TORNEOS.Domain.Handlers
{
    public class DeleteTorneoHandler : ICommandHandler<DeleteTorneoCommand>
    {
        private readonly ITorneoRepository torneoRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteTorneoHandler(ITorneoRepository torneoRepository, IUnitOfWork unitOfWork)
        {
            this.torneoRepository = torneoRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(DeleteTorneoCommand command)
        {
            var torneo = torneoRepository.GetById(command.TorneoId);
            torneoRepository.Delete(torneo);
            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
