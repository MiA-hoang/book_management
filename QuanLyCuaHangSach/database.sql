

-- =======================================================
-- 1. BẢNG NHÂN VIÊN (tblNhanVien)
-- =======================================================
CREATE TABLE tblNhanVien (
    ma_nhan_vien INT IDENTITY(1,1) NOT NULL,
    ten_dang_nhap VARCHAR(50) NOT NULL,
    mat_khau VARCHAR(255) NOT NULL,
    ho_ten NVARCHAR(100) NOT NULL, -- Dùng NVARCHAR để gõ tiếng Việt có dấu
    vai_tro NVARCHAR(50) NOT NULL,  -- Dùng NVARCHAR để gõ tiếng Việt có dấu
    
    CONSTRAINT PK_tblNhanVien PRIMARY KEY (ma_nhan_vien),
    CONSTRAINT UQ_ten_dang_nhap UNIQUE (ten_dang_nhap)
);
GO

-- =======================================================
-- 2. BẢNG KHÁCH HÀNG (tblKhachHang)
-- =======================================================
CREATE TABLE tblKhachHang (
    ma_khach_hang INT IDENTITY(1,1) NOT NULL,
    ten_khach_hang NVARCHAR(100) NOT NULL,
    so_dien_thoai VARCHAR(15) NOT NULL,
    dia_chi NVARCHAR(255) NULL, -- Cho phép NULL theo thiết kế
    
    CONSTRAINT PK_tblKhachHang PRIMARY KEY (ma_khach_hang)
);
GO
select * from tblSach
-- =======================================================
-- 3. BẢNG NHÀ CUNG CẤP (tblNhaCungCap)
-- =======================================================
CREATE TABLE tblNhaCungCap (
    ma_nha_cung_cap INT IDENTITY(1,1) NOT NULL, -- Thêm tăng tự động để dễ quản lý
    ten_nha_cung_cap NVARCHAR(100) NOT NULL,
    so_dien_thoai VARCHAR(15) NOT NULL,
    dia_chi NVARCHAR(255) NULL,
    
    CONSTRAINT PK_tblNhaCungCap PRIMARY KEY (ma_nha_cung_cap)
);
GO

-- =======================================================
-- 4. BẢNG TÁC GIẢ (tblTacGia)
-- =======================================================
CREATE TABLE tblTacGia (
    ma_tac_gia INT IDENTITY(1,1) NOT NULL,
    ten_tac_gia NVARCHAR(100) NOT NULL,
    tieu_su NVARCHAR(MAX) NULL, -- TEXT trong SQL Server tương đương NVARCHAR(MAX) hoặc NTEXT
    
    CONSTRAINT PK_tblTacGia PRIMARY KEY (ma_tac_gia)
);
GO

-- =======================================================
-- 5. BẢNG DANH MỤC (tblDanhMuc)
-- =======================================================
CREATE TABLE tblDanhMuc (
    ma_danh_muc INT IDENTITY(1,1) NOT NULL,
    ten_danh_muc NVARCHAR(100) NOT NULL,
    mo_ta NVARCHAR(MAX) NULL,
    
    CONSTRAINT PK_tblDanhMuc PRIMARY KEY (ma_danh_muc)
);
GO

-- =======================================================
-- 6. BẢNG SÁCH (tblSach)
-- =======================================================
CREATE TABLE tblSach (
    ma_sach INT IDENTITY(1,1) NOT NULL,
    ten_sach NVARCHAR(150) NOT NULL,
    gia_ban DECIMAL(10,2) NOT NULL,
    so_luong INT NOT NULL CONSTRAINT DF_tblSach_so_luong DEFAULT 0,
    ma_danh_muc INT NOT NULL,
    ma_tac_gia INT NOT NULL,
    
    CONSTRAINT PK_tblSach PRIMARY KEY (ma_sach),
    CONSTRAINT FK_tblSach_tblDanhMuc FOREIGN KEY (ma_danh_muc) REFERENCES tblDanhMuc(ma_danh_muc),
    CONSTRAINT FK_tblSach_tblTacGia FOREIGN KEY (ma_tac_gia) REFERENCES tblTacGia(ma_tac_gia)
);
GO

-- =======================================================
-- 7. BẢNG PHIẾU NHẬP (tblPhieuNhap)
-- =======================================================
CREATE TABLE tblPhieuNhap (
    ma_phieu_nhap INT IDENTITY(1,1) NOT NULL,
    ngay_nhap DATE NOT NULL,
    tong_tien DECIMAL(12,2) NOT NULL,
    ma_nha_cung_cap INT NOT NULL,
    ma_nhan_vien INT NOT NULL,
    
    CONSTRAINT PK_tblPhieuNhap PRIMARY KEY (ma_phieu_nhap),
    CONSTRAINT FK_tblPhieuNhap_tblNhaCungCap FOREIGN KEY (ma_nha_cung_cap) REFERENCES tblNhaCungCap(ma_nha_cung_cap),
    CONSTRAINT FK_tblPhieuNhap_tblNhanVien FOREIGN KEY (ma_nhan_vien) REFERENCES tblNhanVien(ma_nhan_vien)
);
GO

