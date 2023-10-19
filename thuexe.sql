use master
if exists (select*from sysdatabases where name = 'ThueXe')
drop database ThueXe
go 
create database ThueXe
go
use ThueXe
go
CREATE TABLE [KhachHang] (
    [maKH]     NVARCHAR (50) NOT NULL,
    [hoTenKH]  NVARCHAR (50) NULL,
    [emailKH]  NVARCHAR (50) NULL,
    [sdtKH]    CHAR (10)     NULL,
    [diaChiKH] NVARCHAR (50) NULL,
    [matKhau]  NVARCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([maKH] ASC)
);
CREATE TABLE [type] (
    [id_type]     INT            IDENTITY (1, 1) NOT NULL,
    [type]        NVARCHAR (50)  NOT NULL,
    [price_day]   INT            NULL,
    [price_month] INT            NULL,
    [image]       NVARCHAR (100) NULL,
    CONSTRAINT [PK_type] PRIMARY KEY CLUSTERED ([id_type] ASC)
);
CREATE TABLE [seat] (
    [id_seat]     INT            IDENTITY (1, 1) NOT NULL,
    [seat_name]        NVARCHAR (50)  NOT NULL,
   
    CONSTRAINT [PK_seat] PRIMARY KEY CLUSTERED ([id_seat] ASC)
);
CREATE TABLE [cars] (
    [id_cars]  INT            IDENTITY (1, 1) NOT NULL,
    [name]     NVARCHAR (50)  NULL,
    [image]    NVARCHAR (100) NULL,
    [price]    INT            NULL,
    [IsActive] BIT            NULL,
    [id_type]  INT            NULL,
	[id_seat] INT			NULL,
    [Hot]    BIT            NULL,
    [describe] NVARCHAR (MAX) NULL,
    [consume]  NCHAR (30)     NULL,
    [status]   NCHAR (10)     NULL,
    CONSTRAINT [Pk_cars] PRIMARY KEY CLUSTERED ([id_cars] ASC),
    CONSTRAINT [FK_cars_type] FOREIGN KEY ([id_type]) REFERENCES [type] ([id_type]),
	CONSTRAINT [FK_cars_seat] FOREIGN KEY ([id_seat]) REFERENCES [seat] ([id_seat])
);
CREATE TABLE [rent] (
    [id_rent] INT           IDENTITY (1, 1) NOT NULL,
    [note]    NVARCHAR (50) NULL,
    [name]    NVARCHAR (50) NULL,
    [phone]   NVARCHAR (20) NULL,
    [mail]    NVARCHAR (50) NULL,
    [date]    DATETIME      NULL,
    CONSTRAINT [PK_rent] PRIMARY KEY CLUSTERED ([id_rent] ASC)
);
CREATE TABLE [rentDetails] (
    [id]      INT IDENTITY (1, 1) NOT NULL,
    [id_rent] INT NOT NULL,
    [id_cars] INT NULL,
    [amount]  INT NULL,
    CONSTRAINT [PK_rentDetails] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rentDetails_cars] FOREIGN KEY ([id_cars]) REFERENCES [cars] ([id_cars]),
    CONSTRAINT [FK_rentDetails_rent] FOREIGN KEY ([id_rent]) REFERENCES [rent] ([id_rent])
);
CREATE TABLE [mail] (
    [ContactId]   INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (150) NULL,
    [Phone]       NVARCHAR (15)  NULL,
    [Email]       NVARCHAR (150) NULL,
    [Message]     NVARCHAR (MAX) NULL,
    [IsRead]      BIT            NOT NULL,
    [CreatedDate] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([ContactId] ASC)
);
CREATE TABLE [employee] (
    [id_employee] INT           IDENTITY (1, 1) NOT NULL,
    [account]     NVARCHAR (20) NULL,
    [pass]        NVARCHAR (30) NULL,
    [name]        NVARCHAR (50) NULL,
    [fulControl]  BIT           NULL,
    CONSTRAINT [pk_employee] PRIMARY KEY CLUSTERED ([id_employee] ASC)
);
CREATE TABLE [contact] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [address]       NVARCHAR (50)  NULL,
    [phone]         NVARCHAR (15)  NULL,
    [email]         NVARCHAR (50)  NULL,
    [facebook_link] NVARCHAR (100) NULL,
    CONSTRAINT [PK__contact__DDDF328E0B62B1D8] PRIMARY KEY CLUSTERED ([id] ASC)
);
CREATE TABLE [bill] (
    [id_bill]    INT           IDENTITY (1, 1) NOT NULL,
    [id_rent]    INT           NOT NULL,
    [money_hour] INT           NULL,
    [date_start] DATETIME      NULL,
    [date_end]   DATETIME      NULL,
    [status]     NVARCHAR (50) NULL,
    CONSTRAINT [PK_nill] PRIMARY KEY CLUSTERED ([id_bill] ASC),
    CONSTRAINT [FK_bill_rent] FOREIGN KEY ([id_rent]) REFERENCES [dbo].[rent] ([id_rent])
);
insert into [employee] values ('admin','123456',N'Nhật Tiến',1)
insert into [employee] values ('ketoan','123456',N'Hữu Thọ',0)
select * from [employee]

insert into [type] values ('TOYOTA',200000,3000000,'/Content/images/toyota.png')
insert into [type] values ('MERCEDES',200000,3000000,'/Content/images/mercedes.png')
insert into [type] values ('FORD',200000,3000000,'/Content/images/ford.png')
insert into [type] values ('HYUNDAI',200000,3000000,'/Content/images/hyundai.png')
insert into [type] values ('BMW',200000,3000000,'/Content/images/bmw.png')
select*from [type]

insert into [seat] values('4 chỗ')
insert into [seat] values('8 chỗ')
insert into [seat] values('16 chỗ')
insert into [seat] values('30 chỗ')
insert into [seat] values('45 chỗ')

insert into [cars] values('HYUNDAI SANTA FE 2022','/Content/images/xe/HYUNDAISANTAFE2022a.png',20000,1,4,2,1,N'HYUNDAI SANTA FE 2022,Số ghế 8 chỗ
NL tiêu hao 8 lít/100km + Huynhdai Santafe xe rất mới đời mới 2022, xe gia đình , sạch sẽ thơm đảm bảo đi không bị say xe.',N'7 lít/100km','95%')
insert into [cars] values('Mercedes Sprinter','/Content/images/xe/MercedesSprintera.png',40000,1,2,3,0,N'Nội thất của Mercedes Sprinter đời mới 2018 được thiết kế với triết lý adVANce, tích hợp sẵn kết nối internet và tính năng Mercedes Pro Connect mới giúp người dùng quản lý đội xe hiệu quả hơn.
',N'9 lít/100km','95%')
insert into [cars] values('BMW 320i 2013','/Content/images/xe/BMW320i2013a.png',80000,1,5,5,1,N'BMW xe gia đình đời 2013 lên full 2020',N'7 lít/100km','90%')
insert into [cars] values ('TOYOTA HIACE','/Content/images/xe/ToyotaHiace.png',200000,1,1,3,1,'<p>
Xe ô tô Toyota Hiace 2023 là thế hệ thứ 6 của dòng Hiace được cải tiến mạnh mẽ về thiết kế và cả tính năng an toàn.</p>','7.5 L/100km','90%')
