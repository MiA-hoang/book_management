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
            label4.Text = _hoTen.Substring(0, 1).ToUpper();
        }
    }
}
