using System.Collections.Generic;

namespace LineCommander.Tests
{
    public class ExampleExitCommand : BaseCommand
    {
        public override string Description()
        {
            return "just an example command that exits the commander when complete";
        }

        public override bool Execute(IEnumerable<string> arguments)
        {
            return false;
        }

        public override IEnumerable<string> MatchingBaseCommands()
        {
            return new List<string>() { "exit" };
        }
    }
}