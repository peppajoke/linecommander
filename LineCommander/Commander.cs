using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineCommander
{
    public interface ICommander
    {
        Task<IEnumerable<BaseCommand>> AddCommands(IEnumerable<BaseCommand> commands);
        Task<IEnumerable<CommandRun>> ListenForCommands();
    }
    public class Commander : ICommander
    {
        private IConsole _console;
        public Commander(IConsole console = null)
        {
            _console = console ?? new SystemConsole();
        }
        private Dictionary<string, BaseCommand> _commands;

        private List<CommandRun> _commandLog = new List<CommandRun>();
        public async Task<IEnumerable<BaseCommand>> AddCommands(IEnumerable<BaseCommand> commands)
        {
            _commands = new Dictionary<string, BaseCommand>();
            foreach (var command in commands)
            {
                command.SetConsole(_console);
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
                keepListening = await ExecuteCommand(GetCommandInput(commandText));
            }
            return _commandLog;
        }

        public async void SendCommandInput(string input)
        {
            await ExecuteCommand(GetCommandInput(input));
        }

        private CommandInput GetCommandInput(string input)
        {
            var arguments = input.Split(' ').ToList();
            if (!arguments.Any())
            {
                // Exit early if there is no command
                // continue;
            }
            var baseCommand = arguments[0];
            arguments.RemoveAt(0);
            return new CommandInput() { Command = baseCommand, Arguments = arguments };
        }

        private async Task<bool> ExecuteCommand(CommandInput input)
        {
            var commandName = input.Command.ToUpper();
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
                var commandRun = new CommandRun() { Command = command, Arguments = input.Arguments };
                _commandLog.Add(commandRun);
                return command.Execute(input.Arguments);
            }
            return true;
        }

        private struct CommandInput
        {
            public string Command;
            public IEnumerable<string> Arguments;
        }
    }
}