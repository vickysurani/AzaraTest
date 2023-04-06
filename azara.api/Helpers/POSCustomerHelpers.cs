using azara.models.Requests.POSCustomer;
using azara.models.Responses.POSCustomer;

namespace azara.api.Helpers
{
    public class POSCustomerHelpers : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        public POSCustomerHelpers(
            AzaraContext DbContext,
            ICrypto Crypto)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
        }

        #endregion Constructor

        #region 1. POSCustomer Get By Id

        public async Task<PosCustomerGetByIdResponse> POSCustomerGetById([FromBody] POSCustomerGetByIdRequest request)
        {
            var posCustomer = await DbContext.POSCustomer.FirstOrDefaultAsync(x => x.ListId.Equals(request.Id));

            if (posCustomer == null)
                throw new ApiException("error_poscustomer_not_found");

            var response = new PosCustomerGetByIdResponse
            {
                Id = posCustomer.Id,
                ListId = posCustomer.ListId,
                TimeCreated = posCustomer.TimeCreated,
                TimeModified = posCustomer.TimeModified,
                AccountBalance = posCustomer.AccountBalance,
                AccountLimit = posCustomer.AccountLimit,
                AmountPastDue = posCustomer.AmountPastDue,
                CompanyName = posCustomer.CompanyName,
                CustomerDiscType = posCustomer.CustomerDiscType,
                CustomerType = posCustomer.CustomerType,
                CustomerID = posCustomer.CustomerID,
                DefaultShipAddress = posCustomer.DefaultShipAddress,
                EMail = posCustomer.EMail,
                FirstName = posCustomer.FirstName,
                FullName = posCustomer.FullName,
                IsAcceptingChecks = posCustomer.IsAcceptingChecks,
                IsNoShipToBilling = posCustomer.IsNoShipToBilling,
                IsOkToEMail = posCustomer.IsOkToEMail,
                IsRewardsMember = posCustomer.IsRewardsMember,
                IsUsingChargeAccount = posCustomer.IsUsingChargeAccount,
                IsUsingWithQB = posCustomer.IsUsingWithQB,
                LastName = posCustomer.LastName,
                LastSale = posCustomer.LastSale,
                Notes = posCustomer.Notes,
                Phone = posCustomer.Phone,
                Phone2 = posCustomer.Phone2,
                Phone3 = posCustomer.Phone3,
                Phone4 = posCustomer.Phone4,
                PriceLevelNumber = posCustomer.PriceLevelNumber,
                Salutation = posCustomer.Salutation,
                StoreExchangeStatus = posCustomer.StoreExchangeStatus,
                TaxCategory = posCustomer.TaxCategory,
                WebNumber = posCustomer.WebNumber,
                BillAddressCity = posCustomer.BillAddressCity,
                BillAddressCountry = posCustomer.BillAddressCountry,
                BillAddressPostalCode = posCustomer.BillAddressPostalCode,
                BillAddressState = posCustomer.BillAddressState,
                BillAddressStreet = posCustomer.BillAddressStreet,
                BillAddressStreet2 = posCustomer.BillAddressStreet2,
                CustomFieldOther = posCustomer.CustomFieldOther,
                CustomFieldSalesLocation = posCustomer.CustomFieldSalesLocation,
                CustomFieldSerial = posCustomer.CustomFieldSerial,
                CustomFieldSerial1 = posCustomer.CustomFieldSerial1,
                CustomFieldSerial2 = posCustomer.CustomFieldSerial2
            };

            return response;
        }

        #endregion 1. POSCustomer Get By Id

        #region 4. POSCustomer Add Update
        //Temptory table to POSCustomer Add/Update
        public async Task<List<POSCustomerDemoListEntity>> POSCustomerAddUpdate()
        {
            var timeModified = await DbContext.POSCustomer.OrderByDescending(o => o.TimeModified).Select(s => s.TimeModified).FirstOrDefaultAsync();

            if (timeModified == null)
            {
                var CustomerDemo1 = await DbContext.POSCustomerDemo.ToListAsync();

                foreach (var customer in CustomerDemo1)
                {
                    var CustomerData = await DbContext.POSCustomer.FirstOrDefaultAsync(o => o.ListId.Equals(customer.ListId));

                    if (CustomerData == null)
                    {
                        await DbContext.AddRangeAsync(new POSCustomerListEntity
                        {
                            ListId = customer.ListId,
                            TimeCreated = customer.TimeCreated,
                            TimeModified = customer.TimeModified,
                            AccountBalance = customer.AccountBalance,
                            AccountLimit = customer.AccountLimit,
                            AmountPastDue = customer.AmountPastDue,
                            CompanyName = customer.CompanyName,
                            CustomerDiscType = customer.CustomerDiscType,
                            CustomerType = customer.CustomerType,
                            CustomerID = customer.CustomerID,
                            DefaultShipAddress = customer.DefaultShipAddress,
                            EMail = customer.EMail,
                            FirstName = customer.FirstName,
                            FullName = customer.FullName,
                            IsAcceptingChecks = customer.IsAcceptingChecks,
                            IsNoShipToBilling = customer.IsNoShipToBilling,
                            IsOkToEMail = customer.IsOkToEMail,
                            IsRewardsMember = customer.IsRewardsMember,
                            IsUsingChargeAccount = customer.IsUsingChargeAccount,
                            IsUsingWithQB = customer.IsUsingWithQB,
                            LastName = customer.LastName,
                            LastSale = customer.LastSale,
                            Notes = customer.Notes,
                            Phone = customer.Phone,
                            Phone2 = customer.Phone2,
                            Phone3 = customer.Phone3,
                            Phone4 = customer.Phone4,
                            PriceLevelNumber = customer.PriceLevelNumber,
                            Salutation = customer.Salutation,
                            StoreExchangeStatus = customer.StoreExchangeStatus,
                            TaxCategory = customer.TaxCategory,
                            WebNumber = customer.WebNumber,
                            BillAddressCity = customer.BillAddressCity,
                            BillAddressCountry = customer.BillAddressCountry,
                            BillAddressPostalCode = customer.BillAddressPostalCode,
                            BillAddressState = customer.BillAddressState,
                            BillAddressStreet = customer.BillAddressStreet,
                            BillAddressStreet2 = customer.BillAddressStreet2,
                            CustomFieldOther = customer.CustomFieldOther,
                            CustomFieldSalesLocation = customer.CustomFieldSalesLocation,
                            CustomFieldSerial = customer.CustomFieldSerial,
                            CustomFieldSerial1 = customer.CustomFieldSerial1,
                            CustomFieldSerial2 = customer.CustomFieldSerial2
                        });
                    }
                }
                try
                {
                    await DbContext.SaveChangesAsync();
                }
                catch (Exception e) { }
                return CustomerDemo1;
            }
            var CustomerDemo = await DbContext.POSCustomerDemo.Where(w => w.TimeModified > timeModified.Value).ToListAsync();

            if (CustomerDemo != null) 
            {
                foreach (var customer in CustomerDemo)
                {
                    var CustomerData = await DbContext.POSCustomer.FirstOrDefaultAsync(o => o.ListId.Equals(customer.ListId));

                    if (CustomerData == null)
                    {
                        await DbContext.AddRangeAsync(new POSCustomerListEntity
                        {
                            ListId = customer.ListId,
                            TimeCreated = customer.TimeCreated,
                            TimeModified = customer.TimeModified,
                            AccountBalance = customer.AccountBalance,
                            AccountLimit = customer.AccountLimit,
                            AmountPastDue = customer.AmountPastDue,
                            CompanyName = customer.CompanyName,
                            CustomerDiscType = customer.CustomerDiscType,
                            CustomerType = customer.CustomerType,
                            CustomerID = customer.CustomerID,
                            DefaultShipAddress = customer.DefaultShipAddress,
                            EMail = customer.EMail,
                            FirstName = customer.FirstName,
                            FullName = customer.FullName,
                            IsAcceptingChecks = customer.IsAcceptingChecks,
                            IsNoShipToBilling = customer.IsNoShipToBilling,
                            IsOkToEMail = customer.IsOkToEMail,
                            IsRewardsMember = customer.IsRewardsMember,
                            IsUsingChargeAccount = customer.IsUsingChargeAccount,
                            IsUsingWithQB = customer.IsUsingWithQB,
                            LastName = customer.LastName,
                            LastSale = customer.LastSale,
                            Notes = customer.Notes,
                            Phone = customer.Phone,
                            Phone2 = customer.Phone2,
                            Phone3 = customer.Phone3,
                            Phone4 = customer.Phone4,
                            PriceLevelNumber = customer.PriceLevelNumber,
                            Salutation = customer.Salutation,
                            StoreExchangeStatus = customer.StoreExchangeStatus,
                            TaxCategory = customer.TaxCategory,
                            WebNumber = customer.WebNumber,
                            BillAddressCity = customer.BillAddressCity,
                            BillAddressCountry = customer.BillAddressCountry,
                            BillAddressPostalCode = customer.BillAddressPostalCode,
                            BillAddressState = customer.BillAddressState,
                            BillAddressStreet = customer.BillAddressStreet,
                            BillAddressStreet2 = customer.BillAddressStreet2,
                            CustomFieldOther = customer.CustomFieldOther,
                            CustomFieldSalesLocation = customer.CustomFieldSalesLocation,
                            CustomFieldSerial = customer.CustomFieldSerial,
                            CustomFieldSerial1 = customer.CustomFieldSerial1,
                            CustomFieldSerial2 = customer.CustomFieldSerial2
                        });
                    }
                    else
                    {
                        CustomerData.TimeModified = customer.TimeModified;
                        CustomerData.AccountBalance = customer.AccountBalance;
                        CustomerData.AccountLimit = customer.AccountLimit;
                        CustomerData.AmountPastDue = customer.AmountPastDue;
                        CustomerData.CompanyName = customer.CompanyName;
                        CustomerData.CustomerDiscType = customer.CustomerDiscType;
                        CustomerData.CustomerType = customer.CustomerType;
                        CustomerData.CustomerID = customer.CustomerID;
                        CustomerData.DefaultShipAddress = customer.DefaultShipAddress;
                        CustomerData.EMail = customer.EMail;
                        CustomerData.FirstName = customer.FirstName;
                        CustomerData.FullName = customer.FullName;
                        CustomerData.IsAcceptingChecks = customer.IsAcceptingChecks;
                        CustomerData.IsNoShipToBilling = customer.IsNoShipToBilling;
                        CustomerData.IsOkToEMail = customer.IsOkToEMail;
                        CustomerData.IsRewardsMember = customer.IsRewardsMember;
                        CustomerData.IsUsingChargeAccount = customer.IsUsingChargeAccount;
                        CustomerData.IsUsingWithQB = customer.IsUsingWithQB;
                        CustomerData.LastName = customer.LastName;
                        CustomerData.LastSale = customer.LastSale;
                        CustomerData.Notes = customer.Notes;
                        CustomerData.Phone = customer.Phone;
                        CustomerData.Phone2 = customer.Phone2;
                        CustomerData.Phone3 = customer.Phone3;
                        CustomerData.Phone4 = customer.Phone4;
                        CustomerData.PriceLevelNumber = customer.PriceLevelNumber;
                        CustomerData.Salutation = customer.Salutation;
                        CustomerData.StoreExchangeStatus = customer.StoreExchangeStatus;
                        CustomerData.TaxCategory = customer.TaxCategory;
                        CustomerData.WebNumber = customer.WebNumber;
                        CustomerData.BillAddressCity = customer.BillAddressCity;
                        CustomerData.BillAddressCountry = customer.BillAddressCountry;
                        CustomerData.BillAddressPostalCode = customer.BillAddressPostalCode;
                        CustomerData.BillAddressState = customer.BillAddressState;
                        CustomerData.BillAddressStreet = customer.BillAddressStreet;
                        CustomerData.BillAddressStreet2 = customer.BillAddressStreet2;
                        CustomerData.CustomFieldOther = customer.CustomFieldOther;
                        CustomerData.CustomFieldSalesLocation = customer.CustomFieldSalesLocation;
                        CustomerData.CustomFieldSerial = customer.CustomFieldSerial;
                        CustomerData.CustomFieldSerial1 = customer.CustomFieldSerial1;
                        CustomerData.CustomFieldSerial2 = customer.CustomFieldSerial2;

                        var response = new PosCustomerGetByIdResponse
                        {
                            TimeModified = customer.TimeModified,
                            AccountBalance = customer.AccountBalance,
                            AccountLimit = customer.AccountLimit,
                            AmountPastDue = customer.AmountPastDue,
                            CompanyName = customer.CompanyName,
                            CustomerDiscType = customer.CustomerDiscType,
                            CustomerType = customer.CustomerType,
                            CustomerID = customer.CustomerID,
                            DefaultShipAddress = customer.DefaultShipAddress,
                            EMail = customer.EMail,
                            FirstName = customer.FirstName,
                            FullName = customer.FullName,
                            IsAcceptingChecks = customer.IsAcceptingChecks,
                            IsNoShipToBilling = customer.IsNoShipToBilling,
                            IsOkToEMail = customer.IsOkToEMail,
                            IsRewardsMember = customer.IsRewardsMember,
                            IsUsingChargeAccount = customer.IsUsingChargeAccount,
                            IsUsingWithQB = customer.IsUsingWithQB,
                            LastName = customer.LastName,
                            LastSale = customer.LastSale,
                            Notes = customer.Notes,
                            Phone = customer.Phone,
                            Phone2 = customer.Phone2,
                            Phone3 = customer.Phone3,
                            Phone4 = customer.Phone4,
                            PriceLevelNumber = customer.PriceLevelNumber,
                            Salutation = customer.Salutation,
                            StoreExchangeStatus = customer.StoreExchangeStatus,
                            TaxCategory = customer.TaxCategory,
                            WebNumber = customer.WebNumber,
                            BillAddressCity = customer.BillAddressCity,
                            BillAddressCountry = customer.BillAddressCountry,
                            BillAddressPostalCode = customer.BillAddressPostalCode,
                            BillAddressState = customer.BillAddressState,
                            BillAddressStreet = customer.BillAddressStreet,
                            BillAddressStreet2 = customer.BillAddressStreet2,
                            CustomFieldOther = customer.CustomFieldOther,
                            CustomFieldSalesLocation = customer.CustomFieldSalesLocation,
                            CustomFieldSerial = customer.CustomFieldSerial,
                            CustomFieldSerial1 = customer.CustomFieldSerial1,
                            CustomFieldSerial2 = customer.CustomFieldSerial2
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
            return CustomerDemo;
        }
        #endregion

        #region Dispose
        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
