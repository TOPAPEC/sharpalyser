using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using LiveCharts; //Core of the library
using LiveCharts.Wpf; //The WPF controls
using LiveCharts.WinForms; //the WinForm wrappers
using System.Threading;
using System.Globalization;

namespace WindowsFormsApp5
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            MessageBox.Show($"Welcome.{Environment.NewLine}" +
                $"You can get additional info if you place cursor over graph elements.{Environment.NewLine}" +
                $"NumericUpDown appears in histograms for single columns, but all the graphs has save button.{Environment.NewLine}" +
                $"You can plot data by selecting it and then choosing type of plot.{Environment.NewLine}" +
                $"X->Y chooses first selected column as X and second as Y.{Environment.NewLine}" +
                $"Histogram rectangle plots selected rectangle area of the table.{Environment.NewLine}" +
                $"You can plot only real numbers the same true for statistical functions.");
            InitializeComponent();
        }
        Csv openTable;
        private void ButtonLoadCsv_Click(object sender, EventArgs e)
        {
            openTable = Csv.CsvFromFile(textBoxCsvPath.Text);
            LoadTable();
        }
        /// <summary>
        /// Loads table from openTable.
        /// </summary>
        private void LoadTable()
        {
            try
            {
                // Clears datagrid's content.
                dataGridView.Rows.Clear();
                dataGridView.Columns.Clear();
                dataGridView.Refresh();
                dataGridView.AllowUserToAddRows = true;
                dataGridView.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                (int rows, int cols) = openTable.Shape;
                foreach (var name in openTable.Header)
                {
                    dataGridView.Columns.Add(name, name);
                }
                for (var i = 0; i < rows; i++)
                {
                    var row = (DataGridViewRow)dataGridView.Rows[0].Clone();
                    for (var j = 0; j < cols; j++)
                        row.Cells[j].Value = openTable[i, j];
                    dataGridView.Rows.Add(row);
                }
                foreach (DataGridViewColumn c in dataGridView.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                    c.Selected = false;
                }
                dataGridView.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                dataGridView.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured while trying to load table for display. {ex.Message}");
            }
        }

        /// <summary>
        /// Draws diagram with rectangle area of the table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemHistogramRectangle_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedCellCount =
                    dataGridView.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                    CheckIfSelectedAreaIsRectangle();
                else
                    throw new ArgumentException("No cells are selected.");
                // Making list of indexes.
                List<int> rows = dataGridView.SelectedCells.Cast<DataGridViewCell>().Select(x => x.RowIndex).Distinct().ToList();
                List<int> cols = dataGridView.SelectedCells.Cast<DataGridViewCell>().Select(x => x.ColumnIndex).Distinct().ToList();
                List<string> rowNames = new List<string>(new string[rows.Count]);
                List<string> colNames = new List<string>(new string[cols.Count]);
                rows.Sort();
                cols.Sort();
                // Getting column and row names.
                // I've chosen to fill rows with indexes as csv files rarely have fancy row headers. 
                rowNames = rowNames.Select((x, index) => x = "Row" + rows[index].ToString()).ToList();
                colNames = colNames.Select((x, index) => x = (string)dataGridView.Columns[cols[index]].HeaderCell.Value).ToList();
                List<List<(int, double)>> rowValuesInd;
                rowValuesInd = new List<List<(int, double)>>(new List<(int, double)>[rows.Count]);
                int iRowMin = dataGridView.SelectedCells.Cast<DataGridViewCell>().Min(x => x.RowIndex);
                // Prarsing cells' data.
                foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                {
                    if (rowValuesInd[cell.RowIndex - iRowMin] == null)
                        rowValuesInd[cell.RowIndex - iRowMin] = new List<(int, double)>();
                    rowValuesInd[cell.RowIndex - iRowMin].Add((cell.ColumnIndex, double.Parse(cell.Value as string)));
                }
                foreach (var row in rowValuesInd)
                    row.Sort();
                var rowValues = rowValuesInd.Select(x => x.Select(el => (double)el.Item2).ToList()).ToList();
                FormGraph newGraph = new FormGraph();
                newGraph.PlotOfRectangle(rowValues, rowNames, colNames);
                newGraph.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to make histogram. {ex.Message}");
            }

        }
        /// <summary>
        /// Checks if selected area is rectangle.
        /// </summary>
        private void CheckIfSelectedAreaIsRectangle()
        {
            // In order to find out if the selection is rectangle we calculate min and max indexes for col and rows.
            int minRowIndex = 1000000000 + 7;
            int maxRowIndex = 0;
            int minColIndex = 1000000000 + 7;
            int maxColIndex = 0;
            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
            {
                //MessageBox.Show(cell.GetType().ToString());
                minRowIndex = Math.Min(minRowIndex, cell.RowIndex);
                maxRowIndex = Math.Max(maxRowIndex, cell.RowIndex);
                minColIndex = Math.Min(minColIndex, cell.ColumnIndex);
                maxColIndex = Math.Max(maxColIndex, cell.ColumnIndex);
            }
            if (dataGridView.SelectedCells.Count != (maxColIndex - minColIndex + 1) * (maxRowIndex - minRowIndex + 1))
                throw new ArgumentException("Selected area is not a rectangle!");
        }
        /// <summary>
        /// Draws histogram from selected table column.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemHistCol_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                CheckIfSelectedAreaContainsOnlyOneColumn();
                var selectedColumnCount =
                    dataGridView.Columns.GetColumnCount(DataGridViewElementStates.Selected);
                if (selectedColumnCount > 0)
                {
                    List<List<double>> values = new List<List<double>>();
                    List<string> headers = new List<string>();
                    foreach (DataGridViewColumn column in dataGridView.SelectedColumns)
                    {
                        values.Add(new List<double>());
                        headers.Add(column.Name);
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            if ((string)row.Cells[column.Index].Value == column.Name)
                                continue;
                            values.Last().Add(double.Parse((string)row.Cells[column.Index].Value));
                        }
                    }
                    FormGraph newGraph = new FormGraph();
                    newGraph.ColumnPlot(values, headers);
                    newGraph.Show();
                }
                else
                    throw new ArgumentException("No columns are selected.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to make histogram. {ex.Message}");
            }
        }
        /// <summary>
        /// Checks if exactly one column on the table is selected;
        /// </summary>
        private void CheckIfSelectedAreaContainsOnlyOneColumn()
        {
            CheckIfSelectedAreaContainsOnlyColumns();
            if (dataGridView.SelectedColumns.Count != 1)
                throw new ArgumentException("One column should be selected.");
        }
        /// <summary>
        /// 
        /// </summary>
        private void CheckIfSelectedAreaContainsOnlyColumns()
        {
            // I find all unique column indexes of selected cells and then check if their number match number of selected columns.
            List<bool> check = new List<bool>(new bool[dataGridView.Columns.Count]);
            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                check[cell.ColumnIndex] = true;
            int numberOfSelectedCols = 0;
            foreach (var item in check)
                numberOfSelectedCols += item == true ? 1 : 0;
            if (numberOfSelectedCols != dataGridView.SelectedColumns.Count)
                throw new ArgumentException("Selected area is not set of columns.");
        }
        /// <summary>
        /// Plots X -> Y.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemPlotXY_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                CheckIfSelectedAreaContainsOnlyColumns();
                if (dataGridView.SelectedColumns.Count != 2)
                    throw new ArgumentException("Exactly two columns should be selected.");
                int colX = dataGridView.SelectedColumns[0].Index;
                int colY = dataGridView.SelectedColumns[1].Index;
                List<double> xValues = new List<double>();
                List<double> yValues = new List<double>();
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if ((string)row.Cells[colX].Value == dataGridView.Columns[colX].Name)
                        continue;
                    xValues.Add(double.Parse((string)row.Cells[colX].Value));
                    yValues.Add(double.Parse((string)row.Cells[colY].Value));
                }
                FormGraph newGraph = new FormGraph();
                newGraph.DrawLineGraph(xValues, yValues);
                newGraph.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to display plot. {ex.Message}");
            }

        }
        /// <summary>
        /// Displays mean value of the column.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeanValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                CheckIfSelectedAreaContainsOnlyOneColumn();
                int colNum = dataGridView.SelectedColumns[0].Index;
                List<double> values = new List<double>();
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if ((string)row.Cells[colNum].Value == dataGridView.Columns[colNum].Name)
                        continue;
                    values.Add(double.Parse((string)row.Cells[colNum].Value));
                }
                MessageBox.Show($"Mean value is {values.Average()}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to count mean value. {ex.Message}");
            }

        }
        /// <summary>
        /// Displays median of the column.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MedianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                CheckIfSelectedAreaContainsOnlyOneColumn();
                int colNum = dataGridView.SelectedColumns[0].Index;
                List<double> values = new List<double>();
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if ((string)row.Cells[colNum].Value == dataGridView.Columns[colNum].Name)
                        continue;
                    values.Add(double.Parse((string)row.Cells[colNum].Value));
                }
                values.Sort();
                int size = values.Count;
                int mid = size / 2;
                double median = (size % 2 != 0) ? (double)values[mid] : ((double)values[mid] + (double)values[mid - 1]) / 2;
                MessageBox.Show($"Median is {median}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to count median. {ex.Message}");
            }
        }
        /// <summary>
        /// Displays standart deviation of the column.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                CheckIfSelectedAreaContainsOnlyOneColumn();
                int colNum = dataGridView.SelectedColumns[0].Index;
                List<double> values = new List<double>();
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if ((string)row.Cells[colNum].Value == dataGridView.Columns[colNum].Name)
                        continue;
                    values.Add(double.Parse((string)row.Cells[colNum].Value));
                }
                double avg = values.Average();
                MessageBox.Show($"Standart deviation equals {Math.Sqrt(values.Sum(x => (x - avg) * (x - avg)) / values.Count)}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to count standart deviation. {ex.Message}");
            }
        }
        /// <summary>
        /// Displays varience of the column.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VarienceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                CheckIfSelectedAreaContainsOnlyOneColumn();
                int colNum = dataGridView.SelectedColumns[0].Index;
                List<double> values = new List<double>();
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if ((string)row.Cells[colNum].Value == dataGridView.Columns[colNum].Name)
                        continue;
                    values.Add(double.Parse((string)row.Cells[colNum].Value));
                }
                double avg = values.Average();
                double varience = values.Sum(x => (x - avg) * (x - avg)) / values.Count;

                MessageBox.Show($"Varience equals {varience}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to count varience. {ex.Message}");
            }
        }


    }
}
