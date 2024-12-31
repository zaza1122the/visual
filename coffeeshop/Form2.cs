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


namespace coffeeshop
{
    public partial class Form2 : Form
    {


        public Form2()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            // Simulated data (replace this with your actual data)
            Dictionary<string, int> itemSalesData = new Dictionary<string, int>
            {
                {"Latte", 100},
                {"Espresso", 200},
                {"Americano", 300},
                {"Brownie", 150},
                {"Cheesecake", 190},
                {"Cookie", 250},

            };

            // Clear existing series
            chart1.Series.Clear();

            // Create a new series
            Series series = new Series("Item Sales");
            series.ChartType = SeriesChartType.Bar;

            // Populate the series with data
            foreach (var item in itemSalesData)
            {
                series.Points.AddXY(item.Key, item.Value);
            }

            // Add the series to the chart
            chart1.Series.Add(series);

            // Set chart title and axis labels
            chart1.Titles.Clear();
            chart1.Titles.Add("Most Sold Items");
            chart1.ChartAreas[0].AxisX.Title = "Items";
            chart1.ChartAreas[0].AxisY.Title = "Quantity";

            // Show the legend
            chart1.Legends.Clear();
            chart1.Legends.Add(new Legend("Legend"));

            // Refresh the chart
            chart1.Invalidate();
        }
    }
}
