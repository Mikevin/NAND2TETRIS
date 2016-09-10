using System.IO;
using System.Text;
using VMTranslator.Types;

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
        private readonly PushPopTranslator _pushPopTranslator;

        public CodeWriter(FileStream fileStream)
        {
            _fileStream = fileStream;
            _streamWriter = new StreamWriter(_fileStream, Encoding.ASCII);

            var filename = Path.GetFileName(fileStream.Name);
            filename = filename?.Split('.')[0];
            _pushPopTranslator = new PushPopTranslator(filename);

            Initialize();
        }

        private void Initialize()
        {
            _streamWriter.Write("//initialization\n");
            //init SP
            _streamWriter.Write("@256\nD=A\n@SP\nM=D\n");
            //init lcl
            _streamWriter.Write($"@{MemorySegment.StartingAddress(MemorySegment.SegmentType.Local)}\nD=A\n@LCL\nM=D\n");
            //init arg
            _streamWriter.Write($"@{MemorySegment.StartingAddress(MemorySegment.SegmentType.Argument)}\nD=A\n@ARG\nM=D\n");
            //init this
            _streamWriter.Write($"@{MemorySegment.StartingAddress(MemorySegment.SegmentType.This)}\nD=A\n@THIS\nM=D\n");
            //init that
            _streamWriter.Write($"@{MemorySegment.StartingAddress(MemorySegment.SegmentType.That)}\nD=A\n@THAT\nM=D\n");
            //init temp
            _streamWriter.Write($"@{MemorySegment.StartingAddress(MemorySegment.SegmentType.Temp)}\nD=A\n@TMP\nM=D\n");
            _streamWriter.Write("//initialization end\n");
        }

        public void WriteArithmetic(string command)
        {
            var translated = ArithmeticTranslator.TranslateArithmeticCommand(command);
            _streamWriter.Write($"//{command}\n");
            _streamWriter.Write(translated);
            _streamWriter.Write($"//{command} end\n");
        }

        public void WritePushPop(CommandType command, string memorySegment, int index)
        {
            var asm = $"//start {command} {memorySegment} {index}\n";
            asm += _pushPopTranslator.Translate(command, memorySegment, index);
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
