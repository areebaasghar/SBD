using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDModelClass
{
    public class EntCustomQuestionOpt
    {
        [Required]
        public int OptionId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public string? OptionText { get; set; }
    }
} 
