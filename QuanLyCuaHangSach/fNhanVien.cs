using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class fNhanVien : Form
    {
        public fNhanVien()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            DAO.Connect();

            string sql = "SELECT * FROM NHAN_VIEN";

            dgvNhanVien.DataSource =
                DAO.LoadDataToTable(sql);

            DAO.Close();

            // ===== ĐỔI TÊN CỘT =====
            dgvNhanVien.Columns["ma_nhan_vien"].HeaderText = "Mã NV";
            dgvNhanVien.Columns["ten_dang_nhap"].HeaderText = "Tên đăng nhập";
            dgvNhanVien.Columns["mat_khau"].HeaderText = "Mật khẩu";
            dgvNhanVien.Columns["ho_ten"].HeaderText = "Họ tên";
            dgvNhanVien.Columns["vai_tro"].HeaderText = "Vai trò";
            dgvNhanVien.Columns["trang_thai"].HeaderText = "Trạng thái";

            // ===== ẨN PASSWORD =====
            dgvNhanVien.Columns["mat_khau"].Visible = false;

            // ===== CHỈNH SIZE =====
            dgvNhanVien.Columns["ma_nhan_vien"].FillWeight = 15;
            dgvNhanVien.Columns["ten_dang_nhap"].FillWeight = 20;
            dgvNhanVien.Columns["ho_ten"].FillWeight = 25;
            dgvNhanVien.Columns["vai_tro"].FillWeight = 15;
            dgvNhanVien.Columns["trang_thai"].FillWeight = 15;
        }
        private string TaoMaNhanVien()
        {
            string ma = "NV01";

            DAO.Connect();
            string sql = @"SELECT TOP 1 ma_nhan_vien
                           FROM NHAN_VIEN
                           ORDER BY ma_nhan_vien DESC";
            string maCuoi = DAO.getFieldValue(sql);
            DAO.Close();

            if (maCuoi != "")
            {
                int so = int.Parse(maCuoi.Substring(2)) + 1;
                ma = "NV" + so.ToString("00");
            }

            return ma;
        }
        private void fNhanVien_Load(object sender, EventArgs e)
        {
            cboVaiTro.Items.Add("Quản trị");
            cboVaiTro.Items.Add("Bán hàng");
            cboVaiTro.Items.Add("Kho");

            cboTrangThai.Items.Add("Hoạt động");
            cboTrangThai.Items.Add("Khóa");

            // ===== CẤU HÌNH DGV =====
            dgvNhanVien.Theme =
                Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Light;

            dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor =
                Color.White;

            dgvNhanVien.CellBorderStyle =
    DataGridViewCellBorderStyle.None;

            dgvNhanVien.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvNhanVien.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor =
    Color.FromArgb(248, 250, 252);

            dgvNhanVien.MultiSelect = false;

            dgvNhanVien.AllowUserToAddRows = false;

            dgvNhanVien.RowHeadersVisible = false;

            dgvNhanVien.RowTemplate.Height = 35;

            dgvNhanVien.ColumnHeadersHeight = 40;

            dgvNhanVien.ReadOnly = true;

            dgvNhanVien.CellFormatting += dgvNhanVien_CellFormatting;

           
            // ===== MÀU NÚT =====
            btnThem.FillColor =
                Color.FromArgb(99, 102, 241);

            btnSua.FillColor =
                Color.FromArgb(37, 99, 235);

            btnXoa.FillColor =
                Color.FromArgb(239, 68, 68);

            btnLamMoi.FillColor =
                Color.FromArgb(249, 115, 22);

            btnLuu.FillColor =
                Color.FromArgb(34, 197, 94);

            // ===== SEARCH =====
            txtTimKiem.PlaceholderText =
                "Tìm theo mã, tên...";

            txtMaNV.Enabled = false;

            LoadData();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row =
                    dgvNhanVien.Rows[e.RowIndex];

                txtMaNV.Text =
                    row.Cells["ma_nhan_vien"].Value.ToString();

                txtTenDangNhap.Text =
                    row.Cells["ten_dang_nhap"].Value.ToString();

                txtMatKhau.Text =
                    row.Cells["mat_khau"].Value.ToString();

                txtTenNV.Text =
                    row.Cells["ho_ten"].Value.ToString();

                cboVaiTro.Text =
                    row.Cells["vai_tro"].Value.ToString();

                cboTrangThai.Text =
                    row.Cells["trang_thai"].Value.ToString();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();

            cboVaiTro.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = -1;



            txtTenNV.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = TaoMaNhanVien();

            txtTenNV.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            cboVaiTro.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = -1;

            txtTenNV.Focus();
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "" ||
                txtTenNV.Text == "" ||
                txtTenDangNhap.Text == "" ||
                txtMatKhau.Text == "" ||
                cboVaiTro.Text == "" ||
                cboTrangThai.Text == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin!");

                return;
            }

            DAO.Connect();

            string sql = @"INSERT INTO NHAN_VIEN
                           VALUES
                           (@manv,@tk,@mk,@hoten,@vaitro,@trangthai)";

            SqlCommand cmd =
                new SqlCommand(sql, DAO.con);

            cmd.Parameters.AddWithValue(
                "@manv", txtMaNV.Text);

            cmd.Parameters.AddWithValue(
                "@tk", txtTenDangNhap.Text);

            cmd.Parameters.AddWithValue(
                "@mk", txtMatKhau.Text);

            cmd.Parameters.AddWithValue(
                "@hoten", txtTenNV.Text);

            cmd.Parameters.AddWithValue(
                "@vaitro", cboVaiTro.Text);

            cmd.Parameters.AddWithValue(
                "@trangthai", cboTrangThai.Text);

            cmd.ExecuteNonQuery();

            DAO.Close();

            MessageBox.Show("Lưu thành công!");

            LoadData();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show(
                    "Vui lòng chọn nhân viên!");

                return;
            }

            DAO.Connect();

            string sql = @"UPDATE NHAN_VIEN
                           SET ten_dang_nhap = @tk,
                               mat_khau = @mk,
                               ho_ten = @hoten,
                               vai_tro = @vaitro,
                               trang_thai = @trangthai
                           WHERE ma_nhan_vien = @manv";

            SqlCommand cmd =
                new SqlCommand(sql, DAO.con);

            cmd.Parameters.AddWithValue(
                "@manv", txtMaNV.Text);

            cmd.Parameters.AddWithValue(
                "@tk", txtTenDangNhap.Text);

            cmd.Parameters.AddWithValue(
                "@mk", txtMatKhau.Text);

            cmd.Parameters.AddWithValue(
                "@hoten", txtTenNV.Text);

            cmd.Parameters.AddWithValue(
                "@vaitro", cboVaiTro.Text);

            cmd.Parameters.AddWithValue(
                "@trangthai", cboTrangThai.Text);

            cmd.ExecuteNonQuery();

            DAO.Close();

            MessageBox.Show("Sửa thành công!");

            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show(
                    "Vui lòng chọn nhân viên!");

                return;
            }

            DialogResult rs = MessageBox.Show(
                "Bạn có muốn xóa không?",
                "Thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (rs == DialogResult.Yes)
            {
                DAO.Connect();

                string sql =
                    @"DELETE FROM NHAN_VIEN
                      WHERE ma_nhan_vien = @manv";

                SqlCommand cmd =
                    new SqlCommand(sql, DAO.con);

                cmd.Parameters.AddWithValue(
                    "@manv", txtMaNV.Text);

                cmd.ExecuteNonQuery();

                DAO.Close();

                MessageBox.Show("Xóa thành công!");

                LoadData();

                btnLamMoi_Click(sender, e);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tukhoa =
                txtTimKiem.Text.Trim();

            DAO.Connect();

            string sql = @"SELECT *
                           FROM NHAN_VIEN
                           WHERE ma_nhan_vien
                           LIKE N'%" + tukhoa + @"%'

                           OR ho_ten
                           LIKE N'%" + tukhoa + @"%'

                           OR ten_dang_nhap
                           LIKE N'%" + tukhoa + @"%'";

            dgvNhanVien.DataSource =
                DAO.LoadDataToTable(sql);

            DAO.Close();
        }
        private void dgvNhanVien_CellFormatting(
    object sender,
    DataGridViewCellFormattingEventArgs e)
        {
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "trang_thai")
            {
                if (e.Value != null)
                {
                    if (e.Value.ToString() == "Hoạt động")
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(34, 197, 94);
                        e.CellStyle.SelectionForeColor = Color.FromArgb(34, 197, 94);
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68);
                        e.CellStyle.SelectionForeColor = Color.FromArgb(239, 68, 68);
                    }
                }
            }
        }
    }
}
