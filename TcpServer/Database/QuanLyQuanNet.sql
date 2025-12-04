USE master;
GO

DROP DATABASE QuanLyQuanNet;

CREATE DATABASE QuanLyQuanNet;
GO

USE QuanLyQuanNet;
GO

SELECT *
FROM Customers;

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

-- Bảng Admins

CREATE TABLE Admins (
    AdminId NVARCHAR(20) PRIMARY KEY,    
    AdminCode NVARCHAR(20),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (AdminId) REFERENCES Users(UserId)
);

-- Bảng Employees

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



INSERT INTO Users (UserId, Username, [Password], FullName, Phone, Email, [Role], Active)
VALUES
('U001', 'admin', '123', 'Nguyen Van A', '0372773025', 'a@gmail.com', 'ADMIN',1 ),
('U002', 'user1', '123', 'Nguyen Van B', '0352653331', 'b@gmail.com', 'EMPLOYEE', 1),
('U003', 'user3', '123', 'Nguyen Van D', '037256789', 'd@gmail.com', 'CUSTOMER', 1);


-- Bảng Customers (Khách hàng)

CREATE TABLE Customers (
    CustomerId NVARCHAR(20) PRIMARY KEY,   
    Balance DECIMAL(12,2) DEFAULT 0,
    RegisterDate DATETIME DEFAULT GETDATE(),
    Vip BIT DEFAULT 0,
    FOREIGN KEY (CustomerId) REFERENCES Users(UserId)
);


-- Bảng Computers (Máy trạm)

CREATE TABLE Computers (
    ComputerId NVARCHAR(20) PRIMARY KEY,   
    ComputerName NVARCHAR(50),
    [Status] NVARCHAR(20) CHECK ([Status] IN ('AVAILABLE', 'IN_USE', 'MAINTENANCE')) DEFAULT 'AVAILABLE',
    PricePerHour DECIMAL(10,2)
);


INSERT INTO Computers (ComputerId, ComputerName, [Status], IpAddress, PricePerHour)
VALUES 
('MAY01', 'Máy 01', 'AVAILABLE', '192.168.1.101', 5000),
('MAY02', 'Máy 02', 'IN_USE', '192.168.1.102', 5000),
('MAY03', 'Máy 03', 'MAINTENANCE', '192.168.1.103', 5000),
('MAY04', 'Máy 04', 'AVAILABLE', '192.168.1.104', 6000);
--INSERT INTO Computers (ComputerId, ComputerName, [Status], IpAddress, PricePerHour)
--VALUES ('MAY03', 'Máy 03', 'MAINTENANCE', '192.168.1.103', 5000);
--EXEC sp_helpconstraint 'Computers';
--ALTER TABLE Computers DROP CONSTRAINT CK_status;
--DELETE FROM Computers WHERE ComputerName = 'MAY07';
--SELECT * FROM Computers;
--ALTER TABLE Computers ADD CONSTRAINT CK_status CHECK (Status in ('AVAILABLE','MAINTENANCE', 'IN_USE'));
--ALTER TABLE Computers DROP CONSTRAINT DF_status;
--ALTER TABLE Computers ADD CONSTRAINT DF_status DEFAULT 'AVAILABLE' FOR Status;
-- Bảng Sessions (Phiên chơi)

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

-- Bảng Services (Dịch vụ)

CREATE TABLE Services (
    ServiceId NVARCHAR(20) PRIMARY KEY,
    ServiceName NVARCHAR(100),
);

-- Bảng Invoices (Hóa đơn)

CREATE TABLE Invoices (
    InvoiceId NVARCHAR(20) PRIMARY KEY,    -- VD: HD001
    SessionId NVARCHAR(20) NULL,
    CustomerId NVARCHAR(20) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(12,2),
    FOREIGN KEY (SessionId) REFERENCES Sessions(SessionId),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);
