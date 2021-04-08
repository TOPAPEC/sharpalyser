using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics;
using System.Windows.Media;
using System.Drawing.Imaging;

namespace WindowsFormsApp5
{
    public partial class FormGraph : Form
    {
        // Number of columns.
        List<List<double>> savedData;
        List<string> savedHeaders;
        int linspaceNumber = 32;
        public FormGraph()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Draws graph from one column.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        public void ColumnPlot(List<List<double>> data, List<string> headers)
        {
            labelScale.Show();
            numericUpDownScale.Show();
            savedData = data;
            savedHeaders = headers;
            List<double> linspaced = new List<double>(new double[linspaceNumber]);
            double minVal = data[0].Min();
            double maxVal = data[0].Max();
            List<double> linspacedValues = Generate.LinearSpaced(linspaceNumber, minVal, maxVal).ToList();
            foreach (var dot in data[0])
            {
                for (int j = 0; j < linspacedValues.Count - 1; j++)
                {
                    if (Math.Abs(Math.Abs(dot - linspacedValues[j]) + Math.Abs(linspacedValues[j + 1] - dot) - linspacedValues[1] + linspacedValues[0]) < 0.00001)
                        linspaced[Math.Abs(dot - linspacedValues[j]) < Math.Abs(linspacedValues[j + 1] - dot) ? j : (j + 1)]++;
                }
            }
            cartesianChart.Series = new SeriesCollection();
            cartesianChart.Series.Add(
                new ColumnSeries
                {
                    Title = "Number of " + headers[0],
                    Values = new ChartValues<double>(linspaced)
                });

            cartesianChart.AxisX.Add(new Axis
            {
                Title = "Value",
                Labels = linspacedValues.Select(x => x.ToString("N2")).ToList()
            });

            cartesianChart.AxisY.Add(new Axis
            {
                Title = "Number of items",
                LabelFormatter = value => value.ToString("N")
            });

        }
        /// <summary>
        /// Draws graph from data from rectangle area of table.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rowHeaders"></param>
        /// <param name="colHeaders"></param>
        public void PlotOfRectangle(List<List<double>> data, List<string> rowHeaders, List<string> colHeaders)
        {
            labelScale.Hide();
            numericUpDownScale.Hide();
            cartesianChart.Series = new SeriesCollection();
            for (int i = 0; i < data.Count; i++)
            {
                cartesianChart.Series.Add(
                new ColumnSeries
                {
                    Title = rowHeaders[i],
                    Values = new ChartValues<double>(data[i])
                });
            }


            cartesianChart.AxisX.Add(new Axis
            {
                Title = "Value",
                Labels = colHeaders
            });

            cartesianChart.AxisY.Add(new Axis
            {
                Title = "Number of items",
                LabelFormatter = value => value.ToString("N")
            });

        }
        /// <summary>
        /// Plots X->Y from given data.
        /// </summary>
        /// <param name="valueX"></param>
        /// <param name="valueY"></param>
        public void DrawLineGraph(List<double> valueX, List<double> valueY)
        {

            labelScale.Hide();
            numericUpDownScale.Hide();
            cartesianChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> (valueX)
                },

            };

            cartesianChart.AxisX.Add(new Axis
            {
                Title = "Axis X",
                Labels = valueY.Select(x => x.ToString()).ToList()
            });
            cartesianChart.AxisY.Add(new Axis
            {
                Title = "Axis Y",
                LabelFormatter = value => value.ToString("N")
            });



            cartesianChart.LegendLocation = LegendLocation.Right;

        }
        /// <summary>
        /// Handles change of histogram scale.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericUpDownScale_ValueChanged(object sender, EventArgs e)
        {
            linspaceNumber = 32 / (int)numericUpDownScale.Value;
            cartesianChart.Series.Clear();
            cartesianChart.AxisX.Clear();
            cartesianChart.AxisY.Clear();
            cartesianChart.Refresh();
            ColumnPlot(savedData, savedHeaders);
        }
        /// <summary>
        /// Saves graph.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(cartesianChart.Width, cartesianChart.Height);
                cartesianChart.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                bmp.Save("graph.png", ImageFormat.Png);
                MessageBox.Show("Saved into the application folder.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to save graph. {ex.Message}");
            }
        }
    }
}
