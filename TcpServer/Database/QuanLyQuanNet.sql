
CREATE DATABASE QuanLyQuanNet;
GO

USE QuanLyQuanNet;
GO

-- Bảng Users
CREATE TABLE Users (
    UserId NVARCHAR(20) PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    [Password] NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100),
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    [Role] NVARCHAR(20) CHECK ([Role] IN ('ADMIN', 'EMPLOYEE', 'CUSTOMER')),
    Active BIT DEFAULT 1
);
GO

-- Thêm dữ liệu mẫu
INSERT INTO Users (UserId, Username, [Password], [Role], Active)
VALUES
('U001', 'admin', '123', 'ADMIN', 1),
('U002', 'employee1', '123', 'EMPLOYEE', 1),
('U003', 'customer1', '123', 'CUSTOMER', 1);
GO