using VMTranslator.Types;

namespace VMTranslator
{
    public class Command
    {
        public Command(commandType type, string arg1, int arg2)
        {
            Type = type;
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

        public commandType Type { get; private set; }

        public string arg1 { get; private set; }

        public int arg2 { get; private set; }
    }
}