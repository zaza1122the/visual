using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MySql.Data.MySqlClient;

namespace coffeeshop
{
    public partial class coffeeshop : Form
    
    {
        

        private MySqlConnection con = new MySqlConnection();
        public coffeeshop()
        {
            InitializeComponent();
            con.ConnectionString = "server=localhost;database=item_tbl;user id=root;password=;";
          
        }
        string username;
        public coffeeshop(string s)
        {
            InitializeComponent();
            username = s;
         

        }




        private void radioButton_coffee_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_coffee.ForeColor = System.Drawing.Color.Blue;
            radioButton_dessert.ForeColor = System.Drawing.Color.RosyBrown;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Latte");
            comboBox1.Items.Add("Espresso");
            comboBox1.Items.Add("Americano");
        }

        private void radioButton_dessert_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_coffee.ForeColor = System.Drawing.Color.Blue;
            radioButton_dessert.ForeColor = System.Drawing.Color.RosyBrown;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Brownie");
            comboBox1.Items.Add("Cheesecake");
            comboBox1.Items.Add("Cookie");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem.ToString() == "Latte")
            {
                textBox1.Text = "100";
            }
            else if (comboBox1.SelectedItem.ToString() == "Espresso")
            {
                textBox1.Text = "200";
            }
            else if (comboBox1.SelectedItem.ToString() == "Americano")
            {
                textBox1.Text = "300";
            }
            else if (comboBox1.SelectedItem.ToString() == "Brownie")
            {
                textBox1.Text = "150";
            }
            else if (comboBox1.SelectedItem.ToString() == "Cheesecake")
            {
                textBox1.Text = "190";
            }
            else if (comboBox1.SelectedItem.ToString() == "Cookie")
            {
                textBox1.Text = "250";
            }
            else
            {
                textBox1.Text = "0";
            }
            textBox_total.Text = "";
            textBox2.Text = "";

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            if (textBox2.Text.Length > 0)
            {
                textBox_total.Text = (Convert.ToInt64(textBox1.Text) * Convert.ToInt64(textBox2.Text)).ToString();
            }

        }

        private void button_additem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(comboBox1.Text, textBox1.Text, textBox_total.Text, textBox2.Text, dateTimePicker1.Text);
            textBox3.Text = (Convert.ToInt16(textBox3.Text) + Convert.ToInt16(textBox_total.Text)).ToString();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox_total.Text = "";

        }

        private void button_deleteitem_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected)
                    {
                        textBox3.Text = (Convert.ToInt16(textBox3.Text) - Convert.ToInt16(dataGridView1.Rows[i].Cells[3].Value)).ToString();
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }
            }


            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox_total.Text = "";

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length > 0)
            {
                textBox5.Text = (Convert.ToInt16(textBox3.Text) - Convert.ToInt16(textBox4.Text)).ToString();
            }

        }

        private void button_save_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO item_tbl Values ('" + dataGridView1.Rows[i].Cells[0].Value + "','" +
                    dataGridView1.Rows[i].Cells[1].Value + "','" + 
                    dataGridView1.Rows[i].Cells[2].Value + "','" +
                    dataGridView1.Rows[i].Cells[3].Value + "','" +
                    dataGridView1.Rows[i].Cells[4].Value + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("saved");
                con.Close();
            }
            dataGridView1.Rows.Clear();

            textBox3.Text = "0";
            textBox4.Text = "";
            textBox5.Text = "";


        }

      

        private void coffeeshop_Load(object sender, EventArgs e, MySqlConnection con)
        {
            try
            {
                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    MessageBox.Show("Successfully connected.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the database: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }


        private void button_loaddata_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoadData LDForm = new LoadData();
            LDForm.ShowDialog();
            LDForm = null;
            this.Show();
        }

        
    }
}
