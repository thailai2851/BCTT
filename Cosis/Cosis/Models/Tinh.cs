using System.Collections.Generic;

namespace Cosis.Models
{
    public class Tinh
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Districts> Districts { get; set; }
    }
}
