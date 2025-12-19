USE master;
GO

-- Xóa database nếu đã tồn tại để tạo mới
DROP DATABASE QuanLyQuanNet;

-- Tạo database
CREATE DATABASE QuanLyQuanNet;
GO

-- Sử dụng database vừa tạo
USE QuanLyQuanNet;
GO

SELECT *
FROM Sessions
SELECT TOP 1 SessionId FROM Sessions WHERE CustomerId = 'U003' AND EndTime IS NULL
-- ==========================================================
-- BẢNG USERS (Người dùng)
-- ==========================================================
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

-- ==========================================================
-- BẢNG ADMINS (Quản trị viên - Kế thừa từ Users)
-- ==========================================================
CREATE TABLE Admins (
    AdminId NVARCHAR(20) PRIMARY KEY,    
    AdminCode NVARCHAR(20),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (AdminId) REFERENCES Users(UserId)
);

-- ==========================================================
-- BẢNG EMPLOYEES (Nhân viên - Kế thừa từ Users)
-- ==========================================================
CREATE TABLE Employees (
    EmployeeId NVARCHAR(20) PRIMARY KEY,
    EmployeeCode NVARCHAR(20),
    Gender NVARCHAR(10),
    BirthDate DATE,
    HiredDate DATE DEFAULT GETDATE(),
    WorkDays INT DEFAULT 0,
    SalaryBase DECIMAL(12,2) DEFAULT 3000000,
    SalaryMonth DECIMAL(12,2),
    FOREIGN KEY (EmployeeId) REFERENCES Users(UserId)
);

-- ==========================================================
-- BẢNG CUSTOMERS (Khách hàng - Kế thừa từ Users)
-- ==========================================================
CREATE TABLE Customers (
    CustomerId NVARCHAR(20) PRIMARY KEY,
    Balance DECIMAL(12,2) DEFAULT 0,
    RegisterDate DATETIME DEFAULT GETDATE(),
    Vip BIT DEFAULT 0,
    FOREIGN KEY (CustomerId) REFERENCES Users(UserId)
);

-- ==========================================================
-- BẢNG COMPUTERS (Máy trạm)
-- ==========================================================
CREATE TABLE Computers (
    ComputerId NVARCHAR(20) PRIMARY KEY,   
    ComputerName NVARCHAR(50),
    [Status] NVARCHAR(20) CHECK ([Status] IN ('AVAILABLE', 'IN_USE', 'MAINTENANCE')) DEFAULT 'AVAILABLE',
    PricePerHour DECIMAL(10,2)
);

-- ==========================================================
-- BẢNG SESSIONS (Phiên chơi)
-- ==========================================================
CREATE TABLE Sessions (
    SessionId NVARCHAR(20) PRIMARY KEY,
    CustomerId NVARCHAR(20) NOT NULL,
    ComputerId NVARCHAR(20) NOT NULL,
    StartTime DATETIME DEFAULT GETDATE(),
    EndTime DATETIME NULL,
    TotalCost DECIMAL(12,2) DEFAULT 0,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    FOREIGN KEY (ComputerId) REFERENCES Computers(ComputerId)
);

-- ==========================================================
-- BẢNG SERVICES (Dịch vụ)
-- ==========================================================
CREATE TABLE Services (
    ServiceId NVARCHAR(20) PRIMARY KEY,
    ServiceName NVARCHAR(100),
);

