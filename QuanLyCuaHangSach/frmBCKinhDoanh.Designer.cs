namespace QuanLyCuaHangSach
{
    partial class frmBCKinhDoanh
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpTuNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.lblTongBan1 = new System.Windows.Forms.Label();
            this.lblTongBan = new System.Windows.Forms.Label();
            this.panel = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.lblTongNhap = new System.Windows.Forms.Label();
            this.guna2CustomGradientPanel4 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.lblLoiNhuan1 = new System.Windows.Forms.Label();
            this.lblLoiNhuan = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTiLe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dgvTopSach = new Guna.UI2.WinForms.Guna2DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.panel.SuspendLayout();
            this.guna2CustomGradientPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTiLe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSach)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpDenNgay);
            this.panel1.Controls.Add(this.dtpTuNgay);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(5, 2);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panel1.Size = new System.Drawing.Size(1070, 57);
            this.panel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(645, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ngày kết thúc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ngày bắt đầu";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Checked = true;
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDenNgay.Location = new System.Drawing.Point(817, 12);
            this.dtpDenNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDenNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 36);
            this.dtpDenNgay.TabIndex = 1;
            this.dtpDenNgay.Value = new System.DateTime(2026, 5, 20, 21, 20, 47, 521);
            this.dtpDenNgay.ValueChanged += new System.EventHandler(this.dtpDenNgay_ValueChanged);
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Checked = true;
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTuNgay.Location = new System.Drawing.Point(173, 12);
            this.dtpTuNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTuNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 36);
            this.dtpTuNgay.TabIndex = 1;
            this.dtpTuNgay.Value = new System.DateTime(2026, 5, 20, 21, 20, 47, 521);
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.dtpTuNgay_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(629, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 32);
            this.label3.TabIndex = 0;
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.BorderColor = System.Drawing.Color.Black;
            this.guna2CustomGradientPanel1.BorderRadius = 5;
            this.guna2CustomGradientPanel1.BorderThickness = 1;
            this.guna2CustomGradientPanel1.Controls.Add(this.lblTongBan1);
            this.guna2CustomGradientPanel1.Controls.Add(this.lblTongBan);
            this.guna2CustomGradientPanel1.FillColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.guna2CustomGradientPanel1.FillColor2 = System.Drawing.Color.LightBlue;
            this.guna2CustomGradientPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(417, 86);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(173, 91);
            this.guna2CustomGradientPanel1.TabIndex = 1;
            // 
            // lblTongBan1
            // 
            this.lblTongBan1.AutoSize = true;
            this.lblTongBan1.BackColor = System.Drawing.Color.Transparent;
            this.lblTongBan1.ForeColor = System.Drawing.Color.Black;
            this.lblTongBan1.Location = new System.Drawing.Point(-5, 0);
            this.lblTongBan1.Name = "lblTongBan1";
            this.lblTongBan1.Size = new System.Drawing.Size(181, 29);
            this.lblTongBan1.TabIndex = 0;
            this.lblTongBan1.Text = "Tổng doanh thu";
            // 
            // lblTongBan
            // 
            this.lblTongBan.AutoSize = true;
            this.lblTongBan.BackColor = System.Drawing.Color.Transparent;
            this.lblTongBan.ForeColor = System.Drawing.Color.DimGray;
            this.lblTongBan.Location = new System.Drawing.Point(27, 42);
            this.lblTongBan.Name = "lblTongBan";
            this.lblTongBan.Size = new System.Drawing.Size(116, 29);
            this.lblTongBan.TabIndex = 1;
            this.lblTongBan.Text = "Tổng bán";
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel.BorderColor = System.Drawing.Color.Black;
            this.panel.BorderRadius = 5;
            this.panel.BorderThickness = 1;
            this.panel.Controls.Add(this.label6);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.lblTongNhap);
            this.panel.FillColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel.FillColor2 = System.Drawing.Color.LightBlue;
            this.panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel.Location = new System.Drawing.Point(5, 86);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(181, 91);
            this.panel.TabIndex = 1;
            // 
            // lblTongNhap
            // 
            this.lblTongNhap.AutoSize = true;
            this.lblTongNhap.BackColor = System.Drawing.Color.Transparent;
            this.lblTongNhap.ForeColor = System.Drawing.Color.Black;
            this.lblTongNhap.Location = new System.Drawing.Point(25, 42);
            this.lblTongNhap.Name = "lblTongNhap";
            this.lblTongNhap.Size = new System.Drawing.Size(119, 29);
            this.lblTongNhap.TabIndex = 1;
            this.lblTongNhap.Text = "tổng nhập";
            // 
            // guna2CustomGradientPanel4
            // 
            this.guna2CustomGradientPanel4.BorderColor = System.Drawing.Color.Black;
            this.guna2CustomGradientPanel4.BorderRadius = 5;
            this.guna2CustomGradientPanel4.BorderThickness = 1;
            this.guna2CustomGradientPanel4.Controls.Add(this.lblLoiNhuan1);
            this.guna2CustomGradientPanel4.Controls.Add(this.lblLoiNhuan);
            this.guna2CustomGradientPanel4.FillColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.guna2CustomGradientPanel4.FillColor2 = System.Drawing.Color.LightBlue;
            this.guna2CustomGradientPanel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2CustomGradientPanel4.Location = new System.Drawing.Point(870, 86);
            this.guna2CustomGradientPanel4.Name = "guna2CustomGradientPanel4";
            this.guna2CustomGradientPanel4.Size = new System.Drawing.Size(152, 91);
            this.guna2CustomGradientPanel4.TabIndex = 1;
            // 
            // lblLoiNhuan1
            // 
            this.lblLoiNhuan1.AutoSize = true;
            this.lblLoiNhuan1.BackColor = System.Drawing.Color.Transparent;
            this.lblLoiNhuan1.ForeColor = System.Drawing.Color.Black;
            this.lblLoiNhuan1.Location = new System.Drawing.Point(3, 0);
            this.lblLoiNhuan1.Name = "lblLoiNhuan1";
            this.lblLoiNhuan1.Size = new System.Drawing.Size(117, 29);
            this.lblLoiNhuan1.TabIndex = 0;
            this.lblLoiNhuan1.Text = "Lợi nhuận";
            // 
            // lblLoiNhuan
            // 
            this.lblLoiNhuan.AutoSize = true;
            this.lblLoiNhuan.BackColor = System.Drawing.Color.Transparent;
            this.lblLoiNhuan.Location = new System.Drawing.Point(22, 42);
            this.lblLoiNhuan.Name = "lblLoiNhuan";
            this.lblLoiNhuan.Size = new System.Drawing.Size(117, 29);
            this.lblLoiNhuan.TabIndex = 1;
            this.lblLoiNhuan.Text = "Lợi nhuận";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(12, 412);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(214, 29);
            this.label5.TabIndex = 5;
            this.label5.Text = "Top sách bán chạy";
            // 
            // chartDoanhThu
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);
            this.chartDoanhThu.Location = new System.Drawing.Point(634, 194);
            this.chartDoanhThu.Name = "chartDoanhThu";
            this.chartDoanhThu.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series1.Name = "Series1";
            this.chartDoanhThu.Series.Add(series1);
            this.chartDoanhThu.Size = new System.Drawing.Size(408, 419);
            this.chartDoanhThu.TabIndex = 43;
            // 
            // chartTiLe
            // 
            chartArea2.Name = "ChartArea1";
            this.chartTiLe.ChartAreas.Add(chartArea2);
            legend1.Name = "Legend1";
            this.chartTiLe.Legends.Add(legend1);
            this.chartTiLe.Location = new System.Drawing.Point(5, 212);
            this.chartTiLe.Name = "chartTiLe";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartTiLe.Series.Add(series2);
            this.chartTiLe.Size = new System.Drawing.Size(609, 197);
            this.chartTiLe.TabIndex = 44;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(728, 616);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(239, 29);
            this.label8.TabIndex = 45;
            this.label8.Text = "Doanh thu theo tháng";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(285, 29);
            this.label9.TabIndex = 45;
            this.label9.Text = "Tỉ lệ doanh thu theo Sách";
            // 
            // dgvTopSach
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvTopSach.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTopSach.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTopSach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvTopSach.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTopSach.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTopSach.ColumnHeadersHeight = 20;
            this.dgvTopSach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTopSach.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTopSach.GridColor = System.Drawing.Color.LightGray;
            this.dgvTopSach.Location = new System.Drawing.Point(5, 458);
            this.dgvTopSach.Name = "dgvTopSach";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTopSach.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTopSach.RowHeadersVisible = false;
            this.dgvTopSach.RowHeadersWidth = 62;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvTopSach.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTopSach.RowTemplate.Height = 28;
            this.dgvTopSach.Size = new System.Drawing.Size(609, 187);
            this.dgvTopSach.TabIndex = 46;
            this.dgvTopSach.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvTopSach.ThemeStyle.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dgvTopSach.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvTopSach.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dgvTopSach.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvTopSach.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvTopSach.ThemeStyle.GridColor = System.Drawing.Color.LightGray;
            this.dgvTopSach.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.White;
            this.dgvTopSach.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvTopSach.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dgvTopSach.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvTopSach.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvTopSach.ThemeStyle.HeaderStyle.Height = 20;
            this.dgvTopSach.ThemeStyle.ReadOnly = false;
            this.dgvTopSach.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvTopSach.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTopSach.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTopSach.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvTopSach.ThemeStyle.RowsStyle.Height = 28;
            this.dgvTopSach.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvTopSach.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(-363, 313);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Top sách bán chạy";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(7, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 29);
            this.label6.TabIndex = 0;
            this.label6.Text = "Tổng nhập";
            // 
            // frmBCKinhDoanh
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1057, 665);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvTopSach);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chartTiLe);
            this.Controls.Add(this.chartDoanhThu);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.guna2CustomGradientPanel4);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBCKinhDoanh";
            this.Opacity = 0.7D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Báo cáo kết quả hoạt động kinh doanh";
            this.Load += new System.EventHandler(this.frmBCKinhDoanh_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.guna2CustomGradientPanel4.ResumeLayout(false);
            this.guna2CustomGradientPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTiLe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDenNgay;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel panel;
        private System.Windows.Forms.Label lblTongBan1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel4;
        private System.Windows.Forms.Label lblLoiNhuan1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTiLe;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2DataGridView dgvTopSach;
        private System.Windows.Forms.Label lblTongBan;
        private System.Windows.Forms.Label lblTongNhap;
        private System.Windows.Forms.Label lblLoiNhuan;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
    }
}