using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azara.models.Responses.MyRewards
{
    public class MyRewardGetByIdResponse
    {
        public Guid? Id { get; set; } 
        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public Guid? UserId { get; set; }

        public Guid? CouponsId { get; set; }

        public Guid? RewardId { get; set; }

        public string? Barcode { get; set; }

        public Guid? StatusId { get; set; }

        public bool? IsReward { get; set; }

    }
}
