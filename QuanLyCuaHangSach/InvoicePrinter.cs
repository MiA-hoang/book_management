using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace QuanLyCuaHangSach
{
    public class InvoicePrinter
    {
        public string Title { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string PartyLabel { get; set; }
        public string PartyName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string EmployeeName { get; set; }
        public DataGridView Data { get; set; }
        public string TotalText { get; set; }

        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fontTitle = new Font("Arial", 18, FontStyle.Bold);
            Font fontHeader = new Font("Arial", 12, FontStyle.Bold);
            Font fontNormal = new Font("Arial", 12);
            Font fontTableBold = new Font("Arial", 11, FontStyle.Bold);
            Font fontTable = new Font("Arial", 11);

            float margin = 50;
            float currentY = 50;
            float pageWidth = e.PageBounds.Width - (margin * 2);

            // Vẽ Tiêu đề căn giữa
            SizeF titleSize = g.MeasureString(Title, fontTitle);
            g.DrawString(Title, fontTitle, Brushes.Black, (e.PageBounds.Width - titleSize.Width) / 2, currentY);
            currentY += 60;

            // Thông tin chung
            g.DrawString($"Mã hóa đơn: {InvoiceNumber}", fontNormal, Brushes.Black, margin, currentY);
            currentY += 25;
            g.DrawString($"Ngày lập: {InvoiceDate:dd/MM/yyyy}", fontNormal, Brushes.Black, margin, currentY);
            currentY += 25;
            g.DrawString($"{PartyLabel}: {PartyName}", fontHeader, Brushes.Black, margin, currentY);
            currentY += 25;
            g.DrawString($"Số điện thoại: {CustomerPhone}", fontNormal, Brushes.Black, margin, currentY);
            currentY += 25;
            g.DrawString($"Địa chỉ: {CustomerAddress}", fontNormal, Brushes.Black, margin, currentY);
            currentY += 25;
            g.DrawString($"Nhân viên lập: {EmployeeName}", fontNormal, Brushes.Black, margin, currentY);
            currentY += 50;

            // Định nghĩa các cột (Tổng cộng khoảng 700-750 đơn vị để vừa A4)
            float colTen = margin;         // Cột Tên Sách (Rộng 300)
            float colSL = margin + 300;    // Cột SL
            float colGia = margin + 370;   // Cột Đơn Giá
            float colTien = margin + 500;  // Cột Thành Tiền

            // Vẽ tiêu đề bảng
            g.DrawString("Tên Sách", fontTableBold, Brushes.Black, colTen, currentY);
            g.DrawString("SL", fontTableBold, Brushes.Black, colSL, currentY);
            g.DrawString("Đơn Giá", fontTableBold, Brushes.Black, colGia, currentY);
            g.DrawString("Thành Tiền", fontTableBold, Brushes.Black, colTien, currentY);

            currentY += 25;
            g.DrawLine(Pens.Black, margin, currentY, e.PageBounds.Width - margin, currentY);
            currentY += 10;

            // Vẽ các dòng dữ liệu
            StringFormat formatRight = new StringFormat() { Alignment = StringAlignment.Far };

            foreach (DataGridViewRow row in Data.Rows)
            {
                if (row.IsNewRow) continue;

                // Cập nhật Index cột khớp với Grid: STT(0), Mã(1), Tên(2), SL(3), Giá(4), Tiền(5)
                string ten = row.Cells[2].Value?.ToString() ?? "";
                string sl = row.Cells[3].Value?.ToString() ?? "0";
                double gia = Convert.ToDouble(row.Cells[4].Value ?? 0);
                double tien = Convert.ToDouble(row.Cells[5].Value ?? 0);

                // Cột tên sách sử dụng RectangleF để tự động xuống dòng nếu quá dài (tránh tràn sang phải)
                RectangleF rectTen = new RectangleF(colTen, currentY, 300, 40);
                g.DrawString(ten, fontTable, Brushes.Black, rectTen);

                // Các cột số sử dụng formatRight để căn lề phải cho đẹp
                g.DrawString(sl, fontTable, Brushes.Black, colSL + 20, currentY, formatRight);
                g.DrawString(gia.ToString("N0"), fontTable, Brushes.Black, colGia + 100, currentY, formatRight);
                g.DrawString(tien.ToString("N0"), fontTable, Brushes.Black, colTien + 130, currentY, formatRight);

                currentY += 40;
                if (currentY > e.PageBounds.Height - 150) break;
            }

            currentY += 20;

            g.DrawLine(
                Pens.Black,
                margin,
                currentY,
                e.PageBounds.Width - margin,
                currentY
            );

            currentY += 25;

            string total = "0 VNĐ";

            if (!string.IsNullOrEmpty(TotalText))
            {
                total = TotalText;
            }

            // Font đậm
            Font totalFont = new Font("Arial", 13, FontStyle.Bold);

            // In label
            g.DrawString(
                "TỔNG TIỀN:",
                totalFont,
                Brushes.Black,
                margin + 380,
                currentY
            );

            // In giá trị
            g.DrawString(
                total,
                totalFont,
                Brushes.Black,
                margin + 520,
                currentY
            );
        }
    }
}