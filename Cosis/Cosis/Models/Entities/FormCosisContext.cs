using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Cosis.Models.Entities
{
    public partial class FormCosisContext : DbContext
    {
        public FormCosisContext()
        {
        }

        public FormCosisContext(DbContextOptions<FormCosisContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DanhSachNhanToAnhHuong> DanhSachNhanToAnhHuong { get; set; }
        public virtual DbSet<Detail> Detail { get; set; }
        public virtual DbSet<LoaiHinhKinhte> LoaiHinhKinhte { get; set; }
        public virtual DbSet<LoaiPhieu> LoaiPhieu { get; set; }
        public virtual DbSet<Master> Master { get; set; }
        public virtual DbSet<NganhHoatDongKinhDoanh> NganhHoatDongKinhDoanh { get; set; }
        public virtual DbSet<NganhKinhDoanh> NganhKinhDoanh { get; set; }
        public virtual DbSet<NhanToAnhHuong> NhanToAnhHuong { get; set; }
        public virtual DbSet<NhanToThu9> NhanToThu9 { get; set; }
        public virtual DbSet<PhuongXa> PhuongXa { get; set; }
        public virtual DbSet<QuanHuyen> QuanHuyen { get; set; }
        public virtual DbSet<TaiKhoang> TaiKhoang { get; set; }
        public virtual DbSet<ThongTinCaThe> ThongTinCaThe { get; set; }
        public virtual DbSet<ThongTinDoanhNghiep> ThongTinDoanhNghiep { get; set; }
        public virtual DbSet<TinhTp> TinhTp { get; set; }
        public virtual DbSet<VaiTro> VaiTro { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-VIEL6S6A\\THAILAI;Initial Catalog=FormCosis;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DanhSachNhanToAnhHuong>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.MaAh)
                    .HasColumnName("MaAH")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaPhieu)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.MaAhNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaAh)
                    .HasConstraintName("FK__DanhSachNh__MaAH__2F9A1060");

                entity.HasOne(d => d.MaPhieuNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaPhieu)
                    .HasConstraintName("FK__DanhSachN__MaPhi__2EA5EC27");
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => new { e.MaPhieu, e.Stt })
                    .HasName("unique_Detail")
                    .IsUnique();

                entity.Property(e => e.DuTinh).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Dvt)
                    .HasColumnName("DVT")
                    .HasMaxLength(20);

                entity.Property(e => e.MaPhieu)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Stt)
                    .HasColumnName("STT")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TenCt)
                    .HasColumnName("TenCT")
                    .HasMaxLength(500);

                entity.Property(e => e.ThthangTruoc)
                    .HasColumnName("THThangTruoc")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Tong).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TongCongDon).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Tttm)
                    .HasColumnName("TTTM")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.MaPhieuNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaPhieu)
                    .HasConstraintName("FK__Detail__MaPhieu__2CBDA3B5");
            });

            modelBuilder.Entity<LoaiHinhKinhte>(entity =>
            {
                entity.HasKey(e => e.MaLh)
                    .HasName("PK__LoaiHinh__2725C77F5F86CA89");

                entity.Property(e => e.MaLh)
                    .HasColumnName("MaLH")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TenLh)
                    .HasColumnName("TenLH")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<LoaiPhieu>(entity =>
            {
                entity.HasKey(e => e.MaLoaiPhieu)
                    .HasName("PK__LoaiPhie__22B219FD45A7F25F");

                entity.Property(e => e.MaLoaiPhieu).HasMaxLength(50);

                entity.Property(e => e.TenLoaiPhieu).HasMaxLength(250);
            });

            modelBuilder.Entity<Master>(entity =>
            {
                entity.HasKey(e => e.MaPhieu)
                    .HasName("PK__Master__2660BFE002E6373D");

                entity.HasIndex(e => new { e.MaSoThue, e.MaSoThue2, e.MaCoSo, e.ThangThucHien, e.ThangDuTinh })
                    .HasName("unique_Master")
                    .IsUnique();

                entity.Property(e => e.MaPhieu)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DiaChiCuThe).HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaCoSo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaLh)
                    .HasColumnName("MaLH")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaPhuongXa)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaQuanHuyen)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaSoThue)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaSoThue2)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaTinhTp)
                    .HasColumnName("MaTinhTP")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Nam)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NgayTao).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SdtnguoiTraLoi)
                    .HasColumnName("SDTNguoiTraLoi")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ten).HasMaxLength(200);

                entity.Property(e => e.TenNguoiTraLoi).HasMaxLength(50);

                entity.Property(e => e.ThangDuTinh)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ThangThucHien)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.MaCoSoNavigation)
                    .WithMany(p => p.Master)
                    .HasForeignKey(d => d.MaCoSo)
                    .HasConstraintName("FK__Master__MaCoSo__22401542");

                entity.HasOne(d => d.MaLhNavigation)
                    .WithMany(p => p.Master)
                    .HasForeignKey(d => d.MaLh)
                    .HasConstraintName("FK__Master__MaLH__214BF109");

                entity.HasOne(d => d.MaPhuongXaNavigation)
                    .WithMany(p => p.Master)
                    .HasForeignKey(d => d.MaPhuongXa)
                    .HasConstraintName("FK__Master__MaPhuong__251C81ED");

                entity.HasOne(d => d.MaQuanHuyenNavigation)
                    .WithMany(p => p.Master)
                    .HasForeignKey(d => d.MaQuanHuyen)
                    .HasConstraintName("FK__Master__MaQuanHu__24285DB4");

                entity.HasOne(d => d.MaTinhTpNavigation)
                    .WithMany(p => p.Master)
                    .HasForeignKey(d => d.MaTinhTp)
                    .HasConstraintName("FK__Master__MaTinhTP__2334397B");

                entity.HasOne(d => d.MaSoThueNavigation)
                    .WithMany(p => p.Master)
                    .HasForeignKey(d => new { d.MaSoThue, d.MaSoThue2 })
                    .HasConstraintName("FK__Master__2610A626");
            });

            modelBuilder.Entity<NganhHoatDongKinhDoanh>(entity =>
            {
                entity.HasKey(e => e.MaNganh)
                    .HasName("PK__NganhHoa__A2CEF50D56126D5A");

                entity.Property(e => e.MaNganh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TenNganh).HasMaxLength(250);
            });

            modelBuilder.Entity<NganhKinhDoanh>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.MaCoSo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaNganh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaSoThue)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaSoThue2)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.MaCoSoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaCoSo)
                    .HasConstraintName("FK__NganhKinh__MaCoS__29E1370A");

                entity.HasOne(d => d.MaNganhNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaNganh)
                    .HasConstraintName("FK__NganhKinh__MaNga__28ED12D1");

                entity.HasOne(d => d.MaSoThueNavigation)
                    .WithMany()
                    .HasForeignKey(d => new { d.MaSoThue, d.MaSoThue2 })
                    .HasConstraintName("FK__NganhKinhDoanh__27F8EE98");
            });

            modelBuilder.Entity<NhanToAnhHuong>(entity =>
            {
                entity.HasKey(e => e.MaAh)
                    .HasName("PK__NhanToAn__27247E42311A4AB2");

                entity.Property(e => e.MaAh)
                    .HasColumnName("MaAH")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NoiDungAh)
                    .HasColumnName("NoiDungAH")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<NhanToThu9>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.MaPhieu)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NoiDung).HasMaxLength(200);

                entity.HasOne(d => d.MaPhieuNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaPhieu)
                    .HasConstraintName("FK__NhanToThu__MaPhi__318258D2");
            });

            modelBuilder.Entity<PhuongXa>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Level).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<QuanHuyen>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<TaiKhoang>(entity =>
            {
                entity.HasKey(e => e.TaiKhoang1);

                entity.Property(e => e.TaiKhoang1)
                    .HasColumnName("TaiKhoang")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdvaiTro)
                    .HasColumnName("IDVaiTro")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdvaiTroNavigation)
                    .WithMany(p => p.TaiKhoang)
                    .HasForeignKey(d => d.IdvaiTro)
                    .HasConstraintName("FK_TaiKhoang_VaiTro");
            });

            modelBuilder.Entity<ThongTinCaThe>(entity =>
            {
                entity.HasKey(e => e.MaCoSo)
                    .HasName("PK__ThongTin__152D0634CE5DCEDE");

                entity.Property(e => e.MaCoSo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DiaChiCuThe).HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaLh)
                    .HasColumnName("MaLH")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaLoaiPhieu).HasMaxLength(50);

                entity.Property(e => e.MaPhuongXa)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaQuanHuyen)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaSoThue)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaSoThue2)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaTinhTp)
                    .HasColumnName("MaTinhTP")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SdtcoSo)
                    .HasColumnName("SDTCoSo")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TaiKhoan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenCoSo).HasMaxLength(200);

                entity.HasOne(d => d.MaLhNavigation)
                    .WithMany(p => p.ThongTinCaThe)
                    .HasForeignKey(d => d.MaLh)
                    .HasConstraintName("FK__ThongTinCa__MaLH__67DE6983");

                entity.HasOne(d => d.MaLoaiPhieuNavigation)
                    .WithMany(p => p.ThongTinCaThe)
                    .HasForeignKey(d => d.MaLoaiPhieu)
                    .HasConstraintName("FK__ThongTinC__MaLoa__7720AD13");

                entity.HasOne(d => d.MaPhuongXaNavigation)
                    .WithMany(p => p.ThongTinCaThe)
                    .HasForeignKey(d => d.MaPhuongXa)
                    .HasConstraintName("FK__ThongTinC__MaPhu__1D7B6025");

                entity.HasOne(d => d.MaQuanHuyenNavigation)
                    .WithMany(p => p.ThongTinCaThe)
                    .HasForeignKey(d => d.MaQuanHuyen)
                    .HasConstraintName("FK__ThongTinC__MaQua__1C873BEC");

                entity.HasOne(d => d.MaTinhTpNavigation)
                    .WithMany(p => p.ThongTinCaThe)
                    .HasForeignKey(d => d.MaTinhTp)
                    .HasConstraintName("FK__ThongTinC__MaTin__1B9317B3");

                entity.HasOne(d => d.TaiKhoanNavigation)
                    .WithMany(p => p.ThongTinCaThe)
                    .HasForeignKey(d => d.TaiKhoan)
                    .HasConstraintName("FK__ThongTinC__TaiKh__6CA31EA0");
            });

            modelBuilder.Entity<ThongTinDoanhNghiep>(entity =>
            {
                entity.HasKey(e => new { e.MaSoThue, e.MaSoThue2 })
                    .HasName("PK__ThongTin__E1639B2CDD779CF0");

                entity.Property(e => e.MaSoThue)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaSoThue2)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DiaChiCuThe).HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaLh)
                    .HasColumnName("MaLH")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaLoaiPhieu).HasMaxLength(50);

                entity.Property(e => e.MaPhuongXa)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaQuanHuyen)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaTinhTp)
                    .HasColumnName("MaTinhTP")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SdtdoanhNghiep)
                    .HasColumnName("SDTDoanhNghiep")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TaiKhoan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDoanhNghiep).HasMaxLength(200);

                entity.HasOne(d => d.MaLhNavigation)
                    .WithMany(p => p.ThongTinDoanhNghiep)
                    .HasForeignKey(d => d.MaLh)
                    .HasConstraintName("FK__ThongTinDo__MaLH__6BAEFA67");

                entity.HasOne(d => d.MaLoaiPhieuNavigation)
                    .WithMany(p => p.ThongTinDoanhNghiep)
                    .HasForeignKey(d => d.MaLoaiPhieu)
                    .HasConstraintName("FK__ThongTinD__MaLoa__725BF7F6");

                entity.HasOne(d => d.MaPhuongXaNavigation)
                    .WithMany(p => p.ThongTinDoanhNghiep)
                    .HasForeignKey(d => d.MaPhuongXa)
                    .HasConstraintName("FK__ThongTinD__MaPhu__17C286CF");

                entity.HasOne(d => d.MaQuanHuyenNavigation)
                    .WithMany(p => p.ThongTinDoanhNghiep)
                    .HasForeignKey(d => d.MaQuanHuyen)
                    .HasConstraintName("FK__ThongTinD__MaQua__16CE6296");

                entity.HasOne(d => d.MaTinhTpNavigation)
                    .WithMany(p => p.ThongTinDoanhNghiep)
                    .HasForeignKey(d => d.MaTinhTp)
                    .HasConstraintName("FK__ThongTinD__MaTin__15DA3E5D");

                entity.HasOne(d => d.TaiKhoanNavigation)
                    .WithMany(p => p.ThongTinDoanhNghiep)
                    .HasForeignKey(d => d.TaiKhoan)
                    .HasConstraintName("FK__ThongTinD__TaiKh__6D9742D9");
            });

            modelBuilder.Entity<TinhTp>(entity =>
            {
                entity.ToTable("TinhTP");

                entity.Property(e => e.Id)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<VaiTro>(entity =>
            {
                entity.HasKey(e => e.IdvaiTro);

                entity.Property(e => e.IdvaiTro)
                    .HasColumnName("IDVaiTro")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TenVaiTro).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
