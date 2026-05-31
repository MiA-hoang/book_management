using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmChiTietHoaDonXuat : Form
    {
        private string _maHD;
        private bool _isNhap;

        public frmChiTietHoaDonXuat(string maHD, bool isNhap)
        {
            InitializeComponent();
            _maHD = maHD;
            _isNhap = isNhap;
            this.Load += frmChiTietHoaDonXuat_Load;
            this.btnCapNhat.Click += btnCapNhat_Click;
            this.btnDong.Click += (s, ev) => this.Close();

        }

        private void frmChiTietHoaDonXuat_Load(object sender, EventArgs e)
        {
            LoadHeader();
            LoadDetails();
        }

        private void LoadHeader()
        {
            DAO.Connect();
            string table = _isNhap ? "tblPhieuNhap" : "tblDonHang";
            string idCol = _isNhap ? "ma_phieu_nhap" : "ma_don_hang";
            string partyJoin = _isNhap ? "JOIN tblNhaCungCap p ON h.ma_nha_cung_cap = p.ma_nha_cung_cap" : "JOIN tblKhachHang p ON h.ma_khach_hang = p.ma_khach_hang";

            string sql = $@"SELECT h.*, nv.ho_ten, p.{(_isNhap ? "ten_nha_cung_cap" : "ten_khach_hang")} as TenDoiTac 
                            FROM {table} h 
                            JOIN tblNhanVien nv ON h.ma_nhan_vien = nv.ma_nhan_vien 
                            {partyJoin} 
                            WHERE h.{idCol} = '{_maHD}'";

            DataTable dt = DAO.LoadDataToTable(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                txtMaHD.Text = _maHD;
                dtpNgay.Value = Convert.ToDateTime(r[_isNhap ? "ngay_nhap" : "ngay_dat"]);
                txtNhanVien.Text = r["ho_ten"].ToString();
                txtKhachHang.Text = r["TenDoiTac"].ToString();
                txtTongTienHeader.Text = Convert.ToDouble(r["tong_tien"]).ToString("N0") + " VNĐ";
                cboTrangThai.Text = r["trang_thai"].ToString();

                if (cboTrangThai.Text == "Trả hàng") btnCapNhat.Enabled = false;
            }
            DAO.Close();
        }

        private void LoadDetails()
        {
            string table = _isNhap ? "tblChiTietNhap" : "tblChiTietDonHang";
            string idCol = _isNhap ? "ma_phieu_nhap" : "ma_don_hang";
            string giaCol = _isNhap ? "gia_nhap" : "gia_ban";
            string sql = $@"SELECT ct.ma_sach, s.ten_sach, ct.so_luong, ct.{giaCol}, (ct.so_luong * ct.{giaCol}) as ThanhTien 
                            FROM {table} ct 
                            JOIN tblSach s ON ct.ma_sach = s.ma_sach 
                            WHERE ct.{idCol} = '{_maHD}'";

            dgvChiTiet.AutoGenerateColumns = false;
            colDonGia.DataPropertyName = giaCol;
            colDonGia.HeaderText = _isNhap ? "Giá nhập" : "Giá bán";

            DataTable dt = DAO.LoadDataToTable(sql);
            dgvChiTiet.DataSource = dt;
            for (int i = 0; i < dgvChiTiet.Rows.Count; i++) dgvChiTiet.Rows[i].Cells["colSTT"].Value = i + 1;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            DAO.Connect();
            SqlTransaction trans = DAO.con.BeginTransaction();
            try
            {
                string table = _isNhap ? "tblPhieuNhap" : "tblDonHang";
                string idCol = _isNhap ? "ma_phieu_nhap" : "ma_don_hang";

                if (cboTrangThai.Text == "Trả hàng")
                {
                    DataTable dtCT = (DataTable)dgvChiTiet.DataSource;
                    foreach (DataRow row in dtCT.Rows)
                    {
                        int sl = Convert.ToInt32(row["so_luong"]);
                        string operatorSign = _isNhap ? "-" : "+";
                        string sqlStock = $"UPDATE tblSach SET so_luong = so_luong {operatorSign} @sl WHERE ma_sach = @ms";
                        SqlCommand cmdStock = new SqlCommand(sqlStock, DAO.con, trans);
                        cmdStock.Parameters.AddWithValue("@sl", sl);
                        cmdStock.Parameters.AddWithValue("@ms", row["ma_sach"]);
                        cmdStock.ExecuteNonQuery();
                    }
                }

                string sqlUpdate = $"UPDATE {table} SET trang_thai = @tt WHERE {idCol} = @ma";
                SqlCommand cmd = new SqlCommand(sqlUpdate, DAO.con, trans);
                cmd.Parameters.AddWithValue("@tt", cboTrangThai.Text);
                cmd.Parameters.AddWithValue("@ma", _maHD);
                cmd.ExecuteNonQuery();

                trans.Commit();
                MessageBox.Show("Cập nhật hóa đơn thành công!");
                this.Close();
            }
            catch (Exception ex) { trans.Rollback(); MessageBox.Show("Lỗi: " + ex.Message); }
            finally { DAO.Close(); }
        }
    }
}
