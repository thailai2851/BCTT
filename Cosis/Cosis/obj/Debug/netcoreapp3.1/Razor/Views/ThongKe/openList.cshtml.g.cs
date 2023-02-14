#pragma checksum "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f2951d3cd61e555ae930a312bec81a9035e5a8af"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ThongKe_openList), @"mvc.1.0.view", @"/Views/ThongKe/openList.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\_ViewImports.cshtml"
using Cosis;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\_ViewImports.cshtml"
using Cosis.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\_ViewImports.cshtml"
using Cosis.Models.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\_ViewImports.cshtml"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f2951d3cd61e555ae930a312bec81a9035e5a8af", @"/Views/ThongKe/openList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"330bfff8c7c5f9dba2758406de113abfa6a13415", @"/Views/_ViewImports.cshtml")]
    public class Views_ThongKe_openList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
  
    FormCosisContext context = new FormCosisContext();
    string maX = ViewBag.ward;
    string maH = ViewBag.district;
    string maT = ViewBag.city;
    string nam = ViewBag.nam;
    string thang = ViewBag.thang;
    string maLoaiPhieu = ViewBag.loaiPhieu;
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
        if(px==null){
            if (h == null){
                if (t==null)
                {
                    name = "Tất cả";
                    IDDiaChi = "0";
                }else{
                    name = t.Name;
                    IDDiaChi = t.Id;
                }
            }else{
                name = h.Name;
                IDDiaChi = h.Id;
            }
        }
        else{
            name = px.Name;
            IDDiaChi = px.Id;
        }
    }

    List<Tinh> getListDiaChi(){
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
    int getTongDN(){
        if (maX == null || maX.Equals("0"))
        {
            if (maH == null || maH.Equals("0"))
            {
                if (maT==null|| maT.Equals("0"))
                {
                    if (maLoaiPhieu.Equals("0"))
                    {
                        return context.ThongTinDoanhNghiep.ToList().Count();
                    }
                    else
                    {
                        return context.ThongTinDoanhNghiep.Where(x => x.MaLoaiPhieu == maLoaiPhieuDN).ToList().Count();
                    }
                }else{
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
        }else
        {
            if (maLoaiPhieu.Equals("0"))
            {
                return context.ThongTinDoanhNghiep.Where(x => x.MaPhuongXa == maX).ToList().Count();
            }else{
                return context.ThongTinDoanhNghiep.Where(x => x.MaPhuongXa == maX && x.MaLoaiPhieu == maLoaiPhieuDN).ToList().Count();
            }
        }
    }
    int getTongDNDL(string maTinh, string maHuyen, string maXa)
    {
        if (maXa==null || maXa.Equals("0"))
        {
            if (maHuyen==null|| maHuyen.Equals("0"))
            {
                if (maTinh==null || maTinh.Equals("0"))
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
        if (maX==null || maX.Equals("0"))
        {
            if (maH == null || maH.Equals("0"))
            {
                if (maT ==null || maT.Equals("0"))
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
        if (maXa==null || maXa.Equals("0"))
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
    int getTongDNT(string maTinh){
        if (maLoaiPhieu.Equals("0"))
        {
            return context.ThongTinDoanhNghiep.Where(x => x.MaTinhTp == maTinh).ToList().Count();
        }
        else
        {
            return context.ThongTinDoanhNghiep.Where(x => x.MaTinhTp == maTinh && x.MaLoaiPhieu == maLoaiPhieuDN).ToList().Count();
        }
    }
    int getTongDNH(string maHuyen){
        if (maLoaiPhieu.Equals("0"))
        {
            return context.ThongTinDoanhNghiep.Where(x => x.MaQuanHuyen == maHuyen).ToList().Count();
        }
        else
        {
            return context.ThongTinDoanhNghiep.Where(x => x.MaQuanHuyen == maHuyen && x.MaLoaiPhieu == maLoaiPhieuDN).ToList().Count();
        }
    }
    int getTongDNX(string maXa){
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

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div id=""list"" class=""body-container"">
            <table class=""table-TK table table-bordered table-striped table-hover text-center"">
                <thead>
                    <tr>
                        <th colspan=""3""></th>
                        <th colspan=""4"">Doanh nghiệp</th>
                        <th colspan=""4"">Cơ sở</th>
                    </tr>
                    <tr>
                        <th>Stt</th>
                        <th>Mã</th>
                        <th>Tên</th>
                        <th>Tổng</th>
                        <th>Đã làm</th>
                        <th>Chưa làm</th>
                        <th>Tỉ lệ hoàn thành</th>
                        <th>Tổng</th>
                        <th>Đã làm</th>
                        <th>Chưa làm</th>
                        <th>Tỉ lệ hoàn thành</th>
                    </tr>
                </thead>
                <tbody>
                    <tr style=""background-color:#17a2b8"">
                       ");
            WriteLiteral(" <td>1</td>\r\n                        <td>");
#nullable restore
#line 375 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(IDDiaChi);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 376 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 377 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(format(tongDN));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 378 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(format(tongDNDL));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 379 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(format(tongDN-tongDNDL));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 380 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(getTiLe(tongDN,tongDNDL).ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("%</td>\r\n                        <td>");
#nullable restore
#line 381 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(format(tongCS));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 382 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(format(tongCSDL));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 383 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(format(tongCS-tongCSDL));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 384 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(getTiLe(tongCS,tongCSDL).ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("%</td>\r\n                    </tr>\r\n");
#nullable restore
#line 386 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
             if(maX.Equals("0")){
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 387 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
             if (maT.Equals("0"))
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 389 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                 for(int i =0; i < listDiaChi.Count(); i++)
                {
                    int a = getTongDNT(listDiaChi[i].Id);
                    int b = getTongDNDL(listDiaChi[i].Id, null, null);
                    int c = getTongCST(listDiaChi[i].Id);
                    int d = getTongCSDL(listDiaChi[i].Id, null, null);

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>");
#nullable restore
#line 396 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                        Write(i+2);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 397 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(listDiaChi[i].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 398 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(listDiaChi[i].Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 399 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(a);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 400 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(b);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 401 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                        Write(a-b);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 402 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(getTiLe(a,b).ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("%</td>\r\n                        <td>");
#nullable restore
#line 403 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(c);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 404 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(d);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 405 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                        Write(c-d);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 406 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                       Write(getTiLe(c,d).ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("%</td>\r\n                    </tr>\r\n");
#nullable restore
#line 408 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 408 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                 
            }else{
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 410 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                 if (maH.Equals("0"))
                {
                    List<Districts> listH = listDiaChi.FirstOrDefault(x => x.Id == maT).Districts;
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 413 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                     for (int i = 0; i < listH.Count(); i++)
                    {
                        int a = getTongDNH(listH[i].Id);
                        int b = getTongDNDL(maT, listH[i].Id, null);
                        int c = getTongCSH(listH[i].Id);
                        int d = getTongCSDL(maT, listH[i].Id, null);

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 420 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                            Write(i + 2);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 421 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(listH[i].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 422 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(listH[i].Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 423 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(a);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 424 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(b);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 425 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                            Write(a-b);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 426 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(getTiLe(a,b).ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("%</td>\r\n                            <td>");
#nullable restore
#line 427 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(c);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 428 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(d);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 429 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                            Write(c-d);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 430 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(getTiLe(c,d).ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("%</td>\r\n                        </tr>\r\n");
#nullable restore
#line 432 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 432 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                     
                }
                else{
                    List<Wards> listX = listDiaChi.FirstOrDefault(x => x.Id == maT).Districts.FirstOrDefault(x=>x.Id==maH).Wards;
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 436 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                     for (int i = 0; i < listX.Count(); i++)
                    {
                        int a = getTongDNX(listX[i].Id);
                        int b = getTongDNDL(maT, maH, listX[i].Id);
                        int c = getTongCSX(listX[i].Id);
                        int d = getTongCSDL(maT, maH, listX[i].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                           <td>");
#nullable restore
#line 443 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(i + 2);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                           <td>");
#nullable restore
#line 444 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                          Write(listX[i].Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                           <td> ");
#nullable restore
#line 445 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(listX[i].Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 446 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(a);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 447 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(b);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 448 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                            Write(a-b);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 449 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(getTiLe(a,b).ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("%</td>\r\n                            <td>");
#nullable restore
#line 450 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(c);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 451 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(d);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 452 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                            Write(c-d);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 453 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                           Write(getTiLe(c,d).ToString("0.00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("%</td>\r\n                        </tr>\r\n");
#nullable restore
#line 455 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 455 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 456 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
                 
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 457 "C:\Users\Admin\Documents\Cosis\Cosis\Cosis\Views\ThongKe\openList.cshtml"
             
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n            </table>\r\n        </div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
