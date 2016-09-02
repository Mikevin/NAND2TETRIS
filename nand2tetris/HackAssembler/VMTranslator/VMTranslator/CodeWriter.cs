using System;
using System.ComponentModel;
using System.IO;
using VMTranslator.Types;

namespace VMTranslator
{
    class CodeWriter
    {
        private FileStream _fileStream;

        public CodeWriter(FileStream fileStream)
        {
            this._fileStream = fileStream;
        }

        public void SetFileName(string fileName)
        {
            throw new NotImplementedException();
        }

        public void WriteArithmetic(string command)
        {
            var translated = TransLateArithmetic(command);
        }

        private string TransLateArithmetic(string command)
        {
            throw new NotImplementedException();
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

        private void WritePop(MemorySegment.SegmentType segment, int index)
        {
            string address = string.Empty;
            switch (segment)
            {
                case MemorySegment.SegmentType.Static:
                    address = $"@{16 + index}";
                    break;
                case MemorySegment.SegmentType.This:
                    address = $@"@THIS
                                  D=M
                                  @{index}
                                  D=A+D";
                    break;
                case MemorySegment.SegmentType.Local:
                    address = $@"@LCL
                                  D=M
                                  @{index}
                                  D=A+D";
                    break;
                case MemorySegment.SegmentType.Argument:
                    address = $@"@ARG
                                  D=M
                                  @{index}
                                  D=A+D";
                    break;
                case MemorySegment.SegmentType.That:
                    address = $@"@THAT
                                  D=M
                                  @{index}
                                  D=A+D";
                    break;
                case MemorySegment.SegmentType.Constant:
                    address = $@"@{index}
                                 D=A";
                    break;
                case MemorySegment.SegmentType.Pointer:
                    break;
                case MemorySegment.SegmentType.Temp:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(segment), segment, null);
            }

            string asm = address +
                        @"@SP
                          A=A-1";
        }

        private void WritePush(MemorySegment.SegmentType segment, int index)
        {
            string address = string.Empty;
            switch (segment)
            {
                case MemorySegment.SegmentType.Static:
                    address = $"@{16 + index}";
                    break;
                case MemorySegment.SegmentType.This:
                    address = $@"@THIS
                                  D=M
                                  @{index}
                                  D=A+D";
                    break;
                case MemorySegment.SegmentType.Local:
                    address = $@"@LCL
                                  D=M
                                  @{index}
                                  D=A+D";
                    break;
                case MemorySegment.SegmentType.Argument:
                    address = $@"@ARG
                                  D=M
                                  @{index}
                                  D=A+D";
                    break;
                case MemorySegment.SegmentType.That:
                    address = $@"@THAT
                                  D=M
                                  @{index}
                                  D=A+D";
                    break;
                case MemorySegment.SegmentType.Constant:
                    address = $@"@{index}
                                 D=A";
                    break;
                case MemorySegment.SegmentType.Pointer:
                    break;
                case MemorySegment.SegmentType.Temp:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(segment), segment, null);
            }

            string asm = address +
                        @"@SP
                          M=D";
        }

        public void Close()
        {

        }
    }
}
