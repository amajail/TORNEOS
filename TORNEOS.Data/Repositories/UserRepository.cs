using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TORNEOS.Data.Infrastructure;
using TORNEOS.Model;
using NHibernate;

namespace TORNEOS.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface IUserRepository : IRepository<User>
    {
    }
}
