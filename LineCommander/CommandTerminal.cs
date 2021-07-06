using System;
using System.Collections.Generic;
using System.Linq;

namespace LineCommander
{
    public class CommandTerminal : ITerminal
    {
        public int InputInt(string message, int min, int max)
        {
            Console.WriteLine(message);
            int response;
            Int32.TryParse(Console.ReadLine(), out response);
            if (response < min)
            {
                TryAgain();
                return InputInt(message, min, max);
            }
            if (response > max)
            {
                TryAgain();
                return InputInt(message, min, max);
            }
            return response;
        }

        public string InputText(string message, bool required = true, int maxLength = 25)
        {
            Console.Write(message);
            Console.WriteLine(( required ? " (required) " : " ") + "max length: " + maxLength);
            var response = Console.ReadLine();
            if (required && String.IsNullOrEmpty(response))
            {
                TryAgain();
                return InputText(message, required, maxLength);
            }

            if (maxLength < response.Length)
            {
                TryAgain();
                return InputText(message, required, maxLength);
            }
            return response;
        }

        public string InputText(string message, IEnumerable<string> whitelist)
        {
            Console.WriteLine(message);
            Console.Write(" (options: " + String.Join(',' ,whitelist) );
            var response = Console.ReadLine();
            if (!whitelist.Contains(response))
            {
                TryAgain();
                return InputText(message, whitelist);
            }
            return response;
        } 

        public bool InputBool(string message)
        {
            Console.WriteLine(message + " y/n");
            var response = Console.ReadLine().ToUpper();
            if (response == "Y" || response == "YES")
            {
                return true;
            }
            if (response == "N" || response == "NO")
            {
                return false;
            }
            TryAgain();
            return InputBool(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        private void TryAgain()
        {
            Console.WriteLine("I didn't understand that. Try again?");
        }
    }
}