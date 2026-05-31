using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmHoaDonNhap : Form
    {
        private PrintDocument printDocument;
        private string _maNV, _tenNV;
        // Parameterless ctor for callers that don't pass employee info
        public frmHoaDonNhap() : this(string.Empty, string.Empty) { }

        public frmHoaDonNhap(string maNV, string tenNV)
        {
            InitializeComponent();
            this._maNV = maNV;
            this._tenNV = tenNV;
            printDocument = new PrintDocument();
        }

        private void FillCombos()
        {
            DAO.Connect();
            DAO.FillDataToCombo("SELECT ma_nha_cung_cap, ten_nha_cung_cap FROM tblNhaCungCap", cboNCC, "ma_nha_cung_cap", "ten_nha_cung_cap");
            DAO.FillDataToCombo("SELECT ma_sach, ten_sach FROM tblSach", cboSach, "ma_sach", "ten_sach");
            DAO.Close();
            cboNCC.SelectedIndex = -1;
            cboSach.SelectedIndex = -1;
        }

        private string TaoMaHoaDon()
        {
            return "HDN" + DateTime.Now.ToString("ddMMyyHHmmss");
        }

        private void frmHoaDonNhap_Load(object sender, EventArgs e)
        {
            txtNhanVien.Text = _tenNV;
            txtNhanVien.ReadOnly = true;
            txtMaHDN.Enabled = false;
            dtNgayNhap.Value = DateTime.Now;
            txtTongTien.Text = "0";

            // Setup colors for buttons
            btnTaoMoi.FillColor = Color.FromArgb(96, 165, 250);
            btnThem.FillColor = Color.FromArgb(59, 130, 246);
            btnLuu.FillColor = Color.FromArgb(37, 99, 235);
            btnXoa.FillColor = Color.FromArgb(71, 85, 105);
            btnIn.FillColor = Color.FromArgb(30, 58, 138);
            btnIn.Click += btnIn_Click;


            FillCombos();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                InvoicePrinter printer = new InvoicePrinter
                {
                    Title = "HÓA ĐƠN NHẬP",
                    InvoiceNumber = txtMaHDN.Text,
                    InvoiceDate = dtNgayNhap.Value,
                    PartyLabel = "Nhà cung cấp",
                    PartyName = cboNCC.Text,
                    Data = dgvChiTiet,
                    TotalText = txtTongTien.Text
                };

                printDocument.PrintPage -= printer.PrintPage;
                printDocument.PrintPage += printer.PrintPage;

                using (PrintPreviewDialog preview = new PrintPreviewDialog())
                {
                    preview.Document = printDocument;
                    preview.Width = 800;
                    preview.Height = 600;
                    preview.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in ấn: " + ex.Message);
            }
        }

        private void TinhTongTien()
        {
            double tong = 0;
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                tong += Convert.ToDouble(row.Cells[4].Value);
            }
            txtTongTien.Text = tong.ToString("N0") + " VNĐ";
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaHDN.Text = TaoMaHoaDon();
            dgvChiTiet.Rows.Clear();
            txtTongTien.Text = "0 VNĐ";
            cboNCC.SelectedIndex = -1;
            dtNgayNhap.Value = DateTime.Now;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboSach.SelectedIndex == -1 || string.IsNullOrEmpty(txtSoLuong.Text) || string.IsNullOrEmpty(txtDonGiaNhap.Text))
            {
                MessageBox.Show("Vui lòng chọn sách và nhập số lượng, đơn giá!");
                return;
            }

            string maSach = cboSach.SelectedValue.ToString();
            string tenSach = cboSach.Text;
            if (!int.TryParse(txtSoLuong.Text, out int sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương!");
                return;
            }
            if (!double.TryParse(txtDonGiaNhap.Text, out double gia) || gia < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ!");
                return;
            }
            double thanhTien = sl * gia;

            // Kiểm tra xem sách đã có trong lưới chưa
            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == maSach)
                {
                    int slCu = Convert.ToInt32(row.Cells[3].Value);
                    row.Cells[3].Value = slCu + sl;
                    row.Cells[5].Value = (slCu + sl) * gia;
                    TinhTongTien();
                    return;
                }
            }

            int stt = dgvChiTiet.Rows.Count + 1;
            dgvChiTiet.Rows.Add(stt, maSach, tenSach, sl, gia, thanhTien);
            TinhTongTien();

            // Reset fields
            cboSach.SelectedIndex = -1;
            txtSoLuong.Clear();
            txtDonGiaNhap.Clear();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.Rows.Count == 0 || cboNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Dữ liệu không hợp lệ để lưu!");
                return;
            }

            DAO.Connect();
            SqlTransaction trans = DAO.con.BeginTransaction();
            try
            {
                // 1. Lưu Header
                string sqlHD = "INSERT INTO tblPhieuNhap(ma_phieu_nhap, ma_nha_cung_cap, ngay_nhap, tong_tien, ma_nhan_vien) VALUES (@ma, @ncc, @ngay, @tien, @manv)";
                SqlCommand cmdHD = new SqlCommand(sqlHD, DAO.con, trans);
                cmdHD.Parameters.AddWithValue("@ma", txtMaHDN.Text);
                cmdHD.Parameters.AddWithValue("@ncc", cboNCC.SelectedValue);
                cmdHD.Parameters.AddWithValue("@ngay", dtNgayNhap.Value);
                cmdHD.Parameters.AddWithValue("@tien", Convert.ToDouble(txtTongTien.Text.Split(' ')[0]));
                cmdHD.Parameters.AddWithValue("@manv", _maNV);
                cmdHD.ExecuteNonQuery();

                // 2. Lưu Details và Cập nhật Kho
                foreach (DataGridViewRow row in dgvChiTiet.Rows)
                {
                    if (row.IsNewRow) continue;
                    string sqlCT = "INSERT INTO tblChiTietNhap(ma_phieu_nhap, ma_sach, so_luong, gia_nhap) VALUES (@ma, @ms, @sl, @dg)";
                    SqlCommand cmdCT = new SqlCommand(sqlCT, DAO.con, trans);
                    cmdCT.Parameters.AddWithValue("@ma", txtMaHDN.Text);
                    cmdCT.Parameters.AddWithValue("@ms", row.Cells[1].Value);
                    cmdCT.Parameters.AddWithValue("@sl", Convert.ToInt32(row.Cells[3].Value));
                    cmdCT.Parameters.AddWithValue("@dg", Convert.ToDouble(row.Cells[4].Value));
                    cmdCT.ExecuteNonQuery();

                    // CẬP NHẬT KHO
                    string sqlUpdateStock = "UPDATE tblSach SET so_luong = so_luong + @sl WHERE ma_sach = @ms";
                    SqlCommand cmdStock = new SqlCommand(sqlUpdateStock, DAO.con, trans);
                    cmdStock.Parameters.AddWithValue("@sl", Convert.ToInt32(row.Cells[3].Value));
                    cmdStock.Parameters.AddWithValue("@ms", row.Cells[1].Value);
                    cmdStock.ExecuteNonQuery();
                }

                trans.Commit();
                MessageBox.Show("Lưu hóa đơn thành công và đã cập nhật tồn kho!");
                btnTaoMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
            finally
            {
                DAO.Close();
            }
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
