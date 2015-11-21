using System;
using FNHMVC.CommandProcessor.Command;
using TORNEOS.Model;
using System.Collections.Generic;

namespace TORNEOS.Model.Commands
{
    public class CreateOrUpdateTorneoCommand : ICommand
    {
        public int TorneoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
