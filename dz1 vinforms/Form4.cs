using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace dz1_vinforms
{
    public partial class Form4 : Form
    {
        private Label labelIntersection;
        public Form4()
        {
            InitializeComponent();
            InitializeChart();
            InitializeLabel();
        }
        private void InitializeChart()
        {
            chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart.Dock = DockStyle.Fill;
            chart.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea());
            this.Controls.Add(chart);
        }

        private void InitializeLabel()
        {
            labelIntersection = new Label();
            labelIntersection.AutoSize = true;
            labelIntersection.Location = new Point(10, 10); // Настройте координаты положения метки по вашему усмотрению
            this.Controls.Add(labelIntersection);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Создаем серии данных для графиков
            chart.Series.Add("sin(x)");
            chart.Series.Add("x^2");

            // Задаем типы графиков
            chart.Series["sin(x)"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart.Series["x^2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            // Задаем цвета графиков
            chart.Series["sin(x)"].Color = Color.Blue;
            chart.Series["x^2"].Color = Color.Red;

            // Добавляем точки на графики
            for (double x = -20; x <= 30; x += 0.4)
            {
                chart.Series["sin(x)"].Points.AddXY(x, Math.Sin(x));
                chart.Series["x^2"].Points.AddXY(x, Math.Pow(x, 2));
            }

            // Определяем точку пересечения графиков
            double intersectionX = FindIntersection();
            double intersectionY = Math.Sin(intersectionX);

            // Определяем точку пересечения графиков
            DataPoint intersectionPointSin = chart.Series["sin(x)"].Points.FindByValue(intersectionY);
            DataPoint intersectionPointXSquare = chart.Series["x^2"].Points.FindByValue(intersectionX);

            if (intersectionPointSin != null && intersectionPointXSquare != null)
            {
                // Отмечаем точку пересечения на графиках
                intersectionPointSin.Color = Color.Green;
                intersectionPointXSquare.Color = Color.Green;
            }

            // Выводим координаты точки пересечения
            label1.Text = string.Format("Точка пересечения: ({0}, {1})", intersectionX, intersectionY);

        }

        private double FindIntersection()
        {
            // Определяем точку пересечения графиков методом половинного деления
            double epsilon = 0.0001; // Точность вычислений
            double minX = -20;
            double maxX = 10;

            while (maxX - minX > epsilon)
            {
                double midX = (minX + maxX) / 2;
                double sinValue = Math.Sin(midX);
                double xSquareValue = Math.Pow(midX, 2);

                if (Math.Abs(sinValue - xSquareValue) < epsilon)
                    return midX;

                if (sinValue < xSquareValue)
                    minX = midX;
                else
                    maxX = midX;
            }

            return (minX + maxX) / 2;
        }
    }
}
