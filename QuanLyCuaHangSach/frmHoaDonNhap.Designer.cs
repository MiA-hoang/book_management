namespace QuanLyCuaHangSach
{
    partial class frmHoaDonNhap
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();

            this.grbHoaDon = new Guna.UI2.WinForms.Guna2GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtNgayNhap = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cboNCC = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMaHDN = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNhanVien = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDiaChiNCC = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSDTNCC = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaNCC_HienThi = new Guna.UI2.WinForms.Guna2TextBox();
            this.grbChiTiet = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.txtDonGiaNhap = new Guna.UI2.WinForms.Guna2TextBox();
            this.nudSoLuong = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.cboSach = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTimSach = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTonKho = new System.Windows.Forms.Label();
            this.dgvChiTiet = new Guna.UI2.WinForms.Guna2DataGridView();
            this.colMaSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTongTien = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTongDauSach = new System.Windows.Forms.Label();
            this.lblTongSoLuong = new System.Windows.Forms.Label();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnIn = new Guna.UI2.WinForms.Guna2Button();
            this.btnTaoMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnThoat = new Guna.UI2.WinForms.Guna2Button();
            this.grbHoaDon.SuspendLayout();
            this.grbChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // grbHoaDon
            // 
            this.grbHoaDon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.grbHoaDon.Controls.Add(this.label2);
            this.grbHoaDon.Controls.Add(this.dtNgayNhap);
            this.grbHoaDon.Controls.Add(this.cboNCC);
            this.grbHoaDon.Controls.Add(this.label4);
            this.grbHoaDon.Controls.Add(this.label3);
            this.grbHoaDon.Controls.Add(this.label5);
            this.grbHoaDon.Controls.Add(this.txtMaHDN);
            this.grbHoaDon.Controls.Add(this.txtNhanVien);
            this.grbHoaDon.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.grbHoaDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grbHoaDon.ForeColor = System.Drawing.Color.Black;
            this.grbHoaDon.Location = new System.Drawing.Point(12, 5);
            this.grbHoaDon.Name = "grbHoaDon";
            this.grbHoaDon.Size = new System.Drawing.Size(1403, 110);

            this.grbHoaDon.TabIndex = 0;
            this.grbHoaDon.Text = "THÔNG TIN HÓA ĐƠN NHẬP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1039, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nhân viên:";
            // 
            // dtNgayNhap
            // 
            this.dtNgayNhap.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dtNgayNhap.BorderRadius = 1;
            this.dtNgayNhap.Checked = true;
            this.dtNgayNhap.FillColor = System.Drawing.Color.White;
            this.dtNgayNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtNgayNhap.ForeColor = System.Drawing.Color.Black;
            this.dtNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtNgayNhap.Location = new System.Drawing.Point(460, 55);

            this.dtNgayNhap.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtNgayNhap.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtNgayNhap.Name = "dtNgayNhap";
            this.dtNgayNhap.Size = new System.Drawing.Size(150, 36);
            this.dtNgayNhap.TabIndex = 1;
            this.dtNgayNhap.Value = new System.DateTime(2026, 5, 22, 18, 6, 52, 486);
            // 
            // cboNCC
            // 
            this.cboNCC.BackColor = System.Drawing.Color.Transparent;
            this.cboNCC.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNCC.FocusedColor = System.Drawing.Color.Empty;
            this.cboNCC.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNCC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboNCC.ItemHeight = 30;
            this.cboNCC.Location = new System.Drawing.Point(713, 55);
            this.cboNCC.Name = "cboNCC";
            this.cboNCC.Size = new System.Drawing.Size(304, 36);

            this.cboNCC.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(634, 60);

            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nhà CC:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(357, 63);

            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ngày lập:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Mã HĐ:";
            // 
            // txtMaHDN
            // 
            this.txtMaHDN.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaHDN.DefaultText = "";
            this.txtMaHDN.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaHDN.Location = new System.Drawing.Point(87, 55);
            this.txtMaHDN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaHDN.Name = "txtMaHDN";
            this.txtMaHDN.PlaceholderText = "";
            this.txtMaHDN.SelectedText = "";
            this.txtMaHDN.Size = new System.Drawing.Size(248, 36);

            this.txtMaHDN.TabIndex = 0;
            // 
            // txtNhanVien
            // 
            this.txtNhanVien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNhanVien.DefaultText = "";
            this.txtNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNhanVien.Location = new System.Drawing.Point(1143, 56);
            this.txtNhanVien.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNhanVien.Name = "txtNhanVien";
            this.txtNhanVien.PlaceholderText = "";
            this.txtNhanVien.SelectedText = "";
            this.txtNhanVien.Size = new System.Drawing.Size(229, 35);
            this.txtNhanVien.TabIndex = 3;
            // 
            // txtDiaChiNCC
            // 
            this.txtDiaChiNCC.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiaChiNCC.DefaultText = "";
            this.txtDiaChiNCC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiaChiNCC.Location = new System.Drawing.Point(0, 0);
            this.txtDiaChiNCC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDiaChiNCC.Name = "txtDiaChiNCC";
            this.txtDiaChiNCC.PlaceholderText = "";
            this.txtDiaChiNCC.SelectedText = "";
            this.txtDiaChiNCC.Size = new System.Drawing.Size(229, 48);
            this.txtDiaChiNCC.TabIndex = 0;
            // 
            // txtSDTNCC
            // 
            this.txtSDTNCC.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSDTNCC.DefaultText = "";
            this.txtSDTNCC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSDTNCC.Location = new System.Drawing.Point(0, 0);
            this.txtSDTNCC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSDTNCC.Name = "txtSDTNCC";
            this.txtSDTNCC.PlaceholderText = "";
            this.txtSDTNCC.SelectedText = "";
            this.txtSDTNCC.Size = new System.Drawing.Size(229, 48);
            this.txtSDTNCC.TabIndex = 0;
            // 
            // txtMaNCC_HienThi
            // 
            this.txtMaNCC_HienThi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaNCC_HienThi.DefaultText = "";
            this.txtMaNCC_HienThi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaNCC_HienThi.Location = new System.Drawing.Point(0, 0);
            this.txtMaNCC_HienThi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaNCC_HienThi.Name = "txtMaNCC_HienThi";
            this.txtMaNCC_HienThi.PlaceholderText = "";
            this.txtMaNCC_HienThi.SelectedText = "";
            this.txtMaNCC_HienThi.Size = new System.Drawing.Size(229, 48);
            this.txtMaNCC_HienThi.TabIndex = 0;
            // 
            // grbChiTiet
            // 
            this.grbChiTiet.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(165)))), ((int)(((byte)(250)))));
            this.grbChiTiet.Controls.Add(this.btnThem);
            this.grbChiTiet.Controls.Add(this.txtDonGiaNhap);
            this.grbChiTiet.Controls.Add(this.nudSoLuong);
            this.grbChiTiet.Controls.Add(this.cboSach);
            this.grbChiTiet.Controls.Add(this.label8);
            this.grbChiTiet.Controls.Add(this.label7);
            this.grbChiTiet.Controls.Add(this.label6);
            this.grbChiTiet.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(165)))), ((int)(((byte)(250)))));
            this.grbChiTiet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grbChiTiet.ForeColor = System.Drawing.Color.Black;
            this.grbChiTiet.Location = new System.Drawing.Point(12, 120);
            this.grbChiTiet.Name = "grbChiTiet";
            this.grbChiTiet.Size = new System.Drawing.Size(1403, 125);

            this.grbChiTiet.TabIndex = 1;
            this.grbChiTiet.Text = "THÊM SÁCH VÀO HÓA ĐƠN";
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(1241, 58);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(136, 40);

            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtDonGiaNhap
            // 
            this.txtDonGiaNhap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDonGiaNhap.DefaultText = "";
            this.txtDonGiaNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDonGiaNhap.Location = new System.Drawing.Point(951, 62);

            this.txtDonGiaNhap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDonGiaNhap.Name = "txtDonGiaNhap";
            this.txtDonGiaNhap.PlaceholderText = "";
            this.txtDonGiaNhap.SelectedText = "";
            this.txtDonGiaNhap.Size = new System.Drawing.Size(182, 36);

            this.txtDonGiaNhap.TabIndex = 2;
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.BackColor = System.Drawing.Color.Transparent;
            this.nudSoLuong.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.nudSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudSoLuong.Location = new System.Drawing.Point(569, 62);
            this.nudSoLuong.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(182, 36);
            this.nudSoLuong.TabIndex = 1;

            // 
            // cboSach
            // 
            this.cboSach.BackColor = System.Drawing.Color.Transparent;
            this.cboSach.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSach.FocusedColor = System.Drawing.Color.Empty;
            this.cboSach.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboSach.ItemHeight = 30;
            this.cboSach.Location = new System.Drawing.Point(69, 62);

            this.cboSach.Name = "cboSach";
            this.cboSach.Size = new System.Drawing.Size(250, 36);
            this.cboSach.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(859, 69);

            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 23);
            this.label8.TabIndex = 0;
            this.label8.Text = "Giá nhập:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(460, 68);

            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "Số lượng:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 69);

            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "Sách:";
            // 
            // txtTimSach
            // 
            this.txtTimSach.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimSach.DefaultText = "";
            this.txtTimSach.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimSach.Location = new System.Drawing.Point(0, 0);
            this.txtTimSach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTimSach.Name = "txtTimSach";
            this.txtTimSach.PlaceholderText = "";
            this.txtTimSach.SelectedText = "";
            this.txtTimSach.Size = new System.Drawing.Size(229, 48);
            this.txtTimSach.TabIndex = 0;
            // 
            // lblTonKho
            // 
            this.lblTonKho.Location = new System.Drawing.Point(0, 0);
            this.lblTonKho.Name = "lblTonKho";
            this.lblTonKho.Size = new System.Drawing.Size(100, 23);
            this.lblTonKho.TabIndex = 0;
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
            this.dgvChiTiet.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvChiTiet.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvChiTiet.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;

            this.dgvChiTiet.ColumnHeadersHeight = 35;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaSach,
            this.colTenSach,
            this.colSoLuong,
            this.colDonGia,
            this.colThanhTien});
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTiet.DefaultCellStyle = dataGridViewCellStyle21;
            this.dgvChiTiet.GridColor = System.Drawing.Color.White;
            this.dgvChiTiet.Location = new System.Drawing.Point(12, 293);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.Size = new System.Drawing.Size(1403, 438);

            this.dgvChiTiet.TabIndex = 8;
            this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvChiTiet.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvChiTiet.ThemeStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dgvChiTiet.ThemeStyle.GridColor = System.Drawing.Color.White;
            this.dgvChiTiet.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.LightBlue;
            this.dgvChiTiet.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvChiTiet.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvChiTiet.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvChiTiet.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvChiTiet.ThemeStyle.HeaderStyle.Height = 35;
            this.dgvChiTiet.ThemeStyle.ReadOnly = false;
            this.dgvChiTiet.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvChiTiet.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvChiTiet.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvChiTiet.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvChiTiet.ThemeStyle.RowsStyle.Height = 22;
            this.dgvChiTiet.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.AliceBlue;
            this.dgvChiTiet.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // colMaSach
            // 
            this.colMaSach.HeaderText = "Mã Sách";
            this.colMaSach.MinimumWidth = 6;
            this.colMaSach.Name = "colMaSach";
            // 
            // colTenSach
            // 
            this.colTenSach.HeaderText = "Tên Sách";
            this.colTenSach.MinimumWidth = 6;
            this.colTenSach.Name = "colTenSach";
            // 
            // colSoLuong
            // 
            this.colSoLuong.HeaderText = "Số Lượng";
            this.colSoLuong.MinimumWidth = 6;
            this.colSoLuong.Name = "colSoLuong";
            // 
            // colDonGia
            // 
            this.colDonGia.HeaderText = "Đơn Giá";
            this.colDonGia.MinimumWidth = 6;
            this.colDonGia.Name = "colDonGia";
            // 
            // colThanhTien
            // 
            this.colThanhTien.HeaderText = "Thành Tiền";
            this.colThanhTien.MinimumWidth = 6;
            this.colThanhTien.Name = "colThanhTien";
            // 
            // colSTT
            // 
            this.colSTT.MinimumWidth = 6;
            this.colSTT.Name = "colSTT";
            this.colSTT.Width = 125;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(941, 763);

            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tổng tiền:";
            // 
            // txtTongTien
            // 
            this.txtTongTien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTongTien.DefaultText = "";
            this.txtTongTien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTongTien.Location = new System.Drawing.Point(1071, 753);

            this.txtTongTien.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.PlaceholderText = "";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.SelectedText = "";
            this.txtTongTien.Size = new System.Drawing.Size(227, 36);

            this.txtTongTien.TabIndex = 4;
            // 
            // lblTongDauSach
            // 
            this.lblTongDauSach.Location = new System.Drawing.Point(0, 0);
            this.lblTongDauSach.Name = "lblTongDauSach";
            this.lblTongDauSach.Size = new System.Drawing.Size(100, 23);
            this.lblTongDauSach.TabIndex = 0;
            // 
            // lblTongSoLuong
            // 
            this.lblTongSoLuong.Location = new System.Drawing.Point(0, 0);
            this.lblTongSoLuong.Name = "lblTongSoLuong";
            this.lblTongSoLuong.Size = new System.Drawing.Size(100, 23);
            this.lblTongSoLuong.TabIndex = 0;
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(11, 753);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(136, 40);

            this.btnXoa.TabIndex = 3;
            this.btnXoa.Text = "Xóa dòng";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(225, 753);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(136, 40);
            this.btnLuu.TabIndex = 5;
            this.btnLuu.Text = "Lưu HĐ";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnIn
            // 
            this.btnIn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnIn.ForeColor = System.Drawing.Color.White;
            this.btnIn.Location = new System.Drawing.Point(436, 753);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(136, 40);
            this.btnIn.TabIndex = 6;
            this.btnIn.Text = "In HĐ";
            // 
            // btnTaoMoi
            // 
            this.btnTaoMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTaoMoi.ForeColor = System.Drawing.Color.White;
            this.btnTaoMoi.Location = new System.Drawing.Point(12, 251);
            this.btnTaoMoi.Name = "btnTaoMoi";
            this.btnTaoMoi.Size = new System.Drawing.Size(120, 35);
            this.btnTaoMoi.TabIndex = 7;
            this.btnTaoMoi.Text = "Tạo mới HĐ";
            this.btnTaoMoi.Click += new System.EventHandler(this.btnTaoMoi_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(0, 0);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(180, 45);
            this.btnThoat.TabIndex = 0;
            // 
            // frmHoaDonNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1417, 803);

            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.btnTaoMoi);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.grbChiTiet);
            this.Controls.Add(this.grbHoaDon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTongTien);
            this.Name = "frmHoaDonNhap";
            this.Text = "frmHoaDonNhap";
            this.Load += new System.EventHandler(this.frmHoaDonNhap_Load);
            this.grbHoaDon.ResumeLayout(false);
            this.grbHoaDon.PerformLayout();
            this.grbChiTiet.ResumeLayout(false);
            this.grbChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GroupBox grbHoaDon;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtNgayNhap;
        private Guna.UI2.WinForms.Guna2ComboBox cboNCC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2TextBox txtMaHDN;
        private Guna.UI2.WinForms.Guna2GroupBox grbChiTiet;
        private Guna.UI2.WinForms.Guna2ComboBox cboSach;
        private Guna.UI2.WinForms.Guna2TextBox txtSoLuong;
        private Guna.UI2.WinForms.Guna2TextBox txtDonGiaNhap;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2DataGridView dgvChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txtTongTien;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnIn;
        private Guna.UI2.WinForms.Guna2Button btnTaoMoi;
        private Guna.UI2.WinForms.Guna2TextBox txtNhanVien;
        private Guna.UI2.WinForms.Guna2TextBox txtDiaChiNCC;
        private Guna.UI2.WinForms.Guna2TextBox txtSDTNCC;
        private Guna.UI2.WinForms.Guna2TextBox txtMaNCC_HienThi;
        private Guna.UI2.WinForms.Guna2TextBox txtTimSach;
        private System.Windows.Forms.Label lblTonKho;
        private Guna.UI2.WinForms.Guna2NumericUpDown nudSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.Label lblTongDauSach;
        private System.Windows.Forms.Label lblTongSoLuong;
        private Guna.UI2.WinForms.Guna2Button btnThoat;
        private System.Windows.Forms.Label label2;
    }
}