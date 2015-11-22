using TORNEOS.Data.Repositories;
using TORNEOS.Data.Infrastructure;
using TORNEOS.Model.Commands;
using FNHMVC.CommandProcessor.Command;
using TORNEOS.Model;
using AutoMapper;

namespace TORNEOS.Domain.Handlers
{
    class CreateOrUpdateTorneoHandler
    {
        private readonly IMappingEngine mapper;
        private readonly ITorneoRepository torneoRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateOrUpdateTorneoHandler(IMappingEngine mapper, ITorneoRepository torneoRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.torneoRepository = torneoRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(CreateOrUpdateTorneoCommand command)
        {
            var torneo = this.mapper.Map<Torneo>(command);

            if (torneo.TorneoId == 0)
                torneoRepository.Add(torneo);
            else
                torneoRepository.Update(torneo);

            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
