using System;

namespace LineCommander
{
    public interface IConsole
    {
        string ReadLine();
        ConsoleKeyInfo ReadKey();

        void WriteLine(string message);
        void Write(string message);

    }
    public class ConsoleProxy : IConsole
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
}