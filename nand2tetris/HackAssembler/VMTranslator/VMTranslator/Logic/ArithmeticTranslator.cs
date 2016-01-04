using System;
using System.Collections.Generic;
using VMTranslator.Types.Commands;

namespace VMTranslator
{


    public class ArithmeticTranslator
    {
        public Command Command { get; }
        private ArithmeticCommand.ArithmeticOperation _arithmeticOperation;

        public ArithmeticTranslator(Command Command)
        {
            this.Command = Command;
            _arithmeticOperation = parseArithmeticOperation();
        }

        private ArithmeticCommand.ArithmeticOperation parseArithmeticOperation()
        {
            switch (Command.arg1.ToLower())
            {
                case "add":
                    return ArithmeticCommand.ArithmeticOperation.add;
                case "sub":
                    return ArithmeticCommand.ArithmeticOperation.sub;
                case "neg":
                    return ArithmeticCommand.ArithmeticOperation.neg;
                case "eq":
                    return ArithmeticCommand.ArithmeticOperation.eq;
                case "gt":
                    return ArithmeticCommand.ArithmeticOperation.gt;
                case "lt":
                    return ArithmeticCommand.ArithmeticOperation.lt;
                case "and":
                    return ArithmeticCommand.ArithmeticOperation.and;
                case "or":
                    return ArithmeticCommand.ArithmeticOperation.or;
                case "not":
                    return ArithmeticCommand.ArithmeticOperation.not;
                default:
                    throw new ArgumentOutOfRangeException("This ArithmeticOperation is not supported.");
            }
        }

        internal IEnumerable<string> Translate()
        {
            List<string> translation;

            switch (_arithmeticOperation)
            {
                case ArithmeticCommand.ArithmeticOperation.gt:
                    translation = writeGt();
                    break;
                case ArithmeticCommand.ArithmeticOperation.or:
                    translation = writeOr();
                    break;
                case ArithmeticCommand.ArithmeticOperation.add:
                    translation = writeAdd();
                    break;
                case ArithmeticCommand.ArithmeticOperation.and:
                    translation = writeAnd();
                    break;
                case ArithmeticCommand.ArithmeticOperation.eq:
                    translation = writeEq();
                    break;
                case ArithmeticCommand.ArithmeticOperation.lt:
                    translation = writeLt();
                    break;
                case ArithmeticCommand.ArithmeticOperation.neg:
                    translation = writeNeg();
                    break;
                case ArithmeticCommand.ArithmeticOperation.not:
                    translation = writeNot();
                    break;
                case ArithmeticCommand.ArithmeticOperation.sub:
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