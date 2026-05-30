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
    public partial class nvKho : Form
    {
        private string _maNhanVien;
        private string _hoTen;
        private string _vaiTro;

        public nvKho(string maNhanVien, string hoTen, string vaiTro)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            _maNhanVien = maNhanVien;
            _hoTen = hoTen;
            _vaiTro = vaiTro;
        }

        private void nvKho_Load(object sender, EventArgs e)
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
            pContent3.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pContent3.Controls.Add(childForm);
            childForm.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            fChangePassWord change = new fChangePassWord(_maNhanVien, _hoTen, _vaiTro);
            change.ShowDialog();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            try
            {
                LoadFormToPanel(new frmHoaDonNhap());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // xem lỗi gì
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            LoadFormToPanel(new frmNhaCungCap());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            LoadFormToPanel(new frmBCNhap());
        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            LoadFormToPanel(new fSanPham());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            TTTaiKhoan frm = new TTTaiKhoan(_maNhanVien);
            frm.ShowDialog();
        }
    }
}