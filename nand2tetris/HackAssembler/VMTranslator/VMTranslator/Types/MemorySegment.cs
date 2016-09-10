using System;

namespace VMTranslator.Types
{
    public static class MemorySegment
    {
        public static int StartingAddress(SegmentType segment)
        {
            switch (segment)
            {
                case SegmentType.This:
                    return 3000;
                case SegmentType.Local:
                    return 300;
                case SegmentType.Argument:
                    return 400;
                case SegmentType.That:
                    return 3010;
                case SegmentType.Temp:
                    return 5;
                default:
                    throw new ArgumentOutOfRangeException(nameof(segment), segment, null);
            }
        }

        public enum SegmentType
        {
            Static,
            This,
            Local,
            Argument,
            That,
            Constant,
            Pointer,
            Temp
        }

        public static SegmentType ParseSegment(string s)
        {
            switch (s.ToLower())
            {
                case "static":
                    return SegmentType.Static;
                case "this":
                    return SegmentType.This;
                case "local":
                    return SegmentType.Local;
                case "argument":
                    return SegmentType.Argument;
                case "that":
                    return SegmentType.That;
                case "constant":
                    return SegmentType.Constant;
                case "pointer":
                    return SegmentType.Pointer;
                case "temp":
                    return SegmentType.Temp;
                default:
                    throw new ArgumentException(s + " is not a known SegmentType.");
            }
        }
    }
}
