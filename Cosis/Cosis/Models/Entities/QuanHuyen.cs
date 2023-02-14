using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Cosis.Models.Entities
{
    public partial class QuanHuyen
    {
        public QuanHuyen()
        {
            Master = new HashSet<Master>();
            ThongTinCaThe = new HashSet<ThongTinCaThe>();
            ThongTinDoanhNghiep = new HashSet<ThongTinDoanhNghiep>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Master> Master { get; set; }
        public virtual ICollection<ThongTinCaThe> ThongTinCaThe { get; set; }
        public virtual ICollection<ThongTinDoanhNghiep> ThongTinDoanhNghiep { get; set; }
    }
}
