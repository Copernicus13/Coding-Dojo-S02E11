using CodingDojo.S02E11.PCR;
using System;

namespace CodingDojo.S02E11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var solver = new ElectricianPuzzle();
            solver.ParseInputFromConsole();
            var result = solver.ComputeSolution();
            Console.WriteLine("Le résultat est : {0}", result);

            Console.Write("Appuyez sur une touche pour continuer…");
            Console.ReadKey();
        }
    }
}
