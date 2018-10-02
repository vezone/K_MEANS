namespace K_Means.Models
{
    public class Output
    {
        int[] m_ClustersBefore = { };
        int[] m_ClustersAfter = { };
        Point[] m_RawData = { };
        Point[] m_MeansBefore;
        Point[] m_MeansAfter;

        public int[] ClustersBefore => m_ClustersBefore;
        public int[] ClustersAfter  => m_ClustersAfter;
        public Point[] RawData      => m_RawData;
        public int Length           => m_RawData.Length;

        public Point[] MeansBefore  => m_MeansBefore;
        public Point[] MeansAfter   => m_MeansAfter;

        public void SetMeansBefore()
        {
            m_MeansBefore = new Point[3] {
                    new Point(new double[] { 0, 0, 0 }),
                    new Point(new double[] { 0, 0, 0 }),
                    new Point(new double[] { 0, 0, 0 })
            };

            int[] counter = new int[] { 0, 0, 0 };
            for (int i = 0; i < m_RawData.Length; i++)
            {
                if (ClustersBefore[i] == 0)
                {
                    m_MeansBefore[0] += m_RawData[i];
                    ++counter[0];
                }
                else if (ClustersBefore[i] == 1)
                {
                    m_MeansBefore[1] += m_RawData[i];
                    ++counter[1];
                }
                else
                {
                    m_MeansBefore[2] += m_RawData[i];
                    ++counter[2];
                }
            }
            m_MeansBefore[0] /= counter[0];
            m_MeansBefore[1] /= counter[1];
            m_MeansBefore[2] /= counter[2];
        }    

        public void SetMeansAfter()
        {
            m_MeansAfter = new Point[3] {
                    new Point(new double[] { 0, 0, 0 }),
                    new Point(new double[] { 0, 0, 0 }),
                    new Point(new double[] { 0, 0, 0 })
             };

            int[] counter = new int[] { 0, 0, 0 };
            for (int i = 0; i < m_RawData.Length; i++)
            {
                if (ClustersAfter[i] == 0)
                {
                    m_MeansAfter[0] += m_RawData[i];
                    ++counter[0];
                }
                else if (ClustersAfter[i] == 1)
                {
                    m_MeansAfter[1] += m_RawData[i];
                    ++counter[1];
                }
                else
                {
                    m_MeansAfter[2] += m_RawData[i];
                    ++counter[2];
                }
            }
            m_MeansAfter[0] /= counter[0];
            m_MeansAfter[1] /= counter[1];
            m_MeansAfter[2] /= counter[2];
        }

        private Output() {}

        public Output(int[] clustersBefore, int[] clustersAfter, Point[] rawData)
        {
            m_ClustersBefore = new int[clustersBefore.Length];
            m_ClustersAfter = new int[clustersAfter.Length];
            m_RawData = new Point[rawData.Length];
            System.Array.Copy(clustersBefore, m_ClustersBefore, clustersBefore.Length);
            System.Array.Copy(clustersAfter, m_ClustersAfter, clustersAfter.Length);
            System.Array.Copy(rawData, m_RawData, rawData.Length);
            SetMeansBefore();
            SetMeansAfter();
        }

    }
}
