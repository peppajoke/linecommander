using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LineCommander
{
    public interface IConsole
    {
        Task<string> ReadLine();
        Task<ConsoleKeyInfo> ReadKey();

        Task WriteLine(string message);
        Task Write(string message);

    }
    public class SystemConsole : IConsole
    {
        public async Task<ConsoleKeyInfo> ReadKey()
        {
            return Console.ReadKey();
        }

        public async Task<string> ReadLine()
        {
            return Console.ReadLine();
        }

        public async Task Write(string message)
        {
            Console.Write(message);
        }

        public async Task WriteLine(string message)
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
        public Task<ConsoleKeyInfo> ReadKey()
        {
            throw new NotImplementedException();
        }

        public async Task<string> ReadLine()
        {
            return Commands.Dequeue();
        }

        public async Task Write(string message)
        {
            ConsoleOutput += message;
        }

        public async Task WriteLine(string message)
        {
            ConsoleOutput += "" + Environment.NewLine + message;
        }
    }
}