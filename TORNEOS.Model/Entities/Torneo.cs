using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TORNEOS.Model
{
    public class Torneo
    {
        public virtual int TorneoId { get; set; }
        public virtual string Nombre{get;set;}
    }
}