-- =======================================================
-- 8. BẢNG CHI TIẾT NHẬP (tblChiTietNhap)
-- =======================================================
CREATE TABLE tblChiTietNhap (
    ma_phieu_nhap INT NOT NULL,
    ma_sach INT NOT NULL,
    so_luong INT NOT NULL,
    gia_nhap DECIMAL(10,2) NOT NULL,
    
    -- Khóa chính phức hợp gồm 2 cột theo thiết kế
    CONSTRAINT PK_tblChiTietNhap PRIMARY KEY (ma_phieu_nhap, ma_sach),
    CONSTRAINT FK_tblChiTietNhap_tblPhieuNhap FOREIGN KEY (ma_phieu_nhap) REFERENCES tblPhieuNhap(ma_phieu_nhap) ON DELETE CASCADE,
    CONSTRAINT FK_tblChiTietNhap_tblSach FOREIGN KEY (ma_sach) REFERENCES tblSach(ma_sach)
);
GO

-- =======================================================
-- 9. BẢNG ĐƠN HÀNG (tblDonHang)
-- =======================================================
CREATE TABLE tblDonHang (
    ma_don_hang INT IDENTITY(1,1) NOT NULL,
    ngay_dat DATE NOT NULL,
    tong_tien DECIMAL(12,2) NOT NULL,
    ma_khach_hang INT NOT NULL,
    ma_nhan_vien INT NOT NULL,
    
    CONSTRAINT PK_tblDonHang PRIMARY KEY (ma_don_hang),
    CONSTRAINT FK_tblDonHang_tblKhachHang FOREIGN KEY (ma_khach_hang) REFERENCES tblKhachHang(ma_khach_hang),
    CONSTRAINT FK_tblDonHang_tblNhanVien FOREIGN KEY (ma_nhan_vien) REFERENCES tblNhanVien(ma_nhan_vien)
);
GO

-- =======================================================
-- 10. BẢNG CHI TIẾT ĐƠN HÀNG (tblChiTietDonHang)
-- =======================================================
CREATE TABLE tblChiTietDonHang (
    ma_don_hang INT NOT NULL,
    ma_sach INT NOT NULL,
    so_luong INT NOT NULL,
    gia_ban DECIMAL(10,2) NOT NULL,
    
    -- Khóa chính phức hợp gồm 2 cột theo thiết kế
    CONSTRAINT PK_tblChiTietDonHang PRIMARY KEY (ma_don_hang, ma_sach),
    CONSTRAINT FK_tblChiTietDonHang_tblDonHang FOREIGN KEY (ma_don_hang) REFERENCES tblDonHang(ma_don_hang) ON DELETE CASCADE,
    CONSTRAINT FK_tblChiTietDonHang_tblSach FOREIGN KEY (ma_sach) REFERENCES tblSach(ma_sach)
);
GO
-- =======================================================
-- CHÈN DỮ LIỆU MẪU CHO BẢNG NHÂN VIÊN (tblNhanVien)
-- =======================================================
INSERT INTO tblNhanVien (ten_dang_nhap, mat_khau, ho_ten, vai_tro)
VALUES 
-- 1. Tài khoản Admin (Quản trị hệ thống)
('admin', '123456', N'Nguyễn Văn A', N'Admin'),
-- 2. Tài khoản Nhân viên kho (Phụ trách form frmBCNhap.cs)
('nvkho', '123456', N'Trần Thị B', N'Nhân viên kho'),

-- 3. Tài khoản Nhân viên bán hàng (Phụ trách form frmBCBanHang.cs)
('nvbanhang', '123456', N'Lê Hoàng C', N'Nhân viên bán hàng');
GO
-- Kiểm tra lại danh sách nhân viên sau khi chèn
SELECT * FROM tblNhanVien;
-- =======================================================
-- TỰ ĐỘNG SINH 500 KHÁCH HÀNG MẪU (MOCK DATA)
-- =======================================================
DECLARE @i INT = 1;
DECLARE @TenHo NVARCHAR(50);
DECLARE @TenDem NVARCHAR(50);
DECLARE @TenChinh NVARCHAR(50);
DECLARE @Quan NVARCHAR(50);
DECLARE @SDT VARCHAR(15);

-- Khai báo mảng dữ liệu mẫu để trộn ngẫu nhiên
DECLARE @DanhSachHo TABLE (ID INT IDENTITY, Ho NVARCHAR(50));
INSERT INTO @DanhSachHo VALUES (N'Nguyễn'), (N'Trần'), (N'Lê'), (N'Phạm'), (N'Hoàng'), (N'Vũ'), (N'Phan'), (N'Đặng'), (N'Bùi'), (N'Đỗ');

