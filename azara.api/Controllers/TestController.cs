namespace azara.api.Controllers;

public class TestController : BaseController
{
    #region Object Declaration And Constructor

    public TestController(
        IConfiguration configrations,
        IStringLocalizer<BaseController> Localizer,
        ICrypto Crypto,
        AzaraContext DbContext,
        IMapper Mapper,
        IFileManagerLogic FileManagerLogic,
        ICSVService CSVService)
    : base(
          Localizer,
          Crypto,
          DbContext,
          Mapper,
          FileManagerLogic,
          CSVService)
    {
    }

    #endregion Object Declaration And Constructor

    #region 1. Select

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]

    [HttpGet("test/get")]
    public IActionResult TestGet()
    {
        var admin = DbContext.Admin.ToList();
        var adminView = DbContext.AdminView.ToList();

        return OkResponse(new { admin, adminView });
    }

    #endregion 1. Select

    #region 2. Select Customer

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [HttpGet("test/get/customer")]
    public IActionResult TestGetCustomer()
    {
        var customerView = DbContext.CustomerView.ToList();

        return OkResponse(customerView);
    }

    #endregion 1. Select

    #region 2. Select Company

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [HttpGet("test/get/company")]
    public IActionResult TestGetCompany()
    {
        var companyView = DbContext.Companyview.ToList();

        return OkResponse(companyView);
    }

    #endregion 2. Select Company

    #region 3. Add

    /*
     *  It's SP for INSERT record into linked server.
     *
     *  CREATE PROCEDURE[dbo].[InsertIntoLinkedServer]
     *      @FirstName NVARCHAR(50)    =	NULL,
     *      @LastName NVARCHAR(50)    =	NULL
     *  AS
     *  BEGIN
     *      SET NOCOUNT ON;
     *
     *      DECLARE @Milisecond NVARCHAR(50) = (SELECT DATEDIFF(ms, CAST(GetUtcDate() AS date), GetUtcDate()));
     *
     *      IF @FirstName IS NULL   SET @FirstName = CONCAT(N'UK', @Milisecond);
     *      IF @LastName IS NULL    SET @LastName = N'Khati'
     *
     *      INSERT INTO[QUICKBOOK]...[Customer]
     *      (FirstName, LastName)
     *      VALUES
     *      (@FirstName, @LastName);
     *  END
     */

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [HttpGet("test/insert")]
    public IActionResult TestAdd()
    {
        string firstName = $"Uditsing{DateTime.Now:yyyyMMddHHmmss}";
        string lastName = $"Khati{DateTime.Now:yyyyMMddHHmmsssss}";

        //  1st way
        DbContext.Database.ExecuteSqlRaw($"EXEC [dbo].[InsertIntoLinkedServer] " +
            $"@FirstName = {firstName}, " +
            $"@LastName = {lastName} ");

        //  2nd way
        //DbContext.Database.ExecuteSqlRaw(
        //    "[dbo].[InsertIntoLinkedServer] @p0, @p1",
        //    parameters: new[] { firstName, lastName });

        var customerView = DbContext.CustomerView.ToList();

        return OkResponse(new { customerCount = customerView.Count, customerList = customerView });
    }

    #endregion 3. Add

    #region 4. Select

    /*
     * This is view for get data from linked server
     *
     *  CREATE VIEW[dbo].[VW_Customer]
     *  AS
     *      SELECT *
     *      FROM[QUICKBOOK]...Customer
     *  GO
     */

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [HttpGet("test/select")]
    public IActionResult TestSelect()
    {
        var companyView = DbContext.CustomerView.ToList();

        return OkResponse(companyView);
    }

    #endregion 4. Select

    #region 5. Update

    /*
     * It's SP for UPDATE record into linked server.
     *
     *  CREATE PROCEDURE[dbo].[UpdateIntoLinkedServer]
     *      @ListId NVARCHAR(50),
     *      @FirstName NVARCHAR(50)
     *  AS
     *  BEGIN
     *      SET NOCOUNT ON;
     *
     *      UPDATE[QUICKBOOK]...[Customer]
     *      SET[FirstName] = @FirstName
     *      WHERE[ListId] = @ListId
     *  END
     */

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [HttpGet("test/update")]
    public IActionResult TestUpdate()
    {
        var customerView = DbContext.CustomerView.ToList();

        string firstName = $"Updated{DateTime.Now:yyyyMMddHHmmss}";

        if (customerView.Any())
            DbContext.Database.ExecuteSqlRaw($"EXEC [dbo].[UpdateIntoLinkedServer] @ListId = {customerView.Select(x => x.ListID).First()}, @FirstName = {firstName}");

        var customerViewUpdated = DbContext.CustomerView.ToList();

        return OkResponse(new { customerCount = customerViewUpdated.Count, customerList = customerViewUpdated });
    }

    #endregion 5. Update

    #region 6. Delete

    /*
     * It's SP for DELETE record into linked server.
     *
     *  CREATE PROCEDURE[dbo].[DeleteFromLinkedServer]
     *      @ListId NVARCHAR(50)
     *  AS
     *  BEGIN
     *  SET NOCOUNT ON;
     *
     *      DELETE FROM[QUICKBOOK]...[Customer]
     *      WHERE[ListId] = @ListId
     *  END
     */

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
    [HttpGet("test/delete")]
    public IActionResult TestDelete()
    {
        var customerView = DbContext.CustomerView.ToList();

        if (customerView.Any())
            DbContext.Database.ExecuteSqlRaw($"EXEC [dbo].[DeleteFromLinkedServer] @ListId = {customerView.Select(x => x.ListID).First()}");

        var customerViewUpdated = DbContext.CustomerView.ToList();

        return OkResponse(new { customerCount = customerViewUpdated.Count, customerList = customerViewUpdated });
    }

    #endregion 6. Delete
}