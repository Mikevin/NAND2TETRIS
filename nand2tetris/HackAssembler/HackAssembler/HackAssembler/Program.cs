using System.IO;

namespace HackAssembler
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var path = @"E:\Learning\Nand2Tetris\nand2tetris\projects\06\pong\Pong.asm";

            var main = new Main(path);
            var result = main.Translate();

            using (var writer =
                new StreamWriter(path + ".hack"))
            {
                foreach (var line in result)
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}