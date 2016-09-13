using System;
using System.IO;
using VMTranslator.Parsing;
using VMTranslator.Types;
using VMTranslator.Writing;

namespace VMTranslator
{
    static class Translator
    {
        public static void TranslateFile(string sourceFilePath, string targetFilePath)
        {
            var inputStream = new FileStream(sourceFilePath, FileMode.Open);
            var outputStream = new FileStream(targetFilePath, FileMode.Create);

            var filename = Path.GetFileName(sourceFilePath);

            var parser = new Parser(inputStream);
            var codeWriter = new CodeWriter(outputStream, filename);
            RunTranslation(parser, codeWriter);

            codeWriter.Close();
        }

        public static void TranslateDirectory(string sourceDirectoryPath, string targetFilePath)
        {
            var outputStream = new FileStream(targetFilePath, FileMode.Create);
            var codeWriter = new CodeWriter(outputStream, "");

            var directoryInfo = new DirectoryInfo(sourceDirectoryPath);
            var sourceFiles = directoryInfo.EnumerateFiles("*.vm", SearchOption.AllDirectories);


            foreach (var sourceFile in sourceFiles)
            {
                var inputStream = sourceFile.OpenRead();
                var parser = new Parser(inputStream);

                codeWriter.UpdateFileName(sourceFile.Name);


                RunTranslation(parser, codeWriter);
                inputStream.Close();
            }

            codeWriter.Close();
        }

        private static void RunTranslation(Parser parser, CodeWriter codeWriter)
        {
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
        }
    }
}