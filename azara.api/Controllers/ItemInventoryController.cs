//namespace azara.api.Controllers
//{
//    public class ItemInventoryController : BaseController
//    {
//        #region Object Declaration And Constructor

//        public ItemInventoryController(
//            IStringLocalizer<BaseController> Localizer,
//            ICrypto Crypto,
//            AzaraContext DbContext,
//            IMapper Mapper,
//            IFileManagerLogic FileManagerLogic)
//        : base(
//              Localizer,
//              Crypto,
//              DbContext,
//              Mapper,
//              FileManagerLogic)
//        {
//        }

//        #endregion Object Declaration And Constructor

//        #region 1. Insert Item Inventory
//        [HttpGet(ActionsConsts.POSItemInventoryData.POSItemInventoryInsert)]
//        public IActionResult POS_Item_Inventory_Insert()
//        {
//            try
//            {
//                //decimal cost = 0.00m;
//                string departmentCode = "SYS";
//                string desc1 = "Demo";
//                string desc2 = "Data";
//                //decimal price1 = 0.00m;
//                //decimal price2 = 0.00m;

//                DbContext.Database.ExecuteSqlRaw($"EXEC [dbo].[InsertItemInventory] @DepartmentCode = {departmentCode} , @Desc1 = {desc1}, @Desc2 = {desc2}");

//                var inventoryView = DbContext.ItemInventoryView.ToList();

//                return OkResponse(new { itemInventoryCount = inventoryView.Count, itemInventoryList = inventoryView });
//            }
//            catch (Exception ex)
//            {

//                return Ok(ex);
//            }
//        }

//        #endregion

//        #region 2. Get Customer List From POS

//        [HttpGet(ActionsConsts.POSItemInventoryData.POSItemInventoryList)]
//        public IActionResult POS_Item_Inventory_GetList()
//        {
//            var inventoryView = DbContext.ItemInventoryView.ToList();

//            return OkResponse(inventoryView);
//        }

//        #endregion
//    }
//}
