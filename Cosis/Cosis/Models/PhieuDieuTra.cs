using Cosis.Models.Entities;
using System.Collections.Generic;

namespace Cosis.Models
{
    public class PhieuDieuTra
    {
        public Master Master { get; set; }
        public List<DanhSachNhanToAnhHuong> DanhSachNhanToAnhHuong { get; set; }
        public NhanToThu9 NhanToThu9 { get; set; }
        public List<Detail> Detail { get; set; }
    }
}
