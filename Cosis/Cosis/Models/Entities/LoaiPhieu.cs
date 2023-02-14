using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Cosis.Models.Entities
{
    public partial class LoaiPhieu
    {
        public LoaiPhieu()
        {
            ThongTinCaThe = new HashSet<ThongTinCaThe>();
            ThongTinDoanhNghiep = new HashSet<ThongTinDoanhNghiep>();
        }

        public string MaLoaiPhieu { get; set; }
        public string TenLoaiPhieu { get; set; }

        public virtual ICollection<ThongTinCaThe> ThongTinCaThe { get; set; }
        public virtual ICollection<ThongTinDoanhNghiep> ThongTinDoanhNghiep { get; set; }
    }
}
