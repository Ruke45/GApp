using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TileApplication
{
    public partial class Login : Form
    {
    //    SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (UName.Text != "" && Upass.Text != "")
            {

                if (UName.Text == "User" && Upass.Text == "123")
                {
                    this.DialogResult = DialogResult.OK;
                   
                }
                else
                {
                    MessageBox.Show("Incorrect User Name or Password");
                }
            }
            else
            {
                MessageBox.Show("Please Insert User Name and Password");

            }
        }
    }
}
