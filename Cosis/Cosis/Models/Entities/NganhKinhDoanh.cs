using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Cosis.Models.Entities
{
    public partial class NganhKinhDoanh
    {
        public string MaSoThue { get; set; }
        public string MaSoThue2 { get; set; }
        public string MaCoSo { get; set; }
        public string MaNganh { get; set; }

        public virtual ThongTinCaThe MaCoSoNavigation { get; set; }
        public virtual NganhHoatDongKinhDoanh MaNganhNavigation { get; set; }
        public virtual ThongTinDoanhNghiep MaSoThueNavigation { get; set; }
    }
}
