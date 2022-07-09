using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_means_Iris_Flower_Sorting
{
    public class Calculations
    {
        public static void addPositions(double[] pos1, double[] pos2)
        {
            for (int i = 0; i < pos1.Length; i++)
                pos1[i] += pos2[i];
        }

        public static double[] deepSubtractPositions(double[] pos1, double[] pos2)
        {
            double[] newPos = new double[pos1.Length];
            for (int i = 0; i < pos1.Length; i++)
                newPos[i] = pos1[i] - pos2[i];
            return newPos;
        }

        public static void dividePosByInt(double[] pos, int num)
        {
            for (int i = 0; i < pos.Length; i++)
                pos[i] /= num;
        }

        public static double getDistance(double[] pos1, double[] pos2)
        {
            double[] vector = deepSubtractPositions(pos2, pos1);
            double distance = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                distance += Math.Pow(vector[i], 2);
            }
            return Math.Sqrt(distance);
        }

        public static double[] calculateZScores(double[] arr)
        {
            double mean = 0;
            for (int i = 0; i < arr.Length; i++)
                mean += arr[i];
            mean /= arr.Length;

            double sum = 0;
            for (int i = 0; i < arr.Length; i++)
                sum += Math.Pow(arr[i] - mean, 2);
            double SD = Math.Sqrt(sum / arr.Length);

            double[] zScores = new double[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                zScores[i] = (arr[i] - mean) / SD;
            return zScores;
        }

        public static double calculateMedian(double[] arr)
        {
            if (arr.Length % 2 == 0)
                return (arr[arr.Length / 2 - 1] + arr[arr.Length / 2]) / 2;
            else
                return arr[(arr.Length - 1) / 2];
        }
    }
}
