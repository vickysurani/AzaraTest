namespace azara.api.Controllers;

public class CustomerController : BaseController
{
    #region Object Declaration And Constructor

    public CustomerController(
        IStringLocalizer<BaseController> Localizer,
        ICrypto Crypto,
        AzaraContext DbContext,
        IMapper Mapper,
        IFileManagerLogic FileManagerLogic,
        ICSVService cSVService)
    : base(
          Localizer,
          Crypto,
          DbContext,
          Mapper,
          FileManagerLogic,
          cSVService
          )
    {
    }

    #endregion Object Declaration And Constructor


    #region 1. Insert Customer
    //[HttpGet(ActionsConsts.POSCustomerData.POSCustomerInsert)]
    //public IActionResult POS_Costomer_Insert()
    //{
    //    string firstName = $"Amisha{DateTime.Now:yyyyMMddHHmmss}";
    //    string lastName = $"Panchal{DateTime.Now:yyyyMMddHHmmsssss}";

    //    DbContext.Database.ExecuteSqlRaw($"EXEC [dbo].[InsertIntoLinkedServer] @FirstName = {firstName}, @LastName = {lastName}");

    //    var customerView = DbContext.CustomerView.ToList();

    //    return OkResponse(new { customerCount = customerView.Count, customerList = customerView });
    //}

    #endregion

    #region 2. Get Customer List From POS

    [AllowAnonymous]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [HttpGet(ActionsConsts.POSCustomerData.POSCustomerList)]
    public IActionResult POS_Costomer_GetList()
    {
        var customerView = DbContext.CustomerView.ToList();

        return OkResponse(customerView);
    }

    #endregion

    #region 3s. Get Company List From POS

    [AllowAnonymous]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [HttpGet(ActionsConsts.POSCustomerData.POSCompanyList)]
    public IActionResult POS_Company_GetList()
    {
        var companyview = DbContext.Companyview.ToList();

        return OkResponse(companyview);
    }

    #endregion

    #region 3s. Get Company List From POS

    //[AllowAnonymous]
    //[HttpGet(ActionsConsts.POSCustomerData.POSInventoryList)]
    //public IActionResult POS_Inventory_GetList()
    //{
    //    var companyview = DbContext.ItemInventoryView.ToList();

    //    return OkResponse(companyview);
    //}

    #endregion
}