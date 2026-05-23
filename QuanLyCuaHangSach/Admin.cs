using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            fChangePassWord change = new fChangePassWord();
            this.Hide();
            change.ShowDialog();
        }

        private void LoadFormToPanel(Form childForm)
        {
            pContent.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pContent.Controls.Add(childForm);
            childForm.Show();
        }

        private void btnHDKD_Click_1(object sender, EventArgs e)
        {
            LoadFormToPanel(new frmBCKinhDoanh());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            LoadFormToPanel(new frmKhachHang());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            LoadFormToPanel(new fNhanVien());
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
