using Cosis.Models;
using Cosis.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Cosis.Controllers
{
    public class ThongKeController : Controller
    {
        public IActionResult Index()
        {
            return View("ThongKe");
        }

        [HttpPost("/openList")]
        public IActionResult openList(string thang, string nam, string loaiPhieu, string city, string district, string ward)
        {
            ViewBag.thang = thang;
            ViewBag.nam = nam;
            ViewBag.loaiPhieu = loaiPhieu;
            FormCosisContext context = new FormCosisContext();
            ViewBag.city = city;
            ViewBag.district = district;
            ViewBag.ward = ward;

            return PartialView();
        }

        [Route("/downloadExcel")]
        public IActionResult downloadExcel(string thang, string nam, string maLoaiPhieu, string maT, string maH, string maX)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                FormCosisContext context = new FormCosisContext();

                string maLoaiPhieuDN = "Form1" + maLoaiPhieu;
                string maLoaiPhieuCS = "Form2" + maLoaiPhieu;

                string IDDiaChi;
                string name;
                getName();
                void getName()
                {
                    TinhTp t = context.TinhTp.Find(ViewBag.city);
                    PhuongXa px = context.PhuongXa.Find(ViewBag.ward);
                    QuanHuyen h = context.QuanHuyen.Find(ViewBag.district);
                    if (px == null)
                    {
                        if (h == null)
                        {
                            if (t == null)
                            {
                                name = "Tất cả";
                                IDDiaChi = "0";
                            }
                            else
                            {
                                name = t.Name;
                                IDDiaChi = t.Id;
                            }
                        }
                        else
                        {
                            name = h.Name;
                            IDDiaChi = h.Id;
                        }
                    }
                    else
                    {
                        name = px.Name;
                        IDDiaChi = px.Id;
                    }
                }

                List<Tinh> getListDiaChi()
                {
                    using (System.IO.StreamReader r = new System.IO.StreamReader("C:\\Users\\Admin\\Documents\\Cosis\\Cosis\\Cosis\\wwwroot\\assets\\DiaChi.json"))
                    {
                        string json = r.ReadToEnd();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tinh>>(json);
                    }
                }
                List<Tinh> listDiaChi = getListDiaChi();


                string format(int a)
                {
                    return a.ToString("N", System.Globalization.CultureInfo.InvariantCulture);
                }
                float getTiLe(int tong, int b)
                {
                    if (tong == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return ((float)b / (float)tong) * 100;
                    }
                }
                //----------------------------------------------------------------------
                int getTongDN()
                {
                    if (maX == null || maX.Equals("0"))
                    {
                        if (maH == null || maH.Equals("0"))
                        {
                            if (maT == null || maT.Equals("0"))
                            {
                                if (maLoaiPhieu.Equals("0"))
                                {
                                    return context.ThongTinDoanhNghiep.ToList().Count();
                                }
                                else
                                {
                                    return context.ThongTinDoanhNghiep.Where(x => x.MaLoaiPhieu == maLoaiPhieuDN).ToList().Count();
                                }
                            }
                            else
                            {
                                if (maLoaiPhieu.Equals("0"))
                                {
                                    return context.ThongTinDoanhNghiep.Where(x => x.MaTinhTp == maT).ToList().Count();
                                }
                                else
                                {
                                    return context.ThongTinDoanhNghiep.Where(x => x.MaTinhTp == maT && x.MaLoaiPhieu == maLoaiPhieuDN).ToList().Count();
                                }
                            }
                        }
                        else
                        {
                            if (maLoaiPhieu.Equals("0"))
                            {
                                return context.ThongTinDoanhNghiep.Where(x => x.MaQuanHuyen == maH).ToList().Count();
                            }
                            else
                            {
                                return context.ThongTinDoanhNghiep.Where(x => x.MaQuanHuyen == maH && x.MaLoaiPhieu == maLoaiPhieuDN).ToList().Count();
                            }
                        }
                    }
                    else
                    {
                        if (maLoaiPhieu.Equals("0"))
                        {
                            return context.ThongTinDoanhNghiep.Where(x => x.MaPhuongXa == maX).ToList().Count();
                        }
                        else
                        {
                            return context.ThongTinDoanhNghiep.Where(x => x.MaPhuongXa == maX && x.MaLoaiPhieu == maLoaiPhieuDN).ToList().Count();
                        }
                    }
                }
                int getTongDNDL(string maTinh, string maHuyen, string maXa)
                {
                    if (maXa == null || maXa.Equals("0"))
                    {
                        if (maHuyen == null || maHuyen.Equals("0"))
                        {
                            if (maTinh == null || maTinh.Equals("0"))
                            {
                                if (maLoaiPhieu.Equals("0"))
                                {
                                    return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinDoanhNghiep t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 where ThangDuTinh = '" + thang + "' and nam = '" + nam + "'").ToList().Count();
                                }
                                else
                                {
                                    return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinDoanhNghiep t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and MaLoaiPhieu = '" + maLoaiPhieuDN + "'").ToList().Count();
                                }
                            }
                            else
                            {
                                if (maLoaiPhieu.Equals("0"))
                                {
                                    return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinDoanhNghiep t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and m.MaTinhTP = '" + maTinh + "'").ToList().Count();
                                }
                                else
                                {
                                    return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinDoanhNghiep t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and MaLoaiPhieu = '" + maLoaiPhieuDN + "' and m.MaTinhTP = '" + maTinh + "'").ToList().Count();
                                }
                            }
                        }
                        else
                        {
                            if (maLoaiPhieu.Equals("0"))
                            {
                                return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinDoanhNghiep t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and m.MaQuanHuyen = '" + maHuyen + "'").ToList().Count();
                            }
                            else
                            {
                                return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinDoanhNghiep t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and MaLoaiPhieu = '" + maLoaiPhieuDN + "' and m.MaQuanHuyen = '" + maHuyen + "'").ToList().Count();
                            }
                        }
                    }
                    else
                    {
                        if (maLoaiPhieu.Equals("0"))
                        {
                            return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinDoanhNghiep t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and m.MaPhuongXa = '" + maXa + "'").ToList().Count();
                        }
                        else
                        {
                            return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinDoanhNghiep t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and MaLoaiPhieu = '" + maLoaiPhieuDN + "'and m.MaPhuongXa = '" + maXa + "'").ToList().Count();
                        }
                    }
                }
                int tongDN = getTongDN();
                int tongDNDL = getTongDNDL(maT, maH, maX);

                //Cơ sở --------------------------------------------------------------
                int getTongCS()
                {
                    if (maX == null || maX.Equals("0"))
                    {
                        if (maH == null || maH.Equals("0"))
                        {
                            if (maT == null || maT.Equals("0"))
                            {
                                if (maLoaiPhieu.Equals("0"))
                                {
                                    return context.ThongTinCaThe.ToList().Count();
                                }
                                else
                                {
                                    return context.ThongTinCaThe.Where(x => x.MaLoaiPhieu == maLoaiPhieuCS).ToList().Count();
                                }
                            }
                            else
                            {
                                if (maLoaiPhieu.Equals("0"))
                                {
                                    return context.ThongTinCaThe.Where(x => x.MaTinhTp == maT).ToList().Count();
                                }
                                else
                                {
                                    return context.ThongTinCaThe.Where(x => x.MaTinhTp == maT && x.MaLoaiPhieu == maLoaiPhieuCS).ToList().Count();
                                }
                            }
                        }
                        else
                        {
                            if (maLoaiPhieu.Equals("0"))
                            {
                                return context.ThongTinCaThe.Where(x => x.MaQuanHuyen == maH).ToList().Count();
                            }
                            else
                            {
                                return context.ThongTinCaThe.Where(x => x.MaQuanHuyen == maH && x.MaLoaiPhieu == maLoaiPhieuCS).ToList().Count();
                            }
                        }
                    }
                    else
                    {
                        if (maLoaiPhieu.Equals("0"))
                        {
                            return context.ThongTinCaThe.Where(x => x.MaPhuongXa == maX).ToList().Count();
                        }
                        else
                        {
                            return context.ThongTinCaThe.Where(x => x.MaPhuongXa == maX && x.MaLoaiPhieu == maLoaiPhieuCS).ToList().Count();
                        }
                    }
                }
                int getTongCSDL(string maTinh, string maHuyen, string maXa)
                {
                    if (maXa == null || maXa.Equals("0"))
                    {
                        if (maHuyen == null || maHuyen.Equals("0"))
                        {
                            if (maTinh == null || maTinh.Equals("0"))
                            {
                                if (maLoaiPhieu.Equals("0"))
                                {
                                    return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinCaThe t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 and t.MaCoSo = m.MaCoSo where ThangDuTinh = '" + thang + "' and nam = '" + nam + "'").ToList().Count();
                                }
                                else
                                {
                                    return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinCaThe t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 and t.MaCoSo = m.MaCoSo where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and MaLoaiPhieu = '" + maLoaiPhieuCS + "'").ToList().Count();
                                }
                            }
                            else
                            {
                                if (maLoaiPhieu.Equals("0"))
                                {
                                    return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinCaThe t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 and t.MaCoSo = m.MaCoSo where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and m.MaTinhTP = '" + maTinh + "'").ToList().Count();
                                }
                                else
                                {
                                    return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinCaThe t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 and t.MaCoSo = m.MaCoSo where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and MaLoaiPhieu = '" + maLoaiPhieuCS + "' and m.MaTinhTP = '" + maTinh + "'").ToList().Count();
                                }
                            }
                        }
                        else
                        {
                            if (maLoaiPhieu.Equals("0"))
                            {
                                return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinCaThe t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 and t.MaCoSo = m.MaCoSo where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and m.MaQuanHuyen = '" + maHuyen + "'").ToList().Count();
                            }
                            else
                            {
                                return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinCaThe t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 and t.MaCoSo = m.MaCoSo where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and MaLoaiPhieu = '" + maLoaiPhieuCS + "' and m.MaQuanHuyen = '" + maHuyen + "'").ToList().Count();
                            }
                        }
                    }
                    else
                    {
                        if (maLoaiPhieu.Equals("0"))
                        {
                            return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinCaThe t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 and t.MaCoSo = m.MaCoSo where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and m.MaPhuongXa = '" + maXa + "'").ToList().Count();
                        }
                        else
                        {
                            return context.Master.FromSqlRaw("select m.[MaSoThue],m.[MaSoThue2],m.[MaCoSo],m.[Ten],m.[MaTinhTP],m.[MaQuanHuyen],m.[MaPhuongXa],m.[DiaChiCuThe],m.[SDT],m.[Email],m.[MaLH],m.[MaPhieu],m.[NgayTao],m.[ThangThucHien],m.[ThangDuTinh],m.[TenNguoiTraLoi],m.[SDTNguoiTraLoi],m.[Nam] from Master m inner join ThongTinCaThe t on t.MaSoThue = m.MaSoThue and t.MaSoThue2 = m.MaSoThue2 and t.MaCoSo = m.MaCoSo where ThangDuTinh = '" + thang + "' and nam = '" + nam + "' and MaLoaiPhieu = '" + maLoaiPhieuCS + "'and m.MaPhuongXa = '" + maXa + "'").ToList().Count();
                        }
                    }
                }
                int tongCS = getTongCS();
                int tongCSDL = getTongCSDL(maT, maH, maX);


                //----------------------------------------------------------------------
                int getTongDNT(string maTinh)
                {
                    if (maLoaiPhieu.Equals("0"))
                    {
                        return context.ThongTinDoanhNghiep.Where(x => x.MaTinhTp == maTinh).ToList().Count();
                    }
                    else
                    {
                        return context.ThongTinDoanhNghiep.Where(x => x.MaTinhTp == maTinh && x.MaLoaiPhieu == maLoaiPhieuDN).ToList().Count();
                    }
                }
                int getTongDNH(string maHuyen)
                {
                    if (maLoaiPhieu.Equals("0"))
                    {
                        return context.ThongTinDoanhNghiep.Where(x => x.MaQuanHuyen == maHuyen).ToList().Count();
                    }
                    else
                    {
                        return context.ThongTinDoanhNghiep.Where(x => x.MaQuanHuyen == maHuyen && x.MaLoaiPhieu == maLoaiPhieuDN).ToList().Count();
                    }
                }
                int getTongDNX(string maXa)
                {
                    if (maLoaiPhieu.Equals("0"))
                    {
                        return context.ThongTinDoanhNghiep.Where(x => x.MaPhuongXa == maXa).ToList().Count();
                    }
                    else
                    {
                        return context.ThongTinDoanhNghiep.Where(x => x.MaPhuongXa == maXa && x.MaLoaiPhieu == maLoaiPhieuDN).ToList().Count();
                    }
                }

                int getTongCST(string maTinh)
                {
                    if (maLoaiPhieu.Equals("0"))
                    {
                        return context.ThongTinCaThe.Where(x => x.MaTinhTp == maTinh).ToList().Count();
                    }
                    else
                    {
                        return context.ThongTinCaThe.Where(x => x.MaTinhTp == maTinh && x.MaLoaiPhieu == maLoaiPhieuCS).ToList().Count();
                    }
                }
                int getTongCSH(string maHuyen)
                {
                    if (maLoaiPhieu.Equals("0"))
                    {
                        return context.ThongTinCaThe.Where(x => x.MaQuanHuyen == maHuyen).ToList().Count();
                    }
                    else
                    {
                        return context.ThongTinCaThe.Where(x => x.MaQuanHuyen == maHuyen && x.MaLoaiPhieu == maLoaiPhieuCS).ToList().Count();
                    }
                }
                int getTongCSX(string maXa)
                {
                    if (maLoaiPhieu.Equals("0"))
                    {
                        return context.ThongTinCaThe.Where(x => x.MaPhuongXa == maXa).ToList().Count();
                    }
                    else
                    {
                        return context.ThongTinCaThe.Where(x => x.MaPhuongXa == maXa && x.MaLoaiPhieu == maLoaiPhieuCS).ToList().Count();
                    }
                }

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Thống kê tiến độ");
                //r 1
                var tenPhieu = worksheet.Cells["A1:L1"];
                tenPhieu.Style.Font.Bold = true;
                tenPhieu.Style.Font.Size = 13;
                tenPhieu.Merge = true;
                tenPhieu.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                tenPhieu.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                tenPhieu.Value = "THỐNG KÊ TIẾN ĐỘ ĐIỀU TRA SẢN XUẤT KINH DOANH";

                //r 2
                worksheet.Cells["A2:L2"].Merge = true;

                //r 3
                worksheet.Cells["A3:L3"].Merge = true;
                worksheet.Cells["A3:L3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A3:L3"].Value = "Tháng " + thang + " Năm " + nam;
                worksheet.Cells["A3:L3"].Style.Font.Bold = true;

                //r 4
                worksheet.Cells["A4:B4"].Merge = true;
                worksheet.Cells["A4:B4"].Value = "1. Loại phiếu:";
                worksheet.Cells["A4:B4"].Style.Font.Bold = true;
                worksheet.Cells["C4:D4"].Merge = true;
                if (maLoaiPhieu == "0")
                {
                    worksheet.Cells["C4:D4"].Value = "Tất cả";
                }
                else
                {
                    worksheet.Cells["C4:D4"].Value = context.LoaiPhieu.Find(maLoaiPhieuDN).TenLoaiPhieu;
                }

                //r 5
                worksheet.Cells["A5:L5"].Merge = true;

                //r 6
                worksheet.Cells["A6:D6"].Merge = true;
                worksheet.Cells["A6:D6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["E6:H6"].Merge = true;
                worksheet.Cells["E6:H6"].Value = "Doanh nghiệp";
                worksheet.Cells["E6:H6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["I6:L6"].Merge = true;
                worksheet.Cells["I6:L6"].Value = "Cá thể";
                worksheet.Cells["I6:L6"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[6, 1, 6, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[6, 1, 6, 12].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet.Cells["A6:L6"].Style.Font.Bold = true;
                worksheet.Cells["A6:L6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //r 7
                worksheet.Cells["A7"].Value = "STT";
                worksheet.Cells["A7"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["B7"].Value = "Mã";
                worksheet.Cells["B7"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["C7:D7"].Merge = true;
                worksheet.Cells["C7:D7"].Value = "Tên";
                worksheet.Cells["C7:D7"].Style.WrapText = true;
                worksheet.Cells["C7:D7"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["E7"].Value = "Tổng";
                worksheet.Cells["E7"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["F7"].Value = "Đã làm";
                worksheet.Cells["F7"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["G7"].Value = "Chưa làm";
                worksheet.Cells["G7"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["H7"].Value = "Tỉ lệ hoàn thành";
                worksheet.Cells["H7"].Style.WrapText = true;
                worksheet.Cells["H7"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["I7"].Value = "Tổng";
                worksheet.Cells["I7"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["J7"].Value = "Đã làm";
                worksheet.Cells["J7"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["K7"].Value = "Chưa làm";
                worksheet.Cells["K7"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["L7"].Value = "Tỉ lệ hoàn thành";
                worksheet.Cells["L7"].Style.WrapText = true;
                worksheet.Cells["L7"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells[7, 1, 7, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[7, 1, 7, 12].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet.Cells["A7:L7"].Style.Font.Bold = true;
                worksheet.Cells["A7:L7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //r 8
                worksheet.Cells["A8"].Value = 1;
                worksheet.Cells["A8"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["B8"].Value = IDDiaChi;
                worksheet.Cells["B8"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["C8:D8"].Merge = true;
                worksheet.Cells["C8:D8"].Value = name;
                worksheet.Cells["C8:D8"].Style.WrapText = true;
                worksheet.Cells["C8:D8"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["E8"].Value = (tongDN);
                worksheet.Cells["E8"].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells["E8"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["F8"].Value = (tongDNDL);
                worksheet.Cells["F8"].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells["F8"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["G8"].Value = (tongDN - tongDNDL);
                worksheet.Cells["G8"].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells["G8"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["H8"].Value = getTiLe(tongDN, tongDNDL);
                worksheet.Cells["H8"].Style.Numberformat.Format = "0.00";
                worksheet.Cells["H8"].Style.WrapText = true;
                worksheet.Cells["H8"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["I8"].Value = (tongCS);
                worksheet.Cells["I8"].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells["I8"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["J8"].Value = (tongCSDL);
                worksheet.Cells["J8"].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells["J8"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["K8"].Value = (tongCS - tongCSDL);
                worksheet.Cells["K8"].Style.Numberformat.Format = "#,##0.00";
                worksheet.Cells["K8"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                worksheet.Cells["L8"].Value = getTiLe(tongCS, tongCSDL);
                worksheet.Cells["L8"].Style.WrapText = true;
                worksheet.Cells["L8"].Style.Numberformat.Format = "0.00";
                worksheet.Cells["L8"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells[8, 1, 8, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[8, 1, 8, 12].Style.Fill.BackgroundColor.SetColor(Color.Aqua);


                //foreach
                if (maX.Equals("0"))
                {
                    if (maT.Equals("0"))
                    {
                        for (int i = 0; i < listDiaChi.Count(); i++)
                        {
                            int a = getTongDNT(listDiaChi[i].Id);
                            int b = getTongDNDL(listDiaChi[i].Id, null, null);
                            int c = getTongCST(listDiaChi[i].Id);
                            int d = getTongCSDL(listDiaChi[i].Id, null, null);
                            int r = i + 9;

                            worksheet.Cells["A" + r].Value = i + 2;
                            worksheet.Cells["A" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                            worksheet.Cells["B" + r].Value = listDiaChi[i].Id;
                            worksheet.Cells["B" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                            var rowten = worksheet.Cells["C" + r + ":D" + r];
                            rowten.Merge = true;
                            rowten.Value = listDiaChi[i].Name;
                            rowten.Style.WrapText = true;
                            rowten.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                            worksheet.Cells["E" + r].Value = a;
                            worksheet.Cells["E" + r].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells["E" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                            worksheet.Cells["F" + r].Value = b;
                            worksheet.Cells["F" + r].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells["F" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                            worksheet.Cells["G" + r].Value = (a - b);
                            worksheet.Cells["G" + r].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells["G" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                            worksheet.Cells["H" + r].Value = getTiLe(a, b);
                            worksheet.Cells["H" + r].Style.Numberformat.Format = "0.00";
                            worksheet.Cells["H" + r].Style.WrapText = true;
                            worksheet.Cells["H" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                            worksheet.Cells["I" + r].Value = c;
                            worksheet.Cells["I" + r].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells["I" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                            worksheet.Cells["J" + r].Value = d;
                            worksheet.Cells["J" + r].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells["J" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                            worksheet.Cells["K" + r].Value = (c - d);
                            worksheet.Cells["K" + r].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells["K" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                            worksheet.Cells["L" + r].Value = getTiLe(c, d);
                            worksheet.Cells["L" + r].Style.WrapText = true;
                            worksheet.Cells["L" + r].Style.Numberformat.Format = "0.00";
                            worksheet.Cells["L" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    else
                    {
                        if (maH.Equals("0"))
                        {
                            List<Districts> listH = listDiaChi.FirstOrDefault(x => x.Id == maT).Districts;
                            for (int i = 0; i < listH.Count(); i++)
                            {
                                int a = getTongDNH(listH[i].Id);
                                int b = getTongDNDL(maT, listH[i].Id, null);
                                int c = getTongCSH(listH[i].Id);
                                int d = getTongCSDL(maT, listH[i].Id, null);

                                int r = i + 9;

                                worksheet.Cells["A" + r].Value = i + 2;
                                worksheet.Cells["A" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["B" + r].Value = listH[i].Id;
                                worksheet.Cells["B" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                var rowten = worksheet.Cells["C" + r + ":D" + r];
                                rowten.Merge = true;
                                rowten.Value = listH[i].Name;
                                rowten.Style.WrapText = true;
                                rowten.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["E" + r].Value = a;
                                worksheet.Cells["E" + r].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells["E" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["F" + r].Value = b;
                                worksheet.Cells["F" + r].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells["F" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["G" + r].Value = (a - b);
                                worksheet.Cells["G" + r].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells["G" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["H" + r].Value = getTiLe(a, b);
                                worksheet.Cells["H" + r].Style.Numberformat.Format = "0.00";
                                worksheet.Cells["H" + r].Style.WrapText = true;
                                worksheet.Cells["H" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["I" + r].Value = c;
                                worksheet.Cells["I" + r].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells["I" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["J" + r].Value = d;
                                worksheet.Cells["J" + r].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells["J" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["K" + r].Value = (c - d);
                                worksheet.Cells["K" + r].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells["K" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["L" + r].Value = getTiLe(c, d);
                                worksheet.Cells["L" + r].Style.WrapText = true;
                                worksheet.Cells["L" + r].Style.Numberformat.Format = "0.00";
                                worksheet.Cells["L" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            }
                        }
                        else
                        {
                            List<Wards> listX = listDiaChi.FirstOrDefault(x => x.Id == maT).Districts.FirstOrDefault(x => x.Id == maH).Wards;
                            for (int i = 0; i < listX.Count(); i++)
                            {
                                int a = getTongDNX(listX[i].Id);
                                int b = getTongDNDL(maT, maH, listX[i].Id);
                                int c = getTongCSX(listX[i].Id);
                                int d = getTongCSDL(maT, maH, listX[i].Id);
                                int r = i + 9;
                                worksheet.Cells["A" + r].Value = i + 2;
                                worksheet.Cells["A" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["B" + r].Value = listX[i].Id;
                                worksheet.Cells["B" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                var rowten = worksheet.Cells["C" + r + ":D" + r];
                                rowten.Merge = true;
                                rowten.Value = listX[i].Name;
                                rowten.Style.WrapText = true;
                                rowten.Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["E" + r].Value = a;
                                worksheet.Cells["E" + r].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells["E" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["F" + r].Value = b;
                                worksheet.Cells["F" + r].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells["F" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["G" + r].Value = (a - b);
                                worksheet.Cells["G" + r].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells["G" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["H" + r].Value = getTiLe(a, b);
                                worksheet.Cells["H" + r].Style.Numberformat.Format = "0.00";
                                worksheet.Cells["H" + r].Style.WrapText = true;
                                worksheet.Cells["H" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["I" + r].Value = c;
                                worksheet.Cells["I" + r].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells["I" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["J" + r].Value = d;
                                worksheet.Cells["J" + r].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells["J" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["K" + r].Value = (c - d);
                                worksheet.Cells["K" + r].Style.Numberformat.Format = "#,##0.00";
                                worksheet.Cells["K" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                                worksheet.Cells["L" + r].Value = getTiLe(c, d);
                                worksheet.Cells["L" + r].Style.WrapText = true;
                                worksheet.Cells["L" + r].Style.Numberformat.Format = "0.00";
                                worksheet.Cells["L" + r].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            }
                        }
                    }
                }
                return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ThongKeTienDo.xlsx");
            }
        }
    }
}
