using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
        }

        private void افزودنکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FAddUser fau = new FAddUser();
            fau.ShowDialog();
        }

        private void افزودنمشتریجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FMoshtari fm = new FMoshtari();
            fm.ShowDialog();
        }

        private void ویرایشاطلاعاتمشتریانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FMoshtariEdit fme = new FMoshtariEdit();
            fme.ShowDialog();
        }

        private void افزودنکالایجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FAddKala fak = new FAddKala();
            fak.ShowDialog();
        }

        private void ویرایشاطلاعاتکالاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FKalaEdit fke = new FKalaEdit();
            fke.ShowDialog();
        }

        private void فاکتورToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FFactor ff = new FFactor();
            ff.ShowDialog();
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            if (memory.username == "")
            {
                FLogin fl = new FLogin();
                fl.ShowDialog();
            }
            if (memory.usertype == 0)
            {
                menuUser.Enabled = false;
                reports.Enabled = false;
            }
            else
            {
                menuUser.Enabled = true;
                reports.Enabled = true;
            }
        }

        private void جستجویمشتریانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FMoshtariSearch fms = new FMoshtariSearch();
            fms.ShowDialog();
        }

        private void جسیجویکالاهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FKalaSearch fks = new FKalaSearch();
            fks.ShowDialog();
        }

        private void گزارشاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FReports fr = new FReports();
            fr.ShowDialog();
        }
    }
}