DECLARE @DanhSachDem TABLE (ID INT IDENTITY, Dem NVARCHAR(50));
INSERT INTO @DanhSachDem VALUES (N'Văn'), (N'Thị'), (N'Minh'), (N'Hoàng'), (N'Anh'), (N'Đức'), (N'Ngọc'), (N'Thu'), (N'Tuấn'), (N'Khánh');

DECLARE @DanhSachTen TABLE (ID INT IDENTITY, Ten NVARCHAR(50));
INSERT INTO @DanhSachTen VALUES (N'Anh'), (N'Bình'), (N'Chi'), (N'Dũng'), (N'Giang'), (N'Hương'), (N'Hùng'), (N'Linh'), (N'Nam'), (N'Sơn'), (N'Tú'), (N'Trang'), (N'Yến'), (N'Hải'), (N'Đạt');

DECLARE @DanhSachQuan TABLE (ID INT IDENTITY, Quan NVARCHAR(50));
INSERT INTO @DanhSachQuan VALUES (N'Đống Đa'), (N'Cầu Giấy'), (N'Thanh Xuân'), (N'Hai Bà Trưng'), (N'Hoàn Kiếm'), (N'Ba Đình'), (N'Hà Đông'), (N'Nam Từ Liêm');

-- Vòng lặp chạy đúng 500 lần để INSERT
WHILE @i <= 500
BEGIN
    -- Lấy ngẫu nhiên Họ, Tên Đệm, Tên Chính và Quận
    SELECT @TenHo = Ho FROM @DanhSachHo WHERE ID = FLOOR(RAND()*(10-1+1)+1);
    SELECT @TenDem = Dem FROM @DanhSachDem WHERE ID = FLOOR(RAND()*(10-1+1)+1);
    SELECT @TenChinh = Ten FROM @DanhSachTen WHERE ID = FLOOR(RAND()*(15-1+1)+1);
    SELECT @Quan = Quan FROM @DanhSachQuan WHERE ID = FLOOR(RAND()*(8-1+1)+1);
    
    -- Sinh số điện thoại ngẫu nhiên dạng 09xxxxxxxx hoặc 03xxxxxxxx
    SET @SDT = CASE WHEN RAND() > 0.5 THEN '09' ELSE '03' END 
               + CAST(FLOOR(RAND()*(99999999-10000000+1)+10000000) AS VARCHAR(10));

    -- Thực hiện chèn vào bảng khách hàng
    INSERT INTO tblKhachHang (ten_khach_hang, so_dien_thoai, dia_chi)
    VALUES (
        @TenHo + ' ' + @TenDem + ' ' + @TenChinh, 
        @SDT, 
        N'Số ' + CAST(FLOOR(RAND()*(150-1+1)+1) AS NVARCHAR(5)) + N', Quận ' + @Quan + N', Hà Nội'
    );

    SET @i = @i + 1;
END;
GO
-- =======================================================
-- KIỂM TRA SỐ LƯỢNG VÀ DỮ LIỆU ĐÃ SINH
-- =======================================================
-- Xem tổng số lượng bản ghi xem đã đủ 500 chưa
SELECT COUNT(*) AS TongSoKhachHang FROM tblKhachHang;

-- Xem thử 20 khách hàng đầu tiên được sinh ra
SELECT TOP 20 * FROM tblKhachHang;

-- =======================================================
-- CHÈN DỮ LIỆU MẪU CHO BẢNG NHÀ CUNG CẤP (tblNhaCungCap)
-- =======================================================
-- Tắt kiểm tra IDENTITY nếu bạn đã lỡ thêm IDENTITY trước đó, 
-- hoặc cứ chạy trực tiếp nếu bảng được tạo theo đúng thiết kế gốc của bạn.
SET IDENTITY_INSERT tblNhaCungCap ON;

INSERT INTO tblNhaCungCap (ma_nha_cung_cap, ten_nha_cung_cap, so_dien_thoai, dia_chi)
VALUES 
(1, N'Nhà xuất bản Trẻ', '02839316289', N'161B Lý Chính Thắng, Quận 3, TP. Hồ Chí Minh'),
(2, N'Công ty Cổ phần Sách Alpha (Alpha Books)', '02437226234', N'11A Nguyễn Hồng Huân, Quận Đống Đa, Hà Nội'),
(3, N'Nhà sách Nhã Nam', '02435147910', N'59 Đỗ Quang, Quận Cầu Giấy, Hà Nội'),
(4, N'Nhà xuất bản Kim Đồng', '02439434730', N'55 Quang Trung, Quận Hai Bà Trưng, Hà Nội'),
(5, N'Công ty Sách Bách Việt', '02462814030', N'Trần Quốc Hoàn, Quận Cầu Giấy, Hà Nội'),
(6, N'Công ty Cổ phần Văn hóa Đông A', '02437367432', N'113 Đông Các, Quận Đống Đa, Hà Nội'),
(7, N'Nhà xuất bản Giáo dục Việt Nam', '02438220801', N'81 Trần Hưng Đạo, Quận Hoàn Kiếm, Hà Nội'),
(8, N'Công ty Sách và Thiết bị Giáo dục Tràng An', '02437567890', N'Số 2 Láng Hạ, Quận Ba Đình, Hà Nội'),
(9, N'Nhà xuất bản Tổng hợp TP.HCM', '02838225340', N'86 Nguyễn Thị Minh Khai, Quận 1, TP. Hồ Chí Minh'),
(10, N'Công ty Cổ phần Sách Thái Hà (ThaiHaBooks)', '02437930480', N'119 C3 Nghĩa Tân, Quận Cầu Giấy, Hà Nội');

