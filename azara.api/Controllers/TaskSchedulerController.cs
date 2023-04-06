using System.Data;
using System.Data.Common;

namespace azara.api.Controllers
{
    public class TaskSchedulerController : BaseController
    {
        #region Object Declaration And Constructor

        public TaskSchedulerController(
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

        #region 1. Get Customer Reward List
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]

        [HttpGet(ActionsConsts.TaskScheduler.GetCustomerRewardPos)]
        public async Task<IActionResult> GetCustomerRewardPosAsync()
        {

            SqlConnection connection = (SqlConnection)DbContext.Database.GetDbConnection();
            try
            {
                if (!ModelState.IsValid)
                    return ErrorResponse(ModelState);
                SqlCommand cmd = new SqlCommand("[dbo].[InserFromLinkedServer]", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                cmd.Dispose();
                return OkResponse(ModelState);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        #endregion 1. Get Customer Reward List

        #region 2. Get Customer List
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]

        [HttpGet(ActionsConsts.TaskScheduler.GetCustomerPos)]
        public async Task<IActionResult> GetCustomerPosAsync()
        {
            SqlConnection connection = (SqlConnection)DbContext.Database.GetDbConnection();
            try
            {
                if (!ModelState.IsValid)
                    return ErrorResponse(ModelState);                
                SqlCommand cmd = new SqlCommand("[dbo].[Inser_Customer_FromLinkedServer]", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                //connection.Dispose();
                cmd.Dispose();

                if (connection.State == ConnectionState.Closed)
                {
                    using var helper = new POSCustomerHelpers(DbContext, Crypto);
                    var response = await helper.POSCustomerAddUpdate();
                }

                return OkResponse(ModelState);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
            finally
            {
                connection.Close();

            }
        }

        #endregion 2. Get Customer List

        #region 3. Get Inventory List
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]

        [HttpGet(ActionsConsts.TaskScheduler.GetInventoryPos)]
        public async Task<IActionResult> GetInventoryPosAsync()
        {
            SqlConnection connection = (SqlConnection)DbContext.Database.GetDbConnection();
            try
            {
                if (!ModelState.IsValid)
                    return ErrorResponse(ModelState);                
                SqlCommand cmd = new SqlCommand("[dbo].[Insert_Inventory_FromLinkedServer]", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                cmd.Dispose();
                
                if (connection.State == ConnectionState.Closed)
                {
                    using var helper = new POSInventoryHelpers(DbContext, Crypto);
                    var response = await helper.POSInventoryAddUpdate();
                }
                return OkResponse(ModelState);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        #endregion 3. Get Inventory List

        #region 4. Get Department List
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]

        [HttpGet(ActionsConsts.TaskScheduler.GetDepartmentPos)]
        public async Task<IActionResult> GetDepartmentPosAsync()
        {
            SqlConnection connection = (SqlConnection)DbContext.Database.GetDbConnection();
            try
            {
                if (!ModelState.IsValid)
                    return ErrorResponse(ModelState);                
                SqlCommand cmd = new SqlCommand("[dbo].[Insert_Department_FromLinkedServer]", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                cmd.Dispose();


                if (connection.State == ConnectionState.Closed)
                {
                    using var helper = new POSDepartmentHelper(DbContext, Crypto);
                    var response = await helper.POSDepartmentAddUpdate();
                }

                return OkResponse(ModelState);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        #endregion 4. Get Department List

        #region 5. Get Sales Receipt List
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]

        [HttpGet(ActionsConsts.TaskScheduler.GetSalesReceiptPos)]
        public async Task<IActionResult> GetSalesReceiptPosAsync()
        {
            SqlConnection connection = (SqlConnection)DbContext.Database.GetDbConnection();
            try
            {
                if (!ModelState.IsValid)
                    return ErrorResponse(ModelState);                
                SqlCommand cmd = new SqlCommand("[dbo].[Insert_POSSalesReceiptTMP_FromLinkedServer]", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                cmd.Dispose();

                if (connection.State == ConnectionState.Closed)
                {
                    using var helper = new POSSalesReceiptHelpers(DbContext, Crypto);
                    var response = await helper.POSSalesReceiptAddUpdate();
                }

                return OkResponse(ModelState);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        #endregion 5. Get Sales Receipt List

    }
}
