using System;
using System.Collections.Generic;
using System.Linq;

namespace LineCommander
{
    public class CommandTerminal : IConsole
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        private void TryAgain()
        {
            Console.WriteLine("I didn't understand that. Try again?");
        }

        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        public ConsoleKeyInfo ReadKey()
        {
            throw new NotImplementedException();
        }

        public void Write(string message)
        {
            throw new NotImplementedException();
        }
    }
}