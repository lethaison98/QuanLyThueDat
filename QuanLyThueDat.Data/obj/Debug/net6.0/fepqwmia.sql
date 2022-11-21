IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AppConfig] (
    [Key] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_AppConfig] PRIMARY KEY ([Key])
);
GO

CREATE TABLE [AppRole] (
    [Id] uniqueidentifier NOT NULL,
    [MoTa] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [NormalizedName] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AppRole] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AppRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AppRoleClaims] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AppUser] (
    [Id] uniqueidentifier NOT NULL,
    [HoTen] nvarchar(max) NULL,
    [DonVi] nvarchar(max) NULL,
    [NgaySinh] datetime2 NOT NULL,
    [UserName] nvarchar(max) NULL,
    [NormalizedUserName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [NormalizedEmail] nvarchar(max) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AppUser] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AppUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AppUserClaims] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AppUserLogins] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NULL,
    [ProviderKey] nvarchar(max) NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    CONSTRAINT [PK_AppUserLogins] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [AppUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AppUserRoles] PRIMARY KEY ([UserId], [RoleId])
);
GO

CREATE TABLE [AppUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AppUserTokens] PRIMARY KEY ([UserId])
);
GO

CREATE TABLE [DoanhNghiep] (
    [IdDoanhNghiep] int NOT NULL IDENTITY,
    [TenDoanhNghiep] nvarchar(max) NOT NULL,
    [DiaChi] nvarchar(max) NULL,
    [SoDienThoai] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [TenNguoiDaiDien] nvarchar(max) NULL,
    [CoQuanQuanLyThue] nvarchar(max) NULL,
    [MaSoThue] nvarchar(max) NULL,
    [NgayCap] datetime2 NULL,
    [NoiCap] nvarchar(max) NULL,
    CONSTRAINT [PK_DoanhNghiep] PRIMARY KEY ([IdDoanhNghiep])
);
GO

CREATE TABLE [HopDongThueDat] (
    [IdHopDongThueDat] int NOT NULL IDENTITY,
    [IdDoanhNghiep] int NOT NULL,
    [IdQuyetDinhThueDat] int NULL,
    [SoHopDong] nvarchar(max) NULL,
    [TenHopDong] nvarchar(max) NULL,
    [NgayKyHopDong] datetime2 NULL,
    [NgayHieuLucHopDong] datetime2 NULL,
    [NgayHetHieuLucHopDong] datetime2 NULL,
    CONSTRAINT [PK_HopDongThueDat] PRIMARY KEY ([IdHopDongThueDat]),
    CONSTRAINT [FK_HopDongThueDat_DoanhNghiep_IdDoanhNghiep] FOREIGN KEY ([IdDoanhNghiep]) REFERENCES [DoanhNghiep] ([IdDoanhNghiep]) ON DELETE CASCADE
);
GO

CREATE TABLE [QuyetDinhDonGiaThueDat] (
    [IdQuyetDinhDonGiaThueDat] int NOT NULL IDENTITY,
    [IdDoanhNghiep] int NOT NULL,
    [SoQuyetDinh] nvarchar(max) NULL,
    [NgayKy] datetime2 NULL,
    [DienTich] decimal(18,2) NOT NULL,
    [ViTriThuaDat] nvarchar(max) NULL,
    [NgayHieuLuc] datetime2 NULL,
    [NgayHetHieuLuc] datetime2 NULL,
    CONSTRAINT [PK_QuyetDinhDonGiaThueDat] PRIMARY KEY ([IdQuyetDinhDonGiaThueDat]),
    CONSTRAINT [FK_QuyetDinhDonGiaThueDat_DoanhNghiep_IdDoanhNghiep] FOREIGN KEY ([IdDoanhNghiep]) REFERENCES [DoanhNghiep] ([IdDoanhNghiep]) ON DELETE CASCADE
);
GO

CREATE TABLE [QuyetDinhGiaoDat] (
    [IdQuyetDinhGiaoDat] int NOT NULL IDENTITY,
    [SoQuyetDinh] nvarchar(max) NULL,
    [NgayKy] datetime2 NULL,
    [ViTriThuaDat] nvarchar(max) NULL,
    [DienTich] decimal(18,2) NOT NULL,
    [NgayHieuLuc] datetime2 NULL,
    [NgayHetHieuLuc] datetime2 NULL,
    [DoanhNghiepIdDoanhNghiep] int NULL,
    CONSTRAINT [PK_QuyetDinhGiaoDat] PRIMARY KEY ([IdQuyetDinhGiaoDat]),
    CONSTRAINT [FK_QuyetDinhGiaoDat_DoanhNghiep_DoanhNghiepIdDoanhNghiep] FOREIGN KEY ([DoanhNghiepIdDoanhNghiep]) REFERENCES [DoanhNghiep] ([IdDoanhNghiep])
);
GO

