using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace coffeeshop
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        static class Program
        {
            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new login());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string user, pass;
            user=textBox1.Text;
            pass=textBox2.Text; 

            if (user =="zahid" && pass =="zahid")

            {
                coffeeshop fm = new coffeeshop();
                fm.Show();
            }
            
           else if (user == "nazanin" && pass == "nazanin")
            {
                coffeeshop fm = new coffeeshop();
                fm.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                textBox1.Clear();
                textBox2.Clear();
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
