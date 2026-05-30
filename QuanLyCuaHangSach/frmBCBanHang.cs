using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
// Cần cài đặt thư viện EPPlus qua NuGet để sử dụng tính năng xuất Excel
namespace QuanLyCuaHangSach
{
    public partial class frmBCBanHang : Form
    {

        public frmBCBanHang()
        {
            InitializeComponent();
        }

        private void frmBCBanHang_Load(object sender, EventArgs e)
        {
             try
            {
                DAO.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối cơ sở dữ liệu thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

               KhoiTaoCacBoLoc();
               TaiDuLieuBaoCao();
               this.AcceptButton = btnTimKiem;
            chartDoanhThu.MouseMove += (s, ev) => {
                var res = chartDoanhThu.HitTest(ev.X, ev.Y);
                if (res.ChartElementType ==
                    System.Windows.Forms.DataVisualization.Charting.ChartElementType.DataPoint)
                {
                    var dp = chartDoanhThu.Series[0].Points[res.PointIndex];
                    toolTip1.SetToolTip(chartDoanhThu,
                        $"Thời gian: {dp.AxisLabel}\nDoanh thu: {dp.YValues[0]:N0} đ");
                }
                else toolTip1.SetToolTip(chartDoanhThu, "");
            };

            chartSachBan.MouseMove += (s, ev) => {
                var res = chartSachBan.HitTest(ev.X, ev.Y);
                if (res.ChartElementType ==
                    System.Windows.Forms.DataVisualization.Charting.ChartElementType.DataPoint)
                {
                    var dp = chartSachBan.Series[0].Points[res.PointIndex];
                    toolTip1.SetToolTip(chartSachBan,
                        $"Sách: {dp.AxisLabel}\nSố lượng: {dp.YValues[0]:N0}");
                }
                else toolTip1.SetToolTip(chartSachBan, "");
            };

            chartTiLeDanhMuc.MouseMove += (s, ev) => {
                var res = chartTiLeDanhMuc.HitTest(ev.X, ev.Y);
                if (res.ChartElementType ==
                    System.Windows.Forms.DataVisualization.Charting.ChartElementType.DataPoint)
                {
                    var dp = chartTiLeDanhMuc.Series[0].Points[res.PointIndex];
                    double total = chartTiLeDanhMuc.Series[0].Points
                        .Sum(p => p.YValues[0]);
                    double pct = total > 0 ? dp.YValues[0] / total * 100 : 0;
                    toolTip1.SetToolTip(chartTiLeDanhMuc,
                        $"Danh mục: {dp.AxisLabel}\nSố lượng: {dp.YValues[0]:N0}\nTỷ lệ: {pct:F1}%");
                }
                else toolTip1.SetToolTip(chartTiLeDanhMuc, "");
            };
        }
        private void KhoiTaoCacBoLoc()
        {
            
            cboNgay.Items.Add("Tất cả");
            for (int i = 1; i <= 31; i++) cboNgay.Items.Add(i.ToString());
            cboNgay.SelectedIndex = 0;

            cboThang.Items.Add("Tất cả");
            for (int i = 1; i <= 12; i++) cboThang.Items.Add(i.ToString());
            cboThang.SelectedIndex = 0;

            cboNam.Items.AddRange(new object[] { "Tất cả","2023","2024", "2025", "2026" });
            cboNam.SelectedIndex = 0; 

            cboTrangThaiDon.Items.AddRange(new object[] { "Tất cả", "Chờ xử lý", "Đang giao", "Đã giao", "Hủy", "Hoàn thành" });
            cboTrangThaiDon.SelectedIndex = 0;

            try
            {
                string sqlDanhMuc = "SELECT ma_danh_muc, ten_danh_muc FROM tblDanhMuc";
                DAO.FillDataToCombo(sqlDanhMuc, cboDanhMuc, "ma_danh_muc", "ten_danh_muc");

                // Thêm lựa chọn "Tất cả" vào đầu danh sách danh mục sách
                DataTable dtDM = (DataTable)cboDanhMuc.DataSource;
                DataRow dr = dtDM.NewRow();
                dr["ma_danh_muc"] = 0; // Quy ước số 0 là chọn tất cả
                dr["ten_danh_muc"] = "Tất cả";
                dtDM.Rows.InsertAt(dr, 0);

                cboDanhMuc.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách danh mục: " + ex.Message);
            }
        }

        private void TaiDuLieuBaoCao()
        {
            DateTime ngayChon;
            ngayChon = LayNgayDuocChon();
            //  chọn ngày ở tương lai (chỉ check khi chọn năm cụ thể)
            if(ngayChon.Year > DateTime.Today.Year || 
               (ngayChon.Year == DateTime.Today.Year && ngayChon.Month > DateTime.Today.Month) ||
               (ngayChon.Year == DateTime.Today.Year && ngayChon.Month == DateTime.Today.Month && ngayChon.Day > DateTime.Today.Day))
            if (ngayChon.Date > DateTime.Today)
            {
                MessageBox.Show("Ngày được chọn là ngày ở tương lai! Vui lòng chọn lại ngày hiện tại hoặc quá khứ.",
                               "Thông báo ngày không hợp lệ",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }
            // =======================================================
            // PHẦN A: XÂY DỰNG CHUỖI ĐIỀU KIỆN LỌC ĐỘNG 
            // =======================================================
            string dieuKien = " WHERE 1=1 ";

            // Lọc theo Năm (Độc lập)
            if (cboNam.SelectedIndex > 0 && cboNam.Text != "Tất cả")
            {
                dieuKien += " AND YEAR(dh.ngay_dat) = " + cboNam.Text;
            }
            // Lọc theo Tháng 
            if (cboThang.SelectedIndex > 0 && cboThang.Text != "Tất cả")
            {
                dieuKien += " AND MONTH(dh.ngay_dat) = " + cboThang.Text;
            }
           // Lọc theo Ngày 
            if (cboNgay.SelectedIndex > 0 && cboNgay.Text != "Tất cả")
            {
                dieuKien += " AND DAY(dh.ngay_dat) = " + cboNgay.Text;
            }
                
            if (cboTrangThaiDon.SelectedIndex > 0)
            {
                dieuKien += " AND dh.trang_thai = N'" + cboTrangThaiDon.Text + "'";
            }
            if (cboDanhMuc.SelectedIndex > 0)
            {
                dieuKien += " AND s.ma_danh_muc = '" + cboDanhMuc.SelectedValue.ToString()+"'";
            }
            
                if (!string.IsNullOrEmpty(txtTenSach.Text.Trim()))
                {
                    // Loại bỏ dấu nháy đơn để chống SQL Injection
                    string tenSach = txtTenSach.Text.Trim().Replace("'", "''");

                    // Ép SQL Server so sánh theo chuẩn mã Latin1_General_CI_AI (Không phân biệt hoa thường, không phân biệt dấu)
                    dieuKien += " AND s.ten_sach COLLATE Latin1_General_CI_AI LIKE N'%" + tenSach + "%' ";
                }
            

            // =======================================================
            // PHẦN B: ĐẨY DỮ LIỆU LÊN DATAGRIDVIEW
            // =======================================================
            string sqlGrid = @"
        SELECT dh.ma_don_hang AS [Mã Đơn Hàng], 
               dh.ngay_dat AS [Ngày Đặt], 
               s.ten_sach AS [Tên Sách], 
               dm.ten_danh_muc AS [Danh Mục], 
               ct.so_luong AS [Số Lượng], 
               ct.gia_ban AS [Giá Bán], 
               (ct.so_luong * ct.gia_ban) AS [Thành Tiền], 
               dh.trang_thai AS [Trạng Thái]
        FROM tblDonHang dh
        JOIN tblChiTietDonHang ct ON dh.ma_don_hang = ct.ma_don_hang
        JOIN tblSach s ON ct.ma_sach = s.ma_sach
        JOIN tblDanhMuc dm ON s.ma_danh_muc = dm.ma_danh_muc" + dieuKien;

            try
            {
                DataTable dtGrid = DAO.LoadDataToTable(sqlGrid);
                dgvBanHang.DataSource = dtGrid;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị lưới bán hàng: " + ex.Message);
            }

            // =======================================================
            // PHẦN C: TÍNH TOÁN CÁC SỐ LIỆU TỔNG QUAN KPIs
            // =======================================================
            string sqlKPI = @"
        SELECT COUNT(DISTINCT dh.ma_don_hang) AS TongDon, 
               ISNULL(SUM(ct.so_luong), 0) AS TongSach, 
               ISNULL(SUM(ct.so_luong * ct.gia_ban), 0) AS TongTien
        FROM tblDonHang dh
        JOIN tblChiTietDonHang ct ON dh.ma_don_hang = ct.ma_don_hang
        JOIN tblSach s ON ct.ma_sach = s.ma_sach
        JOIN tblDanhMuc dm ON s.ma_danh_muc = dm.ma_danh_muc" + dieuKien;

            try
            {
                DataTable dtKPI = DAO.LoadDataToTable(sqlKPI);
                if (dtKPI.Rows.Count > 0)
                {
                    DataRow r = dtKPI.Rows[0];
                    lblTongSoDon.Text = "Tổng số đơn: " + r["TongDon"].ToString();
                    lblTongSoSachBan.Text = "Tổng số sách bán: " + Convert.ToDouble(r["TongSach"]).ToString("N0");
                    lblTongDoanhThu.Text = "Tổng doanh thu: " + Convert.ToDecimal(r["TongTien"]).ToString("N0") + " đ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính toán KPIs: " + ex.Message);
            }

            // =======================================================
            // PHẦN D: XỬ LÝ BIỂU ĐỒ DOANH THU ĐỘNG (LINE CHART)
            // =======================================================
            string sqlChartDT = "";

            // TRƯỜNG HỢP 1: Nếu Năm chọn "Tất cả" -> Gom nhóm hiển thị theo các NĂM (2024, 2025, 2026...)
            if (cboNam.SelectedIndex == 0 || cboNam.Text == "Tất cả")
            {
                sqlChartDT = "SELECT YEAR(dh.ngay_dat) AS ThoiGian, SUM(ct.so_luong * ct.gia_ban) AS DoanhThu " +
                             "FROM tblDonHang dh JOIN tblChiTietDonHang ct ON dh.ma_don_hang = ct.ma_don_hang " +
                             "JOIN tblSach s ON ct.ma_sach = s.ma_sach " +
                             dieuKien +
                             " GROUP BY YEAR(dh.ngay_dat) " +
                             " ORDER BY YEAR(dh.ngay_dat) ASC";
            }
            // TRƯỜNG HỢP 2: Nếu đã chọn 1 Năm cụ thể nhưng Tháng chọn "Tất cả" -> Gom nhóm hiển thị các THÁNG (1-12)
            else if (cboThang.SelectedIndex == 0 || cboThang.Text == "Tất cả")
            {
                sqlChartDT = "SELECT MONTH(dh.ngay_dat) AS ThoiGian, SUM(ct.so_luong * ct.gia_ban) AS DoanhThu " +
                             "FROM tblDonHang dh JOIN tblChiTietDonHang ct ON dh.ma_don_hang = ct.ma_don_hang " +
                             "JOIN tblSach s ON ct.ma_sach = s.ma_sach " +
                             dieuKien +
                             " GROUP BY MONTH(dh.ngay_dat) " +
                             " ORDER BY MONTH(dh.ngay_dat) ASC";
            }
            // TRƯỜNG HỢP 3: Nếu chọn cả Năm và Tháng cụ thể -> Gom nhóm hiển thị các NGÀY trong tháng đó
            else
            {
                sqlChartDT = "SELECT DAY(dh.ngay_dat) AS ThoiGian, SUM(ct.so_luong * ct.gia_ban) AS DoanhThu " +
                             "FROM tblDonHang dh JOIN tblChiTietDonHang ct ON dh.ma_don_hang = ct.ma_don_hang " +
                             "JOIN tblSach s ON ct.ma_sach = s.ma_sach " +
                             dieuKien +
                             " GROUP BY DAY(dh.ngay_dat) " +
                             " ORDER BY DAY(dh.ngay_dat) ASC";
            }

            // Thực hiện nạp dữ liệu vẽ biểu đồ đường
            try
            {
                DataTable dtChartDT = DAO.LoadDataToTable(sqlChartDT);
                chartDoanhThu.Series.Clear();

                System.Windows.Forms.DataVisualization.Charting.Series seriesDT = chartDoanhThu.Series.Add("Doanh thu");
                seriesDT.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line; 
                seriesDT.XValueMember = "ThoiGian";
                seriesDT.YValueMembers = "DoanhThu";

                // Trang trí màu sắc & hiệu ứng
                seriesDT.Color = System.Drawing.Color.DodgerBlue; // Màu xanh dương hiện đại
                seriesDT.BorderWidth = 2; // Làm đường vẽ dày dặn, sắc nét
                seriesDT.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle; // Chấm tròn tại nút mốc
                seriesDT.MarkerSize = 5;
              
                 chartDoanhThu.DataSource = dtChartDT;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị biểu đồ doanh thu: " + ex.Message, "Lỗi đồ thị", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // =======================================================
            // E. VẼ BIỂU ĐỒ TOP 5 SÁCH BÁN CHẠY (COLUMN CHART)
            // =======================================================
            // Câu lệnh SQL lấy ra Top 5 cuốn sách có tổng số lượng bán nhiều nhất kèm theo bộ lọc động
            string sqlChartSach = @"
        SELECT TOP 5 s.ten_sach AS TenSach, SUM(ct.so_luong) AS SL 
        FROM tblDonHang dh 
        JOIN tblChiTietDonHang ct ON dh.ma_don_hang = ct.ma_don_hang 
        JOIN tblSach s ON ct.ma_sach = s.ma_sach 
        " + dieuKien + @"
        GROUP BY s.ten_sach 
        ORDER BY SL DESC";

            try
            {
                DataTable dtChartSach = DAO.LoadDataToTable(sqlChartSach);
                chartSachBan.Series.Clear();

                System.Windows.Forms.DataVisualization.Charting.Series seriesSach = chartSachBan.Series.Add("Số lượng");
                seriesSach.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar; // ĐỒI THÀNH BAR (CỘT NGANG)
                seriesSach.XValueMember = "TenSach";
                seriesSach.YValueMembers = "SL";

                // Trang trí
                
                seriesSach.IsValueShownAsLabel = true; // Hiện số lượng bán ở cuối mỗi thanh cột
                chartSachBan.ChartAreas[0].AxisX.Interval = 1;

                // Đảo ngược trục để cuốn bán chạy nhất nằm lên trên cùng
                chartSachBan.ChartAreas[0].AxisX.IsReversed = true;

                chartSachBan.DataSource = dtChartSach;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị biểu đồ top sách bán chạy: " + ex.Message, "Lỗi đồ thị", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // =======================================================
            // 6. VẼ BIỂU ĐỒ CƠ CẤU DANH MỤC SÁCH BÁN (PIE CHART)
            // =======================================================
            // Câu lệnh SQL gom nhóm theo tên danh mục và tính tổng số lượng bán ra
            string sqlChartDM = @"
        SELECT dm.ten_danh_muc AS TenDM, SUM(ct.so_luong) AS SL 
        FROM tblDonHang dh 
        JOIN tblChiTietDonHang ct ON dh.ma_don_hang = ct.ma_don_hang 
        JOIN tblSach s ON ct.ma_sach = s.ma_sach 
        JOIN tblDanhMuc dm ON s.ma_danh_muc = dm.ma_danh_muc 
        " + dieuKien + @"
        GROUP BY dm.ten_danh_muc";

            try
            {
                // Lấy dữ liệu từ Database
                DataTable dtChartDM = DAO.LoadDataToTable(sqlChartDM);
                chartTiLeDanhMuc.Series.Clear(); // Xóa dữ liệu mẫu ban đầu

                // Tạo một loạt dữ liệu mới dạng tròn tên là "Tỷ lệ"
                System.Windows.Forms.DataVisualization.Charting.Series seriesDM = chartTiLeDanhMuc.Series.Add("Tỷ lệ");
                seriesDM.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie; // Đặt kiểu đồ thị hình tròn (Bánh)

                // Gán dữ liệu vào các trục
                seriesDM.XValueMember = "TenDM"; // Tên danh mục sẽ hiển thị ở cột chú thích màu (Legend)
                seriesDM.YValueMembers = "SL";    // Số lượng bán để chia góc hình tròn

                // --- ĐỊNH DẠNG HIỂN THỊ PHẦN TRĂM (%) CHO ĐẸP VÀ CHUYÊN NGHIỆP ---
                // Hiển thị phần trăm trực tiếp lên từng miếng bánh hình tròn
                seriesDM.IsValueShownAsLabel = true;
                chartTiLeDanhMuc.Series["Tỷ lệ"].Label = "#PERCENT"; // Định dạng số thập phân kèm ký tự %

                // Đẩy tên danh mục ra ngoài bảng chú thích (Legend) bên cạnh để hình tròn nhìn sạch sẽ, không bị đè chữ
                chartTiLeDanhMuc.Series[0]["PieLabelStyle"] = "Inside"; // Hiện số % ở trong miếng bánh
                chartTiLeDanhMuc.Series[0].LegendText = "#VALX (#PERCENT)"; // Hiện tên danh mục + % ở bảng chú giải bên cạnh

                // ĐẶC BIỆT: Thêm hiệu ứng 3D nghiêng nhẹ nhìn "pro" hơn hẳn
                 chartTiLeDanhMuc.ChartAreas[0].Area3DStyle.Enable3D = true;
                chartTiLeDanhMuc.ChartAreas[0].Area3DStyle.Inclination = 40; // Góc nghiêng khối 3D
                // Nạp dữ liệu để hiển thị lên giao diện
                chartTiLeDanhMuc.DataSource = dtChartDM;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị biểu đồ cơ cấu danh mục: " + ex.Message, "Lỗi đồ thị", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DateTime LayNgayDuocChon()
        {
            // =======================================================
            //  KIỂM TRA CHỐNG ĐỂ TRỐNG (NULL HOẶC EMPTY)
            // =======================================================
            if (string.IsNullOrEmpty(cboNgay.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập hoặc chọn Ngày (hoặc chọn 'Tất cả')!", "Thông báo trống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNgay.Focus(); // Nhấp nháy con trỏ vào ô Ngày cho người dùng nhập lại
                return DateTime.MinValue; // Trả về mốc lỗi để chặn các hàm sau
            }

            if (string.IsNullOrEmpty(cboThang.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập hoặc chọn Tháng (hoặc chọn 'Tất cả')!", "Thông báo trống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboThang.Focus();
                return DateTime.MinValue;
            }

            if (string.IsNullOrEmpty(cboNam.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập hoặc chọn Năm (hoặc chọn 'Tất cả')!", "Thông báo trống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNam.Focus();
                return DateTime.MinValue;
            }

            // =======================================================
            //  LOGIC TỰ ĐỘNG TÍNH TOÁN KHI DỮ LIỆU ĐÃ HỢP LỆ
            // =======================================================
            // Xử lý Năm
            if (cboNam.Text != "Tất cả")
            {
                int ngay = DateTime.Today.Day;
                int thang = DateTime.Today.Month;
                int nam = DateTime.Today.Year;
                // Phòng trường hợp người dùng gõ bậy ký tự chữ thay vì gõ số
                if (!int.TryParse(cboNam.Text, out nam))
                {
                    MessageBox.Show("Năm nhập vào phải là một số nguyên hợp lệ!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return DateTime.MinValue;
                }
                if (nam < 2000 || nam > DateTime.Today.Year)
                {
                    MessageBox.Show("Năm nhập vào phải từ 2000 đến năm hiện tại!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return DateTime.MinValue;
                }
                else
                {
                    if (cboThang.Text != "Tất cả")
                    {
                        if (!int.TryParse(cboThang.Text, out thang) || thang < 1 || thang > 12)
                        {
                            MessageBox.Show("Tháng nhập vào phải là số từ 1 đến 12!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return DateTime.MinValue;
                        }
                        if (nam == DateTime.Today.Year && thang > DateTime.Today.Month)
                        {
                            MessageBox.Show("Tháng nhập vào không được lớn hơn tháng hiện tại của năm " + DateTime.Today.Year + "!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return DateTime.MinValue;
                        }
                        if (cboNgay.Text != "Tất cả")
                        {
                            if (!int.TryParse(cboNgay.Text, out ngay) || ngay < 1 || ngay > 31)
                            {
                                MessageBox.Show("Ngày nhập vào phải là số từ 1 đến 31!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return DateTime.MinValue;

                            }
                            if (nam == DateTime.Today.Year && thang == DateTime.Today.Month && ngay > DateTime.Today.Day)
                            {
                                MessageBox.Show("Ngày nhập vào không được lớn hơn ngày hiện tại của tháng " + DateTime.Today.Month + " năm " + DateTime.Today.Year + "!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return DateTime.MinValue;
                            }
                        }

                    }



                    if (cboThang.Text != "Tất cả")
                    {
                        if (!int.TryParse(cboThang.Text, out thang) || thang < 1 || thang > 12)
                        {
                            MessageBox.Show("Tháng nhập vào phải là số từ 1 đến 12!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return DateTime.MinValue;
                        }
                    }

                    if (cboNgay.Text != "Tất cả")
                    {
                        if (!int.TryParse(cboNgay.Text, out ngay) || ngay < 1 || ngay > 31)
                        {
                            MessageBox.Show("Ngày nhập vào phải là số từ 1 đến 31!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return DateTime.MinValue;
                        }
                    }


                    // Ép ngày hợp lệ (ngày 31 tháng 4 -> tự về 30)
                    int ngayToiDa = DateTime.DaysInMonth(nam, thang);
                    if (ngay > ngayToiDa)
                    {
                        MessageBox.Show(" Do ngày không có trong tháng tự động  chọn ngày lớn nhất có trong tháng " + cboThang.Text + " năm " + cboNam.Text + "!", "Thông báo lỗi " +
               "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ngay = ngayToiDa;
                        cboNgay.Text = ngay.ToString();
                    }

                    return new DateTime(nam, thang, ngay);
                }
            }
                      
            else
            {
                // Nếu Năm chọn "Tất cả" thì trả về ngày mặc định (không dùng đến) để tránh lỗi
                return DateTime.MinValue;
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TaiDuLieuBaoCao(); 
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cboNgay.SelectedIndex = 0;
            cboThang.SelectedIndex = 0;
            cboNam.SelectedIndex = 0; 
            cboTrangThaiDon.SelectedIndex = 0;
            cboDanhMuc.SelectedIndex = 0;
            txtTenSach.Clear();

            TaiDuLieuBaoCao(); 
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem DataGridView có dữ liệu để xuất không
            if (dgvBanHang.Rows.Count == 0 || dgvBanHang.DataSource == null)
            {
                MessageBox.Show("Không có dữ liệu trên lưới để xuất file Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ExcelPackage.License.SetNonCommercialPersonal("LeBichThuy");

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Title = "Chọn nơi lưu file báo cáo bán hàng";
            saveFileDialog.FileName = "BaoCaoBanHang_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (OfficeOpenXml.ExcelPackage excelPackage = new OfficeOpenXml.ExcelPackage())
                    {
                        OfficeOpenXml.ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Báo Cáo Doanh Thu");

                        // --- PHẦN 1: TẠO DÒNG TIÊU ĐỀ CỘT ---
                        int colCount = dgvBanHang.Columns.Count;
                        for (int i = 0; i < colCount; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = dgvBanHang.Columns[i].HeaderText;
                            worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                            worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        }

                        // --- PHẦN 2: ĐỔ DỮ LIỆU TỪ GRIDVIEW VÀO EXCEL ---
                        DataTable dt = (DataTable)dgvBanHang.DataSource;
                        for (int row = 0; row < dt.Rows.Count; row++)
                        {
                            for (int col = 0; col < colCount; col++)
                            {
                                var cellValue = dt.Rows[row][col];
                                if (cellValue == DBNull.Value) continue;

                                if (dgvBanHang.Columns[col].HeaderText == "Ngày Đặt")
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = Convert.ToDateTime(cellValue);
                                    worksheet.Cells[row + 2, col + 1].Style.Numberformat.Format = "dd/MM/yyyy";
                                }
                                else if (dgvBanHang.Columns[col].HeaderText == "Giá Bán" || dgvBanHang.Columns[col].HeaderText == "Thành Tiền")
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = Convert.ToDouble(cellValue);
                                    worksheet.Cells[row + 2, col + 1].Style.Numberformat.Format = "#,##0";
                                }
                                else
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = cellValue;
                                }
                            }
                        }

                          if (worksheet.Dimension != null)
                        {
                            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                        }

                        // --- PHẦN 3: LƯU FILE XUỐNG Ổ CỨNG ---
                        FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                        excelPackage.SaveAs(fileInfo);
                    }

                    MessageBox.Show("Xuất báo cáo ra file Excel thành công!\nĐường dẫn: " + saveFileDialog.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình xuất file: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmBCBanHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DAO.Close();
        }
    }
    
}
