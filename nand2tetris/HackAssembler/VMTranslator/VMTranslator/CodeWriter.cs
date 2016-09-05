using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using VMTranslator.Types;

namespace VMTranslator
{
    class CodeWriter
    {
        private const string IncrementSp = "@SP\nM=M+1\n";
        private const string DecrementSp = "@SP\nM=M-1\n";
        private const string StoreSpValueInD = "@SP\nA=M\nD=M\n";
        private const string StoreDValueInSp = "@SP\nM=D\n";

        private FileStream _fileStream;
        private StreamWriter _streamWriter;

        public CodeWriter(FileStream fileStream)
        {
            this._fileStream = fileStream;
            _streamWriter = new StreamWriter(_fileStream, Encoding.ASCII);

            Initialize();
        }

        private void Initialize()
        {
            _streamWriter.Write("@256\nD = A\n@SP\nM = D\n");
        }

        public void WriteArithmetic(string command)
        {
            var ArithmeticType = ParseArithmeticType(command);
            var Translated = TranslateArithmeticToAsm(ArithmeticType);
        }

        private string TranslateArithmeticToAsm(ArithmeticTypeEnum arithmeticType)
        {
            string asm = string.Empty;

            switch (arithmeticType)
            {
                case ArithmeticTypeEnum.Add:
                    asm += DecrementSp;
                    asm += StoreSpValueInD;
                    asm += DecrementSp;
                    asm += "@SP\nA=M\nD=D+M\n";
                    break;
                case ArithmeticTypeEnum.Sub:
                    asm += DecrementSp;
                    asm += StoreSpValueInD;
                    asm += DecrementSp;
                    asm += "@SP\nA=M\nD=M-D\n";
                    break;
                case ArithmeticTypeEnum.Neg:
                    asm += DecrementSp;
                    asm += StoreSpValueInD;
                    asm += "D=-D\n";
                    break;
                case ArithmeticTypeEnum.Eq:
                    asm += DecrementSp;
                    asm += StoreSpValueInD;
                    asm += DecrementSp;
                    asm += "@SP\nA=M\nD=D+M\n";
                    break;
                case ArithmeticTypeEnum.Gt:
                    break;
                case ArithmeticTypeEnum.Lt:
                    break;
                case ArithmeticTypeEnum.And:
                    break;
                case ArithmeticTypeEnum.Or:
                    break;
                case ArithmeticTypeEnum.Not:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(arithmeticType), arithmeticType, null);
            }
            asm += StoreDValueInSp;
            asm += IncrementSp;
            return asm;
        }

        enum ArithmeticTypeEnum
        {
            Add,
            Sub,
            Neg,
            Eq,
            Gt,
            Lt,
            And,
            Or,
            Not
        }

