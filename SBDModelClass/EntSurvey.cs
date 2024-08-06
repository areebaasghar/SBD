using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SBDModelClass
{
    public class EntSurvey
    {   
        public string? UserID { get; set; }  
        public string? SurveyId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Education { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; } 

    }
}
