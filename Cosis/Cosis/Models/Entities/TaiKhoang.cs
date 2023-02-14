using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Cosis.Models.Entities
{
    public partial class TaiKhoang
    {
        public TaiKhoang()
        {
            ThongTinCaThe = new HashSet<ThongTinCaThe>();
            ThongTinDoanhNghiep = new HashSet<ThongTinDoanhNghiep>();
        }

        public string TaiKhoang1 { get; set; }
        public string MatKhau { get; set; }
        public string IdvaiTro { get; set; }

        public virtual VaiTro IdvaiTroNavigation { get; set; }
        public virtual ICollection<ThongTinCaThe> ThongTinCaThe { get; set; }
        public virtual ICollection<ThongTinDoanhNghiep> ThongTinDoanhNghiep { get; set; }
    }
}
