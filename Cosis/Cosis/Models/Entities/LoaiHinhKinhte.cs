using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Cosis.Models.Entities
{
    public partial class LoaiHinhKinhte
    {
        public LoaiHinhKinhte()
        {
            Master = new HashSet<Master>();
            ThongTinCaThe = new HashSet<ThongTinCaThe>();
            ThongTinDoanhNghiep = new HashSet<ThongTinDoanhNghiep>();
        }

        public string MaLh { get; set; }
        public string TenLh { get; set; }

        public virtual ICollection<Master> Master { get; set; }
        public virtual ICollection<ThongTinCaThe> ThongTinCaThe { get; set; }
        public virtual ICollection<ThongTinDoanhNghiep> ThongTinDoanhNghiep { get; set; }
    }
}
