using System;
using System.Collections.Generic;

namespace VMTranslator
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

    public class ArithmeticTranslator
    {
        public Command Command { get; }
        private ArithmeticOperation _arithmeticOperation;

        public ArithmeticTranslator(Command command)
        {
            this.Command = command;
            _arithmeticOperation = parseArithmeticOperation();
        }

        private ArithmeticOperation parseArithmeticOperation()
        {
            switch (Command.arg1.ToLower())
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
                    throw new ArgumentOutOfRangeException("This ArithmeticOperation is not supported.");
            }
        }

        internal IEnumerable<string> Translate()
        {
            List<string> translation;

            switch (_arithmeticOperation)
            {
                case ArithmeticOperation.gt:
                    translation = writeGt();
                    break;
                case ArithmeticOperation.or:
                    translation = writeOr();
                    break;
                case ArithmeticOperation.add:
                    translation = writeAdd();
                    break;
                case ArithmeticOperation.and:
                    translation = writeAnd();
                    break;
                case ArithmeticOperation.eq:
                    translation = writeEq();
                    break;
                case ArithmeticOperation.lt:
                    translation = writeLt();
                    break;
                case ArithmeticOperation.neg:
                    translation = writeNeg();
                    break;
                case ArithmeticOperation.not:
                    translation = writeNot();
                    break;
                case ArithmeticOperation.sub:
                    translation = writeSub();
                    break;
                default:
                    throw new ArithmeticException("Unknown ArithmeticOperation.");

            }

            return translation;
        }

        private List<string> writeSub()
        {
            throw new NotImplementedException();
        }

        private List<string> writeNot()
        {
            throw new NotImplementedException();
        }

        private List<string> writeNeg()
        {
            throw new NotImplementedException();
        }

        private List<string> writeLt()
        {
            throw new NotImplementedException();
        }

        private List<string> writeEq()
        {
            throw new NotImplementedException();
        }

        private List<string> writeAnd()
        {
            throw new NotImplementedException();
        }

        private List<string> writeAdd()
        {
            throw new NotImplementedException();
        }

        private List<string> writeOr()
        {
            throw new NotImplementedException();
        }

        private List<string> writeGt()
        {
            throw new NotImplementedException();
        }
    }
}