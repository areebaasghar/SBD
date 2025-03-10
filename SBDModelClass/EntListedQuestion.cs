using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDModelClass
{
    public class EntListedQuestion
    {
       
        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int fk_SubCatId { get; set; }
        [Required]
        public int fk_ItemId { get; set; }
        [Required]
        public string? QuestionText { get; set; }

        [Required]
        public string? QuestionType { get; set; }
    }
}
