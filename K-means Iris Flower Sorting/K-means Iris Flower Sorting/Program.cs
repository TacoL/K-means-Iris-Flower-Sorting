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
            int kRange = 5;
            for (int k = 1; k <= kRange; k++)
            {
                SortingAlgorithm algo = new SortingAlgorithm(k, fileName);
                Console.WriteLine("WSS for K = " + k + ":       " + algo.executeAlgorithm());
            }
        }
    }
}
