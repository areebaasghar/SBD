using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDModelClass
{
    public class EntAth
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }
        [Required]
        public string? UserId { get; set; }
    }
}
