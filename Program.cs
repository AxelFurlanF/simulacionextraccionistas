using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionExtracciones
{
    class Program
    {
        static void Main(string[] args)
        {
            int e, t, tpll, ns, tf, currentTps;
            int[] tps = { 1, 2, 3, 4 };
            Console.WriteLine("Ingrese número de extraccionistas: ");
            e = Convert.ToInt32(Console.ReadLine());

            t=tpll = 8; tf = 11; ns = 0;
            while (t <= tf)
            {
                currentTps = encontrarMenorTps(tps);
                if (tps[currentTps] < tpll)
                {
                    t = tps[currentTps];
                    ns--;
                    if (ns >= e)
                    {
                        tps[currentTps] = t + generarTA();

                    }

                }
                Console.WriteLine(currentTps);
                Console.ReadKey();
            }
        }

        private static int encontrarMenorTps(int[] tps)
        {
            return Array.IndexOf(tps, tps.Min()); ;
        }
    }
}
