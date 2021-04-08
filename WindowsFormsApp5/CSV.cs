using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    class Csv
    {
        List<string> columnNames;
        List<List<string>> values;
        /// <summary>
        /// Loads csv file and make and object of it.
        /// </summary>
        /// <param name="filename"> Name of csv file. </param>
        /// <returns> Csv object. </returns>
        public static Csv CsvFromFile(string filename)
        {
            List<List<string>> input = new List<List<string>>();
            Csv newCsv;
            try
            {
                using (TextFieldParser parser = new TextFieldParser(filename))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        input.Add(parser.ReadFields().ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while parsing file. {ex.Message}");
                return null;
            }
            try
            {
                newCsv = new Csv();
                newCsv.columnNames = input[0];
                newCsv.values = new List<List<string>>();
                for (var i = 0; i < input.Count - 1; i++)
                {
                    newCsv.values.Add(input[i + 1]);
                    if (newCsv.values[i].Count != newCsv.columnNames.Count)
                        throw new ArgumentException($"Number of elements in line {i + 2} doesn't match one in header.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while creating csv file. {ex.Message}");
                return null;
            }
            return newCsv;
        }
        /// <summary>
        /// Gives access to names of columns.
        /// </summary>
        public List<string> Header
        {
            get { return columnNames; }
        }
        /// <summary>
        /// Gives access to elements of the table.
        /// </summary>
        /// <param name="i"> Number of row. </param>
        /// <param name="j"> Number of column. </param>
        /// <returns> Element in [i,j]. </returns>
        public string this[int i, int j]
        {
            get { return values[i][j]; }
            set { values[i][j] = value; }
        }
        /// <summary>
        /// Allows to get single row.
        /// </summary>
        /// <param name="i"> Row number. </param>
        /// <returns>List containing elements of the row. </returns>
        public List<string> this[int i]
        {
            get { return values[i]; }
        }
        /// <summary>
        /// Returns (rowCount, columnCount) of the table.
        /// </summary>
        public (int, int) Shape
        {
            get { return (values.Count, columnNames.Count); }
        }



    }
}
