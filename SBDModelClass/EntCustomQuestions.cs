using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDModelClass
{
    public class EntCustomQuestions
    {
        public int QuestionId { get; set; }
        public int SubCatId { get; set; }
        public string? QuestionText { get; set; }
        public string? QuestionType { get; set; }
    }
}
