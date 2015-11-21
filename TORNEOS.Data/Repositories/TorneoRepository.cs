using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FNHMVC.Data.Infrastructure;
using TORNEOS.Model;
using NHibernate;

namespace TORNEOS.Data.Repositories
{
    public class TorneoRepository : RepositoryBase<Torneo>, ITorneoRepository
    {
        public TorneoRepository(ISession session)
            : base(session)
        {
        }
    }

    public interface ITorneoRepository : IRepository<Torneo>
    {
    }
}
