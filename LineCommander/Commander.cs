using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineCommander
{
    public interface ICommander
    {
        Task<IEnumerable<ICommand>> AddCommands(IEnumerable<ICommand> commands);
        Task<IEnumerable<CommandRun>> ListenForCommands();
    }
    public class Commander : ICommander
    {
        private IConsole _console;
        public Commander(IConsole console = null)
        {
            _console = console ?? new ConsoleProxy();
        }
        private Dictionary<string, ICommand> _commands;

        private List<CommandRun> _commandLog = new List<CommandRun>();
        public async Task<IEnumerable<ICommand>> AddCommands(IEnumerable<ICommand> commands)
        {
            _commands = new Dictionary<string, ICommand>();
            foreach (var command in commands)
            {
                foreach (var word in command.MatchingBaseCommands())
                {
                    _commands.Add(word.ToUpper(), command);
                }
                // todo, TRY CATCH THIS to catch overwrites
                
            }
            return _commands.Values;
        }

        public async Task<IEnumerable<CommandRun>> ListenForCommands()
        {
            var keepListening = true;
            while(keepListening)
            {
                var commandText = _console.ReadLine();
                var arguments = commandText.Split(' ').ToList();
                if (!arguments.Any())
                {
                    // Exit early if there is no command
                    continue;
                }
                var baseCommand = arguments[0];
                arguments.RemoveAt(0);

                keepListening = await ExecuteCommand(baseCommand, arguments);

            }
            return _commandLog;
        }

        private async Task<bool> ExecuteCommand(string commandName, List<string> arguments)
        {
            commandName = commandName.ToUpper();
            if (commandName == "H" || commandName == "HELP")
            {
                _console.WriteLine("halp");
                return true;
            }

            if (commandName == "QUIT" || commandName == "Q")
            {
                return false;
            }

            if (_commands.ContainsKey(commandName))
            {
                var command = _commands[commandName];
                var commandRun = new CommandRun() { Command = command, Arguments = arguments };
                _commandLog.Add(commandRun);
                return command.Execute(arguments);
            }
            return true;
        }
    }
}