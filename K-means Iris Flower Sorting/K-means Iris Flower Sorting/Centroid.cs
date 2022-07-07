using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_means_Iris_Flower_Sorting
{
    public class Centroid
    {
        private double[] pos;
        private int centroidValue;

        public Centroid(double[] pos, int centroidValue)
        {
            this.pos = pos;
            this.centroidValue = centroidValue;
        }

        public List<DataPoint> getCorrespondingDataPoints(List<DataPoint> dataPoints)
        {
            List<DataPoint> correspondingPoints = new List<DataPoint>();
            foreach (DataPoint point in dataPoints) {
                if (point.getClassification() == centroidValue)
                    correspondingPoints.Add(point);
            }
            return correspondingPoints;
        }
        public void assignMeanPos(List<DataPoint> dataPoints)
        {
            List<DataPoint> correspondingPoints = getCorrespondingDataPoints(dataPoints);
            double[] meanPos = new double[dataPoints[0].getPosition().Length];
            foreach (DataPoint point in correspondingPoints)
            {
                Calculations.addPositions(meanPos, point.getPosition());
            }
            Calculations.dividePosByInt(meanPos, correspondingPoints.Count);
            pos = meanPos;
        }

        public int getCentroidValue()
        {
            return centroidValue;
        }

        public double[] getPosition()
        {
            return pos;
        }
    }
}
