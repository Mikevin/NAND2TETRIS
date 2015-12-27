using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAssembler
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\Learning\Nand2Tetris\nand2tetris\projects\06\max\Max.asm";

            var main = new Main(path);
            var result = main.Translate();

            using (StreamWriter writer =
                new StreamWriter(path + ".hack"))
            {
                foreach (string line in result)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}
