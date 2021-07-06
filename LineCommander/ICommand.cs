using System.Collections.Generic;
using System.Threading.Tasks;

namespace LineCommander
{
    public interface ICommand
    {
        Task<bool> Execute(IEnumerable<string> arguments);
        string Description();

        IEnumerable<string> MatchingBaseCommands();
    }
}