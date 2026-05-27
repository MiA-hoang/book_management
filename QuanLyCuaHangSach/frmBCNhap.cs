using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public partial class frmBCNhap : Form
    {
        public frmBCNhap()
        {
            InitializeComponent();
        }

        private void frmBCNhap_Load(object sender, EventArgs e)
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
            CapNhatTongSoSachTonKho();
            this.AcceptButton = btnTimKiem;
        }
        private void KhoiTaoCacBoLoc()
        {
            cboNgay.Items.Add("Tất cả");
            for (int i = 1; i <= 31; i++) cboNgay.Items.Add(i.ToString());
            cboNgay.SelectedIndex = 0;

            cboThang.Items.Add("Tất cả");
            for (int i = 1; i <= 12; i++) cboThang.Items.Add(i.ToString());
            cboThang.SelectedIndex = 0;

            cboNam.Items.AddRange(new object[] { "Tất cả","2023" ,"2024", "2025", "2026" });
            cboNam.SelectedIndex = 0;

            try
            {
                string sqlDanhMuc = "SELECT ma_danh_muc, ten_danh_muc FROM tblDanhMuc";
                DAO.FillDataToCombo(sqlDanhMuc, cboDanhMuc, "ma_danh_muc", "ten_danh_muc");

                // Thêm tùy chọn "Tất cả" vào đầu ComboBox Danh mục
                DataTable dtDM = (DataTable)cboDanhMuc.DataSource;
                if (dtDM != null)
                {
                    DataRow dr = dtDM.NewRow();
                    dr["ma_danh_muc"] = 0; // Quy ước 0 là chọn tất cả danh mục
                    dr["ten_danh_muc"] = "Tất cả";
                    dtDM.Rows.InsertAt(dr, 0);
                    cboDanhMuc.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách danh mục sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TaiDuLieuBaoCao()
        {
            // Gồm: Biểu đồ chi phí nhập hàng, biểu đồ số lượng sách nhập kho, số liệu tổng quan KPIs (tổng đơn nhập, tổng sách nhập, tổng chi phí nhập)//
            // và danh sách chi tiết các phiếu nhập theo bộ lọc ngày tháng năm + tên sách + danh mục //
            // =======================================================
            // PHẦN A: XÂY DỰNG CHUỖI ĐIỀU KIỆN LỌC ĐỘNG 
            // =======================================================
            string dieuKien = " WHERE 1=1 ";

            if (cboNam.SelectedIndex > 0 && cboNam.Text != "Tất cả")
            {
                dieuKien += " AND YEAR(dn.ngay_nhap) = " + cboNam.Text;
            }
            if (cboThang.SelectedIndex > 0 && cboThang.Text != "Tất cả")
            {
                dieuKien += " AND MONTH(dn.ngay_nhap) = " + cboThang.Text;
            }
            if (cboNgay.SelectedIndex > 0 && cboNgay.Text != "Tất cả")
            {
                dieuKien += " AND DAY(dn.ngay_nhap) = " + cboNgay.Text;
            }

            if (cboDanhMuc.SelectedIndex > 0 && cboDanhMuc.SelectedValue != null)
            {
                dieuKien += " AND s.ma_danh_muc = '" + cboDanhMuc.SelectedValue.ToString() + "'";
            }

            if (!string.IsNullOrEmpty(txtTenSach.Text.Trim()))
            {
                string tenSach = txtTenSach.Text.Trim().Replace("'", "''");
                dieuKien += " AND s.ten_sach COLLATE Latin1_General_CI_AI LIKE N'%" + tenSach + "%' ";
            }

            // =======================================================
            // PHẦN B: TẢI DỮ LIỆU LÊN BẢNG HIỂN THỊ DATAGRIDVIEW
            // =======================================================
            string sqlGrid = @"
                SELECT dn.ma_phieu_nhap AS [Mã Đơn Nhập], 
                       dn.ngay_nhap AS [Ngày Nhập], 
                       s.ten_sach AS [Tên Sách], 
                       dm.ten_danh_muc AS [Danh Mục], 
                       ct.so_luong AS [Số Lượng], 
                       ct.gia_nhap AS [Giá Nhập], 
                       (ct.so_luong * ct.gia_nhap) AS [Thành Tiền] 
                      
                FROM tblPhieuNhap dn
                JOIN tblChiTietNhap ct ON dn.ma_phieu_nhap = ct.ma_phieu_nhap
                JOIN tblSach s ON ct.ma_sach = s.ma_sach
                JOIN tblDanhMuc dm ON s.ma_danh_muc = dm.ma_danh_muc" + dieuKien;

            try
            {
                DataTable dtGrid = DAO.LoadDataToTable(sqlGrid);
                dgvNhapHang.DataSource = dtGrid;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị danh sách lưới nhập hàng: " + ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // =======================================================
            // PHẦN C: TÍNH TOÁN VÀ ĐIỀU CHỈNH CÁC SỐ LIỆU TỔNG QUAN KPIs ( trừ tổng tồn kho)
            // =======================================================
            string sqlKPI = @"
                SELECT COUNT(DISTINCT dn.ma_phieu_nhap) AS TongDonNhap, 
                       ISNULL(SUM(ct.so_luong), 0) AS TongSachNhap, 
                       ISNULL(SUM(ct.so_luong * ct.gia_nhap), 0) AS TongChiPhi
                FROM tblPhieuNhap dn
                JOIN tblChiTietNhap ct ON dn.ma_phieu_nhap = ct.ma_phieu_nhap
                JOIN tblSach s ON ct.ma_sach = s.ma_sach
                JOIN tblDanhMuc dm ON s.ma_danh_muc = dm.ma_danh_muc" + dieuKien;

            try
            {
                DataTable dtKPI = DAO.LoadDataToTable(sqlKPI);
                if (dtKPI.Rows.Count > 0)
                {
                    DataRow r = dtKPI.Rows[0];
                    lblTongSoDonNhap.Text = "Tổng số đơn nhập: " + r["TongDonNhap"].ToString();
                    lblTongSoSachNhapKho.Text = "Tổng số sách nhập kho: " + Convert.ToDouble(r["TongSachNhap"]).ToString("N0");
                     lblTongChiPhiNhapHang.Text = "Tổng chi phi nhập: " + Convert.ToDecimal(r["TongChiPhi"]).ToString("N0") + " đ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính toán các số liệu KPIs nhập hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // =======================================================
            // PHẦN D: BIỂU ĐỒ CHI PHÍ NHẬP HÀNG (AREA CHART - BIỂU ĐỒ VÙNG)
            // =======================================================
            string sqlChartCP = "";

            if (cboNam.SelectedIndex == 0 || cboNam.Text == "Tất cả")
            {
                sqlChartCP = "SELECT YEAR(dn.ngay_nhap) AS ThoiGian, SUM(ct.so_luong * ct.gia_nhap) AS ChiPhi " +
                             "FROM tblPhieuNhap dn JOIN tblChiTietNhap ct ON dn.ma_phieu_nhap = ct.ma_phieu_nhap " +
                             "JOIN tblSach s ON ct.ma_sach = s.ma_sach " + dieuKien +
                             " GROUP BY YEAR(dn.ngay_nhap) ORDER BY YEAR(dn.ngay_nhap) ASC";
            }
            else if (cboThang.SelectedIndex == 0 || cboThang.Text == "Tất cả")
            {
                sqlChartCP = "SELECT MONTH(dn.ngay_nhap) AS ThoiGian, SUM(ct.so_luong * ct.gia_nhap) AS ChiPhi " +
                             "FROM tblPhieuNhap dn JOIN tblChiTietNhap ct ON dn.ma_phieu_nhap = ct.ma_phieu_nhap " +
                             "JOIN tblSach s ON ct.ma_sach = s.ma_sach " + dieuKien +
                             " GROUP BY MONTH(dn.ngay_nhap) ORDER BY MONTH(dn.ngay_nhap) ASC";
            }
            else
            {
                sqlChartCP = "SELECT DAY(dn.ngay_nhap) AS ThoiGian, SUM(ct.so_luong * ct.gia_nhap) AS ChiPhi " +
                             "FROM tblPhieuNhap dn JOIN tblChiTietNhap ct ON dn.ma_phieu_nhap = ct.ma_phieu_nhap " +
                             "JOIN tblSach s ON ct.ma_sach = s.ma_sach " + dieuKien +
                             " GROUP BY DAY(dn.ngay_nhap) ORDER BY DAY(dn.ngay_nhap) ASC";
            }

            try
            {
                DataTable dtChartCP = DAO.LoadDataToTable(sqlChartCP);
                chartChiPhiNhap.Series.Clear();
                chartChiPhiNhap.Titles.Clear();
                System.Windows.Forms.DataVisualization.Charting.Series seriesCP = chartChiPhiNhap.Series.Add("Chi phí");

                if (dtChartCP != null && dtChartCP.Rows.Count == 1)
                {
                    // Nếu chỉ có 1 mốc thời gian duy nhất, chuyển sang Biểu đồ Cột để tránh bị ẩn hình
                    seriesCP.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    seriesCP.Color = System.Drawing.Color.DodgerBlue;
                    seriesCP.IsValueShownAsLabel = true; // Hiện số tiền trên đầu cột cho rõ ràng
                    seriesCP.LabelFormat = "#,##0 đ";

                    // Thêm một dòng thông báo nhỏ phía trên biểu đồ
                    chartChiPhiNhap.Titles.Add("Dữ liệu phát sinh trong 1 mốc duy nhất (Hiển thị dạng cột)");
                    chartChiPhiNhap.Titles[0].ForeColor = System.Drawing.Color.DarkOrange;
                    chartChiPhiNhap.Titles[0].Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Italic);
                }
                else
                {
                    // Nếu từ 2 mốc thời gian trở lên, quay về vẽ Biểu đồ Vùng (Area) như cũ
                    seriesCP.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
                    seriesCP.BorderColor = System.Drawing.Color.DodgerBlue;
                    seriesCP.Color = System.Drawing.Color.FromArgb(100, System.Drawing.Color.DodgerBlue);
                }
                // ------------------------------------------------

                seriesCP.XValueMember = "ThoiGian";
                seriesCP.YValueMembers = "ChiPhi";

                chartChiPhiNhap.ChartAreas[0].AxisX.Interval = 1; // Đảm bảo hiện rõ nấc mốc thời gian
                chartChiPhiNhap.DataSource = dtChartCP;
                chartChiPhiNhap.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết xuất biểu đồ chi phí nhập: " + ex.Message, "Lỗi đồ thị", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // =======================================================
            // E. BIỂU ĐỒ SỐ LƯỢNG SÁCH NHẬP KHO (BAR CHART - THANH NGANG)
            // =======================================================
            string sqlChartNhap = @"
             SELECT TOP 5 
            s.ten_sach AS TenSach, 
            ISNULL(SUM(ct.so_luong), 0) AS SLNhap
            FROM tblPhieuNhap dn
            JOIN tblChiTietNhap ct ON dn.ma_phieu_nhap = ct.ma_phieu_nhap
            JOIN tblSach s ON ct.ma_sach = s.ma_sach "
             + dieuKien + @"
             GROUP BY s.ten_sach
             ORDER BY SLNhap DESC";

            try
            {
                DataTable dtChartNhap = DAO.LoadDataToTable(sqlChartNhap);
                chartNhapKho.Series.Clear();
                chartNhapKho.Titles.Clear();

                System.Windows.Forms.DataVisualization.Charting.Series seriesNhap = chartNhapKho.Series.Add("Số lượng");
                seriesNhap.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar; 
                seriesNhap.XValueMember = "TenSach";
                seriesNhap.YValueMembers = "SLNhap";

                seriesNhap.Color = System.Drawing.Color.CadetBlue;
                seriesNhap.IsValueShownAsLabel = true; // Hiện chỉ số giá trị số lượng ở đầu thanh cột
                seriesNhap.LabelFormat = "#,##0";
                seriesNhap["PointWidth"] = "0.6";

                chartNhapKho.ChartAreas[0].AxisX.Interval = 1; // Đảm bảo hiện rõ tên
                chartNhapKho.ChartAreas[0].AxisX.IsReversed = true; //  để sản phẩm số lượng lớn nhất nằm trên cùng
                chartNhapKho.ChartAreas[0].BackColor = System.Drawing.Color.White; // Xóa nền tối
                chartNhapKho.DataSource = dtChartNhap;
                chartNhapKho.DataBind(); // Thêm lệnh kích hoạt hiển thị dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết xuất biểu đồ số lượng nhập kho: " + ex.Message, "Lỗi đồ thị", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //  Tự động tính toán ngày cuối tháng nếu chọn "Tất cả" cho ngày/tháng
            // =======================================================
            int ngay = DateTime.Today.Day;
            int thang = DateTime.Today.Month;
            int nam = DateTime.Today.Year;

            // Xử lý Năm
            if (cboNam.Text != "Tất cả")
            {
                // Phòng trường hợp người dùng gõ bậy ký tự chữ thay vì gõ số
                if (!int.TryParse(cboNam.Text, out nam))
                {
                    MessageBox.Show("Năm nhập vào phải là một số nguyên hợp lệ!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return DateTime.MinValue;
                }
            }
            else
            {
                nam = DateTime.Today.Year;
            }

            if (cboThang.Text != "Tất cả")
            {
                if (!int.TryParse(cboThang.Text, out thang) || thang < 1 || thang > 12)
                {
                    MessageBox.Show("Tháng nhập vào phải là số từ 1 đến 12!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return DateTime.MinValue;
                }
            }
            else
            {
                thang = (nam == DateTime.Today.Year) ? DateTime.Today.Month : 12;
            }

            if (cboNgay.Text != "Tất cả")
            {
                if (!int.TryParse(cboNgay.Text, out ngay) || ngay < 1 || ngay > 31)
                {
                    MessageBox.Show("Ngày nhập vào phải là số từ 1 đến 31!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return DateTime.MinValue;
                }
            }
            else
            {
                if (nam == DateTime.Today.Year && thang == DateTime.Today.Month)
                {
                    ngay = DateTime.Today.Day; // Nếu trùng tháng/năm hiện tại -> lấy hôm nay
                }
                else
                {
                    ngay = DateTime.DaysInMonth(nam, thang); // Ngược lại lấy ngày cuối tháng
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
        private void CapNhatTongSoSachTonKho()
        {
            DateTime ngayChon;

            if (cboNam.Text == "Tất cả" || string.IsNullOrEmpty(cboNam.Text.Trim()))
            {
                // Ép mốc thời gian về ngày hôm nay để tính tồn kho hiện tại
                ngayChon = DateTime.Today;
            }
            else
            {
                //  năm cụ thể
                ngayChon = LayNgayDuocChon();
                if (ngayChon == DateTime.MinValue) return;

                   // Tất cả ngày/tháng của năm hiện tại -> Tính đến hôm nay
                  if (cboNam.Text == DateTime.Today.Year.ToString() &&
                   (cboThang.SelectedIndex <= 0 || cboThang.Text == "Tất cả") &&
                   (cboNgay.SelectedIndex <= 0 || cboNgay.Text == "Tất cả"))
                   {
                    ngayChon = DateTime.Today;
                   }

                   //  chọn ngày ở tương lai (chỉ check khi chọn năm cụ thể)
                   if (ngayChon.Date > DateTime.Today)
                   {
                     MessageBox.Show("Ngày được chọn là ngày ở tương lai! Vui lòng chọn lại ngày hiện tại hoặc quá khứ.",
                                    "Thông báo ngày không hợp lệ",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                   }
            }

            // Định dạng ngày 
            string strNgayChon = ngayChon.ToString("yyyy-MM-dd 23:59:59");

            // Xử lý chuỗi tên sách tránh lỗi dấu nháy đơn
            string tenSachXuly = txtTenSach.Text.Trim().Replace("'", "''");

            // ====================================================================
            // TRƯỜNG HỢP 1: NẾU Ô TÌM KIẾM TÊN SÁCH BỊ TRỐNG -> XÓA CHỮ LABEL NHƯ CŨ
            // ====================================================================
            if (string.IsNullOrEmpty(tenSachXuly))
            {
                lblTongSoSachTonKho.Text = "";
                string dieuKienDanhMucChoChart = "";
                if (cboDanhMuc.SelectedIndex > 0 && cboDanhMuc.SelectedValue != null)
                {
                    dieuKienDanhMucChoChart = " AND dm.ma_danh_muc = '" + cboDanhMuc.SelectedValue.ToString() + "'";
                }
                CapNhatBieuDoTonKho(strNgayChon, dieuKienDanhMucChoChart, ngayChon);
                return;
            }

            // ====================================================================
            // TRƯỜNG HỢP 2: CÓ NHẬP TÊN SÁCH -> TÍNH TOÁN THEO MỐC NGÀY CHỌN (LABEL Tổng tồn kho)
            // ====================================================================
            string dieuKienTenSachNhap = " JOIN tblSach s_nhap ON ctn.ma_sach = s_nhap.ma_sach " +
                                                " WHERE s_nhap.ten_sach COLLATE Latin1_General_CI_AI LIKE N'%" + tenSachXuly + "%' AND ";

            string dieuKienTenSachXuat = " JOIN tblSach s_xuat ON ctx.ma_sach = s_xuat.ma_sach " +
                                                " WHERE s_xuat.ten_sach COLLATE Latin1_General_CI_AI LIKE N'%" + tenSachXuly + "%' AND ";

            // Câu lệnh SQL tính tổng tồn kho động theo sách được nhập
            string sqlGetTonKho = @"
        SELECT 
            (ISNULL((SELECT SUM(ctn.so_luong) 
                     FROM tblChiTietNhap ctn 
                     JOIN tblPhieuNhap pn ON ctn.ma_phieu_nhap = pn.ma_phieu_nhap "
                             + dieuKienTenSachNhap + @" pn.ngay_nhap <= '" + strNgayChon + @"'), 0)
             - 
             ISNULL((SELECT SUM(ctx.so_luong) 
                     FROM tblChiTietdonhang ctx 
                     JOIN tblDonhang px ON ctx.ma_don_hang = px.ma_don_hang "
                             + dieuKienTenSachXuat + @" px.ngay_dat <= '" + strNgayChon + @"'), 0)) AS TongTon";

            try
            {
                // Lấy giá trị tổng tồn từ Database
                string dynamicTonValue = DAO.getFieldValue(sqlGetTonKho);
                double tongTon = string.IsNullOrEmpty(dynamicTonValue) ? 0 : Convert.ToDouble(dynamicTonValue);

                // Đổi chữ hiển thị trên Label
                lblTongSoSachTonKho.Text = $"Tổng tồn kho của sách '{txtTenSach.Text.Trim()}': " + tongTon.ToString("N0");

                // Đồng bộ vẽ biểu đồ danh mục đi kèm
                string dieuKienDanhMucChoChart = "";
                if (cboDanhMuc.SelectedIndex > 0 && cboDanhMuc.SelectedValue != null)
                {
                    dieuKienDanhMucChoChart = " AND dm.ma_danh_muc = '" + cboDanhMuc.SelectedValue.ToString() + "'";
                }
                CapNhatBieuDoTonKho(strNgayChon, dieuKienDanhMucChoChart, ngayChon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính toán số liệu tồn kho động theo sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void CapNhatBieuDoTonKho(string strNgayChon, string dieuKienDanhMucChoChart, DateTime ngayHienThi)
        {
            
            string sqlChartTon = @"
                SELECT 
                    dm.ten_danh_muc AS TenDanhMuc,
                    (ISNULL((SELECT SUM(ctn.so_luong) 
                             FROM tblChiTietNhap ctn 
                             JOIN tblPhieuNhap pn ON ctn.ma_phieu_nhap = pn.ma_phieu_nhap 
                             JOIN tblSach s2 ON ctn.ma_sach = s2.ma_sach
                             WHERE s2.ma_danh_muc = dm.ma_danh_muc AND pn.ngay_nhap <= '" + strNgayChon + @"'), 0)
                     - 
                     ISNULL((SELECT SUM(ctx.so_luong) 
                             FROM tblChiTietdonhang ctx 
                             JOIN tblDonhang px ON ctx.ma_don_hang = px.ma_don_hang 
                             JOIN tblSach s3 ON ctx.ma_sach = s3.ma_sach
                             WHERE s3.ma_danh_muc = dm.ma_danh_muc AND px.ngay_dat <= '" + strNgayChon + @"'), 0)) AS SLTon
                FROM tblDanhMuc dm
                WHERE 1=1 " + dieuKienDanhMucChoChart + " ORDER BY SLTon DESC";

            try
            {
                DataTable dtChartTon = DAO.LoadDataToTable(sqlChartTon);
                chartTonKho.Series.Clear();
                chartTonKho.Titles.Clear(); 

                // === Tiêu đề hiển thị rõ thông tin ngày tồn kho ===
                string tieuDeChart = "Sách tồn kho tính đến ngày: " + ngayHienThi.ToString("dd/MM/yyyy");
                chartTonKho.Titles.Add(tieuDeChart);
                chartTonKho.Titles[0].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

                System.Windows.Forms.DataVisualization.Charting.Series seriesTon = chartTonKho.Series.Add("Lượng tồn");
                seriesTon.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                seriesTon.XValueMember = "TenDanhMuc";
                seriesTon.YValueMembers = "SLTon";

                seriesTon.Color = System.Drawing.Color.Orange;
                seriesTon.IsValueShownAsLabel = true;
                seriesTon.LabelFormat = "#,##0";

                chartTonKho.ChartAreas[0].AxisX.Interval = 1;
                chartTonKho.DataSource = dtChartTon;
                chartTonKho.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết xuất biểu đồ tồn kho động: " + ex.Message, "Lỗi đồ thị", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TaiDuLieuBaoCao();
            CapNhatTongSoSachTonKho();
            
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra điều kiện dữ liệu trên lưới
            if (dgvNhapHang.Rows.Count == 0 || dgvNhapHang.DataSource == null)
            {
                MessageBox.Show("Không tìm thấy dữ liệu hợp lệ trên lưới để kết xuất tệp Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đăng ký License cấu hình cho thư viện EPPlus 
            ExcelPackage.License.SetNonCommercialPersonal("LeBichThuy");

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Title = "Chọn thư mục lưu trữ file báo cáo công việc nhập hàng";
            saveFileDialog.FileName = "BaoCaoNhapHang_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (OfficeOpenXml.ExcelPackage excelPackage = new OfficeOpenXml.ExcelPackage())
                    {
                        OfficeOpenXml.ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Báo Cáo Nhập Hàng");

                        // --- PHẦN 1: KẾT XUẤT CÁC CỘT TIÊU ĐỀ ---
                        int colCount = dgvNhapHang.Columns.Count;
                        for (int i = 0; i < colCount; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = dgvNhapHang.Columns[i].HeaderText;
                            worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                            worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        }

                        // --- PHẦN 2: ĐỔ DỮ LIỆU CHI TIẾT TỪ DATATABLE ---
                        DataTable dt = (DataTable)dgvNhapHang.DataSource;
                        for (int row = 0; row < dt.Rows.Count; row++)
                        {
                            for (int col = 0; col < colCount; col++)
                            {
                                var cellValue = dt.Rows[row][col];
                                if (cellValue == DBNull.Value) continue;

                                string headerText = dgvNhapHang.Columns[col].HeaderText;

                                if (headerText == "Ngày Nhập" || headerText == "Ngày Đặt")
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = Convert.ToDateTime(cellValue);
                                    worksheet.Cells[row + 2, col + 1].Style.Numberformat.Format = "dd/MM/yyyy";
                                }
                                else if (headerText == "Giá Nhập" || headerText == "Thành Tiền" || headerText == "Giá Bán")
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

                        // Tự động tối ưu căn chỉnh bề rộng các hàng cột (Tránh lỗi tràn ô hoặc trống dữ liệu)
                        if (worksheet.Dimension != null)
                        {
                            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                        }

                        // --- PHẦN 3: ĐÓNG GÓI VÀ LƯU FILE ---
                        FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                        excelPackage.SaveAs(fileInfo);
                    }

                    MessageBox.Show("Kết xuất tệp báo cáo Excel thành công!\nĐường dẫn lưu trữ: " + saveFileDialog.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã phát sinh sự cố hệ thống trong quá trình xuất Excel: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            cboNgay.SelectedIndex = 0;
            cboThang.SelectedIndex = 0;
            cboNam.SelectedIndex = 0;
            cboDanhMuc.SelectedIndex = 0;
            txtTenSach.Clear();

            TaiDuLieuBaoCao(); // Trở về cấu hình kết xuất mặc định ban đầu
            CapNhatTongSoSachTonKho();
        }

        private void frmBCNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DAO.Close();
        }
    }

}
