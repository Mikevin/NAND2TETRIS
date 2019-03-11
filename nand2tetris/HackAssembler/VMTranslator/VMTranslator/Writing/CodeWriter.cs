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

        private string _currentFunction;
        private readonly string _sourceFileName;

        public CodeWriter(FileStream fileStream, string filename)
        {
            _fileStream = fileStream;
            _streamWriter = new StreamWriter(_fileStream, Encoding.ASCII);

            _sourceFileName = filename;
            _pushPopTranslator = new PushPopTranslator(_sourceFileName);

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

        public void InitializeSegments()
        {

            var initializationString = new StringBuilder();
            initializationString.Append("//initialization\n");
            //initializationString.Append(GenerateSegmentInitializationString("TMP", "5"));
            initializationString.Append(GenerateSegmentInitializationString("SP", "261"));
            //initializationString.Append(GenerateSegmentInitializationString("LCL", "300"));
            //initializationString.Append(GenerateSegmentInitializationString("ARG", "400"));
            //initializationString.Append(GenerateSegmentInitializationString("THIS", "3000"));
            //initializationString.Append(GenerateSegmentInitializationString("THAT", "3010"));
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

        private string GetLabelFormat(string label)
        {
            if (string.IsNullOrEmpty(_currentFunction))
            {
                return $"{_sourceFileName}.{label}";
            }

            return $"{_currentFunction}${label}";
        }

        public void WriteLabel(string label)
        {
            var labelString = new StringBuilder();

            var labelFormat = GetLabelFormat(label);
            labelString.Append($"({labelFormat})\n");

            _streamWriter.Write(labelString.ToString());
        }

        public void WriteGoto(string label)
        {
            var gotoString = new StringBuilder();
            var labelFormat = label + "$f";
            gotoString.Append($"@{labelFormat}\n");
            gotoString.Append("0;JMP\n");

            _streamWriter.Write(gotoString.ToString());
        }

        public void WriteIf(string label)
        {
            var ifString = new StringBuilder();
            ifString.Append(DecrementSp);
            ifString.Append(StoreSpValueInD);
            var labelFormat = GetLabelFormat(label);
            ifString.Append($"@{labelFormat}\n");
            ifString.Append("D;JNE\n");

            _streamWriter.Write(ifString.ToString());
        }

        public void WriteCall(string name, int argsCount)
        {
            var callString = new StringBuilder();
            // saves the return address
            var returnLabel = GetLabelFormat(name + "return");
            callString.Append($"@{returnLabel}\nD=A\n");
            callString.Append(StoreDValueInSp);
            callString.Append(IncrementSp);
            // saves the LCL of f
            callString.Append("@LCL\nD=M\n");
            callString.Append(StoreDValueInSp);
            callString.Append(IncrementSp);
            // saves the ARG of f
            callString.Append("@ARG\nD=M\n");
            callString.Append(StoreDValueInSp);
            callString.Append(IncrementSp);
            // saves the THIS of f
            callString.Append("@THIS\nD=M\n");
            callString.Append(StoreDValueInSp);
            callString.Append(IncrementSp);
            // saves the THAT of f
            callString.Append("@THAT\nD=M\n");
            callString.Append(StoreDValueInSp);
            callString.Append(IncrementSp);
            // repositions SP for g
            callString.Append("@SP\nD=M\n");
            callString.Append("@5\nD=D-A\n");
            callString.Append($"@{argsCount}\nD=D-A\n");
            callString.Append("@ARG\nM=D\n");
            // repositions LCL for g
            callString.Append(StoreSpValueInD);
            callString.Append("@LCL\nM=D" +
                              "\n");
            _streamWriter.Write(callString.ToString());

            // transfers control to g
            WriteGoto(name);
            // the generated symbol
            WriteLabel(name + "return");
        }

        public void WriteReturn()
        {
            var returnString = new StringBuilder();
            //store LCL address in register 14
            returnString.Append("@LCL\nD=M\n@R14\nM=D\n");
            //store return address in register 15
            returnString.Append("@R14\nD=M\n@5\nA=D-A\nD=M\n@R15\nM=D\n");
            _streamWriter.Write(returnString.ToString());
            returnString.Clear();
            //reposition the return value for the caller
            WritePushPop(CommandType.Pop, "argument", 0);
            //restore caller's sp by setting @SP to ARG+1
            returnString.Append("@ARG\nD=M\nD=D+1\n@SP\nM=D\n");
            //restore callers that
            returnString.Append("@R14\nD=M\n@1\nA=D-A\nD=M\n@THAT\nM=D\n");
            //restore callers this
            returnString.Append("@R14\nD=M\n@2\nA=D-A\nD=M\n@THIS\nM=D\n");
            //restore callers arg
            returnString.Append("@R14\nD=M\n@3\nA=D-A\nD=M\n@ARG\nM=D\n");
            //restore callers lcl
            returnString.Append("@R14\nD=M\n@4\nA=D-A\nD=M\n@LCL\nM=D\n");
            //goto return addresss
            returnString.Append("@R15\nA=M\n0;JMP\n");
            _streamWriter.Write(returnString.ToString());

            _currentFunction = string.Empty;
        }

        public void WriteFunction(string name, int localsCount)
        {
            _currentFunction = name;
            int count = 0;
            while (count < localsCount)
            {
                WritePushPop(CommandType.Push, "constant", 0);
                count++;
            }
            WriteLabel("f");
        }

        public void Close()
        {
            _streamWriter.Close();
            _fileStream.Close();
        }
    }
}
