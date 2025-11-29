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
    IpAddress NVARCHAR(50),
    PricePerHour DECIMAL(10,2)
);


INSERT INTO Computers (ComputerId, ComputerName, [Status], IpAddress, PricePerHour)
VALUES 
('MAY01', 'Máy 01', 'AVAILABLE', '192.168.1.101', 5000),
('MAY02', 'Máy 02', 'IN_USE', '192.168.1.102', 5000),
('MAY03', 'Máy 03', 'MAINTENANCE', '192.168.1.103', 5000),
('MAY04', 'Máy 04', 'AVAILABLE', '192.168.1.104', 6000);
INSERT INTO Computers (ComputerId, ComputerName, [Status], IpAddress, PricePerHour)
VALUES ('MAY03', 'Máy 03', 'MAINTENANCE', '192.168.1.103', 5000);
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