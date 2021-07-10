# linecommander
A framework for simple command line applications in C#

# Getting started

Step 1: Write 1 or more command classes, for parsing user commands. 

Each command class should extend **LineCommander.BaseCommand**

Example:

    class RemoveFromFileSystemCommand : BaseCommand
    {
        public override string Description()
        {
            return "Deletes a file.";
        }

        public override bool Execute(IEnumerable<string> arguments)
        {
            var fileName = arguments[0];

            if (await _fileSystemManager.Delete(fileName))
            {
                _console.WriteLine(fileName + " deleted.");
            }
            else
            {
                _console.WriteLine("Delete operation failed.");
            }

            return true; // true to keep the command listeninger running, false to kill the process
        }

        public override IEnumerable<string> MatchingBaseCommands()
        {
            return new List<string>() {"rm"}; // "rm" invokes the command
        }
    }
  
  Step 2: Set up a commander to listen for commands.
  
  Example:
  
    var commands = new List<ICommand>() { new RemoveFromFileSystemCommand() };
    var commander = new Commander();
    await commander.AddCommands(commands);
    await commander.ListenForCommands();
  
  Console users can now invoke "rm (file name)" to delete files.
  
  
