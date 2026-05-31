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
    public partial class frmBCKinhDoanh : Form
    {
        public frmBCKinhDoanh()
        {
            InitializeComponent();
        }
        private bool _isLoaded = false;
        private void frmBCKinhDoanh_Load(object sender, EventArgs e)
        {
            _isLoaded = true;
            LoadThongKe();
            
        }
        private void LoadThongKe()
        {
            try
            {
                DAO.Connect();

                string tuNgay = DAO.getSQLdateFromText(dtpTuNgay.Value.ToString("dd/MM/yyyy"));
                string denNgay = DAO.getSQLdateFromText(dtpDenNgay.Value.ToString("dd/MM/yyyy"));

                string sqlNhap = $@"SELECT ISNULL(SUM(tong_tien), 0) FROM tblPhieuNhap 
                            WHERE trang_thai = N'Đã nhập kho'
                            AND ngay_nhap BETWEEN '{tuNgay}' AND '{denNgay}'";
                decimal chiPhi = decimal.Parse(DAO.getFieldValue(sqlNhap));
                lblTongNhap.Text = chiPhi.ToString("N0") + " VNĐ";
                lblTongNhap.ForeColor = Color.Black;

                string sqlBan = $@"SELECT ISNULL(SUM(tong_tien), 0) FROM tblDonHang 
                           WHERE trang_thai = N'Hoàn thành'
                           AND ngay_dat BETWEEN '{tuNgay}' AND '{denNgay}'";
                decimal doanhThu = decimal.Parse(DAO.getFieldValue(sqlBan));
                lblTongBan.Text = doanhThu.ToString("N0") + " VNĐ";
                lblTongBan.ForeColor = Color.Black;

                decimal loiNhuan = doanhThu - chiPhi;
                lblLoiNhuan.Text = loiNhuan.ToString("N0") + " VNĐ";
                lblLoiNhuan.ForeColor = loiNhuan >= 0 ? Color.Green : Color.Red;
                //top sach ban
                string sqlTop = $@"SELECT TOP 5 s.ten_sach, SUM(ct.so_luong) as so_luong
                           FROM tblChiTietDonHang ct
                           JOIN tblSach s ON ct.ma_sach = s.ma_sach
                           JOIN tblDonHang dh ON ct.ma_don_hang = dh.ma_don_hang
                           WHERE dh.trang_thai = N'Hoàn thành'
                           AND dh.ngay_dat BETWEEN '{tuNgay}' AND '{denNgay}'
                           GROUP BY s.ten_sach
                           ORDER BY so_luong DESC";
                DataTable dtTop = DAO.LoadDataToTable(sqlTop);
                dgvTopSach.DataSource = dtTop;

                if (dtTop.Columns.Contains("ten_sach") && dtTop.Columns.Contains("so_luong"))
                {
                    dgvTopSach.ColumnHeadersVisible = true;
                    dgvTopSach.Columns["ten_sach"].HeaderText = "Tên sách";
                    dgvTopSach.Columns["so_luong"].HeaderText = "Số lượng bán";
                    dgvTopSach.Columns["ten_sach"].Width = 400;
                    dgvTopSach.Columns["so_luong"].Width = 150;
                    dgvTopSach.Columns["ten_sach"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                //Doanh thu theo thang
                string sqlThang = $@"SELECT YEAR(ngay_dat) as nam,
                            MONTH(ngay_dat) as thang,
                            SUM(tong_tien) as doanh_thu
                             FROM tblDonHang
                             WHERE trang_thai = N'Hoàn thành'
                             AND ngay_dat BETWEEN '{tuNgay}' AND '{denNgay}'
                             GROUP BY YEAR(ngay_dat), MONTH(ngay_dat)
                             ORDER BY nam, thang";
                DataTable dtThang = DAO.LoadDataToTable(sqlThang);

                chartDoanhThu.Series.Clear();
                chartDoanhThu.ChartAreas[0].AxisX.Title = "Tháng";
                chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu";
                var seriesBar = chartDoanhThu.Series.Add("Doanh thu");
                seriesBar.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                foreach (DataRow row in dtThang.Rows)
                {
                    seriesBar.Points.AddXY(
                        $"T{row["thang"]}/{row["nam"]}",
                        Convert.ToDecimal(row["doanh_thu"])
                    );
                }
             
                foreach (var point in seriesBar.Points)
                {
                    point.ToolTip = "#AXISLABEL\nDoanh thu: #VALY{N0} VNĐ";
                }

                //Ti le sach ban theo danh muc
                string sqlTiLe = $@"SELECT TOP 5 s.ten_sach, SUM(ct.so_luong * ct.gia_ban) as doanh_thu
                    FROM tblChiTietDonHang ct
                    JOIN tblSach s ON ct.ma_sach = s.ma_sach
                    JOIN tblDonHang dh ON ct.ma_don_hang = dh.ma_don_hang
                    WHERE dh.trang_thai = N'Hoàn thành'
                    AND dh.ngay_dat BETWEEN '{tuNgay}' AND '{denNgay}'
                    GROUP BY s.ten_sach
                    ORDER BY doanh_thu DESC";
                DataTable dtTiLe = DAO.LoadDataToTable(sqlTiLe);

                chartTiLe.Series.Clear();
                var seriesPie = chartTiLe.Series.Add("Tỉ lệ");
                seriesPie.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                foreach (DataRow row in dtTiLe.Rows)
                {
                    seriesPie.Points.AddXY(row["ten_sach"].ToString(), Convert.ToDecimal(row["doanh_thu"]));
                }
               
                foreach (var point in seriesPie.Points)
                {
                    point.ToolTip = "#AXISLABEL\nDoanh thu: #VALY{N0} VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                DAO.Close();
            }
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (_isLoaded) LoadThongKe();
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (_isLoaded) LoadThongKe();
        }

       
    }
}
