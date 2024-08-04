using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDModelClass
{
    public class EntUserInfo
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Cnic { get; set; }
        public string? City { get; set; }
        public DateTime? DOB { get; set; } = null;
        public string? PhoneNo { get; set; }
        public string? Gender { get; set; }

    }
}
