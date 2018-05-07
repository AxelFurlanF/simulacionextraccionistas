using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacionExtracciones
{
    class Program
    {
        private const int hv = 100000000;

        static void Main(string[] args)
        {
            int e, t, tpll, ns, tf, currentTps, nt;
            int[] tps = { };
            int[] ito= { };
            int[] sto= { };
            Console.WriteLine("Ingrese número de extraccionistas: ");
            e = Convert.ToInt32(Console.ReadLine());

            t = tpll = 8; tf = 11; ns = nt = 0;
            //hasta que no quede nadie
            do
            {

                //hasta que se acabe la hora
                while (t <= tf)
                {
                    currentTps = encontrarMenorTps(tps);
                    //proximo evento

                    //salida
                    if (tps[currentTps] < tpll)
                    {
                        t = tps[currentTps];
                        ns--;
                        if (ns >= e)
                        {
                            tps[currentTps] = t + generarTA();
                        }
                        else
                        {
                            ito[currentTps] = t;
                            tps[currentTps] = hv;
                        }
                        nt++;
                    }
                    else
                    //entrada
                    {
                        t = tpll;
                        tpll = t + generarIA();

                        if (ns <= e)
                        {
                            currentTps = buscarPuesto(tps);
                            sto[currentTps] += t - ito[currentTps];
                            tps[currentTps] = t + generarTA();

                        }
                    }
                }
                Console.WriteLine(currentTps);
                Console.ReadKey();
            } while (ns > 0);
        }

        private static int buscarPuesto(int[] tps)
        {
            return Array.FindIndex(tps, item => item == hv);

        }

        private static int encontrarMenorTps(int[] tps)
        {
            return Array.IndexOf(tps, tps.Min()); ;
        }
    }
}
