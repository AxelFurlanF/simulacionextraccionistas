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
            int e, t, tpll, ns, tf, currentTps, nt, ss, sll, sta;
            int[] tps = { };
            int[] ito = { };
            int[] sto = { };
            Console.WriteLine("Ingrese número de extraccionistas: ");
            e = Convert.ToInt32(Console.ReadLine());

            t = tpll = 8; tf = 11; ns = nt = 0; ss = sll = sta = 0;

            while (t <= tf)
            {
                simulacion(e, out t, ref tpll, ref ns, out currentTps, ref nt, ref ss, ref sll, ref sta, tps, ito, sto);
            }
            while (ns > 0)
            {
                tpll = hv;
                simulacion(e, out t, ref tpll, ref ns, out currentTps, ref nt, ref ss, ref sll, ref sta, tps, ito, sto);
            }

            Console.WriteLine("PEC: "+ (ss-sll-sta)/nt);
            Console.WriteLine("PTOs: ");
            int promedio=0;
            foreach (var item in sto)
            {
                promedio+= (item * 100) / t;
                Console.WriteLine(item * 100 / t);
            }
            Console.WriteLine("PTOs promedio: "+ promedio/sto.Length);
            Console.ReadKey();



        }

        private static void simulacion(int e, out int t, ref int tpll, ref int ns, out int currentTps, ref int nt, ref int ss, ref int sll, ref int sta, int[] tps, int[] ito, int[] sto)
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
                    int ta = generarTA();
                    tps[currentTps] = t + ta;
                    sta += ta;
                }
                else
                {
                    ito[currentTps] = t;
                    tps[currentTps] = hv;
                }
                ss += t;
                nt++;
            }
            else
            //entrada
            {
                t = tpll;
                tpll = t + generarIA();
                ns++;
                sll += t;
                if (ns <= e)
                {
                    currentTps = buscarPuesto(tps);
                    sto[currentTps] += t - ito[currentTps];
                    int ta = generarTA;
                    tps[currentTps] = t + ta;
                    sta += ta;

                }
            }
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
