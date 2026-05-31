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
    public partial class frmDSHoaDonXuat : Form
    {
        public frmDSHoaDonXuat()
        {
            InitializeComponent();
        }

        private void frmDSHoaDonXuat_Load(object sender, EventArgs e)
        {
            dtDenNgay.Value = DateTime.Now;
            LoadCombos();
            Load_DataGridView();

            // Attach event handlers when the form loads so buttons are wired like the import form.
            try
            {
                this.btnChiTiet.Click += btnChiTiet_Click;
                this.btnIn.Click += btnIn_Click;
                // Export button may not be present; skip wiring.

                // If there's a "Refresh" / "Lam Moi" button, wire it to clear filters and reload.
                try
                {
                    this.btnLamMoi.Click += (s, ev) =>
                    {
                        try { txtTimKiem.Clear(); } catch { }
                        try { cboTrangThaiLoc.SelectedIndex = 0; } catch { }
                        try { cboNCCLoc.SelectedIndex = -1; } catch { }
                        Load_DataGridView();
                    };
                }
                catch { }
            }
            catch { }
        }

        public void frmDSHoaDonXuat_LoadedFinish()
        {
            // kept for compatibility; handler wiring is performed in frmDSHoaDonXuat_Load
        }

        private void LoadCombos()
        {
            DAO.Connect();
            // Fill customers combo if present
            try { DAO.FillDataToCombo("SELECT ma_khach_hang, ten_khach_hang FROM tblKhachHang", cboNCCLoc, "ma_khach_hang", "ten_khach_hang"); } catch { }
            DAO.Close();
        }

        private void Load_DataGridView()
        {
            string sql = @"SELECT dh.ma_don_hang, dh.ngay_dat, kh.ten_khach_hang, nv.ho_ten, dh.tong_tien, dh.trang_thai
                           FROM tblDonHang dh
                           LEFT JOIN tblKhachHang kh ON dh.ma_khach_hang = kh.ma_khach_hang
                           LEFT JOIN tblNhanVien nv ON dh.ma_nhan_vien = nv.ma_nhan_vien
                           WHERE 1=1";

            if (!string.IsNullOrEmpty(txtTimKiem.Text)) sql += $" AND (dh.ma_don_hang LIKE '%{txtTimKiem.Text}%' OR kh.ten_khach_hang LIKE N'%{txtTimKiem.Text}%')";
            if (cboTrangThaiLoc.SelectedIndex > 0) sql += $" AND dh.trang_thai = N'{cboTrangThaiLoc.Text}'";

            // Sử dụng CAST để loại bỏ phần giờ, tránh lỗi lệch ngày khi dùng BETWEEN
            sql += $" AND CAST(dh.ngay_dat AS DATE) BETWEEN '{dtTuNgay.Value:yyyy-MM-dd}' AND '{dtDenNgay.Value:yyyy-MM-dd}'";
            sql += " ORDER BY dh.ngay_dat DESC";

            DAO.Connect();
            DataTable dt = DAO.LoadDataToTable(sql);

            // Kiểm tra dữ liệu tại tầng code (Debug)
            // MessageBox.Show("Số dòng lấy được: " + dt.Rows.Count);

            dgvDSHoaDon.DataSource = dt;
            double tong = 0;
            foreach (DataRow r in dt.Rows) tong += Convert.ToDouble(r["tong_tien"]);
            try
            {
                var lbl = this.Controls.Find("lblTongDoanhThu", true).FirstOrDefault() as System.Windows.Forms.Label;
                if (lbl != null) lbl.Text = $"Tổng doanh thu: {tong:N0} VNĐ";
            }
            catch { }
            DAO.Close();
        }

        private void dgvDSHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string maHD = dgvDSHoaDon.Rows[e.RowIndex].Cells["colMaDon"].Value.ToString();
            frmChiTietHoaDonXuat f = new frmChiTietHoaDonXuat(maHD, false);
            f.ShowDialog();
            Load_DataGridView();
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvDSHoaDon.CurrentRow == null) return;
            string maHD = dgvDSHoaDon.CurrentRow.Cells["colMaDon"].Value.ToString();
            var f = new frmChiTietHoaDonXuat(maHD, false);
            f.ShowDialog();
            Load_DataGridView();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (dgvDSHoaDon.CurrentRow == null) return;
            string maHD = dgvDSHoaDon.CurrentRow.Cells["colMaDon"].Value.ToString();
            var f = new frmChiTietHoaDonXuat(maHD, false);
            f.ShowDialog();
        }


    }
}