SET IDENTITY_INSERT tblNhaCungCap OFF;
GO

-- Kiểm tra danh sách nhà cung cấp vừa tạo
SELECT * FROM tblNhaCungCap;
-- =======================================================
-- 1. CHÈN DỮ LIỆU CHO BẢNG DANH MỤC (tblDanhMuc)
-- =======================================================
-- Sử dụng IDENTITY_INSERT nếu bạn đã chỉnh sửa bảng thành tự động tăng, 
-- hoặc chạy trực tiếp nếu giữ nguyên thiết kế gốc không tự động tăng.
SET IDENTITY_INSERT tblDanhMuc ON;

INSERT INTO tblDanhMuc (ma_danh_muc, ten_danh_muc, mo_ta)
VALUES 
(1, N'Văn học trong nước', N'Các tác phẩm tiểu thuyết, truyện ngắn, thơ ca của tác giả Việt Nam'),
(2, N'Văn học nước ngoài', N'Sách văn học dịch, tiểu thuyết kinh điển và hiện đại quốc tế'),
(3, N'Kinh tế - Kinh doanh', N'Sách quản trị, khởi nghiệp, tài chính, chứng khoán và kỹ năng làm việc'),
(4, N'Tâm lý - Kỹ năng sống', N'Sách phát triển bản thân, tư duy tích cực, tâm lý học ứng dụng'),
(5, N'Thiếu nhi', N'Truyện tranh, truyện cổ tích, sách giáo dục bán trú cho trẻ em'),
(6, N'Khoa học - Công nghệ', N'Sách tin học, lập trình, công nghệ thông tin và khoa học vũ trụ');
SET IDENTITY_INSERT tblDanhMuc OFF;
GO


-- =======================================================
-- 2. CHÈN DỮ LIỆU CHO BẢNG TÁC GIẢ (tblTacGia)
-- =======================================================
SET IDENTITY_INSERT tblTacGia ON;

