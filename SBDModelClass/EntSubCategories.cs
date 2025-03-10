using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBDModelClass
{
    public class EntSubCategories
    {
        public int fk_CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string? SubCatogeryName { get; set; }
        public int MinBudget { get; set; }
        public int MaxBudget { get; set; }
        public string? Description { get; set; }
    }
}
