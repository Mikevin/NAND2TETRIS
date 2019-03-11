using System;

namespace VMTranslator.Types
{
    public static class MemorySegment
    {
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