-- ==========================================================
-- BẢNG INVOICES (Hóa đơn)
-- ==========================================================
CREATE TABLE Invoices (
    InvoiceId NVARCHAR(20) PRIMARY KEY,    -- VD: HD001
    SessionId NVARCHAR(20) NULL,
    CustomerId NVARCHAR(20) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(12,2),
    FOREIGN KEY (SessionId) REFERENCES Sessions(SessionId),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

-- ==========================================================
-- BẢNG CATEGORY (Danh mục đồ ăn, thức uống)
-- ==========================================================
CREATE TABLE Category (
    CategoryId NVARCHAR(20) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);

-- ==========================================================
-- BẢNG FOODANDDRINK (Đồ ăn và thức uống)
-- ==========================================================
CREATE TABLE FoodAndDrink (
    FoodId NVARCHAR(20) PRIMARY KEY,       
    FoodName NVARCHAR(100) NOT NULL,       
    Price DECIMAL(10,2) NOT NULL,          
    CategoryId NVARCHAR(20) NOT NULL, 
    Image NVARCHAR(255) NULL,              
    Available BIT DEFAULT 1,    
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);

-- ==========================================================
-- BẢNG INVOICEDETAILS (Chi tiết hóa đơn)
-- ==========================================================
CREATE TABLE InvoiceDetails (
    InvoiceDetailId NVARCHAR(20) PRIMARY KEY,
    InvoiceId NVARCHAR(20) NOT NULL,
    FoodId NVARCHAR(20) NULL,
    ServiceId NVARCHAR(20) NULL,
    Quantity INT DEFAULT 1,
    Price DECIMAL(10,2) NOT NULL,
    Status NVARCHAR(20)
        CHECK (Status IN ('PENDING', 'PAID', 'COMPLETED', 'CANCELLED')) 
        DEFAULT 'PENDING',
    Note NVARCHAR(255) NULL,
    FOREIGN KEY (InvoiceId) REFERENCES Invoices(InvoiceId),
    FOREIGN KEY (FoodId) REFERENCES FoodAndDrink(FoodId),
    FOREIGN KEY (ServiceId) REFERENCES Services(ServiceId)
);

-- ==========================================================
-- BẢNG CHATMESSAGE (Tin nhắn)
-- ==========================================================
CREATE TABLE ChatMessage (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FromId NVARCHAR(50) NOT NULL,
    ToId NVARCHAR(50) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    IsRead BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- ==========================================================
-- BẢNG IMPORTGOODS (Nhập hàng)
-- ==========================================================
CREATE TABLE ImportGoods (
    ImportId NVARCHAR(20) PRIMARY KEY,
    ImportDate DATETIME,
    ItemName NVARCHAR(200),
    Quantity INT,
    Supplier NVARCHAR(200)
);

GO

-- ==========================================================
-- DỮ LIỆU MẪU (INSERT DATA)
-- ==========================================================

-- INSERT Users
INSERT INTO Users (UserId, Username, [Password], FullName, Phone, Email, [Role], Active)
VALUES
('U001', 'admin', '123', 'Nguyen Van A', '0372773025', 'a@gmail.com', 'ADMIN',1 ),
('U002', 'user1', '123', 'Nguyen Van B', '0352653331', 'b@gmail.com', 'EMPLOYEE', 1),
('U003', 'user3', '123', 'Nguyen Van D', '037256789', 'd@gmail.com', 'CUSTOMER', 1),
('U004', 'admin_manager', 'hashed_pass_123', 'Pham Phu Quang', '090111222', 'admin@company.com', 'ADMIN', 1);

-- INSERT Customers (Phải có User tương ứng)
INSERT INTO Customers (CustomerId, Balance, RegisterDate, Vip) VALUES
('U003', 1000.00, '2025-11-15', 1);

-- INSERT Computers
INSERT INTO Computers (ComputerId, ComputerName, [Status], PricePerHour)
VALUES
('MAY100', 'LAPTOP-D942BILL', 'AVAILABLE', 10000),
('MAY01', 'Máy 01', 'AVAILABLE', 5000),
('MAY02', 'Máy 02', 'IN_USE', 5000),
('MAY03', 'Máy 03', 'MAINTENANCE', 5000),
('MAY04', 'Máy 04', 'AVAILABLE', 6000),
('DESKTOP-KKGPUHE', 'DESKTOP-KKGPUHE', 'AVAILABLE', 10000);

-- INSERT Services
INSERT INTO Services
VALUES
('1', 'FoodDrink'),
('2', 'Nạp tiền')

-- INSERT Sessions
/*
INSERT INTO Sessions (SessionId, CustomerId, ComputerId, StartTime, EndTime, TotalCost) VALUES
('S001', 'U003', 'MAY01', '2025-11-30 14:00:00', '2025-11-30 15:30:00', 50.00), -- Đã sửa CustomerId
('S002', 'U003', 'MAY02', '2025-11-30 16:00:00', '2025-11-30 17:00:00', 30.00), -- Đã sửa CustomerId
('S003', 'U003', 'MAY01', '2025-11-30 14:30:00', '2025-11-30 16:00:00', 45.00), -- Đã sửa CustomerId
('S004', 'U003', 'MAY03', '2025-11-30 17:00:00', NULL, 0.00), -- Phiên chưa kết thúc (U003)
('S005', 'U003', 'MAY04', '2025-11-30 15:00:00', '2025-11-30 15:45:00', 25.00), -- Đã sửa CustomerId
('S006', 'U003', 'MAY01', '2025-12-01 08:30:00', '2025-12-01 10:30:00', 60.00), -- Đã sửa CustomerId
('S007', 'U003', 'MAY02', '2025-12-01 11:00:00', '2025-12-01 12:00:00', 30.00), -- Đã sửa CustomerId
('S008', 'U003', 'MAY03', '2025-12-01 14:00:00', '2025-12-01 15:15:00', 45.00), -- Đã sửa CustomerId
('S009', 'U003', 'MAY01', '2025-12-01 16:30:00', '2025-12-01 17:00:00', 15.00), -- Đã sửa CustomerId
('S010', 'U003', 'MAY02', '2025-12-02 10:00:00', '2025-12-02 11:30:00', 45.00), -- Đã sửa CustomerId
('S011', 'U003', 'MAY03', '2025-12-02 13:00:00', '2025-12-02 14:00:00', 30.00), -- Đã sửa CustomerId
('S012', 'U003', 'MAY01', '2025-12-02 15:00:00', NULL, 0.00), -- Phiên đang diễn ra (U003)
('S013', 'U003', 'MAY01', '2024-12-25 09:00:00', '2024-12-25 11:00:00', 60.00), -- Giáng Sinh 2024
('S014', 'U003', 'MAY03', '2024-12-28 14:00:00', '2024-12-28 15:45:00', 52.50),
('S015', 'U003', 'MAY02', '2024-12-29 18:30:00', '2024-12-29 20:30:00', 60.00),
('S016', 'U003', 'MAY01', '2024-12-31 22:00:00', '2025-01-01 01:00:00', 90.00), -- Phiên giao thừa
('S017', 'U003', 'MAY03', '2024-12-30 13:00:00', '2024-12-30 14:30:00', 45.00);
*/
-- INSERT Invoices (Đã sửa CustomerId)
/*INSERT INTO Invoices (InvoiceId, SessionId, CustomerId, CreatedAt, TotalAmount) VALUES
('HD001', 'S001', 'U003', '2025-11-30 15:31:00', 50.00),
('HD002', 'S002', 'U003', '2025-11-30 17:01:00', 30.00),
('HD003', 'S003', 'U003', '2025-11-30 16:01:00', 45.00),
('HD004', NULL, 'U003', '2025-11-30 18:00:00', 100.00), -- Hóa đơn không liên quan đến session cụ thể
('HD005', 'S005', 'U003', '2025-11-30 15:46:00', 25.00),
('HD006', 'S006', 'U003', '2025-12-01 10:31:00', 60.00),
('HD007', 'S007', 'U003', '2025-12-01 12:01:00', 30.00),
('HD008', 'S008', 'U003', '2025-12-01 15:16:00', 45.00),
('HD009', 'S009', 'U003', '2025-12-01 17:01:00', 15.00),
('HD010', NULL, 'U003', '2025-12-01 10:00:00', 85.00), -- Mua thẻ game/đồ ăn
('HD011', 'S010', 'U003', '2025-12-02 11:31:00', 45.00),
('HD012', 'S011', 'U003', '2025-12-02 14:01:00', 30.00),
('HD013', NULL, 'U003', '2025-12-02 15:00:00', 25.00), -- Mua đồ uống
('HD014', 'S013', 'U003', '2024-12-25 11:01:00', 60.00),
('HD015', 'S014', 'U003', '2024-12-28 15:46:00', 52.50),
('HD016', 'S015', 'U003', '2024-12-29 20:31:00', 60.00),
('HD017', 'S016', 'U003', '2025-01-01 01:01:00', 90.00),
('HD018', 'S017', 'U003', '2024-12-30 14:31:00', 45.00),
('HD019', NULL, 'U003', '2024-12-27 10:00:00', 150.00);*/


GO
insert into Category(CategoryId, CategoryName) values 
('1', 'Mì'),
('2', N'Nước'),
('3', 'Cơm')

INSERT INTO FoodAndDrink 
(FoodId, FoodName, Price, CategoryId, Image, Available)
VALUES
-- ===== MÌ =====
('1', N'Mì bò',35000, '1', 'mi_xao', 1),
('2', N'Mì gà',30000, '1', 'mi_ga', 1),

-- ===== NƯỚC =====
('3', N'Nước cam',5000, '2', 'nuoc_cam', 1),
('4', N'Nước chanh',20000, '2', 'nuoc_chanh', 1),

-- ===== CƠM =====
('5', N'Cơm tấm',40000, '3', 'com_tam', 1),
('6', N'Cơm rang',45000, '3', 'com_ran', 1)


GO
-- ==========================================================
-- VIEW (Khung nhìn)
-- Lệnh CREATE VIEW phải nằm đầu một batch sau GO
-- ==========================================================
-- Hiển thị của bảng Employees
CREATE VIEW EmployeesView AS
SELECT 
    Employees.EmployeeCode AS [Mã nhân viên],
    Users.FullName AS [Họ tên],
    Employees.Gender AS [Giới tính],
    Employees.BirthDate AS [Ngày sinh],
    Users.Phone AS [Số điện thoại],
    Employees.HiredDate AS [Ngày vào làm],
    Employees.WorkDays AS [Số ngày làm],
    Employees.SalaryBase AS [Lương cơ bản],
    Employees.SalaryMonth AS [Lương tháng]
FROM Employees
JOIN Users ON Employees.EmployeeId = Users.UserId;
GO