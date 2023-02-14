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
    public class Form2_1Controller : Controller
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public IActionResult Index()
        {
            FormCosisContext context = new FormCosisContext();
            PhieuDieuTra phieu = new PhieuDieuTra();
            ViewBag.LoaiPhieu = "Form2_1";
            if (User.IsInRole("001"))
            {
                ViewBag.thang = DateTime.Now.Month.ToString();
                ViewBag.nam = DateTime.Now.Year.ToString();
                ViewBag.ShowDC = "1";
                return View(phieu);
            }
            // if (User.IsInRole("002"))
            // {
            //     var ttdn = context.ThongTinDoanhNghiep.FirstOrDefault(x => x.TaiKhoan == User.Identity.Name);
            //     var master = context.Master.FirstOrDefault(x => x.MaSoThue == ttdn.MaSoThue && x.MaSoThue2 == ttdn.MaSoThue2 && x.ThangDuTinh == DateTime.Now.Month.ToString() && x.Nam == DateTime.Now.Year.ToString());
            //     if (master != null)
            //     {
            //         ViewBag.thang = master.ThangDuTinh;
            //         ViewBag.nam = master.Nam;
            //         phieu.Master = master;
            //         var ntah = context.DanhSachNhanToAnhHuong.Where(x => x.MaPhieu == master.MaPhieu.Trim()).ToList();
            //         phieu.DanhSachNhanToAnhHuong = ntah;
            //         var detail = context.Detail.Where(x => x.MaPhieu == master.MaPhieu.Trim()).ToList();
            //         phieu.Detail = detail;
            //         var Nhan9 = context.NhanToThu9.FirstOrDefault(x => x.MaPhieu == master.MaPhieu.Trim());
            //         phieu.NhanToThu9 = Nhan9;
            //         ViewBag.ShowDC = "2";
            //         return View(phieu);
            //     }
            //     else
            //     {
            //         phieu.Master = getMaster(ttdn);
            //         ViewBag.thang = DateTime.Now.Month.ToString();
            //         ViewBag.nam = DateTime.Now.Year.ToString();
            //         ViewBag.ShowDC = "2";
            //         return View(phieu);
            //     }
            // }
            if (User.IsInRole("003"))
            {
                var ttct = context.ThongTinCaThe.FirstOrDefault(x => x.TaiKhoan == User.Identity.Name);
                Master master = new Master();
                if (ttct.MaSoThue == null && ttct.MaSoThue2 == null && ttct.MaCoSo != null)
                {
                    master = context.Master.FirstOrDefault(x => x.MaCoSo == ttct.MaCoSo && x.ThangDuTinh == DateTime.Now.Month.ToString() && x.Nam == DateTime.Now.Year.ToString());
                }
                else if (ttct.MaSoThue != null && ttct.MaSoThue2 != null && ttct.MaCoSo != null)
                {
                    if (ttct.MaSoThue == null && ttct.MaSoThue2 == null)
                    {
                        master = context.Master.FirstOrDefault(x => x.MaCoSo == ttct.MaCoSo && x.ThangDuTinh == DateTime.Now.Month.ToString() && x.Nam == DateTime.Now.Year.ToString());
                    }
                    else
                    {
                        master = context.Master.FirstOrDefault(x => x.MaSoThue == ttct.MaSoThue && x.MaSoThue2 == ttct.MaSoThue2 && x.MaCoSo == ttct.MaCoSo && x.ThangDuTinh == DateTime.Now.Month.ToString() && x.Nam == DateTime.Now.Year.ToString());

                    }

                }
                if (master != null)
                {
                    ViewBag.thang = master.ThangDuTinh;
                    ViewBag.nam = master.Nam;
                    phieu.Master = master;
                    var ntah = context.DanhSachNhanToAnhHuong.Where(x => x.MaPhieu == master.MaPhieu.Trim()).ToList();
                    phieu.DanhSachNhanToAnhHuong = ntah;
                    // var detail = context.Detail.Where(x => x.MaPhieu == master.MaPhieu.Trim()).ToList();
                    var detail = context.Detail.FromSqlRaw("SELECT * FROM Detail WHERE MaPhieu = {0} Order by id ASC", master.MaPhieu).ToList();
                    phieu.Detail = detail;
                    var Nhan9 = context.NhanToThu9.FirstOrDefault(x => x.MaPhieu == master.MaPhieu.Trim());
                    phieu.NhanToThu9 = Nhan9;
                    ViewBag.ShowDC = "2";
                    return View(phieu);
                }
                else
                {
                    phieu.Master = getMasterCT(ttct);
                    ViewBag.thang = DateTime.Now.Month.ToString();
                    ViewBag.nam = DateTime.Now.Year.ToString();
                    ViewBag.ShowDC = "2";
                    return View(phieu);
                }
            }
            return View(phieu);
        }
        [HttpPost("/changePhieu2_1")]
        public IActionResult changePhieu(string thang, string nam, string mcs)
        {
            FormCosisContext context = new FormCosisContext();
            PhieuDieuTra phieu = new PhieuDieuTra();
            ThongTinCaThe ttct;
            Console.WriteLine("Ma Co So:" + mcs);
            ViewBag.LoaiPhieu = "Form2_1";
            if (User.IsInRole("001"))
            {
                if (mcs.Equals("undefined"))
                {
                    ViewBag.thang = thang;
                    ViewBag.nam = nam;
                    ViewBag.CheckDC = "1";
                    return PartialView(phieu);
                }
                ttct = context.ThongTinCaThe.FirstOrDefault(x => x.MaCoSo == mcs);
            }
            else
            {
                ttct = context.ThongTinCaThe.FirstOrDefault(x => x.TaiKhoan == User.Identity.Name);
            }

            var master = context.Master.FirstOrDefault(x => x.MaCoSo == ttct.MaCoSo && x.ThangDuTinh == thang && x.Nam == nam);
            phieu.Master = master;
            if (master != null)
            {
                ViewBag.thang = master.ThangDuTinh;
                ViewBag.nam = master.Nam;
                var ntah = context.DanhSachNhanToAnhHuong.Where(x => x.MaPhieu == master.MaPhieu.Trim()).ToList();
                phieu.DanhSachNhanToAnhHuong = ntah;
                // var detail = context.Detail.Where(x => x.MaPhieu == master.MaPhieu.Trim()).ToList();
                var detail = context.Detail.FromSqlRaw("SELECT * FROM Detail WHERE MaPhieu = {0} Order by id ASC", master.MaPhieu).ToList();
                phieu.Detail = detail;
                var Nhan9 = context.NhanToThu9.FirstOrDefault(x => x.MaPhieu == master.MaPhieu.Trim());
                phieu.NhanToThu9 = Nhan9;
                return PartialView(phieu);
            }
            else
            {
                ViewBag.thang = thang;
                ViewBag.nam = nam;
                phieu.Master = getMasterCT(ttct);
                return PartialView(phieu);
            }
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
        // Thong tin ca the
        private Master getMasterCT(ThongTinCaThe tt)
        {
            Master master = new Master();
            master.Ten = tt.TenCoSo;
            master.MaSoThue = tt.MaSoThue;
            master.MaSoThue2 = tt.MaSoThue2;
            master.MaTinhTp = tt.MaTinhTp;
            master.MaQuanHuyen = tt.MaQuanHuyen;
            master.MaPhuongXa = tt.MaPhuongXa;
            master.DiaChiCuThe = tt.DiaChiCuThe;
            master.Sdt = tt.SdtcoSo;
            master.Email = tt.Email;
            master.MaLh = tt.MaLh;
            master.MaCoSoNavigation = tt;
            return master;
        }

        [HttpPost("/form2_1")]
        public IActionResult InsertForm2_1(PhieuDieuTra phieu, IFormCollection form, int loai, string thangDuTinh, string thangThucHien, string nam)
        {
            FormCosisContext context = new FormCosisContext();
            List<DanhSachNhanToAnhHuong> list = new List<DanhSachNhanToAnhHuong>();
            for (int i = 0; i < 8; i++)
            {
                string name = "DanhSachNhanToAnhHuong" + i;
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
            Console.WriteLine(phieu.Master.MaCoSo);
            if (phieu.Master.MaSoThue != null)
            {
                phieu.Master.MaSoThue2 = phieu.Master.MaSoThue.Substring(10);
                phieu.Master.MaSoThue = phieu.Master.MaSoThue.Substring(0, 10);
                var checkMST = context.ThongTinDoanhNghiep.FromSqlRaw("select*from ThongTinDoanhNghiep where MaSoThue = {0} and MaSoThue2 = {1}", phieu.Master.MaSoThue, phieu.Master.MaSoThue2).FirstOrDefault();
                if (checkMST != null)
                {

                }
                else
                {
                    phieu.Master.MaSoThue = null;
                    phieu.Master.MaSoThue2 = null;
                }
            }
            //
            var check = context.Master.FromSqlRaw("select*from Master where MaSoThue = '" + phieu.Master.MaSoThue + "' and MaSoThue2 = '" + phieu.Master.MaSoThue2 + "' and MaCoSo = '" + phieu.Master.MaCoSo + "' and ThangThucHien = '" + phieu.Master.ThangThucHien + "' and ThangDuTinh = '" + phieu.Master.ThangDuTinh + "' and Nam = '" + phieu.Master.Nam + "'").ToList();
            if (phieu.Master.MaSoThue != null && phieu.Master.MaCoSo != null)
            {
                Console.WriteLine("Da vao 1");
            }
            else if (phieu.Master.MaSoThue != null && phieu.Master.MaCoSo == null)
            {
                Console.WriteLine("Da vao 2");
                check = context.Master.FromSqlRaw("select*from Master where MaSoThue = '" + phieu.Master.MaSoThue + "' and MaCoSo = '" + phieu.Master.MaCoSo + "' and ThangThucHien = '" + phieu.Master.ThangThucHien + "' and ThangDuTinh = '" + phieu.Master.ThangDuTinh + "' and Nam = '" + phieu.Master.Nam + "'").ToList();
            }
            else if (phieu.Master.MaCoSo != null && phieu.Master.MaSoThue == null)
            {
                Console.WriteLine("Da vao 2");
                check = context.Master.FromSqlRaw("select*from Master where MaCoSo = '" + phieu.Master.MaCoSo + "' and ThangThucHien = '" + phieu.Master.ThangThucHien + "' and ThangDuTinh = '" + phieu.Master.ThangDuTinh + "' and Nam = '" + phieu.Master.Nam + "'").ToList();
            }
            //
            if (phieu.Master.MaPhieu != null)
            {
                context.Master.Update(phieu.Master);
                context.SaveChanges();
                if (phieu.NhanToThu9 == null)
                {
                    context.Database.ExecuteSqlRaw("delete from NhanToThu9 where MaPhieu = {0}", phieu.Master.MaPhieu);
                }
                else
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
                    if (dt.ThthangTruoc != null && dt.DuTinh != null)
                    {
                        context.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Detail]([MaPhieu],[STT],[TenCT],[DVT],[THThangTruoc],[DuTinh],[TongCongDon]) values({0},{1},{2},{3},{4},{5},{6})", phieu.Master.MaPhieu, dt.Stt, dt.TenCt, dt.Dvt, dt.ThthangTruoc, dt.DuTinh, dt.TongCongDon);
                    }
                    else if (dt.ThthangTruoc == null && dt.DuTinh == null)
                    {
                        string cutSTT = dt.Stt.Substring(0, dt.Stt.IndexOf("_"));
                        if (cutSTT == "3." || (cutSTT == "3.1." && dt.ThthangTruoc != null && dt.DuTinh != null))
                        {
                            context.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Detail]([MaPhieu],[STT],[TenCT],[DVT],[THThangTruoc],[DuTinh],[TongCongDon]) values({0},{1},{2},{3},{4},{5},{6})", phieu.Master.MaPhieu, dt.Stt, dt.TenCt, dt.Dvt, dt.ThthangTruoc, dt.DuTinh, dt.TongCongDon);
                        }
                    }
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
                    // Console.WriteLine(phieu.NhanToThu9.NoiDung);
                }
                foreach (DanhSachNhanToAnhHuong ds in phieu.DanhSachNhanToAnhHuong)
                {
                    // Console.WriteLine(ds);
                    context.Database.ExecuteSqlRaw("insert into DanhSachNhanToAnhHuong values({0},{1})", phieu.Master.MaPhieu, ds.MaAh);
                }
                foreach (Detail dt in phieu.Detail)
                {
                    if (dt.ThthangTruoc != null && dt.DuTinh != null)
                    {
                        context.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Detail]([MaPhieu],[STT],[TenCT],[DVT],[THThangTruoc],[DuTinh],[TongCongDon]) values({0},{1},{2},{3},{4},{5},{6})", phieu.Master.MaPhieu, dt.Stt, dt.TenCt, dt.Dvt, dt.ThthangTruoc, dt.DuTinh, dt.TongCongDon);
                    }
                    else if (dt.ThthangTruoc == null && dt.DuTinh == null)
                    {
                        string cutSTT = dt.Stt.Substring(0, dt.Stt.IndexOf("_"));
                        if (cutSTT == "3." || (cutSTT == "3.1." && dt.ThthangTruoc != null && dt.DuTinh != null))
                        {
                            context.Database.ExecuteSqlRaw("INSERT INTO [dbo].[Detail]([MaPhieu],[STT],[TenCT],[DVT],[THThangTruoc],[DuTinh],[TongCongDon]) values({0},{1},{2},{3},{4},{5},{6})", phieu.Master.MaPhieu, dt.Stt, dt.TenCt, dt.Dvt, dt.ThthangTruoc, dt.DuTinh, dt.TongCongDon);
                        }
                    }
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
                var pdf = fullView.ConvertUrl("https://localhost:" + HttpContext.Request.Host.Port.ToString() + "/form2_1PDF/" + phieu.Master.MaPhieu);
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

        [HttpPost("/get-tong2")]
        public decimal? getTongDauNam(string mast, string stt, decimal thangTrc, decimal duTinh, string mcs)
        {
            if (mast == null) { return thangTrc + duTinh; }
            string mast1 = mast.Substring(0, 10);
            string mast2 = mast.Substring(10);
            string thangDuTinh = DateTime.Now.Month.ToString();
            string nam = DateTime.Now.Year.ToString();
            //if (mcs == null) { mcs = "00000"; }
            FormCosisContext context = new FormCosisContext();
            SqlParameter[] parameters =
                    {
                        new SqlParameter("@MaSoThue",mast1),
                        new SqlParameter("@MaSoThue2",mast2),
                        new SqlParameter("@MaCoSo",mcs),
                        new SqlParameter("@nam",nam),
                        new SqlParameter("@Stt",stt),
                        new SqlParameter("@thangDuTinh",thangDuTinh)
                    };
            var a = context.Detail.FromSqlRaw("exec TongCongDon @MaSoThue, @MaSoThue2, @MaCoSo, @nam, @Stt,@thangDuTinh", parameters.ToArray()).ToList().FirstOrDefault();
            if (a.ThthangTruoc != null)
                return a.ThthangTruoc + thangTrc + duTinh;
            else
            {
                return thangTrc + duTinh;
            }
        }
        [Route("/In/{maPhieu}")]
        public IActionResult In(string maPhieu)
        {
            var fullView = new HtmlToPdf();
            fullView.Options.WebPageWidth = 1280;
            fullView.Options.PdfPageSize = PdfPageSize.A4;
            fullView.Options.MarginTop = 40;
            fullView.Options.MarginBottom = 40;
            fullView.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            var pdf = fullView.ConvertUrl("https://localhost:" + HttpContext.Request.Host.Port.ToString() + "/form2_1PDF/" + maPhieu);
            var pdfBytes = pdf.Save();
            return File(pdfBytes, "application/pdf", maPhieu + ".pdf");
        }

        [Route("/form2_1PDF/{maPhieu}")]
        public IActionResult Form2_1PDF(string maPhieu)
        {
            var context = new FormCosisContext();
            // maPhieu = "  ";
            Master master = context.Master.Find(maPhieu);
            PhieuDieuTra phieu = new PhieuDieuTra();
            phieu.Master = master;
            if (master.MaCoSo == null)
            {
                List<NganhKinhDoanh> listNganh =
                context.NganhKinhDoanh.FromSqlRaw("select*from NganhKinhDoanh where MaSoThue ='" + master.MaSoThue + "' and MaSoThue2='" + master.MaSoThue2 + "'").ToList();
                ViewBag.NganhKinhDoanh = listNganh;
            }
            else
            {
                List<NganhKinhDoanh> listNganh =
           context.NganhKinhDoanh.FromSqlRaw("select*from NganhKinhDoanh where MaCoSo ='" + master.MaCoSo + "'").ToList();
                ViewBag.NganhKinhDoanh = listNganh;
            }
            var nt9 = context.NhanToThu9.FromSqlRaw("select*from NhanToThu9 where MaPhieu = '" + maPhieu + "'").ToList().FirstOrDefault();
            if (nt9 == null)
            {
                ViewBag.checkNT9 = "False";
            }
            else
            {
                phieu.NhanToThu9 = nt9;
            }
            phieu.DanhSachNhanToAnhHuong = context.DanhSachNhanToAnhHuong.FromSqlRaw("select*from DanhSachNhanToAnhHuong where MaPhieu = '" + maPhieu + "'").ToList();
            phieu.Detail = context.Detail.FromSqlRaw("SELECT * FROM Detail WHERE MaPhieu = {0} Order by id ASC", maPhieu).ToList();
            return View(phieu);
        }
        // add home controller
        [HttpPost("/loadMaster2")]
        public IActionResult loadInfor(string mp, string mast, string macs)
        {
            FormCosisContext context = new FormCosisContext();
            if (mp != null && mp != "undefined")
            {
                var b = context.Master.FirstOrDefault(x => x.MaPhieu == mp);
                return PartialView("_Master2_1", b);
            }
            else
            {
                if (User.IsInRole("002"))
                {
                    var ttdn = context.ThongTinDoanhNghiep.FirstOrDefault(x => x.TaiKhoan == User.Identity.Name);
                    Master b = getMaster(ttdn);
                    return PartialView("_Master2_1", b);
                }
                else if (User.IsInRole("003"))
                {
                    var b = context.ThongTinCaThe.FirstOrDefault(x => x.TaiKhoan == User.Identity.Name);
                    ViewBag.CheckMCS = 0;
                    return PartialView("_Master2_1CN", b);
                }
            }
            //
            if (mast != null)
            {
                string mst1 = mast.Substring(0, 10);
                string mst2 = mast.Substring(10);
                var a = context.ThongTinDoanhNghiep.FromSqlRaw("select*from ThongTinDoanhNghiep where MaSoThue = {0} and MaSoThue2 = {1}", mst1, mst2).FirstOrDefault();
                ViewBag.CheckMCS = 0;
                return PartialView("_Master2_1", a);
            }
            else
            {
                ThongTinDoanhNghiep a = new ThongTinDoanhNghiep();
                return PartialView("_Master2_1", a);
            }

        }

        [HttpPost("/loadMaster2CN")]
        public IActionResult loadInforPersonal(string ms)
        {
            FormCosisContext context = new FormCosisContext();
            var a = context.ThongTinCaThe.FromSqlRaw("select*from ThongTinCaThe where MaCoSo = {0}", ms).FirstOrDefault();
            ViewBag.CheckMCS = 0;
            return PartialView("_Master2_1CN", a);
        }

        [HttpPost("/loaddata_21B")]
        public IActionResult loaddata_21B(string mcs, string MST)
        {
            Console.WriteLine("MS:" + MST + "length:" + MST.Length);
            FormCosisContext context = new FormCosisContext();
            var a = context.NganhKinhDoanh.ToList();
            if (MST.Length == 13)
            {
                string mst1 = mcs.Substring(0, 10);
                string mst2 = mcs.Substring(10);
                a = context.NganhKinhDoanh.FromSqlRaw("select*from NganhKinhDoanh where MaSoThue = {0} and MaSoThue2 = {1}", mst1, mst2).ToList();
            }
            else
            {
                a = context.NganhKinhDoanh.FromSqlRaw("select*from NganhKinhDoanh where MaCoSo = {0}", mcs).ToList();
            }
            // Console.WriteLine(a.ToString());
            ViewBag.ListNghanh = a;
            return PartialView("_2_1B");
        }
        [HttpPost("/loaddata_14B")]
        public IActionResult loaddata_14B(string ms)
        {
            Console.WriteLine("MS:" + ms + "length:" + ms.Length);
            FormCosisContext context = new FormCosisContext();
            var a = context.NganhKinhDoanh.ToList();
            string mst1 = ms.Substring(0, 10);
            string mst2 = ms.Substring(10);
            a = context.NganhKinhDoanh.FromSqlRaw("select*from NganhKinhDoanh where MaSoThue = {0} and MaSoThue2 = {1}", mst1, mst2).ToList();

            // Console.WriteLine(a.ToString());
            ViewBag.ListNghanh = a;
            return PartialView("_1_4B");
        }
    }
}
