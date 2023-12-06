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
	[maKH]     NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_rent] PRIMARY KEY CLUSTERED ([id_rent] ASC),
	CONSTRAINT [FK_rent_kh] FOREIGN KEY ([maKH]) REFERENCES [KhachHang] ([maKH])
);
CREATE TABLE [rentDetails] (
    [id]      INT IDENTITY (1, 1) NOT NULL,
    [id_rent] INT NOT NULL,
    [id_cars] INT NULL,
    [amount]  INT NULL,
	[maKH]     NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_rentDetails] PRIMARY KEY CLUSTERED ([id] ASC),
	 CONSTRAINT [FK_rentDetails_kh] FOREIGN KEY ([maKH]) REFERENCES [KhachHang] ([maKH]),
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

--các tk của nhân viên
insert into [employee] values ('admin','123456',N'Nhật Tiến',1)
insert into [employee] values ('ketoan','123456',N'Hữu Thọ',0)
select * from [employee]
-- các mức giá cho thuê xe
insert into [type] values ('TOYOTA',200000,3000000,'/Content/images/xe/toyota.png')
insert into [type] values ('MERCEDES',200000,3000000,'/Content/images/xe/mercedes.png')
insert into [type] values ('FORD',200000,3000000,'/Content/images/xe/ford.png')
insert into [type] values ('HYUNDAI',200000,3000000,'/Content/images/xe/hyundai.png')
insert into [type] values ('BMW',200000,3000000,'/Content/images/xe/bmw.png')
select*from [type]
-- cacs loại ghế
insert into [seat] values(N'4 chỗ')
insert into [seat] values(N'8 chỗ')
insert into [seat] values(N'16 chỗ')
insert into [seat] values(N'30 chỗ')
insert into [seat] values(N'45 chỗ')
-- Chi tiết xe
insert into [cars] values ('TOYOTA NYC','/Content/images/xe/TOYOTANYC.png',200000,1,1,3,1,N'
Khi nói đến sự kết hợp giữa tiện nghi và độ tin cậy, TOYOTA NYC là một sự lựa chọn không thể bỏ qua. Được thiết kế để phục vụ nhu cầu đa dạng của cuộc sống đô thị, mẫu xe này mang lại trải nghiệm lái linh hoạt, phù hợp với mọi ngõ ngách của thành phố.',N'7.5 L/100km','90%')
insert into [cars] values ('TOYOTA Land Cruiser Prador','/Content/images/xe/ToyotaCruise.png',200000,1,1,3,1,N'
Với khả năng off-road không ai sánh kịp, TOYOTA Land Cruiser Prado là biểu tượng của sức mạnh và độ bền. Xe được trang bị động cơ mạnh mẽ và hệ thống treo thông minh, đảm bảo sự thoải mái tối đa ngay cả trên những cung đường gập ghềnh nhất.',N'7.5 L/100km','90%')
insert into [cars] values ('TOYOTA HIACE','/Content/images/xe/ToyotaHiace.png',200000,1,1,3,1,N'
Xe ô tô Toyota Hiace 2023 là thế hệ thứ 6 của dòng Hiace được cải tiến mạnh mẽ về thiết kế và cả tính năng an toàn.','7.5 L/100km','90%')
insert into [cars] values ('Mescerdes AMG G63','/Content/images/xe/MerG63.png',200000,1,2,3,1,N'
Mercedes-AMG G63 không chỉ là một chiếc SUV cao cấp, nó còn là một biểu tượng của sức mạnh và xa hoa. Với động cơ V8 bi-turbo cực kỳ mạnh mẽ, nội thất tinh xảo và vẻ ngoại thất hầm hố, G63 là sự lựa chọn hàng đầu cho những ai muốn vừa thể hiện đẳng cấp vừa trải nghiệm cảm giác lái phấn khích.',N'7.5 L/100km','90%')
insert into [cars] values('Mercedes Sprinter','/Content/images/xe/MercedesSprintera.png',40000,1,2,3,0,
N'Mercedes Sprinter được xem là chuẩn mực của dòng xe van. Với không gian nội thất rộng rãi, khả năng tùy chỉnh linh hoạt và độ tin cậy vượt trội, Sprinter là sự lựa chọn lý tưởng cho cả việc vận chuyển hàng hóa và chở khách.
',N'9 lít/100km','95%')
insert into [cars] values ('Mescerdes AMG C','/Content/images/xe/MerAMGC.png',200000,1,2,3,1,N'
Mercedes-AMG C mang lại sự pha trộn giữa sự sang trọng và hiệu suất thể thao. Xe có thiết kế ngoại thất sắc sảo, cùng với động cơ AMG mạnh mẽ, đem đến trải nghiệm lái đầy phấn khích mà vẫn không mất đi cảm giác êm ái và thoải mái.','7.5 L/100km','90%')
insert into [cars] values ('Ford Mustang Match-E GT','/Content/images/xe/FordMustang.png',150000,1,3,3,1,N'
SUV chạy điện với phạm vi hoạt động 270 dặm, tăng tốc từ 0-60 MPH trong 3.8 giây, và có hệ thống dẫn động eAWD (dual motor) để tối ưu hóa hiệu suất.','7.5 L/100km','90%')
insert into [cars] values ('Ford stupendous','/Content/images/xe/Fordstupendous.png',200000,1,3,3,1,N'
Ford stupendous mang lại sự pha trộn giữa sự sang trọng và hiệu suất thể thao. Xe có thiết kế ngoại thất sắc sảo, cùng với động cơ AMG mạnh mẽ, đem đến trải nghiệm lái đầy phấn khích mà vẫn không mất đi cảm giác êm ái và thoải mái.','7.5 L/100km','90%')
insert into [cars] values('HYUNDAI SANTA FE 2022','/Content/images/xe/HYUNDAISANTAFE2022a.png',20000,1,4,2,1,N'HYUNDAI SANTA FE 2022 là một mẫu SUV gia đình đa năng, cung cấp không gian nội thất rộng rãi, công nghệ an toàn tiên tiến và hiệu suất vận hành ổn định. Đây là một sự lựa chọn tuyệt vời cho những chuyến đi dài, mang lại sự thoải mái và yên tâm cho mọi hành trình.'
,N'7 lít/100km','95%')
insert into [cars] values ('HYUNDAI STARIA','/Content/images/xe/HuyndaiStaria.png',200000,1,4,3,1,N'
Van/minivan mới với thiết kế rộng rãi và hiện đại, trang bị dẫn động cầu trước, cung cấp nhiều phiên bản khác nhau bao gồm cả phiên bản cao cấp và phiên bản chở hàng.','7.5 L/100km','90%')
insert into [cars] values('BMW 320i 2013','/Content/images/xe/BMW320i2013a.png',80000,1,5,5,1,N'BMW 320i phiên bản 2013 tiếp tục thừa hưởng truyền thống của dòng xe 3 Series với khả năng lái sắc bén, thiết kế ngoại thất thanh lịch và nội thất được chăm chút tỉ mỉ. Dù đã qua nhiều năm, 320i vẫn là lựa chọn hàng đầu cho những ai đam mê sự kết hợp giữa đẳng cấp và hiệu suất lái.',N'7 lít/100km','90%')
insert into [cars] values ('BWM M4','/Content/images/xe/BWMM4.png',200000,1,5,3,1,N'
Phiên bản hiệu suất cao của BMW 4 Series, với động cơ 3.0-litre turbocharged inline-6, sản sinh 431 mã lực, có tùy chọn hộp số sàn 6 cấp hoặc M-DCT 7 cấp.','7.5 L/100km','90%')

-- xem dữ liệu các bảng 

select *from employee
select *from seats
select *from cars
select*from type