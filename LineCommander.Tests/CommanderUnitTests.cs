using NUnit.Framework;
using LineCommander;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineCommander.Tests
{
    public class CommanderUnitTests
    {
        private Mock<ICommand> _mockSimpleCommand;
        private Mock<IConsole> _mockConsole;

        private Commander _commander;

        [SetUp]
        public void Setup()
        {
            _mockSimpleCommand = new Mock<ICommand>();
            _mockConsole = new Mock<IConsole>();
            _commander = new Commander(_mockConsole.Object);
        }

        [Test]
        public async Task TestQuit()
        {
            _mockConsole.Setup(p => p.ReadLine()).Returns("quit");
            //_mockSimpleCommand.Setup(p => p.Execute(new List<string>())).Returns(true);
            
            var cmds = await _commander.AddCommands(new List<ICommand>() { _mockSimpleCommand.Object });
            
            Assert.AreEqual(1, cmds.Count);

            var commandLog = await _commander.ListenForCommands();
            _mockSimpleCommand.Verify(s => s.Execute(It.IsAny<IEnumerable<string>>()), Times.Never());
            Assert.AreEqual(false, commandLog.Any());
        }

        //[Test]
        //public async Task TestMockCommandExecute()
        //{
         //   _mockConsole.Setup(p => p.ReadLine()).Returns(_mockSimpleCommand.GetType().Name);
           // _mockSimpleCommand.Setup(p => p.Execute(new List<string>())).Returns(false);
            
           // var cmds = await _commander.AddCommands(new List<ICommand>() { _mockSimpleCommand.Object });
            
            //Assert.AreEqual(1, cmds.Count);

            //var commandLog = await _commander.ListenForCommands();
            //_mockSimpleCommand.Verify(s => s.Execute(It.IsAny<IEnumerable<string>>()), Times.Never());
            //Assert.AreEqual(1, commandLog.Count());
        //}
    }
}