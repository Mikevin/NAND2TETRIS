using System;
using System.IO;

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
                }
            }

            codeWriter.Close();

            Console.WriteLine("Done.");
            //Console.Read();
        }
    }
}
