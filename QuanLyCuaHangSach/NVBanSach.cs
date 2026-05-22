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
