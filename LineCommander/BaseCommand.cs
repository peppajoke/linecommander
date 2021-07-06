using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineCommander
{
    public abstract class BaseCommand : ICommand
    {
        protected IConsole _console;

        public abstract string Description();
        public abstract Task<bool> Execute(IEnumerable<string> arguments);
        public abstract IEnumerable<string> MatchingBaseCommands();

        public void SetConsole(IConsole console)
        {
            _console = console;
        }

        public async Task<int> InputInt(string message, int min, int max)
        {
            await _console.WriteLine(message);
            int response;
            Int32.TryParse(await _console.ReadLine(), out response);
            if (response < min)
            {
                TryAgain();
                return await InputInt(message, min, max);
            }
            if (response > max)
            {
                TryAgain();
                return await InputInt(message, min, max);
            }
            return response;
        }

        public async Task<string> InputText(string message, bool required = true, int maxLength = 25)
        {
            await _console.Write(message);
            await _console.WriteLine(( required ? " (required) " : " ") + "max length: " + maxLength);
            var response = await _console.ReadLine();
            if (required && String.IsNullOrEmpty(response))
            {
                TryAgain();
                return await InputText(message, required, maxLength);
            }

            if (maxLength < response.Length)
            {
                TryAgain();
                return await InputText(message, required, maxLength);
            }
            return response;
        }

        public async Task<string> InputText(string message, IEnumerable<string> acceptableValues)
        {
            await _console.WriteLine(message);
            await _console.Write(" (options: " + String.Join(',' , acceptableValues));
            var response = await _console.ReadLine();
            if (!acceptableValues.Contains(response))
            {
                TryAgain();
                return await InputText(message, acceptableValues);
            }
            return response;
        } 

        public async Task<bool> InputBool(string message)
        {
            await _console.WriteLine(message + " y/n");
            var response = await _console.ReadLine();
            response = response.ToUpper();
            if (response == "Y" || response == "YES")
            {
                return true;
            }
            if (response == "N" || response == "NO")
            {
                return false;
            }
            TryAgain();
            return await InputBool(message);
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