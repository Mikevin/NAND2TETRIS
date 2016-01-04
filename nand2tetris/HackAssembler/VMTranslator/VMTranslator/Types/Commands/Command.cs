namespace VMTranslator
{
    public class Command
    {
        public Command(string arg1, int arg2)
        {
            this.arg1 = arg1;
            this.arg2 = arg2;
        }

        public enum commandType
        {
            C_ARITHMETIC,
            C_PUSH,
            C_POP,
            C_LABEL,
            C_GOTO,
            C_IF,
            C_FUNCTION,
            C_RETURN,
            C_CALL,
            INVALID
        }

        protected string arg1 { get; set; }

        protected int arg2 { get; set; }
    }
}