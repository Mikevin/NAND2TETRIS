namespace VMTranslator
{
    public enum CommandType
    {
        CArithmetic,
        CPush,
        CPop,
        CLabel,
        CGoto,
        CIf,
        CFunction,
        CReturn,
        CCall,
        Invalid
    }
}