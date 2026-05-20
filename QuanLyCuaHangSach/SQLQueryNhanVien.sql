CREATE TABLE NHAN_VIEN
(
    ma_nhan_vien VARCHAR(10) PRIMARY KEY,

    ten_dang_nhap VARCHAR(50) NOT NULL UNIQUE,

    mat_khau VARCHAR(100) NOT NULL,

    ho_ten NVARCHAR(100) NOT NULL,

    vai_tro NVARCHAR(50) NOT NULL,
    -- Quản trị / Bán hàng / Kho

    trang_thai NVARCHAR(30) NOT NULL
    DEFAULT N'Hoạt động'
    -- Hoạt động / Khóa
)
GO

INSERT INTO NHAN_VIEN
VALUES
('NV01', 'admin', '123456',
 N'Nguyễn Văn A',
 N'Quản trị',
 N'Hoạt động'),

('NV02', 'banhang1', '123456',
 N'Trần Thị B',
 N'Bán hàng',
 N'Hoạt động'),

('NV03', 'kho1', '123456',
 N'Lê Văn C',
 N'Kho',
 N'Khóa')
GO