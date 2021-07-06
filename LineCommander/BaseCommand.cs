using System.Collections.Generic;

namespace LineCommander
{
    public abstract class BaseCommand : ICommand
    {
        protected ITerminal _console;
        public BaseCommand(ITerminal console = null)
        {
            _console = console ?? new CommandTerminal();
        }

        public abstract string Description();
        public abstract bool Execute(IEnumerable<string> arguments);
        public abstract IEnumerable<string> MatchingBaseCommands();
    }
}