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
        public fChangePassWord()
        {
            InitializeComponent();
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
                txtCurrent.UseSystemPasswordChar = true;
            }
        }
    }
}
