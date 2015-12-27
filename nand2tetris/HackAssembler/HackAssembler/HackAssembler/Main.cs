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

        public string FilePath { get; private set; }

        public List<string> Translate()
        {
            FirstPass();
            return SecondPass();
        }

        private void FirstPass()
        {
            while (parser.hasMoreCommands)
            {
                parser.Advance();
                if (parser.commandType == Parser.CommandType.L_COMMAND)
                {
                    SymbolTable.addEntry(parser.symbol, SymbolTable.NextAddress);
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
                            var symbol = int.Parse(parser.symbol);
                            outputBinary = 0 + Convert.ToString(symbol, 2).PadLeft(15, '0');
                        break;
                        case Parser.CommandType.L_COMMAND:
                            var address = SymbolTable.GetAddress(parser.symbol);
                            outputBinary = 0 + Convert.ToString(address, 2).PadLeft(15, '0');
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
            string binary = "111";
            binary += Code.comp(parser.comp);
            binary += Code.dest(parser.dest);
            binary += Code.jump(parser.jump);

            return binary;
        }
    }
}