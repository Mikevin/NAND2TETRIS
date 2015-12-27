using System;
using System.Collections.Generic;

namespace VMTranslator
{
    internal class CodeWriter
    {
        private readonly List<Command> _commandsList;

        public CodeWriter(List<Command> commandsList)
        {
            _commandsList = commandsList;
        }

        public List<string> Write()
        {
            List<string> outputLinesList = new List<string>();

            foreach (Command command in _commandsList)
            {
                if (command.Type == Command.commandType.C_ARITHMETIC)
                {
                    outputLinesList.AddRange(WriteArithmetic(command));
                }
                else if (command.Type == Command.commandType.C_PUSH ||
                         command.Type == Command.commandType.C_POP)
                {
                    outputLinesList.AddRange(WritePushPop(command));
                }
            }

            return outputLinesList;
        }

        private IEnumerable<string> WritePushPop(Command command)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> WriteArithmetic(Command command)
        {
            throw new NotImplementedException();
        }
    }
}