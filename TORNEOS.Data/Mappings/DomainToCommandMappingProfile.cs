using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TORNEOS.Model;
using TORNEOS.Model.Commands;

namespace TORNEOS.Data
{
    public class DomainToCommandMappingProfile : Profile
    {
        //public override string ProfileName
        //{
        //    get { return "DomainToCommandMappingProfile"; }
        //}

        protected override void Configure()
        {
            Mapper.CreateMap<Torneo, CreateOrUpdateTorneoCommand>();
            Mapper.CreateMap<Torneo, DeleteTorneoCommand>();
            Mapper.CreateMap<User, UserRegisterCommand>().ForMember(x => x.Password, o => o.Ignore());
        }
    }
}
