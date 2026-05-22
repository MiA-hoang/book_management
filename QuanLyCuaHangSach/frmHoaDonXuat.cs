using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmHoaDonXuat : Form
    {
        public frmHoaDonXuat()
        {
            InitializeComponent();
        }

        private void FillCombos()
        {
            DAO.Connect();
            DAO.FillDataToCombo("SELECT ma_khach_hang, ten_khach_hang FROM KHACH_HANG", cboKhachHang, "ma_khach_hang", "ten_khach_hang");
            DAO.FillDataToCombo("SELECT ma_sach, ten_sach FROM SACH", cboSach, "ma_sach", "ten_sach");
            DAO.Close();
        }

        private void frmHoaDonXuat_Load(object sender, EventArgs e)
        {
            txtMaHDX.Enabled = false;
            dtNgayXuat.Value = DateTime.Now;
            txtTongTien.Text = "0";
            btnTaoMoi.FillColor = Color.FromArgb(96, 165, 250);
            btnThem.FillColor = Color.FromArgb(59, 130, 246);
            btnLuu.FillColor = Color.FromArgb(37, 99, 235);
            btnXoa.FillColor = Color.FromArgb(71, 85, 105);
            btnIn.FillColor = Color.FromArgb(30, 58, 138);
            btnThoat.FillColor = Color.FromArgb(51, 65, 85);
            FillCombos();
        }

        private void cboSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSach.SelectedIndex != -1 && cboSach.SelectedValue is string)
            {
                DAO.Connect();
                string sql = "SELECT gia_ban FROM SACH WHERE ma_sach = N'" + cboSach.SelectedValue.ToString() + "'";
                txtGiaBan.Text = DAO.getFieldValue(sql);
                DAO.Close();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboSach.SelectedIndex == -1 || string.IsNullOrEmpty(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng chọn sách và nhập số lượng!");
                return;
            }

            string maSach = cboSach.SelectedValue.ToString();
            int slMua = int.Parse(txtSoLuong.Text);

            // KIỂM TRA TỒN KHO
            DAO.Connect();
            string sqlCheck = "SELECT so_luong FROM SACH WHERE ma_sach = N'" + maSach + "'";
            int tonKho = int.Parse(DAO.getFieldValue(sqlCheck));
            DAO.Close();

            if (slMua > tonKho)
            {
                MessageBox.Show($"Không đủ hàng! Hiện chỉ còn {tonKho} cuốn trong kho.");
                return;
            }

            double gia = double.Parse(txtGiaBan.Text);
            double thanhTien = slMua * gia;

            // Kiểm tra trùng mã trong DataGridView
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.Cells[0].Value.ToString() == maSach)
                {
                    int slMoi = int.Parse(row.Cells[2].Value.ToString()) + slMua;
                    if (slMoi > tonKho)
                    {
                        MessageBox.Show("Tổng số lượng trong đơn hàng vượt quá tồn kho!");
                        return;
                    }
                    row.Cells[2].Value = slMoi;
                    row.Cells[4].Value = slMoi * gia;
                    TinhTongTien();
                    return;
                }
            }

            dgvChiTiet.Rows.Add(maSach, cboSach.Text, slMua, gia, thanhTien);
            TinhTongTien();
        }

        private void TinhTongTien()
        {
            double tong = 0;
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
                tong += Convert.ToDouble(row.Cells[4].Value);
            txtTongTien.Text = tong.ToString();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaHDX.Text = "HDX" + DateTime.Now.ToString("ddMMyyHHmmss");
            dgvChiTiet.Rows.Clear();
            txtTongTien.Text = "0";
            cboKhachHang.SelectedIndex = -1;
            cboSach.SelectedIndex = -1;
            txtSoLuong.Clear();
            txtGiaBan.Clear();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.Rows.Count == 0 || cboKhachHang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng và sản phẩm!");
                return;
            }

            DAO.Connect();
            SqlTransaction trans = DAO.con.BeginTransaction();
            try
            {
                // 1. Lưu Header
                string sqlHD = "INSERT INTO HOA_DON_XUAT(ma_hd_xuat, ma_khach_hang, ngay_xuat, tong_tien) VALUES (@ma, @kh, @ngay, @tien)";
                SqlCommand cmdHD = new SqlCommand(sqlHD, DAO.con, trans);
                cmdHD.Parameters.AddWithValue("@ma", txtMaHDX.Text);
                cmdHD.Parameters.AddWithValue("@kh", cboKhachHang.SelectedValue);
                cmdHD.Parameters.AddWithValue("@ngay", dtNgayXuat.Value);
                cmdHD.Parameters.AddWithValue("@tien", txtTongTien.Text);
                cmdHD.ExecuteNonQuery();

                // 2. Lưu Details và Cập nhật Kho
                foreach (DataGridViewRow row in dgvChiTiet.Rows)
                {
                    string sqlCT = "INSERT INTO CHI_TIET_HD_XUAT(ma_hd_xuat, ma_sach, so_luong, gia_ban) VALUES (@ma, @ms, @sl, @gb)";
                    SqlCommand cmdCT = new SqlCommand(sqlCT, DAO.con, trans);
                    cmdCT.Parameters.AddWithValue("@ma", txtMaHDX.Text);
                    cmdCT.Parameters.AddWithValue("@ms", row.Cells[0].Value);
                    cmdCT.Parameters.AddWithValue("@sl", row.Cells[2].Value);
                    cmdCT.Parameters.AddWithValue("@gb", row.Cells[3].Value);
                    cmdCT.ExecuteNonQuery();

                    // CẬP NHẬT TRỪ KHO
                    string sqlUpdateStock = "UPDATE SACH SET so_luong = so_luong - @sl WHERE ma_sach = @ms";
                    SqlCommand cmdStock = new SqlCommand(sqlUpdateStock, DAO.con, trans);
                    cmdStock.Parameters.AddWithValue("@sl", row.Cells[2].Value);
                    cmdStock.Parameters.AddWithValue("@ms", row.Cells[0].Value);
                    cmdStock.ExecuteNonQuery();
                }

                trans.Commit();
                MessageBox.Show("Lưu hóa đơn bán thành công!");
                btnTaoMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally { DAO.Close(); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count > 0)
            {
                dgvChiTiet.Rows.RemoveAt(dgvChiTiet.SelectedRows[0].Index);
                TinhTongTien();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
