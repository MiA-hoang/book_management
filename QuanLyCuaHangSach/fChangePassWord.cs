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
    public partial class fChangePassWord : Form
    {
        private string _maNhanVien;
        private string _hoTen;
        private string _vaiTro;
        public fChangePassWord()
        {
            InitializeComponent();
        }
        public fChangePassWord(string maNhanVien, string hoTen, string vaiTro)
        {
            InitializeComponent();
            _maNhanVien = maNhanVien;
            _hoTen = hoTen;
            _vaiTro = vaiTro;
        }


        private void check_CheckedChanged(object sender, EventArgs e)
        {
            if (check.Checked == true)
            {
                txtCurrent.UseSystemPasswordChar = false;
                txtNew.UseSystemPasswordChar = false;
            }
            else
            {
                txtCurrent.UseSystemPasswordChar = true;
                txtNew.UseSystemPasswordChar = true;
            }
        }

        private void fChangePassWord_Load(object sender, EventArgs e)
        {
            label3.Text = _hoTen.ToUpper();
            label6.Text = _vaiTro;

            string[] ten = _hoTen.Trim().Split(' ');
            label4.Text = ten[ten.Length - 1][0].ToString().ToUpper();

            DAO.Connect();

            string sql = @"SELECT ten_dang_nhap
                   FROM tblNhanVien
                   WHERE ma_nhan_vien = '" + _maNhanVien + "'";

            txbUser.Text = DAO.getFieldValue(sql);

            DAO.Close();

            txbUser.ReadOnly = true;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if(txtCurrent.Text.Trim() == ""){
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại!");
                txtCurrent.Focus();
                return;
            }
            if (txtNew.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!");
                txtNew.Focus();
                return;
            }

            DAO.Connect();

            string sqlCheck = @"
                SELECT COUNT(*)
                FROM tblNhanVien
                WHERE ma_nhan_vien = @manv
                AND mat_khau = @mk";

            SqlCommand cmdCheck =
                new SqlCommand(sqlCheck, DAO.con);

            cmdCheck.Parameters.AddWithValue(
                "@manv", _maNhanVien);

            cmdCheck.Parameters.AddWithValue(
                "@mk", txtCurrent.Text);

            int count = (int)cmdCheck.ExecuteScalar();

            if (count == 0)
            {
                MessageBox.Show(
                    "Mật khẩu hiện tại không đúng!");

                DAO.Close();
                txtCurrent.Focus();
                return;
            }
            if (txtCurrent.Text == txtNew.Text)
            {
                MessageBox.Show(
                    "Mật khẩu mới không được trùng với mật khẩu hiện tại!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNew.Focus();
                txtNew.SelectAll();

                DAO.Close();
                return;
            }

            // Cập nhật mật khẩu mới
            string sqlUpdate = @"
                UPDATE tblNhanVien
                SET mat_khau = @newpass
                WHERE ma_nhan_vien = @manv";

            SqlCommand cmdUpdate =
                new SqlCommand(sqlUpdate, DAO.con);

            cmdUpdate.Parameters.AddWithValue( "@newpass", txtNew.Text);
            cmdUpdate.Parameters.AddWithValue( "@manv", _maNhanVien);
            cmdUpdate.ExecuteNonQuery();
            DAO.Close();
            MessageBox.Show("Đổi mật khẩu thành công!");
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
