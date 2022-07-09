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

            int kRange = 10;
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

            Console.WriteLine("Deltas");
            double[] deltas = new double[medianSSEs.Length - 1];
            for (int i = 0; i < deltas.Length; i++)
            {
                deltas[i] = medianSSEs[i] - medianSSEs[i + 1];
                Console.WriteLine(deltas[i]);
            }

            //Console.WriteLine();

            //Console.WriteLine("Ratios");
            //double[] ratios = new double[deltas.Length - 1];
            //for (int i = 0; i < ratios.Length; i++)
            //{
            //    ratios[i] = deltas[i] / deltas[i + 1];
            //    Console.WriteLine(ratios[i]);
            //}

            //Choose optimal K

            Console.WriteLine("\nZ-scores");
            double[] zScores = Calculations.calculateZScores(deltas);
            for (int i = 0; i < zScores.Length; i++)
                Console.WriteLine(zScores[i]);
            int optimalId = 0;
            double lowestAbsZScore = zScores[optimalId];
            for (int i = 1; i < zScores.Length; i++)
            {
                if (Math.Abs(zScores[i]) < lowestAbsZScore)
                {
                    optimalId = i;
                    lowestAbsZScore = Math.Abs(zScores[i]);
                }
            }
            int optimalK = optimalId + 2;

            Console.WriteLine("Optimal K = " + optimalK);

            ////some property of elbow curve????? explore!! (Uses mean instead of median though, so may be wrong)

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

            Console.WriteLine("\nFinal Test");
            SortingAlgorithm finalAlgo = new SortingAlgorithm(optimalK, fileName);
            while (Math.Abs(finalAlgo.executeAlgorithm() - medianSSEs[optimalK - 1]) > 0.001)
                finalAlgo = new SortingAlgorithm(optimalK, fileName);
            finalAlgo.checkValidity();
        }
    }
}
