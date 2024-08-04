using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SBDModelClass
{
    //SurveyId varchar(50) Unchecked
    //UserID  varchar(50) Checked


    public class EntSurvey
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Education { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }




    }
}
