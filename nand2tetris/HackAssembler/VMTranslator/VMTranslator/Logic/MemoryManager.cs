using System.Collections.Generic;
using System.Linq;

namespace VMTranslator.Logic
{
    class MemoryManager
    {
        private List<Command> commandsList;

        public MemoryManager(List<Command> commandsList)
        {
            this.commandsList = commandsList;
        }

        public List<Command> Parse()
        {
            var qualifiedCommands =
                commandsList.Where(c => c.Type == Command.commandType.C_PUSH || c.Type == Command.commandType.C_POP);
            foreach (Command qualifiedCommand in qualifiedCommands)
            {

            }
        }
    }
}
