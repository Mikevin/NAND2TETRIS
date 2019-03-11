using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HackAssembler
{
    public class Assembler
    {
        private readonly List<string> _lines;
        private SymbolTable _symbolTable;
        private List<Command> _commands;

        public Assembler(List<string> lines)
        {
            _lines = lines;
        }

        public List<string> Assemble()
        {
            _symbolTable = new SymbolTable();
            _commands = Parser.Parse(_lines);
            FillSymbolTable();
            return Translate();
        }

        private void FillSymbolTable()
        {
            for (var i = 0; i < _commands.Count; i++)
            {
                if (_commands[i].CommandType != Command.CommandTypeEnum.LCommand)
                {
                    continue;
                }
                SymbolCommand symbolCommand = _commands[i] as SymbolCommand;
                if (!_symbolTable.Contains(symbolCommand.Symbol))
                {
                    _symbolTable.AddLabel(symbolCommand.Symbol, i);
                }
            }
        }

        private List<string> Translate()
        {
            var output = new List<string>();

            foreach (var command in _commands)
            {
                string binaryTranslation = null;
                switch (command.CommandType)
                {
                    case Command.CommandTypeEnum.ACommand:
                        binaryTranslation = TranslateAddress((SymbolCommand)command);
                        break;
                    case Command.CommandTypeEnum.CCommand:
                        binaryTranslation = TranslateCodeCommand((CodeCommand)command);
                        break;
                    case Command.CommandTypeEnum.LCommand:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                if (!string.IsNullOrEmpty(binaryTranslation))
                {
                    output.Add(binaryTranslation);
                }
            }

            return output;
        }

        private string TranslateAddress(SymbolCommand command)
        {
            if (int.TryParse(command.Symbol, out var constantAddress))
            {
                return 0 + Convert.ToString(constantAddress, 2).PadLeft(15, '0');
            }

            if (!_symbolTable.Contains(command.Symbol))
            {
                _symbolTable.AddVariable(command.Symbol);
            }
            var address = _symbolTable.GetAddress(command.Symbol);
            return 0 + Convert.ToString(address, 2).PadLeft(15, '0');
        }

        private string TranslateCodeCommand(CodeCommand command)
        {
            var binary = "111";
            binary += TranslateInstruction.Comp(command.Comp);
            binary += TranslateInstruction.Dest(command.Dest);
            binary += TranslateInstruction.Jump(command.Jump);

            return binary;
        }
    }
}