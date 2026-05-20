namespace QuanLyCuaHangSach
{
    partial class frmNhaCungCap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.grpThongTinNCC = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenSach = new System.Windows.Forms.TextBox();
            this.lblTenNCC = new System.Windows.Forms.Label();
            this.txtMaSach = new System.Windows.Forms.TextBox();
            this.lblMaNCC = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.grpTimKiem = new System.Windows.Forms.GroupBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.dgvNCC = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.grpThongTinNCC.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNCC)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.Controls.Add(this.btnLamMoi);
            this.pnlTop.Controls.Add(this.btnXoa);
            this.pnlTop.Controls.Add(this.btnSua);
            this.pnlTop.Controls.Add(this.btnThem);
            this.pnlTop.Location = new System.Drawing.Point(21, 12);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(850, 63);
            this.pnlTop.TabIndex = 1;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.White;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.Location = new System.Drawing.Point(418, 4);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(106, 56);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "↻ Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.White;
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(289, 4);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(90, 59);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "🗑 Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.White;
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(159, 4);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(90, 59);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "✎ Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.White;
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(29, 3);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(90, 60);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "+ Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            // 
            // grpThongTinNCC
            // 
            this.grpThongTinNCC.BackColor = System.Drawing.Color.White;
            this.grpThongTinNCC.Controls.Add(this.btnLuu);
            this.grpThongTinNCC.Controls.Add(this.panel1);
            this.grpThongTinNCC.Controls.Add(this.txtDiaChi);
            this.grpThongTinNCC.Controls.Add(this.label6);
            this.grpThongTinNCC.Controls.Add(this.txtSDT);
            this.grpThongTinNCC.Controls.Add(this.label3);
            this.grpThongTinNCC.Controls.Add(this.txtTenSach);
            this.grpThongTinNCC.Controls.Add(this.lblTenNCC);
            this.grpThongTinNCC.Controls.Add(this.txtMaSach);
            this.grpThongTinNCC.Controls.Add(this.lblMaNCC);
            this.grpThongTinNCC.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTinNCC.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpThongTinNCC.Location = new System.Drawing.Point(21, 96);
            this.grpThongTinNCC.Name = "grpThongTinNCC";
            this.grpThongTinNCC.Size = new System.Drawing.Size(850, 165);
            this.grpThongTinNCC.TabIndex = 2;
            this.grpThongTinNCC.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(850, 25);
            this.panel1.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(173, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "Thông tin nhà cung cấp";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(419, 104);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(213, 30);
            this.txtDiaChi.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(327, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 30);
            this.label6.TabIndex = 9;
            this.label6.Text = "Địa chỉ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(419, 44);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(213, 30);
            this.txtSDT.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(327, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = "SĐT";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTenSach
            // 
            this.txtTenSach.Location = new System.Drawing.Point(101, 104);
            this.txtTenSach.Name = "txtTenSach";
            this.txtTenSach.Size = new System.Drawing.Size(178, 30);
            this.txtTenSach.TabIndex = 3;
            // 
            // lblTenNCC
            // 
            this.lblTenNCC.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblTenNCC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenNCC.Location = new System.Drawing.Point(9, 104);
            this.lblTenNCC.Name = "lblTenNCC";
            this.lblTenNCC.Size = new System.Drawing.Size(86, 30);
            this.lblTenNCC.TabIndex = 2;
            this.lblTenNCC.Text = "Tên NCC";
            this.lblTenNCC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMaSach
            // 
            this.txtMaSach.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaSach.Location = new System.Drawing.Point(101, 44);
            this.txtMaSach.Name = "txtMaSach";
            this.txtMaSach.Size = new System.Drawing.Size(178, 31);
            this.txtMaSach.TabIndex = 1;
            // 
            // lblMaNCC
            // 
            this.lblMaNCC.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblMaNCC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaNCC.Location = new System.Drawing.Point(11, 46);
            this.lblMaNCC.Name = "lblMaNCC";
            this.lblMaNCC.Size = new System.Drawing.Size(84, 30);
            this.lblMaNCC.TabIndex = 0;
            this.lblMaNCC.Text = "Mã NCC";
            this.lblMaNCC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(695, 122);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(90, 37);
            this.btnLuu.TabIndex = 15;
            this.btnLuu.Text = "💾 Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.label10);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 27);
            this.panel2.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "Tìm kiếm ";
            // 
            // grpTimKiem
            // 
            this.grpTimKiem.BackColor = System.Drawing.Color.White;
            this.grpTimKiem.Controls.Add(this.panel2);
            this.grpTimKiem.Controls.Add(this.btnTimKiem);
            this.grpTimKiem.Controls.Add(this.lblTuKhoa);
            this.grpTimKiem.Controls.Add(this.txtTimKiem);
            this.grpTimKiem.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTimKiem.ForeColor = System.Drawing.Color.MidnightBlue;
            this.grpTimKiem.Location = new System.Drawing.Point(21, 283);
            this.grpTimKiem.Name = "grpTimKiem";
            this.grpTimKiem.Size = new System.Drawing.Size(850, 92);
            this.grpTimKiem.TabIndex = 3;
            this.grpTimKiem.TabStop = false;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.White;
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.Black;
            this.btnTimKiem.Location = new System.Drawing.Point(695, 51);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(90, 35);
            this.btnTimKiem.TabIndex = 10;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // lblTuKhoa
            // 
            this.lblTuKhoa.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblTuKhoa.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuKhoa.Location = new System.Drawing.Point(98, 51);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(89, 30);
            this.lblTuKhoa.TabIndex = 1;
            this.lblTuKhoa.Text = "Từ khóa";
            this.lblTuKhoa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(220, 51);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(245, 30);
            this.txtTimKiem.TabIndex = 0;
            // 
            // dgvNCC
            // 
            this.dgvNCC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNCC.BackgroundColor = System.Drawing.Color.White;
            this.dgvNCC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNCC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNCC.Location = new System.Drawing.Point(20, 392);
            this.dgvNCC.Name = "dgvNCC";
            this.dgvNCC.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvNCC.RowHeadersWidth = 51;
            this.dgvNCC.RowTemplate.Height = 24;
            this.dgvNCC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNCC.Size = new System.Drawing.Size(850, 159);
            this.dgvNCC.TabIndex = 4;
            // 
            // frmNhaCungCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.dgvNCC);
            this.Controls.Add(this.grpTimKiem);
            this.Controls.Add(this.grpThongTinNCC);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmNhaCungCap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNhaCungCap";
            this.pnlTop.ResumeLayout(false);
            this.grpThongTinNCC.ResumeLayout(false);
            this.grpThongTinNCC.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpTimKiem.ResumeLayout(false);
            this.grpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNCC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.GroupBox grpThongTinNCC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenSach;
        private System.Windows.Forms.Label lblTenNCC;
        private System.Windows.Forms.TextBox txtMaSach;
        private System.Windows.Forms.Label lblMaNCC;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox grpTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Label lblTuKhoa;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvNCC;
    }
}