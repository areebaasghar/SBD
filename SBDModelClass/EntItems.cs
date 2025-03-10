using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDModelClass
{
    public class EntItems
    {
        [Required]
        public int ItemId { get; set; }

        [Required]
        public int fk_SubCatId { get; set; }

        [Required]
        public string? ItemName { get; set; }
    }
}
