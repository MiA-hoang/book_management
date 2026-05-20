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
    public partial class nvKho : Form
    {
        public nvKho()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            fChangePassWord change = new fChangePassWord();
            this.Hide();
            change.ShowDialog();
            this.Show();
        }

        private void LoadFormToPanel(Form childForm)
        {
            pContent.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pContent.Controls.Add(childForm);
            childForm.Show();
        }
    }
}
