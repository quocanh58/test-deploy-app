using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class StyleDTO
    {
        public string StyleId { get; set; } = null!;

        public string StyleName { get; set; } = null!;

        public string StyleDescription { get; set; } = null!;

        public string? OriginalCountry { get; set; }

        //public virtual ICollection<WatercolorsPainting> WatercolorsPaintings { get; set; } = new List<WatercolorsPainting>();
    }
}
