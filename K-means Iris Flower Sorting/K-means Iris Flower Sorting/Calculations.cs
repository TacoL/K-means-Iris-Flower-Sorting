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
    }
}
