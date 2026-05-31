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
        private string _maNhanVien;
        private string _hoTen;
        private string _vaiTro;

        public Admin(string maNhanVien, string hoTen, string vaiTro)
        {
            _maNhanVien = maNhanVien;
            _hoTen = hoTen ?? string.Empty;
            _vaiTro = vaiTro ?? string.Empty;
            InitializeComponent();
        }

        public Admin()
        {
            InitializeComponent();
        }
        private void Admin_Load(object sender, EventArgs e)
        {
            label2.Text = _hoTen.ToUpper();
            label3.Text = _vaiTro;
            timer1.Interval = 1000;
            timer1.Tick += (s, ev) =>
            {
                lblThoiGian.Text = DateTime.Now.ToString("HH:mm:ss - dddd, dd/MM/yyyy",
                    new System.Globalization.CultureInfo("vi-VN"));
            };
            timer1.Start();
            lblThoiGian.Text = DateTime.Now.ToString("HH:mm:ss - dddd, dd/MM/yyyy",
                new System.Globalization.CultureInfo("vi-VN"));
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {

            fChangePassWord change = new fChangePassWord();
            this.Hide();
            change.ShowDialog();
            this.Show();

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
        private void btnHDKD_Click_1(object sender, EventArgs e) { LoadFormToPanel(new frmBCKinhDoanh()); }
        private void guna2Button6_Click(object sender, EventArgs e) { LoadFormToPanel(new frmKhachHang()); }
        private void guna2Button4_Click(object sender, EventArgs e) { LoadFormToPanel(new fNhanVien()); }
        private void guna2Button3_Click(object sender, EventArgs e) { LoadFormToPanel(new fSanPham()); }
        private void guna2Button5_Click(object sender, EventArgs e) { LoadFormToPanel(new frmNhaCungCap()); }
        private void guna2Button11_Click(object sender, EventArgs e) { LoadFormToPanel(new frmBCNhap()); }
        private void guna2Button10_Click(object sender, EventArgs e) { LoadFormToPanel(new frmBCBanHang()); }
        private void guna2Button7_Click(object sender, EventArgs e) { LoadFormToPanel(new frmDSHoaDonNhap()); }
        private void guna2Button8_Click(object sender, EventArgs e) { LoadFormToPanel(new frmDSHoaDonXuat()); }
        private void btnLogOut_Click(object sender, EventArgs e) { this.Close(); }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            TTTaiKhoan frm = new TTTaiKhoan(_maNhanVien);
            frm.ShowDialog();
        }
    }
}