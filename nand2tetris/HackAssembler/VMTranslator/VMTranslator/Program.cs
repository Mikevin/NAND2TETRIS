using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VMTranslator
{
    class Program
    {
        const string _inputFile = @"E:\Learning\Nand2Tetris\nand2tetris\projects\07\StackArithmetic\SimpleAdd\SimpleAdd.vm";

        public string _FileName => Path.GetFileName(_inputFile);

        private static List<string> linesList;
        private static List<string> outputLinesList;
        private static List<Command> commandsList;

        static void Main(string[] args)
        {
            PrepareFile();
            PopulateCommandList();
            var codeWriter = new CodeWriter(commandsList);
            outputLinesList = codeWriter.Write();
            Console.WriteLine(commandsList.Count);
        }

        private static void PopulateCommandList()
        {
            commandsList = new List<Command>(linesList.Count);
            foreach (string line in linesList)
            {
                var command = LineParser.GetCommand(line);
                if (command.Type != Command.commandType.INVALID)
                {
                    commandsList.Add(command);
                }
            }
        }

        private static void PrepareFile()
        {
            List<string> inputLinesList = File.ReadAllLines(_inputFile).ToList();
            //initialize here so we can supply capacity for speed
            linesList = new List<string>(inputLinesList.Count);


            foreach (string line in inputLinesList)
            {
                //skip commented lines or save text before comments
                var parts = line.Split('/');
                if (!string.IsNullOrEmpty(parts[0]))
                {
                    linesList.Add(parts[0]);
                }
            }
        }
    }
}
