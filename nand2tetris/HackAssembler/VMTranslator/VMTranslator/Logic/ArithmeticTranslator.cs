using System;
using System.Collections.Generic;
using VMTranslator.Types.Commands;

namespace VMTranslator
{


    public class ArithmeticTranslator
    {
        public Command Command { get; }
        private ArithmeticCommand.ArithmeticOperation _arithmeticOperation;

        public ArithmeticTranslator(Command command)
        {
            this.Command = command;
            _arithmeticOperation = ParseArithmeticOperation();
        }

        private ArithmeticCommand.ArithmeticOperation ParseArithmeticOperation()
        {
            switch (Command.Arg1.ToLower())
            {
                case "add":
                    return ArithmeticCommand.ArithmeticOperation.Add;
                case "sub":
                    return ArithmeticCommand.ArithmeticOperation.Sub;
                case "neg":
                    return ArithmeticCommand.ArithmeticOperation.Neg;
                case "eq":
                    return ArithmeticCommand.ArithmeticOperation.Eq;
                case "gt":
                    return ArithmeticCommand.ArithmeticOperation.Gt;
                case "lt":
                    return ArithmeticCommand.ArithmeticOperation.Lt;
                case "and":
                    return ArithmeticCommand.ArithmeticOperation.And;
                case "or":
                    return ArithmeticCommand.ArithmeticOperation.Or;
                case "not":
                    return ArithmeticCommand.ArithmeticOperation.Not;
                default:
                    throw new ArgumentOutOfRangeException("This ArithmeticOperation is not supported.");
            }
        }

        internal IEnumerable<string> Translate()
        {
            List<string> translation;

            switch (_arithmeticOperation)
            {
                case ArithmeticCommand.ArithmeticOperation.Gt:
                    translation = WriteGt();
                    break;
                case ArithmeticCommand.ArithmeticOperation.Or:
                    translation = WriteOr();
                    break;
                case ArithmeticCommand.ArithmeticOperation.Add:
                    translation = WriteAdd();
                    break;
                case ArithmeticCommand.ArithmeticOperation.And:
                    translation = WriteAnd();
                    break;
                case ArithmeticCommand.ArithmeticOperation.Eq:
                    translation = WriteEq();
                    break;
                case ArithmeticCommand.ArithmeticOperation.Lt:
                    translation = WriteLt();
                    break;
                case ArithmeticCommand.ArithmeticOperation.Neg:
                    translation = WriteNeg();
                    break;
                case ArithmeticCommand.ArithmeticOperation.Not:
                    translation = WriteNot();
                    break;
                case ArithmeticCommand.ArithmeticOperation.Sub:
                    translation = WriteSub();
                    break;
                default:
                    throw new ArithmeticException("Unknown ArithmeticOperation.");

            }

            return translation;
        }

        private List<string> WriteSub()
        {
            throw new NotImplementedException();
        }

        private List<string> WriteNot()
        {
            throw new NotImplementedException();
        }

        private List<string> WriteNeg()
        {
            throw new NotImplementedException();
        }

        private List<string> WriteLt()
        {
            throw new NotImplementedException();
        }

        private List<string> WriteEq()
        {
            throw new NotImplementedException();
        }

        private List<string> WriteAnd()
        {
            throw new NotImplementedException();
        }

        private List<string> WriteAdd()
        {
            throw new NotImplementedException();
        }

        private List<string> WriteOr()
        {
            throw new NotImplementedException();
        }

        private List<string> WriteGt()
        {
            throw new NotImplementedException();
        }
    }
}