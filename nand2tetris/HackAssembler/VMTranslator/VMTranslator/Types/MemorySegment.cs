using System;

namespace VMTranslator.Types
{
    public static class MemorySegment
    {
        public enum SegmentType
        {
            STATIC,
            THIS,
            LOCAL,
            ARGUMENT,
            THAT,
            CONSTANT,
            POINTER,
            TEMP
        }

        public static SegmentType ParseSegment(string s)
        {
            switch (s.ToLower())
            {
                case "static":
                    return SegmentType.STATIC;
                case "this":
                    return SegmentType.THIS;
                case "local":
                    return SegmentType.LOCAL;
                case "argument":
                    return SegmentType.ARGUMENT;
                case "that":
                    return SegmentType.THAT;
                case "constant":
                    return SegmentType.CONSTANT;
                case "pointer":
                    return SegmentType.POINTER;
                case "temp":
                    return SegmentType.TEMP;
                default:
                    throw new ArgumentException(s + " is not a known SegmentType.");
            }
        }
    }
}
