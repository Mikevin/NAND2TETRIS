using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using VMTranslator.Types;

namespace VMTranslator
{
    public class PushPopTranslator
    {
        public PushPopTranslator(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; private set; }

        public string Translate(CommandType command, string memorySegment, int index)
        {
            var asm = string.Empty;
            var segment = MemorySegment.ParseSegment(memorySegment);

            switch (command)
            {
                case CommandType.CPush:
                    asm += Push(segment, index);
                    break;
                case CommandType.CPop:
                    asm += Pop(segment, index);
                    break;
                default:
                    throw new InvalidEnumArgumentException("Command is not of type Push or Pop.");
            }
            asm = asm.Replace("\t", "").Replace(" ", "");
            return asm;
        }

        /// <summary>
        /// Push onto the stack
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string Push(MemorySegment.SegmentType segment, int index)
        {
            var stringBuilder = new StringBuilder();
            //make sure value is stored in register D
            var retrieveValueString = RetrieveValue(segment, index);
            stringBuilder.Append($"// {segment} {index}\n");
            stringBuilder.Append(retrieveValueString);
            stringBuilder.Append(CodeWriter.StoreDValueInSp);
            stringBuilder.Append(CodeWriter.IncrementSp);
            stringBuilder.Append($"//end {segment} {index}\n");

            return stringBuilder.ToString();
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
                    asm = $@"@{FileName + '.' + index}
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
                    asm = $@"@TMP
                             D=M
                             @{index}
                             A=D+A
                             D=M";//TODO M=D?
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(segment), segment, null);
            }

            asm += "\n";
            return asm;
        }

        /// <summary>
        /// Pop from the stack onto the segment base + index
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string Pop(MemorySegment.SegmentType segment, int index)
        {
            Debug.Assert(index > -1);

            int temporaryRegister = 13;
            var storeAddressinR1String = StoreAddressInRegister(segment, index, temporaryRegister);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"// {segment} {index}\n");
            stringBuilder.Append(storeAddressinR1String);
            stringBuilder.Append(CodeWriter.DecrementSp);
            stringBuilder.Append(CodeWriter.StoreSpValueInD);
            stringBuilder.Append($"@R{temporaryRegister}\nM=D\n");
            stringBuilder.Append($"//end {segment} {index}\n");

            return stringBuilder.ToString().Replace("\t", "").Replace(" ", "");
        }

        private string StoreAddressInRegister(MemorySegment.SegmentType segment, int index, int register)
        {
            string asm = string.Empty;
            switch (segment)
            {
                case MemorySegment.SegmentType.Static:
                    asm = $@"@{FileName + '.' + index}
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
                    asm = $@"@TMP
                             D=M
                             @{index}
                             D=D+A";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(segment), segment, null);
            }
            asm += $"\n@R{register}\nM=D\n";
            return asm;
        }
    }
}