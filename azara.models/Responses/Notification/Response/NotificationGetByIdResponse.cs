using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azara.models.Responses.Notification.Response
{
    public class NotificationGetByIdResponse
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReadableTime { get; set; }

        public Guid? UserId { get; set; }

        public string? Icon { get; set; }   

    }
}
