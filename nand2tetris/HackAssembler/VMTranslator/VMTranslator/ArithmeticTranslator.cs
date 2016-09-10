using System;

namespace VMTranslator
{
    public static class ArithmeticTranslator
    {
        private static int _labelCount;

        public static string TranslateArithmeticCommand(string command)
        {
            var type = ParseArithmeticType(command);
            return TranslateArithmeticToAsm(type).Replace("\t", "").Replace(" ", "");
        }

        /// <summary>
        /// Translates the given ArithmeticTypeEnum case to ASM string
        /// </summary>
        /// <param name="arithmeticType"></param>
        /// <returns></returns>
        private static string TranslateArithmeticToAsm(ArithmeticTypeEnum arithmeticType)
        {
            //in case of equality operators: the order of the inputs is the same as the order on the stack
            //for example if the stack contains a in the first slot and b in the second then GT => a > b
            string asm = string.Empty;

            switch (arithmeticType)
            {
                case ArithmeticTypeEnum.Add:
                    asm += CodeWriter.DecrementSp;
                    asm += CodeWriter.StoreSpValueInD;
                    asm += CodeWriter.DecrementSp;
                    asm += "@SP\nA=M\nD=D+M\n";
                    asm += CodeWriter.StoreDValueInSp;
                    asm += CodeWriter.IncrementSp;
                    break;
                case ArithmeticTypeEnum.Sub:
                    asm += CodeWriter.DecrementSp;
                    asm += CodeWriter.StoreSpValueInD;
                    asm += CodeWriter.DecrementSp;
                    asm += "@SP\nA=M\nD=M-D\n";
                    asm += CodeWriter.StoreDValueInSp;
                    asm += CodeWriter.IncrementSp;
                    break;
                case ArithmeticTypeEnum.Neg:
                    asm += CodeWriter.DecrementSp;
                    asm += CodeWriter.StoreSpValueInD;
                    asm += "D=-D\n";
                    asm += CodeWriter.StoreDValueInSp;
                    asm += CodeWriter.IncrementSp;
                    break;
                case ArithmeticTypeEnum.Eq:
                    asm += CodeWriter.DecrementSp;
                    asm += CodeWriter.StoreSpValueInD;
                    asm += CodeWriter.DecrementSp;
                    asm += "@SP\nA=M\nD=D-M\n";
                    asm += $@"@EQUAL{_labelCount}
                                D;JEQ
                                D=0
                                @END_EQUAL{_labelCount}
                                0;JMP
                                (EQUAL{_labelCount})
                                D=-1
                                @END_EQUAL{_labelCount}
                                0;JMP
                                (END_EQUAL{_labelCount})";
                    asm += "\n";
                    asm += CodeWriter.StoreDValueInSp;
                    asm += CodeWriter.IncrementSp;
                    _labelCount++;
                    break;
                case ArithmeticTypeEnum.Gt:
                    asm += CodeWriter.DecrementSp;
                    asm += CodeWriter.StoreSpValueInD;
                    asm += CodeWriter.DecrementSp;
                    asm += "@SP\nA=M\nD=D-M\n";
                    asm += $@"@TRUE{_labelCount}
                                D;JLT
                                D=0
                                @END_COMPARISON{_labelCount}
                                0;JMP
                                (TRUE{_labelCount})
                                D=-1
                                @END_COMPARISON{_labelCount}
                                0;JMP
                                (END_COMPARISON{_labelCount})";
                    asm += "\n";
                    asm += CodeWriter.StoreDValueInSp;
                    asm += CodeWriter.IncrementSp;
                    _labelCount++;
                    break;
                case ArithmeticTypeEnum.Lt:
                    asm += CodeWriter.DecrementSp;
                    asm += CodeWriter.StoreSpValueInD;
                    asm += CodeWriter.DecrementSp;
                    asm += "@SP\nA=M\nD=D-M\n";
                    asm += $@"@TRUE{_labelCount}
                                D;JGT
                                D=0
                                @END_COMPARISON{_labelCount}
                                0;JMP
                                (TRUE{_labelCount})
                                D=-1
                                @END_COMPARISON{_labelCount}
                                0;JMP
                                (END_COMPARISON{_labelCount})";
                    asm += "\n";
                    asm += CodeWriter.StoreDValueInSp;
                    asm += CodeWriter.IncrementSp;
                    _labelCount++;
                    break;
                case ArithmeticTypeEnum.And:
                    asm += CodeWriter.DecrementSp;
                    asm += CodeWriter.StoreSpValueInD;
                    asm += CodeWriter.DecrementSp;
                    asm += "A=M\nD=D&M\n";
                    asm += CodeWriter.StoreDValueInSp;
                    asm += CodeWriter.IncrementSp;
                    break;
                case ArithmeticTypeEnum.Or:
                    asm += CodeWriter.DecrementSp;
                    asm += CodeWriter.StoreSpValueInD;
                    asm += CodeWriter.DecrementSp;
                    asm += "A=M\nD=D|M\n";
                    asm += CodeWriter.StoreDValueInSp;
                    asm += CodeWriter.IncrementSp;
                    break;
                case ArithmeticTypeEnum.Not:
                    asm += CodeWriter.DecrementSp;
                    asm += CodeWriter.StoreSpValueInD;
                    asm += "D=!D\n";
                    asm += CodeWriter.StoreDValueInSp;
                    asm += CodeWriter.IncrementSp;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(arithmeticType), arithmeticType, null);
            }
            return asm;
        }

        private enum ArithmeticTypeEnum
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

        private static ArithmeticTypeEnum ParseArithmeticType(string command)
        {
            switch (command)
            {
                case "add":
                    return ArithmeticTypeEnum.Add;
                case "sub":
                    return ArithmeticTypeEnum.Sub;
                case "neg":
                    return ArithmeticTypeEnum.Neg;
                case "eq":
                    return ArithmeticTypeEnum.Eq;
                case "gt":
                    return ArithmeticTypeEnum.Gt;
                case "lt":
                    return ArithmeticTypeEnum.Lt;
                case "and":
                    return ArithmeticTypeEnum.And;
                case "or":
                    return ArithmeticTypeEnum.Or;
                case "not":
                    return ArithmeticTypeEnum.Not;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}