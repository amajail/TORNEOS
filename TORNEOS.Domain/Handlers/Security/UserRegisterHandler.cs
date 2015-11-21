using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FNHMVC.CommandProcessor.Command;
using TORNEOS.Model.Commands;
using TORNEOS.Data.Repositories;
using TORNEOS.Data.Infrastructure;
using TORNEOS.Model;
using AutoMapper;

namespace TORNEOS.Domain.Handlers.Security
{
    public class UserRegisterHandler : ICommandHandler<UserRegisterCommand>
    {
        private readonly IMappingEngine mapper;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserRegisterHandler(IMappingEngine mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public ICommandResult Execute(UserRegisterCommand command)
        {
            var user = this.mapper.Map<User>(command);
            if (!string.IsNullOrEmpty(command.Password)) user.Password = command.Password;

            userRepository.Add(user);
            unitOfWork.Commit();
            return new CommandResult(true);
        }
    }
}
