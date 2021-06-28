using System.Collections.Generic;

namespace LineCommander.Tests
{
    public class ExampleExitCommand : ICommand
    {
        public string Description()
        {
            return "just an example command that exits the commander when complete";
        }

        public bool Execute(IEnumerable<string> arguments)
        {
            return false;
        }

        public IEnumerable<string> MatchingBaseCommands()
        {
            return new List<string>() { "exit" };
        }
    }
}