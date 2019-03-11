using System.IO;
using System.Linq;

namespace HackAssembler
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string path = @"E:\Learning\Nand2Tetris\nand2tetris\projects\06\pong\Pong.asm";
            var lines = File.ReadAllLines(path).ToList();
            var assembler = new Assembler(lines);
            var result = assembler.Assemble();


            File.WriteAllLines(path + ".hack", result);
        }
    }
}