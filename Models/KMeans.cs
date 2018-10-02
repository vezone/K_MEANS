using System;
using System.Collections.Generic;
using System.Globalization;

namespace K_Means.Models
{
    public struct Point
    {
            public Point(double[] points)
            {
                Dimension = points.Length;
                Data = new double[points.Length];
                Array.Copy(points, Data, points.Length);
            }
            public Point(Point point)
                : this(point.Data)
            {
            }
            public Point(int dimension)
            {
                Dimension = dimension;
                Data = new double[dimension];
            }
            public Point(string input, char separator = '|')
            {
                string[] points = input.Split(separator);
                if (points.Length > 0)
                {
                    Dimension = points.Length;
                    Data = new double[Dimension];
                    for (int i = 0; i < Dimension; i++)
                    {
                        try
                        {
                            Data[i] = Double.Parse(points[i]);
                        }
                        catch (Exception ex)
                        {
                            //Data[i] = Double.Parse(points[i], NumberFormatInfo.InvariantInfo); // NumberFormatInfo.InvariantInfo
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                else
                {
                    Dimension = 0;
                    Data = new double[0];
                    Console.WriteLine("ctor: null");
                }
            }

            public double[] Data { get; set; }

            public int Dimension { get; set; }

            public double Summary
            {
                get
                {
                    double Summary = 0.0;
                    for (int i = 0; i < Dimension; i++)
                        Summary += Data[i];
                    return Summary;
                }
            }

            public double DistanceEuclide(Point other)
            {
                Point result =
                    (this - other) * (this - other);

                return Math.Sqrt(result.Summary);
            }

            public double DistanceManhattan(Point other)
            {
                double result = 0.0;
                for (int i = 0; i < Dimension; i++)
                    result += Math.Abs(Data[i] - other.Data[i]);

                return result;
            }

            public static Point operator +(
                Point right,
                Point left)
            {
                if (right.Dimension == left.Dimension)
                {

                    double[] data = new double[right.Dimension];
                    for (int i = 0; i < right.Dimension; i++)
                        data[i] += right.Data[i] + left.Data[i];
                    return new Point(data);
                }
                else
                {
                    return right;
                }
            }

            public static Point operator -(
                Point right,
                Point left)
            {
                if (right.Dimension == left.Dimension)
                {

                    double[] data = new double[right.Dimension];
                    for (int i = 0; i < right.Dimension; i++)
                        data[i] += right.Data[i] - left.Data[i];
                    return new Point(data);
                }
                else
                {
                    return right;
                }
            }

            public static Point operator *(
                Point right,
                Point left)
            {
                if (right.Dimension == left.Dimension)
                {

                    double[] data = new double[right.Dimension];
                    for (int i = 0; i < right.Dimension; i++)
                        data[i] += right.Data[i] * left.Data[i];
                    return new Point(data);
                }
                else
                {
                    return right;
                }
            }

            public static Point operator /(
                Point right,
                int val)
            {
                double[] data = new double[right.Dimension];
                for (int i = 0; i < right.Dimension; i++)
                    data[i] += right.Data[i] / val;
                return new Point(data);
            }

            public static Point operator /(
                Point right,
                Point left)
            {
                if (right.Dimension == left.Dimension)
                {

                    double[] data = new double[right.Dimension];
                    for (int i = 0; i < right.Dimension; i++)
                        data[i] += right.Data[i] / left.Data[i];
                    return new Point(data);
                }
                else
                {
                    return right;
                }
            }

            public override string ToString()
            {
                string data = "";
                for (int i = 0; i < Dimension; i++)
                    data += Data[i] + ((i != Dimension - 1) ? "|" : "");
                return data;
            }

            public string ToString2()
            {
                string data = "";
                for (int i = 0; i < Dimension; i++)
                    data += Data[i] + ((i != Dimension - 1) ? ", " : "");
                return data;
            }
            
    }

    public enum MethodeName
    {
        Euclide = 0, Manhattan = 1
    }

    public class KMeans
    {
        static int[] beforeClustering;
        static Point[] rawData;
    
        public static Point[] GetRawData(int mode = 0, string path = "")
        {
            if (mode == 0)
                return rawData;
            else
            {
                string readedString = "";
                if (path.Length > 0)
                    readedString = Input.ReadInput(path);
                else
                    readedString = Input.ReadInput();

                int length = 0;
                foreach (char ch in readedString)
                {
                    if (ch == '\r')
                    {
                        ++length;
                    }
                }

                string[] inputString = new string[length];
                int id = 0;
                string str = "";
                foreach (char ch in readedString)
                {
                    if (ch == '\r')
                    {
                        inputString[id] = str;
                        str = "";
                        ++id;
                    }
                    else
                    {
                        if (ch != '\n')
                            str += ch;
                    }
                }

                Point[] newRawData = new Point[inputString.Length];

                for (int i = 0; i < inputString.Length; i++)
                {
                    newRawData[i] = new Point(inputString[i]);
                }

                return newRawData;
            }
        }

        public static void SetRawData()
        {
            //@"E:\Programming\C_Sharp\K-Means\Additional\input2.txt"
            rawData = GetRawData(1,
                @"E:\General\Programming\C_Sharp\WPF\K-Means\Additional\input2.txt");

            //string input = String.Empty;
            //input = "M = [";
            //for (int i = 0; i < (rawData.Length-1); i++)
            //{
            //    input += $"{rawData[i].ToString2()}; ";
            //}
            //input += $"{rawData[rawData.Length-1].ToString2()};]";

            //Input.CreateInput(input,
            //    @"E:\Programming\C_Sharp\K-Means\Additional\input3.txt");
        }

        public static Output Run(int mode = 0)
        {
            SetRawData();

            int numClusters = 3;
            int[] clustering;

            if (mode == 0)
                clustering = Cluster(rawData, numClusters, MethodeName.Euclide);

            else
                clustering = Cluster(rawData, numClusters, MethodeName.Manhattan);
            
            
            return new Output(beforeClustering, clustering, rawData);
        }

        //K-means
        public static int[] Cluster(
            Point[] rawData,
            int numClusters,
            MethodeName methodeName)
        {
            bool changed = true,
                 success = true;

            Point[] data = rawData;
            int[] clustering = InitClustering(data.Length);
            beforeClustering = new int[clustering.Length];
            Array.Copy(clustering, beforeClustering, clustering.Length);
            Point[] means = Allocate(numClusters);

            int maxCount = 10 * data.Length;
            int iterations = 0;
            while (changed &&
                   success &&
                   iterations < maxCount)
            {
                ++iterations;
                success = UpdateMeans(data, clustering, ref means);
                changed = UpdateClustering(data, ref clustering, means, methodeName);
            }

            return clustering;
        }

        private static int[] InitClustering(
            int Length)
        {
            int[] clustering = new int[Length];

            for (int i = 0; i < (Length / 3); i++)
                clustering[i] = 0;

            for (int i = Length / 3; i < (2 * (Length / 3)); i++)
                clustering[i] = 1;

            for (int i = (2 * (Length / 3)); (i < Length); i++)
                clustering[i] = 2;

            return clustering;
        }

        private static Point[] Allocate(
            int numClusters)
        {
            Point[] result = new Point[numClusters];
            for (int k = 0; k < numClusters; ++k)
                result[k] = new Point();
            return result;
        }

        private static bool UpdateMeans(
            Point[] data,
            int[] clustering,
            ref Point[] means)
        {
            int numClusters = means.Length;
            int[] clusterCounts = new int[numClusters];

            for (int i = 0; i < data.Length; i++)
                ++clusterCounts[clustering[i]]; //cluster = clustering[i]

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false;

            for (int k = 0; k < means.Length; ++k)
                means[k] = new Point(data[0].Dimension);

            for (int i = 0; i < data.Length; i++)
                means[clustering[i]] += data[i];

            for (int k = 0; k < means.Length; ++k)
                means[k] /= clusterCounts[k];

            return true;
        }

        private static bool UpdateClustering(
            Point[] data,
            ref int[] clustering,
            Point[] means,
            MethodeName methodeName)
        {
            if (methodeName == MethodeName.Euclide)
            {
                int numClusters = means.Length;
                bool changed = false;

                int[] newClustering = new int[clustering.Length];
                Array.Copy(clustering,
                           newClustering,
                           clustering.Length);

                double[] distances =
                    new double[numClusters];

                for (int i = 0; i < data.Length; ++i)
                {
                    for (int k = 0; k < numClusters; ++k)
                        distances[k] =
                            data[i].DistanceEuclide(means[k]);

                    if (MinIndex(distances) != newClustering[i])
                    {
                        changed = true;
                        newClustering[i] = MinIndex(distances);
                    }
                }

                if (!changed)
                    return false;

                int[] clusterCounts = new int[numClusters];
                for (int i = 0; i < data.Length; ++i)
                    ++clusterCounts[newClustering[i]];

                for (int k = 0; k < numClusters; ++k)
                    if (clusterCounts[k] == 0)
                        return false;

                Array.Copy(newClustering, clustering,
                    newClustering.Length);

                return true;
            }
            else
            {
                int numClusters = means.Length;
                bool changed = false;

                int[] newClustering = new int[clustering.Length];
                Array.Copy(clustering,
                           newClustering,
                           clustering.Length);

                double[] distances =
                    new double[numClusters];

                for (int i = 0; i < data.Length; ++i)
                {
                    for (int k = 0; k < numClusters; ++k)
                        distances[k] =
                            data[i].DistanceManhattan(means[k]);

                    if (MinIndex(distances) != newClustering[i])
                    {
                        changed = true;
                        newClustering[i] = MinIndex(distances);
                    }
                }

                if (!changed)
                    return false;

                int[] clusterCounts = new int[numClusters];
                for (int i = 0; i < data.Length; ++i)
                    ++clusterCounts[newClustering[i]];

                for (int k = 0; k < numClusters; ++k)
                    if (clusterCounts[k] == 0)
                        return false;

                Array.Copy(newClustering, clustering,
                    newClustering.Length);

                return true;
            }
        }

        private static int MinIndex(
            double[] distance)
        {
            int indexOfMin = 0;
            double smallDist = distance[0];

            for (int k = 1;
                 k < distance.Length;
                 ++k)
            {
                if (distance[k] < smallDist)
                {
                    smallDist = distance[k];
                    indexOfMin = k;
                }
            }

            return indexOfMin;
        }

        private static List<List<Point>> GetClusters(
            Point[] data, int[] clustering)
        {
            List<List<Point>> list = new List<List<Point>>();

            List<Point> cluster0 = new List<Point>();
            List<Point> cluster1 = new List<Point>();
            List<Point> cluster2 = new List<Point>();

            for (int i = 0; i < data.Length; i++)
            {
                if (clustering[i] == 0)
                    cluster0.Add(data[i]);
                else if (clustering[i] == 1)
                    cluster1.Add(data[i]);
                else
                    cluster2.Add(data[i]);
            }

            list.Add(cluster0);
            list.Add(cluster1);
            list.Add(cluster2);

            return list;
        }

    }
    
}