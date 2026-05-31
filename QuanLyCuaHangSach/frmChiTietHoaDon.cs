using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmChiTietHoaDon : Form
    {
        private string _maHD;
        private bool _isNhap;

        public frmChiTietHoaDon(string maHD, bool isNhap)
        {
            InitializeComponent();
            _maHD = maHD;
            _isNhap = isNhap;
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
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
                txtNgay.Text = Convert.ToDateTime(r[_isNhap ? "ngay_nhap" : "ngay_dat"]).ToShortDateString();
                txtNhanVien.Text = r["ho_ten"].ToString();
                txtDoiTac.Text = r["TenDoiTac"].ToString();
                txtTongTien.Text = Convert.ToDouble(r["tong_tien"]).ToString("N0") + " VNĐ";
                cboTrangThai.Text = r["trang_thai"].ToString();
                txtGhiChu.Text = r["ghi_chu"].ToString();

                // Nếu đã trả hàng thì không cho sửa nữa
                if (cboTrangThai.Text == "Trả hàng") btnLuu.Enabled = false;
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
            DataTable dt = DAO.LoadDataToTable(sql);

            // Thêm cột STT vào DataTable
            if (!dt.Columns.Contains("STT"))
                dt.Columns.Add("STT", typeof(int)).SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++) dt.Rows[i]["STT"] = i + 1;

            dgvChiTiet.DataSource = dt;

            // Đổi tên cột hiển thị tiếng Việt
            dgvChiTiet.Columns["STT"].HeaderText = "STT";
            dgvChiTiet.Columns["ma_sach"].HeaderText = "Mã sách";
            dgvChiTiet.Columns["ten_sach"].HeaderText = "Tên sách";
            dgvChiTiet.Columns["so_luong"].HeaderText = "Số lượng";
            dgvChiTiet.Columns[giaCol].HeaderText = _isNhap ? "Giá nhập" : "Đơn giá";
            dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành tiền";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DAO.Connect();
            SqlTransaction trans = DAO.con.BeginTransaction();
            try
            {
                string table = _isNhap ? "tblPhieuNhap" : "tblDonHang";
                string idCol = _isNhap ? "ma_phieu_nhap" : "ma_don_hang";

                // 1. Nếu chuyển sang Trả hàng -> Cập nhật tồn kho
                if (cboTrangThai.Text == "Trả hàng")
                {
                    DataTable dtCT = (DataTable)dgvChiTiet.DataSource;
                    foreach (DataRow row in dtCT.Rows)
                    {
                        int sl = Convert.ToInt32(row["so_luong"]);
                        string operatorSign = _isNhap ? "-" : "+"; // Nhập trả NCC: - kho. Bán khách trả: + kho.
                        string sqlStock = $"UPDATE tblSach SET so_luong = so_luong {operatorSign} @sl WHERE ma_sach = @ms";
                        SqlCommand cmdStock = new SqlCommand(sqlStock, DAO.con, trans);
                        cmdStock.Parameters.AddWithValue("@sl", sl);
                        cmdStock.Parameters.AddWithValue("@ms", row["ma_sach"]);
                        cmdStock.ExecuteNonQuery();
                    }
                }

                // 2. Cập nhật trạng thái và ghi chú
                string sqlUpdate = $"UPDATE {table} SET trang_thai = @tt, ghi_chu = @gc WHERE {idCol} = @ma";
                SqlCommand cmd = new SqlCommand(sqlUpdate, DAO.con, trans);
                cmd.Parameters.AddWithValue("@tt", cboTrangThai.Text);
                cmd.Parameters.AddWithValue("@gc", txtGhiChu.Text);
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