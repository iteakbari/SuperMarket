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
    public partial class FAddUser : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=SuperMarket;Integrated Security=True");

        public FAddUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
                int usertype;
                if (radioButton1.Checked == true)
                {
                    usertype = 0;
                }
                else
                {
                    usertype = 1;
                }

                try
                {
                    conn.Open();

                    SqlCommand com = new SqlCommand("INSERT INTO [AddUser](username,pass,usertype,vorood) VALUES(@username,@pass,@usertype,@vorood)", conn);
                    com.Parameters.AddWithValue("@username", textBox1.Text);
                    com.Parameters.AddWithValue("@pass", textBox2.Text);
                    com.Parameters.AddWithValue("@usertype", usertype);
                    com.Parameters.AddWithValue("@vorood", 0);
                    com.ExecuteNonQuery();

                    conn.Close();

                    textBox1.Text = textBox2.Text = "";
                    textBox1.Focus();

                    MessageBox.Show("اطلاعات کاربر جدید با موفقیت ثبت شد");

                    FAddUser_Load(sender, e);
                }
                catch (Exception)
                {
                    MessageBox.Show("این نام کاربری قبلا ثبت شده است");
                }
            }
        }

        private void FAddUser_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT AddUser.username AS [نام کاربری], userNo.typename AS [سطح دسترسی], AddUser.vorood AS [تعداد ورود], AddUser.lastDate AS [آخرین تاریخ], AddUser.lastTime AS [آخرین زمان] FROM AddUser INNER JOIN userNo ON AddUser.usertype = userNo.usertype", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 70;
            dataGridView1.Columns[3].Width = 90;
            dataGridView1.Columns[4].Width = 90;
        }
    }
}
