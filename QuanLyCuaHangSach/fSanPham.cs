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
    public partial class fSanPham : Form
    {
        DataTable tblSach;
        public fSanPham()
        {
            InitializeComponent();
        }

        private void fSanPham_Load(object sender, EventArgs e)
        {
            DAO.Connect();


            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            Load_DataGridView();

            DAO.FillDataToCombo(
                "SELECT ma_danh_muc, ten_danh_muc FROM tblDanhMuc",
                cboDanhMuc,
                "ma_danh_muc",
                "ten_danh_muc"
            );

            DAO.FillDataToCombo(
                "SELECT ma_tac_gia, ten_tac_gia FROM tblTacGia",
                cboTacGia,
                "ma_tac_gia",
                "ten_tac_gia"
            );
            DAO.FillDataToCombo(
                "SELECT ma_danh_muc, ten_danh_muc FROM tblDanhMuc",
                cboLocDanhMuc,
                "ma_danh_muc",
                "ten_danh_muc"
            );

            cboLocDanhMuc.SelectedIndex = -1;
            cboDanhMuc.SelectedIndex = -1;
            cboTacGia.SelectedIndex = -1;

            ResetValues();
        }
        private void ResetValues()
        {
            txtMaSach.Text = "";
            txtTenSach.Text = "";

            txtGiaBan.Text = "";
            txtSoLuong.Text = "";

            cboDanhMuc.SelectedIndex = -1;
            cboTacGia.SelectedIndex = -1;
            cboLocDanhMuc.SelectedIndex = -1;
            cboTonKho.SelectedIndex = -1;

            txtAnh.Text = "";

            picSach.Image = null;

            txtTuKhoa.Text = "";
            txtGiaMin.Text = "";
            txtGiaMax.Text = "";
            txtMaSach.Enabled = false;
        }
        private void Load_DataGridView()
        {
            string sql = @"
            SELECT s.ma_sach,
             s.ten_sach,
             s.gia_ban,
             s.so_luong,

             s.ma_danh_muc,
             dm.ten_danh_muc,

             s.ma_tac_gia,
             tg.ten_tac_gia,

             s.hinh_anh

         FROM tblSach s
         JOIN tblDanhMuc dm
             ON s.ma_danh_muc = dm.ma_danh_muc
         JOIN tblTacGia tg
             ON s.ma_tac_gia = tg.ma_tac_gia";

            tblSach = DAO.LoadDataToTable(sql);
            dgvSach.DataSource = tblSach;

            dgvSach.Columns[0].HeaderText = "Mã sách";
            dgvSach.Columns[1].HeaderText = "Tên sách";
            dgvSach.Columns[2].HeaderText = "Giá bán";
            dgvSach.Columns[3].HeaderText = "Số lượng";

            dgvSach.Columns[5].HeaderText = "Danh mục";
            dgvSach.Columns[7].HeaderText = "Tác giả";

            // ẩn cột mã + ảnh
            dgvSach.Columns[4].Visible = false;
            dgvSach.Columns[6].Visible = false;
            dgvSach.Columns[8].Visible = false;

            dgvSach.AllowUserToAddRows = false;

            dgvSach.EditMode =
                DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tblSach.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }

            txtMaSach.Text =
                dgvSach.CurrentRow.Cells[0].Value.ToString();

            txtTenSach.Text =
                dgvSach.CurrentRow.Cells[1].Value.ToString();

            txtGiaBan.Text =
                dgvSach.CurrentRow.Cells[2].Value.ToString();

            txtSoLuong.Text =
                dgvSach.CurrentRow.Cells[3].Value.ToString();

            // lấy mã ẩn để đổ vào combobox
            cboDanhMuc.SelectedValue =
                dgvSach.CurrentRow.Cells[4].Value;

            cboTacGia.SelectedValue =
                dgvSach.CurrentRow.Cells[6].Value;

            txtAnh.Text =
                     dgvSach.CurrentRow.Cells[8].Value == DBNull.Value
                     ? ""
                     : dgvSach.CurrentRow.Cells[8].Value.ToString();

            if (txtAnh.Text.Trim() != ""
                && File.Exists(txtAnh.Text))
            {
                using (FileStream fs =
                    new FileStream(txtAnh.Text,
                    FileMode.Open,
                    FileAccess.Read))
                {
                    picSach.Image =
                        Image.FromStream(fs);
                }

                picSach.SizeMode =
                    PictureBoxSizeMode.StretchImage;
            }
            else
            {
                picSach.Image = null;
            }
            txtMaSach.Enabled = false;

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

            picSach.Image = null;

            txtMaSach.Text = TaoMaSP();
            txtMaSach.Enabled = false;
            txtTenSach.Focus();
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            dlg.Title = "Chọn ảnh sách";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtAnh.Text = dlg.FileName;

                using (FileStream fs =
                             new FileStream(dlg.FileName,
                             FileMode.Open,
                             FileAccess.Read))
                {
                    picSach.Image =
                        Image.FromStream(fs);
                }

                picSach.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // kiểm tra dữ liệu
            if (txtMaSach.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sách!");
                txtMaSach.Focus();
                return;
            }

            string sqlCheck = @"
            SELECT ma_sach
            FROM tblSach
            WHERE ma_sach = N'"
            + txtMaSach.Text.Trim()
            + "'";

            //if (DAO.CheckKey(sqlCheck))
            //{
            //    MessageBox.Show("Mã sách đã tồn tại!");

            //    txtMaSach.Focus();
            //    txtMaSach.Text = "";

            //    return;
            //}

            if (txtTenSach.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sách!");
                txtTenSach.Focus();
                return;
            }

            decimal giaBan;
            int soLuong;

            if (!decimal.TryParse(txtGiaBan.Text, out giaBan))
            {
                MessageBox.Show("Giá bán phải là số!");
                txtGiaBan.Focus();
                return;
            }

            if (!int.TryParse(txtSoLuong.Text, out soLuong))
            {
                MessageBox.Show("Số lượng phải là số nguyên!");
                txtSoLuong.Focus();
                return;
            }

            if (giaBan <= 0)
            {
                MessageBox.Show("Giá bán phải lớn hơn 0!");
                txtGiaBan.Focus();
                return;
            }

            if (soLuong < 0)
            {
                MessageBox.Show("Số lượng không được âm!");
                txtSoLuong.Focus();
                return;
            }

            if (cboDanhMuc.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn danh mục!");
                cboDanhMuc.Focus();
                return;
            }

            if (cboTacGia.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn tác giả!");
                cboTacGia.Focus();
                return;
            }

            try
            {
                string sql = @"INSERT INTO tblSach
                 (ma_sach,
                 ten_sach,
                 gia_ban,
                 so_luong,
                 ma_danh_muc,
                 ma_tac_gia,
                 hinh_anh)

                 VALUES
                 (N'" + txtMaSach.Text.Trim() + @"',
                  N'" + txtTenSach.Text.Trim() + @"',
                  " + txtGiaBan.Text.Trim() + @",
                  " + txtSoLuong.Text.Trim() + @",
                  N'" + cboDanhMuc.SelectedValue.ToString() + @"',
                  N'" + cboTacGia.SelectedValue.ToString() + @"',
                  N'" + txtAnh.Text.Trim() + "')";

                SqlCommand cmd = new SqlCommand(sql, DAO.con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Thêm thành công!");

                Load_DataGridView();
                ResetValues();

                btnThem.Enabled = true;
                btnLuu.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;

            if (tblSach.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }

            if (txtMaSach.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn sách!");
                return;
            }

            // kiểm tra dữ liệu
            if (txtTenSach.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sách!");
                txtTenSach.Focus();
                return;
            }

            decimal giaBan;
            int soLuong;

            if (!decimal.TryParse(txtGiaBan.Text, out giaBan))
            {
                MessageBox.Show("Giá bán phải là số!");
                txtGiaBan.Focus();
                return;
            }

            if (!int.TryParse(txtSoLuong.Text, out soLuong))
            {
                MessageBox.Show("Số lượng phải là số nguyên!");
                txtSoLuong.Focus();
                return;
            }

            if (giaBan <= 0)
            {
                MessageBox.Show("Giá bán phải lớn hơn 0!");
                txtGiaBan.Focus();
                return;
            }

            if (soLuong < 0)
            {
                MessageBox.Show("Số lượng không được âm!");
                txtSoLuong.Focus();
                return;
            }

            if (cboDanhMuc.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn danh mục!");
                return;
            }

            if (cboTacGia.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn tác giả!");
                return;
            }

            try
            {
                sql = @"UPDATE tblSach SET
                ten_sach = N'" + txtTenSach.Text.Trim() + @"',
                gia_ban = " + giaBan + @",
                so_luong = " + soLuong + @",
                ma_danh_muc = N'" + cboDanhMuc.SelectedValue.ToString() + @"',
                ma_tac_gia = N'" + cboTacGia.SelectedValue.ToString() + @"',
                hinh_anh = N'" + txtAnh.Text.Trim() + @"'
                WHERE ma_sach = N'" + txtMaSach.Text + "'";


                SqlCommand cmd = new SqlCommand(sql, DAO.con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Sửa thành công!");

                Load_DataGridView();
                ResetValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;

            if (tblSach.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }

            if (txtMaSach.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn sách cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa sách này không?",
                                "Xác nhận",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string sqlCheck = @"
                    SELECT ma_sach
                    FROM tblChiTietNhap
                    WHERE ma_sach = N'"
                + txtMaSach.Text.Trim()
                + "'";

            if (DAO.CheckKey(sqlCheck))
            {
                MessageBox.Show(
                    "Sách đã phát sinh phiếu nhập, không thể xóa!");
                return;
            }

            sql = @"DELETE FROM tblSach
            WHERE ma_sach = N'"
                    + txtMaSach.Text.Trim()
                    + "'";

            SqlCommand cmd = new SqlCommand(sql, DAO.con);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Xóa thành công!");

            Load_DataGridView();
            ResetValues();

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql = @"
         SELECT s.ma_sach,
           s.ten_sach,
           s.gia_ban,
           s.so_luong,
           s.ma_danh_muc,
           dm.ten_danh_muc,
           s.ma_tac_gia,
           tg.ten_tac_gia,
           s.hinh_anh
         FROM tblSach s
         JOIN tblDanhMuc dm
             ON s.ma_danh_muc = dm.ma_danh_muc
         JOIN tblTacGia tg
             ON s.ma_tac_gia = tg.ma_tac_gia
         WHERE 1=1 ";

            // TỪ KHÓA - TÌM GẦN ĐÚNG
            if (txtTuKhoa.Text.Trim() != "")
            {
                sql += @"
                AND s.ten_sach COLLATE SQL_Latin1_General_CP1_CI_AI
                LIKE N'%"
                + txtTuKhoa.Text.Trim()
                + "%'";
            }

            // DANH MỤC
            if (cboLocDanhMuc.SelectedIndex != -1)
            {
                sql += @" AND s.ma_danh_muc = N'"
                    + cboLocDanhMuc.SelectedValue.ToString()
                    + "'";
            }

            // GIÁ NHỎ NHẤT
            decimal giaMin;
            if (txtGiaMin.Text.Trim() != "")
            {
                if (!decimal.TryParse(
                    txtGiaMin.Text,
                    out giaMin))
                {
                    MessageBox.Show("Giá nhỏ nhất không hợp lệ!");
                    txtGiaMin.Focus();
                    return;
                }

                sql += " AND s.gia_ban >= "
                    + giaMin;
            }

            // GIÁ LỚN NHẤT
            decimal giaMax;
            if (txtGiaMax.Text.Trim() != "")
            {
                if (!decimal.TryParse(
                    txtGiaMax.Text,
                    out giaMax))
                {
                    MessageBox.Show("Giá lớn nhất không hợp lệ!");
                    txtGiaMax.Focus();
                    return;
                }

                sql += " AND s.gia_ban <= "
                    + giaMax;
            }

            // TỒN KHO
            if (cboTonKho.SelectedIndex != -1)
            {
                if (cboTonKho.Text == "Còn hàng")
                {
                    sql += " AND s.so_luong > 0";
                }
                else if (cboTonKho.Text == "Hết hàng")
                {
                    sql += " AND s.so_luong = 0";
                }
            }

            tblSach = DAO.LoadDataToTable(sql);

            dgvSach.DataSource = tblSach;

            if (tblSach.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu!");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetValues();

            Load_DataGridView();

            cboTonKho.SelectedIndex = -1;

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        // Them ham sinh mã tự động
        private string TaoMaSP()
        {
            string sql = @"
                SELECT ISNULL(
                    MAX(CAST(SUBSTRING(ma_sach, 5, LEN(ma_sach)) AS INT)),
                    0)
                FROM tblSach";

            SqlCommand cmd = new SqlCommand(sql, DAO.con);

            int so = Convert.ToInt32(cmd.ExecuteScalar());

            return "BOOK" + (so + 1).ToString("D3");
        }
    }
}
