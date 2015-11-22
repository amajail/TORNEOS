using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TORNEOS.Model;

namespace TORNEOS.Data.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        //public override string ProfileName
        //{
        //    get { return "DomainToDTOMappingProfile"; }
        //}

        protected override void Configure()
        {
            Mapper.CreateMap<Torneo, DTOTorneo>();
            Mapper.CreateMap<User, DTOUser>();
        }
    }
}