CREATE TABLE [QuyetDinhGiaoLaiDat] (
    [IdQuyetDinhGiaoLaiDat] int NOT NULL IDENTITY,
    [IdDoanhNghiep] int NOT NULL,
    [SoQuyetDinh] nvarchar(max) NULL,
    [NgayKy] datetime2 NULL,
    [ViTriThuaDat] nvarchar(max) NULL,
    [TongDienTich] decimal(18,2) NOT NULL,
    [DienTichKhongPhaiNop] decimal(18,2) NOT NULL,
    [DienTichPhaiNop] decimal(18,2) NOT NULL,
    [TrangThai] bit NOT NULL,
    [NgayHieuLuc] datetime2 NULL,
    [NgayHetHieuLuc] datetime2 NULL,
    CONSTRAINT [PK_QuyetDinhGiaoLaiDat] PRIMARY KEY ([IdQuyetDinhGiaoLaiDat]),
    CONSTRAINT [FK_QuyetDinhGiaoLaiDat_DoanhNghiep_IdDoanhNghiep] FOREIGN KEY ([IdDoanhNghiep]) REFERENCES [DoanhNghiep] ([IdDoanhNghiep]) ON DELETE CASCADE
);
GO

CREATE TABLE [QuyetDinhMienTienThueDat] (
    [IdQuyetDinhMienTienThueDat] int NOT NULL IDENTITY,
    [IdDoanhNghiep] int NOT NULL,
    [IdQuyetDinhThueDat] int NULL,
    [SoQuyetDinhMienTienThueDat] nvarchar(max) NULL,
    [TenQuyetDinhMienTienThueDat] nvarchar(max) NULL,
    [NgayQuyetDinhMienTienThueDat] datetime2 NULL,
    [DienTichMienTienThueDat] decimal(18,2) NOT NULL,
    [ThoiHanMienTienThueDat] nvarchar(max) NULL,
    [NgayHieuLucMienTienThueDat] datetime2 NULL,
    [NgayHetHieuLucMienTienThueDat] datetime2 NULL,
    CONSTRAINT [PK_QuyetDinhMienTienThueDat] PRIMARY KEY ([IdQuyetDinhMienTienThueDat]),
    CONSTRAINT [FK_QuyetDinhMienTienThueDat_DoanhNghiep_IdDoanhNghiep] FOREIGN KEY ([IdDoanhNghiep]) REFERENCES [DoanhNghiep] ([IdDoanhNghiep]) ON DELETE CASCADE
);
GO

CREATE TABLE [QuyetDinhThueDat] (
    [IdQuyetDinhThueDat] int NOT NULL IDENTITY,
    [IdDoanhNghiep] int NOT NULL,
    [SoQuyetDinhThueDat] nvarchar(max) NULL,
    [TenQuyetDinhThueDat] nvarchar(max) NULL,
    [NgayQuyetDinhThueDat] datetime2 NULL,
    [SoQuyetDinhGiaoDat] nvarchar(max) NULL,
    [TenQuyetDinhGiaoDat] nvarchar(max) NULL,
    [NgayQuyetDinhGiaoDat] datetime2 NULL,
    [TongDienTich] decimal(18,2) NOT NULL,
    [ThoiHanThue] nvarchar(max) NULL,
    [DenNgayThue] datetime2 NULL,
    [TuNgayThue] datetime2 NULL,
    [MucDichSuDung] nvarchar(max) NULL,
    [HinhThucThue] nvarchar(max) NULL,
    [ViTriThuaDat] nvarchar(max) NULL,
    [DiaChiThuaDat] nvarchar(max) NULL,
    CONSTRAINT [PK_QuyetDinhThueDat] PRIMARY KEY ([IdQuyetDinhThueDat]),
    CONSTRAINT [FK_QuyetDinhThueDat_DoanhNghiep_IdDoanhNghiep] FOREIGN KEY ([IdDoanhNghiep]) REFERENCES [DoanhNghiep] ([IdDoanhNghiep]) ON DELETE CASCADE
);
GO

