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
    public partial class FAddKala : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=SuperMarket;Integrated Security=True");

        public FAddKala()
        {
            InitializeComponent();
        }

        Boolean checkKode;

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Focus();
            }
            else if (checkKode == true)
            {
                textBox1.Focus();
                MessageBox.Show("کالایی با این کد قبلا ثبت شده است");
            }
            else if(textBox2.Text == ""){
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

                    SqlCommand com = new SqlCommand("insert into [kala] (kkey,kname,price,mojodi)values(@kkey,@kname,@price,@mojodi)", conn);
                    com.Parameters.AddWithValue("@kkey", Convert.ToInt64(textBox1.Text));
                    com.Parameters.AddWithValue("@kname", textBox2.Text);
                    com.Parameters.AddWithValue("@price", Convert.ToInt32(textBox3.Text));
                    com.Parameters.AddWithValue("@mojodi", Convert.ToInt32(textBox4.Text));
                    com.ExecuteNonQuery();

                    MessageBox.Show("اطلاعات کالا با موفقیت ثبت شد");
                    FAddKala_Load(sender, e);
                }
                catch (Exception)
                {
                    MessageBox.Show("اطلاعات بدرستی وارد نشده است");
                }
                conn.Close();
            }
        }

        private void FAddKala_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select kkey[کد کالا],kname[نام کالا],price[قیمت کالا],mojodi[موجودی] from kala", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 70;

            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand com = new SqlCommand("select * from kala where kkey=@kkey",conn);
                com.Parameters.AddWithValue("@kkey", textBox1.Text);
                checkKode=Convert.ToBoolean(com.ExecuteScalar());
                conn.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
