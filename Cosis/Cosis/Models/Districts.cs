using System.Collections.Generic;

namespace Cosis.Models
{
    public class Districts
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Wards> Wards { get; set; }
    }
}