CREATE TABLE [ThongBaoDonGiaThueDat] (
    [IdThongBaoDonGiaThueDat] int NOT NULL IDENTITY,
    [IdDoanhNghiep] int NOT NULL,
    [IdQuyetDinhThueDat] int NULL,
    [IdHopDongThueDat] int NULL,
    [SoQuyetDinhThueDat] nvarchar(max) NULL,
    [TenQuyetDinhThueDat] nvarchar(max) NULL,
    [NgayQuyetDinhThueDat] datetime2 NULL,
    [MucDichSuDung] nvarchar(max) NULL,
    [ViTriThuaDat] nvarchar(max) NULL,
    [DiaChiThuaDat] nvarchar(max) NULL,
    [ThoiHanThue] nvarchar(max) NULL,
    [DenNgayThue] datetime2 NULL,
    [TuNgayThue] datetime2 NULL,
    [TongDienTich] decimal(18,2) NOT NULL,
    [SoThongBaoDonGiaThueDat] nvarchar(max) NULL,
    [TenThongBaoDonGiaThueDat] nvarchar(max) NULL,
    [LanThongBaoDonGiaThueDat] nvarchar(max) NULL,
    [NgayThongBaoDonGiaThueDat] datetime2 NULL,
    [DienTichKhongPhaiNop] decimal(18,2) NOT NULL,
    [DienTichPhaiNop] decimal(18,2) NOT NULL,
    [DonGia] decimal(18,2) NOT NULL,
    [ThoiHanDonGia] nvarchar(max) NULL,
    [NgayHieuLucDonGiaThueDat] datetime2 NULL,
    [NgayHetHieuLucDonGiaThueDat] datetime2 NULL,
    [HinhThucThue] nvarchar(max) NULL,
    CONSTRAINT [PK_ThongBaoDonGiaThueDat] PRIMARY KEY ([IdThongBaoDonGiaThueDat]),
    CONSTRAINT [FK_ThongBaoDonGiaThueDat_DoanhNghiep_IdDoanhNghiep] FOREIGN KEY ([IdDoanhNghiep]) REFERENCES [DoanhNghiep] ([IdDoanhNghiep]) ON DELETE CASCADE
);
GO

CREATE TABLE [ThongBaoTienThueDat] (
    [IdThongBaoTienThueDat] int NOT NULL IDENTITY,
    [IdDoanhNghiep] int NOT NULL,
    [IdQuyetDinhThueDat] int NULL,
    [IdHopDongThueDat] int NULL,
    [IdQuyetDinhMienTienThueDat] int NULL,
    [IdThongBaoDonGiaThueDat] int NULL,
    [SoThongBaoTienThueDat] nvarchar(max) NULL,
    [Nam] int NOT NULL,
    [NgayThongBaoTienThueDat] datetime2 NULL,
    [LanThongBaoTienThueDat] nvarchar(max) NULL,
    [SoQuyetDinhThueDat] nvarchar(max) NULL,
    [TenQuyetDinhThueDat] nvarchar(max) NULL,
    [NgayQuyetDinhThueDat] datetime2 NULL,
    [MucDichSuDung] nvarchar(max) NULL,
    [ViTriThuaDat] nvarchar(max) NULL,
    [DiaChiThuaDat] nvarchar(max) NULL,
    [ThoiHanThue] nvarchar(max) NULL,
    [DenNgayThue] datetime2 NULL,
    [TuNgayThue] datetime2 NULL,
    [TongDienTich] decimal(18,2) NOT NULL,
    [SoThongBaoDonGiaThueDat] nvarchar(max) NULL,
    [TenThongBaoDonGiaThueDat] nvarchar(max) NULL,
    [NgayThongBaoDonGiaThueDat] datetime2 NULL,
    [DonGia] decimal(18,2) NOT NULL,
    [DienTichKhongPhaiNop] decimal(18,2) NOT NULL,
    [SoTienMienGiam] decimal(18,2) NOT NULL,
    [DienTichPhaiNop] decimal(18,2) NOT NULL,
    [SoTien] decimal(18,2) NOT NULL,
    [SoTienPhaiNop] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_ThongBaoTienThueDat] PRIMARY KEY ([IdThongBaoTienThueDat]),
    CONSTRAINT [FK_ThongBaoTienThueDat_DoanhNghiep_IdDoanhNghiep] FOREIGN KEY ([IdDoanhNghiep]) REFERENCES [DoanhNghiep] ([IdDoanhNghiep]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_HopDongThueDat_IdDoanhNghiep] ON [HopDongThueDat] ([IdDoanhNghiep]);
GO

CREATE INDEX [IX_QuyetDinhDonGiaThueDat_IdDoanhNghiep] ON [QuyetDinhDonGiaThueDat] ([IdDoanhNghiep]);
GO

CREATE INDEX [IX_QuyetDinhGiaoDat_DoanhNghiepIdDoanhNghiep] ON [QuyetDinhGiaoDat] ([DoanhNghiepIdDoanhNghiep]);
GO

CREATE INDEX [IX_QuyetDinhGiaoLaiDat_IdDoanhNghiep] ON [QuyetDinhGiaoLaiDat] ([IdDoanhNghiep]);
GO

CREATE INDEX [IX_QuyetDinhMienTienThueDat_IdDoanhNghiep] ON [QuyetDinhMienTienThueDat] ([IdDoanhNghiep]);
GO

CREATE INDEX [IX_QuyetDinhThueDat_IdDoanhNghiep] ON [QuyetDinhThueDat] ([IdDoanhNghiep]);
GO

CREATE INDEX [IX_ThongBaoDonGiaThueDat_IdDoanhNghiep] ON [ThongBaoDonGiaThueDat] ([IdDoanhNghiep]);
GO

CREATE INDEX [IX_ThongBaoTienThueDat_IdDoanhNghiep] ON [ThongBaoTienThueDat] ([IdDoanhNghiep]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221026085322_initial1', N'6.0.9');
GO

COMMIT;
GO

