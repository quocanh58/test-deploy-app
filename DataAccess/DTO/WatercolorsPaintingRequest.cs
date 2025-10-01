using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class WatercolorsPaintingRequest
    {
        public string PaintingId { get; set; } = null!;

        //[RegularExpression(@"^[^\d]{1,100}$", ErrorMessage = "Fullname can have a maximum of 100 characters")]
        public string PaintingName { get; set; } = null!;

        public string? PaintingDescription { get; set; }

        public string? PaintingAuthor { get; set; }

        [Range(0, 100000000, ErrorMessage = "Price >= 0")]
        public decimal? Price { get; set; }

        [Range(1000, 100000000, ErrorMessage = "PublishYear >= 1000")]
        public int? PublishYear { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? StyleId { get; set; }
    }
}
