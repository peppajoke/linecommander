using System;
using System.Collections.Generic;

namespace LineCommander
{
    public interface IConsole
    {
        string ReadLine();
        ConsoleKeyInfo ReadKey();

        void WriteLine(string message);
        void Write(string message);

    }
    public class SystemConsole : IConsole
    {
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class FakeConsole : IConsole
    {
        public Queue<string> Commands;
        public string ConsoleOutput;
        public void StageCommands(IEnumerable<string> commands)
        {
            Commands = new Queue<string>();
            foreach (var command in commands) 
            {
                Commands.Enqueue(command);
            }
        }
        public ConsoleKeyInfo ReadKey()
        {
            throw new NotImplementedException();
        }

        public string ReadLine()
        {
            return Commands.Dequeue();
        }

        public void Write(string message)
        {
            ConsoleOutput += message;
        }

        public void WriteLine(string message)
        {
            ConsoleOutput += "" + Environment.NewLine + message;
        }
    }
}