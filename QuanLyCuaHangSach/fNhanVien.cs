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

            string sql = "SELECT * FROM tblNhanVien";

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

        }
        private string TaoMaNhanVien()
        {
            string ma = "EMP001";

            DAO.Connect();

            string sql = @"
        SELECT TOP 1 ma_nhan_vien
        FROM tblNhanVien
        ORDER BY ma_nhan_vien DESC";

            string maCuoi = DAO.getFieldValue(sql);

            DAO.Close();

            if (maCuoi != "")
            {
                int so = int.Parse(maCuoi.Substring(3)) + 1;
                ma = "EMP" + so.ToString("000");
            }

            return ma;
        }
        private void fNhanVien_Load(object sender, EventArgs e)
        {
            cboVaiTro.Items.Add("Nhân viên quản lí");
            cboVaiTro.Items.Add("Nhân viên bán sách");
            cboVaiTro.Items.Add("Nhân viên kho");

            cboTrangThai.Items.Add("Đang làm");
            cboTrangThai.Items.Add("Nghỉ việc");

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
            btnLamMoi_Click(sender, e);
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

            string checkSql = @"SELECT COUNT(*)
                    FROM tblNhanVien
                    WHERE ten_dang_nhap = @tk";

            SqlCommand checkCmd =
                new SqlCommand(checkSql, DAO.con);

            checkCmd.Parameters.AddWithValue(
                "@tk", txtTenDangNhap.Text.Trim());

            int count = (int)checkCmd.ExecuteScalar();

            if (count > 0)
            {
                MessageBox.Show(
                    "Tên đăng nhập đã tồn tại, vui lòng nhập tên khác!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtTenDangNhap.Focus();
                txtTenDangNhap.SelectAll();

                DAO.Close();
                return;

                string sql = @"INSERT INTO tblNhanVien
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
                btnLamMoi_Click(sender, e);
            }
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

            string sql = @"UPDATE tblNhanVien
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
                    @"DELETE FROM tblNhanVien
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
                           FROM tblNhanVien
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
        private void dgvNhanVien_CellFormatting( object sender,DataGridViewCellFormattingEventArgs e)
        {
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "trang_thai")
            {
                if (e.Value != null)
                {
                    if (e.Value.ToString() == "Đang làm")
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
