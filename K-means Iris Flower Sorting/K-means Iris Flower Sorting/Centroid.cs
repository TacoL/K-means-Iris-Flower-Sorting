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
        private double[] lastPos;
        private int centroidId;

        public Centroid(double[] pos, int centroidId)
        {
            this.pos = pos;
            lastPos = pos; //failsafe
            this.centroidId = centroidId;
        }

        public List<DataPoint> getCorrespondingDataPoints(List<DataPoint> dataPoints)
        {
            List<DataPoint> correspondingPoints = new List<DataPoint>();
            foreach (DataPoint point in dataPoints) {
                if (point.getClassification() == centroidId)
                    correspondingPoints.Add(point);
            }
            return correspondingPoints;
        }
        public void assignMeanPos(List<DataPoint> dataPoints)
        {
            List<DataPoint> correspondingPoints = getCorrespondingDataPoints(dataPoints);
            double[] meanPos = new double[dataPoints[0].getPosition().Length];
            foreach (DataPoint point in correspondingPoints)
                Calculations.addPositions(meanPos, point.getPosition());
            Calculations.dividePosByInt(meanPos, correspondingPoints.Count);
            lastPos = pos;
            pos = meanPos;
        }

        public int getCentroidId()
        {
            return centroidId;
        }

        public double[] getPosition()
        {
            return pos;
        }

        public double[] getLastPosition()
        {
            return lastPos;
        }

        public double getDeltaPos()
        {
            return Calculations.getDistance(lastPos, pos);
        }
    }
}
