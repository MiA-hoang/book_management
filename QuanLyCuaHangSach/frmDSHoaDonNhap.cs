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
    public partial class frmDSHoaDonNhap : Form
    {
        public frmDSHoaDonNhap()
        {
            InitializeComponent();
        }

        private void frmDSHoaDonNhap_Load(object sender, EventArgs e)
        {
            dtDenNgay.Value = DateTime.Now;
            LoadCombos();
            Load_DataGridView();
        }

        private void LoadCombos()
        {
            DAO.Connect();
            DAO.FillDataToCombo("SELECT ma_nha_cung_cap, ten_nha_cung_cap FROM tblNhaCungCap", cboNCCLoc, "ma_nha_cung_cap", "ten_nha_cung_cap");
            DAO.Close();
            cboNCCLoc.SelectedIndex = -1;
        }

        private void Load_DataGridView()
        {
            string sql = @"SELECT pn.ma_phieu_nhap, pn.ngay_nhap, ncc.ten_nha_cung_cap, nv.ho_ten, pn.tong_tien, pn.trang_thai
                           FROM tblPhieuNhap pn
                           LEFT JOIN tblNhaCungCap ncc ON pn.ma_nha_cung_cap = ncc.ma_nha_cung_cap
                           LEFT JOIN tblNhanVien nv ON pn.ma_nhan_vien = nv.ma_nhan_vien
                           WHERE 1=1";

            if (!string.IsNullOrEmpty(txtTimKiem.Text))
                sql += $" AND pn.ma_phieu_nhap LIKE '%{txtTimKiem.Text}%'";
            if (cboTrangThaiLoc.SelectedIndex > 0)
                sql += $" AND pn.trang_thai = N'{cboTrangThaiLoc.Text}'";
            if (cboNCCLoc.SelectedIndex != -1 && cboNCCLoc.SelectedValue != null)
                sql += $" AND pn.ma_nha_cung_cap = {cboNCCLoc.SelectedValue}";

            sql += $" AND CAST(pn.ngay_nhap AS DATE) BETWEEN '{dtTuNgay.Value:yyyy-MM-dd}' AND '{dtDenNgay.Value:yyyy-MM-dd}'";
            sql += " ORDER BY pn.ngay_nhap DESC";

            DAO.Connect();
            DataTable dt = DAO.LoadDataToTable(sql);


            // MessageBox.Show("Số dòng nhập: " + dt.Rows.Count);

            dgvDSHoaDon.DataSource = dt;
            SetLabelTextByName("lblTongDon", $"Tổng số hóa đơn: {dt.Rows.Count}");
            double tong = 0;
            foreach (DataRow r in dt.Rows) tong += Convert.ToDouble(r["tong_tien"]);
            SetLabelTextByName("lblTongTien", $"Tổng tiền nhập: {tong:N0} VNĐ");
            DAO.Close();
        }

        private void SetLabelTextByName(string name, string text)
        {
            try
            {
                var ctl = this.Controls.Find(name, true).FirstOrDefault() as System.Windows.Forms.Label;
                if (ctl != null) ctl.Text = text;
            }
            catch { }
        }

        private void btnTimKiem_Click(object sender, EventArgs e) => Load_DataGridView();
        private void btnLamMoi_Click(object sender, EventArgs e) { txtTimKiem.Clear(); cboTrangThaiLoc.SelectedIndex = 0; cboNCCLoc.SelectedIndex = -1; Load_DataGridView(); }

        private void dgvDSHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string maHD = dgvDSHoaDon.Rows[e.RowIndex].Cells["colMaPhieu"].Value.ToString();
            frmChiTietHoaDonNhap f = new frmChiTietHoaDonNhap(maHD, true); // true = Nhập
            f.ShowDialog();
            Load_DataGridView();
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvDSHoaDon.CurrentRow == null) return;
            string maHD = dgvDSHoaDon.CurrentRow.Cells["colMaPhieu"].Value.ToString();
            var f = new frmChiTietHoaDonNhap(maHD, true);
            f.ShowDialog();
            Load_DataGridView();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // Open detail form where user can print
            if (dgvDSHoaDon.CurrentRow == null) return;
            string maHD = dgvDSHoaDon.CurrentRow.Cells["colMaPhieu"].Value.ToString();
            var f = new frmChiTietHoaDonNhap(maHD, true);
            f.ShowDialog();
        }

  
    }
}