using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azara.models.Requests.POSSubCategory
{
    public class POSSubCategoryUpdateRequest
    {
        public int ProductSubCategoryId { get; set; }

        public string? Image { get; set; }

    }
}
