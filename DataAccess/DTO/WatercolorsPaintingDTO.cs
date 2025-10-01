using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class WatercolorsPaintingDTO
    {
        public string PaintingId { get; set; } = null!;

        public string PaintingName { get; set; } = null!;

        public string? PaintingDescription { get; set; }

        public string? PaintingAuthor { get; set; }

        public decimal? Price { get; set; }

        public int? PublishYear { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? StyleId { get; set; }

        public virtual StyleDTO? Style { get; set; }
    }
}
