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
            Programa:
            int e, t, tpll, ns, tf, currentTps, nt, ss, sll, sta;
            Random rnd = new Random();

            Console.WriteLine("Ingrese número de extraccionistas: ");
            e = Convert.ToInt32(Console.ReadLine());

            //crear los puestos
            int[] tps = new int[e];
            int[] ito = new int[e];
            int[] sto = new int[e];
            int[] atendidos = new int[e];
            //inicializar todos los tps en hv
            for (int i = 0; i < tps.Length; i++)
            {
                tps[i] = hv;
            }


            t = tpll = 0; tf = 200; ns = nt = 0; ss = sll = sta = 0;

            while (t <= tf)
            {
                simulacion(e, out t, ref tpll, ref ns, out currentTps, ref nt, ref ss, ref sll, ref sta, tps, ito, sto, rnd, atendidos);
            }
            while (ns > 0)
            {
                tpll = hv;
                simulacion(e, out t, ref tpll, ref ns, out currentTps, ref nt, ref ss, ref sll, ref sta, tps, ito, sto, rnd, atendidos);
            }

            Console.WriteLine("Personas totales: " + nt);
            Console.WriteLine("PEC: " + (ss - sll - sta) / nt);
            Console.WriteLine("Atendidos: ");
            foreach (var item in atendidos)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("PTOs: ");
            int promedio = 0;
            foreach (var item in sto)
            {
                promedio += (item * 100) / t;
                Console.WriteLine(item * 100 / t);
            }
            Console.WriteLine("PTOs promedio: " + promedio / sto.Length);
            Console.WriteLine("Duración simulación: " + t);

            if (Console.ReadLine().ToLower() == "y")
            {
                goto Programa;
            }




        }

        private static void simulacion(int e, out int t, ref int tpll, ref int ns, out int currentTps, ref int nt, ref int ss, ref int sll, ref int sta, int[] tps, int[] ito, int[] sto, Random rnd, int[] atendidos)
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
                    int ta = generarTA(rnd);
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
                atendidos[currentTps]++;
            }
            else
            //entrada
            {
                t = tpll;
                tpll = t + generarIA(rnd);
                ns++;
                sll += t;
                if (ns <= e)
                {
                    currentTps = buscarPuesto(tps, sto);
                    sto[currentTps] += t - ito[currentTps];
                    int ta = generarTA(rnd);
                    tps[currentTps] = t + ta;
                    sta += ta;

                }
            }
        }

        private static int generarIA(Random rnd)
        {

            double x = rnd.NextDouble() * (0.975 - 0.01) + 0.01;
            int r = Convert.ToInt32(0.163 / Math.Pow((-1 + Math.Pow(x, (-0.0269811))), 0.8375209380234506));
            Console.WriteLine("IA generado: " + r);
            return r;
        }

        private static int generarTA(Random rnd)
        {

            double x = rnd.NextDouble() * (0.998 - 0.001) + 0.001;
            int r = Convert.ToInt32(Math.Pow(Math.Pow(1 - x, -1.1886) - 1, 0.1961 * 2.1947));
            Console.WriteLine("TA generado: " + r);
            return r+9;
        }

        private static int buscarPuesto(int[] tps, int[] sto)
        {
            if (tps.Length == 0)
            {
                return 0;
            }
            int[]filtrados = tps.Select((s, i) => new { i, s })
                            .Where(e => e.s == hv)
                            .Select(t => t.i)
                            .ToArray();
            int min=hv;
            int final = hv;
            foreach (var item in filtrados)
            {
                if (min>sto[item])
                {
                    min = sto[item];
                    final = item;
                }   
            }

            return final;


        }

        private static int encontrarMenorTps(int[] tps)
        {
            if (tps.Length == 0)
            {
                return 0;
            }
            return Array.IndexOf(tps, tps.Min());
        }
    }
}
