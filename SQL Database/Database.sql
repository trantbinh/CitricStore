use CitricStore
Go

--TẠO BẢNG
Create table NHAPHATHANH
(
	MaNPH int Identity(1,1),
	TenNPH nvarchar(70) not null,
	constraint PK_NHAPHATHANH primary key (MaNPH)
)

Create table THELOAI
(
	MaTheLoai int identity(1,1),
	TenTheLoai nvarchar(50),
	constraint PK_THELOAI primary key (MaTheLoai)
)

Create table THELOAIGAME
(
	MaTheLoaiGame int identity(1,1),
	TenTheLoai nvarchar(50),
	constraint PK_THELOAIGAME primary key (MaTheLoaiGame)
)

Create table THELOAIAPP
(
	MaTheLoaiApp int identity(1,1),
	TenTheLoai nvarchar(50),
	constraint PK_THELOAIAPP primary key (MaTheLoaiApp)
)

 Create table HEDIEUHANH
(
	MaHDH	int identity(1,1),
	TenHDH	nvarchar(50),
	constraint PK_HEDIEUHANH primary key (MaHDH)
)

 Create table NGONNGU
(
	MaNgonNgu	int identity(1,1),
	TenNgonNgu	nvarchar(70),
	constraint PK_NGONNGU primary key (MaNgonNgu)
)

 Create table KHACHHANG
 (
	MaKH INT IDENTITY(1,1),
	HoTenKH nVarchar(50) NOT NULL,
	DienthoaiKH Varchar(10),
	TenDN Varchar(15) UNIQUE,
	Matkhau Varchar(15) NOT NULL,
	Ngaysinh SMALLDATETIME,
	GioiTinh nvarchar(3),
	Email Varchar(50) UNIQUE,
	CONSTRAINT PK_KHACHHANG PRIMARY KEY(MaKH)
 )

Create table APP
(
	MaApp int Identity(1,1),
	TenApp nvarchar(100) not null,
	GioiThieu ntext,
	DungLuong nvarchar(10),
	MaTheLoaiApp int,
	MaNPH int,
	MaHDH int,
	MaNgonNgu int,
	NgayCapNhat datetime,
	DanhGia nvarchar(5),
	DonGia decimal(18,2),
	HinhNen nvarchar(30),
	HinhCT1 nvarchar(30),
	HinhCT2 nvarchar(30),
	HinhCT3 nvarchar(30),
	HinhCT4 nvarchar(30),
	LinkTai ntext,
	constraint PK_APP primary key (MaApp)
)
SELECT * FROM KHACHHANG
Create table GAME
(
	MaGame int Identity(1,1),
	TenGame nvarchar(100) not null,
	GioiThieu ntext,
	DungLuong nvarchar(10),
	NgonNgu nvarchar(50),
	HeDieuHanh nvarchar(50),
	MaTheLoaiGame int,
	MaNPH int,
	MaHDH int,
	MaNgonNgu int,
	NgayCapNhat datetime,
	DanhGia nvarchar(5),
	DonGia decimal(18,2),
	HinhNen nvarchar(30),
	HinhCT1 nvarchar(30),
	HinhCT2 nvarchar(30),
	HinhCT3 nvarchar(30),
	HinhCT4 nvarchar(30),
	LinkTai ntext,
	constraint PK_Game primary key (MaGame)
)

Create table OVERALL
(
	Ma int Identity(1,1),
	Ten nvarchar(100) not null,
	GioiThieu ntext,
	DungLuong nvarchar(10),
	MaNPH int,
	MaHDH int,
	MaNgonNgu int,
	MaTheLoai int,
	NgayCapNhat datetime,
	DanhGia nvarchar(5),
	DonGia decimal(18,2),
	HinhNen nvarchar(30),
	HinhCT1 nvarchar(30),
	HinhCT2 nvarchar(30),
	HinhCT3 nvarchar(30),
	HinhCT4 nvarchar(30),
	LinkTai ntext,
	AppOrGame nvarchar(10),
	constraint PK_Overall primary key (Ma)
)

 Create table ORDER_INFO
 (
	MaOrder int identity(1,1),
	NgayOrder datetime,
	MaKH int,
	TenOrder nvarchar(max),
	SDTOrder varchar(10),
	DiaChiOrder nvarchar(max),
	TrangThaiXuLy int,
	EmailOrder varchar(50),
	Constraint PK_ORDERINFO primary key (MaOrder)
 )

 Create table ORDER_PRODUCT
 (
	ID int identity(1,1),
	MaUngDung int,
	MaOrder int,
	SoLuong int,
	DonGia decimal (18,2),
	constraint PK_ORDERPRODUCT
							primary key (ID)

 )

 Create table TRANGTHAIDONHANG
 (
	Ma int identity(1,1),
	Ten nvarchar(max),
	constraint PK_TRANGTHAI primary key(Ma)
 )

