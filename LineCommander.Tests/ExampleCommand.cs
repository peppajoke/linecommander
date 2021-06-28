using System.Collections.Generic;

namespace LineCommander.Tests
{
    internal class ExampleCommand : ICommand
    {
        public string Description()
        {
            return "just an example command";
        }

        public bool Execute(IEnumerable<string> arguments)
        {
            return true;
        }

        public IEnumerable<string> MatchingBaseCommands()
        {
            return new List<string>() {"example"};
        }
    }
}