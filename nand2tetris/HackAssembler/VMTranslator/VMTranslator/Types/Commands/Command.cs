using System.Collections.Generic;

namespace VMTranslator
{
    public partial class Command
    {
        private static List<string> _arithmeticBooleanCommands = new List<string> { "add", "sub", "neg", "eq", "gt", "lt", "and", "or", "not" };
        private static List<string> _pushPopCommands = new List<string> { "push", "pop" };

        public string CommandString { get; }
        public CommandType CommandType { get; private set; }

        public Command(string command, string arg1, int arg2)
        {
            CommandString = command;
            CommandType = LookupCommand(command);
            this.Arg1 = arg1;
            this.Arg2 = arg2;
        }

        public string Arg1 { get; }

        public int Arg2 { get; }

        private CommandType LookupCommand(string command)
        {
            if (_arithmeticBooleanCommands.Contains(command.Trim()))
            {
                return CommandType.CArithmetic;
            }
            if (command == "push")
            {
                return CommandType.CPush;
            }
            if (command == "pop")
            {
                return CommandType.CPop;
            }

            return CommandType.Invalid;
        }
    }
}