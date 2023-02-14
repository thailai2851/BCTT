using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Cosis.Models.Entities
{
    public partial class DanhSachNhanToAnhHuong
    {
        public string MaPhieu { get; set; }
        public string MaAh { get; set; }
        public DanhSachNhanToAnhHuong(string MaAh)
        {
            this.MaAh = MaAh;
        }
        public virtual NhanToAnhHuong MaAhNavigation { get; set; }
        public virtual Master MaPhieuNavigation { get; set; }
    }
}
