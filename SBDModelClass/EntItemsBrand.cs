using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDModelClass
{
    public class EntItemsBrand
    {
        [Required]
        public int BrandId { get; set; }

        [Required]
        public int fk_ItemId { get; set; }

        [Required]
        public string? BrandName { get; set; }
    }
}


