using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace HackAssembler
{
    public class CodeCommand : Command
    {
        public string Dest { get; set; }
        public string Comp { get; set; }
        public string Jump { get; set; }

        public override string ToString()
        {
            return $"{Enum.GetName(typeof(CommandType), CommandType)} Dest:{Dest} Comp:{Comp} Jump:{Jump}";
        }
    }

    public class SymbolCommand : Command
    {
        public string Symbol { get; set; }

        public override string ToString()
        {
            return Enum.GetName(typeof(CommandType), CommandType) + Symbol;
        }
    }
    
    public class Command
    {
        public CommandTypeEnum CommandType { get; set; }

        public enum CommandTypeEnum
        {
            ACommand,
            CCommand,
            LCommand
        }
    }

    public static class Parser
    {
        private const char InstructionSymbolA = '@';
        private const char InstructionSymbolL = '(';

        private static readonly Regex _regex = new Regex("(?<dest>[\\w^]+)?=?(?<comp>[\\w\\d\\+\\-\\!\\|\\&]+);?(?<jump>[\\w]+)?",
            RegexOptions.Compiled);


        public static List<Command> Parse(List<string> lines)
        {
            var result = new List<Command>();
            foreach (var rawline in lines)
            {
                var line = CleanLine(rawline);
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                Command command = ParseCommand(line);
                result.Add(command);
            }
            return result;
        }

        private static Command ParseCommand(string line)
        {
            switch (DetermineType(line))
            {
                case Command.CommandTypeEnum.ACommand:
                    return ParseACommand(line);
                case Command.CommandTypeEnum.CCommand:
                    return ParseCCommand(line);
                case Command.CommandTypeEnum.LCommand:
                    return ParseLSymbol(line);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static Command.CommandTypeEnum DetermineType(string line)
        {
            switch (line[0])
            {
                case InstructionSymbolA:
                    return Command.CommandTypeEnum.ACommand;
                case InstructionSymbolL:
                    return Command.CommandTypeEnum.LCommand;
                default:
                    return Command.CommandTypeEnum.CCommand;
            }
        }

        private static string CleanLine(string rawLine)
        {
            return rawLine.Trim().Split('/')[0];
        }
        private static Command ParseCCommand(string line)
        {
            var matches = _regex.Matches(line);
            if (matches.Count != 1)
            {
                return null;
            }

            var command = new CodeCommand(){CommandType = Command.CommandTypeEnum.CCommand};

            var match = matches[0];
            var destGroup = match.Groups["dest"];
            var compGroup = match.Groups["comp"];
            var jumpGroup = match.Groups["jump"];

            if (destGroup.Success)
            {
                command.Dest = destGroup.Value;
            }

            if (compGroup.Success)
            {
                command.Comp = compGroup.Value;
            }

            if (jumpGroup.Success)
            {
                command.Jump = jumpGroup.Value;
            }

            return command;
        }

        private static Command ParseACommand(string line)
        {
            var symbol = line.TrimStart('@');

            return new SymbolCommand(){CommandType = Command.CommandTypeEnum.ACommand, Symbol = symbol};
        }

        private static Command ParseLSymbol(string line)
        {
            var symbol = line.Trim('(').Trim(')');
            return new SymbolCommand(){CommandType = Command.CommandTypeEnum.LCommand, Symbol = symbol};

        }
    }
}