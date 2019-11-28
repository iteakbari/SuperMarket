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
    public partial class FMoshtariEdit : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=SuperMarket;Integrated Security=True");

        public FMoshtariEdit()
        {
            InitializeComponent();
        }

        Boolean checkKode = false;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select name,family,address,phone from moshtari where mkey='" + Convert.ToInt32(textBox1.Text) + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                textBox2.Text = dt.Rows[0].ItemArray[0].ToString();
                textBox3.Text = dt.Rows[0].ItemArray[1].ToString();
                textBox4.Text = dt.Rows[0].ItemArray[2].ToString();
                textBox5.Text = dt.Rows[0].ItemArray[3].ToString();

                checkKode = true;
            }
            catch (Exception)
            {
                textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
                checkKode = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (checkKode == false)
            {
                MessageBox.Show("این کد اشتراک وجود ندارد");
                textBox1.Focus();
            }
            else if(textBox1.Text == "")
            {
                textBox1.Focus();
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
            else if (textBox5.Text == "")
            {
                textBox5.Focus();
            }
            else
            {
                try
                {
                    conn.Open();

                    SqlCommand com = new SqlCommand("update moshtari set name=@name,family=@family,address=@address,phone=@phone where mkey=@mkey ", conn);
                    com.Parameters.AddWithValue("@name", textBox2.Text);
                    com.Parameters.AddWithValue("@family", textBox3.Text);
                    com.Parameters.AddWithValue("@address", textBox4.Text);
                    com.Parameters.AddWithValue("@phone", Convert.ToInt64(textBox5.Text));
                    com.Parameters.AddWithValue("@mkey", Convert.ToInt32(textBox1.Text));
                    com.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("ویرایش اطلاعات با موفقیت انجام شد");

                    FMoshtariEdit_Load(sender, e);
                }
                catch(Exception)
                {
                    MessageBox.Show("خطا. اطلاعات بروزرسانی نشد لطفا داده های ورودی را بررسی کنید");
                }
            }
        }

        private void FMoshtariEdit_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select mkey[کد اشتراک],name[نام مشتری],family[نام خانوادگی],address[آدرس],phone[شماره تماس] from moshtari", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[2].Width = 95;
            dataGridView1.Columns[3].Width = 210;

            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
            textBox1.Focus();
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
                MessageBox.Show("این کد اشتراک وجود ندارد");
            }
            else
            {
                try
                {
                    conn.Open();

                    SqlCommand com = new SqlCommand("DELETE FROM moshtari WHERE mkey= @mkey", conn);
                    com.Parameters.AddWithValue("@mkey", Convert.ToInt32(textBox1.Text));
                    com.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("اطلاعات مشتری با موفقیت حذف شد");

                    FMoshtariEdit_Load(sender, e);
                }
                catch(Exception)
                {
                    MessageBox.Show("خطا. اطلاعات حذف نشد لطفا داده های ورودی را بررسی کنید");
                }
            }
        }
    }
}
