using azara.models.Responses.POSCustomerRewards;

namespace azara.api.Helpers
{
    public class POSCustomerRewardsHelper : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        public POSCustomerRewardsHelper(
            AzaraContext DbContext,
            ICrypto Crypto)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
        }

        #endregion Constructor

        #region 2. Get POSCustomerRewards List

        public async Task<PaginationResponse> POSCustomerRewardsGetListAsync([FromBody] PaginationRequest request)
        {
            var posCustomerRewardsList = await (from S in DbContext.POSRewardTbl

                                                select new POSCustomerRewardsResponse
                                                {
                                                    //Id = S.Id,
                                                    //ListId = S.ListId,
                                                    //AccountBalance = S.AccountBalance,
                                                    //AccountLimit = S.AccountLimit,
                                                    //AmountPastDue = S.AmountPastDue,
                                                    //CompanyName = S.CompanyName,
                                                    //CustomerDiscPercent = S.CustomerDiscPercent,
                                                    //CustomerDiscType = S.CustomerDiscType,
                                                    //CustomerType = S.CustomerType,
                                                    //CustomerID = S.CustomerID,
                                                    //DefaultShipAddress = S.DefaultShipAddress,
                                                    //EMail = S.EMail,
                                                    //FirstName = S.FirstName,
                                                    FullName = S.FullName,
                                                    //IsAcceptingChecks = S.IsAcceptingChecks,
                                                    //IsNoShipToBilling = S.IsNoShipToBilling,
                                                    //IsOkToEMail = S.IsOkToEMail,
                                                    //IsRewardsMember = S.IsRewardsMember,
                                                    //IsUsingChargeAccount = S.IsUsingChargeAccount,
                                                    //IsUsingWithQB = S.IsUsingWithQB,
                                                    //LastName = S.LastName,
                                                    //LastSale = S.LastSale,
                                                    //Notes = S.Notes,
                                                    //Phone = S.Phone,
                                                    //Phone2 = S.Phone2,
                                                    //Phone3 = S.Phone3,
                                                    //Phone4 = S.Phone4,
                                                    //PriceLevelNumber = S.PriceLevelNumber,
                                                    //Salutation = S.Salutation,
                                                    //StoreExchangeStatus = S.StoreExchangeStatus,
                                                    //TaxCategory = S.TaxCategory,
                                                    //WebNumber = S.WebNumber,
                                                    //BillAddressCity = S.BillAddressCity,
                                                    //BillAddressCountry = S.BillAddressCountry,
                                                    //BillAddressPostalCode = S.BillAddressPostalCode,
                                                    //BillAddressState = S.BillAddressState,
                                                    //BillAddressStreet = S.BillAddressStreet,
                                                    //BillAddressStreet2 = S.BillAddressStreet2,
                                                    //RewardSeqNo = S.RewardSeqNo,
                                                    //RewardAmount = S.RewardAmount,
                                                    //RewardPercent = S.RewardPercent,
                                                    //RewardEarnedDate = S.RewardEarnedDate,
                                                    //RewardMatureDate = S.RewardMatureDate,
                                                    RewardExpirationDate = S.RewardExpirationDate,
                                                    RewardStatus = S.RewardStatus
                                                    //FQPrimaryKey = S.FQPrimaryKey,
                                                    //CustomFieldOther = S.CustomFieldOther,
                                                    //CustomFieldSalesLocation = S.CustomFieldSalesLocation,
                                                    //CustomFieldSerial = S.CustomFieldSerial,
                                                    //CustomFieldSerial1 = S.CustomFieldSerial1,
                                                    //CustomFieldSerial2 = S.CustomFieldSerial2
                                                }).ToListAsync();


            if (posCustomerRewardsList == null)
                throw new ApiException("error_store_not_found");

            var total = posCustomerRewardsList.Count();
            var totalPages = POSCustomerRewardsHelper.CalculateTotalPages(total, request.PageSize);
            var posCustomerRewardsPaginationList = posCustomerRewardsList.Take(request.PageSize);

            var response = new
            {
                Total = total,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                Offset = request.PageNo,
                Details = posCustomerRewardsPaginationList
            };

            return new PaginationResponse
            {
                Total = response.Total,
                TotalPages = totalPages,
                OffSet = response.Offset,
                Details = response.Details
            };
        }

        #endregion 2. Get POSCustomerRewards List

        #region 3. Get POSCustomer List

        public async Task<PaginationResponse> POSCustomerGetListAsync([FromBody] PaginationRequest request)
        {
            var posCustomerList = await (from S in DbContext.POSCustomer

                                         select new POSCustomerResponse
                                         {
                                             ListId = S.ListId,
                                             TimeCreated = S.TimeCreated,
                                             TimeModified = S.TimeModified,
                                             AccountBalance = S.AccountBalance,
                                             AccountLimit = S.AccountLimit,
                                             AmountPastDue = S.AmountPastDue,
                                             CompanyName = S.CompanyName,
                                             CustomerDiscPercent = S.CustomerDiscPercent,
                                             CustomerDiscType = S.CustomerDiscType,
                                             CustomerType = S.CustomerType,
                                             CustomerID = S.CustomerID,
                                             DefaultShipAddress = S.DefaultShipAddress,
                                             EMail = S.EMail,
                                             FirstName = S.FirstName,
                                             FullName = S.FullName,
                                             IsAcceptingChecks = S.IsAcceptingChecks,
                                             IsNoShipToBilling = S.IsNoShipToBilling,
                                             IsOkToEMail = S.IsOkToEMail,
                                             IsRewardsMember = S.IsRewardsMember,
                                             IsUsingChargeAccount = S.IsUsingChargeAccount,
                                             IsUsingWithQB = S.IsUsingWithQB,
                                             LastName = S.LastName,
                                             LastSale = S.LastSale,
                                             Notes = S.Notes,
                                             Phone = S.Phone,
                                             Phone2 = S.Phone2,
                                             Phone3 = S.Phone3,
                                             Phone4 = S.Phone4,
                                             PriceLevelNumber = S.PriceLevelNumber,
                                             Salutation = S.Salutation,
                                             StoreExchangeStatus = S.StoreExchangeStatus,
                                             TaxCategory = S.TaxCategory,
                                             WebNumber = S.WebNumber,
                                             BillAddressCity = S.BillAddressCity,
                                             BillAddressCountry = S.BillAddressCountry,
                                             BillAddressPostalCode = S.BillAddressPostalCode,
                                             BillAddressState = S.BillAddressState,
                                             BillAddressStreet = S.BillAddressStreet,
                                             BillAddressStreet2 = S.BillAddressStreet2,
                                             CustomFieldOther = S.CustomFieldOther,
                                             CustomFieldSalesLocation = S.CustomFieldSalesLocation,
                                             CustomFieldSerial = S.CustomFieldSerial,
                                             CustomFieldSerial1 = S.CustomFieldSerial1,
                                             CustomFieldSerial2 = S.CustomFieldSerial2

                                         }).ToListAsync();


            if (posCustomerList == null)
                throw new ApiException("error_store_not_found");

            var total = posCustomerList.Count();
            var totalPages = POSCustomerRewardsHelper.CalculateTotalPages(total, request.PageSize);
            var posCustomerPaginationList = posCustomerList.Take(request.PageSize);

            var response = new
            {
                Total = total,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                Offset = request.PageNo,
                Details = posCustomerPaginationList
            };

            return new PaginationResponse
            {
                Total = response.Total,
                TotalPages = totalPages,
                OffSet = response.Offset,
                Details = response.Details
            };
        }

        #endregion 3. Get POSCustomer List


        #region 3. Get POSInventory List

        public async Task<PaginationResponse> POSInventoryAsync([FromBody] PaginationRequest request)
        {
            var posInventoryList = await (from I in DbContext.POSInventory

                                          select new POSItemInventoryResponse
                                          {
                                              ListId = I.ListId,
                                              TimeCreated = I.TimeCreated,
                                              TimeModified = I.TimeModified,
                                              ALU = I.ALU,
                                              Attribute = I.Attribute,
                                              COGSAccount = I.COGSAccount,
                                              Cost = I.Cost,
                                              DepartmentCode = I.DepartmentCode,
                                              DepartmentListID = I.DepartmentListID,
                                              Desc1 = I.Desc1,
                                              IncomeAccount = I.IncomeAccount,
                                              IsBelowReorder = I.IsBelowReorder,
                                              IsEligibleForCommission = I.IsEligibleForCommission,
                                              IsPrintingTags = I.IsPrintingTags,
                                              IsUnorderable = I.IsUnorderable,
                                              ItemNumber = I.ItemNumber,
                                              ItemType = I.ItemType,
                                              Keywords = I.Keywords,
                                              LastReceived = I.LastReceived,
                                              MarginPercent = I.MarginPercent,
                                              MarkupPercent = I.MarkupPercent,
                                              MSRP = I.MSRP,
                                              OnHandStore01 = I.OnHandStore01,
                                              OnHandStore02 = I.OnHandStore02,
                                              OrderByUnit = I.OrderByUnit,
                                              OrderCost = I.OrderCost,
                                              Price1 = I.Price1,
                                              Price2 = I.Price2,
                                              Price3 = I.Price3,
                                              Price4 = I.Price4,
                                              Price5 = I.Price5,
                                              QuantityOnCustomerOrder = I.QuantityOnCustomerOrder,
                                              QuantityOnHand = I.QuantityOnHand,
                                              QuantityOnOrder = I.QuantityOnOrder,
                                              QuantityOnPendingOrder = I.QuantityOnPendingOrder,
                                              ReorderPoint = I.ReorderPoint,
                                              ReorderPointStore01 = I.ReorderPointStore01,
                                              SellByUnit = I.SellByUnit,
                                              SerialFlag = I.SerialFlag,
                                              Size = I.Size,
                                              StoreExchangeStatus = I.StoreExchangeStatus,
                                              UnitOfMeasure = I.UnitOfMeasure,
                                              UPC = I.UPC,
                                              VendorCode = I.VendorCode,
                                              VendorListID = I.VendorListID,
                                              WebDesc = I.WebDesc,
                                              WebPrice = I.WebPrice,
                                              Manufacturer = I.Manufacturer,
                                              Weight = I.Weight,
                                              WebSKU = I.WebSKU,
                                              WebCategories = I.WebCategories,
                                              UnitOfMeasure1ALU = I.UnitOfMeasure1ALU,
                                              UnitOfMeasure1MSRP = I.UnitOfMeasure1MSRP,
                                              UnitOfMeasure1NumberOfBaseUnits = I.UnitOfMeasure1NumberOfBaseUnits,
                                              UnitOfMeasure1Price1 = I.UnitOfMeasure1Price1,
                                              UnitOfMeasure1Price2 = I.UnitOfMeasure1Price2,
                                              UnitOfMeasure1Price3 = I.UnitOfMeasure1Price3,
                                              UnitOfMeasure1Price4 = I.UnitOfMeasure1Price4,
                                              UnitOfMeasure1Price5 = I.UnitOfMeasure1Price5,
                                              UnitOfMeasure1UnitOfMeasure = I.UnitOfMeasure1UnitOfMeasure,
                                              UnitOfMeasure1UPC = I.UnitOfMeasure1UPC,
                                              UnitOfMeasure2ALU = I.UnitOfMeasure2ALU,
                                              UnitOfMeasure2MSRP = I.UnitOfMeasure2MSRP,
                                              UnitOfMeasure2NumberOfBaseUnits = I.UnitOfMeasure2NumberOfBaseUnits,
                                              UnitOfMeasure2Price1 = I.UnitOfMeasure2Price1,
                                              UnitOfMeasure2Price2 = I.UnitOfMeasure2Price2,
                                              UnitOfMeasure2Price3 = I.UnitOfMeasure2Price3,
                                              UnitOfMeasure2Price4 = I.UnitOfMeasure2Price4,
                                              UnitOfMeasure2Price5 = I.UnitOfMeasure2Price5,
                                              UnitOfMeasure2UnitOfMeasure = I.UnitOfMeasure2UnitOfMeasure,
                                              UnitOfMeasure2UPC = I.UnitOfMeasure2UPC,
                                              UnitOfMeasure3ALU = I.UnitOfMeasure3ALU,
                                              UnitOfMeasure3MSRP = I.UnitOfMeasure3MSRP,
                                              UnitOfMeasure3NumberOfBaseUnits = I.UnitOfMeasure3NumberOfBaseUnits,
                                              UnitOfMeasure3Price1 = I.UnitOfMeasure3Price1,
                                              UnitOfMeasure3Price2 = I.UnitOfMeasure3Price2,
                                              UnitOfMeasure3Price3 = I.UnitOfMeasure3Price3,
                                              UnitOfMeasure3Price4 = I.UnitOfMeasure3Price4,
                                              UnitOfMeasure3Price5 = I.UnitOfMeasure3Price5,
                                              UnitOfMeasure3UnitOfMeasure = I.UnitOfMeasure3UnitOfMeasure,
                                              UnitOfMeasure3UPC = I.UnitOfMeasure3UPC,
                                              VendorInfo2ALU = I.VendorInfo2ALU,
                                              VendorInfo2OrderCost = I.VendorInfo2OrderCost,
                                              VendorInfo2UPC = I.VendorInfo2UPC,
                                              VendorInfo2VendorListID = I.VendorInfo2VendorListID,
                                              VendorInfo3ALU = I.VendorInfo3ALU,
                                              VendorInfo3OrderCost = I.VendorInfo3OrderCost,
                                              VendorInfo3UPC = I.VendorInfo3UPC,
                                              VendorInfo3VendorListID = I.VendorInfo3VendorListID,
                                              VendorInfo4ALU = I.VendorInfo4ALU,
                                              VendorInfo4OrderCost = I.VendorInfo4OrderCost,
                                              VendorInfo4UPC = I.VendorInfo4UPC,
                                              VendorInfo4VendorListID = I.VendorInfo4VendorListID,
                                              VendorInfo5ALU = I.VendorInfo5ALU,
                                              VendorInfo5OrderCost = I.VendorInfo5OrderCost,
                                              VendorInfo5UPC = I.VendorInfo5UPC,
                                              VendorInfo5VendorListID = I.VendorInfo5VendorListID

                                          }).ToListAsync();


            if (posInventoryList == null)
                throw new ApiException("error_store_not_found");

            var total = posInventoryList.Count();
            var totalPages = POSCustomerRewardsHelper.CalculateTotalPages(total, request.PageSize);
            var posCustomerPaginationList = posInventoryList.Take(request.PageSize);

            var response = new
            {
                Total = total,
                TotalPages = totalPages,
                PageSize = request.PageSize,
                Offset = request.PageNo,
                Details = posCustomerPaginationList
            };

            return new PaginationResponse
            {
                Total = response.Total,
                TotalPages = totalPages,
                OffSet = response.Offset,
                Details = response.Details
            };
        }
        #endregion 3. Get POSInventory List


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

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
