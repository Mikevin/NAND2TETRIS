using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace VMTranslator
{
    static class Program
    {

        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }

            [Option('d', "directory", Required = false, HelpText = "Translate every file in directory.")]
            public bool UseDirectory { get; set; } = false;

            [Value(0, MetaName = "input", HelpText = "Path to input file.", Required = true)]
            public string InputPath { get; set; }

            [Value(1, MetaName = "output", HelpText = "Path to output file.", Required = false)]
            public string OutputPath { get; set; }
        }

        private static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(RunOptionsAndReturnExitCode)
                .WithNotParsed<Options>(HandleParseError);
        }

        private static void RunOptionsAndReturnExitCode(Options options)
        {
            if (options.UseDirectory)
            {
                var directoryInfo = new DirectoryInfo(options.InputPath);
                if (!directoryInfo.Exists)
                {
                    throw new ArgumentException($"Input directory path {options.InputPath} does not exist");
                }

                string inputDirectory = directoryInfo.FullName;

                string outputFileName;
                if (string.IsNullOrEmpty(options.OutputPath))
                {
                    outputFileName = inputDirectory + directoryInfo.Name + ".asm";
                }
                else
                {
                    outputFileName = options.OutputPath;
                }

                TranslateDirectory(inputDirectory, inputDirectory + outputFileName);
            }
            else
            {
                var fileInfo = new FileInfo(options.InputPath);
                if (!fileInfo.Exists)
                {
                    throw new ArgumentException($"Input file path {options.InputPath} does not exist");
                }

                string inputFile = fileInfo.FullName;

                string outputFileName;
                if (string.IsNullOrEmpty(options.OutputPath))
                {
                    outputFileName = inputFile.Replace(".vm", ".asm");
                }
                else
                {
                    outputFileName = options.OutputPath;
                }
                TranslateFile(inputFile, outputFileName);
            }
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            foreach (var error in errs)
            {
                Console.Write(error);
            }
            Console.ReadLine();
        }

        private static void TranslateDirectory(string inputDirectory, string outputFilePath)
        {
            Translator.TranslateDirectory(inputDirectory, outputFilePath);
        }

        private static void TranslateFile(string inputFile, string filePath)
        {
            //const string InputFile = @"E:\Learning\Nand2Tetris\nand2tetris\projects\vmtest.vm";

            Translator.TranslateFile(inputFile, filePath);
        }
    }
}
