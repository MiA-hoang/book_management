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
    public partial class fLogin : Form
    {
        private int _soLanSai = 0;
        public fLogin()
        {
            InitializeComponent();
        }


        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txbUser.Text.Trim();
            string password = txbPassWord.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DAO.Connect();

                string sql = $"SELECT ma_nhan_vien, ho_ten, vai_tro, trang_thai FROM tblNhanVien " +
              $"WHERE ten_dang_nhap = '{username}' AND mat_khau = '{password}'";

                DataTable dt = DAO.LoadDataToTable(sql);
                if (dt.Rows.Count == 0)
                {
                    _soLanSai++;
                    int conLai = 3 - _soLanSai;

                    if (_soLanSai >= 3)
                    {
                        MessageBox.Show("Bạn đã nhập sai quá 3 lần. Chương trình sẽ đóng!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txbUser.Enabled = false;
                        txbPassWord.Enabled = false;
                        btnLogin.Enabled = false;
                        return;
                    }

                    MessageBox.Show($"Tên đăng nhập hoặc mật khẩu không đúng! Còn {conLai} lần thử.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string vaiTro = dt.Rows[0]["vai_tro"].ToString();
                string trangThai = dt.Rows[0]["trang_thai"].ToString();

                if (trangThai == "Nghỉ việc")
                {
                    MessageBox.Show("Tài khoản đã bị vô hiệu hóa. Vui lòng liên hệ quản lý!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string hoTen = dt.Rows[0]["ho_ten"].ToString();
                string maNhanVien = dt.Rows[0]["ma_nhan_vien"].ToString();
                this.Hide();

                Form mainForm = null;
                switch (vaiTro)
                {
                    case "Nhân viên quản lí":
                        mainForm = new Admin(maNhanVien, hoTen, vaiTro);
                        break;
                    case "Nhân viên bán sách":
                        mainForm = new NVBanSach(maNhanVien, hoTen, vaiTro);
                        break;
                    case "Nhân viên kho":
                        mainForm = new nvKho(maNhanVien, hoTen, vaiTro);
                        break;
                    default:
                        MessageBox.Show("Vai trò không hợp lệ!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Show();
                        return;
                }

                mainForm.ShowDialog();

                if (!this.IsDisposed)
                    this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DAO.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked== true)
            {
                txbPassWord.UseSystemPasswordChar = false;
            }
            else
            {
                txbPassWord.UseSystemPasswordChar = true;
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}
