using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_means_Iris_Flower_Sorting
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            string fileName = "IRIS.csv";
            //SortingAlgorithm algo = new SortingAlgorithm(3, fileName);
            //Console.WriteLine("SSE for K = " + 3 + ":       " + algo.executeAlgorithm());

            int kRange = 4;
            double[] medianSSEs = new double[kRange];
            for (int k = 1; k <= kRange; k++)
            {
                double[] store = new double[2000];
                for (int i = 0; i < 2000; i++)
                {
                    SortingAlgorithm algo = new SortingAlgorithm(k, fileName);
                    store[i] = algo.executeAlgorithm();
                }
                Array.Sort(store);
                medianSSEs[k - 1] = Calculations.calculateMedian(store);
                Console.WriteLine("SSE for K = " + k + ":       " + medianSSEs[k - 1]);
            }

            Console.WriteLine();

            //double[] ratios = new double[medianSSEs.Length - 1];
            //for (int i = 0; i < ratios.Length; i++)
            //{
            //    ratios[i] = medianSSEs[i] / medianSSEs[i + 1];
            //    Console.WriteLine(ratios[i]);
            //}

            double[] deltas = new double[medianSSEs.Length - 1];
            for (int i = 0; i < deltas.Length; i++)
            {
                deltas[i] = medianSSEs[i] - medianSSEs[i + 1];
                Console.WriteLine(deltas[i]);
            }

            Console.WriteLine("\nZ-scores");
            double[] zScores = Calculations.calculateZScores(deltas);
            for (int i = 0; i < zScores.Length; i++)
                Console.WriteLine(zScores[i]);

            ////some property of elbow curve????? explore!! [IMPORTANT]

            ///*double[] deltas = new double[kRange - 1];
            //for (int i = 0; i < deltas.Length; i++)
            //{
            //    deltas[i] = averageSSEs[i] - averageSSEs[i + 1];
            //    Console.WriteLine(deltas[i]);
            //}

            //Console.WriteLine();*/

            //double[] ratios = new double[averageSSEs.Length - 1];
            //for (int i = 0; i < ratios.Length; i++)
            //{
            //    ratios[i] = averageSSEs[i] / averageSSEs[i + 1];
            //    Console.WriteLine(ratios[i]);
            //}

            ////Choose optimal K (ex: deltas[0] -> averageSSEs[1] -> K = 2)
        }
    }
}
