use master

drop database QLVESO

create database QLVESO

use QLVESO

create table DaiLy
(
	MaDaiLy varchar(20) primary key,
	TenDaiLy nvarchar(50) not null,
	DiaChi nvarchar(100) not null,
	SDT varchar(15) not null,
	Flag bit
)


create table LoaiVeso
(
	MaLoaiVeSo varchar(20) primary key,
	Tinh nvarchar(20) not null,
	/* NgaySo date, */
	Flag bit
)


create table SoLuongDK 
(
	ID varchar(20) primary key,
	MaDaiLy varchar(20),
	NgayDK date not null,
	SoLuongDK int not null,
	foreign key (MaDaiLy) references DaiLy(MaDaiLy),
	Flag bit
)

select * from SoLuongDK


create table DotPhatHanh
(
	MaDaiLy varchar(20),
	MaLoaiVeSo varchar(20),
	NgayNhan date not null,
	SoLuong int not null,
	SLBanDuoc int,
	TienThanhToan decimal(19,3), /* (100-HoaHong)/100* DoanhThu */
	Flag bit,
	primary key (MaDaiLy, MaLoaiVeSo),
	foreign key (MaDaiLy) references DaiLy(MaDaiLy),
	foreign key (MaLoaiVeSo) references LoaiVeso(MaLoaiVeSo)
)


create table Giai (
	MaGiai varchar(20) primary key,
	TenGiai nvarchar(30),
	SoTienNhan decimal(19, 3),
	Flag bit
)


create table KetQuaSoXo
(
	ID int primary key,  
	MaLoaiVeSo varchar(20),
	MaGiai varchar(20),
	NgaySo date,
	SoTrung varchar(10),
	Flag bit,

	foreign key (MaLoaiVeSo) references LoaiVeSo(MaLoaiVeSo),
	foreign key (MaGiai) references Giai(MaGiai)
)


create table PhieuThu 
(
	MaPhieuThu varchar(20) primary key,
	MaDaiLy varchar(20),
	NgayNop date,
	SoTienNop decimal(19,3),
	Flag bit
	foreign key (MaDaiLy) references DaiLy(MaDaiLy)
)


create table PhieuChi
(
	MaPhieuChi varchar(20) primary key,
	Ngay date,
	NoiDung nvarchar(200),
	SoTien decimal (19, 3)
)

create table CongNo
(
	ID varchar(20) primary key,
	MaDaiLy varchar(20),
	Ngay date,
	SoTienNo decimal(19,3),
	Flag bit,
	
	foreign key (MaDaiLy) references DaiLy(MaDaiLy)
)

/*

DaiLi 1
LoaiVeSo 1
Giai 1

SoLuongDK 2
DotDotPhatHanh 2

KetQuaSoXo 3 
PhieuThu 3
PhieuChi 3

1. Long
2. Tan
3. Hieu

*/

/*NHẬP DỮ LIỆU*/

insert into DaiLy values ('DL01', N'Đại lý vé số Phương Trang', N'93 Phan Đình Phùng, Phường 17, Quận Phú Nhuận, TP.Hồ Chí Minh', '098 877 7444',1)
insert into DaiLy values ('DL02', N'Đại lý vé số Đổi Đời', N'155 Vạn Kiếp, Phường 3, Quận Bình Thạnh, TP.Hồ Chí Minh', '090 364 2129', 1)
insert into DaiLy values ('DL03', N'Đại lý vé số Tấn Lộc', N'155 Vạn Kiếp, Phường 3, Quận Bình Thạnh, TP.Hồ Chí Minh', '090 364 2129', 1)
insert into DaiLy values ('DL04', N'Đại lý vé số Bình An', N'92 Bà Huyện Thanh Quan, Phường 9, Quận 3, TP.Hồ Chí Minh', '096 772 9729', 1)
insert into DaiLy values ('DL05', N'Đại lý vé số Phát Tài', N'329 Phan Đình Phùng, Phường 1, Quận Phú Nhuận, TP.Hồ Chí Minh', '093 412 8268', 1)
insert into DaiLy values ('DL06', N'Đại lý vé số Hòa Phát', N'14 Xô Viết Nghệ Tĩnh, Phường 19, Quận Bình Thạnh, TP.Hồ Chí Minh', '090 398 0280', 1)