        private ArithmeticTypeEnum ParseArithmeticType(string command)
        {
            switch (command)
            {
                case "add":
                    return ArithmeticTypeEnum.Add;
                case "sub":
                    return ArithmeticTypeEnum.Sub;
                case "neg":
                    return ArithmeticTypeEnum.Neg;
                case "eq":
                    return ArithmeticTypeEnum.Eq;
                case "gt":
                    return ArithmeticTypeEnum.Gt;
                case "lt":
                    return ArithmeticTypeEnum.Lt;
                case "and":
                    return ArithmeticTypeEnum.And;
                case "or":
                    return ArithmeticTypeEnum.Or;
                case "not":
                    return ArithmeticTypeEnum.Not;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void WritePushPop(CommandType command, string memorySegment, int index)
        {
            var segment = MemorySegment.ParseSegment(memorySegment);

            if (command == CommandType.CPush)
            {
                WritePush(segment, index);
            }
            else if (command == CommandType.CPop)
            {
                WritePop(segment, index);
            }
            else
            {
                throw new InvalidEnumArgumentException("Command is not of type Push or Pop.");
            }
        }
        /// <summary>
        /// Pops element from MemorySegment onto the stack
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="index"></param>
        private void WritePop(MemorySegment.SegmentType segment, int index)
        {
            Debug.Assert(index > -1);
            var stringBuilder = new StringBuilder();
            //make sure value is stored in register D
            var RetrieveValueString = RetrieveValue(segment, index);
            stringBuilder.Append(RetrieveValueString);
            stringBuilder.Append(StoreDValueInSp);
            stringBuilder.Append(IncrementSp);

            _streamWriter.Write(stringBuilder.ToString());
        }

        /// <summary>
        /// Retrieves value from the memory location indicated by segment and index and stores it in register D
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string RetrieveValue(MemorySegment.SegmentType segment, int index)
        {
            string asm = string.Empty;

            switch (segment)
            {
                case MemorySegment.SegmentType.Static:
                    asm = $@"@{16 + index}
                             D=M";
                    break;
                case MemorySegment.SegmentType.This:
                    asm = $@"@THIS
                             D=M
                             @{index}
                             A=D+A
                             D=M";
                    break;
                case MemorySegment.SegmentType.Local:
                    asm = $@"@LCL
                             D=M
                             @{index}
                             A=D+A
                             D=M";
                    break;
                case MemorySegment.SegmentType.Argument:
                    asm = $@"@ARG
                             D=M
                             @{index}
                             A=D+A
                             D=M";
                    break;
                case MemorySegment.SegmentType.That:
                    asm = $@"@THAT
                             D=M
                             @{index}
                             A=D+A
                             D=M";
                    break;
                case MemorySegment.SegmentType.Constant:
                    asm = $@"@{index}
                             D=A";
                    break;
                case MemorySegment.SegmentType.Pointer:
                    break;
                case MemorySegment.SegmentType.Temp:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(segment), segment, null);
            }

            asm += "\n";
            return asm;
        }
        /// <summary>
        /// Pushes value from the stack to the selected memorysegment
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="index"></param>
        private void WritePush(MemorySegment.SegmentType segment, int index)
        {
            Debug.Assert(index > -1);
            int temporaryRegister = 1;
            var storeAddressinR1String = StoreAddressInRegister(segment, index, temporaryRegister);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(storeAddressinR1String);
            stringBuilder.Append(DecrementSp);
            stringBuilder.Append(StoreSpValueInD);
            stringBuilder.Append($"@R{temporaryRegister}\nM=D\n");

            _streamWriter.Write(stringBuilder.ToString().Replace("\t", "").Replace(" ", ""));
        }

        private string StoreAddressInRegister(MemorySegment.SegmentType segment, int index, int register)
        {
            string asm = string.Empty;
            switch (segment)
            {
                case MemorySegment.SegmentType.Static:
                    asm = $@"@{16 + index}
                             D=A";
                    break;
                case MemorySegment.SegmentType.This:
                    asm = $@"@THIS
                             D=M
                             @{index}
                             D=D+A";
                    break;
                case MemorySegment.SegmentType.Local:
                    asm = $@"@LOCAL
                             D=M
                             @{index}
                             D=D+A";
                    break;
                case MemorySegment.SegmentType.Argument:
                    asm = $@"@ARG
                             D=M
                             @{index}
                             D=D+A";
                    break;
                case MemorySegment.SegmentType.That:
                    asm = $@"@THAT
                             D=M
                             @{index}
                             D=D+A";
                    break;
                case MemorySegment.SegmentType.Constant:
                    asm = $@"@{index}
                             D=A
                             @R2
                             M=D
                             D=A";
                    break;
                case MemorySegment.SegmentType.Pointer:
                    break;
                case MemorySegment.SegmentType.Temp:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(segment), segment, null);
            }
            asm += $"\n@R{register}\nM=D\n";
            return asm;
        }

        public void Close()
        {
            _streamWriter.Close();
            _fileStream.Close();
        }
    }
}
