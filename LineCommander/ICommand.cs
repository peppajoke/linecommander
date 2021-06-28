using System.Collections.Generic;

namespace LineCommander
{
    public interface ICommand
    {
        bool Execute(IEnumerable<string> arguments);
        string Description();
    }
}