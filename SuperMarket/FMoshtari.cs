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
    public partial class FMoshtari : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=SuperMarket;Integrated Security=True");

        public FMoshtari()
        {
            InitializeComponent();
        }

        public void newCode()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select MAX(mkey) from moshtari", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                textBox1.Text = (Convert.ToInt32(dt.Rows[0].ItemArray[0]) + 1).ToString();
            }
            catch (Exception)
            {
                textBox1.Text = "1000";
            }
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
            else if (textBox3.Text == "")
            {
                textBox3.Focus();
            }
            else if (textBox4.Text == "")
            {
                textBox4.Focus();
            }
            else if (textBox5.Text=="")
            {
                textBox5.Focus();
            }
            else
            {
                conn.Open();

                SqlCommand com = new SqlCommand("insert into [moshtari](mkey,name,family,address,phone)values(@mkey,@name,@family,@address,@phone)", conn);
                com.Parameters.AddWithValue("@mkey",Convert.ToInt32(textBox1.Text));
                com.Parameters.AddWithValue("@name", textBox2.Text);
                com.Parameters.AddWithValue("@family", textBox3.Text);
                com.Parameters.AddWithValue("@address", textBox4.Text);
                com.Parameters.AddWithValue("@phone", Convert.ToInt64(textBox5.Text));
                com.ExecuteNonQuery();

                conn.Close();

                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "";
                textBox2.Focus();

                MessageBox.Show("اطلاعات مشتری با موفقیت ثبت شد");

                FMoshtari_Load(sender, e);
            }
        }

        private void FMoshtari_Load(object sender, EventArgs e)
        {
            newCode();

            SqlDataAdapter da = new SqlDataAdapter("select mkey[شماره اشتراک],name[نام ],family[نام خانوادگی],address[آدرس],phone[شماره تماس] from [moshtari]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
