﻿using System;
using System.IO;

namespace VMTranslator
{
    class Program
    {
        const string InputFile = @"E:\Learning\Nand2Tetris\nand2tetris\projects\07\StackArithmetic\SimpleAdd\SimpleAdd.vm";

        public static string FileName => Path.GetFileName(InputFile);

        static void Main(string[] args)
        {
            var inputFileStream = new FileStream(InputFile, FileMode.Open);
            string outputFileName = FileName.Replace(".vm", ".asm");
            if (File.Exists(outputFileName))
            {
                File.Delete(outputFileName);
            }
            var outputFileStream = new FileStream(outputFileName, FileMode.CreateNew);
            var parser = new Parser(inputFileStream);
            var codeWriter = new CodeWriter(outputFileStream);
            while (parser.HasMoreCommands)
            {
                parser.Advance();

                switch (parser.CurrentCommandType)
                {
                    case CommandType.CPush:
                        codeWriter.WritePushPop(CommandType.CPush, parser.Arg1, parser.Arg2);
                        break;
                    case CommandType.CPop:
                        codeWriter.WritePushPop(CommandType.CPop, parser.Arg1, parser.Arg2);
                        break;
                    default:
                        break;
                }
            }

            codeWriter.Close();

            Console.WriteLine("Done.");
            Console.Read();
        }
    }
}