INSERT INTO tblTacGia (ma_tac_gia, ten_tac_gia, tieu_su)
VALUES 
(1, N'Nguyễn Nhật Ánh', N'Nhà văn Việt Nam chuyên viết cho tuổi mới lớn với nhiều tác phẩm sinh động, đoạt giải thưởng ASEAN.'),
(2, N'Nam Cao', N'Nhà văn hiện thực xuất sắc của văn học Việt Nam thế kỷ XX với các tác phẩm kinh điển như Chí Phèo, Lão Hạc.'),
(3, N'Dale Carnegie', N'Tác giả người Mỹ, nổi tiếng toàn cầu với cuốn sách Đắc Nhân Tâm - cẩm nang giao tiếp kinh điển mọi thời đại.'),
(4, N'Haruki Murakami', N'Nhà văn người Nhật Bản nổi tiếng với phong cách hiện thực huyền ảo qua Rừng Na Uy, Biên niên ký chim vặn dây cót.'),
(5, N'J.K. Rowling', N'Nữ nhà văn người Anh, cha đẻ của loạt tiểu thuyết phù thủy đình đám thế giới Harry Potter.'),
(6, N'Tony Buổi Sáng', N'Tác giả ẩn danh người Việt, nổi tiếng với các cuốn sách truyền cảm hứng cho giới trẻ như Cà phê cùng Tony, Trên đường băng.'),
(7, N'Robert Kiyosaki', N'Doanh nhân, nhà đầu tư và tác giả bộ sách Dạy con làm giàu (Cha giàu Cha nghèo) cực kỳ ăn khách.'),
(8, N'Tô Hoài', N'Cố nhà văn Việt Nam với tác phẩm Dế Mèn Phiêu Lưu Ký gắn liền với tuổi thơ của nhiều thế hệ đọc giả.'),
(9, N'Napoleon Hill', N'Chuyên gia người Mỹ sáng lập trường phái thành công học, tác giả cuốn Nghĩ giàu và làm giàu.'),
(10, N'Stephen King', N'Ông hoàng truyện kinh dị, viễn tưởng người Mỹ với hàng loạt tác phẩm được chuyển thể thành phim điện ảnh bom tấn.'),
(11, N'Xuân Quỳnh', N'Nữ nhà thơ tình xuất sắc của văn học Việt Nam hiện đại với tác phẩm Sóng, Thuyền và Biển.'),
(12, N'Nguyễn Du', N'Đại thi hào dân tộc Việt Nam, Danh nhân văn hóa thế giới, cha đẻ của Kiệt tác Truyện Kiều.'),
(13, N'Victor Hugo', N'Nhà văn, nhà thơ, nhà viết kịch đại tài thuộc chủ nghĩa lãng mạn của Pháp với tác phẩm Những người khốn khổ.'),
(14, N'Ernest Hemingway', N'Nhà văn vĩ đại người Mỹ, đoạt giải Nobel Văn học năm 1954 với tác phẩm Ông già và biển cả.'),
(15, N'Lev Tolstoy', N'Nhà văn vĩ đại người Nga, đỉnh cao của chủ nghĩa hiện thực với các kiệt tác Chiến tranh và hòa bình, Anna Karenina.'),
(16, N'Paulo Coelho', N'Nhà văn tiểu thuyết nổi tiếng người Brazil, tác giả của cuốn sách Nhà Giả Kim bán chạy thứ hai thế giới sau Kinh Thánh.'),
(17, N'Nguyễn Ngọc Tư', N'Nữ nhà văn miền Tây Nam Bộ với lối viết mộc mạc, giàu cảm xúc, nổi tiếng với tập truyện ngắn Cánh đồng bất tận.'),
(18, N'Osho', N'Nhà huyền môn, bậc thầy tâm linh người Ấn Độ với hàng trăm cuốn sách về tư duy, thiền định và phong cách sống.'),
(19, N'Vũ Trọng Phụng', N'Ông vua phóng sự đất Bắc, nhà văn hiện thực phê phán xuất sắc với cuốn tiểu thuyết Số đỏ.'),
(20, N'Hồ Chí Minh', N'Anh hùng giải phóng dân tộc, Danh nhân văn hóa thế giới, tác giả của tập thơ Nhật ký trong tù.'),
(21, N'Arthur Conan Doyle', N'Nhà văn người Scotland nổi tiếng toàn cầu với bộ truyện trinh thám về thám tử lừng danh Sherlock Holmes.'),
(22, N'Agatha Christie', N'Nữ hoàng trinh thám người Anh với các vụ án ly kỳ của thám tử Hercule Poirot và bà Marple.'),
(23, N'Kien Nguyen', N'Tác giả người Mỹ gốc Việt nổi tiếng với tiểu thuyết The Unwanted (Người không mong đợi).'),
(24, N'Brian Tracy', N'Chuyên gia đào tạo, tác giả người Mỹ gốc Canada về chủ đề phát triển bản thân, nghệ thuật bán hàng và quản trị thời gian.'),
(25, N'Simon Sinek', N'Tác giả, diễn giả người Mỹ nổi tiếng với tư duy Start With Why (Bắt đầu với câu hỏi Tại sao).'),
(26, N'Mark Manson', N'Blogger, tác giả người Mỹ nổi tiếng với cuốn sách Nghệ thuật tinh tế của việc đếch quan tâm.'),
(27, N'Huy Cận', N'Nhà thơ xuất sắc của phong trào Thơ Mới, nổi tiếng với tập lửa thiêng và bài thơ Tràng Giang.'),
(28, N'Lỗ Tấn', N'Nhà văn hiện thực vĩ đại của văn học Trung Quốc hiện đại, tác giả của AQ chính truyện.'),
(29, N'Gao Xingjian', N'Nhà văn người Pháp gốc Hoa đoạt giải Nobel Văn học năm 2000 với tác phẩm Linh Sơn.'),
(30, N'Thomas Quy', N'Tác giả trẻ viết sách kỹ năng và công nghệ thông tin định hướng cho sinh viên tại Việt Nam.');
SET IDENTITY_INSERT tblTacGia OFF;
GO

-- =======================================================
-- TỰ ĐỘNG SINH 1500 CUỐN SÁCH MẪU (MOCK DATA)
-- =======================================================
DECLARE @i INT = 1;
DECLARE @TenSach NVARCHAR(150);
DECLARE @GiaBan DECIMAL(10,2);
DECLARE @SoLuongTon INT;
DECLARE @MaDanhMuc INT;
DECLARE @MaTacGia INT;

