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
                    outputLinesList.Add(WritePushPop(command));
                }
            }

            return outputLinesList;
        }

        private string WritePushPop(Command command)
        {
            string result;

            switch (command.Type)
            {
                case Command.commandType.C_PUSH:
                    result = "push";
                    break;
                case Command.commandType.C_POP:
                    result = "pop";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("This command is not of type PUSH or POP.");
            }

            return result + " " + command.arg2;
        }

        private IEnumerable<string> WriteArithmetic(Command command)
        {
            var translator = new ArithmeticTranslator(command);
            return translator.Translate();
        }
    }
}