Create table BANK
(
	BankID int identity(1,1),
	BankName nvarchar(Max),
	constraint PK_BANK primary key (BankID)
)





 --KHOÁ NGOẠI
 Alter table APP add constraint FK_APP_NHAPHATHANH
					 foreign key (MaNPH)
					 references NHAPHATHANH (MaNPH) 
 Alter table APP add constraint FK_APP_THELOAIAPP
					 foreign key (MaTheLoaiApp)
					 references THELOAIAPP (MaTheLoaiApp) 
 Alter table APP add constraint FK_APP_NGONNGU
					 foreign key (MaNgonNgu)
					 references NGONNGU (MaNgonNgu) 
 Alter table APP add constraint FK_APP_HEDIEUHANH
					 foreign key (MaHDH)
					 references HEDIEUHANH (MaHDH) 

					 select * from game
 Alter table GAME add constraint FK_GAME_NHAPHATHANH
					 foreign key (MaNPH)
					 references NHAPHATHANH (MaNPH) 
 Alter table GAME add constraint FK_GAME_THELOAIGAME
					 foreign key (MaTheLoaiGame)
					 references THELOAIGAME (MaTheLoaiGame) 
 Alter table GAME add constraint FK_GAME_NGONNGU
					 foreign key (MaNgonNgu)
					 references NGONNGU (MaNgonNgu) 
 Alter table GAME add constraint FK_GAME_HEDIEUHANH
					 foreign key (MaHDH)
					 references HEDIEUHANH (MaHDH) 


 Alter table OVERALL add constraint FK_OVERALL_NHAPHATHANH
					 foreign key (MaNPH)
					 references NHAPHATHANH (MaNPH) 
 Alter table OVERALL add constraint FK_OVERALL_NGONNGU
					 foreign key (MaNgonNgu)
					 references NGONNGU (MaNgonNgu) 
 Alter table OVERALL add constraint FK_OVERALL_HEDIEUHANH
					 foreign key (MaHDH)
					 references HEDIEUHANH (MaHDH) 
 Alter table OVERALL add constraint FK_OVERALL_THELOAI
					 foreign key (MaTheLoai)
					 references THELOAI (MaTheLoai) 

 Alter table ORDER_PRODUCT add constraint FK_ORDERPRODUCT_ORDERINFO
							foreign key (MaOrder)
							references ORDER_INFO (MaOrder) 
 Alter table ORDER_PRODUCT add constraint FK_ORDERPRODUCT_OVERALL
							foreign key (MaUngDung)
							references OVERALL (Ma) 

 Alter table ORDER_INFO add constraint FK_ORDERINFO_KHACHHANG
						foreign key (MaKH)
						references KHACHHANG (MaKH) 
 Alter table ORDER_INFO add constraint FK_ORDERINFO_TRANGTHAI
						foreign key (TrangThaiXuLy)
						references TRANGTHAIDONHANG (Ma)
 Alter table ORDER_INFO add constraint FK_ORDERINFO_BANK
						foreign key (MaNganHang)
						references BANK (BankID)


-- TẠO CỘT TRONG BẢNG
alter table Overall
add AppOrGame nvarchar(10)

-- XOÁ CỘT TRONG BẢNG
alter table oRDER_info
drop column TrangThaiXuLy

-- XOÁ DÒNG TRONG BẢNG
delete from OVERALL
where Ma > 1

-- CẬP NHẬT DỮ LIỆU
Update OVERALL
set AppOrGame = N'Game'
where Ma>27


 Select * from ORDER_INFO
 Select * from BANK