-- Khai báo từ khóa đặc trưng theo từng danh mục để tên sách nghe hợp lý hơn
DECLARE @TuKhoa TABLE (ID INT, CumTu NVARCHAR(50));
INSERT INTO @TuKhoa VALUES 
(1, N'Tuyển tập truyện ngắn'), (1, N'Tiểu thuyết cuộc đời'), (1, N'Ký ức'),
(2, N'Bản dịch kinh điển'), (2, N'Hành trình phương Tây'), (2, N'Phía sau'),
(3, N'Bí quyết làm giàu'), (3, N'Quản trị dòng tiền'), (3, N'Chiến lược kinh doanh'),
(4, N'Tư duy tích cực'), (4, N'Đánh thức tiềm năng'), (4, N'Nghệ thuật sống'),
(5, N'Thế giới diệu kỳ'), (5, N'Truyện cổ tích chọn lọc'), (5, N'Bài học đầu đời'),
(6, N'Cẩm nang Lập trình C#'), (6, N'Khám phá Vũ trụ'), (6, N'Dữ liệu lớn và AI');

-- Bật tính năng chèn giá trị trực tiếp cho cột khóa chính nếu cột ma_sach có IDENTITY
SET IDENTITY_INSERT tblSach ON;

WHILE @i <= 1500
BEGIN
    -- Phân bổ tuần tự danh mục từ 1 đến 6 dựa trên biến chạy @i
    SET @MaDanhMuc = (@i % 6) + 1;
    
    -- Phân bổ tuần tự tác giả từ 1 đến 30 dựa trên biến chạy @i
    SET @MaTacGia = (@i % 30) + 1;
    
    -- Tạo giá bán ngẫu nhiên từ 50,000 đến 250,000 (làm tròn đến nghìn đồng)
    SET @GiaBan = FLOOR(RAND() * (250 - 50 + 1) + 50) * 1000;
    
    -- Tạo số lượng tồn kho ngẫu nhiên ban đầu từ 10 đến 100 cuốn
    SET @SoLuongTon = FLOOR(RAND() * (100 - 10 + 1) + 10);
    
    -- Lấy ngẫu nhiên một cụm từ đặc trưng hợp với Danh mục hiện tại
    SELECT TOP 1 @TenSach = CumTu FROM @TuKhoa WHERE ID = @MaDanhMuc ORDER BY NEWID();
    
    -- Ghép nối thành tên sách hoàn chỉnh kèm mã số để không bị trùng lặp
    SET @TenSach = @TenSach + N' - Tập ' + CAST((@i % 5) + 1 AS NVARCHAR(2)) + N' (Mã: ' + CAST(@i AS NVARCHAR(5)) + N')';

    -- Chèn dữ liệu vào bảng sách
    INSERT INTO tblSach (ma_sach, ten_sach, gia_ban, so_luong, ma_danh_muc, ma_tac_gia)
    VALUES (@i, @TenSach, @GiaBan, @SoLuongTon, @MaDanhMuc, @MaTacGia);

    SET @i = @i + 1;
END;

SET IDENTITY_INSERT tblSach OFF;
GO
-- =======================================================
-- TỰ ĐỘNG SINH 100 PHIẾU NHẬP VÀ CHI TIẾT NHẬP
-- =======================================================
DECLARE @PN_ID INT = 1;
DECLARE @NgayNhap DATE;
DECLARE @MaNCC INT;
DECLARE @MaNV INT;

-- Biến trung gian phục vụ sinh chi tiết
DECLARE @SoDongChiTiet INT;
DECLARE @j INT;
DECLARE @MaSachRandom INT;
DECLARE @SoLuongNhap INT;
DECLARE @GiaBanSach DECIMAL(10,2);
DECLARE @GiaNhapSach DECIMAL(10,2);
DECLARE @TongTienPhieuNhap DECIMAL(12,2);

-- Bật IDENTITY_INSERT cho bảng phiếu nhập chính
SET IDENTITY_INSERT tblPhieuNhap ON;