INSERT INTO Sessions (SessionId, CustomerId, ComputerId, StartTime, EndTime, TotalCost) VALUES
('S001', 'U001', 'MAY01', '2025-11-30 14:00:00', '2025-11-30 15:30:00', 50.00),
('S002', 'U001', 'MAY02', '2025-11-30 16:00:00', '2025-11-30 17:00:00', 30.00),
('S003', 'U002', 'MAY01', '2025-11-30 14:30:00', '2025-11-30 16:00:00', 45.00),
('S004', 'U003', 'MAY03', '2025-11-30 17:00:00', NULL, 0.00), -- Phiên chưa kết thúc
('S005', 'U004', 'MAY04', '2025-11-30 15:00:00', '2025-11-30 15:45:00', 25.00);
---==================
INSERT INTO Invoices (InvoiceId, SessionId, CustomerId, CreatedAt, TotalAmount) VALUES
('HD001', 'S001', 'U001', '2025-11-30 15:31:00', 50.00),
('HD002', 'S002', 'U001', '2025-11-30 17:01:00', 30.00),
('HD003', 'S003', 'U002', '2025-11-30 16:01:00', 45.00),
('HD004', NULL, 'U003', '2025-11-30 18:00:00', 100.00), -- Hóa đơn không liên quan đến session cụ thể (SessionId là NULL)
('HD005', 'S005', 'U004', '2025-11-30 15:46:00', 25.00);
--==================
INSERT INTO Sessions (SessionId, CustomerId, ComputerId, StartTime, EndTime, TotalCost) VALUES
('S006', 'U001', 'MAY01', '2025-12-01 08:30:00', '2025-12-01 10:30:00', 60.00),
('S007', 'U002', 'MAY02', '2025-12-01 11:00:00', '2025-12-01 12:00:00', 30.00),
('S008', 'U003', 'MAY03', '2025-12-01 14:00:00', '2025-12-01 15:15:00', 45.00),
('S009', 'U004', 'MAY01', '2025-12-01 16:30:00', '2025-12-01 17:00:00', 15.00),
('S010', 'U001', 'MAY02', '2025-12-02 10:00:00', '2025-12-02 11:30:00', 45.00),
('S011', 'U002', 'MAY03', '2025-12-02 13:00:00', '2025-12-02 14:00:00', 30.00),
('S012', 'U004', 'MAY01', '2025-12-02 15:00:00', NULL, 0.00); -- Phiên đang diễn ra
---================
INSERT INTO Invoices (InvoiceId, SessionId, CustomerId, CreatedAt, TotalAmount) VALUES
('HD006', 'S006', 'U001', '2025-12-01 10:31:00', 60.00), -- Thanh toán Session S006
('HD007', 'S007', 'U002', '2025-12-01 12:01:00', 30.00), -- Thanh toán Session S007
('HD008', 'S008', 'U003', '2025-12-01 15:16:00', 45.00), -- Thanh toán Session S008
('HD009', 'S009', 'U004', '2025-12-01 17:01:00', 15.00), -- Thanh toán Session S009
('HD010', NULL, 'U001', '2025-12-01 10:00:00', 85.00), -- Mua thẻ game/đồ ăn
('HD011', 'S010', 'U001', '2025-12-02 11:31:00', 45.00), -- Thanh toán Session S010
('HD012', 'S011', 'U002', '2025-12-02 14:01:00', 30.00), -- Thanh toán Session S011
('HD013', NULL, 'U003', '2025-12-02 15:00:00', 25.00); -- Mua đồ uống
--==============================
INSERT INTO Sessions (SessionId, CustomerId, ComputerId, StartTime, EndTime, TotalCost) VALUES
('S013', 'U001', 'MAY01', '2024-12-25 09:00:00', '2024-12-25 11:00:00', 60.00), -- Giáng Sinh 2024
('S014', 'U002', 'MAY03', '2024-12-28 14:00:00', '2024-12-28 15:45:00', 52.50),
('S015', 'U003', 'MAY02', '2024-12-29 18:30:00', '2024-12-29 20:30:00', 60.00),
('S016', 'U004', 'MAY01', '2024-12-31 22:00:00', '2025-01-01 01:00:00', 90.00), -- Phiên giao thừa
('S017', 'U001', 'MAY03', '2024-12-30 13:00:00', '2024-12-30 14:30:00', 45.00);
--===========================
INSERT INTO Invoices (InvoiceId, SessionId, CustomerId, CreatedAt, TotalAmount) VALUES
('HD014', 'S013', 'U001', '2024-12-25 11:01:00', 60.00), -- Thanh toán S013
('HD015', 'S014', 'U002', '2024-12-28 15:46:00', 52.50), -- Thanh toán S014
('HD016', 'S015', 'U003', '2024-12-29 20:31:00', 60.00), -- Thanh toán S015
('HD017', 'S016', 'U004', '2025-01-01 01:01:00', 90.00), -- Thanh toán S016 (lưu ý ngày tạo hóa đơn đã sang 2025)
('HD018', 'S017', 'U001', '2024-12-30 14:31:00', 45.00), -- Thanh toán S017
('HD019', NULL, 'U002', '2024-12-27 10:00:00', 150.00); -- Hóa đơn mua thẻ/dịch vụ khác

CREATE TABLE Category (
    CategoryId NVARCHAR(20) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);

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

-- Bảng InvoiceDetails (Chi tiết hóa đơn)

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

INSERT INTO Services
VALUES
('1', 'Dịch vụ 1'),
('2', 'Dịch vụ 2'),
('3', 'Dịch vụ 3');


CREATE TABLE ChatMessage (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FromId NVARCHAR(50) NOT NULL,
    ToId NVARCHAR(50) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    IsRead BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE()
);


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
go