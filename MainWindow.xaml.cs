//#define NUI

namespace K_Means
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private SciChart.Charting3D.Model.XyzDataSeries3D<double> m_ScatterDataBeforeE;
        private SciChart.Charting3D.Model.XyzDataSeries3D<double> m_ScatterDataAfterE;
        private SciChart.Charting3D.Model.XyzDataSeries3D<double> m_ScatterDataBeforeM;
        private SciChart.Charting3D.Model.XyzDataSeries3D<double> m_ScatterDataAfterM;

        public MainWindow()
        {
#if NUI
            Models.Output output = Models.KMeans.Run();
#else
            InitializeComponent();
            Loaded += OnLoaded;
#endif

        }
        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            btnBack_Click(sender, e);
        }

        private void btnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((m_ScatterDataBeforeE == null) && 
                (m_ScatterDataAfterE  == null))
            {
                // Create XyDataSeries to host data for our charts
                m_ScatterDataBeforeE = new SciChart.Charting3D.Model.XyzDataSeries3D<double>();
                m_ScatterDataAfterE  = new SciChart.Charting3D.Model.XyzDataSeries3D<double>();
                Models.Output output = Models.KMeans.Run();

                System.Windows.Media.Color colorBefore, colorAfter, colorMeans;
                for (int i = 0; i < output.Length; i++)
                {

                    if (output.ClustersBefore[i] == 0)
                        colorBefore = System.Windows.Media.Colors.Blue;
                    else if (output.ClustersBefore[i] == 1)
                        colorBefore = System.Windows.Media.Colors.Red;
                    else
                        colorBefore = System.Windows.Media.Colors.Yellow;

                    if (output.ClustersAfter[i] == 0)
                        colorAfter = System.Windows.Media.Colors.Blue;
                    else if (output.ClustersAfter[i] == 1)
                        colorAfter = System.Windows.Media.Colors.Red;
                    else
                        colorAfter = System.Windows.Media.Colors.Yellow;

                    m_ScatterDataBeforeE.Append(output.RawData[i].Data[0],
                            output.RawData[i].Data[1],
                            output.RawData[i].Data[2],
                            new SciChart.Charting3D.Model.PointMetadata3D(colorBefore, 1.0f));

                    m_ScatterDataAfterE.Append(output.RawData[i].Data[0],
                            output.RawData[i].Data[1],
                            output.RawData[i].Data[2],
                            new SciChart.Charting3D.Model.PointMetadata3D(colorAfter, 1.0f));
                }

                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                        colorMeans = System.Windows.Media.Colors.DarkBlue;
                    else if (i == 1)
                        colorMeans = System.Windows.Media.Colors.DarkRed;
                    else
                        colorMeans = System.Windows.Media.Colors.LightYellow;

                    m_ScatterDataBeforeE.Append(
                        output.MeansBefore[i].Data[0],
                        output.MeansBefore[i].Data[1],
                        output.MeansBefore[i].Data[2],
                        new SciChart.Charting3D.Model.PointMetadata3D(colorMeans, 1.0f));
                    m_ScatterDataAfterE.Append(
                        output.MeansAfter[i].Data[0],
                        output.MeansAfter[i].Data[1],
                        output.MeansAfter[i].Data[2],
                        new SciChart.Charting3D.Model.PointMetadata3D(colorMeans, 1.0f));
                }

                output = null;

            }
            // Assign dataseries to RenderSeries
            ScatterSeries3DBefore.DataSeries = m_ScatterDataBeforeE;
            ScatterSeries3DAfter.DataSeries = m_ScatterDataAfterE;
            TextBlockName.Text = "Euclide";
        }

        private void btnForward_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if ((m_ScatterDataBeforeM == null) &&
                (m_ScatterDataAfterM  == null))
            {
                // Create XyDataSeries to host data for our charts
                m_ScatterDataBeforeM = new SciChart.Charting3D.Model.XyzDataSeries3D<double>();
                m_ScatterDataAfterM  = new SciChart.Charting3D.Model.XyzDataSeries3D<double>();
                Models.Output output = Models.KMeans.Run(1);

                System.Windows.Media.Color colorBefore, colorAfter, colorMeans;
                for (int i = 0; i < output.Length; i++)
                {

                    if (output.ClustersBefore[i] == 0)
                        colorBefore = System.Windows.Media.Colors.Blue;
                    else if (output.ClustersBefore[i] == 1)
                        colorBefore = System.Windows.Media.Colors.Red;
                    else
                        colorBefore = System.Windows.Media.Colors.Yellow;

                    if (output.ClustersAfter[i] == 0)
                        colorAfter = System.Windows.Media.Colors.Blue;
                    else if (output.ClustersAfter[i] == 1)
                        colorAfter = System.Windows.Media.Colors.Red;
                    else
                        colorAfter = System.Windows.Media.Colors.Yellow;

                    m_ScatterDataBeforeM.Append(output.RawData[i].Data[0],
                            output.RawData[i].Data[1],
                            output.RawData[i].Data[2],
                            new SciChart.Charting3D.Model.PointMetadata3D(colorBefore, 1.0f));

                    m_ScatterDataAfterM.Append(output.RawData[i].Data[0],
                            output.RawData[i].Data[1],
                            output.RawData[i].Data[2],
                            new SciChart.Charting3D.Model.PointMetadata3D(colorAfter, 1.0f));
                }

                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                        colorMeans = System.Windows.Media.Colors.DarkBlue;
                    else if (i == 1)
                        colorMeans = System.Windows.Media.Colors.DarkRed;
                    else
                        colorMeans = System.Windows.Media.Colors.LightYellow;

                    m_ScatterDataBeforeM.Append(
                        output.MeansBefore[i].Data[0],
                        output.MeansBefore[i].Data[1],
                        output.MeansBefore[i].Data[2],
                        new SciChart.Charting3D.Model.PointMetadata3D(colorMeans, 1.0f));
                    m_ScatterDataAfterM.Append(
                        output.MeansAfter[i].Data[0],
                        output.MeansAfter[i].Data[1],
                        output.MeansAfter[i].Data[2],
                        new SciChart.Charting3D.Model.PointMetadata3D(colorMeans, 1.0f));
                }

                output = null;

            }

            // Assign dataseries to RenderSeries
            ScatterSeries3DBefore.DataSeries = m_ScatterDataBeforeM;
            ScatterSeries3DAfter.DataSeries = m_ScatterDataAfterM;
            TextBlockName.Text = "Manhattan";
        }
    }

}