WHILE @PN_ID <= 100
BEGIN
    -- 1. Sinh dữ liệu cho bảng cha (tblPhieuNhap)
    -- Ngay nhập ngẫu nhiên trong khoảng năm 2024 - 2026
    SET @NgayNhap = DATEADD(DAY, FLOOR(RAND() * (365 * 2)), '2024-01-01');
    -- Mã NCC từ 1 đến 10
    SET @MaNCC = FLOOR(RAND() * (10 - 1 + 1) + 1);
    -- Mã nhân viên kho (ID = 2 theo dữ liệu trước đó)
    SET @MaNV = 2; 
    
    -- Chèn tạm phiếu nhập với tổng tiền = 0 (sẽ cập nhật lại sau khi tính tổng chi tiết)
    INSERT INTO tblPhieuNhap (ma_phieu_nhap, ngay_nhap, tong_tien, ma_nha_cung_cap, ma_nhan_vien)
    VALUES (@PN_ID, @NgayNhap, 0, @MaNCC, @MaNV);

    -- 2. Sinh dữ liệu cho bảng con (tblChiTietNhap)
    -- Mỗi phiếu nhập có từ 2 đến 5 đầu sách khác nhau
    SET @SoDongChiTiet = FLOOR(RAND() * (5 - 2 + 1) + 2);
    SET @j = 1;
    SET @TongTienPhieuNhap = 0;

    WHILE @j <= @SoDongChiTiet
    BEGIN
        -- Bốc ngẫu nhiên mã sách từ 1 đến 1500
        SET @MaSachRandom = FLOOR(RAND() * (1500 - 1 + 1) + 1);
        -- Số lượng nhập từ 20 đến 100 cuốn
        SET @SoLuongNhap = FLOOR(RAND() * (100 - 20 + 1) + 20);
        
        -- Lấy giá bán từ bảng sách để tính giá nhập (Giá nhập = 70% giá bán)
        SELECT @GiaBanSach = gia_ban FROM tblSach WHERE ma_sach = @MaSachRandom;
        SET @GiaNhapSach = @GiaBanSach * 0.7;

        -- Kiểm tra xem sách này đã tồn tại trong phiếu nhập này chưa (tránh trùng Khóa chính hợp phần)
        IF NOT EXISTS (SELECT 1 FROM tblChiTietNhap WHERE ma_phieu_nhap = @PN_ID AND ma_sach = @MaSachRandom)
        BEGIN
            INSERT INTO tblChiTietNhap (ma_phieu_nhap, ma_sach, so_luong, gia_nhap)
            VALUES (@PN_ID, @MaSachRandom, @SoLuongNhap, @GiaNhapSach);
            
            -- Cộng dồn vào tổng tiền phiếu
            SET @TongTienPhieuNhap = @TongTienPhieuNhap + (@SoLuongNhap * @GiaNhapSach);
            SET @j = @j + 1;
        END
    END

    -- 3. Cập nhật lại tổng tiền chuẩn xác cho bảng cha
    UPDATE tblPhieuNhap 
    SET tong_tien = @TongTienPhieuNhap 
    WHERE ma_phieu_nhap = @PN_ID;

    SET @PN_ID = @PN_ID + 1;
END;

SET IDENTITY_INSERT tblPhieuNhap OFF;
GO

-- Kiểm tra kết quả phần Nhập kho
SELECT COUNT(*) AS TongSoPhieuNhap FROM tblPhieuNhap;
SELECT COUNT(*) AS TongChiTietNhap FROM tblChiTietNhap;
-- =======================================================
-- TỰ ĐỘNG SINH 3500 ĐƠN HÀNG VÀ CHI TIẾT ĐƠN HÀNG
-- =======================================================
DECLARE @DH_ID INT = 1;
DECLARE @NgayDat DATE;
DECLARE @MaKH INT;
DECLARE @MaNVBan INT;

-- Biến trung gian phục vụ sinh chi tiết đơn hàng
DECLARE @SoDongChiTietDH INT;
DECLARE @k INT;
DECLARE @MaSachCon INT;
DECLARE @SoLuongBan INT;
DECLARE @GiaBanThucTe DECIMAL(10,2);
DECLARE @TongTienDonHang DECIMAL(12,2);

SET IDENTITY_INSERT tblDonHang ON;

WHILE @DH_ID <= 3500
BEGIN
    -- 1. Sinh dữ liệu cho bảng cha (tblDonHang)
    -- Phân bổ ngày đặt từ năm 2024 đến năm 2026 để đồ thị hình đường (Line Chart) kéo dài mượt mà
    SET @NgayDat = DATEADD(DAY, FLOOR(RAND() * (365 * 2.5)), '2024-01-01');
    -- Khách hàng ngẫu nhiên từ 1 đến 500
    SET @MaKH = FLOOR(RAND() * (500 - 1 + 1) + 1);
    -- Nhân viên bán hàng (ID = 3)
    SET @MaNVBan = 3;

    -- Chèn tạm đơn hàng với tổng tiền = 0
    INSERT INTO tblDonHang (ma_don_hang, ngay_dat, tong_tien, ma_khach_hang, ma_nhan_vien)
    VALUES (@DH_ID, @NgayDat, 0, @MaKH, @MaNVBan);

    -- 2. Sinh dữ liệu cho bảng con (tblChiTietDonHang)
    -- Mỗi khách mua từ 1 đến 4 đầu sách khác nhau trên một đơn hàng
    SET @SoDongChiTietDH = FLOOR(RAND() * (4 - 1 + 1) + 1);
    SET @k = 1;
    SET @TongTienDonHang = 0;

    WHILE @k <= @SoDongChiTietDH
    BEGIN
        SET @MaSachCon = FLOOR(RAND() * (1500 - 1 + 1) + 1);
        -- Khách mua lẻ từ 1 đến 5 cuốn
        SET @SoLuongBan = FLOOR(RAND() * (5 - 1 + 1) + 1);

        -- Lấy giá bán hiện hành của cuốn sách
        SELECT @GiaBanThucTe = gia_ban FROM tblSach WHERE ma_sach = @MaSachCon;

        -- Tránh trùng lặp mã sách trong cùng 1 đơn hàng
        IF NOT EXISTS (SELECT 1 FROM tblChiTietDonHang WHERE ma_don_hang = @DH_ID AND ma_sach = @MaSachCon)
        BEGIN
            INSERT INTO tblChiTietDonHang (ma_don_hang, ma_sach, so_luong, gia_ban)
            VALUES (@DH_ID, @MaSachCon, @SoLuongBan, @GiaBanThucTe);

            SET @TongTienDonHang = @TongTienDonHang + (@SoLuongBan * @GiaBanThucTe);
            SET @k = @k + 1;
        END
    END

    -- 3. Cập nhật lại tổng tiền chuẩn xác cho đơn hàng
    UPDATE tblDonHang 
    SET tong_tien = @TongTienDonHang 
    WHERE ma_don_hang = @DH_ID;

    SET @DH_ID = @DH_ID + 1;
