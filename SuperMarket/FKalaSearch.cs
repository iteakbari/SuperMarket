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
    public partial class FKalaSearch : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=SuperMarket;Integrated Security=True");

        public FKalaSearch()
        {
            InitializeComponent();
        }

        private void FKalaSearch_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select kkey[کد کالا],kname[نام کالا],price[قیمت کالا],mojodi[موجودی] from kala", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 80;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select kkey[کد کالا],kname[نام کالا],price[قیمت کالا],mojodi[موجودی] from kala where kkey LIKE '"+ textBox1.Text+"%'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select kkey[کد کالا],kname[نام کالا],price[قیمت کالا],mojodi[موجودی] from kala where kname LIKE N'%" + textBox2.Text + "%'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }
    }
}
