using NUnit.Framework;
using LineCommander;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace LineCommander.Tests
{
    public class MockUnitTests
    {
        private Mock<BaseCommand> _mockSimpleCommand;
        private Mock<IConsole> _mockConsole;
        private Commander _commander;

        [SetUp]
        public void Setup()
        {
            _mockSimpleCommand = new Mock<BaseCommand>();
            _mockConsole = new Mock<IConsole>();
            _commander = new Commander(_mockConsole.Object);
        }

        [Test]
        public async Task TestHelpQuit()
        {
            _mockConsole.Setup(p => p.ReadLine()).Returns("help");
            _mockConsole.Setup(p => p.ReadLine()).Returns("quit");
            _mockSimpleCommand.Setup(p => p.Execute(new List<string>())).Returns(true);
            
            var cmds = await _commander.AddCommands(new List<BaseCommand>() { _mockSimpleCommand.Object });
            
            Assert.AreEqual(0, cmds.Count());

            var commandLog = await _commander.ListenForCommands();
            Assert.AreEqual(false, commandLog.Any());
        }

        [Test]
        public async Task TestMockCommandExecute()
        {
            _mockConsole.Setup(p => p.ReadLine()).Returns("test");
            _mockSimpleCommand.Setup(p => p.Execute(new List<string>())).Returns(false);
            _mockSimpleCommand.Setup(p => p.MatchingBaseCommands()).Returns(new List<string>() {"test"});
            
            var cmds = await _commander.AddCommands(new List<BaseCommand>() { _mockSimpleCommand.Object });
            //Assert.AreEqual("asdf", cmds.First().Key);
            Assert.AreEqual(1, cmds.Count());

            var commandLog = await _commander.ListenForCommands();
            Assert.AreEqual(1, commandLog.Count());
        }
    }
}