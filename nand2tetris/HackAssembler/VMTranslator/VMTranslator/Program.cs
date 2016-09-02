using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VMTranslator.Logic;

namespace VMTranslator
{
    class Program
    {
        const string InputFile = @"E:\Learning\Nand2Tetris\nand2tetris\projects\07\StackArithmetic\SimpleAdd\SimpleAdd.vm";

        public string FileName => Path.GetFileName(InputFile);

        private static List<string> _linesList;
        private static List<string> _outputLinesList;
        private static List<Command> _commandsList;

        static void Main(string[] args)
        {
            PrepareFile();
            PopulateCommandList();
            var memoryManager = new MemoryManager(_commandsList);
            _commandsList = memoryManager.Parse();
            var codeWriter = new CodeWriter(_commandsList);
            _outputLinesList = codeWriter.Write();
            Console.WriteLine(_commandsList.Count);
        }

        private static void PopulateCommandList()
        {
            _commandsList = new List<Command>(_linesList.Count);
            foreach (string line in _linesList)
            {
                var command = LineParser.GetCommand(line);
                if (command.Type != Command.CommandType.Invalid)
                {
                    _commandsList.Add(command);
                }
            }
        }

        private static void PrepareFile()
        {
            List<string> inputLinesList = File.ReadAllLines(InputFile).ToList();
            //initialize here so we can supply capacity for speed
            _linesList = new List<string>(inputLinesList.Count);


            foreach (string line in inputLinesList)
            {
                //skip commented lines or save text before comments
                var parts = line.Split('/');
                if (!string.IsNullOrEmpty(parts[0]))
                {
                    _linesList.Add(parts[0]);
                }
            }
        }
    }
}
