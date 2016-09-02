using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace VMTranslator
{
    class Parser
    {

        private static Regex _regex = new Regex("(?<type>[add|sub|neg|eq|gt|lt|and|or|not|push|pop]+)\\s?(?<arg1>[\\d|static|this|local|argument|that|constant|pointer|temp]+)?\\s?(?<arg2>[\\d]+)?.*", RegexOptions.Compiled);

        private static readonly List<string> _arithmeticBooleanCommands = new List<string>
        {
            "add",
            "sub",
            "neg",
            "eq",
            "gt",
            "lt",
            "and",
            "or",
            "not"
        };
        public Parser(FileStream fileStream)
        {
            _fileStream = fileStream;
            _streamReader = new StreamReader(_fileStream);
        }

        private FileStream _fileStream;
        private StreamReader _streamReader;

        public bool HasMoreCommands => !_streamReader.EndOfStream;

        public void Advance()
        {
            if (!HasMoreCommands)
            {
                return;
            }

            var line = ReadLine();
            ParseLine(line);
        }

        private void ParseLine(string line)
        {
            line = line.Trim();
            var parts = line.Split(' ');
            if (parts[0] != null)
            {
                SetCommandType(parts[0]);
            }
            if (parts[1] != null)
            {
                SetArg1(parts[1], parts[0]);
            }
            if (parts[2] != null)
            {
                SetArg2(parts[3]);
            }
        }

        private void SetArg2(string arg)
        {
            short arg2 = Int16.Parse(arg);
            switch (CurrentCommandType)
            {
                case CommandType.CPush:
                case CommandType.CPop:
                case CommandType.CFunction:
                case CommandType.CCall:
                    Arg2 = arg2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetArg1(string arg, string command)
        {
            if (CurrentCommandType == CommandType.CArithmetic)
            {
                Arg1 = command;
                return;
            }

            Arg1 = arg;
        }

        private void SetCommandType(string command)
        {
            if (_arithmeticBooleanCommands.Contains(command))
            {
                CurrentCommandType = CommandType.CArithmetic;
                return;
            }

            switch (command)
            {
                case "push":
                    CurrentCommandType = CommandType.CPush;
                    break;
                case "pop":
                    CurrentCommandType = CommandType.CPop;
                    break;
                default:
                    throw new InvalidDataException($"Unknown command: {command}.");
            }
        }

        private string ReadLine()
        {
            string line = string.Empty;
            while (HasMoreCommands)
            {
                line = _streamReader.ReadLine();
                //skip comments
                if (!string.IsNullOrEmpty(line) && !line.StartsWith("/"))
                {
                    break;
                }
            }

            return line;
        }

        public CommandType CurrentCommandType { get; private set; } = CommandType.Invalid;

        public string Arg1 { get; private set; }

        public int Arg2 { get; private set; }
    }
}
