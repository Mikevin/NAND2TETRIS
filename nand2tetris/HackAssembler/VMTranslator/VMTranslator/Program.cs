using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VMTranslator
{
    class Program
    {
        const string _inputFile = @"";
        private static List<string> linesList;
        private static List<Command> commandsList;
        static void Main(string[] args)
        {
            PrepareFile();
            PopulateCommandList();
        }

        private static void PopulateCommandList()
        {
            foreach (string line in linesList)
            {
                var command = LineParser.GetCommand(line);
            }
        }

        private static void PrepareFile()
        {
            List<string> lineList = File.ReadAllLines(_inputFile).ToList();
            //initialize here so we can supply capacity for speed
            lineList = new List<string>(lineList.Count);


            foreach (string line in lineList)
            {
                //skip commented lines or save text before comments
                var parts = line.Split('/');
                if (!string.IsNullOrEmpty(parts[0]))
                {
                    lineList.Add(parts[0]);
                }
            }
        }
    }

    internal class LineParser
    {
        private static Regex regex = new Regex("(?<type>[add|sub|neg|eq|gt|lt|and|or|not|push|pop]+)\\s?(?<arg1>[\\d|static|this|local|argument|that|constant|pointer|temp]+)?\\s?(?<arg2>[\\d]+)?.*", RegexOptions.Compiled);

        public static Command GetCommand(string line)
        {
            throw new NotImplementedException();
        }
    }

    internal class Command
    {
        public Command(commandType type, string arg1, int arg2)
        {
            Type = type;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }

        internal enum commandType
        {
            C_ARITHMETIC,
            C_PUSH,
            C_POP,
            C_LABEL,
            C_GOTO,
            C_IF,
            C_FUNCTION,
            C_RETURN,
            C_CALL
        }

        public commandType Type { get; private set; }

        public string arg1 { get; private set; }

        public int arg2 { get; private set; }
    }
}
