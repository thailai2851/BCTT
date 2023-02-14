using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Cosis.Models.Entities
{
    public partial class VaiTro
    {
        public VaiTro()
        {
            TaiKhoang = new HashSet<TaiKhoang>();
        }

        public string IdvaiTro { get; set; }
        public string TenVaiTro { get; set; }

        public virtual ICollection<TaiKhoang> TaiKhoang { get; set; }
    }
}
