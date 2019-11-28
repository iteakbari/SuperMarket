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
    public partial class FReports : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=SuperMarket;Integrated Security=True");

        public FReports()
        {
            InitializeComponent();
        }

        public void showData()
        {
            if(textBox1.Text=="" && textBox2.Text == "")
            {
                SqlDataAdapter da = new SqlDataAdapter("select factor.fkey[کد فاکتور],factor.username[نام کاربر],factor.mkey[کد مشتری],moshtari.name[نام مشتری],moshtari.family[نام خانوادگی],factor.datex[تاریخ خرید],factor.timex[زمان خرید],factor.sumPrice[مبلغ فاکتور] from factor INNER JOIN moshtari ON factor.mkey=moshtari.mkey ORDER BY factor.datex DESC", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Width = 80;
                dataGridView1.Columns[1].Width = 70;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].Width = 80;
                dataGridView1.Columns[5].Width = 80;
                dataGridView1.Columns[6].Width = 80;
                dataGridView1.Columns[7].Width = 90;
            }
            else if (textBox1.Text != "" && textBox2.Text == "")
            {
                SqlDataAdapter da = new SqlDataAdapter("select factor.fkey[کد فاکتور],factor.username[نام کاربر],factor.mkey[کد مشتری],moshtari.name[نام مشتری],moshtari.family[نام خانوادگی],factor.datex[تاریخ خرید],factor.timex[زمان خرید],factor.sumPrice[مبلغ فاکتور] from factor INNER JOIN moshtari ON factor.mkey=moshtari.mkey where factor.datex >= '" + textBox1.Text +"%' ORDER BY factor.datex DESC", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            else if (textBox1.Text == "" && textBox2.Text != "")
            {
                SqlDataAdapter da = new SqlDataAdapter("select factor.fkey[کد فاکتور],factor.username[نام کاربر],factor.mkey[کد مشتری],moshtari.name[نام مشتری],moshtari.family[نام خانوادگی],factor.datex[تاریخ خرید],factor.timex[زمان خرید],factor.sumPrice[مبلغ فاکتور] from factor INNER JOIN moshtari ON factor.mkey=moshtari.mkey where factor.datex <= '" + textBox2.Text + "%' ORDER BY factor.datex DESC", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select factor.fkey[کد فاکتور],factor.username[نام کاربر],factor.mkey[کد مشتری],moshtari.name[نام مشتری],moshtari.family[نام خانوادگی],factor.datex[تاریخ خرید],factor.timex[زمان خرید],factor.sumPrice[مبلغ فاکتور] from factor INNER JOIN moshtari ON factor.mkey=moshtari.mkey where  factor.datex >= '" + textBox1.Text + "%' AND factor.datex <= '" + textBox2.Text + "%' ORDER BY factor.datex DESC", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }

            lblRecord.Text =(Convert.ToInt32( dataGridView1.Rows.Count)-1).ToString();
        }

        private void FReports_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            showData();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            showData();
        }
    }
}
