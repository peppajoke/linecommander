using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LineCommander.Tests
{
    public class ExampleTests
    {
            private ExampleCommand _command;
            private ExampleExitCommand _exitCommand;
            private FakeConsole _console;
            private Commander _commander;

        [SetUp]
        public void Setup()
        {
            _console = new FakeConsole();
            _command = new ExampleCommand();
            _exitCommand = new ExampleExitCommand();
            _commander = new Commander(_console);
        }

        [Test]
        public async Task TestExampleCommand()
        {
            var cmds = await _commander.AddCommands(new List<ICommand>() { _command, _exitCommand });
            _console.StageCommands(new List<string>() { "nothing", "none", "example", "ExaMpLe", "EXAMPLE", "quit"});
            var cmdLog = await _commander.ListenForCommands();
            Assert.AreEqual(3, cmdLog.Count());
        }
        
        [Test]
        public async Task TestNoCommands()
        {
            var cmds = await _commander.AddCommands(new List<ICommand>() { _command, _exitCommand });
            _console.StageCommands(new List<string>() { "nothing", "none", "one", "two", "three", "quit"});
            var cmdLog = await _commander.ListenForCommands();
            Assert.AreEqual(0, cmdLog.Count());
        }

        [Test]
        public async Task TestExitCommand()
        {
            var cmds = await _commander.AddCommands(new List<ICommand>() { _command, _exitCommand });
            
            _console.StageCommands(new List<string>() { "nothing", "none", "one", "exit", "three", "exit"});
            var cmdLog = await _commander.ListenForCommands();
            Assert.AreEqual(1, cmdLog.Count());
        }
    }
}