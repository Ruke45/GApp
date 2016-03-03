using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace TileApplication
{
    public partial class MainForm : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TilesDBConnectionString"].ToString());
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private static void Upload()
        {
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Multiselect = true;
            op1.ShowDialog();
            op1.Filter = "allfiles|*.xls";
            //  textBox1.Text = op1.FileName;
            int count = 0;
            string[] FName;
            foreach (string s in op1.FileNames)
            {
                FName = s.Split('\\');
                File.Copy(s, Application.StartupPath + "\\Image\\" + FName[FName.Length - 1]);
                count++;
            }
            MessageBox.Show(Convert.ToString(count) + " File(s) copied");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            string Size = txtName.Text;
            string Color = txtColor.Text;
            double Price = Convert.ToDouble(txtPrice.Text);

            try
            {
                con.Open();
                    using (SqlCommand command = new SqlCommand(
                    "INSERT INTO Tiles VALUES(@ID, @Name, @Size, @Color, @Price)", con))
                    {
                        command.Parameters.Add(new SqlParameter("ID", id));
                        command.Parameters.Add(new SqlParameter("Name", name));
                        command.Parameters.Add(new SqlParameter("Size", Size));
                        command.Parameters.Add(new SqlParameter("Color", Color));
                        command.Parameters.Add(new SqlParameter("Price", Price));
                        command.ExecuteNonQuery();
                        MessageBox.Show("Insert Succesfull !");
                        this.tilesTableAdapter.Fill(this.tilesDBDataSet1.Tiles);
                    }

            }catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally{
                con.Close();
            }

          
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            string Size = txtName.Text;
            string Color = txtColor.Text;
            double Price = Convert.ToDouble(txtPrice.Text);
            try
            {
    
                  con.Open();
                  using (SqlCommand command = new SqlCommand("UPDATE Tiles SET Name=@Name," +
                      "Size=@Size," +
                      "Color=@Color," +
                      "Price=@Price " +
                      "WHERE Id=@ID", con))
                    {
                        command.Parameters.Add(new SqlParameter("ID", id));
                        command.Parameters.Add(new SqlParameter("Name", name));
                        command.Parameters.Add(new SqlParameter("Size", Size));
                        command.Parameters.Add(new SqlParameter("Color", Color));
                        command.Parameters.Add(new SqlParameter("Price", Price));
                        int rows = command.ExecuteNonQuery();

                        //rows number of record got updated
                    }
                  MessageBox.Show("Details Updated Sucessfully");
                  this.tilesTableAdapter.Fill(this.tilesDBDataSet1.Tiles);
                }
            
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
    
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                    int id = int.Parse(txtId.Text);
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("DELETE FROM Tiles " +
                            "WHERE Id=@Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show("Deleted Succesfully");
                        this.tilesTableAdapter.Fill(this.tilesDBDataSet1.Tiles);
                    }
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'tilesDBDataSet1.Tiles' table. You can move, or remove it, as needed.
            this.tilesTableAdapter.Fill(this.tilesDBDataSet1.Tiles);
            // TODO: This line of code loads data into the 'tilesDBDataSet.Tiles' table. You can move, or remove it, as needed.
          //  this.tilesTableAdapter.Fill(this.tilesDBDataSet.Tiles);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
    
                string sql = "select * from Tiles where ID = '" + txtId.Text + "' ";
                SqlDataAdapter dataadapter = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                con.Open();
                dataadapter.Fill(ds, "Tiles");
                con.Close();
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Tiles";

  
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }
    }//
}