insert into LoaiVeso values ('AG67-T03', N'An Giang', 1)
insert into LoaiVeso values ('BL31-T07', N'Bạc Liêu', 1)
insert into LoaiVeso values ('BTRE34-T08', N'Bến Tre', 1)
insert into LoaiVeso values ('BD53-T12', N'Bình Dương', 1)
insert into LoaiVeso values ('BPH93-T04', N'Bình Phước', 1)
insert into LoaiVeso values ('BTH86-T10', N'Bình Thuận', 1)
insert into LoaiVeso values ('CM69-T13', N'Cà Mau', 1)
insert into LoaiVeso values ('CT65-T14', N'Cần Thơ', 1)
insert into LoaiVeso values ('DL49-T15', N'Đà Lạt', 1)
insert into LoaiVeso values ('DNAI22-T05', N'Đồng Nai', 1)
insert into LoaiVeso values ('DT25-T06', N'Đồng Tháp', 1)
insert into LoaiVeso values ('HG95-T16', N'Hậu Giang', 1)
insert into LoaiVeso values ('KG68-T17', N'Kiên Giang', 1)
insert into LoaiVeso values ('LA01-T01', N'Long An', 1)
insert into LoaiVeso values ('STR45-T11', N'Sóc Trăng', 1)
insert into LoaiVeso values ('TN39-T09', N'Tây Ninh', 1)
insert into LoaiVeso values ('TG08-T02', N'Tiền Giang', 1)
insert into LoaiVeso values ('TP32-T08', N'Thành Phố HCM', 1)
insert into LoaiVeso values ('TV84-T18', N'Trà Vinh', 1)
insert into LoaiVeso values ('VL64-T19', N'Vĩnh Long', 1)
insert into LoaiVeso values ('VT72-T20', N'Vũng Tàu', 1)


insert into Giai values ('GI01', N'Giải nhất', 30000000, 1)
insert into Giai values ('GI02', N'Giải nhì', 15000000, 1)
insert into Giai values ('GI03', N'Giải ba', 10000000, 1)
insert into Giai values ('GI04', N'Giải tư', 3000000, 1)
insert into Giai values ('GI05', N'Giải năm', 1000000, 1)
insert into Giai values ('GI06', N'Giải sáu', 400000, 1)
insert into Giai values ('GI07', N'Giải bảy', 200000, 1)
insert into Giai values ('GI08', N'Giải tám', 100000, 1)
insert into Giai values ('GIDB', N'Giải đặc biệt', 2000000000, 1)
insert into Giai values ('GIPDB', N'Giải phụ đặc biệt', 50000000, 1)
insert into Giai values ('GIKK', N'Giải Khuyến khích', 6000000, 1)


insert into SoLuongDK values ('DK001', 'DL01', '02/23/2018', 100, 1)
insert into SoLuongDK values ('DK002', 'DL02', '03/22/2018', 150, 1)
insert into SoLuongDK values ('DK003', 'DL03', '04/21/2018', 100, 1)
insert into SoLuongDK values ('DK004', 'DL04', '05/26/2018', 150, 1)
insert into SoLuongDK values ('DK005', 'DL05', '06/27/2018', 200, 1)
insert into SoLuongDK values ('DK006', 'DL06', '07/29/2018', 300, 1)
insert into SoLuongDK values ('DK007', 'DL02', '08/23/2018', 200, 1)

/*so luong giao tiep theo = sldk * ti le ban 3 dot gan nhat*/
/*hoa hong: la bien toan cuc quy dinh. trong CT */


