namespace azara.api.Controllers;

public class MyRewardsController : BaseController
{
    #region Object Declaration And Constructor

    public MyRewardsController(
        IConfiguration configrations,
        IStringLocalizer<BaseController> Localizer,
        ICrypto Crypto,
        AzaraContext DbContext,
        IMapper Mapper,
        IFileManagerLogic FileManagerLogic,
        ICSVService CSVService) : base(
            Localizer,
            Crypto,
            DbContext,
            Mapper,
            FileManagerLogic,
            CSVService)
    {
    }

    #endregion Object Declaration And Constructor

    #region 1. Insert MyRewards

    [HttpPost(ActionsConsts.MyRewards.InsertMyRewards)]
    public async Task<IActionResult> MyRewardsInsertAsync([FromBody] MyRewardsInsertUpdateRequest request)
    {
        if (request == null) request = new MyRewardsInsertUpdateRequest();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        request.UserId = GetUserId(User);

        using var helper = new MyRewardsHelpers(DbContext, Crypto, Mapper);
        var response = await helper.MyRewardsInsert(request);

        if (response == null) return ErrorResponse();
        if (!response.IsSuccess) return ErrorResponse(response.ErrorMessage);

        //DbContext.SaveChanges();

        return OkResponse();
    }

    #endregion 1. Insert MyRewards

    #region 2. MyReward Get By Id 
    [HttpPost(ActionsConsts.MyRewards.MyRewardGetById)]
    public async Task<IActionResult> MyRewardGetById([FromBody] MyRewardGetById request)
    {
        if (request == null) request = new MyRewardGetById();
        if (!ModelState.IsValid)
            return ErrorResponse(ModelState);

        using var helper = new MyRewardsHelpers(DbContext, Crypto, Mapper);
        var response = await helper.MyRewardGetById(request);

        if (response == null) return ErrorResponse();
       

        //DbContext.SaveChanges();

        return OkResponse(response);
    }
    #endregion


    //#region 4. Get MyRewards List

    //[HttpPost(ActionsConsts.MyRewards.GetMyRewardsList)]
    //public async Task<IActionResult> MyRewardsGetListAsync([FromBody] BaseIdRequest request)
    //{
    //    if (!ModelState.IsValid)
    //        return ErrorResponse(ModelState);

    //    using var helper = new MyRewardsHelpers(DbContext, Crypto, Mapper);

    //    var response = await helper.MyRewardsGetList(request);
    //    if (response is null) return ErrorResponse();

    //    return OkResponse(response);
    //}

    //#endregion 4. Get MyRewards List
}