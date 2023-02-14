using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Cosis.Models.Entities
{
    public partial class Master
    {
        public string MaSoThue { get; set; }
        public string MaSoThue2 { get; set; }
        public string MaCoSo { get; set; }
        public string Ten { get; set; }
        public string MaTinhTp { get; set; }
        public string MaQuanHuyen { get; set; }
        public string MaPhuongXa { get; set; }
        public string DiaChiCuThe { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string MaLh { get; set; }
        public string MaPhieu { get; set; }
        public DateTime? NgayTao { get; set; }
        public string ThangThucHien { get; set; }
        public string ThangDuTinh { get; set; }
        public string TenNguoiTraLoi { get; set; }
        public string SdtnguoiTraLoi { get; set; }
        public string Nam { get; set; }

        public virtual ThongTinCaThe MaCoSoNavigation { get; set; }
        public virtual LoaiHinhKinhte MaLhNavigation { get; set; }
        public virtual PhuongXa MaPhuongXaNavigation { get; set; }
        public virtual QuanHuyen MaQuanHuyenNavigation { get; set; }
        public virtual ThongTinDoanhNghiep MaSoThueNavigation { get; set; }
        public virtual TinhTp MaTinhTpNavigation { get; set; }
    }
}
