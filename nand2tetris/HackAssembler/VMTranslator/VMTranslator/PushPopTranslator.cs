using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using VMTranslator.Types;

namespace VMTranslator
{
    static class PushPopTranslator
    {

        public static string TransLatePushPop(CommandType command, string memorySegment, int index)
        {
            var asm = string.Empty;
            var segment = MemorySegment.ParseSegment(memorySegment);

            switch (command)
            {
                case CommandType.CPush:
                    asm += WritePush(segment, index);
                    break;
                case CommandType.CPop:
                    asm += WritePop(segment, index);
                    break;
                default:
                    throw new InvalidEnumArgumentException("Command is not of type Push or Pop.");
            }

            return asm;
        }

        /// <summary>
        /// Pops element from the stack onto the selected MemorySegment
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="index"></param>
        private static string WritePop(MemorySegment.SegmentType segment, int index)
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
        private static string RetrieveValue(MemorySegment.SegmentType segment, int index)
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
        /// Pushes value from the selected memorysegment to the stack
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="index"></param>
        private static string WritePush(MemorySegment.SegmentType segment, int index)
        {
            Debug.Assert(index > -1);

            //check constant because it works in a very different way
            if (segment == MemorySegment.SegmentType.Constant)
            {
                return $"@{index}\nD=A\n" + CodeWriter.StoreDValueInSp + CodeWriter.IncrementSp;
            }

            int temporaryRegister = 1;
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

        private static string StoreAddressInRegister(MemorySegment.SegmentType segment, int index, int register)
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
    }
}