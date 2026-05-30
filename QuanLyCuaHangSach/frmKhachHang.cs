using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace QuanLyCuaHangSach
{
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            DAO.Connect();

            string sql = "SELECT * FROM tblKhachHang";

            dgvKhachHang.DataSource =
                DAO.LoadDataToTable(sql);

            DAO.Close();
            if (dgvKhachHang.DataSource is DataTable dt)
                dt.DefaultView.RowFilter = "";
        }
        private string TaoMaKhachHang()
        {
            string ma = "KH01";

            DAO.Connect();

            string sql =
                @"SELECT TOP 1 ma_khach_hang
                  FROM tblKhachHang
                  ORDER BY ma_khach_hang DESC";

            string maCuoi = DAO.getFieldValue(sql);

            DAO.Close();

            if (maCuoi != "")
            {
                int so =
                    int.Parse(maCuoi.Substring(2)) + 1;

                ma = "KH" + so.ToString("00");
            }

            return ma;
        }
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            dgvKhachHang.Theme =
                Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Light;

            dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor =
                Color.White;

            dgvKhachHang.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;
            btnThem.FillColor =
        Color.FromArgb(99, 102, 241);

            btnLuu.FillColor =
                Color.FromArgb(34, 197, 94);

            btnSua.FillColor =
                Color.FromArgb(37, 99, 235);

            btnXoa.FillColor =
                Color.FromArgb(239, 68, 68);

            btnLamMoi.FillColor =
                Color.FromArgb(249, 115, 22);
            txtTimKiem.PlaceholderText =
    "Tìm theo mã, tên, SĐT...";

            LoadData();

            txtMaKH.Enabled = false;
            dgvKhachHang.Columns["ma_khach_hang"].HeaderText = "Mã KH";
            dgvKhachHang.Columns["ten_khach_hang"].HeaderText = "Tên khách hàng";
            dgvKhachHang.Columns["so_dien_thoai"].HeaderText = "Số điện thoại";
            dgvKhachHang.Columns["dia_chi"].HeaderText = "Địa chỉ";
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                txtMaKH.Text =
                    row.Cells["ma_khach_hang"].Value.ToString();

                txtTenKH.Text =
                    row.Cells["ten_khach_hang"].Value.ToString();

                txtSDT.Text =
                    row.Cells["so_dien_thoai"].Value.ToString();

                txtDiaChi.Text =
                    row.Cells["dia_chi"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = TaoMaKhachHang();

            txtTenKH.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();

            txtTenKH.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DAO.Connect();

            string sql = @"INSERT INTO tblKhachHang
                   VALUES
                   (@makh, @tenkh, @sdt, @diachi)";

            SqlCommand cmd =
                new SqlCommand(sql, DAO.con);

            cmd.Parameters.AddWithValue("@makh", txtMaKH.Text);
            cmd.Parameters.AddWithValue("@tenkh", txtTenKH.Text);
            cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
            cmd.Parameters.AddWithValue("@diachi", txtDiaChi.Text);

            cmd.ExecuteNonQuery();

            DAO.Close();

            MessageBox.Show("Lưu thành công!");

            LoadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DAO.Connect();

            string sql = @"UPDATE tblKhachHang
                   SET ten_khach_hang = @tenkh,
                       so_dien_thoai = @sdt,
                       dia_chi = @diachi
                   WHERE ma_khach_hang = @makh";

            SqlCommand cmd =
                new SqlCommand(sql, DAO.con);

            cmd.Parameters.AddWithValue("@makh", txtMaKH.Text);
            cmd.Parameters.AddWithValue("@tenkh", txtTenKH.Text);
            cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
            cmd.Parameters.AddWithValue("@diachi", txtDiaChi.Text);

            cmd.ExecuteNonQuery();

            DAO.Close();

            MessageBox.Show("Sửa thành công!");

            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show(
         "Bạn có muốn xóa không?",
         "Thông báo",
         MessageBoxButtons.YesNo,
         MessageBoxIcon.Question);

            if (rs == DialogResult.Yes)
            {
                DAO.Connect();

                string sql = @"DELETE FROM tblKhachHang
                       WHERE ma_khach_hang = @makh";

                SqlCommand cmd =
                    new SqlCommand(sql, DAO.con);

                cmd.Parameters.AddWithValue("@makh", txtMaKH.Text);

                cmd.ExecuteNonQuery();

                DAO.Close();

                MessageBox.Show("Xóa thành công!");

                LoadData();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();

            txtTenKH.Focus();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            DAO.Connect();

            string tukhoa = txtTimKiem.Text.Trim();

            string sql = @"
        SELECT *
        FROM tblKhachHang
        WHERE ma_khach_hang LIKE N'%" + tukhoa + @"%'
        OR ten_khach_hang LIKE N'%" + tukhoa + @"%'
        OR so_dien_thoai LIKE N'%" + tukhoa + @"%'";

            dgvKhachHang.DataSource =
                DAO.LoadDataToTable(sql);

            DAO.Close();
        }

    
    }
}
