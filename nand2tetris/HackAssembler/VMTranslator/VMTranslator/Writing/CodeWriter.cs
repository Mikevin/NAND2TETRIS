﻿using System;
using System.IO;
using System.Text;
using VMTranslator.Translating;
using VMTranslator.Types;

namespace VMTranslator.Writing
{
    internal class CodeWriter
    {
        public const string IncrementSp = "@SP\nM=M+1\n";
        public const string DecrementSp = "@SP\nM=M-1\n";
        public const string StoreSpValueInD = "@SP\nA=M\nD=M\n";
        public const string StoreDValueInSp = "@SP\nA=M\nM=D\n";

        private readonly FileStream _fileStream;
        private readonly StreamWriter _streamWriter;
        private readonly PushPopTranslator _pushPopTranslator;

        public CodeWriter(FileStream fileStream)
        {
            _fileStream = fileStream;
            _streamWriter = new StreamWriter(_fileStream, Encoding.ASCII);

            var filename = ParseFilenameFromPath(fileStream);
            _pushPopTranslator = new PushPopTranslator(filename);

            InitializeSegments();
        }

        private static string ParseFilenameFromPath(FileStream fileStream)
        {
            var filename = Path.GetFileName(fileStream.Name);
            filename = filename?.Split('.')[0];
            return filename;
        }

        private static string GenerateSegmentInitializationString(string identifier, string value)
        {
            return $"@{value}\nD=A\n@{identifier}\nM=D\n";
        }

        private void InitializeSegments()
        {

            var initializationString = new StringBuilder();
            initializationString.Append("//initialization\n");
            initializationString.Append(GenerateSegmentInitializationString("SP", "256"));
            initializationString.Append(GenerateSegmentInitializationString("LCL", "300"));
            initializationString.Append(GenerateSegmentInitializationString("ARG", "400"));
            initializationString.Append(GenerateSegmentInitializationString("THIS", "3000"));
            initializationString.Append(GenerateSegmentInitializationString("THAT", "3010"));
            initializationString.Append(GenerateSegmentInitializationString("TMP", "5"));
            initializationString.Append("//initialization end\n");
            _streamWriter.Write(initializationString.ToString());
        }

        public void WriteArithmetic(string command)
        {
            var translated = ArithmeticTranslator.TranslateArithmeticCommand(command);
            var arithmeticString = new StringBuilder();
            arithmeticString.Append($"//{command}\n");
            arithmeticString.Append(translated);
            arithmeticString.Append($"//{command} end\n");
            _streamWriter.Write(arithmeticString.ToString());
        }

        public void WritePushPop(CommandType command, string memorySegment, int index)
        {
            var pushPopString = new StringBuilder();
            pushPopString.Append($"//start {command} {memorySegment} {index}\n");
            pushPopString.Append(_pushPopTranslator.Translate(command, memorySegment, index));
            pushPopString.Append($"//end {command} {memorySegment} {index}\n");

            _streamWriter.Write(pushPopString.ToString());
        }

        public void WriteLabel(string label)
        {
            throw new NotImplementedException();
        }

        public void WriteGoto(string label)
        {
            throw new NotImplementedException();
        }

        public void WriteIf(string label)
        {
            throw new NotImplementedException();
        }

        public void WriteCall(string name, int argsCount)
        {
            throw new NotImplementedException();
        }

        public void WriteReturn()
        {
            throw new NotImplementedException();
        }

        public void WriteFunction(string name, int localsCount)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            _streamWriter.Close();
            _fileStream.Close();
        }
    }
}
