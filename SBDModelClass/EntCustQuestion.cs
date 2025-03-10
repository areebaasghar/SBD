using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDModelClass
{
    public class EntCustQuestion
    {
        public int QuestionId { get; set; }
        public int fk_subCategoryId{ get; set; }
        public string? QuestionText { get; set; }
        public string? QuestionType { get; set; }
    }
}

