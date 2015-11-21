using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using TORNEOS.Model;
using FluentNHibernate.Mapping;

namespace TORNEOS.Data.Mappings
{
    public class TorneoMap : ClassMap<Torneo>
    {
        public TorneoMap()
        {
            Id(x => x.TorneoId).GeneratedBy.Identity();
            Map(x => x.Nombre).Length(100).Not.Nullable();
        }
    }
}
