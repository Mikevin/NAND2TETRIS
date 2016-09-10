﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace VMTranslator
{
    class Parser
    {

        private static Regex _regex = new Regex("(?<type>[add|sub|neg|eq|gt|lt|and|or|not|push|pop]+)\\s?(?<arg1>[\\d|static|this|local|argument|that|constant|pointer|temp]+)?\\s?(?<arg2>[\\d]+)?.*", RegexOptions.Compiled);

        private static readonly List<string> ArithmeticBooleanCommands = new List<string>
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
            _streamReader = new StreamReader(fileStream);
        }

        private readonly StreamReader _streamReader;

        public bool HasMoreCommands => !_streamReader.EndOfStream;

        public void Advance()
        {
            ResetValues();

            if (!HasMoreCommands)
            {
                return;
            }

            var line = ReadLine();
            ParseLine(line);
        }

        private void ResetValues()
        {
            CurrentCommandType = CommandType.Invalid;
            Arg1 = string.Empty;
            Arg2 = 0;
        }

        private void ParseLine(string line)
        {
            line = line.Trim();
            var parts = line.Split(' ');
            if (parts.Length > 0)
            {
                SetCommandType(parts[0]);
                //set Arg1 to command string in case of Arithmetic command; as per specification
                //Needs to happen here because normally Arg1 is only handled when the line has multiple parts
                if (CurrentCommandType == CommandType.Arithmetic)
                {
                    Arg1 = parts[0];
                }
            }
            if (parts.Length > 1)
            {
                Arg1 = parts[1];
            }
            if (parts.Length > 2)
            {
                SetArg2(parts[2]);
            }
        }

        private void SetArg2(string arg)
        {
            short arg2 = Int16.Parse(arg);
            switch (CurrentCommandType)
            {
                case CommandType.Push:
                case CommandType.Pop:
                case CommandType.Function:
                case CommandType.Call:
                    Arg2 = arg2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetCommandType(string command)
        {
            if (ArithmeticBooleanCommands.Contains(command))
            {
                CurrentCommandType = CommandType.Arithmetic;
                return;
            }

            switch (command)
            {
                case "push":
                    CurrentCommandType = CommandType.Push;
                    break;
                case "pop":
                    CurrentCommandType = CommandType.Pop;
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
