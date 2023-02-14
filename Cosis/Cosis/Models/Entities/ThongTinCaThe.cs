using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Cosis.Models.Entities
{
    public partial class ThongTinCaThe
    {
        public ThongTinCaThe()
        {
            Master = new HashSet<Master>();
        }

        public string MaSoThue { get; set; }
        public string MaSoThue2 { get; set; }
        public string MaCoSo { get; set; }
        public string TenCoSo { get; set; }
        public string MaTinhTp { get; set; }
        public string MaQuanHuyen { get; set; }
        public string MaPhuongXa { get; set; }
        public string DiaChiCuThe { get; set; }
        public string SdtcoSo { get; set; }
        public string Email { get; set; }
        public string MaLh { get; set; }
        public string TaiKhoan { get; set; }
        public string MaLoaiPhieu { get; set; }

        public virtual LoaiHinhKinhte MaLhNavigation { get; set; }
        public virtual LoaiPhieu MaLoaiPhieuNavigation { get; set; }
        public virtual PhuongXa MaPhuongXaNavigation { get; set; }
        public virtual QuanHuyen MaQuanHuyenNavigation { get; set; }
        public virtual TinhTp MaTinhTpNavigation { get; set; }
        public virtual TaiKhoang TaiKhoanNavigation { get; set; }
        public virtual ICollection<Master> Master { get; set; }
    }
}
