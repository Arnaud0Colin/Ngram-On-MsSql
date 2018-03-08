using Ngram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTest
{
    class Program
    {
        static void Write(string Sample, string Cible)
        {
            float[] result = CalculDistance.IndiceSimilarite(Sample, Cible, 1, true, 1, StringComparison.InvariantCultureIgnoreCase);
            Console.WriteLine($"{Sample} <=> {Cible}");
            for(int i =0; i< result.Length; i++)
            {
                Console.WriteLine($"{((CalculDistance.eIndiceSimilarite)i).ToString().PadRight(15)} = {result[i]*100:F2}%");
            }

            Console.WriteLine();
        }


        static void Main(string[] args)
        {

            Write("la crise economique", "le scenario comique");

            Write("rehcehcre","recherche");

            Write("Avenue denis papin", "Avenue denis papin");

            //Write("Place de l'eglise", "Rue paipn denis ");

            Console.ReadKey();
        }
    }
}
