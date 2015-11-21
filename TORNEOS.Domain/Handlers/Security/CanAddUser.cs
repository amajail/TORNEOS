using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TORNEOS.Model.Commands;
using FNHMVC.CommandProcessor.Command;
using TORNEOS.Data.Infrastructure;
using TORNEOS.Data.Repositories;
using FNHMVC.Core.Common;
using TORNEOS.Model;

namespace TORNEOS.Domain.Handlers
{
    public class CanAddUser : IValidationHandler<UserRegisterCommand>
    {
        private readonly IUserRepository userRepository;

        public CanAddUser(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<ValidationResult> Validate(UserRegisterCommand command)
        {
            User isUserExists = null;
            isUserExists = userRepository.Get(c => c.Email == command.Email);

            if (isUserExists != null)
            {
                yield return new ValidationResult("EMail", Resources.UserExists);
            }
        }
    }
}