insert into DotPhatHanh values ('DL01', 'TN39-T09', '2018/10/04', 100, 80, 720000, 1)
insert into DotPhatHanh values ('DL01', 'AG67-T03', '2018/10/04', 100, 90, 810000, 1)
insert into DotPhatHanh values ('DL01', 'BTH86-T10', '2018/10/04', 100, 100, 900000, 1)
insert into DotPhatHanh values ('DL02', 'TN39-T09', '2018/10/04', 150, 130, 1170000, 1)
insert into DotPhatHanh values ('DL02', 'AG67-T03', '2018/10/04', 150, 140, 1260000, 1)
insert into DotPhatHanh values ('DL03', 'BTH86-T10', '2018/10/04', 100, 95, 855000, 1)
insert into DotPhatHanh values ('DL05', 'TN39-T09', '2018/10/04', 200, 190, 1710000, 1)
insert into DotPhatHanh values ('DL05', 'BTH86-T10', '2018/10/04', 200, 180, 1620000, 1)
insert into DotPhatHanh values ('DL06', 'AG67-T03', '2018/10/04', 300, 250, 2250000, 1)
insert into DotPhatHanh values ('DL01', 'VL64-T19', '2018/10/05', 90, 80, 720000, 1)
insert into DotPhatHanh values ('DL01', 'BD53-T12', '2018/10/05', 90, 70, 630000, 1)
insert into DotPhatHanh values ('DL02', 'VL64-T19', '2018/10/05', 135, 135, 1215000, 1)
insert into DotPhatHanh values ('DL02', 'BD53-T12', '2018/10/05', 135, 130, 1170000, 1)
insert into DotPhatHanh values ('DL06', 'BD53-T12', '2018/10/05', 250, 240, 2160000, 1)
insert into DotPhatHanh values ('DL02', 'TP32-T08', '2018/10/06', 145, 145, 1305000, 1)
insert into DotPhatHanh values ('DL02', 'LA01-T01', '2018/10/06', 145, 140, 1260000, 1)

insert into PhieuThu values ('PTH0001', 'DL02', '2018/10/06',1000000,1)
insert into PhieuThu values ('PTH0002', 'DL05', '2018/10/05',500000,1)
insert into PhieuThu values ('PTH0003', 'DL01', '2018/10/06',1000000,1)
insert into PhieuThu values ('PTH0004', 'DL02', '2018/10/06',1000000,1)
insert into PhieuThu values ('PTH0005', 'DL03', '2018/10/06',500000,1)
insert into PhieuThu values ('PTH0006', 'DL06', '2018/10/06',500000,1)

insert into CongNo values ('1', 'DL01', '2018/10/06', 2780000, 1)
insert into CongNo values ('2', 'DL02', '2018/10/04', 2430000, 1)
insert into CongNo values ('3', 'DL02', '2018/10/06', 5380000, 1)
insert into CongNo values ('4', 'DL03', '2018/10/06', 355000, 1)
insert into CongNo values ('5', 'DL05', '2018/10/06', 2830000, 1)
insert into CongNo values ('6', 'DL06', '2018/10/06', 3910000, 1)

insert into PhieuChi values ('PCH0001', '2018/10/05',N'Chi phí in ấn',2000000)
insert into PhieuChi values ('PCH0002', '2018/10/06',N'Chi phí vận chuyển vé',8000000)

insert into KetQuaSoXo values ('1', 'TP32-T08', 'GI01','2018/10/06','77282',1)
insert into KetQuaSoXo values ('2', 'TP32-T08', 'GI02','2018/10/06','75104',1)
insert into KetQuaSoXo values ('3', 'TP32-T08', 'GI03','2018/10/06','42663',1)
insert into KetQuaSoXo values ('4', 'TP32-T08', 'GI03','2018/10/06','30772',1)
insert into KetQuaSoXo values ('5', 'TP32-T08', 'GI04','2018/10/06','35641',1)
insert into KetQuaSoXo values ('6', 'TP32-T08', 'GI04','2018/10/06','15591',1)
insert into KetQuaSoXo values ('7', 'TP32-T08', 'GI04','2018/10/06','03619',1)
insert into KetQuaSoXo values ('8', 'TP32-T08', 'GI04','2018/10/06','30705',1)
insert into KetQuaSoXo values ('9', 'TP32-T08', 'GI04','2018/10/06','99993',1)
insert into KetQuaSoXo values ('10', 'TP32-T08', 'GI04','2018/10/06','36204',1)
insert into KetQuaSoXo values ('11', 'TP32-T08', 'GI04','2018/10/06','74553',1)
insert into KetQuaSoXo values ('12', 'TP32-T08', 'GI05','2018/10/06','9840',1)
insert into KetQuaSoXo values ('13', 'TP32-T08', 'GI06','2018/10/06','7076',1)
insert into KetQuaSoXo values ('14', 'TP32-T08', 'GI06','2018/10/06','5152',1)
insert into KetQuaSoXo values ('15', 'TP32-T08', 'GI06','2018/10/06','2296',1)
insert into KetQuaSoXo values ('16', 'TP32-T08', 'GI07','2018/10/06','279',1)
insert into KetQuaSoXo values ('17', 'TP32-T08', 'GI08','2018/10/06','38',1)
insert into KetQuaSoXo values ('18', 'TP32-T08', 'GIDB','2018/10/06','075811',1)


