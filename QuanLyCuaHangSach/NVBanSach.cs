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
    public partial class NVBanSach : Form
    {
        private string _maNhanVien;
        private string _hoTen;
        private string _vaiTro;

        public NVBanSach(string maNhanVien, string hoTen, string vaiTro)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            _maNhanVien = maNhanVien;
            _hoTen = hoTen;
            _vaiTro = vaiTro;
        }

        private void NVBanSach_Load(object sender, EventArgs e)
        {
            lbTen.Text = _hoTen.ToUpper();
            lbVtro.Text = _vaiTro;

            timer1.Interval = 1000;
            timer1.Tick += (s, ev) =>
            {
                lbTG1.Text = DateTime.Now.ToString("HH:mm:ss - dddd, dd/MM/yyyy",
                    new System.Globalization.CultureInfo("vi-VN"));
            };
            timer1.Start();
            lbTG1.Text = DateTime.Now.ToString("HH:mm:ss - dddd, dd/MM/yyyy",
                new System.Globalization.CultureInfo("vi-VN"));
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
       

        private void btnTraCuuSach_Click_1(object sender, EventArgs e){ LoadFormToPanel(new fSanPham());}
        private void btnLapHoaDonBan_Click_1(object sender, EventArgs e) { LoadFormToPanel(new frmHoaDonXuat()); }
        private void btnKhachHang_Click_1(object sender, EventArgs e) { LoadFormToPanel(new frmKhachHang());}
        private void btnDSHoaDonBan_Click(object sender, EventArgs e){ LoadFormToPanel(new frmBCBanHang());}
        private void btnLogOut_Click(object sender, EventArgs e){ this.Close();}

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            fChangePassWord change = new fChangePassWord(_maNhanVien, _hoTen, _vaiTro);
            change.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            TTTaiKhoan frm = new TTTaiKhoan(_maNhanVien);
            frm.ShowDialog();
        }
    }
}