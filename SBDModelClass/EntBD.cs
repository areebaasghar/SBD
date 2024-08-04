using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SBDModelClass
{
    public class EntBD
    {
        [Required] 
        public string? BusinessId { get; set; }

        [Required]
        public string? UserID { get; set; }

        [Required]
        public string? BusinessName { get; set; }

        [Required]
        public string? BusinessAddress { get; set; }

        [Required]
        public string? BusinessPhone { get; set; }

        [Required]
        public string? BusinessEmail { get; set; }
    }
}
