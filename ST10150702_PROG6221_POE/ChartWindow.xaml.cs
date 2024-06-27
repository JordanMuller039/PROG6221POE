using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ST10150702_PROG6221_POE
{
    public partial class ChartWindow : Window
    {
        private Dictionary<string, int> foodGroupCounts;

        public ChartWindow()
        {
            InitializeComponent();
        }

        public void SetFoodGroupCounts(Dictionary<string, int> counts)
        {
            foodGroupCounts = counts;
        }

        private void PieChartCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            ShowPieChart();
        }

        public void ShowPieChart()
        {
            PieChartCanvas.Children.Clear();

            if (foodGroupCounts == null || foodGroupCounts.Count == 0)
            {
                MessageBox.Show("No data to display in pie chart.");
                return;
            }

            double total = 0;
            foreach (var count in foodGroupCounts.Values)
            {
                total += count;
            }

            double angle = 0;
            double centerX = PieChartCanvas.ActualWidth / 2;
            double centerY = PieChartCanvas.ActualHeight / 2;
            double radius = Math.Min(centerX, centerY) - 10;

            if (centerX == 0 || centerY == 0)
            {
                MessageBox.Show("Canvas dimensions are zero. Please check the Canvas size.");
                return;
            }

            Random random = new Random();
            foreach (var entry in foodGroupCounts)
            {
                double slicePercentage = entry.Value / total;
                double sliceAngle = slicePercentage * 360;

                PathFigure figure = new PathFigure();
                figure.StartPoint = new Point(centerX, centerY);

                double endAngle = angle + sliceAngle;
                bool isLargeArc = sliceAngle > 180.0;

                Point startPoint = new Point(
                    centerX + radius * Math.Cos(Math.PI * angle / 180),
                    centerY + radius * Math.Sin(Math.PI * angle / 180));

                Point endPoint = new Point(
                    centerX + radius * Math.Cos(Math.PI * endAngle / 180),
                    centerY + radius * Math.Sin(Math.PI * endAngle / 180));

                figure.Segments.Add(new LineSegment(startPoint, true));
                figure.Segments.Add(new ArcSegment(
                    endPoint,
                    new Size(radius, radius),
                    0,
                    isLargeArc,
                    SweepDirection.Clockwise,
                    true));
                figure.Segments.Add(new LineSegment(new Point(centerX, centerY), true));

                PathGeometry geometry = new PathGeometry();
                geometry.Figures.Add(figure);

                Path path = new Path();
                path.Fill = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)));
                path.Data = geometry;

                PieChartCanvas.Children.Add(path);

                angle = endAngle;
            }
        }
    }
}
