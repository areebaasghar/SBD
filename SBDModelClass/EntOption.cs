using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDModelClass
{
    public class EntOption
    {
        [Required]
        public string? OptionId { get; set; }

        [Required]
        public string? QuestionId { get; set; }

        [Required]
        public string? OptionText { get; set; }

    }
}
