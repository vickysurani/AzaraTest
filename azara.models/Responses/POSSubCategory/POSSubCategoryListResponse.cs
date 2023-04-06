using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azara.models.Responses.POSSubCategory
{
    public class POSSubCategoryListResponse
    {
        public int Id { get; set; }
        public string ?Name { get; set; }

        public string? Image { get; set; }

        public bool? Active { get; set; }

        public string ?Attribute { get; set; }

        public string? DepartmentListId { get; set; }

        public string? DepartmentListName { get; set; }

    }
}
