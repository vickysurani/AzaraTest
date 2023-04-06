namespace azara.models.Requests.AdminPanel;

public class DashboardRequest
{
    public int NewUser { get; set; }

    public int TotalSales { get; set; }

    public int TotalUser { get; set; }

    public DateTypeEnum DateTypeEnum { get; set; }
}