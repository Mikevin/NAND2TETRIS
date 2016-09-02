using System;

namespace VMTranslator.Types.Commands
{
    class ArithmeticCommand : Command
    {
        public enum ArithmeticOperation
        {
            add,
            sub,
            neg,
            eq,
            gt,
            lt,
            and,
            or,
            not
        }

        public ArithmeticOperation Operation { get; private set; }

        public ArithmeticCommand(string arg1, int arg2) : base(arg1, arg2)
        {
            Operation = parseArithmeticOperation(arg1);
        }

        private ArithmeticOperation parseArithmeticOperation(string s)
        {
            switch (s.ToLower())
            {
                case "add":
                    return ArithmeticOperation.add;
                case "sub":
                    return ArithmeticOperation.sub;
                case "neg":
                    return ArithmeticOperation.neg;
                case "eq":
                    return ArithmeticOperation.eq;
                case "gt":
                    return ArithmeticOperation.gt;
                case "lt":
                    return ArithmeticOperation.lt;
                case "and":
                    return ArithmeticOperation.and;
                case "or":
                    return ArithmeticOperation.or;
                case "not":
                    return ArithmeticOperation.not;
                default:
                    throw new ArgumentOutOfRangeException(nameof(s));
            }
        }
    }
}
