using System;
using System.Collections.Generic;

namespace HackAssembler
{
    public class Main
    {
        private Parser parser;

        public Main(string filePath)
        {
            FilePath = filePath;
            parser = new Parser(FilePath);
        }

        public string FilePath { get; }

        public List<string> Translate()
        {
            FirstPass();
            return SecondPass();
        }

        private void FirstPass()
        {
            var counter = 0;
            while (parser.hasMoreCommands)
            {
                parser.Advance();
                if (parser.commandType == Parser.CommandType.L_COMMAND)
                {
                    if (!SymbolTable.contains(parser.symbol))
                    {
                        SymbolTable.addEntry(parser.symbol, counter);
                    }
                }
                else
                {
                    counter++;
                }
            }
        }

        private List<string> SecondPass()
        {
            parser = new Parser(FilePath);
            var output = new List<string>();

            while (parser.hasMoreCommands)
            {
                parser.Advance();
                string outputBinary = null;
                switch (parser.commandType)
                {
                    case Parser.CommandType.A_COMMAND:
                        int value;
                        if (int.TryParse(parser.symbol, out value))
                        {
                            outputBinary = 0 + Convert.ToString(value, 2).PadLeft(15, '0');
                        }
                        else
                        {
                            var address = SymbolTable.GetAddress(parser.symbol);
                            outputBinary = 0 + Convert.ToString(address, 2).PadLeft(15, '0');
                        }
                        break;
                    case Parser.CommandType.C_COMMAND:
                        outputBinary = TranslateCommand();
                        break;
                }
                if (!string.IsNullOrEmpty(outputBinary))
                {
                    output.Add(outputBinary);
                }
            }

            return output;
        }

        private string TranslateCommand()
        {
            var binary = "111";
            binary += Code.comp(parser.comp);
            binary += Code.dest(parser.dest);
            binary += Code.jump(parser.jump);

            return binary;
        }
    }
}