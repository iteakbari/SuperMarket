using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SuperMarket
{
    public partial class FLogin : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=SuperMarket;Integrated Security=True");

        public FLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Focus();
            }
            else if (textBox2.Text == "")
            {
                textBox2.Focus();
            }
            else
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("select pass,usertype from AddUser where username LIKE N'" + textBox1.Text+"'", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    string password = dt.Rows[0].ItemArray[0].ToString();
                    memory.usertype = Convert.ToInt32(dt.Rows[0].ItemArray[1]);

                    if (textBox2.Text == password)
                    {
                        memory.username = textBox1.Text;

                        conn.Open();
                        SqlCommand com = new SqlCommand("update [AddUser] set vorood=vorood+1,lastDate=@lastDate,lastTime=@lastTime where username=@username", conn);
                        com.Parameters.AddWithValue("@lastDate", shamsiDate.m2sh(DateTime.Now));
                        com.Parameters.AddWithValue("@lastTime", DateTime.Now.ToLongTimeString());
                        com.Parameters.AddWithValue("@username", textBox1.Text);
                        com.ExecuteNonQuery();
                        conn.Close();

                        this.Close();
                    }
                    else
                    {
                        textBox2.Focus();
                        MessageBox.Show("رمز عبور اشتباه است");
                    }
                }
                catch (Exception)
                {
                    textBox1.Focus();
                    MessageBox.Show("نام کاربری اشتباه است");
                }
            }
        }

        private void bntExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
