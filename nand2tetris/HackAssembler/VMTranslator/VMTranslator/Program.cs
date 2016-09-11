using System;
using System.IO;
using VMTranslator.Parsing;
using VMTranslator.Types;
using VMTranslator.Writing;

namespace VMTranslator
{
    static class Program
    {
        const string InputFile = @"E:\Learning\Nand2Tetris\nand2tetris\projects\07\MemoryAccess\StaticTest\StaticTest.vm";

        static void Main(string[] args)
        {
            var inputFileStream = new FileStream(InputFile, FileMode.Open);
            string outputFilePath = InputFile.Replace(".vm", ".asm");
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }
            var outputFileStream = new FileStream(outputFilePath, FileMode.CreateNew);

            Translate(inputFileStream, outputFileStream);

            Console.WriteLine("Done.");
            //Console.Read();
        }

        private static void Translate(FileStream inputFileStream, FileStream outputFileStream)
        {
            var parser = new Parser(inputFileStream);
            var codeWriter = new CodeWriter(outputFileStream);
            while (parser.HasMoreCommands)
            {
                parser.Advance();

                switch (parser.CurrentCommandType)
                {
                    case CommandType.Push:
                        codeWriter.WritePushPop(CommandType.Push, parser.Arg1, parser.Arg2);
                        break;
                    case CommandType.Pop:
                        codeWriter.WritePushPop(CommandType.Pop, parser.Arg1, parser.Arg2);
                        break;
                    case CommandType.Arithmetic:
                        codeWriter.WriteArithmetic(parser.Arg1);
                        break;
                    case CommandType.Label:
                        codeWriter.WriteLabel(parser.Arg1);
                        break;
                    case CommandType.Goto:
                        codeWriter.WriteGoto(parser.Arg1);
                        break;
                    case CommandType.If:
                        codeWriter.WriteIf(parser.Arg1);
                        break;
                    case CommandType.Function:
                        codeWriter.WriteFunction(parser.Arg1, parser.Arg2);
                        break;
                    case CommandType.Return:
                        codeWriter.WriteReturn();
                        break;
                    case CommandType.Call:
                        codeWriter.WriteCall(parser.Arg1, parser.Arg2);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            codeWriter.Close();
        }
    }
}
