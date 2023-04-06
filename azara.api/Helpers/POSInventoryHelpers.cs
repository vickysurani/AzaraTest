using azara.models.Requests.POSInventory;
using azara.models.Responses.POSInventory;

namespace azara.api.Helpers
{
    public class POSInventoryHelpers : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        public POSInventoryHelpers(
            AzaraContext DbContext,
            ICrypto Crypto)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
        }

        #endregion Constructor

        #region 1. POS Inventory Get By Id

        public async Task<POSInventoryGetByIdResponse> POSInventoryGetById([FromBody] POSInventoryGetByIdRequest request)
        {
            var inventory = await DbContext.POSInventory.FirstOrDefaultAsync(x => x.ListId.Equals(request.Id));

            if (inventory == null)
                throw new ApiException("error_inventory_not_found");

            var store = await DbContext.Stores.ToListAsync();

            var storeDetails = new List<StoreDetails>();

            if (inventory.OnHandStore01 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore01"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }

            }

            if (inventory.OnHandStore02 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore02"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore03 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore03"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore04 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore04"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore05 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore05"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore06 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore06"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore07 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore07"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore08 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore08"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore09 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore09"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore10 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore10"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore11 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore11"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore12 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore12"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore13 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore13"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore14 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore14"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore15 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore15"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore16 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore16"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore17 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore17"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore18 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore18"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore19 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore19"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            if (inventory.OnHandStore20 != 0)
            {
                var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore20"));
                if (d != null)
                {
                    storeDetails.Add(new StoreDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                }

                if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                {
                    foreach (var item in storeDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                    {
                        if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                            item.IsLocationFound = true;
                    }
                }
            }

            var response = new POSInventoryGetByIdResponse
            {
                Id = inventory.Id,
                ListId = inventory.ListId,
                TimeCreated = inventory.TimeCreated,
                TimeModified = inventory.TimeModified,
                ALU = inventory.ALU,
                Attribute = inventory.Attribute,
                COGSAccount = inventory.COGSAccount,
                Cost = inventory.Cost,
                DepartmentCode = inventory.DepartmentCode,
                DepartmentListID = inventory.DepartmentListID,
                Desc1 = inventory.Desc1,
                Description = inventory.Description,
                IncomeAccount = inventory.IncomeAccount,
                IsBelowReorder = inventory.IsBelowReorder,
                IsEligibleForCommission = inventory.IsEligibleForCommission,
                IsPrintingTags = inventory.IsPrintingTags,
                IsUnorderable = inventory.IsUnorderable,
                ItemNumber = inventory.ItemNumber,
                ItemType = inventory.ItemType,
                Keywords = inventory.Keywords,
                LastReceived = inventory.LastReceived,
                MarginPercent = inventory.MarginPercent,
                MarkupPercent = inventory.MarkupPercent,
                MSRP = inventory.MSRP,
                OnHandStore01 = inventory.OnHandStore01,
                OnHandStore02 = inventory.OnHandStore02,
                OnHandStore03 = inventory.OnHandStore03,
                OnHandStore04 = inventory.OnHandStore04,
                OnHandStore05 = inventory.OnHandStore05,
                OnHandStore06 = inventory.OnHandStore06,
                OnHandStore07 = inventory.OnHandStore07,
                OnHandStore08 = inventory.OnHandStore08,
                OnHandStore09 = inventory.OnHandStore09,
                OnHandStore10 = inventory.OnHandStore10,
                OnHandStore11 = inventory.OnHandStore11,
                OnHandStore12 = inventory.OnHandStore12,
                OnHandStore13 = inventory.OnHandStore13,
                OnHandStore14 = inventory.OnHandStore14,
                OnHandStore15 = inventory.OnHandStore15,
                OnHandStore16 = inventory.OnHandStore16,
                OnHandStore17 = inventory.OnHandStore17,
                OnHandStore18 = inventory.OnHandStore18,
                OnHandStore19 = inventory.OnHandStore19,
                OnHandStore20 = inventory.OnHandStore20,
                OrderByUnit = inventory.OrderByUnit,
                OrderCost = inventory.OrderCost,
                Price1 = inventory.Price1,
                Price2 = inventory.Price2,
                Price3 = inventory.Price3,
                Price4 = inventory.Price4,
                Price5 = inventory.Price5,
                QuantityOnCustomerOrder = inventory.QuantityOnCustomerOrder,
                QuantityOnHand = inventory.QuantityOnHand,
                QuantityOnOrder = inventory.QuantityOnOrder,
                QuantityOnPendingOrder = inventory.QuantityOnPendingOrder,
                ReorderPoint = inventory.ReorderPoint,
                ReorderPointStore01 = inventory.ReorderPointStore01,
                SellByUnit = inventory.SellByUnit,
                SerialFlag = inventory.SerialFlag,
                Size = inventory.Size,
                StoreExchangeStatus = inventory.StoreExchangeStatus,
                UnitOfMeasure = inventory.UnitOfMeasure,
                UPC = inventory.UPC,
                VendorCode = inventory.VendorCode,
                VendorListID = inventory.VendorListID,
                WebDesc = inventory.WebDesc,
                WebPrice = inventory.WebPrice,
                Manufacturer = inventory.Manufacturer,
                Weight = inventory.Weight,
                WebSKU = inventory.WebSKU,
                WebCategories = inventory.WebCategories,
                UnitOfMeasure1ALU = inventory.UnitOfMeasure1ALU,
                UnitOfMeasure1MSRP = inventory.UnitOfMeasure1MSRP,
                UnitOfMeasure1NumberOfBaseUnits = inventory.UnitOfMeasure1NumberOfBaseUnits,
                UnitOfMeasure1Price1 = inventory.UnitOfMeasure1Price1,
                UnitOfMeasure1Price2 = inventory.UnitOfMeasure1Price2,
                UnitOfMeasure1Price3 = inventory.UnitOfMeasure1Price3,
                UnitOfMeasure1Price4 = inventory.UnitOfMeasure1Price4,
                UnitOfMeasure1Price5 = inventory.UnitOfMeasure1Price5,
                UnitOfMeasure1UnitOfMeasure = inventory.UnitOfMeasure1UnitOfMeasure,
                UnitOfMeasure1UPC = inventory.UnitOfMeasure1UPC,
                UnitOfMeasure2ALU = inventory.UnitOfMeasure2ALU,
                UnitOfMeasure2MSRP = inventory.UnitOfMeasure2MSRP,
                UnitOfMeasure2NumberOfBaseUnits = inventory.UnitOfMeasure2NumberOfBaseUnits,
                UnitOfMeasure2Price1 = inventory.UnitOfMeasure2Price1,
                UnitOfMeasure2Price2 = inventory.UnitOfMeasure2Price2,
                UnitOfMeasure2Price3 = inventory.UnitOfMeasure2Price3,
                UnitOfMeasure2Price4 = inventory.UnitOfMeasure2Price4,
                UnitOfMeasure2Price5 = inventory.UnitOfMeasure2Price5,
                UnitOfMeasure2UnitOfMeasure = inventory.UnitOfMeasure2UnitOfMeasure,
                UnitOfMeasure2UPC = inventory.UnitOfMeasure2UPC,
                UnitOfMeasure3ALU = inventory.UnitOfMeasure3ALU,
                UnitOfMeasure3MSRP = inventory.UnitOfMeasure3MSRP,
                UnitOfMeasure3NumberOfBaseUnits = inventory.UnitOfMeasure3NumberOfBaseUnits,
                UnitOfMeasure3Price1 = inventory.UnitOfMeasure3Price1,
                UnitOfMeasure3Price2 = inventory.UnitOfMeasure3Price2,
                UnitOfMeasure3Price3 = inventory.UnitOfMeasure3Price3,
                UnitOfMeasure3Price4 = inventory.UnitOfMeasure3Price4,
                UnitOfMeasure3Price5 = inventory.UnitOfMeasure3Price5,
                UnitOfMeasure3UnitOfMeasure = inventory.UnitOfMeasure3UnitOfMeasure,
                UnitOfMeasure3UPC = inventory.UnitOfMeasure3UPC,
                VendorInfo2ALU = inventory.VendorInfo2ALU,
                VendorInfo2OrderCost = inventory.VendorInfo2OrderCost,
                VendorInfo2UPC = inventory.VendorInfo2UPC,
                VendorInfo2VendorListID = inventory.VendorInfo2VendorListID,
                VendorInfo3ALU = inventory.VendorInfo3ALU,
                VendorInfo3OrderCost = inventory.VendorInfo3OrderCost,
                VendorInfo3UPC = inventory.VendorInfo3UPC,
                VendorInfo3VendorListID = inventory.VendorInfo3VendorListID,
                VendorInfo4ALU = inventory.VendorInfo4ALU,
                VendorInfo4OrderCost = inventory.VendorInfo4OrderCost,
                VendorInfo4UPC = inventory.VendorInfo4UPC,
                VendorInfo4VendorListID = inventory.VendorInfo4VendorListID,
                VendorInfo5ALU = inventory.VendorInfo5ALU,
                VendorInfo5OrderCost = inventory.VendorInfo5OrderCost,
                VendorInfo5UPC = inventory.VendorInfo5UPC,
                VendorInfo5VendorListID = inventory.VendorInfo5VendorListID,
                Image = inventory.Image,
                StoreDetails = storeDetails,
            };

            return response;
        }

        #endregion 1. POS Inventory Get By Id

        #region 2. POS Inventory Update

        public async Task<POSInventoryUpdateResponse> POSInventoryUpdate([FromBody] POSInventoryUpdateRequest request)
        {
            var inventory = DbContext.POSInventory.FirstOrDefault(x => x.Id.Equals(request.Id));

            if (inventory == null)
                throw new ApiException("error_inventory_not_found");

            inventory.Id = request.Id;
            inventory.Image = request.Image ?? inventory.Image;
            inventory.Description = request.Description;
            inventory.TimeCreated = request.TimeCreated;
            inventory.TimeModified = DateTime.UtcNow;

            var response = new POSInventoryUpdateResponse
            {
                Id = inventory.Id,
                Image = inventory.Image,
                Description = inventory.Description,
                TimeCreated = inventory.TimeCreated
            };

            DbContext.SaveChanges();

            return response;
        }

        #endregion 2. POS Inventory Update

        #region 3. POS Inventory List without Pagination

        public async Task<List<POSInventoryListResponse>> POSInventoryList([FromBody] POSInventoryListRequest request)
        {
            var inventoryList = await (
                from INV in DbContext.POSInventory

                where (string.IsNullOrWhiteSpace(request.Desc1)
                    || INV.Desc1.Contains(request.Desc1))

                select new POSInventoryListResponse
                {
                    Id = INV.Id,
                    ListId = INV.ListId,
                    //TimeCreated = INV.TimeCreated,
                    //TimeModified = INV.TimeModified,
                    //ALU = INV.ALU,
                    Attribute = INV.Attribute,
                    //COGSAccount = INV.COGSAccount,
                    Description = INV.Description,
                    Cost = INV.Cost,
                    DepartmentCode = INV.DepartmentCode,
                    DepartmentListID = INV.DepartmentListID,
                    Desc1 = INV.Desc1,
                    //IncomeAccount = INV.IncomeAccount,
                    //IsBelowReorder = INV.IsBelowReorder,
                    //IsEligibleForCommission = INV.IsEligibleForCommission,
                    //IsPrintingTags = INV.IsPrintingTags,
                    //IsUnorderable = INV.IsUnorderable,
                    //ItemNumber = INV.ItemNumber,
                    //ItemType = INV.ItemType,
                    //Keywords = INV.Keywords,
                    //LastReceived = INV.LastReceived,
                    //MarginPercent = INV.MarginPercent,
                    //MarkupPercent = INV.MarkupPercent,
                    //MSRP = INV.MSRP,
                    OnHandStore01 = INV.OnHandStore01,
                    OnHandStore02 = INV.OnHandStore02,
                    OnHandStore03 = INV.OnHandStore03,
                    OnHandStore04 = INV.OnHandStore04,
                    OnHandStore05 = INV.OnHandStore05,
                    OnHandStore06 = INV.OnHandStore06,
                    OnHandStore07 = INV.OnHandStore07,
                    OnHandStore08 = INV.OnHandStore08,
                    OnHandStore09 = INV.OnHandStore09,
                    OnHandStore10 = INV.OnHandStore10,
                    OnHandStore11 = INV.OnHandStore11,
                    OnHandStore12 = INV.OnHandStore12,
                    OnHandStore13 = INV.OnHandStore13,
                    OnHandStore14 = INV.OnHandStore14,
                    OnHandStore15 = INV.OnHandStore15,
                    OnHandStore16 = INV.OnHandStore16,
                    OnHandStore17 = INV.OnHandStore17,
                    OnHandStore18 = INV.OnHandStore18,
                    OnHandStore19 = INV.OnHandStore19,
                    OnHandStore20 = INV.OnHandStore20,
                    //OrderByUnit = INV.OrderByUnit,
                    //OrderCost = INV.OrderCost,
                    Price1 = INV.Price1,
                    Price2 = INV.Price2,
                    //Price3 = INV.Price3,
                    //Price4 = INV.Price4,
                    //Price5 = INV.Price5,
                    //QuantityOnCustomerOrder = INV.QuantityOnCustomerOrder,
                    QuantityOnHand = INV.QuantityOnHand,
                    //QuantityOnOrder = INV.QuantityOnOrder,
                    //QuantityOnPendingOrder = INV.QuantityOnPendingOrder,
                    //ReorderPoint = INV.ReorderPoint,
                    //ReorderPointStore01 = INV.ReorderPointStore01,
                    //SellByUnit = INV.SellByUnit,
                    //SerialFlag = INV.SerialFlag,
                    //Size = INV.Size,
                    //StoreExchangeStatus = INV.StoreExchangeStatus,
                    //UnitOfMeasure = INV.UnitOfMeasure,
                    //UPC = INV.UPC,
                    //VendorCode = INV.VendorCode,
                    //VendorListID = INV.VendorListID,
                    //WebDesc = INV.WebDesc,
                    //WebPrice = INV.WebPrice,
                    //Manufacturer = INV.Manufacturer,
                    //Weight = INV.Weight,
                    //WebSKU = INV.WebSKU,
                    //WebCategories = INV.WebCategories,
                    //UnitOfMeasure1ALU = INV.UnitOfMeasure1ALU,
                    //UnitOfMeasure1MSRP = INV.UnitOfMeasure1MSRP,
                    //UnitOfMeasure1NumberOfBaseUnits = INV.UnitOfMeasure1NumberOfBaseUnits,
                    //UnitOfMeasure1Price1 = INV.UnitOfMeasure1Price1,
                    //UnitOfMeasure1Price2 = INV.UnitOfMeasure1Price2,
                    //UnitOfMeasure1Price3 = INV.UnitOfMeasure1Price3,
                    //UnitOfMeasure1Price4 = INV.UnitOfMeasure1Price4,
                    //UnitOfMeasure1Price5 = INV.UnitOfMeasure1Price5,
                    //UnitOfMeasure1UnitOfMeasure = INV.UnitOfMeasure1UnitOfMeasure,
                    //UnitOfMeasure1UPC = INV.UnitOfMeasure1UPC,
                    //UnitOfMeasure2ALU = INV.UnitOfMeasure2ALU,
                    //UnitOfMeasure2MSRP = INV.UnitOfMeasure2MSRP,
                    //UnitOfMeasure2NumberOfBaseUnits = INV.UnitOfMeasure2NumberOfBaseUnits,
                    //UnitOfMeasure2Price1 = INV.UnitOfMeasure2Price1,
                    //UnitOfMeasure2Price2 = INV.UnitOfMeasure2Price2,
                    //UnitOfMeasure2Price3 = INV.UnitOfMeasure2Price3,
                    //UnitOfMeasure2Price4 = INV.UnitOfMeasure2Price4,
                    //UnitOfMeasure2Price5 = INV.UnitOfMeasure2Price5,
                    //UnitOfMeasure2UnitOfMeasure = INV.UnitOfMeasure2UnitOfMeasure,
                    //UnitOfMeasure2UPC = INV.UnitOfMeasure2UPC,
                    //UnitOfMeasure3ALU = INV.UnitOfMeasure3ALU,
                    //UnitOfMeasure3MSRP = INV.UnitOfMeasure3MSRP,
                    //UnitOfMeasure3NumberOfBaseUnits = INV.UnitOfMeasure3NumberOfBaseUnits,
                    //UnitOfMeasure3Price1 = INV.UnitOfMeasure3Price1,
                    //UnitOfMeasure3Price2 = INV.UnitOfMeasure3Price2,
                    //UnitOfMeasure3Price3 = INV.UnitOfMeasure3Price3,
                    //UnitOfMeasure3Price4 = INV.UnitOfMeasure3Price4,
                    //UnitOfMeasure3Price5 = INV.UnitOfMeasure3Price5,
                    //UnitOfMeasure3UnitOfMeasure = INV.UnitOfMeasure3UnitOfMeasure,
                    //UnitOfMeasure3UPC = INV.UnitOfMeasure3UPC,
                    //VendorInfo2ALU = INV.VendorInfo2ALU,
                    //VendorInfo2OrderCost = INV.VendorInfo2OrderCost,
                    //VendorInfo2UPC = INV.VendorInfo2UPC,
                    //VendorInfo2VendorListID = INV.VendorInfo2VendorListID,
                    //VendorInfo3ALU = INV.VendorInfo3ALU,
                    //VendorInfo3OrderCost = INV.VendorInfo3OrderCost,
                    //VendorInfo3UPC = INV.VendorInfo3UPC,
                    //VendorInfo3VendorListID = INV.VendorInfo3VendorListID,
                    //VendorInfo4ALU = INV.VendorInfo4ALU,
                    //VendorInfo4OrderCost = INV.VendorInfo4OrderCost,
                    //VendorInfo4UPC = INV.VendorInfo4UPC,
                    //VendorInfo4VendorListID = INV.VendorInfo4VendorListID,
                    //VendorInfo5ALU = INV.VendorInfo5ALU,
                    //VendorInfo5OrderCost = INV.VendorInfo5OrderCost,
                    //VendorInfo5UPC = INV.VendorInfo5UPC,
                    //VendorInfo5VendorListID = INV.VendorInfo5VendorListID,
                    Image = INV.Image,
                    IsActive = INV.Active,
                }).Where(x => x.IsActive == true).ToListAsync();

            var store = await DbContext.Stores.ToListAsync();

            var response = new List<POSInventoryListResponse>();

            foreach (var storeList in inventoryList)
            {
                var StoreListDetails = new List<StoreListDetails>();
                var storeDetailLists = storeList;

                if (storeList.OnHandStore01 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore01"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore02 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore02"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore03 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore03"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore04 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore04"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore05 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore05"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore06 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore06"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore07 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore07"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore08 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore08"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore09 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore09"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore10 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore10"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore11 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore11"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore12 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore12"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore13 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore13"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore14 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore14"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore15 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore15"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore16 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore16"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore17 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore17"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore18 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore18"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore19 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore19"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                if (storeList.OnHandStore20 != 0)
                {
                    var d = store.FirstOrDefault(x => x.Code.Equals("OnHandStore20"));
                    if (d != null)
                    {
                        StoreListDetails.Add(new StoreListDetails { Name = d.Name, Latitude = d.Latitude, Longitude = d.Longitude });
                    }

                    if (!string.IsNullOrWhiteSpace(request.LocationDetail))
                    {
                        foreach (var item in StoreListDetails.Where(w => !string.IsNullOrWhiteSpace(w.Longitude) && !string.IsNullOrWhiteSpace(w.Latitude)))
                        {
                            if (IsGeoLocationFound(request.LocationDetail, Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)))
                                item.IsLocationFound = true;
                        }
                    }

                }

                storeDetailLists.StoreListDetails = StoreListDetails;
                response.Add(storeDetailLists);
            }


            string[] sort = request.SortBy.ToLower().Split("_");

            switch (sort[0])
            {
                case "category":

                    if (sort.Length > 1 && sort[1] == "desc")
                        response = response.OrderByDescending(x => x.Desc1).ToList();
                    else
                        response = response.OrderBy(x => x.Desc1).ToList();
                    break;

                case "price":

                    if (sort.Length > 1 && sort[1] == "desc")
                        response = response.OrderByDescending(x => x.Price2).ToList();
                    else
                        response = response.OrderBy(x => x.Price2).ToList();
                    break;

                case "discount":

                    if (sort.Length > 1 && sort[1] == "desc")
                        response = response.OrderByDescending(x => x.Price1).ToList();
                    else
                        response = response.OrderBy(x => x.Price1).ToList();
                    break;

                //case "store":

                //    if (sort.Length > 1 && sort[1] == "desc")
                //        productList = productList.OrderByDescending(x => x.StoreName).ToList();
                //    else
                //        productList = productList.OrderBy(x => x.StoreName).ToList();
                //    break;

                default:

                    response = response.OrderByDescending(x => x.TimeModified).ToList();
                    break;
            }

            return response;
        }

        #endregion 3. POS Inventory List without Pagination

        #region 4. POSInventory Add Update
        //Temptory table to POSInventory Add/Update
        public async Task<List<POSInventoryDemoEntity>> POSInventoryAddUpdate()
        {
            var timeModified = await DbContext.POSInventory.OrderByDescending(o => o.TimeModified).Select(s => s.TimeModified).FirstOrDefaultAsync();

            if (timeModified == null)
            {
                var InvenoryDemo1 = await DbContext.POSInventoryDemo.ToListAsync();
                foreach (var inventory in InvenoryDemo1)
                {
                    var InventoryData = await DbContext.POSInventory.FirstOrDefaultAsync(o => o.ListId.Equals(inventory.ListId));

                    if (InventoryData == null)
                    {
                        await DbContext.AddRangeAsync(new POSInventoryEntity
                        {
                            ListId = inventory.ListId,
                            TimeCreated = inventory.TimeCreated,
                            TimeModified = inventory.TimeModified,
                            ALU = inventory.ALU,
                            Attribute = inventory.Attribute,
                            COGSAccount = inventory.COGSAccount,
                            Description = inventory.Description,
                            Cost = inventory.Cost,
                            DepartmentCode = inventory.DepartmentCode,
                            DepartmentListID = inventory.DepartmentListID,
                            Desc1 = inventory.Desc1,
                            IncomeAccount = inventory.IncomeAccount,
                            IsBelowReorder = inventory.IsBelowReorder,
                            IsEligibleForCommission = inventory.IsEligibleForCommission,
                            IsPrintingTags = inventory.IsPrintingTags,
                            IsUnorderable = inventory.IsUnorderable,
                            ItemNumber = inventory.ItemNumber,
                            ItemType = inventory.ItemType,
                            Keywords = inventory.Keywords,
                            LastReceived = inventory.LastReceived,
                            MarginPercent = inventory.MarginPercent,
                            MarkupPercent = inventory.MarkupPercent,
                            MSRP = inventory.MSRP,
                            OnHandStore01 = inventory.OnHandStore01,
                            OnHandStore02 = inventory.OnHandStore02,
                            OnHandStore03 = inventory.OnHandStore03,
                            OnHandStore04 = inventory.OnHandStore04,
                            OnHandStore05 = inventory.OnHandStore05,
                            OnHandStore06 = inventory.OnHandStore06,
                            OnHandStore07 = inventory.OnHandStore07,
                            OnHandStore08 = inventory.OnHandStore08,
                            OnHandStore09 = inventory.OnHandStore09,
                            OnHandStore10 = inventory.OnHandStore10,
                            OnHandStore11 = inventory.OnHandStore11,
                            OnHandStore12 = inventory.OnHandStore12,
                            OnHandStore13 = inventory.OnHandStore13,
                            OnHandStore14 = inventory.OnHandStore14,
                            OnHandStore15 = inventory.OnHandStore15,
                            OnHandStore16 = inventory.OnHandStore16,
                            OnHandStore17 = inventory.OnHandStore17,
                            OnHandStore18 = inventory.OnHandStore18,
                            OnHandStore19 = inventory.OnHandStore19,
                            OnHandStore20 = inventory.OnHandStore20,
                            OrderByUnit = inventory.OrderByUnit,
                            OrderCost = inventory.OrderCost,
                            Price1 = inventory.Price1,
                            Price2 = inventory.Price2,
                            Price3 = inventory.Price3,
                            Price4 = inventory.Price4,
                            Price5 = inventory.Price5,
                            QuantityOnCustomerOrder = inventory.QuantityOnCustomerOrder,
                            QuantityOnHand = inventory.QuantityOnHand,
                            QuantityOnOrder = inventory.QuantityOnOrder,
                            QuantityOnPendingOrder = inventory.QuantityOnPendingOrder,
                            ReorderPoint = inventory.ReorderPoint,
                            ReorderPointStore01 = inventory.ReorderPointStore01,
                            SellByUnit = inventory.SellByUnit,
                            SerialFlag = inventory.SerialFlag,
                            Size = inventory.Size,
                            StoreExchangeStatus = inventory.StoreExchangeStatus,
                            UnitOfMeasure = inventory.UnitOfMeasure,
                            UPC = inventory.UPC,
                            VendorCode = inventory.VendorCode,
                            VendorListID = inventory.VendorListID,
                            WebDesc = inventory.WebDesc,
                            WebPrice = inventory.WebPrice,
                            Manufacturer = inventory.Manufacturer,
                            Weight = inventory.Weight,
                            WebSKU = inventory.WebSKU,
                            WebCategories = inventory.WebCategories,
                            UnitOfMeasure1ALU = inventory.UnitOfMeasure1ALU,
                            UnitOfMeasure1MSRP = inventory.UnitOfMeasure1MSRP,
                            UnitOfMeasure1NumberOfBaseUnits = inventory.UnitOfMeasure1NumberOfBaseUnits,
                            UnitOfMeasure1Price1 = inventory.UnitOfMeasure1Price1,
                            UnitOfMeasure1Price2 = inventory.UnitOfMeasure1Price2,
                            UnitOfMeasure1Price3 = inventory.UnitOfMeasure1Price3,
                            UnitOfMeasure1Price4 = inventory.UnitOfMeasure1Price4,
                            UnitOfMeasure1Price5 = inventory.UnitOfMeasure1Price5,
                            UnitOfMeasure1UnitOfMeasure = inventory.UnitOfMeasure1UnitOfMeasure,
                            UnitOfMeasure1UPC = inventory.UnitOfMeasure1UPC,
                            UnitOfMeasure2ALU = inventory.UnitOfMeasure2ALU,
                            UnitOfMeasure2MSRP = inventory.UnitOfMeasure2MSRP,
                            UnitOfMeasure2NumberOfBaseUnits = inventory.UnitOfMeasure2NumberOfBaseUnits,
                            UnitOfMeasure2Price1 = inventory.UnitOfMeasure2Price1,
                            UnitOfMeasure2Price2 = inventory.UnitOfMeasure2Price2,
                            UnitOfMeasure2Price3 = inventory.UnitOfMeasure2Price3,
                            UnitOfMeasure2Price4 = inventory.UnitOfMeasure2Price4,
                            UnitOfMeasure2Price5 = inventory.UnitOfMeasure2Price5,
                            UnitOfMeasure2UnitOfMeasure = inventory.UnitOfMeasure2UnitOfMeasure,
                            UnitOfMeasure2UPC = inventory.UnitOfMeasure2UPC,
                            UnitOfMeasure3ALU = inventory.UnitOfMeasure3ALU,
                            UnitOfMeasure3MSRP = inventory.UnitOfMeasure3MSRP,
                            UnitOfMeasure3NumberOfBaseUnits = inventory.UnitOfMeasure3NumberOfBaseUnits,
                            UnitOfMeasure3Price1 = inventory.UnitOfMeasure3Price1,
                            UnitOfMeasure3Price2 = inventory.UnitOfMeasure3Price2,
                            UnitOfMeasure3Price3 = inventory.UnitOfMeasure3Price3,
                            UnitOfMeasure3Price4 = inventory.UnitOfMeasure3Price4,
                            UnitOfMeasure3Price5 = inventory.UnitOfMeasure3Price5,
                            UnitOfMeasure3UnitOfMeasure = inventory.UnitOfMeasure3UnitOfMeasure,
                            UnitOfMeasure3UPC = inventory.UnitOfMeasure3UPC,
                            VendorInfo2ALU = inventory.VendorInfo2ALU,
                            VendorInfo2OrderCost = inventory.VendorInfo2OrderCost,
                            VendorInfo2UPC = inventory.VendorInfo2UPC,
                            VendorInfo2VendorListID = inventory.VendorInfo2VendorListID,
                            VendorInfo3ALU = inventory.VendorInfo3ALU,
                            VendorInfo3OrderCost = inventory.VendorInfo3OrderCost,
                            VendorInfo3UPC = inventory.VendorInfo3UPC,
                            VendorInfo3VendorListID = inventory.VendorInfo3VendorListID,
                            VendorInfo4ALU = inventory.VendorInfo4ALU,
                            VendorInfo4OrderCost = inventory.VendorInfo4OrderCost,
                            VendorInfo4UPC = inventory.VendorInfo4UPC,
                            VendorInfo4VendorListID = inventory.VendorInfo4VendorListID,
                            VendorInfo5ALU = inventory.VendorInfo5ALU,
                            VendorInfo5OrderCost = inventory.VendorInfo5OrderCost,
                            VendorInfo5UPC = inventory.VendorInfo5UPC,
                            VendorInfo5VendorListID = inventory.VendorInfo5VendorListID,
                            Active = true,
                            Image = inventory.Image,
                        });
                    }
                }
                try
                {
                    await DbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw;
                }
                return InvenoryDemo1;
            }
            var InvenoryDemo = await DbContext.POSInventoryDemo.Where(w => w.TimeModified > timeModified.Value).ToListAsync();
            if(InvenoryDemo != null) {
                foreach (var inventory in InvenoryDemo)
                {
                    var InventoryData = await DbContext.POSInventory.FirstOrDefaultAsync(o => o.ListId.Equals(inventory.ListId));

                    if (InventoryData == null)
                    {
                        await DbContext.AddRangeAsync(new POSInventoryEntity
                        {
                            ListId = inventory.ListId,
                            TimeCreated = inventory.TimeCreated,
                            TimeModified = inventory.TimeModified,
                            ALU = inventory.ALU,
                            Attribute = inventory.Attribute,
                            COGSAccount = inventory.COGSAccount,
                            Description = inventory.Description,
                            Cost = inventory.Cost,
                            DepartmentCode = inventory.DepartmentCode,
                            DepartmentListID = inventory.DepartmentListID,
                            Desc1 = inventory.Desc1,
                            IncomeAccount = inventory.IncomeAccount,
                            IsBelowReorder = inventory.IsBelowReorder,
                            IsEligibleForCommission = inventory.IsEligibleForCommission,
                            IsPrintingTags = inventory.IsPrintingTags,
                            IsUnorderable = inventory.IsUnorderable,
                            ItemNumber = inventory.ItemNumber,
                            ItemType = inventory.ItemType,
                            Keywords = inventory.Keywords,
                            LastReceived = inventory.LastReceived,
                            MarginPercent = inventory.MarginPercent,
                            MarkupPercent = inventory.MarkupPercent,
                            MSRP = inventory.MSRP,
                            OnHandStore01 = inventory.OnHandStore01,
                            OnHandStore02 = inventory.OnHandStore02,
                            OnHandStore03 = inventory.OnHandStore03,
                            OnHandStore04 = inventory.OnHandStore04,
                            OnHandStore05 = inventory.OnHandStore05,
                            OnHandStore06 = inventory.OnHandStore06,
                            OnHandStore07 = inventory.OnHandStore07,
                            OnHandStore08 = inventory.OnHandStore08,
                            OnHandStore09 = inventory.OnHandStore09,
                            OnHandStore10 = inventory.OnHandStore10,
                            OnHandStore11 = inventory.OnHandStore11,
                            OnHandStore12 = inventory.OnHandStore12,
                            OnHandStore13 = inventory.OnHandStore13,
                            OnHandStore14 = inventory.OnHandStore14,
                            OnHandStore15 = inventory.OnHandStore15,
                            OnHandStore16 = inventory.OnHandStore16,
                            OnHandStore17 = inventory.OnHandStore17,
                            OnHandStore18 = inventory.OnHandStore18,
                            OnHandStore19 = inventory.OnHandStore19,
                            OnHandStore20 = inventory.OnHandStore20,
                            OrderByUnit = inventory.OrderByUnit,
                            OrderCost = inventory.OrderCost,
                            Price1 = inventory.Price1,
                            Price2 = inventory.Price2,
                            Price3 = inventory.Price3,
                            Price4 = inventory.Price4,
                            Price5 = inventory.Price5,
                            QuantityOnCustomerOrder = inventory.QuantityOnCustomerOrder,
                            QuantityOnHand = inventory.QuantityOnHand,
                            QuantityOnOrder = inventory.QuantityOnOrder,
                            QuantityOnPendingOrder = inventory.QuantityOnPendingOrder,
                            ReorderPoint = inventory.ReorderPoint,
                            ReorderPointStore01 = inventory.ReorderPointStore01,
                            SellByUnit = inventory.SellByUnit,
                            SerialFlag = inventory.SerialFlag,
                            Size = inventory.Size,
                            StoreExchangeStatus = inventory.StoreExchangeStatus,
                            UnitOfMeasure = inventory.UnitOfMeasure,
                            UPC = inventory.UPC,
                            VendorCode = inventory.VendorCode,
                            VendorListID = inventory.VendorListID,
                            WebDesc = inventory.WebDesc,
                            WebPrice = inventory.WebPrice,
                            Manufacturer = inventory.Manufacturer,
                            Weight = inventory.Weight,
                            WebSKU = inventory.WebSKU,
                            WebCategories = inventory.WebCategories,
                            UnitOfMeasure1ALU = inventory.UnitOfMeasure1ALU,
                            UnitOfMeasure1MSRP = inventory.UnitOfMeasure1MSRP,
                            UnitOfMeasure1NumberOfBaseUnits = inventory.UnitOfMeasure1NumberOfBaseUnits,
                            UnitOfMeasure1Price1 = inventory.UnitOfMeasure1Price1,
                            UnitOfMeasure1Price2 = inventory.UnitOfMeasure1Price2,
                            UnitOfMeasure1Price3 = inventory.UnitOfMeasure1Price3,
                            UnitOfMeasure1Price4 = inventory.UnitOfMeasure1Price4,
                            UnitOfMeasure1Price5 = inventory.UnitOfMeasure1Price5,
                            UnitOfMeasure1UnitOfMeasure = inventory.UnitOfMeasure1UnitOfMeasure,
                            UnitOfMeasure1UPC = inventory.UnitOfMeasure1UPC,
                            UnitOfMeasure2ALU = inventory.UnitOfMeasure2ALU,
                            UnitOfMeasure2MSRP = inventory.UnitOfMeasure2MSRP,
                            UnitOfMeasure2NumberOfBaseUnits = inventory.UnitOfMeasure2NumberOfBaseUnits,
                            UnitOfMeasure2Price1 = inventory.UnitOfMeasure2Price1,
                            UnitOfMeasure2Price2 = inventory.UnitOfMeasure2Price2,
                            UnitOfMeasure2Price3 = inventory.UnitOfMeasure2Price3,
                            UnitOfMeasure2Price4 = inventory.UnitOfMeasure2Price4,
                            UnitOfMeasure2Price5 = inventory.UnitOfMeasure2Price5,
                            UnitOfMeasure2UnitOfMeasure = inventory.UnitOfMeasure2UnitOfMeasure,
                            UnitOfMeasure2UPC = inventory.UnitOfMeasure2UPC,
                            UnitOfMeasure3ALU = inventory.UnitOfMeasure3ALU,
                            UnitOfMeasure3MSRP = inventory.UnitOfMeasure3MSRP,
                            UnitOfMeasure3NumberOfBaseUnits = inventory.UnitOfMeasure3NumberOfBaseUnits,
                            UnitOfMeasure3Price1 = inventory.UnitOfMeasure3Price1,
                            UnitOfMeasure3Price2 = inventory.UnitOfMeasure3Price2,
                            UnitOfMeasure3Price3 = inventory.UnitOfMeasure3Price3,
                            UnitOfMeasure3Price4 = inventory.UnitOfMeasure3Price4,
                            UnitOfMeasure3Price5 = inventory.UnitOfMeasure3Price5,
                            UnitOfMeasure3UnitOfMeasure = inventory.UnitOfMeasure3UnitOfMeasure,
                            UnitOfMeasure3UPC = inventory.UnitOfMeasure3UPC,
                            VendorInfo2ALU = inventory.VendorInfo2ALU,
                            VendorInfo2OrderCost = inventory.VendorInfo2OrderCost,
                            VendorInfo2UPC = inventory.VendorInfo2UPC,
                            VendorInfo2VendorListID = inventory.VendorInfo2VendorListID,
                            VendorInfo3ALU = inventory.VendorInfo3ALU,
                            VendorInfo3OrderCost = inventory.VendorInfo3OrderCost,
                            VendorInfo3UPC = inventory.VendorInfo3UPC,
                            VendorInfo3VendorListID = inventory.VendorInfo3VendorListID,
                            VendorInfo4ALU = inventory.VendorInfo4ALU,
                            VendorInfo4OrderCost = inventory.VendorInfo4OrderCost,
                            VendorInfo4UPC = inventory.VendorInfo4UPC,
                            VendorInfo4VendorListID = inventory.VendorInfo4VendorListID,
                            VendorInfo5ALU = inventory.VendorInfo5ALU,
                            VendorInfo5OrderCost = inventory.VendorInfo5OrderCost,
                            VendorInfo5UPC = inventory.VendorInfo5UPC,
                            VendorInfo5VendorListID = inventory.VendorInfo5VendorListID,
                            Active = true,
                            Image = inventory.Image,
                        });
                    }
                    else
                    {

                        InventoryData.TimeModified = inventory.TimeModified;
                        InventoryData.ALU = inventory.ALU;
                        InventoryData.Attribute = inventory.Attribute;
                        InventoryData.COGSAccount = inventory.COGSAccount;
                        InventoryData.Description = inventory.Description;
                        InventoryData.Cost = inventory.Cost;
                        InventoryData.DepartmentCode = inventory.DepartmentCode;
                        InventoryData.DepartmentListID = inventory.DepartmentListID;
                        InventoryData.Desc1 = inventory.Desc1;
                        InventoryData.IncomeAccount = inventory.IncomeAccount;
                        InventoryData.IsBelowReorder = inventory.IsBelowReorder;
                        InventoryData.IsEligibleForCommission = inventory.IsEligibleForCommission;
                        InventoryData.IsPrintingTags = inventory.IsPrintingTags;
                        InventoryData.IsUnorderable = inventory.IsUnorderable;
                        InventoryData.ItemNumber = inventory.ItemNumber;
                        InventoryData.ItemType = inventory.ItemType;
                        InventoryData.Keywords = inventory.Keywords;
                        InventoryData.LastReceived = inventory.LastReceived;
                        InventoryData.MarginPercent = inventory.MarginPercent;
                        InventoryData.MarkupPercent = inventory.MarkupPercent;
                        InventoryData.MSRP = inventory.MSRP;
                        InventoryData.OnHandStore01 = inventory.OnHandStore01;
                        InventoryData.OnHandStore02 = inventory.OnHandStore02;
                        InventoryData.OnHandStore03 = inventory.OnHandStore03;
                        InventoryData.OnHandStore04 = inventory.OnHandStore04;
                        InventoryData.OnHandStore05 = inventory.OnHandStore05;
                        InventoryData.OnHandStore06 = inventory.OnHandStore06;
                        InventoryData.OnHandStore07 = inventory.OnHandStore07;
                        InventoryData.OnHandStore08 = inventory.OnHandStore08;
                        InventoryData.OnHandStore09 = inventory.OnHandStore09;
                        InventoryData.OnHandStore10 = inventory.OnHandStore10;
                        InventoryData.OnHandStore11 = inventory.OnHandStore11;
                        InventoryData.OnHandStore12 = inventory.OnHandStore12;
                        InventoryData.OnHandStore13 = inventory.OnHandStore13;
                        InventoryData.OnHandStore14 = inventory.OnHandStore14;
                        InventoryData.OnHandStore15 = inventory.OnHandStore15;
                        InventoryData.OnHandStore16 = inventory.OnHandStore16;
                        InventoryData.OnHandStore17 = inventory.OnHandStore17;
                        InventoryData.OnHandStore18 = inventory.OnHandStore18;
                        InventoryData.OnHandStore19 = inventory.OnHandStore19;
                        InventoryData.OnHandStore20 = inventory.OnHandStore20;
                        InventoryData.OrderByUnit = inventory.OrderByUnit;
                        InventoryData.OrderCost = inventory.OrderCost;
                        InventoryData.Price1 = inventory.Price1;
                        InventoryData.Price2 = inventory.Price2;
                        InventoryData.Price3 = inventory.Price3;
                        InventoryData.Price4 = inventory.Price4;
                        InventoryData.Price5 = inventory.Price5;
                        InventoryData.QuantityOnCustomerOrder = inventory.QuantityOnCustomerOrder;
                        InventoryData.QuantityOnHand = inventory.QuantityOnHand;
                        InventoryData.QuantityOnOrder = inventory.QuantityOnOrder;
                        InventoryData.QuantityOnPendingOrder = inventory.QuantityOnPendingOrder;
                        InventoryData.ReorderPoint = inventory.ReorderPoint;
                        InventoryData.ReorderPointStore01 = inventory.ReorderPointStore01;
                        InventoryData.SellByUnit = inventory.SellByUnit;
                        InventoryData.SerialFlag = inventory.SerialFlag;
                        InventoryData.Size = inventory.Size;
                        InventoryData.StoreExchangeStatus = inventory.StoreExchangeStatus;
                        InventoryData.UnitOfMeasure = inventory.UnitOfMeasure;
                        InventoryData.UPC = inventory.UPC;
                        InventoryData.VendorCode = inventory.VendorCode;
                        InventoryData.VendorListID = inventory.VendorListID;
                        InventoryData.WebDesc = inventory.WebDesc;
                        InventoryData.WebPrice = inventory.WebPrice;
                        InventoryData.Manufacturer = inventory.Manufacturer;
                        InventoryData.Weight = inventory.Weight;
                        InventoryData.WebSKU = inventory.WebSKU;
                        InventoryData.WebCategories = inventory.WebCategories;
                        InventoryData.UnitOfMeasure1ALU = inventory.UnitOfMeasure1ALU;
                        InventoryData.UnitOfMeasure1MSRP = inventory.UnitOfMeasure1MSRP;
                        InventoryData.UnitOfMeasure1NumberOfBaseUnits = inventory.UnitOfMeasure1NumberOfBaseUnits;
                        InventoryData.UnitOfMeasure1Price1 = inventory.UnitOfMeasure1Price1;
                        InventoryData.UnitOfMeasure1Price2 = inventory.UnitOfMeasure1Price2;
                        InventoryData.UnitOfMeasure1Price3 = inventory.UnitOfMeasure1Price3;
                        InventoryData.UnitOfMeasure1Price4 = inventory.UnitOfMeasure1Price4;
                        InventoryData.UnitOfMeasure1Price5 = inventory.UnitOfMeasure1Price5;
                        InventoryData.UnitOfMeasure1UnitOfMeasure = inventory.UnitOfMeasure1UnitOfMeasure;
                        InventoryData.UnitOfMeasure1UPC = inventory.UnitOfMeasure1UPC;
                        InventoryData.UnitOfMeasure2ALU = inventory.UnitOfMeasure2ALU;
                        InventoryData.UnitOfMeasure2MSRP = inventory.UnitOfMeasure2MSRP;
                        InventoryData.UnitOfMeasure2NumberOfBaseUnits = inventory.UnitOfMeasure2NumberOfBaseUnits;
                        InventoryData.UnitOfMeasure2Price1 = inventory.UnitOfMeasure2Price1;
                        InventoryData.UnitOfMeasure2Price2 = inventory.UnitOfMeasure2Price2;
                        InventoryData.UnitOfMeasure2Price3 = inventory.UnitOfMeasure2Price3;
                        InventoryData.UnitOfMeasure2Price4 = inventory.UnitOfMeasure2Price4;
                        InventoryData.UnitOfMeasure2Price5 = inventory.UnitOfMeasure2Price5;
                        InventoryData.UnitOfMeasure2UnitOfMeasure = inventory.UnitOfMeasure2UnitOfMeasure;
                        InventoryData.UnitOfMeasure2UPC = inventory.UnitOfMeasure2UPC;
                        InventoryData.UnitOfMeasure3ALU = inventory.UnitOfMeasure3ALU;
                        InventoryData.UnitOfMeasure3MSRP = inventory.UnitOfMeasure3MSRP;
                        InventoryData.UnitOfMeasure3NumberOfBaseUnits = inventory.UnitOfMeasure3NumberOfBaseUnits;
                        InventoryData.UnitOfMeasure3Price1 = inventory.UnitOfMeasure3Price1;
                        InventoryData.UnitOfMeasure3Price2 = inventory.UnitOfMeasure3Price2;
                        InventoryData.UnitOfMeasure3Price3 = inventory.UnitOfMeasure3Price3;
                        InventoryData.UnitOfMeasure3Price4 = inventory.UnitOfMeasure3Price4;
                        InventoryData.UnitOfMeasure3Price5 = inventory.UnitOfMeasure3Price5;
                        InventoryData.UnitOfMeasure3UnitOfMeasure = inventory.UnitOfMeasure3UnitOfMeasure;
                        InventoryData.UnitOfMeasure3UPC = inventory.UnitOfMeasure3UPC;
                        InventoryData.VendorInfo2ALU = inventory.VendorInfo2ALU;
                        InventoryData.VendorInfo2OrderCost = inventory.VendorInfo2OrderCost;
                        InventoryData.VendorInfo2UPC = inventory.VendorInfo2UPC;
                        InventoryData.VendorInfo2VendorListID = inventory.VendorInfo2VendorListID;
                        InventoryData.VendorInfo3ALU = inventory.VendorInfo3ALU;
                        InventoryData.VendorInfo3OrderCost = inventory.VendorInfo3OrderCost;
                        InventoryData.VendorInfo3UPC = inventory.VendorInfo3UPC;
                        InventoryData.VendorInfo3VendorListID = inventory.VendorInfo3VendorListID;
                        InventoryData.VendorInfo4ALU = inventory.VendorInfo4ALU;
                        InventoryData.VendorInfo4OrderCost = inventory.VendorInfo4OrderCost;
                        InventoryData.VendorInfo4UPC = inventory.VendorInfo4UPC;
                        InventoryData.VendorInfo4VendorListID = inventory.VendorInfo4VendorListID;
                        InventoryData.VendorInfo5ALU = inventory.VendorInfo5ALU;
                        InventoryData.VendorInfo5OrderCost = inventory.VendorInfo5OrderCost;
                        InventoryData.VendorInfo5UPC = inventory.VendorInfo5UPC;
                        InventoryData.VendorInfo5VendorListID = inventory.VendorInfo5VendorListID;

                        var response = new POSInventoryGetByIdResponse
                        {
                            //TimeCreated = inventory.TimeCreated,
                            TimeModified = inventory.TimeModified,
                            ALU = inventory.ALU,
                            Attribute = inventory.Attribute,
                            COGSAccount = inventory.COGSAccount,
                            Cost = inventory.Cost,
                            DepartmentCode = inventory.DepartmentCode,
                            DepartmentListID = inventory.DepartmentListID,
                            Desc1 = inventory.Desc1,
                            Description = inventory.Description,
                            IncomeAccount = inventory.IncomeAccount,
                            IsBelowReorder = inventory.IsBelowReorder,
                            IsEligibleForCommission = inventory.IsEligibleForCommission,
                            IsPrintingTags = inventory.IsPrintingTags,
                            IsUnorderable = inventory.IsUnorderable,
                            ItemNumber = inventory.ItemNumber,
                            ItemType = inventory.ItemType,
                            Keywords = inventory.Keywords,
                            LastReceived = inventory.LastReceived,
                            MarginPercent = inventory.MarginPercent,
                            MarkupPercent = inventory.MarkupPercent,
                            MSRP = inventory.MSRP,
                            OnHandStore01 = inventory.OnHandStore01,
                            OnHandStore02 = inventory.OnHandStore02,
                            OnHandStore03 = inventory.OnHandStore03,
                            OnHandStore04 = inventory.OnHandStore04,
                            OnHandStore05 = inventory.OnHandStore05,
                            OnHandStore06 = inventory.OnHandStore06,
                            OnHandStore07 = inventory.OnHandStore07,
                            OnHandStore08 = inventory.OnHandStore08,
                            OnHandStore09 = inventory.OnHandStore09,
                            OnHandStore10 = inventory.OnHandStore10,
                            OnHandStore11 = inventory.OnHandStore11,
                            OnHandStore12 = inventory.OnHandStore12,
                            OnHandStore13 = inventory.OnHandStore13,
                            OnHandStore14 = inventory.OnHandStore14,
                            OnHandStore15 = inventory.OnHandStore15,
                            OnHandStore16 = inventory.OnHandStore16,
                            OnHandStore17 = inventory.OnHandStore17,
                            OnHandStore18 = inventory.OnHandStore18,
                            OnHandStore19 = inventory.OnHandStore19,
                            OnHandStore20 = inventory.OnHandStore20,
                            OrderByUnit = inventory.OrderByUnit,
                            OrderCost = inventory.OrderCost,
                            Price1 = inventory.Price1,
                            Price2 = inventory.Price2,
                            Price3 = inventory.Price3,
                            Price4 = inventory.Price4,
                            Price5 = inventory.Price5,
                            QuantityOnCustomerOrder = inventory.QuantityOnCustomerOrder,
                            QuantityOnHand = inventory.QuantityOnHand,
                            QuantityOnOrder = inventory.QuantityOnOrder,
                            QuantityOnPendingOrder = inventory.QuantityOnPendingOrder,
                            ReorderPoint = inventory.ReorderPoint,
                            ReorderPointStore01 = inventory.ReorderPointStore01,
                            SellByUnit = inventory.SellByUnit,
                            SerialFlag = inventory.SerialFlag,
                            Size = inventory.Size,
                            StoreExchangeStatus = inventory.StoreExchangeStatus,
                            UnitOfMeasure = inventory.UnitOfMeasure,
                            UPC = inventory.UPC,
                            VendorCode = inventory.VendorCode,
                            VendorListID = inventory.VendorListID,
                            WebDesc = inventory.WebDesc,
                            WebPrice = inventory.WebPrice,
                            Manufacturer = inventory.Manufacturer,
                            Weight = inventory.Weight,
                            WebSKU = inventory.WebSKU,
                            WebCategories = inventory.WebCategories,
                            UnitOfMeasure1ALU = inventory.UnitOfMeasure1ALU,
                            UnitOfMeasure1MSRP = inventory.UnitOfMeasure1MSRP,
                            UnitOfMeasure1NumberOfBaseUnits = inventory.UnitOfMeasure1NumberOfBaseUnits,
                            UnitOfMeasure1Price1 = inventory.UnitOfMeasure1Price1,
                            UnitOfMeasure1Price2 = inventory.UnitOfMeasure1Price2,
                            UnitOfMeasure1Price3 = inventory.UnitOfMeasure1Price3,
                            UnitOfMeasure1Price4 = inventory.UnitOfMeasure1Price4,
                            UnitOfMeasure1Price5 = inventory.UnitOfMeasure1Price5,
                            UnitOfMeasure1UnitOfMeasure = inventory.UnitOfMeasure1UnitOfMeasure,
                            UnitOfMeasure1UPC = inventory.UnitOfMeasure1UPC,
                            UnitOfMeasure2ALU = inventory.UnitOfMeasure2ALU,
                            UnitOfMeasure2MSRP = inventory.UnitOfMeasure2MSRP,
                            UnitOfMeasure2NumberOfBaseUnits = inventory.UnitOfMeasure2NumberOfBaseUnits,
                            UnitOfMeasure2Price1 = inventory.UnitOfMeasure2Price1,
                            UnitOfMeasure2Price2 = inventory.UnitOfMeasure2Price2,
                            UnitOfMeasure2Price3 = inventory.UnitOfMeasure2Price3,
                            UnitOfMeasure2Price4 = inventory.UnitOfMeasure2Price4,
                            UnitOfMeasure2Price5 = inventory.UnitOfMeasure2Price5,
                            UnitOfMeasure2UnitOfMeasure = inventory.UnitOfMeasure2UnitOfMeasure,
                            UnitOfMeasure2UPC = inventory.UnitOfMeasure2UPC,
                            UnitOfMeasure3ALU = inventory.UnitOfMeasure3ALU,
                            UnitOfMeasure3MSRP = inventory.UnitOfMeasure3MSRP,
                            UnitOfMeasure3NumberOfBaseUnits = inventory.UnitOfMeasure3NumberOfBaseUnits,
                            UnitOfMeasure3Price1 = inventory.UnitOfMeasure3Price1,
                            UnitOfMeasure3Price2 = inventory.UnitOfMeasure3Price2,
                            UnitOfMeasure3Price3 = inventory.UnitOfMeasure3Price3,
                            UnitOfMeasure3Price4 = inventory.UnitOfMeasure3Price4,
                            UnitOfMeasure3Price5 = inventory.UnitOfMeasure3Price5,
                            UnitOfMeasure3UnitOfMeasure = inventory.UnitOfMeasure3UnitOfMeasure,
                            UnitOfMeasure3UPC = inventory.UnitOfMeasure3UPC,
                            VendorInfo2ALU = inventory.VendorInfo2ALU,
                            VendorInfo2OrderCost = inventory.VendorInfo2OrderCost,
                            VendorInfo2UPC = inventory.VendorInfo2UPC,
                            VendorInfo2VendorListID = inventory.VendorInfo2VendorListID,
                            VendorInfo3ALU = inventory.VendorInfo3ALU,
                            VendorInfo3OrderCost = inventory.VendorInfo3OrderCost,
                            VendorInfo3UPC = inventory.VendorInfo3UPC,
                            VendorInfo3VendorListID = inventory.VendorInfo3VendorListID,
                            VendorInfo4ALU = inventory.VendorInfo4ALU,
                            VendorInfo4OrderCost = inventory.VendorInfo4OrderCost,
                            VendorInfo4UPC = inventory.VendorInfo4UPC,
                            VendorInfo4VendorListID = inventory.VendorInfo4VendorListID,
                            VendorInfo5ALU = inventory.VendorInfo5ALU,
                            VendorInfo5OrderCost = inventory.VendorInfo5OrderCost,
                            VendorInfo5UPC = inventory.VendorInfo5UPC,
                            VendorInfo5VendorListID = inventory.VendorInfo5VendorListID,
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
            return InvenoryDemo;
        }
        #endregion

        #region 5. Get Store Based on 5 km Radius

        public bool IsGeoLocationFound(string LocationDetail, double storeLat, double storeLong)
        {
            if (!string.IsNullOrWhiteSpace(LocationDetail))
            {
                bool isFound = false;
                List<double> locationDetail = JsonConvert.DeserializeObject<List<double>>(LocationDetail);
                if (locationDetail != null && locationDetail.Count > 0)
                {
                    double userLat = locationDetail.LastOrDefault(); //Convert.ToDouble(23.063616);
                    double userLong = locationDetail.FirstOrDefault(); //Convert.ToDouble(72.531605);

                    if (storeLat >= userLat && storeLong >= userLong) // 1 km to 0.008 degree
                        isFound = userLat + 0.045 >= storeLat && userLong + 0.045 >= storeLong;
                    else if (storeLat <= userLat && storeLong >= userLong)
                        isFound = userLat - 0.045 <= storeLat && userLong + 0.045 >= storeLong;
                    else if (storeLat <= userLat && storeLong <= userLong)
                        isFound = userLat - 0.045 <= storeLat && userLong - 0.045 <= storeLong;
                    else if (storeLat >= userLat && storeLong <= userLong)
                        isFound = userLat + 0.045 >= storeLat && userLong - 0.045 <= storeLong;
                }
                return isFound;
            }
            else
                return false;
        }

        #endregion 5. Get Store Based on 5 km Radius

        #region 6. POSInventory Status Update

        public async Task<BaseResponse> POSInventoryStatusUpdate([FromBody] BaseListIdStatusUpdateRequest request)
        {
            var posInventory = await DbContext.POSInventory.FirstOrDefaultAsync(x => x.ListId.Equals(request.Id));

            if (posInventory == null)
                throw new ApiException("error_posinventory_not_found");

            posInventory.Active = request.Active;
            DbContext.Update(posInventory);
            await DbContext.SaveChangesAsync();
            return new BaseResponse { IsSuccess = true };
        }

        #endregion

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
