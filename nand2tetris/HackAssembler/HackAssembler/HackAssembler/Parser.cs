using System.IO;
using System.Text.RegularExpressions;

namespace HackAssembler
{
    public class Parser
    {
        public enum CommandTypeEnum
        {
            ACommand,
            CCommand,
            LCommand
        }

        private int _currentLine = -1;

        private string[] _lines;

        private readonly Regex _regex = new Regex("(?<dest>[\\w^]+)?=?(?<comp>[\\w\\d\\+\\-\\!\\|\\&]+);?(?<jump>[\\w]+)?",
            RegexOptions.Compiled);

        public Parser(string filePath)
        {
            FilePath = filePath;
            Initialize();
        }

        public bool HasMoreCommands { get; private set; }

        public string FilePath { get; }

        public CommandTypeEnum CommandType { get; private set; }

        public string Symbol { get; private set; }

        public string Dest { get; private set; }

        public string Comp { get; private set; }

        public string Jump { get; private set; }

        private void Initialize()
        {
            _lines = File.ReadAllLines(FilePath);

            if (_lines.Length < 1)
            {
                return;
            }

            Advance();
        }

        public void Advance()
        {
            Dest = string.Empty;
            Comp = string.Empty;
            Jump = string.Empty;
            Symbol = string.Empty;

            _currentLine++;
            var line = _lines[_currentLine].Trim();

            if (line.StartsWith("//") ||
                line.Length == 0)
            {
                if (HasMoreCommands)
                {
                    Advance();
                    return;
                }
            }

            line = CleanLine(line);

            if (line.StartsWith("@"))
            {
                ParseASymbol(line);
            }
            else if (line.StartsWith("("))
            {
                ParseLSymbol(line);
            }
            else
            {
                CommandType = CommandTypeEnum.CCommand;
                ParseCommand(line);
            }


            if (_lines.Length > _currentLine + 1)
            {
                HasMoreCommands = true;
                return;
            }

            HasMoreCommands = false;
        }

        private string CleanLine(string line)
        {
            return line.Split('/')[0];
        }

        private void ParseCommand(string line)
        {
            var matches = _regex.Matches(line);
            if (matches.Count != 1)
            {
                return;
            }

            var match = matches[0];
            var destGroup = match.Groups["dest"];
            var compGroup = match.Groups["comp"];
            var jumpGroup = match.Groups["jump"];

            if (destGroup.Success)
            {
                Dest = destGroup.Value;
            }

            if (compGroup.Success)
            {
                Comp = compGroup.Value;
            }

            if (jumpGroup.Success)
            {
                Jump = jumpGroup.Value;
            }
        }

        private void ParseASymbol(string line)
        {
            var symbolvalue = line.TrimStart('@');
            CommandType = CommandTypeEnum.ACommand;
            Symbol = symbolvalue;
        }

        private void ParseLSymbol(string line)
        {
            var symbolvalue = line.Trim('(').Trim(')');
            CommandType = CommandTypeEnum.LCommand;
            Symbol = symbolvalue;
        }
    }
}