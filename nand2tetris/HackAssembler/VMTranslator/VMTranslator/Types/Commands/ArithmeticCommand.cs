using System;

namespace VMTranslator.Types.Commands
{
    class ArithmeticCommand : Command
    {
        public enum ArithmeticOperation
        {
            Add,
            Sub,
            Neg,
            Eq,
            Gt,
            Lt,
            And,
            Or,
            Not
        }

        public ArithmeticOperation Operation { get; private set; }

        public ArithmeticCommand(string arg1, int arg2) : base(arg1, arg2)
        {
            Operation = ParseArithmeticOperation(arg1);
        }

        private ArithmeticOperation ParseArithmeticOperation(string s)
        {
            switch (s.ToLower())
            {
                case "add":
                    return ArithmeticOperation.Add;
                case "sub":
                    return ArithmeticOperation.Sub;
                case "neg":
                    return ArithmeticOperation.Neg;
                case "eq":
                    return ArithmeticOperation.Eq;
                case "gt":
                    return ArithmeticOperation.Gt;
                case "lt":
                    return ArithmeticOperation.Lt;
                case "and":
                    return ArithmeticOperation.And;
                case "or":
                    return ArithmeticOperation.Or;
                case "not":
                    return ArithmeticOperation.Not;
                default:
                    throw new ArgumentOutOfRangeException(nameof(s));
            }
        }
    }
}
