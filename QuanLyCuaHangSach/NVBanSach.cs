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
        public NVBanSach()
        {
            InitializeComponent();
        }

<<<<<<< Updated upstream
=======
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


        private void btnTraCuuSach_Click_1(object sender, EventArgs e) { LoadFormToPanel(new fSanPham()); }
        private void btnLapHoaDonBan_Click_1(object sender, EventArgs e) { LoadFormToPanel(new frmHoaDonXuat(_maNhanVien, _hoTen)); }
        private void btnKhachHang_Click_1(object sender, EventArgs e) { LoadFormToPanel(new frmKhachHang()); }
        private void btnDSHoaDonBan_Click(object sender, EventArgs e) { LoadFormToPanel(new frmBCBanHang()); }
        private void btnLogOut_Click(object sender, EventArgs e) { this.Close(); }

>>>>>>> Stashed changes
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            fChangePassWord change = new fChangePassWord();
            this.Hide();
            change.ShowDialog();
            this.Show();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // Mở trang Khách hàng
            frmKhachHang f = new frmKhachHang();
            f.ShowDialog();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // Mở trang Lập hóa đơn bán (Hóa đơn xuất)
            frmHoaDonXuat f = new frmHoaDonXuat();
            f.ShowDialog();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Mở trang Tra cứu sách (Sản phẩm)
            fSanPham f = new fSanPham();
            f.ShowDialog();
        }
    }
}