END;

SET IDENTITY_INSERT tblDonHang OFF;
GO
-- =======================================================
-- PHẦN 1: THỐNG KÊ TỔNG SỐ BẢN GHI TRÊN TẤT CẢ CÁC BẢNG
-- =======================================================
SELECT 'tblNhanVien' AS [Tên Bảng], COUNT(*) AS [Số Lượng Bản Ghi] FROM tblNhanVien
UNION ALL
SELECT 'tblKhachHang', COUNT(*) FROM tblKhachHang
UNION ALL
SELECT 'tblNhaCungCap', COUNT(*) FROM tblNhaCungCap
UNION ALL
SELECT 'tblTacGia', COUNT(*) FROM tblTacGia
UNION ALL
SELECT 'tblDanhMuc', COUNT(*) FROM tblDanhMuc
UNION ALL
SELECT 'tblSach', COUNT(*) FROM tblSach
UNION ALL
SELECT 'tblPhieuNhap', COUNT(*) FROM tblPhieuNhap
UNION ALL
SELECT 'tblChiTietNhap', COUNT(*) FROM tblChiTietNhap
UNION ALL
SELECT 'tblDonHang', COUNT(*) FROM tblDonHang
UNION ALL
SELECT 'tblChiTietDonHang', COUNT(*) FROM tblChiTietDonHang;
GO


-- =======================================================
-- PHẦN 2: TRUY VẤN KIỂM TRA LOGIC DỮ LIỆU (MẪU NGẪU NHIÊN)
-- =======================================================

-- 1. Kiểm tra ngẫu nhiên 5 cuốn sách xem có map đúng danh mục và tác giả không
PRINT N'--- Kiểm tra bảng Sách ---';
SELECT TOP 5 s.ma_sach, s.ten_sach, s.gia_ban, s.so_luong AS [Số lượng tồn gốc], dm.ten_danh_muc, tg.ten_tac_gia
FROM tblSach s
JOIN tblDanhMuc dm ON s.ma_danh_muc = dm.ma_danh_muc
JOIN tblTacGia tg ON s.ma_tac_gia = tg.ma_tac_gia
ORDER BY NEWID(); -- Lấy ngẫu nhiên

-- 2. Kiểm tra log nhập kho: Thông tin phiếu nhập và tổng tiền chi tiết gom lại
PRINT N'--- Kiểm tra Đóng gói Phiếu Nhập & Chi Tiết Nhập ---';
SELECT TOP 3 p.ma_phieu_nhap, p.ngay_nhap, p.tong_tien AS [Tổng tiền trên phiếu],
             COUNT(ct.ma_sach) AS [Số đầu sách nhập], 
             SUM(ct.so_luong) AS [Tổng số cuốn đã nhập]
FROM tblPhieuNhap p
JOIN tblChiTietNhap ct ON p.ma_phieu_nhap = ct.ma_phieu_nhap
GROUP BY p.ma_phieu_nhap, p.ngay_nhap, p.tong_tien
ORDER BY p.ma_phieu_nhap ASC;

-- 3. Kiểm tra log bán hàng: Đơn hàng mua lẻ của khách hàng
PRINT N'--- Kiểm tra Đóng gói Đơn Hàng & Chi Tiết Đơn ---';
SELECT TOP 3 dh.ma_don_hang, dh.ngay_dat, kh.ten_khach_hang, nv.ho_ten AS [Nhân viên bán],
             dh.tong_tien AS [Tổng tiền hóa đơn],
             COUNT(ct.ma_sach) AS [Số mặt hàng mua]
FROM tblDonHang dh
JOIN tblKhachHang kh ON dh.ma_khach_hang = kh.ma_khach_hang
JOIN tblNhanVien nv ON dh.ma_nhan_vien = nv.ma_nhan_vien
JOIN tblChiTietDonHang ct ON dh.ma_don_hang = ct.ma_don_hang
GROUP BY dh.ma_don_hang, dh.ngay_dat, kh.ten_khach_hang, nv.ho_ten, dh.tong_tien
ORDER BY dh.ma_don_hang DESC;
GO

