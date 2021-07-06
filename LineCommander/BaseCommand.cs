using System;
using System.Collections.Generic;
using System.Linq;

namespace LineCommander
{
    public abstract class BaseCommand : ICommand
    {
        protected IConsole _console;

        public abstract string Description();
        public abstract bool Execute(IEnumerable<string> arguments);
        public abstract IEnumerable<string> MatchingBaseCommands();

        public void SetConsole(IConsole console)
        {
            _console = console;
        }

        public int InputInt(string message, int min, int max)
        {
            _console.WriteLine(message);
            int response;
            Int32.TryParse(_console.ReadLine(), out response);
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
            _console.Write(message);
            _console.WriteLine(( required ? " (required) " : " ") + "max length: " + maxLength);
            var response = _console.ReadLine();
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

        public string InputText(string message, IEnumerable<string> acceptableValues)
        {
            _console.WriteLine(message);
            _console.Write(" (options: " + String.Join(',' , acceptableValues));
            var response = _console.ReadLine();
            if (!acceptableValues.Contains(response))
            {
                TryAgain();
                return InputText(message, acceptableValues);
            }
            return response;
        } 

        public bool InputBool(string message)
        {
            _console.WriteLine(message + " y/n");
            var response = _console.ReadLine().ToUpper();
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
            _console.WriteLine(message);
        }

        private void TryAgain()
        {
            _console.WriteLine("I didn't understand that. Try again?");
        }
    }
}