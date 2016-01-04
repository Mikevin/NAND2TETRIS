using System;
using System.Text.RegularExpressions;
using VMTranslator.Types;
using VMTranslator.Types.Commands;

namespace VMTranslator
{
    internal class LineParser
    {
        private static Regex regex = new Regex("(?<type>[add|sub|neg|eq|gt|lt|and|or|not|push|pop]+)\\s?(?<arg1>[\\d|static|this|local|argument|that|constant|pointer|temp]+)?\\s?(?<arg2>[\\d]+)?.*", RegexOptions.Compiled);

        private static string type = string.Empty;
        private static string arg1 = string.Empty;
        private static int arg2;


        public static Command GetCommand(string line)
        {
            type = string.Empty;
            arg1 = string.Empty;
            arg2 = -1;

            var match = GetMatch(line);
            LoadElements(match);
            return CreateCommand();
        }

        private static Command CreateCommand()
        {
            switch (type)
            {
                case "add":
                case "sub":
                case "neg":
                case "eq":
                case "gt":
                case "lt":
                case "and":
                case "or":
                case "not":
                    return new ArithmeticCommand(arg1, arg2);
                case "push":
                case "pop":
                    return new PushPopCommand(arg1, arg2);
            }

            throw new NotSupportedException("Type: " + type + " is not supported.");
        }

        private static Command.commandType ParseCommandType(string s)
        {
            switch (s)
            {
                case "add":
                case "sub":
                case "neg":
                case "eq":
                case "gt":
                case "lt":
                case "and":
                case "or":
                case "not":
                    return Command.commandType.C_ARITHMETIC;
                case "push":
                    return Command.commandType.C_PUSH;
                case "pop":
                    return Command.commandType.C_POP;
            }
            return Command.commandType.INVALID;
        }

        private static void LoadElements(Match match)
        {
            var typeGroup = match.Groups["type"];
            var arg1Group = match.Groups["arg1"];
            var arg2Group = match.Groups["arg2"];

            if (typeGroup.Success)
            {
                type = typeGroup.Value;
            }

            if (arg1Group.Success)
            {
                arg1 = arg1Group.Value;
            }

            if (arg2Group.Success)
            {
                arg2 = Convert.ToInt32(arg2Group.Value);
            }
        }

        private static Match GetMatch(string line)
        {
            var matches = regex.Matches(line);
            if (matches.Count != 1)
            {
                return null;
            }

            var match = matches[0];
            return match;
        }
    }
}