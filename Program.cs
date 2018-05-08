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
            int[] tps = {hv };
            int[] ito = {0 };
            int[] sto = {0 };
            Console.WriteLine("Ingrese número de extraccionistas: ");
            e = Convert.ToInt32(Console.ReadLine());

            t = tpll = 0; tf = 180; ns = nt = 0; ss = sll = sta = 0;

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
                    int ta = generarTA();
                    tps[currentTps] = t + ta;
                    sta += ta;

                }
            }
        }

        private static int generarIA()
        {
            Random rnd = new Random();
            double x = rnd.NextDouble() * (0.975 - 0.01) + 0.01;
            int r = Convert.ToInt32(0.163 / Math.Pow( (-1 + Math.Pow(x, (-0.0269811))) , 0.8375209380234506));
            Console.WriteLine("IA generado: " + r);
            return r;
        }

        private static int generarTA()
        {
            Random rnd = new Random();
            double x = rnd.NextDouble() * (0.998 - 0.001) + 0.001;
            int r = Convert.ToInt32(Math.Pow (Math.Pow(1-x,-1.1886)-1 , 0.1961 * 2.1947));
            Console.WriteLine("TA generado: " + r);
            return r;
        }

        private static int buscarPuesto(int[] tps)
        {
            if (tps.Length == 0)
            {
                return 0;
            }
            return Array.FindIndex(tps, item => item == hv);

        }

        private static int encontrarMenorTps(int[] tps)
        {
            if (tps.Length==0)
            {
                return 0;
            }
            return Array.IndexOf(tps, tps.Min()); ;
        }
    }
}
