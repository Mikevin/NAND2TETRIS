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
