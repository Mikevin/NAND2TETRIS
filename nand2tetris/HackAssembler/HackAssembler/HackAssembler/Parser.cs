using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        private string[] lines;
        private int currentLine = -1;

        private Regex regex = new Regex("(?<dest>[\\w^]?)=?(?<comp>[\\w\\d\\+\\-]+);?(?<jump>[\\w]+)?", RegexOptions.Compiled);

        public Parser(string filePath)
        {
            FilePath=filePath;
            Initialize();
        }

        private void Initialize()
        {
            lines = File.ReadAllLines(FilePath);

            if (lines.Length < 1)
            {
                return;
            }

            Advance();
        }

        public bool hasMoreCommands { get; private set; } = false;

        public string FilePath { get; private set; }

        public CommandType commandType { get; private set; }

        public string symbol { get; private set; }

        public string dest { get; private set; }

        public string comp { get; private set; }

        public string jump { get; private set; }

        public void Advance()
        {
            dest = string.Empty;
            comp = string.Empty;
            jump = string.Empty;
            symbol = string.Empty;

            currentLine++;
            string line = lines[currentLine].Trim();

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

            if (line.StartsWith("@") ||
                line.StartsWith("("))
            {
                ParseSymbol(line);
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

        private void ParseSymbol(string line)
        {
            string symbolvalue = line.TrimStart('@');
            int number;
            if (int.TryParse(symbolvalue, out number))
            {
                commandType = CommandType.A_COMMAND;
            }
            else
            {
                commandType = CommandType.L_COMMAND;
            }
            symbol = symbolvalue;
        }
    }
}
