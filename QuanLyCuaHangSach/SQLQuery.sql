CREATE TABLE KHACH_HANG
(
    ma_khach_hang   VARCHAR(10) PRIMARY KEY,
    ten_khach_hang  VARCHAR(100) NOT NULL,
    so_dien_thoai   VARCHAR(15) NOT NULL,
    dia_chi         VARCHAR(255)
)
GO

INSERT INTO KHACH_HANG
(ma_khach_hang, ten_khach_hang, so_dien_thoai, dia_chi)
VALUES
('KH01', 'Le Minh Khang', '0912345678', 'Ha Noi'),
('KH02', 'Pham Thi Lan', '0988777666', 'Da Nang'),
('KH03', 'Nguyen Hoang Nam', '0909988776', 'TP HCM'),
('KH04', 'Tran Thi Mai', '0911223344', 'Can Tho'),
('KH05', 'Vo Quoc Bao', '0933555777', 'Dong Nai'),
('KH06', 'Huynh Gia Huy', '0977665544', 'Binh Duong'),
('KH07', 'Le Thi Ngoc', '0988123123', 'Hue'),
('KH08', 'Pham Minh Quan', '0922334455', 'Da Lat'),
('KH09', 'Doan Thanh Tung', '0966777888', 'Hai Phong'),
('KH10', 'Nguyen Thi Yen', '0944556677', 'Vung Tau')
GO

﻿CREATE TABLE NHAN_VIEN
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