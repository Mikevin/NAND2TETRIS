using System;
using System.Text.RegularExpressions;

namespace VMTranslator
{
    internal class LineParser
    {
        private static Regex _regex = new Regex("(?<type>[add|sub|neg|eq|gt|lt|and|or|not|push|pop]+)\\s?(?<arg1>[\\d|static|this|local|argument|that|constant|pointer|temp]+)?\\s?(?<arg2>[\\d]+)?.*", RegexOptions.Compiled);

        private static string _type = string.Empty;
        private static string _arg1 = string.Empty;
        private static int _arg2;


        public static Command GetCommand(string line)
        {
            _type = string.Empty;
            _arg1 = string.Empty;
            _arg2 = -1;

            var match = GetMatch(line);
            LoadElements(match);
            return CreateCommand();
        }

        private static Command CreateCommand()
        {
            switch (_type)
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
                case "push":
                case "pop":
                    break;
            }

            throw new NotSupportedException("Type: " + _type + " is not supported.");
        }

        private static void LoadElements(Match match)
        {
            var typeGroup = match.Groups["type"];
            var arg1Group = match.Groups["arg1"];
            var arg2Group = match.Groups["arg2"];

            if (typeGroup.Success)
            {
                _type = typeGroup.Value;
            }

            if (arg1Group.Success)
            {
                _arg1 = arg1Group.Value;
            }

            if (arg2Group.Success)
            {
                _arg2 = Convert.ToInt32(arg2Group.Value);
            }
        }

        private static Match GetMatch(string line)
        {
            var matches = _regex.Matches(line);
            if (matches.Count != 1)
            {
                return null;
            }

            var match = matches[0];
            return match;
        }
    }
}