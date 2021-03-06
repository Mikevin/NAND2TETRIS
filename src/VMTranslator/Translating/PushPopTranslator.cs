﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using VMTranslator.Types;
using VMTranslator.Writing;

namespace VMTranslator.Translating
{
    public class PushPopTranslator
    {
        public PushPopTranslator(string fileName)
        {
            FileName = fileName;
        }

        private string FileName { get; }

        public string Translate(CommandType command, string memorySegment, int index)
        {
            var asm = string.Empty;
            var segment = MemorySegment.ParseSegment(memorySegment);

            switch (command)
            {
                case CommandType.Push:
                    asm += Push(segment, index);
                    break;
                case CommandType.Pop:
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
            var retrieveValueString = LoadValueIntoD(segment, index);
            stringBuilder.Append(retrieveValueString);
            stringBuilder.Append(CodeWriter.StoreDValueInSp);
            stringBuilder.Append(CodeWriter.IncrementSp);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Retrieves value from the memory location indicated by segment and index and stores it in register D
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private string LoadValueIntoD(MemorySegment.SegmentType segment, int index)
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
                    if (index == 0)
                    {
                        asm = @"@THIS
                                 D=M";
                    }
                    else if (index == 1)
                    {
                        asm = @"@THAT
                                 D=M";
                    }
                    else
                    {
                        throw new Exception("Pointer must have index 0 or 1.");
                    }
                    break;
                case MemorySegment.SegmentType.Temp:
                    asm = $@"@TMP
                             D=M
                             @{index}
                             A=D+A
                             D=M";
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
            stringBuilder.Append($"@R{temporaryRegister}\nA=M\nM=D\n");
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
                    asm = $@"@LCL
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
                    if (index == 0)
                    {
                        asm = @"@THIS
                             D=A";
                    }
                    else if (index == 1)
                    {
                        asm = @"@THAT
                             D=A";
                    }
                    else
                    {
                        throw new Exception("Pointer must have index 0 or 1.");
                    }
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