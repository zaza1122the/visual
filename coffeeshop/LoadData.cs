using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Linq;

namespace coffeeshop
{
    public partial class LoadData : Form
    {
        private MySqlConnection con = new MySqlConnection("server=localhost;database=project;user id=root;password=;");

        public LoadData()
        {
            InitializeComponent();
        }

        private void LoadData_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                con.Open();
                MySqlCommand command = new MySqlCommand("SELECT ItemsName, SUM(Quantity) AS Total FROM item_tbl GROUP BY ItemsName", con);
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                
                var mostSoldItems = dt.AsEnumerable()
                    .GroupBy(row => row.Field<string>("ItemsName"))
                    .Select(g => new
                    {
                        ItemName = g.Key,
                        TotalQuantitySold = g.Sum(row =>
                        {
                            object totalValue = row["Total"];
                            return totalValue == DBNull.Value ? 0 : Convert.ToDecimal(totalValue);
                        })
                    })
                    .OrderByDescending(g => g.TotalQuantitySold)
                    .Take(5);

                foreach (var item in mostSoldItems)
                {
                    this.chart1.Series["Items"].Points.AddXY(item.ItemName, item.TotalQuantitySold);
                }

                this.chart1.Titles.Add("Most sold item");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR" + ex);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bmp, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bmp, 100, 150);

            e.Graphics.DrawString("COFFEE SHOP", new Font("Arial", 20, FontStyle.Bold), Brushes.Red, new Point(185, 10));
            e.Graphics.DrawString("Sale Receipt", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, new Point(240, 40));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
        }

       

        private void mostSoldItems_Click_1(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
        }

       
    }
}
