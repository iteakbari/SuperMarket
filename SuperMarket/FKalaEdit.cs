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
    public partial class FKalaEdit : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=SuperMarket;Integrated Security=True");

        Boolean checkKode = false;

        public FKalaEdit()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select kname,price,mojodi from kala where kkey='" + Convert.ToInt64(textBox1.Text) + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                textBox2.Text = dt.Rows[0].ItemArray[0].ToString();
                textBox3.Text = dt.Rows[0].ItemArray[1].ToString();
                textBox4.Text = dt.Rows[0].ItemArray[2].ToString();

                checkKode = true;
            }
            catch (Exception)
            {
                textBox2.Text = textBox3.Text = textBox4.Text = "";
                checkKode = false;
            }
        }

        private void FKalaEdit_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select kkey[کد کالا],kname[نام کالا],price[قیمت کالا],mojodi[موجودی] from kala", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 80;

            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            textBox1.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Focus();
            }
            else if (checkKode == false)
            {
                textBox1.Focus();
                MessageBox.Show("کالایی با این کد وجود ندارد");
            }
            else if (textBox2.Text == "")
            {
                textBox2.Focus();
            }
            else if (textBox3.Text == "")
            {
                textBox3.Focus();
            }
            else if (textBox4.Text == "")
            {
                textBox4.Focus();
            }
            else
            {
                try
                {
                    conn.Open();

                    SqlCommand com = new SqlCommand("update kala set kname=@kname,price=@price,mojodi=@mojodi where  kkey=@kkey", conn);
                    com.Parameters.AddWithValue("@kname", textBox2.Text);
                    com.Parameters.AddWithValue("@price", Convert.ToInt32(textBox3.Text));
                    com.Parameters.AddWithValue("@mojodi", Convert.ToInt32(textBox4.Text));
                    com.Parameters.AddWithValue("@kkey", Convert.ToInt64(textBox1.Text));
                    com.ExecuteNonQuery();

                    MessageBox.Show("اطلاعات کالا با موفقیت بروزرسانی شد");
                    FKalaEdit_Load(sender, e);
                }
                catch (Exception)
                {
                    MessageBox.Show("خطا. لطفا داده های ورودی را بررسی کنید");
                }
                conn.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Focus();
            }
            else if (checkKode == false)
            {
                textBox1.Focus();
                MessageBox.Show("کالایی با این کد یاغت نشد");
            }
            else
            {
                try
                {
                    conn.Open();

                    SqlCommand com = new SqlCommand("delete from kala where kkey=@kkey", conn);
                    com.Parameters.AddWithValue("@kkey", Convert.ToInt64(textBox1.Text));
                    com.ExecuteNonQuery();

                    MessageBox.Show("کالا با موفقیت حذف شد");
                    FKalaEdit_Load(sender, e);
                }
                catch (Exception)
                {
                    MessageBox.Show("خطا. لطفا داده های ورودی را بررسی کنید");
                }
                conn.Close();
            }
        }
    }
}
