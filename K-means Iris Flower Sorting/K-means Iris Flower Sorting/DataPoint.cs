using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_means_Iris_Flower_Sorting
{
    public class DataPoint
    {
        private double[] pos; //consider making it const??
        private int classifiedAs;

        public DataPoint(double[] pos)
        {
            this.pos = pos;
        }

        //methods

        public void classifyDataPoint(List<Centroid> centroids)
        {
            Centroid centroidOfLowestDistance = centroids[0];
            double lowestDistance = Calculations.getDistance(pos, centroidOfLowestDistance.getPosition());
            foreach (Centroid centroid in centroids)
            {
                double distance = Calculations.getDistance(pos, centroidOfLowestDistance.getPosition());
                if (distance < lowestDistance)
                {
                    lowestDistance = distance;
                    centroidOfLowestDistance = centroid;
                }
            }

            classifiedAs = centroidOfLowestDistance.getCentroidId();
        }

        public double[] getPosition()
        {
            return pos;
        }

        public int getClassification()
        {
            return classifiedAs;
        }

        public double distanceFromCentroid(List<Centroid> centroids)
        {
            return Calculations.getDistance(pos, centroids[classifiedAs].getPosition());
        }

        /*public static DataPoint operator +(DataPoint a, DataPoint b)
        {
            double[] newPos = new double[a.pos.Length];
            for (int i = 0; i < a.pos.Length; i++)
                newPos[i] = a.pos[i] += b.pos[i];
            return new DataPoint(newPos);
        }*/
    }
}
