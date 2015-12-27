using System.IO;
using System.Text.RegularExpressions;

namespace HackAssembler
{
    public class Parser
    {
        public enum CommandType
        {
            A_COMMAND,
            C_COMMAND,
            L_COMMAND
        }

        private int currentLine = -1;

        private string[] lines;

        private readonly Regex regex = new Regex("(?<dest>[\\w^]?)=?(?<comp>[\\w\\d\\+\\-]+);?(?<jump>[\\w]+)?",
            RegexOptions.Compiled);

        public Parser(string filePath)
        {
            FilePath = filePath;
            Initialize();
        }

        public bool hasMoreCommands { get; private set; }

        public string FilePath { get; }

        public CommandType commandType { get; private set; }

        public string symbol { get; private set; }

        public string dest { get; private set; }

        public string comp { get; private set; }

        public string jump { get; private set; }

        private void Initialize()
        {
            lines = File.ReadAllLines(FilePath);

            if (lines.Length < 1)
            {
                return;
            }

            Advance();
        }

        public void Advance()
        {
            dest = string.Empty;
            comp = string.Empty;
            jump = string.Empty;
            symbol = string.Empty;

            currentLine++;
            var line = lines[currentLine].Trim();

            if (line.StartsWith("//") ||
                line.Length == 0)
            {
                if (hasMoreCommands)
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
                commandType = CommandType.C_COMMAND;
                ParseCommand(line);
            }


            if (lines.Length > currentLine + 1)
            {
                hasMoreCommands = true;
                return;
            }

            hasMoreCommands = false;
        }

        private string CleanLine(string line)
        {
            return line.Split('/')[0];
        }

        private void ParseCommand(string line)
        {
            var matches = regex.Matches(line);
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
                dest = destGroup.Value;
            }

            if (compGroup.Success)
            {
                comp = compGroup.Value;
            }

            if (jumpGroup.Success)
            {
                jump = jumpGroup.Value;
            }
        }

        private void ParseASymbol(string line)
        {
            var symbolvalue = line.TrimStart('@');
            commandType = CommandType.A_COMMAND;
            symbol = symbolvalue;
        }

        private void ParseLSymbol(string line)
        {
            var symbolvalue = line.Trim('(').Trim(')');
            commandType = CommandType.L_COMMAND;
            symbol = symbolvalue;
        }
    }
}