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

            int kRange = 5;
            double[] averageSSEs = new double[kRange];
            for (int k = 1; k <= kRange; k++)
            {
                double avgSSE = 0;
                for (int i = 0; i < 2000; i++)
                {
                    SortingAlgorithm algo = new SortingAlgorithm(k, fileName);
                    avgSSE += algo.executeAlgorithm();
                }
                averageSSEs[k - 1] = avgSSE / 2000;
                Console.WriteLine("SSE for K = " + k + ":       " + (avgSSE / 2000));
            }

            Console.WriteLine();

            double[] deltas = new double[kRange - 1];
            double meanDelta = 0;
            for (int i = 0; i < deltas.Length; i++)
            {
                deltas[i] = averageSSEs[i] - averageSSEs[i + 1];
                meanDelta += deltas[i];
                Console.WriteLine(deltas[i]);
            }
            meanDelta /= deltas.Length;

            //test
            Console.WriteLine();
            double[] accelerations = new double[deltas.Length - 1];
            double meanAccel = 0;
            for (int i = 0; i < accelerations.Length; i++)
            {
                accelerations[i] = deltas[i] - deltas[i + 1];
                meanAccel += accelerations[i];
                Console.WriteLine(accelerations[i]);
            }
            meanAccel /= accelerations.Length;
            //end test

            //Choose optimal K (ex: deltas[0] -> averageSSEs[1] -> K = 2)
            int deltasOptimalIdx = 0;
            for (int i = 0; i < deltas.Length; i++)
            {
                if (deltas[i] > meanDelta)
                    deltasOptimalIdx = i;
            }
        }
    }
}
