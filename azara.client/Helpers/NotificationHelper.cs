using azara.client.Models.Notification;

namespace azara.client.Helpers
{
    public class NotificationHelper
    {
        internal static async Task<dynamic> NotificationListGetById(NotificationGetList request)
        {
            var url = $"{ApiEndPointConsts.Notification.GetNotificationByUserId}";
            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Service.PostAPIWithoutToken(url, stringContent);

            return response.meta.statusCode != StatusCodeConsts.Success
                ? new CallAPIList() { meta = response.meta }
                : new CallAPI() { meta = response.meta, data = response.data };
        }
    }
}
