using System;
using System.Collections.Generic;

namespace HackAssembler
{
    public class Main
    {
        private Parser _parser;

        public Main(string filePath)
        {
            FilePath = filePath;
            _parser = new Parser(FilePath);
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
            while (_parser.HasMoreCommands)
            {
                _parser.Advance();
                if (_parser.CommandType == Parser.CommandTypeEnum.LCommand)
                {
                    if (!SymbolTable.Contains(_parser.Symbol))
                    {
                        SymbolTable.AddEntry(_parser.Symbol, counter);
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
            _parser = new Parser(FilePath);
            var output = new List<string>();

            while (_parser.HasMoreCommands)
            {
                _parser.Advance();
                string outputBinary = null;
                switch (_parser.CommandType)
                {
                    case Parser.CommandTypeEnum.ACommand:
                        outputBinary = TranslateAddress();
                        break;
                    case Parser.CommandTypeEnum.CCommand:
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

        private string TranslateAddress()
        {
            string outputBinary;
            int value;
            if (int.TryParse(_parser.Symbol, out value))
            {
                outputBinary = 0 + Convert.ToString(value, 2).PadLeft(15, '0');
            }
            else
            {
                if (!SymbolTable.Contains(_parser.Symbol))
                {
                    SymbolTable.AddEntry(_parser.Symbol, SymbolTable.NextAddress);
                }
                var address = SymbolTable.GetAddress(_parser.Symbol);
                outputBinary = 0 + Convert.ToString(address, 2).PadLeft(15, '0');
            }
            return outputBinary;
        }

        private string TranslateCommand()
        {
            var binary = "111";
            binary += Code.Comp(_parser.Comp);
            binary += Code.Dest(_parser.Dest);
            binary += Code.Jump(_parser.Jump);

            return binary;
        }
    }
}