using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class TTTaiKhoan : Form
    {
        string _maNV;
        public TTTaiKhoan(string maNV)
        {
            InitializeComponent();
            _maNV = maNV;
        }
        private void LoadThongTin()
        {
            DAO.Connect();
            string sql = @"SELECT ma_nhan_vien, ho_ten, 
                              ten_dang_nhap, vai_tro, trang_thai
                       FROM tblNhanVien
                       WHERE ma_nhan_vien = N'" + _maNV + "'";

            DataTable dt = DAO.LoadDataToTable(sql);
            DAO.Close();

            if (dt.Rows.Count == 0) return;
            DataRow r = dt.Rows[0];
            txtMaNV.Text = r["ma_nhan_vien"].ToString();
            txtChucVu.Text = r["vai_tro"].ToString();
            txtHoTen.Text = r["ho_ten"].ToString();
            txtTenDN.Text = r["ten_dang_nhap"].ToString();
            txtTrangThai.Text = r["trang_thai"].ToString();

            string hoTen = r["ho_ten"].ToString().Trim();
            string[] parts = hoTen.Split(' ');
            lblAvatar.Text = parts.Length > 0
                ? parts[parts.Length - 1].Substring(0, 1).ToUpper()
                : "?";

            lblTenNV.Text = hoTen;
            lblChucVu.Text = r["vai_tro"].ToString();

            txtMaNV.Enabled = false;
            txtChucVu.Enabled = false;
            txtTrangThai.Enabled = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            fChangePassWord frm = new fChangePassWord(txtMaNV.Text,  txtHoTen.Text, txtChucVu.Text);
            frm.ShowDialog();
        }

        private void TTTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadThongTin();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtHoTen.Text.Trim() == "")
            {
                MessageBox.Show("Họ tên không được để trống!");
                txtHoTen.Focus();
                return;
            }

            if (txtTenDN.Text.Trim() == "")
            {
                MessageBox.Show("Tên đăng nhập không được để trống!");
                txtTenDN.Focus();
                return;
            }

            try
            {
                DAO.Connect();
                string sql = @"UPDATE tblNhanVien SET
                           ho_ten       = N'" + txtHoTen.Text.Trim() + @"',
                           ten_dang_nhap = N'" + txtTenDN.Text.Trim() + @"'
                           WHERE ma_nhan_vien = N'" + _maNV + "'";

                SqlCommand cmd = new SqlCommand(sql, DAO.con);
                cmd.ExecuteNonQuery();
                DAO.Close();

                MessageBox.Show("Cập nhật thành công!");
                LoadThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
