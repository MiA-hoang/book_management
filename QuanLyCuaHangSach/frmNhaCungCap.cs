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
    public partial class frmNhaCungCap : Form
    {
        DataTable tblNhaCungCap;
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            DAO.Connect();

            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            cboLocTimKiem.SelectedIndex = 0;

            Load_DataGridView();
            ResetValues();
        }
        private void ResetValues()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            mskSDT.Text = "";
            txtTuKhoa.Text = "";

            txtMaNCC.Enabled = false;
        }
        private void Load_DataGridView()
        {
            string sql = @"
            SELECT ma_nha_cung_cap,
                ten_nha_cung_cap,
                so_dien_thoai,
                dia_chi
            FROM tblNhaCungCap";

            tblNhaCungCap = DAO.LoadDataToTable(sql);

            dgvNCC.DataSource = tblNhaCungCap;      
            dgvNCC.Columns[0].HeaderText = "Mã NCC";
            dgvNCC.Columns[1].HeaderText = "Tên nhà cung cấp";
            dgvNCC.Columns[2].HeaderText = "Số điện thoại";
            dgvNCC.Columns[3].HeaderText = "Địa chỉ";

            dgvNCC.Columns[0].Width = 120;
            dgvNCC.Columns[1].Width = 220;
            dgvNCC.Columns[2].Width = 150;
            dgvNCC.Columns[3].Width = 300;

            dgvNCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvNCC.AllowUserToAddRows = false;

            dgvNCC.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tblNhaCungCap.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }

            txtMaNCC.Text =
                dgvNCC.CurrentRow.Cells[0].Value.ToString();

            txtTenNCC.Text =
                dgvNCC.CurrentRow.Cells[1].Value.ToString();

            mskSDT.Text =
                dgvNCC.CurrentRow.Cells[2].Value.ToString();

            txtDiaChi.Text =
                dgvNCC.CurrentRow.Cells[3].Value.ToString();

            // khóa mã khi sửa
            txtMaNCC.Enabled = false;

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValues();

            btnThem.Enabled = false;
            btnLuu.Enabled = true;

            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtMaNCC.Text = TaoMaNCC();
            txtMaNCC.Enabled = false;

            txtTenNCC.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;

            // kiểm tra mã NCC
            if (txtMaNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show(
                    "Bạn phải nhập mã nhà cung cấp!");

                txtMaNCC.Focus();
                return;
            }

            // kiểm tra tên NCC
            if (txtTenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show(
                    "Bạn phải nhập tên nhà cung cấp!");

                txtTenNCC.Focus();
                return;
            }

            // kiểm tra địa chỉ
            string sdt = new string(mskSDT.Text
                        .Where(char.IsDigit)
                        .ToArray());

            if (sdt == "")
            {
                MessageBox.Show(
                    "Bạn phải nhập số điện thoại!");

                mskSDT.Focus();
                return;
            }

            if (sdt.Length < 10 || sdt.Length > 11)
            {
                MessageBox.Show(
                    "Số điện thoại phải từ 10-11 số!");

                mskSDT.Focus();
                return;
            }

            // kiểm tra SĐT
            if (mskSDT.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Bạn phải nhập số điện thoại!");

                mskSDT.Focus();
                return;
            }

            // kiểm tra mã bị trùng
            sql = @"SELECT ma_nha_cung_cap
            FROM tblNhaCungCap
            WHERE ma_nha_cung_cap = N'"
                    + txtMaNCC.Text.Trim()
                    + "'";

            if (DAO.CheckKey(sql))
            {
                MessageBox.Show(
                    "Mã nhà cung cấp đã tồn tại!");

                txtMaNCC.Focus();
                txtMaNCC.Text = "";

                return;
            }

            // câu lệnh insert
            sql = @"INSERT INTO tblNhaCungCap
                                    (ma_nha_cung_cap,
                                    ten_nha_cung_cap,
                                    so_dien_thoai,
                                    dia_chi)

            VALUES
            (N'" + txtMaNCC.Text.Trim() + @"',
             N'" + txtTenNCC.Text.Trim() + @"',
             N'" + sdt + @"',
             N'" + txtDiaChi.Text.Trim() + "')";

            SqlCommand cmd = new SqlCommand(sql, DAO.con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Thêm nhà cung cấp thành công!");

            Load_DataGridView();
            ResetValues();

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;

            if (tblNhaCungCap.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }

            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn nhà cung cấp!");
                return;
            }

            // kiểm tra tên
            if (txtTenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung cấp!");

                txtTenNCC.Focus();
                return;
            }

            // kiểm tra địa chỉ
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ!");

                txtDiaChi.Focus();
                return;
            }

            // kiểm tra SĐT
            string sdt = new string(mskSDT.Text
                        .Where(char.IsDigit)
                        .ToArray());

            if (sdt == "")
            {
                MessageBox.Show(
                    "Bạn phải nhập số điện thoại!");

                mskSDT.Focus();
                return;
            }

            if (sdt.Length < 10 || sdt.Length > 11)
            {
                MessageBox.Show(
                    "Số điện thoại phải từ 10-11 số!");

                mskSDT.Focus();
                return;
            }

            if (MessageBox.Show("Bạn có muốn sửa thông tin nhà cung cấp này không?",
                                "Xác nhận",
             MessageBoxButtons.YesNo,
             MessageBoxIcon.Question)
             == DialogResult.No)
            {
                return;
            }

            sql = @"UPDATE tblNhaCungCap SET
            ten_nha_cung_cap = N'" + txtTenNCC.Text.Trim() + @"',
            so_dien_thoai = N'" + sdt + @"',
            dia_chi = N'" + txtDiaChi.Text.Trim() + @"'

            WHERE ma_nha_cung_cap = N'" + txtMaNCC.Text + "'";

            SqlCommand cmd = new SqlCommand(sql, DAO.con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Sửa thành công!");

            Load_DataGridView();
            ResetValues();

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;

            if (tblNhaCungCap.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }

            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn nhà cung cấp cần xóa!");
                return;
            }

            // hỏi xác nhận
            if (MessageBox.Show("Bạn có chắc muốn xóa nhà cung cấp này không?",
                                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                == DialogResult.No)
            {
                return;
            }

            sql = @"DELETE FROM tblNhaCungCap
            WHERE ma_nha_cung_cap = N'" + txtMaNCC.Text + "'";

            SqlCommand cmd = new SqlCommand(sql, DAO.con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Xóa thành công!");

            Load_DataGridView();
            ResetValues();

            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql = @"
            SELECT ma_nha_cung_cap,
                   ten_nha_cung_cap,
                   so_dien_thoai,
                   dia_chi
            FROM tblNhaCungCap
            WHERE 1=1 ";

            string tuKhoa = txtTuKhoa.Text.Trim();

            if (tuKhoa == "")
            {
                MessageBox.Show(
                    "Hãy nhập từ khóa tìm kiếm!");

                txtTuKhoa.Focus();
                return;
            }

            if (tuKhoa != "")
            {
                // TẤT CẢ
                if (cboLocTimKiem.Text == "Tất cả")
                {
                    sql += @"
            AND (
                ma_nha_cung_cap
                COLLATE Latin1_General_CI_AI
                LIKE N'%" + tuKhoa + @"%'

                OR ten_nha_cung_cap
                COLLATE Latin1_General_CI_AI
                LIKE N'%" + tuKhoa + @"%'

                OR dia_chi
                COLLATE Latin1_General_CI_AI
                LIKE N'%" + tuKhoa + @"%'

                OR so_dien_thoai
                LIKE N'%" + tuKhoa + @"%'
            )";
                }

                // MÃ NCC
                else if (
                    cboLocTimKiem.Text == "Mã NCC")
                {
                    sql += @"
            AND ma_nha_cung_cap
                COLLATE Latin1_General_CI_AI
                LIKE N'%" + tuKhoa + "%'";
                }

                // TÊN NCC
                else if (
                    cboLocTimKiem.Text == "Tên NCC")
                {
                    sql += @"
            AND ten_nha_cung_cap
                COLLATE Latin1_General_CI_AI
                LIKE N'%" + tuKhoa + "%'";
                }

                // ĐỊA CHỈ
                else if (
                    cboLocTimKiem.Text == "Địa chỉ")
                {
                    sql += @"
            AND dia_chi
                COLLATE Latin1_General_CI_AI
                LIKE N'%" + tuKhoa + "%'";
                }

                // SĐT
                else if (
                    cboLocTimKiem.Text == "SĐT")
                {
                    sql += @"
            AND so_dien_thoai
                LIKE N'%" + tuKhoa + "%'";
                }
            }

            tblNhaCungCap = DAO.LoadDataToTable(sql);

            dgvNCC.DataSource = tblNhaCungCap;

            if (tblNhaCungCap.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu!");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetValues();

            Load_DataGridView();

            cboLocTimKiem.SelectedIndex = 0;

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        // Them ham sinh mã tự động
        private string TaoMaNCC()
        {
            string sql = @"
            SELECT TOP 1 ma_nha_cung_cap
            FROM tblNhaCungCap
            ORDER BY ma_nha_cung_cap DESC";

                    SqlCommand cmd = new SqlCommand(sql, DAO.con);

                    object result = cmd.ExecuteScalar();

                    if (result == null)
                        return "NCC001";

                    string maCu = result.ToString();

                    int so = int.Parse(maCu.Substring(3));

                    return "NCC" + (so + 1).ToString("D3");
        }
    }
}