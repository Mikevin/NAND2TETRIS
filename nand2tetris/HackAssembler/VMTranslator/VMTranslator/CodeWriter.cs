using System.IO;
using System.Text;

namespace VMTranslator
{
    class CodeWriter
    {
        public const string IncrementSp = "@SP\nM=M+1\n";
        public const string DecrementSp = "@SP\nM=M-1\n";
        public const string StoreSpValueInD = "@SP\nA=M\nD=M\n";
        public const string StoreDValueInSp = "@SP\nA=M\nM=D\n";

        private readonly FileStream _fileStream;
        private readonly StreamWriter _streamWriter;

        public CodeWriter(FileStream fileStream)
        {
            _fileStream = fileStream;
            _streamWriter = new StreamWriter(_fileStream, Encoding.ASCII);

            Initialize();
        }

        private void Initialize()
        {
            _streamWriter.Write("//initialization\n@257\nD=A\n@SP\nM=D\n//initialization end\n");
        }

        public void WriteArithmetic(string command)
        {
            var translated = ArithmeticTranslator.TranslateArithmeticCommand(command);
            _streamWriter.Write($"//{command}\n");
            _streamWriter.Write(translated.Replace("\t", "").Replace(" ", ""));
            _streamWriter.Write($"//{command} end\n");
        }

        public void WritePushPop(CommandType command, string memorySegment, int index)
        {
            var asm = $"//start {command} {memorySegment} {index}\n";
            asm += PushPopTranslator.TransLatePushPop(command, memorySegment, index);
            asm += $"//end {command} {memorySegment} {index}\n";
            _streamWriter.Write(asm);
        }

        public void Close()
        {
            _streamWriter.Close();
            _fileStream.Close();
        }
    }
}
