use CitricStore
Go

--TẠO BẢNG
Create table NHAPHATHANH
(
	MaNPH int Identity(1,1),
	TenNPH nvarchar(70) not null,
	constraint PK_NHAPHATHANH primary key (MaNPH)
)

Create table UNGDUNG
(
	MaUngDung int Identity(1,1),
	TenUngDung nvarchar(100) not null,
	GioiThieu ntext,
	KichThuocRam nvarchar(10),
	NgonNgu nvarchar(50),
	HeDieuHanh nvarchar(50),
	LinkTai ntext,
	MaTheLoai int,
	HinhMinhHoa nvarchar(50),
	MaNPH int,
	NgayCapNhat smalldatetime,
	constraint PK_UNGDUNG primary key (MaUngDung)
)
 Create table THELOAI
 (
	MaTheLoai int identity(1,1),
	TenTheLoai nvarchar(50),
	constraint PK_THELOAI primary key (MaTheLoai)
 )

 Create table KHACHHANG
 (
	MaKH INT IDENTITY(1,1),
	HoTenKH nVarchar(50) NOT NULL,
	DienthoaiKH Varchar(10),
	TenDN Varchar(15) UNIQUE,
	Matkhau Varchar(15) NOT NULL,
	Ngaysinh SMALLDATETIME,
	Gioitinh Bit Default 1,
	Email Varchar(50) UNIQUE,
	Daduyet Bit Default 0,
	CONSTRAINT PK_KHACHHANG PRIMARY KEY(MaKH)
 )

 --KHOÁ NGOẠI
 Alter table UNGDUNG add constraint FK_UNGDUNG_NHAPHATHANH
					 foreign key (MaNPH)
					 references NHAPHATHANH (MaNPH) 
 Alter table UNGDUNG add constraint FK_UNGDUNG_THELOAI
					 foreign key (MaTheLoai)
					 references THELOAI (MaTheLoai) 
