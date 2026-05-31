﻿using System;
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
    public partial class nvKho : Form
    {
        public nvKho()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            fChangePassWord change = new fChangePassWord();
            this.Hide();
            change.ShowDialog();
<<<<<<< Updated upstream
            this.Show();
=======
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            try
            {
                LoadFormToPanel(new frmHoaDonNhap(_maNhanVien, _hoTen));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // xem lỗi gì
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            LoadFormToPanel(new frmNhaCungCap());
>>>>>>> Stashed changes
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // Mở trang Hóa đơn nhập
            frmHoaDonNhap f = new frmHoaDonNhap();
            f.ShowDialog();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // Mở trang Tồn kho (Tra cứu sách)
            fSanPham f = new fSanPham();
            f.ShowDialog();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Mở trang Nhà cung cấp
            frmNhaCungCap f = new frmNhaCungCap();
            f.ShowDialog();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Mở trang Tra cứu sách
            fSanPham f = new fSanPham();
            f.ShowDialog();
        }
    }
}
