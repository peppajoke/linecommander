using System.Collections.Generic;

namespace LineCommander
{
    public interface ITerminal
    {
        int InputInt(string message, int min, int max);
        string InputText(string message, bool required = true, int maxLength = 25);
        string InputText(string message, IEnumerable<string> acceptableValues);
        bool InputBool(string message);

        void WriteLine(string message);
    }
}