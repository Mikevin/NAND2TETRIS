﻿using System.Collections.Generic;

namespace HackAssembler
{
    public static class SymbolTable
    {
        private static readonly Dictionary<string, int> SymbolDict = new Dictionary<string, int>();

        static SymbolTable()
        {
            SymbolDict.Add("R0", 0);
            SymbolDict.Add("R1", 1);
            SymbolDict.Add("R2", 2);
            SymbolDict.Add("R3", 3);
            SymbolDict.Add("R4", 4);
            SymbolDict.Add("R5", 5);
            SymbolDict.Add("R6", 6);
            SymbolDict.Add("R7", 7);
            SymbolDict.Add("R8", 8);
            SymbolDict.Add("R9", 9);
            SymbolDict.Add("R10", 10);
            SymbolDict.Add("R11", 11);
            SymbolDict.Add("R12", 12);
            SymbolDict.Add("R13", 13);
            SymbolDict.Add("R14", 14);
            SymbolDict.Add("R15", 15);

            SymbolDict.Add("SCREEN", 16384);
            SymbolDict.Add("KBD", 24576);

            SymbolDict.Add("SP", 0);
            SymbolDict.Add("LCL", 1);
            SymbolDict.Add("ARG", 2);
            SymbolDict.Add("THIS", 3);
            SymbolDict.Add("THAT", 4);
        }

        public static int NextAddress { get; private set; } = 16;

        public static void AddEntry(string symbol, int address)
        {
            if (SymbolDict.ContainsKey(symbol))
            {
                return;
            }

            SymbolDict.Add(symbol, address);

            if (address == NextAddress)
            {
                NextAddress++;
            }
        }

        public static bool Contains(string symbol)
        {
            return SymbolDict.ContainsKey(symbol);
        }

        public static int GetAddress(string symbol)
        {
            int value;
            if (SymbolDict.TryGetValue(symbol, out value))
            {
                return value;
            }

            return -1;
        }
    }
}