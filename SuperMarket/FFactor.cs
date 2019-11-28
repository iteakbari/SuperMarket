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
    public partial class FFactor : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=SuperMarket;Integrated Security=True");

        Boolean checkKodeMoshtari = false;
        Boolean checkKodeKala = false;
        Boolean checkFactor = false;

        public void newCode()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select MAX(fkey) from factor", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                lblFactor.Text = (Convert.ToInt32(dt.Rows[0].ItemArray[0])+1).ToString();
            }
            catch (Exception)
            {
                lblFactor.Text = "2000";
            }
        }

        public void showData()
        {
            SqlDataAdapter da = new SqlDataAdapter("select  aghlam.kkey[کد کالا],kala.kname[نام کالا],aghlam.tedad[تعداد],kala.price[قیمت واحد],aghlam.tedad*kala.price[قیمت کل] from kala inner join aghlam on kala.kkey= aghlam.kkey  where aghlam.fkey='"+Convert.ToInt64(lblFactor.Text) +"'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 65;
            dataGridView1.Columns[1].Width = 109;
            dataGridView1.Columns[2].Width = 45;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 80;

            da = new SqlDataAdapter("select sum(kala.price*aghlam.tedad) from kala inner join aghlam on kala.kkey=aghlam.kkey where aghlam.fkey='"+Convert.ToInt64(lblFactor.Text) +"'", conn);
            dt=new DataTable();
            da.Fill(dt);

            lblFactorPrice.Text = dt.Rows[0].ItemArray[0].ToString();
        }


        public FFactor()
        {
            InitializeComponent();
        }

        private void FFactor_Load(object sender, EventArgs e)
        {
            newCode();

            lblDate.Text = shamsiDate.m2sh(DateTime.Now);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select name,family,address,phone from moshtari where mkey='" + Convert.ToInt32(textBox1.Text) + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                lblName.Text = dt.Rows[0].ItemArray[0].ToString();
                lblFamily.Text = dt.Rows[0].ItemArray[1].ToString();
                lblAddress.Text = dt.Rows[0].ItemArray[2].ToString();
                lblPhone.Text = dt.Rows[0].ItemArray[3].ToString();

                checkKodeMoshtari = true;
            }
            catch (Exception)
            {
                lblName.Text = lblFamily.Text = lblAddress.Text = lblPhone.Text = "";
                checkKodeMoshtari = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select kname,price,mojodi from kala where kkey='" + Convert.ToInt64(textBox2.Text) + "'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                lblKalaName.Text = dt.Rows[0].ItemArray[0].ToString();
                lblPrice.Text = dt.Rows[0].ItemArray[1].ToString();
                lblMojodi.Text = dt.Rows[0].ItemArray[2].ToString();

                checkKodeKala = true;
            }
            catch (Exception)
            {
                lblKalaName.Text = lblPrice.Text = lblMojodi.Text = "";
                checkKodeKala = false;
            }
        }

        private void btnSabt_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Focus();
            }
            else if (checkKodeMoshtari == false)
            {
                textBox1.Focus();
                MessageBox.Show("کد مشتری وارد شده معتبر نیست");
            }
            else if (textBox2.Text == "")
            {
                textBox2.Focus();
            }
            else if (checkKodeKala == false)
            {
                textBox2.Focus();
                MessageBox.Show("کد کالا وارد شده معتبر نیست");
            }
            else if (textBox3.Text == "")
            {
                textBox3.Focus();
            }
            else if (Convert.ToInt32(textBox3.Text) > Convert.ToInt32(lblMojodi.Text))
            {
                textBox3.Focus();
                MessageBox.Show("تعداد درخواست شده از موجودی بیشتر است");
            }
            else
            {
                
                if (checkFactor == false)
                {
                    conn.Open();

                    SqlCommand com = new SqlCommand("insert into factor(fkey,username,mkey,datex,timex)values(@fkey,@username,@mkey,@datex,@timex)", conn);
                    com.Parameters.AddWithValue("@fkey", Convert.ToInt32(lblFactor.Text));
                    com.Parameters.AddWithValue("@username", memory.username);
                    com.Parameters.AddWithValue("@mkey", Convert.ToInt32(textBox1.Text));
                    com.Parameters.AddWithValue("@datex", shamsiDate.m2sh(DateTime.Now));
                    com.Parameters.AddWithValue("@timex", DateTime.Now.ToLongTimeString());
                    com.ExecuteNonQuery();

                    conn.Close();

                    checkFactor = true;
                }

                conn.Open();

                SqlCommand com1 = new SqlCommand("insert into aghlam (fkey,kkey,tedad)values(@fkey,@kkey,@tedad)", conn);
                com1.Parameters.AddWithValue("@fkey", Convert.ToInt32(lblFactor.Text));
                com1.Parameters.AddWithValue("@kkey", Convert.ToInt64(textBox2.Text));
                com1.Parameters.AddWithValue("@tedad", Convert.ToInt32(textBox3.Text));
                com1.ExecuteNonQuery();

                com1 = new SqlCommand("update kala set mojodi=mojodi-@tedad where kkey=@kkey",conn);
                com1.Parameters.AddWithValue("@tedad", Convert.ToInt32(textBox3.Text));
                com1.Parameters.AddWithValue("@kkey", Convert.ToInt64(textBox2.Text));
                com1.ExecuteNonQuery();

                conn.Close();

                textBox2.Text = textBox3.Text = "";
                textBox2.Focus();

                showData();
            }
        }

        private void btnMoshtariSearch_Click(object sender, EventArgs e)
        {
            FMoshtariSearch fms = new FMoshtariSearch();
            fms.ShowDialog();
        }

        private void btnKalaSearch_Click(object sender, EventArgs e)
        {
            FKalaSearch fks = new FKalaSearch();
            fks.ShowDialog();
        }

        private void btnEndBuy_Click(object sender, EventArgs e)
        {
            conn.Open();

            SqlCommand com = new SqlCommand("update factor set sumPrice=@sumPrice where fkey=@fkey", conn);
            com.Parameters.AddWithValue("@sumPrice", Convert.ToInt64(lblFactorPrice.Text));
            com.Parameters.AddWithValue("@fkey", Convert.ToInt32(lblFactor.Text));
            com.ExecuteNonQuery();

            conn.Close();

            this.Close();
        }

        private void btnNextFactor_Click(object sender, EventArgs e)
        {
            if (checkFactor == true)
            {
                btnEndBuy_Click(sender, e);

                checkFactor = false;

                FFactor ff = new FFactor();
                ff.ShowDialog();
            }
        }
    }
}
