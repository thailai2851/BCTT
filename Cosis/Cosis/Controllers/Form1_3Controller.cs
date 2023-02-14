using Cosis.Models;
using Cosis.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace Cosis.Controllers
{
    public class Form1_3Controller : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private static Random random = new Random();
        public Form1_3Controller(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

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
            ViewBag.LoaiPhieu = "Form1_3";
            if (User.IsInRole("001"))
            {
                ViewBag.thang = DateTime.Now.Month.ToString();
                ViewBag.nam = DateTime.Now.Year.ToString();
                return View(phieu);
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
                    var detail = context.Detail.Where(x => x.MaPhieu == master.MaPhieu.Trim()).ToList();
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
        [HttpPost("/changePhieu1_3")]
        public IActionResult changePhieu(string thang, string nam, string mst1, string mst2)
        {

            FormCosisContext context = new FormCosisContext();
            PhieuDieuTra phieu = new PhieuDieuTra();
            ThongTinDoanhNghiep ttdn;
            ViewBag.LoaiPhieu = "Form1_3";
            if (User.IsInRole("001"))
            {
                if (mst1.Equals("undefined"))
                {
                    ViewBag.thang = thang;
                    ViewBag.nam = nam;
                    return PartialView(phieu);
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
                var detail = context.Detail.Where(x => x.MaPhieu == master.MaPhieu.Trim()).ToList();
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
        [HttpPost("/form1_3")]
        public IActionResult InsertForm1_3(PhieuDieuTra phieu, IFormCollection form, int loai, string thangDuTinh, string thangThucHien, string nam)
        {
            FormCosisContext context = new FormCosisContext();

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

                var pdf = fullView.ConvertUrl("https://localhost:" + HttpContext.Request.Host.Port.ToString() + "/form1_3PDF/" + phieu.Master.MaPhieu);

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
        [HttpPost("/get-tong")]
        public decimal? getTongDauNam(string mast, string stt, decimal thangTrc, decimal duTinh, string thangDuTinh, string nam)
        {
            if (mast == null) { return thangTrc + duTinh; }
            string mast1 = mast.Substring(0, 10);
            string mast2 = mast.Substring(10);
            FormCosisContext context = new FormCosisContext();
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
        [Route("/form1_3PDF/{maPhieu}")]
        public IActionResult Form1_3PDF(string maPhieu)
        {
            FormCosisContext context = new FormCosisContext();
            PhieuDieuTra phieu = new PhieuDieuTra();
            ViewBag.LoaiPhieu = "Form1_3";
            phieu = GetPhieu(maPhieu);
            ViewBag.thang = phieu.Master.ThangDuTinh;
            ViewBag.nam = phieu.Master.Nam;

            return View(phieu);
        }
        [Route("Export")]
        public IActionResult Export()
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ĐIỀU TRA HOẠT ĐỘNG THƯƠNG MẠI VÀ DỊCH VỤ");
                /*                using (var cells = worksheet.Cells[1, 1, 1, 4])
                                {
                                    cells.Style.Font.Bold = true;
                                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                }
                                //First add the headers
                                worksheet.Cells[1, 1].Value = "ID";
                                worksheet.Cells[1, 2].Value = "Name";
                                worksheet.Cells[1, 3].Value = "Gender";
                                worksheet.Cells[1, 4].Value = "Salary (in $)";
                                //Add values
                                worksheet.Cells["A2"].Value = "00000";
                                worksheet.Cells["B2"].Value = "Jon";
                                worksheet.Cells["C2"].Value = "M";
                                worksheet.Cells["D2"].Value = 5000;

                                worksheet.Cells["A3"].Value = 1001;
                                worksheet.Cells["B3"].Value = "Graham";
                                worksheet.Cells["C3"].Value = "M";
                                worksheet.Cells["D3"].Value = 10000;

                                worksheet.Cells["A4"].Value = 1002;
                                worksheet.Cells["B4"].Value = "Jenny";
                                worksheet.Cells["C4"].Value = "F";
                                worksheet.Cells["D4"].Value = 5000;

                                worksheet.Cells[5, 1].Value = "ID";
                                worksheet.Cells[5, 2].Value = "Name";
                                worksheet.Cells[5, 3].Value = "Gender";
                                worksheet.Cells[5, 4].Value = "Salary (in $)";*/

                PhieuDieuTra phieu = GetPhieu("ZKJ8D");
                FormCosisContext context = new FormCosisContext();
                //row 1
                var tenPhieu = worksheet.Cells["A1:K1"];
                tenPhieu.Style.Font.Bold = true;
                tenPhieu.Style.Font.Size = 13;
                tenPhieu.Merge = true;
                tenPhieu.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                tenPhieu.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                tenPhieu.Value = "ĐIỀU TRA HOẠT ĐỘNG THƯƠNG MẠI VÀ DỊCH VỤ";

                //row 2
                worksheet.Cells["A2:K2"].Merge = true;

                //row 3
                worksheet.Cells["A3:K3"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["B3"].Value = "Phiếu 1.3/DN-VT-T";
                worksheet.Cells["B3"].Style.Font.Bold = true;
                worksheet.Cells["G3"].Value = "Mã số thuế";
                worksheet.Cells["I3"].Value = phieu.Master.MaSoThue;
                worksheet.Cells["K3"].Value = phieu.Master.MaSoThue2;

                //row 4
                worksheet.Cells["A4:K4"].Merge = true;

                //row 5
                worksheet.Cells["A5:K5"].Merge = true;
                worksheet.Cells["A5:K5"].Value = "PHIẾU THU THẬP THÔNG TIN ĐỐI VỚI DOANH NGHIỆP/HỢP TÁC XÃ";
                worksheet.Cells["A5:K5"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["A5:K5"].Style.Font.Size = 13;
                worksheet.Cells["A5:K5"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A5:K5"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet.Cells["A5:K5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A5:K5"].Style.Font.Bold = true;

                //row 6
                worksheet.Cells["A6:K6"].Merge = true;
                worksheet.Cells["A6:K6"].Value = "(Áp dụng đối với doanh nghiệp/chi nhánh doanh nghiệp, hợp tác xã có hoạt động vận tải, kho bãi)";
                worksheet.Cells["A6:K6"].Style.Font.Bold = true;
                worksheet.Cells["A6:K6"].Style.Font.Italic = true;
                worksheet.Cells["A6:K6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells["A6:K6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //row 7
                worksheet.Cells["A7:K7"].Merge = true;

                //row 8
                worksheet.Cells["A8:K8"].Merge = true;
                worksheet.Cells["A8:K8"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A8:K8"].Value = "Tháng " + phieu.Master.NgayTao.Value.Month + " Năm " + phieu.Master.NgayTao.Value.Year;
                //row 9
                worksheet.Cells["A9:K9"].Merge = true;

                //row 10
                worksheet.Cells["A10:K10"].Merge = true;
                worksheet.Cells["A10:K10"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A10:K10"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet.Cells["A10:K10"].Style.Font.Size = 13;
                worksheet.Cells["A10:K10"].Style.Font.Bold = true;
                worksheet.Cells["A10:K10"].Value = "A. THÔNG TIN CHUNG";
                worksheet.Cells["A10:K10"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                //row 11
                worksheet.Cells["A11:B11"].Merge = true;
                worksheet.Cells["C11:K11"].Merge = true;
                worksheet.Cells["A11:B11"].Value = "Tên doanh nghiệp/HTX:";
                worksheet.Cells["C11:K11"].Value = phieu.Master.Ten;

                //row 12
                worksheet.Cells["A12:B12"].Merge = true;
                worksheet.Cells["A12:B12"].Merge = true;
                worksheet.Cells["A12:B12"].Value = "Địa chỉ doanh nghiệp";

                //row 13
                worksheet.Cells["B13:C13"].Merge = true;
                worksheet.Cells["D13:K13"].Merge = true;
                worksheet.Cells["B13:C13"].Value = "Tỉnh/TP trực thuộc TW:";
                worksheet.Cells["D13:K13"].Value = getTinh(phieu.Master.MaTinhTp);

                //row 14
                worksheet.Cells["B14:C14"].Merge = true;
                worksheet.Cells["D14:K14"].Merge = true;
                worksheet.Cells["B14:C14"].Value = "Huyện/quận (thị xã, TP thuộc tỉnh):";
                worksheet.Cells["D14:K14"].Value = getHuyen(phieu.Master.MaQuanHuyen);

                //row 15
                worksheet.Cells["B15:C15"].Merge = true;
                worksheet.Cells["D15:K15"].Merge = true;
                worksheet.Cells["B15:C15"].Value = "Xã/phường/thị trấn:";
                worksheet.Cells["D15:K15"].Value = getXa(phieu.Master.MaPhuongXa);

                //row 16
                worksheet.Cells["B16:C16"].Merge = true;
                worksheet.Cells["D16:K16"].Merge = true;
                worksheet.Cells["B16:C16"].Value = "Thôn, ấp(số nhà, đường phố):";
                worksheet.Cells["D16:K16"].Value = phieu.Master.DiaChiCuThe;

                //ROW 17
                worksheet.Cells["A17:B17"].Merge = true;
                worksheet.Cells["C17:K17"].Merge = true;
                worksheet.Cells["A17:B17"].Value = "Số điện thoại:";
                worksheet.Cells["C17:K17"].Value = phieu.Master.Sdt;

                //row 18
                worksheet.Cells["A18:B18"].Merge = true;
                worksheet.Cells["C18:K18"].Merge = true;
                worksheet.Cells["A18:B18"].Value = "Email:";
                worksheet.Cells["C18:K18"].Value = phieu.Master.Email;

                //row 19
                worksheet.Cells["A19:B19"].Merge = true;
                worksheet.Cells["C19:K19"].Merge = true;
                worksheet.Cells["A19:B19"].Value = "Loại hình kinh tế:";
                worksheet.Cells["C19:K19"].Value = context.LoaiHinhKinhte.Find(phieu.Master.MaLh).TenLh;

                //row 20
                List<NganhKinhDoanh> lits = GetNganhKinhDoanhs(phieu.Master.MaSoThue, phieu.Master.MaSoThue2);
                int SLNganh = lits.Count;
                worksheet.Cells["A20:B20"].Value = "Ngành hoạt động kinh doanh";
                for (int i = 1; i <= SLNganh; i++)
                {
                    int r = 19 + i;

                    worksheet.Cells["A" + r + ":B" + r].Merge = true;

                    worksheet.Cells["C" + r + ":D" + r].Merge = true;
                    worksheet.Cells["C" + r + ":D" + r].Value = lits[i - 1].MaNganh;

                    worksheet.Cells["E" + r + ":F" + r].Merge = true;
                    worksheet.Cells["E" + r + ":F" + r].Value = "Tên ngành HĐKD:";

                    worksheet.Cells["G" + r + ":K" + r].Merge = true;
                    worksheet.Cells["G" + r + ":K" + r].Value = getNganhHoatDongKinhDoanh(lits[i - 1].MaNganh).TenNganh;
                }
                //next row
                int cr = 20 + SLNganh;
                var s = worksheet.Cells["A" + cr + ":K" + cr];
                s.Merge = true;
                s.Style.Fill.PatternType = ExcelFillStyle.Solid;
                s.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                s.Style.Font.Size = 13;
                s.Style.Font.Bold = true;
                s.Value = "B. KẾT QUẢ HOẠT ĐỘNG KINH DOANH";
                s.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                // next
                int crn = 21 + SLNganh;
                worksheet.Row(crn).Style.Font.Bold = true;
                worksheet.Row(crn).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A" + crn + ":C" + crn].Value = "Tên chỉ tiêu";
                worksheet.Cells["A" + crn + ":C" + crn].Merge = true;
                worksheet.Cells["D" + crn + ":E" + crn].Value = "Đơn vị tính";
                worksheet.Cells["D" + crn + ":E" + crn].Merge = true;
                worksheet.Cells["F" + crn + ":G" + crn].Value = "Tháng thực hiện " + phieu.Master.ThangThucHien + "/" + phieu.Master.Nam;
                worksheet.Cells["F" + crn + ":G" + crn].Merge = true;
                worksheet.Cells["H" + crn + ":I" + crn].Value = "Dự tính " + phieu.Master.ThangDuTinh + "/" + phieu.Master.Nam;
                worksheet.Cells["H" + crn + ":I" + crn].Merge = true;
                worksheet.Cells["J" + crn + ":K" + crn].Value = "CỘNG DỒN TỪ ĐẦU NĂM ĐẾN CUỐI THÁNG BÁO CÁO";
                worksheet.Cells["J" + crn + ":K" + crn].Merge = true;

                //next
                int cn = 22 + SLNganh;
                worksheet.Row(cn).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A" + cn + ":C" + cn].Value = "A";
                worksheet.Cells["A" + cn + ":C" + cn].Merge = true;
                worksheet.Cells["D" + cn + ":E" + cn].Value = "B";
                worksheet.Cells["D" + cn + ":E" + cn].Merge = true;
                worksheet.Cells["F" + cn + ":G" + cn].Value = "1";
                worksheet.Cells["F" + cn + ":G" + cn].Merge = true;
                worksheet.Cells["H" + cn + ":I" + cn].Value = "2";
                worksheet.Cells["H" + cn + ":I" + cn].Merge = true;
                worksheet.Cells["J" + cn + ":K" + cn].Value = "3";
                worksheet.Cells["J" + cn + ":K" + cn].Merge = true;

                /*worksheet.Cells[FromRow, FromColumn, ToRow, ToColumn].Merge = true;*/
                return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "tet.xlsx");
            }
        }

        public PhieuDieuTra GetPhieu(string maPhieu)
        {
            FormCosisContext context = new FormCosisContext();
            Master master = context.Master.Find(maPhieu);
            PhieuDieuTra phieu = new PhieuDieuTra();
            phieu.Master = master;
            phieu.NhanToThu9 = context.NhanToThu9.FromSqlRaw("select*from NhanToThu9 where MaPhieu = '" + maPhieu + "'").ToList().FirstOrDefault();
            phieu.DanhSachNhanToAnhHuong = context.DanhSachNhanToAnhHuong.FromSqlRaw("select*from DanhSachNhanToAnhHuong where MaPhieu = '" + maPhieu + "'").ToList();
            phieu.Detail = context.Detail.FromSqlRaw("select*from Detail where MaPhieu = '" + maPhieu + "'").ToList();
            return phieu;
        }

        public List<NganhKinhDoanh> GetNganhKinhDoanhs(string MaSoThue, string MaSoThue2)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            FormCosisContext context = new FormCosisContext();
            return
            context.NganhKinhDoanh.FromSqlRaw("select*from NganhKinhDoanh where MaSoThue ='" + MaSoThue + "' and MaSoThue2='" + MaSoThue2 + "'").ToList();
        }
        string toStringMonth(int i)
        {
            if (i.ToString().Length > 1) return i.ToString();
            else return "0" + i;
        }
        string getTinh(string ma)
        {
            FormCosisContext context = new FormCosisContext();
            return context.TinhTp.Find(ma).Name;
        }
        string getHuyen(string ma)
        {
            FormCosisContext context = new FormCosisContext();
            return context.QuanHuyen.Find(ma).Name;
        }
        string getXa(string ma)
        {
            FormCosisContext context = new FormCosisContext();
            return context.PhuongXa.Find(ma).Name;
        }
        NganhHoatDongKinhDoanh getNganhHoatDongKinhDoanh(string ma)
        {
            FormCosisContext context = new FormCosisContext();
            return context.NganhHoatDongKinhDoanh.Find(ma);
        }
        NhanToAnhHuong nhanToAnhHuong(string ma)
        {
            FormCosisContext context = new FormCosisContext();
            return context.NhanToAnhHuong.Find(ma);
        }
    }
}
