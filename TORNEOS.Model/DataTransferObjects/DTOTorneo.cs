using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FNHMVC.Core.Common;

namespace TORNEOS.Model
{

    [DataContract(IsReference = true)]
    public class DTOTorneo
    {
                [DataMember]
                public string Nombre{get;set;}
    }
}
