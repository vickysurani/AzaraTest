using azara.models.Requests.POSInventory;
using azara.models.Responses.POSInventory;
using System.Data;

namespace azara.api.Controllers
{
    public class POSInventoryController : BaseController
    {
        #region Object Declaration And Constructor

        public POSInventoryController(
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

        #region Calculate Total Pages

        public static int CalculateTotalPages(
            int total,
            int? pageSize)
        {
            var pages = Convert.ToDecimal(total) / Convert.ToDecimal(pageSize);
            var response = pages < 1 ? 1 : Convert.ToInt32(Math.Ceiling(pages));

            return response;
        }

        #endregion Calculate Total Pages

        #region 1. POS Inventory Get By Id

        [HttpPost(ActionsConsts.POSInventory.GetByIdInventoryPos)]
        public async Task<IActionResult> InventoryPosGetById([FromBody] POSInventoryGetByIdRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSInventoryHelpers(DbContext, Crypto);

            var response = await helper.POSInventoryGetById(request);

            return OkResponse(response);
        }

        #endregion 1. POS Inventory Get By Id

        #region 2. POS Inventory Update

        [HttpPost(ActionsConsts.POSInventory.UpdateInventoryPos)]
        public async Task<IActionResult> BlogUpdateAsync(
        [FromBody] POSInventoryUpdateRequest request,
        [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (request == null) request = new POSInventoryUpdateRequest();
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSInventoryHelpers(DbContext, Crypto);

            var response = await helper.POSInventoryUpdate(request);

            if (response == null) return ErrorResponse();

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.InventoryUpdate,
            new
            {
                request.Id,
            });

            return OkResponse(response);
        }

        #endregion 2. POS Inventory Update

        #region 3. POS Inventory GetList with Pagination

        [HttpPost(ActionsConsts.POSInventory.GetListInventoryPos)]
        public async Task<IActionResult> GetInventoryPosListAsync([FromBody] POSInventotyGetListRequest request)
        {
            #region SP Through
            //try
            //{
            //    if (!ModelState.IsValid)
            //        return ErrorResponse(ModelState);
            //    SqlConnection connection = (SqlConnection)DbContext.Database.GetDbConnection();
            //    SqlDataAdapter da = new SqlDataAdapter();
            //    SqlCommand cmd = new SqlCommand("POS_Inventory_List", connection);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add(new SqlParameter 
            //    { 
            //        ParameterName = "PageSize", 
            //        Value = request.PageSize,
            //        DbType=DbType.Int64 
            //    });
            //    cmd.Parameters.Add(new SqlParameter 
            //    { 
            //        ParameterName = "PageNo", 
            //        Value = request.PageNo,
            //        DbType=DbType.Int64 
            //    });
            //    cmd.Parameters.Add(new SqlParameter 
            //    { 
            //        ParameterName = "SearchParam", 
            //        Value = request.SearchParam,
            //        DbType=DbType.String 
            //    });
            //    cmd.Parameters.Add(new SqlParameter 
            //    { 
            //        ParameterName = "OrderBy", 
            //        Value = request.OrderBy,
            //        DbType=DbType.String 
            //    });

            //    da.SelectCommand = cmd;
            //    DataTable dt = new DataTable();

            //    da.Fill(dt);

            //    IList<POSInventoryGetList> items = dt.AsEnumerable().Select(x =>
            //    new POSInventoryGetList
            //    {
            //        Id = x.Field<int>("Id"),
            //        ListId = x.Field<string>("ListId"),
            //        Desc1= x.Field<string>("Desc1"),
            //        Price1 = x.Field<decimal>("Price1"),
            //        Price2 = x.Field<decimal>("Price2"),
            //        Price3 = x.Field<decimal>("Price3"),
            //        Price4 = x.Field<decimal>("Price4"),
            //        Price5 = x.Field<decimal>("Price5"),
            //        Image = x.Field<string>("Image")
            //    }).ToList();

            //    return OkResponse(items);
            //}
            //catch (Exception ex)
            //{
            //    return ErrorResponse(ex.Message);
            //}
            #endregion

            #region Entity Framework

            var inventoryList = (from INV in DbContext.POSInventory
                                 join D in DbContext.POSDepartments on INV.DepartmentListID equals D.ListId
                                 where string.IsNullOrEmpty(request.SearchParam) ||
                                           INV.Attribute.Contains(request.SearchParam) 
                                           //||
                                           //INV.Price1.ToString().Contains(request.SearchParam)
                                 select new POSInventoryGetByIdResponse
                                 {
                                     Id = INV.Id,
                                     ListId = INV.ListId,
                                     TimeCreated = INV.TimeCreated,
                                     TimeModified = INV.TimeModified,
                                     ALU = INV.ALU,
                                     Attribute = INV.Attribute,
                                     COGSAccount = INV.COGSAccount,
                                     Cost = INV.Cost,
                                     DepartmentCode = D.DepartmentName,
                                     DepartmentListID = INV.DepartmentListID,
                                     Description = INV.Description,
                                     Desc1 = INV.Desc1,
                                     IncomeAccount = INV.IncomeAccount,
                                     IsBelowReorder = INV.IsBelowReorder,
                                     IsEligibleForCommission = INV.IsEligibleForCommission,
                                     IsPrintingTags = INV.IsPrintingTags,
                                     IsUnorderable = INV.IsUnorderable,
                                     ItemNumber = INV.ItemNumber,
                                     ItemType = INV.ItemType,
                                     Keywords = INV.Keywords,
                                     LastReceived = INV.LastReceived,
                                     MarginPercent = INV.MarginPercent,
                                     MarkupPercent = INV.MarkupPercent,
                                     MSRP = INV.MSRP,
                                     OnHandStore01 = INV.OnHandStore01,
                                     OnHandStore02 = INV.OnHandStore02,
                                     OrderByUnit = INV.OrderByUnit,
                                     OrderCost = INV.OrderCost,
                                     Price1 = INV.Price1,
                                     Price2 = INV.Price2,
                                     Price3 = INV.Price3,
                                     Price4 = INV.Price4,
                                     Price5 = INV.Price5,
                                     QuantityOnCustomerOrder = INV.QuantityOnCustomerOrder,
                                     QuantityOnHand = INV.QuantityOnHand,
                                     QuantityOnOrder = INV.QuantityOnOrder,
                                     QuantityOnPendingOrder = INV.QuantityOnPendingOrder,
                                     ReorderPoint = INV.ReorderPoint,
                                     ReorderPointStore01 = INV.ReorderPointStore01,
                                     SellByUnit = INV.SellByUnit,
                                     SerialFlag = INV.SerialFlag,
                                     Size = INV.Size,
                                     StoreExchangeStatus = INV.StoreExchangeStatus,
                                     UnitOfMeasure = INV.UnitOfMeasure,
                                     UPC = INV.UPC,
                                     VendorCode = INV.VendorCode,
                                     VendorListID = INV.VendorListID,
                                     WebDesc = INV.WebDesc,
                                     WebPrice = INV.WebPrice,
                                     Manufacturer = INV.Manufacturer,
                                     Weight = INV.Weight,
                                     WebSKU = INV.WebSKU,
                                     WebCategories = INV.WebCategories,
                                     UnitOfMeasure1ALU = INV.UnitOfMeasure1ALU,
                                     UnitOfMeasure1MSRP = INV.UnitOfMeasure1MSRP,
                                     UnitOfMeasure1NumberOfBaseUnits = INV.UnitOfMeasure1NumberOfBaseUnits,
                                     UnitOfMeasure1Price1 = INV.UnitOfMeasure1Price1,
                                     UnitOfMeasure1Price2 = INV.UnitOfMeasure1Price2,
                                     UnitOfMeasure1Price3 = INV.UnitOfMeasure1Price3,
                                     UnitOfMeasure1Price4 = INV.UnitOfMeasure1Price4,
                                     UnitOfMeasure1Price5 = INV.UnitOfMeasure1Price5,
                                     UnitOfMeasure1UnitOfMeasure = INV.UnitOfMeasure1UnitOfMeasure,
                                     UnitOfMeasure1UPC = INV.UnitOfMeasure1UPC,
                                     UnitOfMeasure2ALU = INV.UnitOfMeasure2ALU,
                                     UnitOfMeasure2MSRP = INV.UnitOfMeasure2MSRP,
                                     UnitOfMeasure2NumberOfBaseUnits = INV.UnitOfMeasure2NumberOfBaseUnits,
                                     UnitOfMeasure2Price1 = INV.UnitOfMeasure2Price1,
                                     UnitOfMeasure2Price2 = INV.UnitOfMeasure2Price2,
                                     UnitOfMeasure2Price3 = INV.UnitOfMeasure2Price3,
                                     UnitOfMeasure2Price4 = INV.UnitOfMeasure2Price4,
                                     UnitOfMeasure2Price5 = INV.UnitOfMeasure2Price5,
                                     UnitOfMeasure2UnitOfMeasure = INV.UnitOfMeasure2UnitOfMeasure,
                                     UnitOfMeasure2UPC = INV.UnitOfMeasure2UPC,
                                     UnitOfMeasure3ALU = INV.UnitOfMeasure3ALU,
                                     UnitOfMeasure3MSRP = INV.UnitOfMeasure3MSRP,
                                     UnitOfMeasure3NumberOfBaseUnits = INV.UnitOfMeasure3NumberOfBaseUnits,
                                     UnitOfMeasure3Price1 = INV.UnitOfMeasure3Price1,
                                     UnitOfMeasure3Price2 = INV.UnitOfMeasure3Price2,
                                     UnitOfMeasure3Price3 = INV.UnitOfMeasure3Price3,
                                     UnitOfMeasure3Price4 = INV.UnitOfMeasure3Price4,
                                     UnitOfMeasure3Price5 = INV.UnitOfMeasure3Price5,
                                     UnitOfMeasure3UnitOfMeasure = INV.UnitOfMeasure3UnitOfMeasure,
                                     UnitOfMeasure3UPC = INV.UnitOfMeasure3UPC,
                                     VendorInfo2ALU = INV.VendorInfo2ALU,
                                     VendorInfo2OrderCost = INV.VendorInfo2OrderCost,
                                     VendorInfo2UPC = INV.VendorInfo2UPC,
                                     VendorInfo2VendorListID = INV.VendorInfo2VendorListID,
                                     VendorInfo3ALU = INV.VendorInfo3ALU,
                                     VendorInfo3OrderCost = INV.VendorInfo3OrderCost,
                                     VendorInfo3UPC = INV.VendorInfo3UPC,
                                     VendorInfo3VendorListID = INV.VendorInfo3VendorListID,
                                     VendorInfo4ALU = INV.VendorInfo4ALU,
                                     VendorInfo4OrderCost = INV.VendorInfo4OrderCost,
                                     VendorInfo4UPC = INV.VendorInfo4UPC,
                                     VendorInfo4VendorListID = INV.VendorInfo4VendorListID,
                                     VendorInfo5ALU = INV.VendorInfo5ALU,
                                     VendorInfo5OrderCost = INV.VendorInfo5OrderCost,
                                     VendorInfo5UPC = INV.VendorInfo5UPC,
                                     VendorInfo5VendorListID = INV.VendorInfo5VendorListID,
                                     Image = INV.Image,
                                     IsActive = INV.Active
                                 }).AsNoTracking().ToList();

            //var inventoryList = DbContext.POSInventory.Where(x => string.IsNullOrEmpty(request.SearchParam) ||
            //                                                 x.Desc1.Contains(request.SearchParam) ||
            //                                                 x.Price1.ToString().Contains(request.SearchParam))
            //                                          .Select(x => new POSInventoryGetByIdResponse
            //                                          {
            //                                              Id = x.Id,
            //                                              ListId = x.ListId,
            //                                              TimeCreated = x.TimeCreated,
            //                                              TimeModified = x.TimeModified,
            //                                              ALU = x.ALU,
            //                                              Attribute = x.Attribute,
            //                                              COGSAccount = x.COGSAccount,
            //                                              Cost = x.Cost,
            //                                              DepartmentCode = x.DepartmentCode,
            //                                              DepartmentListID = x.DepartmentListID,
            //                                              Desc1 = x.Desc1,
            //                                              IncomeAccount = x.IncomeAccount,
            //                                              IsBelowReorder = x.IsBelowReorder,
            //                                              IsEligibleForCommission = x.IsEligibleForCommission,
            //                                              IsPrintingTags = x.IsPrintingTags,
            //                                              IsUnorderable = x.IsUnorderable,
            //                                              ItemNumber = x.ItemNumber,
            //                                              ItemType = x.ItemType,
            //                                              Keywords = x.Keywords,
            //                                              LastReceived = x.LastReceived,
            //                                              MarginPercent = x.MarginPercent,
            //                                              MarkupPercent = x.MarkupPercent,
            //                                              MSRP = x.MSRP,
            //                                              OnHandStore01 = x.OnHandStore01,
            //                                              OnHandStore02 = x.OnHandStore02,
            //                                              OrderByUnit = x.OrderByUnit,
            //                                              OrderCost = x.OrderCost,
            //                                              Price1 = x.Price1,
            //                                              Price2 = x.Price2,
            //                                              Price3 = x.Price3,
            //                                              Price4 = x.Price4,
            //                                              Price5 = x.Price5,
            //                                              QuantityOnCustomerOrder = x.QuantityOnCustomerOrder,
            //                                              QuantityOnHand = x.QuantityOnHand,
            //                                              QuantityOnOrder = x.QuantityOnOrder,
            //                                              QuantityOnPendingOrder = x.QuantityOnPendingOrder,
            //                                              ReorderPoint = x.ReorderPoint,
            //                                              ReorderPointStore01 = x.ReorderPointStore01,
            //                                              SellByUnit = x.SellByUnit,
            //                                              SerialFlag = x.SerialFlag,
            //                                              Size = x.Size,
            //                                              StoreExchangeStatus = x.StoreExchangeStatus,
            //                                              UnitOfMeasure = x.UnitOfMeasure,
            //                                              UPC = x.UPC,
            //                                              VendorCode = x.VendorCode,
            //                                              VendorListID = x.VendorListID,
            //                                              WebDesc = x.WebDesc,
            //                                              WebPrice = x.WebPrice,
            //                                              Manufacturer = x.Manufacturer,
            //                                              Weight = x.Weight,
            //                                              WebSKU = x.WebSKU,
            //                                              WebCategories = x.WebCategories,
            //                                              UnitOfMeasure1ALU = x.UnitOfMeasure1ALU,
            //                                              UnitOfMeasure1MSRP = x.UnitOfMeasure1MSRP,
            //                                              UnitOfMeasure1NumberOfBaseUnits = x.UnitOfMeasure1NumberOfBaseUnits,
            //                                              UnitOfMeasure1Price1 = x.UnitOfMeasure1Price1,
            //                                              UnitOfMeasure1Price2 = x.UnitOfMeasure1Price2,
            //                                              UnitOfMeasure1Price3 = x.UnitOfMeasure1Price3,
            //                                              UnitOfMeasure1Price4 = x.UnitOfMeasure1Price4,
            //                                              UnitOfMeasure1Price5 = x.UnitOfMeasure1Price5,
            //                                              UnitOfMeasure1UnitOfMeasure = x.UnitOfMeasure1UnitOfMeasure,
            //                                              UnitOfMeasure1UPC = x.UnitOfMeasure1UPC,
            //                                              UnitOfMeasure2ALU = x.UnitOfMeasure2ALU,
            //                                              UnitOfMeasure2MSRP = x.UnitOfMeasure2MSRP,
            //                                              UnitOfMeasure2NumberOfBaseUnits = x.UnitOfMeasure2NumberOfBaseUnits,
            //                                              UnitOfMeasure2Price1 = x.UnitOfMeasure2Price1,
            //                                              UnitOfMeasure2Price2 = x.UnitOfMeasure2Price2,
            //                                              UnitOfMeasure2Price3 = x.UnitOfMeasure2Price3,
            //                                              UnitOfMeasure2Price4 = x.UnitOfMeasure2Price4,
            //                                              UnitOfMeasure2Price5 = x.UnitOfMeasure2Price5,
            //                                              UnitOfMeasure2UnitOfMeasure = x.UnitOfMeasure2UnitOfMeasure,
            //                                              UnitOfMeasure2UPC = x.UnitOfMeasure2UPC,
            //                                              UnitOfMeasure3ALU = x.UnitOfMeasure3ALU,
            //                                              UnitOfMeasure3MSRP = x.UnitOfMeasure3MSRP,
            //                                              UnitOfMeasure3NumberOfBaseUnits = x.UnitOfMeasure3NumberOfBaseUnits,
            //                                              UnitOfMeasure3Price1 = x.UnitOfMeasure3Price1,
            //                                              UnitOfMeasure3Price2 = x.UnitOfMeasure3Price2,
            //                                              UnitOfMeasure3Price3 = x.UnitOfMeasure3Price3,
            //                                              UnitOfMeasure3Price4 = x.UnitOfMeasure3Price4,
            //                                              UnitOfMeasure3Price5 = x.UnitOfMeasure3Price5,
            //                                              UnitOfMeasure3UnitOfMeasure = x.UnitOfMeasure3UnitOfMeasure,
            //                                              UnitOfMeasure3UPC = x.UnitOfMeasure3UPC,
            //                                              VendorInfo2ALU = x.VendorInfo2ALU,
            //                                              VendorInfo2OrderCost = x.VendorInfo2OrderCost,
            //                                              VendorInfo2UPC = x.VendorInfo2UPC,
            //                                              VendorInfo2VendorListID = x.VendorInfo2VendorListID,
            //                                              VendorInfo3ALU = x.VendorInfo3ALU,
            //                                              VendorInfo3OrderCost = x.VendorInfo3OrderCost,
            //                                              VendorInfo3UPC = x.VendorInfo3UPC,
            //                                              VendorInfo3VendorListID = x.VendorInfo3VendorListID,
            //                                              VendorInfo4ALU = x.VendorInfo4ALU,
            //                                              VendorInfo4OrderCost = x.VendorInfo4OrderCost,
            //                                              VendorInfo4UPC = x.VendorInfo4UPC,
            //                                              VendorInfo4VendorListID = x.VendorInfo4VendorListID,
            //                                              VendorInfo5ALU = x.VendorInfo5ALU,
            //                                              VendorInfo5OrderCost = x.VendorInfo5OrderCost,
            //                                              VendorInfo5UPC = x.VendorInfo5UPC,
            //                                              VendorInfo5VendorListID = x.VendorInfo5VendorListID,
            //                                              Image = x.Image,
            //                                          }).AsNoTracking().ToList();

            var total = inventoryList.Count();
            var totalPages = CalculateTotalPages(total, request.PageSize);
            var inventoryPaginationList = inventoryList.Skip(request.PageNo * request.PageSize).Take(request.PageSize);

            var response = new
            {
                Total = total,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                Offset = request.PageNo,
                Details = inventoryPaginationList
            };

            var paginationResponse = new PaginationResponse
            {
                Total = response.Total,
                TotalPages = response.Total,
                OffSet = response.Offset,
                Details = response.Details
            };

            return OkResponse(paginationResponse);

            #endregion
        }

        #endregion 3. POS Inventory GetList with Pagination

        #region 4. POS Inventory List without Pagination

        [HttpPost(ActionsConsts.POSInventory.InventoryPosList)]
        public async Task<IActionResult> POSInventoryListAsync([FromBody] POSInventoryListRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSInventoryHelpers(DbContext, Crypto);

            var response = await helper.POSInventoryList(request);
            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }

        #endregion 4. POS Inventory List without Pagination

        #region 5. DISTINCT SubCategory List

        [HttpPost(ActionsConsts.POSInventory.DistinctSubCategoryList)]
        public async Task<IActionResult> DistinctSubCategoryListAsync([FromBody] POSInventoryDistinctSubCategoryList request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            var categorychecks = (from PS in DbContext.POSDepartments
                                  join P in DbContext.POSSubCategory on PS.ListId equals P.DepartmentListId

                                  select new POSSubCategoryEntity
                                  {
                                      Name = P.Name,
                                      Image = PS.Image,
                                      DepartmentListId = P.DepartmentListId
                                  }).Distinct().ToList();

            var response = DbContext.POSInventory.Where(x => x.DepartmentListID == request.DepartmentListID).Select(x => new
            {
                x.Desc1,
                x.Image
            }).Distinct();

            //var response = DbContext.POSInventory.Where(x => x.DepartmentListID == request.DepartmentListID).ToList();
            //var inventory = DbContext.POSInventory.FirstOrDefault(x => x.DepartmentListID.Equals(request.DepartmentListID));

            //if (inventory == null)
            //    throw new ApiException("error_inventory_not_found");

            //var response = new POSSubcategoryByIdResponse
            //{
            //    Desc1 = inventory.Desc1,
            //    Image = inventory.Image
            //};


            if (response is null) return ErrorResponse();

            return OkResponse(response);
        }

        #endregion

        #region 6. POS Inventory List without Pagination
        [HttpGet(ActionsConsts.POSInventory.GetInventoryPosList)]
        public async Task<IActionResult> GetPOSInventoryListAsync()
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            var Inventory = DbContext.POSInventory.ToList();

            return OkResponse(Inventory);
        }
        #endregion

        #region 7. POSInventory Add Update
        //Temptory Table to POSInventory Add/Update

        [HttpGet(ActionsConsts.POSInventory.POSInventoryAddUpdate)]
        public async Task<IActionResult> POSInventoryAddUpdate()
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);


            using var helper = new POSInventoryHelpers(DbContext, Crypto);

            var response = await helper.POSInventoryAddUpdate();

            return OkResponse(response);
        }

        #endregion

        #region 8. POSInventory status Update

        [Authorize, HttpPost(ActionsConsts.POSInventory.POSInventoryStatusUpdate)]
        public async Task<IActionResult> POSInventoryStatusUpdateAsync(
            [FromBody] BaseListIdStatusUpdateRequest request,
            [FromServices] IHubContext<SignalRHelpers> hubContext)
        {
            if (!ModelState.IsValid)
                return ErrorResponse(ModelState);

            using var helper = new POSInventoryHelpers(DbContext, Crypto);

            var response = await helper.POSInventoryStatusUpdate(request);

            await new SignalRHelpers(hubContext).SendMessage(NotificationTypeConsts.InventoryStatus,
            new
            {
                request.Id,
            });

            return OkResponse();
        }

        #endregion

    }
}
