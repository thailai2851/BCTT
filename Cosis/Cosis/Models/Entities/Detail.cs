using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Cosis.Models.Entities
{
    public partial class Detail
    {
        public string MaPhieu { get; set; }
        public string Stt { get; set; }
        public string TenCt { get; set; }
        public string Dvt { get; set; }
        public decimal? ThthangTruoc { get; set; }
        public decimal? DuTinh { get; set; }
        public decimal? TongCongDon { get; set; }
        public decimal? Tttm { get; set; }
        public decimal? Tong { get; set; }

        public virtual Master MaPhieuNavigation { get; set; }
    }
}
