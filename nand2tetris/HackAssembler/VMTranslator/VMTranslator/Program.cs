using System;

namespace VMTranslator
{
    static class Program
    {

        static void Main(string[] args)
        {
            //TranslateFile();
            TranslateDirectory();

            Console.WriteLine("Done.");
            //Console.Read();
        }

        private static void TranslateDirectory()
        {
            const string InputDirectory = @"E:\Learning\Nand2Tetris\nand2tetris\projects\08\FunctionCalls\FibonacciElement\";
            string outputFilePath = InputDirectory + "FibonacciElement.asm";

            Translator.TranslateDirectory(InputDirectory, outputFilePath);
        }

        private static void TranslateFile()
        {
            const string InputFile = @"E:\Learning\Nand2Tetris\nand2tetris\projects\08\FunctionCalls\SimpleFunction\SimpleFunction.vm";
            //const string InputFile = @"E:\Learning\Nand2Tetris\nand2tetris\projects\vmtest.vm";

            string outputFilePath = InputFile.Replace(".vm", ".asm");

            Translator.TranslateFile(InputFile, outputFilePath);
        }
    }
}
