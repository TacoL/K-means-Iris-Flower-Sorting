using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace K_means_Iris_Flower_Sorting
{
    public class SortingAlgorithm
    {
        public int numClusters;
        private List<DataPoint> dataPoints = new List<DataPoint>();
        private List<Centroid> centroids = new List<Centroid>();
        public static Random r = new Random();
        public double maxError = 0.01;

        public SortingAlgorithm(int numClusters, String fileName)
        {
            StreamReader sr = new StreamReader(File.OpenRead(@fileName));
            String line = sr.ReadLine(); //skips first line

            double[] minPos = { Double.MaxValue, Double.MaxValue, Double.MaxValue, Double.MaxValue };
            double[] maxPos = { Double.MinValue, Double.MinValue, Double.MinValue, Double.MinValue };

            while ((line = sr.ReadLine()) != null)
            {
                String[] dividedString = line.Split(',');
                double[] pos = new double[4]; //change 4 to dividedString.length

                for (int i = 0; i < dividedString.Length; i++)
                {
                    if (i < 4) //inputs. Don't need this conditional when data is only inputs (pos for points)
                    {
                        pos[i] = Double.Parse(dividedString[i]);
                        if (pos[i] < minPos[i])
                            minPos[i] = pos[i];
                        if (pos[i] > maxPos[i])
                            maxPos[i] = pos[i];
                    }
                }
                dataPoints.Add(new DataPoint(pos, dividedString[4])); //second paramater for validity purposes
            }
            sr.Close();

            //create and randomize centroid positions
            for (int clusterId = 0; clusterId < numClusters; clusterId++)
            {
                int numInputsPerPoint = 4; //define up there when i get the chance
                double[] pos = new double[numInputsPerPoint];
                for (int i = 0; i < numInputsPerPoint; i++)
                    pos[i] = minPos[i] + r.NextDouble() * (maxPos[i] - minPos[i]);
                centroids.Add(new Centroid(pos, clusterId));
            }
        }

        public void updateCentroids()
        {
            dataPoints.ForEach(point => point.classifyDataPoint(centroids));
            centroids.ForEach(centroid => centroid.assignMeanPos(dataPoints));
        }

        public double executeAlgorithm()
        {
            //do the thing
            updateCentroids();
            double sumDeltaCentroidPos = 0;
            centroids.ForEach(centroid => sumDeltaCentroidPos += centroid.getDeltaPos());
            while (sumDeltaCentroidPos > maxError)
            {
                updateCentroids();
                sumDeltaCentroidPos = 0;
                centroids.ForEach(centroid => sumDeltaCentroidPos += centroid.getDeltaPos());
            }

            //check validity
            for (int centroidId = 0; centroidId < centroids.Count; centroidId++)
            {
                List<DataPoint> correspondingPoints = centroids[centroidId].getCorrespondingDataPoints(dataPoints);
                //Console.WriteLine("CENTROID " + centroidId);
                foreach (DataPoint point in correspondingPoints)
                {
                    //Console.WriteLine(point.getTrueIdentity());
                }
            }

            //calculate SSE
            double SSE = 0;
            dataPoints.ForEach(dataPoint => SSE += Math.Pow(dataPoint.distanceFromCentroid(centroids), 2));
            return SSE;
        }
    }
}
