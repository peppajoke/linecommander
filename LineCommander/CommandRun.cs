using System.Collections.Generic;

namespace LineCommander
{
    public class CommandRun
    {
        public ICommand Command;
        public IEnumerable<string> Arguments;
    }
}