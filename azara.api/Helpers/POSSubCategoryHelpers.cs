using azara.models.Requests.POSInventory;
using azara.models.Requests.POSSubCategory;
using azara.models.Responses.POSSubCategory;


namespace azara.api.Helpers
{
    public class POSSubCategoryHelpers : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        public POSSubCategoryHelpers(
            AzaraContext DbContext,
            ICrypto Crypto)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
        }

        #endregion Constructor

        #region 1. POSSubCategory Insert Update
        public async Task<dynamic> POSSubCategoryInsertAndUpdate()
        {

            // Take a list of a POSSubCategory

            var listOfPosSubCategory = await DbContext.POSSubCategory.ToListAsync();

            if (listOfPosSubCategory.Count == 0)
            {
                var InvenoryDemo1 = await DbContext.POSInventory.ToListAsync();
                foreach (var inventory in InvenoryDemo1)
                {
                    var departmentDetails = await DbContext.POSDepartments.FirstOrDefaultAsync(x => x.ListId.Equals(inventory.DepartmentListID));
                    if (InvenoryDemo1 != null)
                    {
                        await DbContext.AddRangeAsync(new POSSubCategoryEntity
                        {
                            ListId = inventory.ListId,
                            Created = inventory.TimeCreated,
                            Modified = inventory.TimeModified,
                            Name = inventory.Desc1,
                            Attribute = inventory.Attribute,
                            Active = true,
                            DepartmentListId = inventory.DepartmentListID,
                            //Image = inventory.Image,
                            DepartmentName = departmentDetails.DepartmentName,
                        });
                    }

                }
            }

            await DbContext.SaveChangesAsync();

            // Top 1 time modified from POSSubCategory desc based on ListId

            var timeModified = await DbContext.POSSubCategory.OrderByDescending(o => o.Modified).Select(s => s.Modified).FirstOrDefaultAsync();

            // Take a list from PosInventory which is greater than Time Modified

            var newPosInvetoryList = await DbContext.POSInventory.Where(w => w.TimeModified > timeModified.Value).ToListAsync();

            // Check new List with ListId of that if List Id not exists in PosSubCategory then Insert or else update

            foreach(var inventory in newPosInvetoryList)
            {
                var InventoryData = await DbContext.POSSubCategory.FirstOrDefaultAsync(o => o.ListId.Equals(inventory.ListId));
                var departmentDetails = await DbContext.POSDepartments.FirstOrDefaultAsync(x => x.ListId.Equals(inventory.DepartmentListID));

                if (InventoryData == null)
                {                    
                    await DbContext.AddRangeAsync(new POSSubCategoryEntity
                    {
                        ListId = inventory.ListId,
                        Name = inventory.Desc1,
                        Attribute = inventory.Attribute,
                        Created = inventory.TimeCreated,
                        Modified = inventory.TimeModified,
                        DepartmentListId = inventory.DepartmentListID,
                        DepartmentName = departmentDetails.DepartmentName,
                        Active = true,
                        Image = inventory.Image,
                    });
                }
                else
                {
                    InventoryData.Modified = inventory.TimeModified;
                    InventoryData.Name= inventory.Desc1;
                    InventoryData.Attribute = inventory.Attribute;
                    InventoryData.DepartmentListId = inventory.DepartmentListID;
                    InventoryData.DepartmentName = departmentDetails?.DepartmentName;
                    InventoryData.Active = inventory.Active;
                    InventoryData.Image = inventory.Image;
                }
            }

            await DbContext.SaveChangesAsync();

            return timeModified;


        }
        #endregion

        // need to groupby and distinct here also. 
        #region 2.POSSubCategory List Without Pagination
        public async Task<List<POSSubCategoryListResponse>> POSSubCategoryList([FromBody] POSInventoryDistinctSubCategoryList request)
        {

            var groupedCustomers = await DbContext.POSSubCategory
             .GroupBy(c => new { c.Name, c.DepartmentName })
             .Select(g => new POSSubCategoryListResponse { 
                 Name = g.Key.Name , 
                 DepartmentListName = g.Key.DepartmentName,
                 Image = g.FirstOrDefault().Image,
                 Id = g.FirstOrDefault().Id,
                 Active = g.FirstOrDefault().Active,
                 Attribute = g.FirstOrDefault().Attribute,
                 DepartmentListId = g.FirstOrDefault().DepartmentListId
             })
             .Where(g=> g.DepartmentListId == request.DepartmentListID)
            .ToListAsync();

            return groupedCustomers;

        }
        #endregion

        #region 3. POSSubCategory By Id
        public async Task<POSSubCategoryListResponse> POSSubCategoryGetById([FromBody] POSSubCategoryByIdRequest request)
        {
            var possubcategoty = await DbContext.POSSubCategory.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));


            if (possubcategoty == null)
                throw new ApiException("error_possubcategory_not_found");

            var response = new POSSubCategoryListResponse
            {

                Name = possubcategoty.Name,
                Image = possubcategoty.Image,
                Active = possubcategoty.Active,
                Attribute = possubcategoty.Attribute,
                DepartmentListId = possubcategoty.DepartmentListId
            };

            return response;

        }
        #endregion

        #region 4. POSSubCategory Update
        public async Task<POSSubCategoryUpdteResponse> POSSubCategoryUpdate([FromBody] POSSubCategoryUpdateRequest request)
        {
            var possubcategory = DbContext.POSSubCategory.FirstOrDefault(x => x.Id.Equals(request.ProductSubCategoryId));

            if (possubcategory == null)
                throw new ApiException("error_possubcategory_not_found");

            possubcategory.Id = request.ProductSubCategoryId;
            possubcategory.Image = request.Image;

            var response = new POSSubCategoryUpdteResponse
            {
                //Id = possubcategory.Id,
                Image = possubcategory.Image,
            };
            DbContext.SaveChanges();

            return response;
        }
        #endregion

        #region 5.POSSubCategory Staus Update
        public async Task<BaseResponse> POSSubCategoryStatusUpdate([FromBody] BaseIntStatusUpdateRequest request)
        {
            var posSubCategory = await DbContext.POSSubCategory.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

            if (posSubCategory == null)
                throw new ApiException("error_possubcategory_not_found");

            posSubCategory.Active = request.Active;
            DbContext.Update(posSubCategory);
            await DbContext.SaveChangesAsync();
            return new BaseResponse { IsSuccess = true };
        }
        #endregion

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
