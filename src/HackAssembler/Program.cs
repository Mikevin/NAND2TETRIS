using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandLine;

namespace HackAssembler
{
    internal static class Program
    {
        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }

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

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            foreach (var error in errs)
            {
                Console.Write(error);
            }
            Console.ReadLine();
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            //const string path = @"E:\Learning\Nand2Tetris\nand2tetris\projects\06\pong\Pong.asm";
            var path = opts.InputPath;
            if (!File.Exists(opts.InputPath))
            {
                throw new ArgumentException($"Input file path {opts.InputPath} does not exist");
            }
            var outputPath = string.IsNullOrEmpty(opts.OutputPath) ? opts.InputPath + ".hack" : opts.OutputPath;
            var lines = File.ReadAllLines(path).ToList();
            var assembler = new Assembler(lines);
            var result = assembler.Assemble();


            File.WriteAllLines(outputPath, result);
        }
    }
}