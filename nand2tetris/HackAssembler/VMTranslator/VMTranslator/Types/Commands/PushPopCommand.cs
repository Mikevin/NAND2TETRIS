namespace VMTranslator.Types
{
    class PushPopCommand : Command
    {
        public MemorySegment.SegmentType SegmentType { get; private set; }

        public int Address => arg2;

        public PushPopCommand(string arg1, int arg2) : base(arg1, arg2)
        {
            SegmentType = MemorySegment.ParseSegment(arg1);
        }
    }
}
