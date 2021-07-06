using System.Collections.Generic;

namespace LineCommander.Tests
{
    internal class ExampleCommand : BaseCommand
    {
        public override string Description()
        {
            return "just an example command";
        }

        public override bool Execute(IEnumerable<string> arguments)
        {
            return true;
        }

        public override IEnumerable<string> MatchingBaseCommands()
        {
            return new List<string>() {"example"};
        }
    }
}