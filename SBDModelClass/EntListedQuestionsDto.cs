using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDModelClass
{
    public class EntListedQuestionsDto
    {
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public string? QuestionText { get; set; }

        [Required]
        public string? QuestionType { get; set; }
        [Required]
        public string? SubCatogeryName { get; set; }
        [Required]
        public string? ItemName { get; set; }
    }
}
