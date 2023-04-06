using azara.models.Requests.POSDepartment;
using azara.models.Responses.POSDepartment;
using static System.Net.Mime.MediaTypeNames;

namespace azara.api.Helpers
{
    public class POSDepartmentHelper : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        public POSDepartmentHelper(
            AzaraContext DbContext,
            ICrypto Crypto)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
        }

        #endregion Constructor

        #region 1. POS Department Get By Id

        public async Task<POSDepartmentGetByIdResponse> POSDepartmentGetByID([FromBody] POSDepartmentGetByIdRequest request)
        {
            var department = await DbContext.POSDepartments.FirstOrDefaultAsync(x => x.ListId.Equals(request.Id));
            var inventory = await (from POSD in DbContext.POSDepartments
                                   join POSI in DbContext.POSInventory
                                   on POSD.ListId equals POSI.DepartmentListID
                                   where POSD.DepartmentCode == department.DepartmentCode

                                   select new DepartmentGetByIdResponse
                                   {
                                       Desc1 = POSI.Desc1,
                                       DepartmentCode = POSI.DepartmentCode,
                                       Cost = POSI.Cost,
                                       Keyword = POSI.Keywords,
                                       QuantityOnHand = POSI.QuantityOnHand,
                                       OnHandStore01 = POSI.OnHandStore01,
                                       OnHandStore02 = POSI.OnHandStore02,
                                       Price1 = POSI.Price1,
                                       Price2 = POSI.Price2,
                                       Price3 = POSI.Price3,
                                       Price4 = POSI.Price4,
                                       Price5 = POSI.Price5,
                                       Description = POSI.Description,
                                   }
                                   ).ToListAsync();


            if (department == null)
            {
                throw new Exception("error_department_not_found");
            }

            if (inventory == null)
            {
                throw new Exception("error_inventory_not_found");
            }

            var response = new POSDepartmentGetByIdResponse
            {
                Id = department.Id,
                ListId = department.ListId,
                TimeCreated = department.TimeCreated,
                TimeModified = department.TimeModified,
                DefaultMarginPercent = department.DefaultMarginPercent,
                DefaultMarkupPercent = department.DefaultMarkupPercent,
                DepartmentCode = department.DepartmentCode,
                TaxCode = department.TaxCode,
                DepartmentName = department.DepartmentName,
                StoreExchangeStatus = department.StoreExchangeStatus,
                Image = department.Image,
                DepartmentGetByIdResponse = inventory
            };

            return response;
        }
        #endregion 1. POS Department Get By Id

        #region 2. POS Department Update
        public async Task<POSDepartmentUpdateResponse> POSDepartmentUpdate([FromBody] POSDepartmentUpdateRequest request)
        {
            var department = DbContext.POSDepartments.FirstOrDefault(x => x.Id.Equals(request.Id));

            if (department == null)
                throw new ApiException("error_department_not_found");

            department.Id = request.Id;
            department.Image = request.Image;
            department.TimeCreated = request.TimeCreated;
            department.TimeModified = DateTime.UtcNow;

            var response = new POSDepartmentUpdateResponse
            {
                Id = department.Id,
                Image = department.Image,
                TimeCreated = department.TimeCreated
            };

            DbContext.SaveChanges();

            return response;
        }
        #endregion

        #region 3. POS Department List without Pagination
        public async Task<List<DepartmentListResponse>> POSDepartmentList([FromBody] POSDepartmentListRequest request)
        {
            var departmentList = await (
                from DEPT in DbContext.POSDepartments
                select new DepartmentListResponse
                {
                    Id = DEPT.Id,
                    ListId = DEPT.ListId,

                    DefaultMarginPercent = DEPT.DefaultMarginPercent,
                    DefaultMarkupPercent = DEPT.DefaultMarkupPercent,
                    DepartmentCode = DEPT.DepartmentCode,
                    DepartmentName = DEPT.DepartmentName,
                    Image = DEPT.Image,
                    IsActive = DEPT.Active

                }).Where(x => x.IsActive == true).ToListAsync();

            return departmentList;
        }

        #endregion 3. POS Department List without Pagination

        #region 4. POSDepartment Add Update
        //Temptory table to POSDepartment Add/Update
        public async Task<List<POSDepartmentDemoEntity>> POSDepartmentAddUpdate()
        {
                var timeModified = await DbContext.POSDepartments.OrderByDescending(o => o.TimeModified).Select(s => s.TimeModified).FirstOrDefaultAsync();

            if (timeModified == null)
            {
                var departmentDemo1 = await DbContext.POSDepartmentDemo.ToListAsync();
                foreach (var department in departmentDemo1)
                {
                    var departmentData = await DbContext.POSDepartments.FirstOrDefaultAsync(o => o.ListId.Equals(department.ListId));

                    if (departmentData == null)
                    {
                        await DbContext.AddRangeAsync(new POSDepartmentEntity
                        {
                            ListId = department.ListId,
                            TimeCreated = department.TimeCreated,
                            TimeModified = department.TimeModified,
                            DefaultMarginPercent = department.DefaultMarginPercent,
                            DefaultMarkupPercent = department.DefaultMarkupPercent,
                            DepartmentCode = department.DepartmentCode,
                            DepartmentName = department.DepartmentName,
                            StoreExchangeStatus = department.StoreExchangeStatus,
                            TaxCode = department.TaxCode,
                            Active = true,
                            Image = department.Image,
                        });
                    }
                }
                try
                {
                    await DbContext.SaveChangesAsync();
                }
                catch (Exception e) { }
                return departmentDemo1;
            }

            var departmentDemo = await DbContext.POSDepartmentDemo.Where(w => w.TimeModified > timeModified.Value).ToListAsync();

            if (departmentDemo != null)
            {
                foreach (var department in departmentDemo)
                {
                    var departmentData = await DbContext.POSDepartments.FirstOrDefaultAsync(o => o.ListId.Equals(department.ListId));

                    if (departmentData == null)
                    {
                        await DbContext.AddRangeAsync(new POSDepartmentEntity
                        {
                            ListId = department.ListId,
                            TimeCreated = department.TimeCreated,
                            TimeModified = department.TimeModified,
                            DefaultMarginPercent = department.DefaultMarginPercent,
                            DefaultMarkupPercent = department.DefaultMarkupPercent,
                            DepartmentCode = department.DepartmentCode,
                            DepartmentName = department.DepartmentName,
                            StoreExchangeStatus = department.StoreExchangeStatus,
                            TaxCode = department.TaxCode,
                            Active = true,
                            Image = department.Image,
                        });
                    }
                    else
                    {
                        departmentData.TimeModified = department.TimeModified;
                        departmentData.DefaultMarginPercent = department.DefaultMarginPercent;
                        departmentData.DefaultMarkupPercent = department.DefaultMarkupPercent;
                        departmentData.DepartmentCode = department.DepartmentCode;
                        departmentData.DepartmentName = department.DepartmentName;
                        departmentData.StoreExchangeStatus = department.StoreExchangeStatus;
                        departmentData.TaxCode = department.TaxCode;
                        departmentData.Image = department.Image;


                        var response = new POSDepartmentGetByIdResponse
                        {
                            //TimeCreated = inventory.TimeCreated,
                            TimeModified = department.TimeModified,
                            DefaultMarginPercent = department.DefaultMarginPercent,
                            DefaultMarkupPercent = department.DefaultMarkupPercent,
                            DepartmentCode = department.DepartmentCode,
                            DepartmentName = department.DepartmentName,
                            StoreExchangeStatus = department.StoreExchangeStatus,
                            TaxCode = department.TaxCode,
                            Image = department.Image,
                        };
                        DbContext.SaveChanges();
                    }
                }
                try
                {
                    await DbContext.SaveChangesAsync();
                }
                catch (Exception e) { }
            }

            return departmentDemo;
        }
        #endregion

        #region 5. POSDepartment Status Update

        public async Task<BaseResponse> POSDepartmentStatusUpdate([FromBody] BaseListIdStatusUpdateRequest request)
        {
            var posDepartment = await DbContext.POSDepartments.FirstOrDefaultAsync(x => x.ListId.Equals(request.Id));

            if (posDepartment == null)
                throw new ApiException("error_posinventory_not_found");

            posDepartment.Active = request.Active;
            DbContext.Update(posDepartment);
            await DbContext.SaveChangesAsync();
            return new BaseResponse { IsSuccess = true };
        }

        #endregion

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
