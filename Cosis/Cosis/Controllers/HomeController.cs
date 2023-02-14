using Cosis.Models;
using Cosis.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Cosis.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string result)
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("/loadMaster")]
        public IActionResult loadDiaChi(string ms)
        {
            FormCosisContext context = new FormCosisContext();
            string mst1 = ms.Substring(0, 10);
            string mst2 = ms.Substring(10);
            var a = context.ThongTinDoanhNghiep.FromSqlRaw("select*from ThongTinDoanhNghiep where MaSoThue = {0} and MaSoThue2 = {1}",mst1,mst2).FirstOrDefault();
            return PartialView("_Master",a);
        }
        [HttpPost("/loadMaster1")]
        public IActionResult loadMaster(string ms,string mast)
        {
            FormCosisContext context = new FormCosisContext();
            if (ms != null)
            {
                var a = context.Master.FirstOrDefault(x => x.MaPhieu == ms);
                return PartialView("_Master1_3", a);
            }
            else
            {
                if (User.IsInRole("002"))
                {
                    var ttdn = context.ThongTinDoanhNghiep.FirstOrDefault(x => x.TaiKhoan == User.Identity.Name);
                    Master a = getMaster(ttdn);
                    return PartialView("_Master1_3", a);
                }
                else
                {
                    string mst1 = mast.Substring(0, 10);
                    string mst2 = mast.Substring(10);
                    var ttdn = context.ThongTinDoanhNghiep.FirstOrDefault(x => x.MaSoThue == mst1 && x.MaSoThue2 == mst2);
                    Master a = getMaster(ttdn);
                    return PartialView("_Master1_3", a);
                }
            }
        }
        [HttpPost("/get-tableTTDN")]
        public IActionResult loadTableTTDN(string thang, string nam, string loaiPhieu)
        {
            ViewBag.thang = thang;
            ViewBag.nam = nam;
            ViewBag.LoaiPhieu = loaiPhieu;
            return PartialView("_tableTTDoanhNghiep");
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
        [HttpGet("/HuongDan")]
        public IActionResult ViewHuongDan()
        {
            return View("HuongDan");
        }
        [HttpPost("/loaddata_2_3")]
        public IActionResult loaddata_2_3(string ms)
        {
            Console.WriteLine("MS:" + ms + "length:" + ms.Length);
            FormCosisContext context = new FormCosisContext();
            var a = context.NganhKinhDoanh.ToList();
            if (ms.Length == 13)
            {
                string mst1 = ms.Substring(0, 10);
                string mst2 = ms.Substring(10);
                a = context.NganhKinhDoanh.FromSqlRaw("select*from NganhKinhDoanh where MaSoThue = {0} and MaSoThue2 = {1}", mst1, mst2).ToList();
            }
            else
            {
                a = context.NganhKinhDoanh.FromSqlRaw("select*from NganhKinhDoanh where MaCoSo = {0}", ms).ToList();
            }
            Console.WriteLine(a.ToString());
            ViewBag.ListNghanh = a;
            return PartialView("_2_3");
        }
        [HttpPost("/loadMaster2CN")]
        public IActionResult loadInforPersonal(string ms)
        {
            FormCosisContext context = new FormCosisContext();
            // var b = context.Master.FromSqlRaw("select*from Master where MaSoThue = {0} and MaSoThue2 = {1}", mst1, mst2).FirstOrDefault();
            // Console.WriteLine(a1);
            var a = context.ThongTinCaThe.FromSqlRaw("select*from ThongTinCaThe where MaCoSo = {0}", ms).FirstOrDefault();
            ViewBag.CheckMCS = 0;
            return PartialView("_Master2_1CN", a);
        }
        private Master getMasterCN(ThongTinCaThe tt)
        {
            Master master = new Master();
            master.Ten = tt.TenCoSo;
            master.MaCoSo = tt.MaCoSo;
            master.MaSoThue = tt.MaSoThue;
            master.MaSoThue2 = tt.MaSoThue2;
            master.MaTinhTp = tt.MaTinhTp;
            master.MaQuanHuyen = tt.MaQuanHuyen;
            master.MaPhuongXa = tt.MaPhuongXa;
            master.DiaChiCuThe = tt.DiaChiCuThe;
            master.Sdt = tt.SdtcoSo;
            master.Email = tt.Email;
            master.MaLh = tt.MaLh;
            return master;
        }
        [HttpPost("/get-tableTTCT")]
        public IActionResult loadTableTTCT(string thang, string nam, string loaiPhieu)
        {
            ViewBag.thang = thang;
            ViewBag.nam = nam;
            ViewBag.LoaiPhieu = loaiPhieu;
            return PartialView("_tableTTCaThe");
        }
        [HttpPost("/loadMaster2_3")]
        public IActionResult loadMaster2_3(string ms, string mcs)
        {
            FormCosisContext context = new FormCosisContext();
            if (ms != null)
            {
                var a = context.Master.FirstOrDefault(x => x.MaPhieu == ms);
                return PartialView("_Master2_3", a);
            }
            else
            {
                if (User.IsInRole("003"))
                {
                    var ttdn = context.ThongTinCaThe.FirstOrDefault(x => x.TaiKhoan == User.Identity.Name);
                    Master a = getMasterCN(ttdn);
                    return PartialView("_Master2_3", a);
                }
                else
                {

                    var ttdn = context.ThongTinCaThe.FirstOrDefault(x => x.MaCoSo == mcs);
                    Master a = getMasterCN(ttdn);
                    return PartialView("_Master2_3", a);
                }
            }
        }
    }
}
