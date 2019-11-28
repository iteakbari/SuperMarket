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
    public partial class FMoshtariSearch : Form
    {

        SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=SuperMarket;Integrated Security=True");

        public FMoshtariSearch()
        {
            InitializeComponent();
        }

        private void FMoshtariSearch_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select mkey[کد اشتراک],name[نام مشتری],family[نام خانوادگی],address[آدرس],phone[شماره تماس] from moshtari", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[2].Width = 95;
            dataGridView1.Columns[3].Width = 210;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select mkey[کد اشتراک],name[نام مشتری],family[نام خانوادگی],address[آدرس],phone[شماره تماس] from moshtari where mkey like'" + textBox1.Text + "%'order by mkey asc", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select mkey[کد اشتراک],name[نام مشتری],family[نام خانوادگی],address[آدرس],phone[شماره تماس] from moshtari where name LIKE N'%" + textBox2.Text + "%'ORDER BY name ASC", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {

            }
        }
    }
}
