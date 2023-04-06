using System.ComponentModel;

namespace azara.models.Enums;

public class AdminPanelEnums
{
    public enum DateTypeEnum
    {
        [Description("Today")]
        Today = 0,

        [Description("Weekly")]
        Week = 1,

        [Description("Monthly")]
        Month = 2
    }
}