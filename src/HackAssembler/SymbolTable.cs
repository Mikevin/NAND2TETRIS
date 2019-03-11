using System.Collections.Generic;

namespace HackAssembler
{
    public class SymbolTable
    {
        private readonly Dictionary<string, int> _symbolDict = new Dictionary<string, int>();
        private int _LabelCount = 0;

        public SymbolTable()
        {
            _symbolDict.Add("R0", 0);
            _symbolDict.Add("R1", 1);
            _symbolDict.Add("R2", 2);
            _symbolDict.Add("R3", 3);
            _symbolDict.Add("R4", 4);
            _symbolDict.Add("R5", 5);
            _symbolDict.Add("R6", 6);
            _symbolDict.Add("R7", 7);
            _symbolDict.Add("R8", 8);
            _symbolDict.Add("R9", 9);
            _symbolDict.Add("R10", 10);
            _symbolDict.Add("R11", 11);
            _symbolDict.Add("R12", 12);
            _symbolDict.Add("R13", 13);
            _symbolDict.Add("R14", 14);
            _symbolDict.Add("R15", 15);

            _symbolDict.Add("SCREEN", 16384);
            _symbolDict.Add("KBD", 24576);

            _symbolDict.Add("SP", 0);
            _symbolDict.Add("LCL", 1);
            _symbolDict.Add("ARG", 2);
            _symbolDict.Add("THIS", 3);
            _symbolDict.Add("THAT", 4);
        }

        private int _currentVariableAddress  = 16;

        public void AddVariable(string symbol)
        {
            if (!_symbolDict.ContainsKey(symbol))
            {
                _symbolDict.Add(symbol, _currentVariableAddress);
                _currentVariableAddress++;
            }
        }

        public void AddLabel(string name, int i)
        {
            if (_symbolDict.ContainsKey(name)) return;
            var index = i - _LabelCount;
            _symbolDict.Add(name, index);
            _LabelCount++;
        }

        public bool Contains(string symbol)
        {
            return _symbolDict.ContainsKey(symbol);
        }

        public int GetAddress(string symbol)
        {
            int value;
            if (_symbolDict.TryGetValue(symbol, out value))
            {
                return value;
            }

            return -1;
        }
    }
}