using Cosis.Models;
using Cosis.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cosis.Controllers
{
    public class Form1_1Controller : Controller
    {
        private readonly FormCosisContext context;

        public Form1_1Controller(FormCosisContext context)
        {
            this.context = context;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public IActionResult Index()
        {
            string s = HttpContext.Request.Host.Port.ToString();
            PhieuDieuTra phieu = new PhieuDieuTra();
            ViewBag.LoaiPhieu = "Form1_1";
            if (User.IsInRole("001"))
            {
                ViewBag.thang = DateTime.Now.Month.ToString();
                ViewBag.nam = DateTime.Now.Year.ToString();

                return View("ViewIndexAdmin");
            }
            if (User.IsInRole("002"))
            {

                var ttdn = context.ThongTinDoanhNghiep.FirstOrDefault(x => x.TaiKhoan == User.Identity.Name);
                var master = context.Master.FirstOrDefault(x => x.MaSoThue == ttdn.MaSoThue && x.MaSoThue2 == ttdn.MaSoThue2 && x.ThangDuTinh == DateTime.Now.Month.ToString() && x.Nam == DateTime.Now.Year.ToString());
                if (master != null)
                {
                    ViewBag.thang = master.ThangDuTinh;
                    ViewBag.nam = master.Nam;
                    phieu.Master = master;
                    var ntah = context.DanhSachNhanToAnhHuong.Where(x => x.MaPhieu == master.MaPhieu.Trim()).ToList();
                    phieu.DanhSachNhanToAnhHuong = ntah;
                    var detail = context.Detail.Where(x => x.MaPhieu == master.MaPhieu.Trim()).OrderBy(x => x.Stt).ToList();
                    phieu.Detail = detail;
                    var Nhan9 = context.NhanToThu9.FirstOrDefault(x => x.MaPhieu == master.MaPhieu.Trim());
                    phieu.NhanToThu9 = Nhan9;
                    return View(phieu);
                }
                else
                {
                    phieu.Master = getMaster(ttdn);
                    ViewBag.thang = DateTime.Now.Month.ToString();
                    ViewBag.nam = DateTime.Now.Year.ToString();
                    return View(phieu);
                }
            }

            return View(phieu);
        }
        [HttpPost("/loadTable1-1")]
        public IActionResult LoadTable(string ms)
        {
            ViewBag.MaSoThue1 = ms.Substring(0, 10);
            ViewBag.MaSoThue2 = ms.Substring(10);
            return PartialView("_Table1_1");
        }

        [HttpPost("/form1_1")]
        public IActionResult InsertForm1_1(PhieuDieuTra phieu, IFormCollection form, int loai, string thangDuTinh, string thangThucHien, string nam)
        {
            List<DanhSachNhanToAnhHuong> list = new List<DanhSachNhanToAnhHuong>();
            for (int i = 0; i < 8; i++)
            {
                string name = "DanhSachNhanToAnhHuong_" + i;
                string value = form[name];
                if (value != null)
                {
                    list.Add(new DanhSachNhanToAnhHuong(value));
                }

            }
            phieu.DanhSachNhanToAnhHuong = list;
            phieu.Master.NgayTao = DateTime.Now;
            phieu.Master.Nam = nam;
            phieu.Master.ThangThucHien = thangThucHien;
            phieu.Master.ThangDuTinh = thangDuTinh;
            phieu.Master.MaSoThue2 = phieu.Master.MaSoThue.Substring(10);
            phieu.Master.MaSoThue = phieu.Master.MaSoThue.Substring(0, 10);
            phieu.Master.MaCoSo = "00000";
            if (phieu.Master.MaPhieu != null)
            {
                context.Master.Update(phieu.Master);
                context.SaveChanges();
                if (phieu.NhanToThu9 != null)
                {
                    context.Database.ExecuteSqlRaw("delete from NhanToThu9 where MaPhieu = {0}", phieu.Master.MaPhieu);
                    context.Database.ExecuteSqlRaw("insert into NhanToThu9 values({0},{1})", phieu.Master.MaPhieu, phieu.NhanToThu9.NoiDung);
                }
                context.Database.ExecuteSqlRaw("delete from DanhSachNhanToAnhHuong where MaPhieu = {0}", phieu.Master.MaPhieu);
                foreach (DanhSachNhanToAnhHuong ds in phieu.DanhSachNhanToAnhHuong)
                {
                    context.Database.ExecuteSqlRaw("insert into DanhSachNhanToAnhHuong values({0},{1})", phieu.Master.MaPhieu, ds.MaAh);
                }
                context.Database.ExecuteSqlRaw("delete from Detail where MaPhieu = {0}", phieu.Master.MaPhieu);
                foreach (Detail dt in phieu.Detail)
                {
                    context.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Detail]([MaPhieu],[STT],[TenCT],[DVT],[THThangTruoc],[DuTinh],[TongCongDon]) values({0},{1},{2},{3},{4},{5},{6})", phieu.Master.MaPhieu, dt.Stt, dt.TenCt, dt.Dvt, dt.ThthangTruoc, dt.DuTinh, dt.TongCongDon);
                }
            }
            else
            {
                while (true)
                {
                    string maPhieu = RandomString(5);
                    if (context.Master.Find(maPhieu) == null)
                    {
                        phieu.Master.MaPhieu = maPhieu;
                        break;
                    }
                }
                context.Master.Add(phieu.Master);
                context.SaveChanges();
                if (phieu.NhanToThu9 != null)
                {
                    context.Database.ExecuteSqlRaw("insert into NhanToThu9 values({0},{1})", phieu.Master.MaPhieu, phieu.NhanToThu9.NoiDung);
                }
                foreach (DanhSachNhanToAnhHuong ds in phieu.DanhSachNhanToAnhHuong)
                {
                    context.Database.ExecuteSqlRaw("insert into DanhSachNhanToAnhHuong values({0},{1})", phieu.Master.MaPhieu, ds.MaAh);
                }
                foreach (Detail dt in phieu.Detail)
                {
                    context.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Detail]([MaPhieu],[STT],[TenCT],[DVT],[THThangTruoc],[DuTinh],[TongCongDon]) values({0},{1},{2},{3},{4},{5},{6})", phieu.Master.MaPhieu, dt.Stt, dt.TenCt, dt.Dvt, dt.ThthangTruoc, dt.DuTinh, dt.TongCongDon);
                }
            }
            TempData["ThongBao"] = "Thành công!";
            if (loai == 1)
            {
                var fullView = new HtmlToPdf();
                fullView.Options.WebPageWidth = 1280;
                fullView.Options.PdfPageSize = PdfPageSize.A4;
                fullView.Options.MarginTop = 40;
                fullView.Options.MarginBottom = 40;
                fullView.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

                var pdf = fullView.ConvertUrl("https://localhost:" + HttpContext.Request.Host.Port.ToString() + "/form1_1PDF/" + phieu.Master.MaPhieu);

                var pdfBytes = pdf.Save();
                return File(pdfBytes, "application/pdf", phieu.Master.MaPhieu + ".pdf");
            }
            if (loai == 2)
            {

            }
            if (loai == 3)
            {

            }
            return RedirectToAction("Index");
        }
        [HttpPost("/form1_1/get-tong")]
        public decimal? getTongDauNam(string mast, string stt, decimal thangTrc, decimal duTinh, string thangDuTinh, string nam)
        {
            if (mast == null) { return thangTrc + duTinh; }
            string mast1 = mast.Substring(0, 10);
            string mast2 = mast.Substring(10);
            SqlParameter[] parameters =
                    {
                        new SqlParameter("@MaSoThue",mast1),
                        new SqlParameter("@MaSoThue2",mast2),
                        new SqlParameter("@MaCoSo","00000"),
                        new SqlParameter("@nam",nam),
                        new SqlParameter("@Stt",stt),
                        new SqlParameter("@thangDuTinh",thangDuTinh),
                    };
            var a = context.Detail.FromSqlRaw("exec TongCongDon @MaSoThue, @MaSoThue2, @MaCoSo, @nam, @Stt, @thangDuTinh", parameters.ToArray()).ToList().FirstOrDefault();
            if (a.ThthangTruoc != null)
                return a.ThthangTruoc + thangTrc + duTinh;
            else
            {
                return thangTrc + duTinh;
            }
        }
        [Route("/form1_1PDF/{maPhieu}")]
        public IActionResult Form1_1PDF(string maPhieu)
        {
            PhieuDieuTra phieu = new PhieuDieuTra();
            ViewBag.LoaiPhieu = "Form1_1";
            phieu = GetPhieu(maPhieu);
            ViewBag.thang = phieu.Master.ThangDuTinh;
            ViewBag.nam = phieu.Master.Nam;

            return View(phieu);
        }

        [HttpPost("/changePhieu1_1")]
        public IActionResult changePhieu1_1(string thang, string nam, string mst1, string mst2)
        {
            PhieuDieuTra phieu = new PhieuDieuTra();
            ThongTinDoanhNghiep ttdn;
            ViewBag.LoaiPhieu = "Form1_1";
            if (User.IsInRole("001"))
            {
                if (mst1.Equals("undefined"))
                {
                    ViewBag.thang = thang;
                    ViewBag.nam = nam;
                    return null;
                }
                ttdn = context.ThongTinDoanhNghiep.FirstOrDefault(x => x.MaSoThue == mst1 && x.MaSoThue2 == mst2);
            }
            else
            {
                ttdn = context.ThongTinDoanhNghiep.FirstOrDefault(x => x.TaiKhoan == User.Identity.Name);
            }

            var master = context.Master.FirstOrDefault(x => x.MaSoThue == ttdn.MaSoThue && x.MaSoThue2 == ttdn.MaSoThue2 && x.ThangDuTinh == thang && x.Nam == nam);
            phieu.Master = master;
            if (master != null)
            {
                ViewBag.thang = master.ThangDuTinh;
                ViewBag.nam = master.Nam;
                var ntah = context.DanhSachNhanToAnhHuong.Where(x => x.MaPhieu == master.MaPhieu.Trim()).ToList();
                phieu.DanhSachNhanToAnhHuong = ntah;
                var detail = context.Detail.Where(x => x.MaPhieu == master.MaPhieu.Trim()).OrderBy(x => x.Stt).ToList();
                phieu.Detail = detail;
                var Nhan9 = context.NhanToThu9.FirstOrDefault(x => x.MaPhieu == master.MaPhieu.Trim());
                phieu.NhanToThu9 = Nhan9;
                return PartialView(phieu);
            }
            else
            {
                ViewBag.thang = thang;
                ViewBag.nam = nam;
                phieu.Master = getMaster(ttdn);
                return PartialView(phieu);
            }
        }
        public PhieuDieuTra GetPhieu(string maPhieu)
        {
            Master master = context.Master.Find(maPhieu);
            PhieuDieuTra phieu = new PhieuDieuTra();
            phieu.Master = master;
            phieu.NhanToThu9 = context.NhanToThu9.FromSqlRaw("select*from NhanToThu9 where MaPhieu = '" + maPhieu + "'").ToList().FirstOrDefault();
            phieu.DanhSachNhanToAnhHuong = context.DanhSachNhanToAnhHuong.FromSqlRaw("select*from DanhSachNhanToAnhHuong where MaPhieu = '" + maPhieu + "'").ToList();
            phieu.Detail = context.Detail.FromSqlRaw("select*from Detail where MaPhieu = '" + maPhieu + "'").ToList();
            return phieu;
        }
        [HttpPost("/ThemDong")]
        public IActionResult ThemDong(string mast, int stt)
        {
            ViewBag.stt = stt;
            ViewBag.mast1 = mast.Substring(0, 10);
            ViewBag.mast2 = mast.Substring(10);
            return PartialView();
        }
        public List<NganhKinhDoanh> GetNganhKinhDoanhs(string MaSoThue, string MaSoThue2)
        {
            return
            context.NganhKinhDoanh.FromSqlRaw("select*from NganhKinhDoanh where MaSoThue ='" + MaSoThue + "' and MaSoThue2='" + MaSoThue2 + "'").ToList();
        }
        string getTinh(string ma)
        {
            return context.TinhTp.Find(ma).Name;
        }
        string getHuyen(string ma)
        {
            return context.QuanHuyen.Find(ma).Name;
        }
        string getXa(string ma)
        {
            return context.PhuongXa.Find(ma).Name;
        }
        NganhHoatDongKinhDoanh getNganhHoatDongKinhDoanh(string ma)
        {
            return context.NganhHoatDongKinhDoanh.Find(ma);
        }
        NhanToAnhHuong nhanToAnhHuong(string ma)
        {
            return context.NhanToAnhHuong.Find(ma);
        }
        private Master getMaster(ThongTinDoanhNghiep tt)
        {
            Master master = new Master();
            master.Ten = tt.TenDoanhNghiep;
            master.MaSoThue = tt.MaSoThue;
            master.MaSoThue2 = tt.MaSoThue2;
            master.MaTinhTp = tt.MaTinhTp;
            master.MaQuanHuyen = tt.MaQuanHuyen;
            master.MaPhuongXa = tt.MaPhuongXa;
            master.DiaChiCuThe = tt.DiaChiCuThe;
            master.Sdt = tt.SdtdoanhNghiep;
            master.Email = tt.Email;
            master.MaLh = tt.MaLh;
            return master;
        }
    }
}
