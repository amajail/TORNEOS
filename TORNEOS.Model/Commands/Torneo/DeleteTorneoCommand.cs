using FNHMVC.CommandProcessor.Command;

namespace TORNEOS.Model.Commands
{
    public class DeleteTorneoCommand : ICommand
    {
        public int TorneoId { get; set; }
    }
}
