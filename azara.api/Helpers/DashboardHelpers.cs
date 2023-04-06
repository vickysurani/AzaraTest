using azara.models.Responses.Dashboard;

namespace azara.api.Helpers
{
    public class DashboardHelpers : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }


        public DashboardHelpers(
            AzaraContext DbContext,
            ICrypto Crypto)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
        }

        #endregion Constructor

        #region Dashboard

        public async Task<BaseResponse> DashboardData()
        {
            try
            {
                var todaysRegisteredUserCount = DbContext.User.Where(x => x.Created.Date.Equals(DateTime.Today.Date)).Count();
                var weeklyRegisteredUserCount = DbContext.User.Where(x => x.Created.Date > DateTime.Now.AddDays(-7).Date).Count();
                var monthlyRegisteredUserCount = DbContext.User.Where(x => x.Created.Date.Month == DateTime.Now.Date.Month &&
                                                                      x.Created.Date.Year == DateTime.Now.Date.Year).Count();
                //var productCount = DbContext.Product.Where(x => x.Active && !x.Deleted).Count();
                var productCount = (from P in DbContext.Product
                                    join S in DbContext.Stores on P.StoreDropdownId equals S.Id.ToString()
                                    join PC in DbContext.ProductCategories on P.ProductCategoryId equals PC.Id
                                    join O in DbContext.Offers on P.OfferId equals O.Id
                                    where
                                    !P.Deleted &&
                                    P.Active
                                    select new { }).Count();
                var storeCount = DbContext.Stores.Where(x => x.Active && !x.Deleted).Count();
                var contactRequestCount = DbContext.ContactUs.Where(x => x.Active && !x.Deleted).Count();
                var blogCount = DbContext.Blogs.Where(x => x.Active && !x.Deleted).Count();

                var response = new DashboardResponse
                {
                    TodaysRegisteredUserCount = todaysRegisteredUserCount,
                    WeeklyRegisteredUserCount = weeklyRegisteredUserCount,
                    MonthlyRegisteredUserCount = monthlyRegisteredUserCount,
                    ProductCount = productCount,
                    StoreCount = storeCount,
                    ContactRequestCount = contactRequestCount,
                    BlogCount = blogCount,
                };

                return await Task.FromResult(new BaseResponse { IsSuccess = true, Data = JsonConvert.SerializeObject(response) });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new BaseResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    Data = JsonConvert.SerializeObject(ex)
                });
            }
        }
        #endregion

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
