using azara.models.Requests.POSStore;
using azara.models.Responses.POSStore;

namespace azara.api.Helpers
{
    public class POSStoreHelper : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        IMapper Mapper { get; set; }

        public POSStoreHelper(
            AzaraContext DbContext,
            ICrypto Crypto,
            IMapper Mapper)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
            this.Mapper = Mapper;
        }

        #endregion Constructor

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

        #region 1. Store Get By Id

        public async Task<POSStoreGetByIdResponse> POSStoreGetById([FromBody] POSStoreGetByIdRequest request)
        {

            var store = DbContext.POSStores.FirstOrDefault(x => x.Id.Equals(request.Id));

            if (store == null)
                throw new ApiException("error_posstore_not_found");


            var response = new POSStoreGetByIdResponse
            {
                Name = store.Name,
                StoreNumber = store.StoreNumber,
                OnHandStore01 = store.OnHandStore01,
                OnHandStore02 = store.OnHandStore02,
                OnHandStore03 = store.OnHandStore03,
                OnHandStore04 = store.OnHandStore04,
                OnHandStore05 = store.OnHandStore05,
                OnHandStore06 = store.OnHandStore06,
                OnHandStore07 = store.OnHandStore07,
                OnHandStore08 = store.OnHandStore08,
                OnHandStore09 = store.OnHandStore09,
                OnHandStore10 = store.OnHandStore10,
                OnHandStore11 = store.OnHandStore11,
                OnHandStore12 = store.OnHandStore12,
                OnHandStore13 = store.OnHandStore13,
                OnHandStore14 = store.OnHandStore14,
                OnHandStore15 = store.OnHandStore15,
                OnHandStore16 = store.OnHandStore16,
                OnHandStore17 = store.OnHandStore17,
                OnHandStore18 = store.OnHandStore18,
                OnHandStore19 = store.OnHandStore19,
                OnHandStore20 = store.OnHandStore20
            };

            return response;
        }

        #endregion 1. Store Get By Id 

        #region 2. Store Get List Without Pagination
        public async Task<List<POSStoreGetListResponse>> POSStoreGetListAsync([FromBody] POSStoreGetListRequest request)
        {
            var storeList = await (from st in DbContext.POSStores
                                   select new POSStoreGetListResponse
                                   {
                                       Name = st.Name,
                                       StoreNumber = st.StoreNumber,
                                       OnHandStore01 = st.OnHandStore01,
                                       OnHandStore02 = st.OnHandStore02,
                                       OnHandStore03 = st.OnHandStore03,
                                       OnHandStore04 = st.OnHandStore04,
                                       OnHandStore05 = st.OnHandStore05,
                                       OnHandStore06 = st.OnHandStore06,
                                       OnHandStore07 = st.OnHandStore07,
                                       OnHandStore08 = st.OnHandStore08,
                                       OnHandStore09 = st.OnHandStore09,
                                       OnHandStore10 = st.OnHandStore10,
                                       OnHandStore11 = st.OnHandStore11,
                                       OnHandStore12 = st.OnHandStore12,
                                       OnHandStore13 = st.OnHandStore13,
                                       OnHandStore14 = st.OnHandStore14,
                                       OnHandStore15 = st.OnHandStore15,
                                       OnHandStore16 = st.OnHandStore16,
                                       OnHandStore17 = st.OnHandStore17,
                                       OnHandStore18 = st.OnHandStore18,
                                       OnHandStore19 = st.OnHandStore19,
                                       OnHandStore20 = st.OnHandStore20,

                                   }).ToListAsync();

            return storeList;
        }

        #endregion 2. Store Get List Without Pagination

        #region 3. Store Update
        public async Task<POSStoreUpdateResponse> POSStoreUpdate([FromBody] POSStoreUpdateRequest request)
        {
            var posstore = DbContext.POSStores.FirstOrDefault(x => x.Id.Equals(request.Id));

            if (posstore == null)
                throw new ApiException("error_posstore_not_found");

            posstore.Name = request.Name;
            DbContext.SaveChanges();

            var response = new POSStoreUpdateResponse
            {
                Id = request.Id,
                Name = request.Name,
            };

            return response;
        }
        #endregion 3. Store Update


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
