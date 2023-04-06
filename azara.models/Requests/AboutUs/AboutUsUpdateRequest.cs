using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azara.models.Requests.AboutUs
{
    public class AboutUsUpdateRequest
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}
