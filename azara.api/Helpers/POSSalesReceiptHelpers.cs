using azara.models.Entities.POSSales;
using azara.models.Responses.POSSalesReceipt;

namespace azara.api.Helpers
{
    public class POSSalesReceiptHelpers : IDisposable
    {
        #region Constructor

        AzaraContext DbContext { get; set; }

        ICrypto Crypto { get; set; }

        public POSSalesReceiptHelpers(
            AzaraContext DbContext,
            ICrypto Crypto)
        {
            this.DbContext = DbContext;
            this.Crypto = Crypto;
        }

        #endregion Constructor

        #region 1. POSSalesReceipt Add Update
        //Temptory table to POSSalesReceipt Add/Update
        public async Task<List<POSSalesReceiptTMP>> POSSalesReceiptAddUpdate()
        {
            var timeModified = await DbContext.POSSalesReceipt.OrderByDescending(o => o.TimeModified).Select(s => s.TimeModified).FirstOrDefaultAsync();
            if (timeModified != null)
            {
                var posSalesReceiptDemo = await DbContext.POSSalesReceiptTMP.Where(w => w.TimeModified > timeModified.Value).ToListAsync();

                foreach (var salesReceipt in posSalesReceiptDemo)
                {
                    var salesReceiptData = await DbContext.POSSalesReceipt.FirstOrDefaultAsync(o => o.TxnID.Equals(salesReceipt.TxnID));

                    if (salesReceiptData == null)
                    {
                        await DbContext.AddRangeAsync(new POSSalesReceipt
                        {
                            TxnID = salesReceipt.TxnID,
                            TimeCreated = salesReceipt.TimeCreated,
                            TimeModified = salesReceipt.TimeModified,
                            Associate = salesReceipt.Associate,
                            Cashier = salesReceipt.Cashier,
                            Comments = salesReceipt.Comments,
                            CustomerListID = salesReceipt.CustomerListID,
                            Discount = salesReceipt.Discount,
                            DiscountPercent = salesReceipt.DiscountPercent,
                            HistoryDocStatus = salesReceipt.HistoryDocStatus,
                            ItemsCount = salesReceipt.ItemsCount,
                            PriceLevelNumber = salesReceipt.PriceLevelNumber,
                            PromoCode = salesReceipt.PromoCode,
                            QuickBooksFlag = salesReceipt.QuickBooksFlag,
                            SalesOrderTxnID = salesReceipt.SalesOrderTxnID,
                            SalesReceiptNumber = salesReceipt.SalesReceiptNumber,
                            SalesReceiptType = salesReceipt.SalesReceiptType,
                            ShipDate = salesReceipt.ShipDate,
                            StoreExchangeStatus = salesReceipt.StoreExchangeStatus,
                            StoreNumber = salesReceipt.StoreNumber,
                            Subtotal = salesReceipt.Subtotal,
                            TaxAmount = salesReceipt.TaxAmount,
                            TaxCategory = salesReceipt.TaxCategory,
                            TaxPercentage = salesReceipt.TaxPercentage,
                            TenderType = salesReceipt.TenderType,
                            Total = salesReceipt.Total,
                            TrackingNumber = salesReceipt.TrackingNumber,
                            TxnDate = salesReceipt.TxnDate,
                            TxnState = salesReceipt.TxnState,
                            Workstation = salesReceipt.Workstation,
                            BillingInformationCity = salesReceipt.BillingInformationCity,
                            BillingInformationCompanyName = salesReceipt.BillingInformationCompanyName,
                            BillingInformationCountry = salesReceipt.BillingInformationCountry,
                            BillingInformationFirstName = salesReceipt.BillingInformationFirstName,
                            BillingInformationLastName = salesReceipt.BillingInformationLastName,
                            BillingInformationPhone = salesReceipt.BillingInformationPhone,
                            BillingInformationPhone2 = salesReceipt.BillingInformationPhone2,
                            BillingInformationPhone3 = salesReceipt.BillingInformationPhone3,
                            BillingInformationPhone4 = salesReceipt.BillingInformationPhone4,
                            BillingInformationPostalCode = salesReceipt.BillingInformationPostalCode,
                            BillingInformationSalutation = salesReceipt.BillingInformationSalutation,
                            BillingInformationState = salesReceipt.BillingInformationState,
                            BillingInformationStreet = salesReceipt.BillingInformationStreet,
                            ShippingInformationCity = salesReceipt.ShippingInformationCity,
                            ShippingInformationCompanyName = salesReceipt.ShippingInformationCompanyName,
                            ShippingInformationCountry = salesReceipt.ShippingInformationCountry,
                            ShippingInformationFullName = salesReceipt.ShippingInformationFullName,
                            ShippingInformationPhone = salesReceipt.ShippingInformationPhone,
                            ShippingInformationPhone2 = salesReceipt.ShippingInformationPhone2,
                            ShippingInformationPhone3 = salesReceipt.ShippingInformationPhone3,
                            ShippingInformationPhone4 = salesReceipt.ShippingInformationPhone4,
                            ShippingInformationPostalCode = salesReceipt.ShippingInformationPostalCode,
                            ShippingInformationShipBy = salesReceipt.ShippingInformationShipBy,
                            ShippingInformationShipping = salesReceipt.ShippingInformationShipping,
                            ShippingInformationState = salesReceipt.ShippingInformationState,
                            ShippingInformationStreet = salesReceipt.ShippingInformationStreet,
                            TenderAccount01TenderAmount = salesReceipt.TenderAccount01TenderAmount,
                            TenderAccount01TipAmount = salesReceipt.TenderAccount01TipAmount,
                            TenderAccount02TenderAmount = salesReceipt.TenderAccount02TenderAmount,
                            TenderAccount02TipAmount = salesReceipt.TenderAccount02TipAmount,
                            TenderAccount03TenderAmount = salesReceipt.TenderAccount03TenderAmount,
                            TenderAccount03TipAmount = salesReceipt.TenderAccount03TipAmount,
                            TenderAccount04TenderAmount = salesReceipt.TenderAccount04TenderAmount,
                            TenderAccount04TipAmount = salesReceipt.TenderAccount04TipAmount,
                            TenderAccount05TenderAmount = salesReceipt.TenderAccount05TenderAmount,
                            TenderAccount06TenderAmount = salesReceipt.TenderAccount06TenderAmount,
                            TenderAccount06TipAmount = salesReceipt.TenderAccount06TipAmount,
                            TenderAccount07TenderAmount = salesReceipt.TenderAccount07TenderAmount,
                            TenderAccount07TipAmount = salesReceipt.TenderAccount07TipAmount,
                            TenderAccount08TenderAmount = salesReceipt.TenderAccount08TenderAmount,
                            TenderAccount08TipAmount = salesReceipt.TenderAccount08TipAmount,
                            TenderAccount09TenderAmount = salesReceipt.TenderAccount09TenderAmount,
                            TenderAccount09TipAmount = salesReceipt.TenderAccount09TipAmount,
                            TenderAccount10TenderAmount = salesReceipt.TenderAccount10TenderAmount,
                            TenderAccount10TipAmount = salesReceipt.TenderAccount10TipAmount,
                            TenderCheck01CheckNumber = salesReceipt.TenderCheck01CheckNumber,
                            TenderCheck01TenderAmount = salesReceipt.TenderCheck01TenderAmount,
                            TenderCheck02CheckNumber = salesReceipt.TenderCheck02CheckNumber,
                            TenderCheck02TenderAmount = salesReceipt.TenderCheck02TenderAmount,
                            TenderCheck03CheckNumber = salesReceipt.TenderCheck03CheckNumber,
                            TenderCheck03TenderAmount = salesReceipt.TenderCheck03TenderAmount,
                            TenderCheck04CheckNumber = salesReceipt.TenderCheck04CheckNumber,
                            TenderCheck04TenderAmount = salesReceipt.TenderCheck04TenderAmount,
                            TenderCheck05CheckNumber = salesReceipt.TenderCheck05CheckNumber,
                            TenderCheck05TenderAmount = salesReceipt.TenderCheck05TenderAmount,
                            TenderCheck06CheckNumber = salesReceipt.TenderCheck06CheckNumber,
                            TenderCheck06TenderAmount = salesReceipt.TenderCheck06TenderAmount,
                            TenderCheck07CheckNumber = salesReceipt.TenderCheck07CheckNumber,
                            TenderCheck07TenderAmount = salesReceipt.TenderCheck07TenderAmount,
                            TenderCheck08CheckNumber = salesReceipt.TenderCheck08CheckNumber,
                            TenderCheck08TenderAmount = salesReceipt.TenderCheck08TenderAmount,
                            TenderCheck09CheckNumber = salesReceipt.TenderCheck09CheckNumber,
                            TenderCheck09TenderAmount = salesReceipt.TenderCheck09TenderAmount,
                            TenderCheck10CheckNumber = salesReceipt.TenderCheck10CheckNumber,
                            TenderCheck10TenderAmount = salesReceipt.TenderCheck10TenderAmount,
                            TenderCash01TenderAmount = salesReceipt.TenderCash01TenderAmount,
                            TenderCash02TenderAmount = salesReceipt.TenderCash02TenderAmount,
                            TenderCash03TenderAmount = salesReceipt.TenderCash03TenderAmount,
                            TenderCash04TenderAmount = salesReceipt.TenderCash04TenderAmount,
                            TenderCash05TenderAmount = salesReceipt.TenderCash05TenderAmount,
                            TenderCash06TenderAmount = salesReceipt.TenderCash06TenderAmount,
                            TenderCash07TenderAmount = salesReceipt.TenderCash07TenderAmount,
                            TenderCash08TenderAmount = salesReceipt.TenderCash08TenderAmount,
                            TenderCash09TenderAmount = salesReceipt.TenderCash09TenderAmount,
                            TenderCash10TenderAmount = salesReceipt.TenderCash10TenderAmount,
                            TenderCreditCard01CardName = salesReceipt.TenderCreditCard01CardName,
                            TenderCreditCard01TenderAmount = salesReceipt.TenderCreditCard01TenderAmount,
                            TenderCreditCard01TipAmount = salesReceipt.TenderCreditCard01TipAmount,
                            TenderCreditCard02CardName = salesReceipt.TenderCreditCard02CardName,
                            TenderCreditCard02TenderAmount = salesReceipt.TenderCreditCard02TenderAmount,
                            TenderCreditCard02TipAmount = salesReceipt.TenderCreditCard02TipAmount,
                            TenderCreditCard03CardName = salesReceipt.TenderCreditCard03CardName,
                            TenderCreditCard03TenderAmount = salesReceipt.TenderCreditCard03TenderAmount,
                            TenderCreditCard03TipAmount = salesReceipt.TenderCreditCard03TipAmount,
                            TenderCreditCard04CardName = salesReceipt.TenderCreditCard04CardName,
                            TenderCreditCard04TenderAmount = salesReceipt.TenderCreditCard04TenderAmount,
                            TenderCreditCard04TipAmount = salesReceipt.TenderCreditCard04TipAmount,
                            TenderCreditCard05CardName = salesReceipt.TenderCreditCard05CardName,
                            TenderCreditCard05TenderAmount = salesReceipt.TenderCreditCard05TenderAmount,
                            TenderCreditCard05TipAmount = salesReceipt.TenderCreditCard05TipAmount,
                            TenderDebitCard01Cashback = salesReceipt.TenderDebitCard01Cashback,
                            TenderDebitCard01TenderAmount = salesReceipt.TenderDebitCard01TenderAmount,
                            TenderDebitCard02Cashback = salesReceipt.TenderDebitCard02Cashback,
                            TenderDebitCard02TenderAmount = salesReceipt.TenderDebitCard02TenderAmount,
                            TenderDebitCard03Cashback = salesReceipt.TenderDebitCard03Cashback,
                            TenderDebitCard03TenderAmount = salesReceipt.TenderDebitCard03TenderAmount,
                            TenderDebitCard04Cashback = salesReceipt.TenderDebitCard04Cashback,
                            TenderDebitCard04TenderAmount = salesReceipt.TenderDebitCard04TenderAmount,
                            TenderDebitCard05Cashback = salesReceipt.TenderDebitCard05Cashback,
                            TenderDebitCard05TenderAmount = salesReceipt.TenderDebitCard05TenderAmount,
                            TenderDeposit01TenderAmount = salesReceipt.TenderDeposit01TenderAmount,
                            TenderDeposit02TenderAmount = salesReceipt.TenderDeposit02TenderAmount,
                            TenderDeposit03TenderAmount = salesReceipt.TenderDeposit03TenderAmount,
                            TenderDeposit04TenderAmount = salesReceipt.TenderDeposit04TenderAmount,
                            TenderDeposit05TenderAmount = salesReceipt.TenderDeposit05TenderAmount,
                            TenderGift01TenderAmount = salesReceipt.TenderGift01TenderAmount,
                            TenderGift01GiftCertificateNumber = salesReceipt.TenderGift01GiftCertificateNumber,
                            TenderGift02TenderAmount = salesReceipt.TenderGift02TenderAmount,
                            TenderGift02GiftCertificateNumber = salesReceipt.TenderGift02GiftCertificateNumber,
                            TenderGift03TenderAmount = salesReceipt.TenderGift03TenderAmount,
                            TenderGift03GiftCertificateNumber = salesReceipt.TenderGift03GiftCertificateNumber,
                            TenderGift04TenderAmount = salesReceipt.TenderGift04TenderAmount,
                            TenderGift04GiftCertificateNumber = salesReceipt.TenderGift04GiftCertificateNumber,
                            TenderGift05TenderAmount = salesReceipt.TenderGift05TenderAmount,
                            TenderGift05GiftCertificateNumber = salesReceipt.TenderGift05GiftCertificateNumber,
                            TenderGift06TenderAmount = salesReceipt.TenderGift06TenderAmount,
                            TenderGift06GiftCertificateNumber = salesReceipt.TenderGift06GiftCertificateNumber,
                            TenderGift07TenderAmount = salesReceipt.TenderGift07TenderAmount,
                            TenderGift07GiftCertificateNumber = salesReceipt.TenderGift07GiftCertificateNumber,
                            TenderGift08TenderAmount = salesReceipt.TenderGift08TenderAmount,
                            TenderGift08GiftCertificateNumber = salesReceipt.TenderGift08GiftCertificateNumber,
                            TenderGift09TenderAmount = salesReceipt.TenderGift09TenderAmount,
                            TenderGift10TenderAmount = salesReceipt.TenderGift10TenderAmount,
                            TenderGift10GiftCertificateNumber = salesReceipt.TenderGift10GiftCertificateNumber,
                            TenderGiftCard01TenderAmount = salesReceipt.TenderGiftCard01TenderAmount,
                            TenderGiftCard01TipAmount = salesReceipt.TenderGiftCard01TipAmount,
                            TenderGiftCard02TenderAmount = salesReceipt.TenderGiftCard02TenderAmount,
                            TenderGiftCard02TipAmount = salesReceipt.TenderGiftCard02TipAmount,
                            TenderGiftCard03TenderAmount = salesReceipt.TenderGiftCard03TenderAmount,
                            TenderGiftCard03TipAmount = salesReceipt.TenderGiftCard03TipAmount,
                            TenderGiftCard04TenderAmount = salesReceipt.TenderGiftCard04TenderAmount,
                            TenderGiftCard04TipAmount = salesReceipt.TenderGiftCard04TipAmount,
                            TenderGiftCard05TenderAmount = salesReceipt.TenderGiftCard05TenderAmount,
                            TenderGiftCard05TipAmount = salesReceipt.TenderGiftCard05TipAmount,
                            FQSaveToCache = salesReceipt.FQSaveToCache,
                        });
                    }
                    else
                    {
                        salesReceiptData.TimeModified = salesReceipt.TimeModified;
                        salesReceiptData.Associate = salesReceipt.Associate;
                        salesReceiptData.Cashier = salesReceipt.Cashier;
                        salesReceiptData.Comments = salesReceipt.Comments;
                        salesReceiptData.CustomerListID = salesReceipt.CustomerListID;
                        salesReceiptData.Discount = salesReceipt.Discount;
                        salesReceiptData.DiscountPercent = salesReceipt.DiscountPercent;
                        salesReceiptData.HistoryDocStatus = salesReceipt.HistoryDocStatus;
                        salesReceiptData.ItemsCount = salesReceipt.ItemsCount;
                        salesReceiptData.PriceLevelNumber = salesReceipt.PriceLevelNumber;
                        salesReceiptData.PromoCode = salesReceipt.PromoCode;
                        salesReceiptData.QuickBooksFlag = salesReceipt.QuickBooksFlag;
                        salesReceiptData.SalesOrderTxnID = salesReceipt.SalesOrderTxnID;
                        salesReceiptData.SalesReceiptNumber = salesReceipt.SalesReceiptNumber;
                        salesReceiptData.SalesReceiptType = salesReceipt.SalesReceiptType;
                        salesReceiptData.ShipDate = salesReceipt.ShipDate;
                        salesReceiptData.StoreExchangeStatus = salesReceipt.StoreExchangeStatus;
                        salesReceiptData.StoreNumber = salesReceipt.StoreNumber;
                        salesReceiptData.Subtotal = salesReceipt.Subtotal;
                        salesReceiptData.TaxAmount = salesReceipt.TaxAmount;
                        salesReceiptData.TaxCategory = salesReceipt.TaxCategory;
                        salesReceiptData.TaxPercentage = salesReceipt.TaxPercentage;
                        salesReceiptData.TenderType = salesReceipt.TenderType;
                        salesReceiptData.Total = salesReceipt.Total;
                        salesReceiptData.TrackingNumber = salesReceipt.TrackingNumber;
                        salesReceiptData.TxnDate = salesReceipt.TxnDate;
                        salesReceiptData.TxnState = salesReceipt.TxnState;
                        salesReceiptData.Workstation = salesReceipt.Workstation;
                        salesReceiptData.BillingInformationCity = salesReceipt.BillingInformationCity;
                        salesReceiptData.BillingInformationCompanyName = salesReceipt.BillingInformationCompanyName;
                        salesReceiptData.BillingInformationCountry = salesReceipt.BillingInformationCountry;
                        salesReceiptData.BillingInformationFirstName = salesReceipt.BillingInformationFirstName;
                        salesReceiptData.BillingInformationLastName = salesReceipt.BillingInformationLastName;
                        salesReceiptData.BillingInformationPhone = salesReceipt.BillingInformationPhone;
                        salesReceiptData.BillingInformationPhone2 = salesReceipt.BillingInformationPhone2;
                        salesReceiptData.BillingInformationPhone3 = salesReceipt.BillingInformationPhone3;
                        salesReceiptData.BillingInformationPhone4 = salesReceipt.BillingInformationPhone4;
                        salesReceiptData.BillingInformationPostalCode = salesReceipt.BillingInformationPostalCode;
                        salesReceiptData.BillingInformationSalutation = salesReceipt.BillingInformationSalutation;
                        salesReceiptData.BillingInformationState = salesReceipt.BillingInformationState;
                        salesReceiptData.BillingInformationStreet = salesReceipt.BillingInformationStreet;
                        salesReceiptData.ShippingInformationCity = salesReceipt.ShippingInformationCity;
                        salesReceiptData.ShippingInformationCompanyName = salesReceipt.ShippingInformationCompanyName;
                        salesReceiptData.ShippingInformationCountry = salesReceipt.ShippingInformationCountry;
                        salesReceiptData.ShippingInformationFullName = salesReceipt.ShippingInformationFullName;
                        salesReceiptData.ShippingInformationPhone = salesReceipt.ShippingInformationPhone;
                        salesReceiptData.ShippingInformationPhone2 = salesReceipt.ShippingInformationPhone2;
                        salesReceiptData.ShippingInformationPhone3 = salesReceipt.ShippingInformationPhone3;
                        salesReceiptData.ShippingInformationPhone4 = salesReceipt.ShippingInformationPhone4;
                        salesReceiptData.ShippingInformationPostalCode = salesReceipt.ShippingInformationPostalCode;
                        salesReceiptData.ShippingInformationShipBy = salesReceipt.ShippingInformationShipBy;
                        salesReceiptData.ShippingInformationShipping = salesReceipt.ShippingInformationShipping;
                        salesReceiptData.ShippingInformationState = salesReceipt.ShippingInformationState;
                        salesReceiptData.ShippingInformationStreet = salesReceipt.ShippingInformationStreet;
                        salesReceiptData.TenderAccount01TenderAmount = salesReceipt.TenderAccount01TenderAmount;
                        salesReceiptData.TenderAccount01TipAmount = salesReceipt.TenderAccount01TipAmount;
                        salesReceiptData.TenderAccount02TenderAmount = salesReceipt.TenderAccount02TenderAmount;
                        salesReceiptData.TenderAccount02TipAmount = salesReceipt.TenderAccount02TipAmount;
                        salesReceiptData.TenderAccount03TenderAmount = salesReceipt.TenderAccount03TenderAmount;
                        salesReceiptData.TenderAccount03TipAmount = salesReceipt.TenderAccount03TipAmount;
                        salesReceiptData.TenderAccount04TenderAmount = salesReceipt.TenderAccount04TenderAmount;
                        salesReceiptData.TenderAccount04TipAmount = salesReceipt.TenderAccount04TipAmount;
                        salesReceiptData.TenderAccount05TenderAmount = salesReceipt.TenderAccount05TenderAmount;
                        salesReceiptData.TenderAccount06TenderAmount = salesReceipt.TenderAccount06TenderAmount;
                        salesReceiptData.TenderAccount06TipAmount = salesReceipt.TenderAccount06TipAmount;
                        salesReceiptData.TenderAccount07TenderAmount = salesReceipt.TenderAccount07TenderAmount;
                        salesReceiptData.TenderAccount07TipAmount = salesReceipt.TenderAccount07TipAmount;
                        salesReceiptData.TenderAccount08TenderAmount = salesReceipt.TenderAccount08TenderAmount;
                        salesReceiptData.TenderAccount08TipAmount = salesReceipt.TenderAccount08TipAmount;
                        salesReceiptData.TenderAccount09TenderAmount = salesReceipt.TenderAccount09TenderAmount;
                        salesReceiptData.TenderAccount09TipAmount = salesReceipt.TenderAccount09TipAmount;
                        salesReceiptData.TenderAccount10TenderAmount = salesReceipt.TenderAccount10TenderAmount;
                        salesReceiptData.TenderAccount10TipAmount = salesReceipt.TenderAccount10TipAmount;
                        salesReceiptData.TenderCheck01CheckNumber = salesReceipt.TenderCheck01CheckNumber;
                        salesReceiptData.TenderCheck01TenderAmount = salesReceipt.TenderCheck01TenderAmount;
                        salesReceiptData.TenderCheck02CheckNumber = salesReceipt.TenderCheck02CheckNumber;
                        salesReceiptData.TenderCheck02TenderAmount = salesReceipt.TenderCheck02TenderAmount;
                        salesReceiptData.TenderCheck03CheckNumber = salesReceipt.TenderCheck03CheckNumber;
                        salesReceiptData.TenderCheck03TenderAmount = salesReceipt.TenderCheck03TenderAmount;
                        salesReceiptData.TenderCheck04CheckNumber = salesReceipt.TenderCheck04CheckNumber;
                        salesReceiptData.TenderCheck04TenderAmount = salesReceipt.TenderCheck04TenderAmount;
                        salesReceiptData.TenderCheck05CheckNumber = salesReceipt.TenderCheck05CheckNumber;
                        salesReceiptData.TenderCheck05TenderAmount = salesReceipt.TenderCheck05TenderAmount;
                        salesReceiptData.TenderCheck06CheckNumber = salesReceipt.TenderCheck06CheckNumber;
                        salesReceiptData.TenderCheck06TenderAmount = salesReceipt.TenderCheck06TenderAmount;
                        salesReceiptData.TenderCheck07CheckNumber = salesReceipt.TenderCheck07CheckNumber;
                        salesReceiptData.TenderCheck07TenderAmount = salesReceipt.TenderCheck07TenderAmount;
                        salesReceiptData.TenderCheck08CheckNumber = salesReceipt.TenderCheck08CheckNumber;
                        salesReceiptData.TenderCheck08TenderAmount = salesReceipt.TenderCheck08TenderAmount;
                        salesReceiptData.TenderCheck09CheckNumber = salesReceipt.TenderCheck09CheckNumber;
                        salesReceiptData.TenderCheck09TenderAmount = salesReceipt.TenderCheck09TenderAmount;
                        salesReceiptData.TenderCheck10CheckNumber = salesReceipt.TenderCheck10CheckNumber;
                        salesReceiptData.TenderCheck10TenderAmount = salesReceipt.TenderCheck10TenderAmount;
                        salesReceiptData.TenderCash01TenderAmount = salesReceipt.TenderCash01TenderAmount;
                        salesReceiptData.TenderCash02TenderAmount = salesReceipt.TenderCash02TenderAmount;
                        salesReceiptData.TenderCash03TenderAmount = salesReceipt.TenderCash03TenderAmount;
                        salesReceiptData.TenderCash04TenderAmount = salesReceipt.TenderCash04TenderAmount;
                        salesReceiptData.TenderCash05TenderAmount = salesReceipt.TenderCash05TenderAmount;
                        salesReceiptData.TenderCash06TenderAmount = salesReceipt.TenderCash06TenderAmount;
                        salesReceiptData.TenderCash07TenderAmount = salesReceipt.TenderCash07TenderAmount;
                        salesReceiptData.TenderCash08TenderAmount = salesReceipt.TenderCash08TenderAmount;
                        salesReceiptData.TenderCash09TenderAmount = salesReceipt.TenderCash09TenderAmount;
                        salesReceiptData.TenderCash10TenderAmount = salesReceipt.TenderCash10TenderAmount;
                        salesReceiptData.TenderCreditCard01CardName = salesReceipt.TenderCreditCard01CardName;
                        salesReceiptData.TenderCreditCard01TenderAmount = salesReceipt.TenderCreditCard01TenderAmount;
                        salesReceiptData.TenderCreditCard01TipAmount = salesReceipt.TenderCreditCard01TipAmount;
                        salesReceiptData.TenderCreditCard02CardName = salesReceipt.TenderCreditCard02CardName;
                        salesReceiptData.TenderCreditCard02TenderAmount = salesReceipt.TenderCreditCard02TenderAmount;
                        salesReceiptData.TenderCreditCard02TipAmount = salesReceipt.TenderCreditCard02TipAmount;
                        salesReceiptData.TenderCreditCard03CardName = salesReceipt.TenderCreditCard03CardName;
                        salesReceiptData.TenderCreditCard03TenderAmount = salesReceipt.TenderCreditCard03TenderAmount;
                        salesReceiptData.TenderCreditCard03TipAmount = salesReceipt.TenderCreditCard03TipAmount;
                        salesReceiptData.TenderCreditCard04CardName = salesReceipt.TenderCreditCard04CardName;
                        salesReceiptData.TenderCreditCard04TenderAmount = salesReceipt.TenderCreditCard04TenderAmount;
                        salesReceiptData.TenderCreditCard04TipAmount = salesReceipt.TenderCreditCard04TipAmount;
                        salesReceiptData.TenderCreditCard05CardName = salesReceipt.TenderCreditCard05CardName;
                        salesReceiptData.TenderCreditCard05TenderAmount = salesReceipt.TenderCreditCard05TenderAmount;
                        salesReceiptData.TenderCreditCard05TipAmount = salesReceipt.TenderCreditCard05TipAmount;
                        salesReceiptData.TenderDebitCard01Cashback = salesReceipt.TenderDebitCard01Cashback;
                        salesReceiptData.TenderDebitCard01TenderAmount = salesReceipt.TenderDebitCard01TenderAmount;
                        salesReceiptData.TenderDebitCard02Cashback = salesReceipt.TenderDebitCard02Cashback;
                        salesReceiptData.TenderDebitCard02TenderAmount = salesReceipt.TenderDebitCard02TenderAmount;
                        salesReceiptData.TenderDebitCard03Cashback = salesReceipt.TenderDebitCard03Cashback;
                        salesReceiptData.TenderDebitCard03TenderAmount = salesReceipt.TenderDebitCard03TenderAmount;
                        salesReceiptData.TenderDebitCard04Cashback = salesReceipt.TenderDebitCard04Cashback;
                        salesReceiptData.TenderDebitCard04TenderAmount = salesReceipt.TenderDebitCard04TenderAmount;
                        salesReceiptData.TenderDebitCard05Cashback = salesReceipt.TenderDebitCard05Cashback;
                        salesReceiptData.TenderDebitCard05TenderAmount = salesReceipt.TenderDebitCard05TenderAmount;
                        salesReceiptData.TenderDeposit01TenderAmount = salesReceipt.TenderDeposit01TenderAmount;
                        salesReceiptData.TenderDeposit02TenderAmount = salesReceipt.TenderDeposit02TenderAmount;
                        salesReceiptData.TenderDeposit03TenderAmount = salesReceipt.TenderDeposit03TenderAmount;
                        salesReceiptData.TenderDeposit04TenderAmount = salesReceipt.TenderDeposit04TenderAmount;
                        salesReceiptData.TenderDeposit05TenderAmount = salesReceipt.TenderDeposit05TenderAmount;
                        salesReceiptData.TenderGift01TenderAmount = salesReceipt.TenderGift01TenderAmount;
                        salesReceiptData.TenderGift01GiftCertificateNumber = salesReceipt.TenderGift01GiftCertificateNumber;
                        salesReceiptData.TenderGift02TenderAmount = salesReceipt.TenderGift02TenderAmount;
                        salesReceiptData.TenderGift02GiftCertificateNumber = salesReceipt.TenderGift02GiftCertificateNumber;
                        salesReceiptData.TenderGift03TenderAmount = salesReceipt.TenderGift03TenderAmount;
                        salesReceiptData.TenderGift03GiftCertificateNumber = salesReceipt.TenderGift03GiftCertificateNumber;
                        salesReceiptData.TenderGift04TenderAmount = salesReceipt.TenderGift04TenderAmount;
                        salesReceiptData.TenderGift04GiftCertificateNumber = salesReceipt.TenderGift04GiftCertificateNumber;
                        salesReceiptData.TenderGift05TenderAmount = salesReceipt.TenderGift05TenderAmount;
                        salesReceiptData.TenderGift05GiftCertificateNumber = salesReceipt.TenderGift05GiftCertificateNumber;
                        salesReceiptData.TenderGift06TenderAmount = salesReceipt.TenderGift06TenderAmount;
                        salesReceiptData.TenderGift06GiftCertificateNumber = salesReceipt.TenderGift06GiftCertificateNumber;
                        salesReceiptData.TenderGift07TenderAmount = salesReceipt.TenderGift07TenderAmount;
                        salesReceiptData.TenderGift07GiftCertificateNumber = salesReceipt.TenderGift07GiftCertificateNumber;
                        salesReceiptData.TenderGift08TenderAmount = salesReceipt.TenderGift08TenderAmount;
                        salesReceiptData.TenderGift08GiftCertificateNumber = salesReceipt.TenderGift08GiftCertificateNumber;
                        salesReceiptData.TenderGift09TenderAmount = salesReceipt.TenderGift09TenderAmount;
                        salesReceiptData.TenderGift10TenderAmount = salesReceipt.TenderGift10TenderAmount;
                        salesReceiptData.TenderGift10GiftCertificateNumber = salesReceipt.TenderGift10GiftCertificateNumber;
                        salesReceiptData.TenderGiftCard01TenderAmount = salesReceipt.TenderGiftCard01TenderAmount;
                        salesReceiptData.TenderGiftCard01TipAmount = salesReceipt.TenderGiftCard01TipAmount;
                        salesReceiptData.TenderGiftCard02TenderAmount = salesReceipt.TenderGiftCard02TenderAmount;
                        salesReceiptData.TenderGiftCard02TipAmount = salesReceipt.TenderGiftCard02TipAmount;
                        salesReceiptData.TenderGiftCard03TenderAmount = salesReceipt.TenderGiftCard03TenderAmount;
                        salesReceiptData.TenderGiftCard03TipAmount = salesReceipt.TenderGiftCard03TipAmount;
                        salesReceiptData.TenderGiftCard04TenderAmount = salesReceipt.TenderGiftCard04TenderAmount;
                        salesReceiptData.TenderGiftCard04TipAmount = salesReceipt.TenderGiftCard04TipAmount;
                        salesReceiptData.TenderGiftCard05TenderAmount = salesReceipt.TenderGiftCard05TenderAmount;
                        salesReceiptData.TenderGiftCard05TipAmount = salesReceipt.TenderGiftCard05TipAmount;
                        salesReceiptData.FQSaveToCache = salesReceipt.FQSaveToCache;

                        var response = new POSSalesReceiptGetByIdResponse
                        {
                            TxnID = salesReceipt.TxnID,
                            TimeCreated = salesReceipt.TimeCreated,
                            TimeModified = salesReceipt.TimeModified,
                            Associate = salesReceipt.Associate,
                            Cashier = salesReceipt.Cashier,
                            Comments = salesReceipt.Comments,
                            CustomerListID = salesReceipt.CustomerListID,
                            Discount = salesReceipt.Discount,
                            DiscountPercent = salesReceipt.DiscountPercent,
                            HistoryDocStatus = salesReceipt.HistoryDocStatus,
                            ItemsCount = salesReceipt.ItemsCount,
                            PriceLevelNumber = salesReceipt.PriceLevelNumber,
                            PromoCode = salesReceipt.PromoCode,
                            QuickBooksFlag = salesReceipt.QuickBooksFlag,
                            SalesOrderTxnID = salesReceipt.SalesOrderTxnID,
                            SalesReceiptNumber = salesReceipt.SalesReceiptNumber,
                            SalesReceiptType = salesReceipt.SalesReceiptType,
                            ShipDate = salesReceipt.ShipDate,
                            StoreExchangeStatus = salesReceipt.StoreExchangeStatus,
                            StoreNumber = salesReceipt.StoreNumber,
                            Subtotal = salesReceipt.Subtotal,
                            TaxAmount = salesReceipt.TaxAmount,
                            TaxCategory = salesReceipt.TaxCategory,
                            TaxPercentage = salesReceipt.TaxPercentage,
                            TenderType = salesReceipt.TenderType,
                            Total = salesReceipt.Total,
                            TrackingNumber = salesReceipt.TrackingNumber,
                            TxnDate = salesReceipt.TxnDate,
                            TxnState = salesReceipt.TxnState,
                            Workstation = salesReceipt.Workstation,
                            BillingInformationCity = salesReceipt.BillingInformationCity,
                            BillingInformationCompanyName = salesReceipt.BillingInformationCompanyName,
                            BillingInformationCountry = salesReceipt.BillingInformationCountry,
                            BillingInformationFirstName = salesReceipt.BillingInformationFirstName,
                            BillingInformationLastName = salesReceipt.BillingInformationLastName,
                            BillingInformationPhone = salesReceipt.BillingInformationPhone,
                            BillingInformationPhone2 = salesReceipt.BillingInformationPhone2,
                            BillingInformationPhone3 = salesReceipt.BillingInformationPhone3,
                            BillingInformationPhone4 = salesReceipt.BillingInformationPhone4,
                            BillingInformationPostalCode = salesReceipt.BillingInformationPostalCode,
                            BillingInformationSalutation = salesReceipt.BillingInformationSalutation,
                            BillingInformationState = salesReceipt.BillingInformationState,
                            BillingInformationStreet = salesReceipt.BillingInformationStreet,
                            ShippingInformationCity = salesReceipt.ShippingInformationCity,
                            ShippingInformationCompanyName = salesReceipt.ShippingInformationCompanyName,
                            ShippingInformationCountry = salesReceipt.ShippingInformationCountry,
                            ShippingInformationFullName = salesReceipt.ShippingInformationFullName,
                            ShippingInformationPhone = salesReceipt.ShippingInformationPhone,
                            ShippingInformationPhone2 = salesReceipt.ShippingInformationPhone2,
                            ShippingInformationPhone3 = salesReceipt.ShippingInformationPhone3,
                            ShippingInformationPhone4 = salesReceipt.ShippingInformationPhone4,
                            ShippingInformationPostalCode = salesReceipt.ShippingInformationPostalCode,
                            ShippingInformationShipBy = salesReceipt.ShippingInformationShipBy,
                            ShippingInformationShipping = salesReceipt.ShippingInformationShipping,
                            ShippingInformationState = salesReceipt.ShippingInformationState,
                            ShippingInformationStreet = salesReceipt.ShippingInformationStreet,
                            TenderAccount01TenderAmount = salesReceipt.TenderAccount01TenderAmount,
                            TenderAccount01TipAmount = salesReceipt.TenderAccount01TipAmount,
                            TenderAccount02TenderAmount = salesReceipt.TenderAccount02TenderAmount,
                            TenderAccount02TipAmount = salesReceipt.TenderAccount02TipAmount,
                            TenderAccount03TenderAmount = salesReceipt.TenderAccount03TenderAmount,
                            TenderAccount03TipAmount = salesReceipt.TenderAccount03TipAmount,
                            TenderAccount04TenderAmount = salesReceipt.TenderAccount04TenderAmount,
                            TenderAccount04TipAmount = salesReceipt.TenderAccount04TipAmount,
                            TenderAccount05TenderAmount = salesReceipt.TenderAccount05TenderAmount,
                            TenderAccount06TenderAmount = salesReceipt.TenderAccount06TenderAmount,
                            TenderAccount06TipAmount = salesReceipt.TenderAccount06TipAmount,
                            TenderAccount07TenderAmount = salesReceipt.TenderAccount07TenderAmount,
                            TenderAccount07TipAmount = salesReceipt.TenderAccount07TipAmount,
                            TenderAccount08TenderAmount = salesReceipt.TenderAccount08TenderAmount,
                            TenderAccount08TipAmount = salesReceipt.TenderAccount08TipAmount,
                            TenderAccount09TenderAmount = salesReceipt.TenderAccount09TenderAmount,
                            TenderAccount09TipAmount = salesReceipt.TenderAccount09TipAmount,
                            TenderAccount10TenderAmount = salesReceipt.TenderAccount10TenderAmount,
                            TenderAccount10TipAmount = salesReceipt.TenderAccount10TipAmount,
                            TenderCheck01CheckNumber = salesReceipt.TenderCheck01CheckNumber,
                            TenderCheck01TenderAmount = salesReceipt.TenderCheck01TenderAmount,
                            TenderCheck02CheckNumber = salesReceipt.TenderCheck02CheckNumber,
                            TenderCheck02TenderAmount = salesReceipt.TenderCheck02TenderAmount,
                            TenderCheck03CheckNumber = salesReceipt.TenderCheck03CheckNumber,
                            TenderCheck03TenderAmount = salesReceipt.TenderCheck03TenderAmount,
                            TenderCheck04CheckNumber = salesReceipt.TenderCheck04CheckNumber,
                            TenderCheck04TenderAmount = salesReceipt.TenderCheck04TenderAmount,
                            TenderCheck05CheckNumber = salesReceipt.TenderCheck05CheckNumber,
                            TenderCheck05TenderAmount = salesReceipt.TenderCheck05TenderAmount,
                            TenderCheck06CheckNumber = salesReceipt.TenderCheck06CheckNumber,
                            TenderCheck06TenderAmount = salesReceipt.TenderCheck06TenderAmount,
                            TenderCheck07CheckNumber = salesReceipt.TenderCheck07CheckNumber,
                            TenderCheck07TenderAmount = salesReceipt.TenderCheck07TenderAmount,
                            TenderCheck08CheckNumber = salesReceipt.TenderCheck08CheckNumber,
                            TenderCheck08TenderAmount = salesReceipt.TenderCheck08TenderAmount,
                            TenderCheck09CheckNumber = salesReceipt.TenderCheck09CheckNumber,
                            TenderCheck09TenderAmount = salesReceipt.TenderCheck09TenderAmount,
                            TenderCheck10CheckNumber = salesReceipt.TenderCheck10CheckNumber,
                            TenderCheck10TenderAmount = salesReceipt.TenderCheck10TenderAmount,
                            TenderCash01TenderAmount = salesReceipt.TenderCash01TenderAmount,
                            TenderCash02TenderAmount = salesReceipt.TenderCash02TenderAmount,
                            TenderCash03TenderAmount = salesReceipt.TenderCash03TenderAmount,
                            TenderCash04TenderAmount = salesReceipt.TenderCash04TenderAmount,
                            TenderCash05TenderAmount = salesReceipt.TenderCash05TenderAmount,
                            TenderCash06TenderAmount = salesReceipt.TenderCash06TenderAmount,
                            TenderCash07TenderAmount = salesReceipt.TenderCash07TenderAmount,
                            TenderCash08TenderAmount = salesReceipt.TenderCash08TenderAmount,
                            TenderCash09TenderAmount = salesReceipt.TenderCash09TenderAmount,
                            TenderCash10TenderAmount = salesReceipt.TenderCash10TenderAmount,
                            TenderCreditCard01CardName = salesReceipt.TenderCreditCard01CardName,
                            TenderCreditCard01TenderAmount = salesReceipt.TenderCreditCard01TenderAmount,
                            TenderCreditCard01TipAmount = salesReceipt.TenderCreditCard01TipAmount,
                            TenderCreditCard02CardName = salesReceipt.TenderCreditCard02CardName,
                            TenderCreditCard02TenderAmount = salesReceipt.TenderCreditCard02TenderAmount,
                            TenderCreditCard02TipAmount = salesReceipt.TenderCreditCard02TipAmount,
                            TenderCreditCard03CardName = salesReceipt.TenderCreditCard03CardName,
                            TenderCreditCard03TenderAmount = salesReceipt.TenderCreditCard03TenderAmount,
                            TenderCreditCard03TipAmount = salesReceipt.TenderCreditCard03TipAmount,
                            TenderCreditCard04CardName = salesReceipt.TenderCreditCard04CardName,
                            TenderCreditCard04TenderAmount = salesReceipt.TenderCreditCard04TenderAmount,
                            TenderCreditCard04TipAmount = salesReceipt.TenderCreditCard04TipAmount,
                            TenderCreditCard05CardName = salesReceipt.TenderCreditCard05CardName,
                            TenderCreditCard05TenderAmount = salesReceipt.TenderCreditCard05TenderAmount,
                            TenderCreditCard05TipAmount = salesReceipt.TenderCreditCard05TipAmount,
                            TenderDebitCard01Cashback = salesReceipt.TenderDebitCard01Cashback,
                            TenderDebitCard01TenderAmount = salesReceipt.TenderDebitCard01TenderAmount,
                            TenderDebitCard02Cashback = salesReceipt.TenderDebitCard02Cashback,
                            TenderDebitCard02TenderAmount = salesReceipt.TenderDebitCard02TenderAmount,
                            TenderDebitCard03Cashback = salesReceipt.TenderDebitCard03Cashback,
                            TenderDebitCard03TenderAmount = salesReceipt.TenderDebitCard03TenderAmount,
                            TenderDebitCard04Cashback = salesReceipt.TenderDebitCard04Cashback,
                            TenderDebitCard04TenderAmount = salesReceipt.TenderDebitCard04TenderAmount,
                            TenderDebitCard05Cashback = salesReceipt.TenderDebitCard05Cashback,
                            TenderDebitCard05TenderAmount = salesReceipt.TenderDebitCard05TenderAmount,
                            TenderDeposit01TenderAmount = salesReceipt.TenderDeposit01TenderAmount,
                            TenderDeposit02TenderAmount = salesReceipt.TenderDeposit02TenderAmount,
                            TenderDeposit03TenderAmount = salesReceipt.TenderDeposit03TenderAmount,
                            TenderDeposit04TenderAmount = salesReceipt.TenderDeposit04TenderAmount,
                            TenderDeposit05TenderAmount = salesReceipt.TenderDeposit05TenderAmount,
                            TenderGift01TenderAmount = salesReceipt.TenderGift01TenderAmount,
                            TenderGift01GiftCertificateNumber = salesReceipt.TenderGift01GiftCertificateNumber,
                            TenderGift02TenderAmount = salesReceipt.TenderGift02TenderAmount,
                            TenderGift02GiftCertificateNumber = salesReceipt.TenderGift02GiftCertificateNumber,
                            TenderGift03TenderAmount = salesReceipt.TenderGift03TenderAmount,
                            TenderGift03GiftCertificateNumber = salesReceipt.TenderGift03GiftCertificateNumber,
                            TenderGift04TenderAmount = salesReceipt.TenderGift04TenderAmount,
                            TenderGift04GiftCertificateNumber = salesReceipt.TenderGift04GiftCertificateNumber,
                            TenderGift05TenderAmount = salesReceipt.TenderGift05TenderAmount,
                            TenderGift05GiftCertificateNumber = salesReceipt.TenderGift05GiftCertificateNumber,
                            TenderGift06TenderAmount = salesReceipt.TenderGift06TenderAmount,
                            TenderGift06GiftCertificateNumber = salesReceipt.TenderGift06GiftCertificateNumber,
                            TenderGift07TenderAmount = salesReceipt.TenderGift07TenderAmount,
                            TenderGift07GiftCertificateNumber = salesReceipt.TenderGift07GiftCertificateNumber,
                            TenderGift08TenderAmount = salesReceipt.TenderGift08TenderAmount,
                            TenderGift08GiftCertificateNumber = salesReceipt.TenderGift08GiftCertificateNumber,
                            TenderGift09TenderAmount = salesReceipt.TenderGift09TenderAmount,
                            TenderGift10TenderAmount = salesReceipt.TenderGift10TenderAmount,
                            TenderGift10GiftCertificateNumber = salesReceipt.TenderGift10GiftCertificateNumber,
                            TenderGiftCard01TenderAmount = salesReceipt.TenderGiftCard01TenderAmount,
                            TenderGiftCard01TipAmount = salesReceipt.TenderGiftCard01TipAmount,
                            TenderGiftCard02TenderAmount = salesReceipt.TenderGiftCard02TenderAmount,
                            TenderGiftCard02TipAmount = salesReceipt.TenderGiftCard02TipAmount,
                            TenderGiftCard03TenderAmount = salesReceipt.TenderGiftCard03TenderAmount,
                            TenderGiftCard03TipAmount = salesReceipt.TenderGiftCard03TipAmount,
                            TenderGiftCard04TenderAmount = salesReceipt.TenderGiftCard04TenderAmount,
                            TenderGiftCard04TipAmount = salesReceipt.TenderGiftCard04TipAmount,
                            TenderGiftCard05TenderAmount = salesReceipt.TenderGiftCard05TenderAmount,
                            TenderGiftCard05TipAmount = salesReceipt.TenderGiftCard05TipAmount,
                            FQSaveToCache = salesReceipt.FQSaveToCache,
                        };
                        DbContext.SaveChanges();
                    }
                }
                try
                {
                    await DbContext.SaveChangesAsync();
                }
                catch (Exception e) { }

                return posSalesReceiptDemo;
            }
            else
            {
                var posSalesReceiptDemo = await DbContext.POSSalesReceiptTMP.ToListAsync();

                foreach (var salesReceipt in posSalesReceiptDemo)
                {
                    var salesReceiptData = await DbContext.POSSalesReceipt.FirstOrDefaultAsync(o => o.TxnID.Equals(salesReceipt.TxnID));

                    if (salesReceiptData == null)
                    {
                        await DbContext.AddRangeAsync(new POSSalesReceipt
                        {
                            TxnID = salesReceipt.TxnID,
                            TimeCreated = salesReceipt.TimeCreated,
                            TimeModified = salesReceipt.TimeModified,
                            Associate = salesReceipt.Associate,
                            Cashier = salesReceipt.Cashier,
                            Comments = salesReceipt.Comments,
                            CustomerListID = salesReceipt.CustomerListID,
                            Discount = salesReceipt.Discount,
                            DiscountPercent = salesReceipt.DiscountPercent,
                            HistoryDocStatus = salesReceipt.HistoryDocStatus,
                            ItemsCount = salesReceipt.ItemsCount,
                            PriceLevelNumber = salesReceipt.PriceLevelNumber,
                            PromoCode = salesReceipt.PromoCode,
                            QuickBooksFlag = salesReceipt.QuickBooksFlag,
                            SalesOrderTxnID = salesReceipt.SalesOrderTxnID,
                            SalesReceiptNumber = salesReceipt.SalesReceiptNumber,
                            SalesReceiptType = salesReceipt.SalesReceiptType,
                            ShipDate = salesReceipt.ShipDate,
                            StoreExchangeStatus = salesReceipt.StoreExchangeStatus,
                            StoreNumber = salesReceipt.StoreNumber,
                            Subtotal = salesReceipt.Subtotal,
                            TaxAmount = salesReceipt.TaxAmount,
                            TaxCategory = salesReceipt.TaxCategory,
                            TaxPercentage = salesReceipt.TaxPercentage,
                            TenderType = salesReceipt.TenderType,
                            Total = salesReceipt.Total,
                            TrackingNumber = salesReceipt.TrackingNumber,
                            TxnDate = salesReceipt.TxnDate,
                            TxnState = salesReceipt.TxnState,
                            Workstation = salesReceipt.Workstation,
                            BillingInformationCity = salesReceipt.BillingInformationCity,
                            BillingInformationCompanyName = salesReceipt.BillingInformationCompanyName,
                            BillingInformationCountry = salesReceipt.BillingInformationCountry,
                            BillingInformationFirstName = salesReceipt.BillingInformationFirstName,
                            BillingInformationLastName = salesReceipt.BillingInformationLastName,
                            BillingInformationPhone = salesReceipt.BillingInformationPhone,
                            BillingInformationPhone2 = salesReceipt.BillingInformationPhone2,
                            BillingInformationPhone3 = salesReceipt.BillingInformationPhone3,
                            BillingInformationPhone4 = salesReceipt.BillingInformationPhone4,
                            BillingInformationPostalCode = salesReceipt.BillingInformationPostalCode,
                            BillingInformationSalutation = salesReceipt.BillingInformationSalutation,
                            BillingInformationState = salesReceipt.BillingInformationState,
                            BillingInformationStreet = salesReceipt.BillingInformationStreet,
                            ShippingInformationCity = salesReceipt.ShippingInformationCity,
                            ShippingInformationCompanyName = salesReceipt.ShippingInformationCompanyName,
                            ShippingInformationCountry = salesReceipt.ShippingInformationCountry,
                            ShippingInformationFullName = salesReceipt.ShippingInformationFullName,
                            ShippingInformationPhone = salesReceipt.ShippingInformationPhone,
                            ShippingInformationPhone2 = salesReceipt.ShippingInformationPhone2,
                            ShippingInformationPhone3 = salesReceipt.ShippingInformationPhone3,
                            ShippingInformationPhone4 = salesReceipt.ShippingInformationPhone4,
                            ShippingInformationPostalCode = salesReceipt.ShippingInformationPostalCode,
                            ShippingInformationShipBy = salesReceipt.ShippingInformationShipBy,
                            ShippingInformationShipping = salesReceipt.ShippingInformationShipping,
                            ShippingInformationState = salesReceipt.ShippingInformationState,
                            ShippingInformationStreet = salesReceipt.ShippingInformationStreet,
                            TenderAccount01TenderAmount = salesReceipt.TenderAccount01TenderAmount,
                            TenderAccount01TipAmount = salesReceipt.TenderAccount01TipAmount,
                            TenderAccount02TenderAmount = salesReceipt.TenderAccount02TenderAmount,
                            TenderAccount02TipAmount = salesReceipt.TenderAccount02TipAmount,
                            TenderAccount03TenderAmount = salesReceipt.TenderAccount03TenderAmount,
                            TenderAccount03TipAmount = salesReceipt.TenderAccount03TipAmount,
                            TenderAccount04TenderAmount = salesReceipt.TenderAccount04TenderAmount,
                            TenderAccount04TipAmount = salesReceipt.TenderAccount04TipAmount,
                            TenderAccount05TenderAmount = salesReceipt.TenderAccount05TenderAmount,
                            TenderAccount06TenderAmount = salesReceipt.TenderAccount06TenderAmount,
                            TenderAccount06TipAmount = salesReceipt.TenderAccount06TipAmount,
                            TenderAccount07TenderAmount = salesReceipt.TenderAccount07TenderAmount,
                            TenderAccount07TipAmount = salesReceipt.TenderAccount07TipAmount,
                            TenderAccount08TenderAmount = salesReceipt.TenderAccount08TenderAmount,
                            TenderAccount08TipAmount = salesReceipt.TenderAccount08TipAmount,
                            TenderAccount09TenderAmount = salesReceipt.TenderAccount09TenderAmount,
                            TenderAccount09TipAmount = salesReceipt.TenderAccount09TipAmount,
                            TenderAccount10TenderAmount = salesReceipt.TenderAccount10TenderAmount,
                            TenderAccount10TipAmount = salesReceipt.TenderAccount10TipAmount,
                            TenderCheck01CheckNumber = salesReceipt.TenderCheck01CheckNumber,
                            TenderCheck01TenderAmount = salesReceipt.TenderCheck01TenderAmount,
                            TenderCheck02CheckNumber = salesReceipt.TenderCheck02CheckNumber,
                            TenderCheck02TenderAmount = salesReceipt.TenderCheck02TenderAmount,
                            TenderCheck03CheckNumber = salesReceipt.TenderCheck03CheckNumber,
                            TenderCheck03TenderAmount = salesReceipt.TenderCheck03TenderAmount,
                            TenderCheck04CheckNumber = salesReceipt.TenderCheck04CheckNumber,
                            TenderCheck04TenderAmount = salesReceipt.TenderCheck04TenderAmount,
                            TenderCheck05CheckNumber = salesReceipt.TenderCheck05CheckNumber,
                            TenderCheck05TenderAmount = salesReceipt.TenderCheck05TenderAmount,
                            TenderCheck06CheckNumber = salesReceipt.TenderCheck06CheckNumber,
                            TenderCheck06TenderAmount = salesReceipt.TenderCheck06TenderAmount,
                            TenderCheck07CheckNumber = salesReceipt.TenderCheck07CheckNumber,
                            TenderCheck07TenderAmount = salesReceipt.TenderCheck07TenderAmount,
                            TenderCheck08CheckNumber = salesReceipt.TenderCheck08CheckNumber,
                            TenderCheck08TenderAmount = salesReceipt.TenderCheck08TenderAmount,
                            TenderCheck09CheckNumber = salesReceipt.TenderCheck09CheckNumber,
                            TenderCheck09TenderAmount = salesReceipt.TenderCheck09TenderAmount,
                            TenderCheck10CheckNumber = salesReceipt.TenderCheck10CheckNumber,
                            TenderCheck10TenderAmount = salesReceipt.TenderCheck10TenderAmount,
                            TenderCash01TenderAmount = salesReceipt.TenderCash01TenderAmount,
                            TenderCash02TenderAmount = salesReceipt.TenderCash02TenderAmount,
                            TenderCash03TenderAmount = salesReceipt.TenderCash03TenderAmount,
                            TenderCash04TenderAmount = salesReceipt.TenderCash04TenderAmount,
                            TenderCash05TenderAmount = salesReceipt.TenderCash05TenderAmount,
                            TenderCash06TenderAmount = salesReceipt.TenderCash06TenderAmount,
                            TenderCash07TenderAmount = salesReceipt.TenderCash07TenderAmount,
                            TenderCash08TenderAmount = salesReceipt.TenderCash08TenderAmount,
                            TenderCash09TenderAmount = salesReceipt.TenderCash09TenderAmount,
                            TenderCash10TenderAmount = salesReceipt.TenderCash10TenderAmount,
                            TenderCreditCard01CardName = salesReceipt.TenderCreditCard01CardName,
                            TenderCreditCard01TenderAmount = salesReceipt.TenderCreditCard01TenderAmount,
                            TenderCreditCard01TipAmount = salesReceipt.TenderCreditCard01TipAmount,
                            TenderCreditCard02CardName = salesReceipt.TenderCreditCard02CardName,
                            TenderCreditCard02TenderAmount = salesReceipt.TenderCreditCard02TenderAmount,
                            TenderCreditCard02TipAmount = salesReceipt.TenderCreditCard02TipAmount,
                            TenderCreditCard03CardName = salesReceipt.TenderCreditCard03CardName,
                            TenderCreditCard03TenderAmount = salesReceipt.TenderCreditCard03TenderAmount,
                            TenderCreditCard03TipAmount = salesReceipt.TenderCreditCard03TipAmount,
                            TenderCreditCard04CardName = salesReceipt.TenderCreditCard04CardName,
                            TenderCreditCard04TenderAmount = salesReceipt.TenderCreditCard04TenderAmount,
                            TenderCreditCard04TipAmount = salesReceipt.TenderCreditCard04TipAmount,
                            TenderCreditCard05CardName = salesReceipt.TenderCreditCard05CardName,
                            TenderCreditCard05TenderAmount = salesReceipt.TenderCreditCard05TenderAmount,
                            TenderCreditCard05TipAmount = salesReceipt.TenderCreditCard05TipAmount,
                            TenderDebitCard01Cashback = salesReceipt.TenderDebitCard01Cashback,
                            TenderDebitCard01TenderAmount = salesReceipt.TenderDebitCard01TenderAmount,
                            TenderDebitCard02Cashback = salesReceipt.TenderDebitCard02Cashback,
                            TenderDebitCard02TenderAmount = salesReceipt.TenderDebitCard02TenderAmount,
                            TenderDebitCard03Cashback = salesReceipt.TenderDebitCard03Cashback,
                            TenderDebitCard03TenderAmount = salesReceipt.TenderDebitCard03TenderAmount,
                            TenderDebitCard04Cashback = salesReceipt.TenderDebitCard04Cashback,
                            TenderDebitCard04TenderAmount = salesReceipt.TenderDebitCard04TenderAmount,
                            TenderDebitCard05Cashback = salesReceipt.TenderDebitCard05Cashback,
                            TenderDebitCard05TenderAmount = salesReceipt.TenderDebitCard05TenderAmount,
                            TenderDeposit01TenderAmount = salesReceipt.TenderDeposit01TenderAmount,
                            TenderDeposit02TenderAmount = salesReceipt.TenderDeposit02TenderAmount,
                            TenderDeposit03TenderAmount = salesReceipt.TenderDeposit03TenderAmount,
                            TenderDeposit04TenderAmount = salesReceipt.TenderDeposit04TenderAmount,
                            TenderDeposit05TenderAmount = salesReceipt.TenderDeposit05TenderAmount,
                            TenderGift01TenderAmount = salesReceipt.TenderGift01TenderAmount,
                            TenderGift01GiftCertificateNumber = salesReceipt.TenderGift01GiftCertificateNumber,
                            TenderGift02TenderAmount = salesReceipt.TenderGift02TenderAmount,
                            TenderGift02GiftCertificateNumber = salesReceipt.TenderGift02GiftCertificateNumber,
                            TenderGift03TenderAmount = salesReceipt.TenderGift03TenderAmount,
                            TenderGift03GiftCertificateNumber = salesReceipt.TenderGift03GiftCertificateNumber,
                            TenderGift04TenderAmount = salesReceipt.TenderGift04TenderAmount,
                            TenderGift04GiftCertificateNumber = salesReceipt.TenderGift04GiftCertificateNumber,
                            TenderGift05TenderAmount = salesReceipt.TenderGift05TenderAmount,
                            TenderGift05GiftCertificateNumber = salesReceipt.TenderGift05GiftCertificateNumber,
                            TenderGift06TenderAmount = salesReceipt.TenderGift06TenderAmount,
                            TenderGift06GiftCertificateNumber = salesReceipt.TenderGift06GiftCertificateNumber,
                            TenderGift07TenderAmount = salesReceipt.TenderGift07TenderAmount,
                            TenderGift07GiftCertificateNumber = salesReceipt.TenderGift07GiftCertificateNumber,
                            TenderGift08TenderAmount = salesReceipt.TenderGift08TenderAmount,
                            TenderGift08GiftCertificateNumber = salesReceipt.TenderGift08GiftCertificateNumber,
                            TenderGift09TenderAmount = salesReceipt.TenderGift09TenderAmount,
                            TenderGift10TenderAmount = salesReceipt.TenderGift10TenderAmount,
                            TenderGift10GiftCertificateNumber = salesReceipt.TenderGift10GiftCertificateNumber,
                            TenderGiftCard01TenderAmount = salesReceipt.TenderGiftCard01TenderAmount,
                            TenderGiftCard01TipAmount = salesReceipt.TenderGiftCard01TipAmount,
                            TenderGiftCard02TenderAmount = salesReceipt.TenderGiftCard02TenderAmount,
                            TenderGiftCard02TipAmount = salesReceipt.TenderGiftCard02TipAmount,
                            TenderGiftCard03TenderAmount = salesReceipt.TenderGiftCard03TenderAmount,
                            TenderGiftCard03TipAmount = salesReceipt.TenderGiftCard03TipAmount,
                            TenderGiftCard04TenderAmount = salesReceipt.TenderGiftCard04TenderAmount,
                            TenderGiftCard04TipAmount = salesReceipt.TenderGiftCard04TipAmount,
                            TenderGiftCard05TenderAmount = salesReceipt.TenderGiftCard05TenderAmount,
                            TenderGiftCard05TipAmount = salesReceipt.TenderGiftCard05TipAmount,
                            FQSaveToCache = salesReceipt.FQSaveToCache,
                        });
                    }
                    else
                    {
                        salesReceiptData.TimeModified = salesReceipt.TimeModified;
                        salesReceiptData.Associate = salesReceipt.Associate;
                        salesReceiptData.Cashier = salesReceipt.Cashier;
                        salesReceiptData.Comments = salesReceipt.Comments;
                        salesReceiptData.CustomerListID = salesReceipt.CustomerListID;
                        salesReceiptData.Discount = salesReceipt.Discount;
                        salesReceiptData.DiscountPercent = salesReceipt.DiscountPercent;
                        salesReceiptData.HistoryDocStatus = salesReceipt.HistoryDocStatus;
                        salesReceiptData.ItemsCount = salesReceipt.ItemsCount;
                        salesReceiptData.PriceLevelNumber = salesReceipt.PriceLevelNumber;
                        salesReceiptData.PromoCode = salesReceipt.PromoCode;
                        salesReceiptData.QuickBooksFlag = salesReceipt.QuickBooksFlag;
                        salesReceiptData.SalesOrderTxnID = salesReceipt.SalesOrderTxnID;
                        salesReceiptData.SalesReceiptNumber = salesReceipt.SalesReceiptNumber;
                        salesReceiptData.SalesReceiptType = salesReceipt.SalesReceiptType;
                        salesReceiptData.ShipDate = salesReceipt.ShipDate;
                        salesReceiptData.StoreExchangeStatus = salesReceipt.StoreExchangeStatus;
                        salesReceiptData.StoreNumber = salesReceipt.StoreNumber;
                        salesReceiptData.Subtotal = salesReceipt.Subtotal;
                        salesReceiptData.TaxAmount = salesReceipt.TaxAmount;
                        salesReceiptData.TaxCategory = salesReceipt.TaxCategory;
                        salesReceiptData.TaxPercentage = salesReceipt.TaxPercentage;
                        salesReceiptData.TenderType = salesReceipt.TenderType;
                        salesReceiptData.Total = salesReceipt.Total;
                        salesReceiptData.TrackingNumber = salesReceipt.TrackingNumber;
                        salesReceiptData.TxnDate = salesReceipt.TxnDate;
                        salesReceiptData.TxnState = salesReceipt.TxnState;
                        salesReceiptData.Workstation = salesReceipt.Workstation;
                        salesReceiptData.BillingInformationCity = salesReceipt.BillingInformationCity;
                        salesReceiptData.BillingInformationCompanyName = salesReceipt.BillingInformationCompanyName;
                        salesReceiptData.BillingInformationCountry = salesReceipt.BillingInformationCountry;
                        salesReceiptData.BillingInformationFirstName = salesReceipt.BillingInformationFirstName;
                        salesReceiptData.BillingInformationLastName = salesReceipt.BillingInformationLastName;
                        salesReceiptData.BillingInformationPhone = salesReceipt.BillingInformationPhone;
                        salesReceiptData.BillingInformationPhone2 = salesReceipt.BillingInformationPhone2;
                        salesReceiptData.BillingInformationPhone3 = salesReceipt.BillingInformationPhone3;
                        salesReceiptData.BillingInformationPhone4 = salesReceipt.BillingInformationPhone4;
                        salesReceiptData.BillingInformationPostalCode = salesReceipt.BillingInformationPostalCode;
                        salesReceiptData.BillingInformationSalutation = salesReceipt.BillingInformationSalutation;
                        salesReceiptData.BillingInformationState = salesReceipt.BillingInformationState;
                        salesReceiptData.BillingInformationStreet = salesReceipt.BillingInformationStreet;
                        salesReceiptData.ShippingInformationCity = salesReceipt.ShippingInformationCity;
                        salesReceiptData.ShippingInformationCompanyName = salesReceipt.ShippingInformationCompanyName;
                        salesReceiptData.ShippingInformationCountry = salesReceipt.ShippingInformationCountry;
                        salesReceiptData.ShippingInformationFullName = salesReceipt.ShippingInformationFullName;
                        salesReceiptData.ShippingInformationPhone = salesReceipt.ShippingInformationPhone;
                        salesReceiptData.ShippingInformationPhone2 = salesReceipt.ShippingInformationPhone2;
                        salesReceiptData.ShippingInformationPhone3 = salesReceipt.ShippingInformationPhone3;
                        salesReceiptData.ShippingInformationPhone4 = salesReceipt.ShippingInformationPhone4;
                        salesReceiptData.ShippingInformationPostalCode = salesReceipt.ShippingInformationPostalCode;
                        salesReceiptData.ShippingInformationShipBy = salesReceipt.ShippingInformationShipBy;
                        salesReceiptData.ShippingInformationShipping = salesReceipt.ShippingInformationShipping;
                        salesReceiptData.ShippingInformationState = salesReceipt.ShippingInformationState;
                        salesReceiptData.ShippingInformationStreet = salesReceipt.ShippingInformationStreet;
                        salesReceiptData.TenderAccount01TenderAmount = salesReceipt.TenderAccount01TenderAmount;
                        salesReceiptData.TenderAccount01TipAmount = salesReceipt.TenderAccount01TipAmount;
                        salesReceiptData.TenderAccount02TenderAmount = salesReceipt.TenderAccount02TenderAmount;
                        salesReceiptData.TenderAccount02TipAmount = salesReceipt.TenderAccount02TipAmount;
                        salesReceiptData.TenderAccount03TenderAmount = salesReceipt.TenderAccount03TenderAmount;
                        salesReceiptData.TenderAccount03TipAmount = salesReceipt.TenderAccount03TipAmount;
                        salesReceiptData.TenderAccount04TenderAmount = salesReceipt.TenderAccount04TenderAmount;
                        salesReceiptData.TenderAccount04TipAmount = salesReceipt.TenderAccount04TipAmount;
                        salesReceiptData.TenderAccount05TenderAmount = salesReceipt.TenderAccount05TenderAmount;
                        salesReceiptData.TenderAccount06TenderAmount = salesReceipt.TenderAccount06TenderAmount;
                        salesReceiptData.TenderAccount06TipAmount = salesReceipt.TenderAccount06TipAmount;
                        salesReceiptData.TenderAccount07TenderAmount = salesReceipt.TenderAccount07TenderAmount;
                        salesReceiptData.TenderAccount07TipAmount = salesReceipt.TenderAccount07TipAmount;
                        salesReceiptData.TenderAccount08TenderAmount = salesReceipt.TenderAccount08TenderAmount;
                        salesReceiptData.TenderAccount08TipAmount = salesReceipt.TenderAccount08TipAmount;
                        salesReceiptData.TenderAccount09TenderAmount = salesReceipt.TenderAccount09TenderAmount;
                        salesReceiptData.TenderAccount09TipAmount = salesReceipt.TenderAccount09TipAmount;
                        salesReceiptData.TenderAccount10TenderAmount = salesReceipt.TenderAccount10TenderAmount;
                        salesReceiptData.TenderAccount10TipAmount = salesReceipt.TenderAccount10TipAmount;
                        salesReceiptData.TenderCheck01CheckNumber = salesReceipt.TenderCheck01CheckNumber;
                        salesReceiptData.TenderCheck01TenderAmount = salesReceipt.TenderCheck01TenderAmount;
                        salesReceiptData.TenderCheck02CheckNumber = salesReceipt.TenderCheck02CheckNumber;
                        salesReceiptData.TenderCheck02TenderAmount = salesReceipt.TenderCheck02TenderAmount;
                        salesReceiptData.TenderCheck03CheckNumber = salesReceipt.TenderCheck03CheckNumber;
                        salesReceiptData.TenderCheck03TenderAmount = salesReceipt.TenderCheck03TenderAmount;
                        salesReceiptData.TenderCheck04CheckNumber = salesReceipt.TenderCheck04CheckNumber;
                        salesReceiptData.TenderCheck04TenderAmount = salesReceipt.TenderCheck04TenderAmount;
                        salesReceiptData.TenderCheck05CheckNumber = salesReceipt.TenderCheck05CheckNumber;
                        salesReceiptData.TenderCheck05TenderAmount = salesReceipt.TenderCheck05TenderAmount;
                        salesReceiptData.TenderCheck06CheckNumber = salesReceipt.TenderCheck06CheckNumber;
                        salesReceiptData.TenderCheck06TenderAmount = salesReceipt.TenderCheck06TenderAmount;
                        salesReceiptData.TenderCheck07CheckNumber = salesReceipt.TenderCheck07CheckNumber;
                        salesReceiptData.TenderCheck07TenderAmount = salesReceipt.TenderCheck07TenderAmount;
                        salesReceiptData.TenderCheck08CheckNumber = salesReceipt.TenderCheck08CheckNumber;
                        salesReceiptData.TenderCheck08TenderAmount = salesReceipt.TenderCheck08TenderAmount;
                        salesReceiptData.TenderCheck09CheckNumber = salesReceipt.TenderCheck09CheckNumber;
                        salesReceiptData.TenderCheck09TenderAmount = salesReceipt.TenderCheck09TenderAmount;
                        salesReceiptData.TenderCheck10CheckNumber = salesReceipt.TenderCheck10CheckNumber;
                        salesReceiptData.TenderCheck10TenderAmount = salesReceipt.TenderCheck10TenderAmount;
                        salesReceiptData.TenderCash01TenderAmount = salesReceipt.TenderCash01TenderAmount;
                        salesReceiptData.TenderCash02TenderAmount = salesReceipt.TenderCash02TenderAmount;
                        salesReceiptData.TenderCash03TenderAmount = salesReceipt.TenderCash03TenderAmount;
                        salesReceiptData.TenderCash04TenderAmount = salesReceipt.TenderCash04TenderAmount;
                        salesReceiptData.TenderCash05TenderAmount = salesReceipt.TenderCash05TenderAmount;
                        salesReceiptData.TenderCash06TenderAmount = salesReceipt.TenderCash06TenderAmount;
                        salesReceiptData.TenderCash07TenderAmount = salesReceipt.TenderCash07TenderAmount;
                        salesReceiptData.TenderCash08TenderAmount = salesReceipt.TenderCash08TenderAmount;
                        salesReceiptData.TenderCash09TenderAmount = salesReceipt.TenderCash09TenderAmount;
                        salesReceiptData.TenderCash10TenderAmount = salesReceipt.TenderCash10TenderAmount;
                        salesReceiptData.TenderCreditCard01CardName = salesReceipt.TenderCreditCard01CardName;
                        salesReceiptData.TenderCreditCard01TenderAmount = salesReceipt.TenderCreditCard01TenderAmount;
                        salesReceiptData.TenderCreditCard01TipAmount = salesReceipt.TenderCreditCard01TipAmount;
                        salesReceiptData.TenderCreditCard02CardName = salesReceipt.TenderCreditCard02CardName;
                        salesReceiptData.TenderCreditCard02TenderAmount = salesReceipt.TenderCreditCard02TenderAmount;
                        salesReceiptData.TenderCreditCard02TipAmount = salesReceipt.TenderCreditCard02TipAmount;
                        salesReceiptData.TenderCreditCard03CardName = salesReceipt.TenderCreditCard03CardName;
                        salesReceiptData.TenderCreditCard03TenderAmount = salesReceipt.TenderCreditCard03TenderAmount;
                        salesReceiptData.TenderCreditCard03TipAmount = salesReceipt.TenderCreditCard03TipAmount;
                        salesReceiptData.TenderCreditCard04CardName = salesReceipt.TenderCreditCard04CardName;
                        salesReceiptData.TenderCreditCard04TenderAmount = salesReceipt.TenderCreditCard04TenderAmount;
                        salesReceiptData.TenderCreditCard04TipAmount = salesReceipt.TenderCreditCard04TipAmount;
                        salesReceiptData.TenderCreditCard05CardName = salesReceipt.TenderCreditCard05CardName;
                        salesReceiptData.TenderCreditCard05TenderAmount = salesReceipt.TenderCreditCard05TenderAmount;
                        salesReceiptData.TenderCreditCard05TipAmount = salesReceipt.TenderCreditCard05TipAmount;
                        salesReceiptData.TenderDebitCard01Cashback = salesReceipt.TenderDebitCard01Cashback;
                        salesReceiptData.TenderDebitCard01TenderAmount = salesReceipt.TenderDebitCard01TenderAmount;
                        salesReceiptData.TenderDebitCard02Cashback = salesReceipt.TenderDebitCard02Cashback;
                        salesReceiptData.TenderDebitCard02TenderAmount = salesReceipt.TenderDebitCard02TenderAmount;
                        salesReceiptData.TenderDebitCard03Cashback = salesReceipt.TenderDebitCard03Cashback;
                        salesReceiptData.TenderDebitCard03TenderAmount = salesReceipt.TenderDebitCard03TenderAmount;
                        salesReceiptData.TenderDebitCard04Cashback = salesReceipt.TenderDebitCard04Cashback;
                        salesReceiptData.TenderDebitCard04TenderAmount = salesReceipt.TenderDebitCard04TenderAmount;
                        salesReceiptData.TenderDebitCard05Cashback = salesReceipt.TenderDebitCard05Cashback;
                        salesReceiptData.TenderDebitCard05TenderAmount = salesReceipt.TenderDebitCard05TenderAmount;
                        salesReceiptData.TenderDeposit01TenderAmount = salesReceipt.TenderDeposit01TenderAmount;
                        salesReceiptData.TenderDeposit02TenderAmount = salesReceipt.TenderDeposit02TenderAmount;
                        salesReceiptData.TenderDeposit03TenderAmount = salesReceipt.TenderDeposit03TenderAmount;
                        salesReceiptData.TenderDeposit04TenderAmount = salesReceipt.TenderDeposit04TenderAmount;
                        salesReceiptData.TenderDeposit05TenderAmount = salesReceipt.TenderDeposit05TenderAmount;
                        salesReceiptData.TenderGift01TenderAmount = salesReceipt.TenderGift01TenderAmount;
                        salesReceiptData.TenderGift01GiftCertificateNumber = salesReceipt.TenderGift01GiftCertificateNumber;
                        salesReceiptData.TenderGift02TenderAmount = salesReceipt.TenderGift02TenderAmount;
                        salesReceiptData.TenderGift02GiftCertificateNumber = salesReceipt.TenderGift02GiftCertificateNumber;
                        salesReceiptData.TenderGift03TenderAmount = salesReceipt.TenderGift03TenderAmount;
                        salesReceiptData.TenderGift03GiftCertificateNumber = salesReceipt.TenderGift03GiftCertificateNumber;
                        salesReceiptData.TenderGift04TenderAmount = salesReceipt.TenderGift04TenderAmount;
                        salesReceiptData.TenderGift04GiftCertificateNumber = salesReceipt.TenderGift04GiftCertificateNumber;
                        salesReceiptData.TenderGift05TenderAmount = salesReceipt.TenderGift05TenderAmount;
                        salesReceiptData.TenderGift05GiftCertificateNumber = salesReceipt.TenderGift05GiftCertificateNumber;
                        salesReceiptData.TenderGift06TenderAmount = salesReceipt.TenderGift06TenderAmount;
                        salesReceiptData.TenderGift06GiftCertificateNumber = salesReceipt.TenderGift06GiftCertificateNumber;
                        salesReceiptData.TenderGift07TenderAmount = salesReceipt.TenderGift07TenderAmount;
                        salesReceiptData.TenderGift07GiftCertificateNumber = salesReceipt.TenderGift07GiftCertificateNumber;
                        salesReceiptData.TenderGift08TenderAmount = salesReceipt.TenderGift08TenderAmount;
                        salesReceiptData.TenderGift08GiftCertificateNumber = salesReceipt.TenderGift08GiftCertificateNumber;
                        salesReceiptData.TenderGift09TenderAmount = salesReceipt.TenderGift09TenderAmount;
                        salesReceiptData.TenderGift10TenderAmount = salesReceipt.TenderGift10TenderAmount;
                        salesReceiptData.TenderGift10GiftCertificateNumber = salesReceipt.TenderGift10GiftCertificateNumber;
                        salesReceiptData.TenderGiftCard01TenderAmount = salesReceipt.TenderGiftCard01TenderAmount;
                        salesReceiptData.TenderGiftCard01TipAmount = salesReceipt.TenderGiftCard01TipAmount;
                        salesReceiptData.TenderGiftCard02TenderAmount = salesReceipt.TenderGiftCard02TenderAmount;
                        salesReceiptData.TenderGiftCard02TipAmount = salesReceipt.TenderGiftCard02TipAmount;
                        salesReceiptData.TenderGiftCard03TenderAmount = salesReceipt.TenderGiftCard03TenderAmount;
                        salesReceiptData.TenderGiftCard03TipAmount = salesReceipt.TenderGiftCard03TipAmount;
                        salesReceiptData.TenderGiftCard04TenderAmount = salesReceipt.TenderGiftCard04TenderAmount;
                        salesReceiptData.TenderGiftCard04TipAmount = salesReceipt.TenderGiftCard04TipAmount;
                        salesReceiptData.TenderGiftCard05TenderAmount = salesReceipt.TenderGiftCard05TenderAmount;
                        salesReceiptData.TenderGiftCard05TipAmount = salesReceipt.TenderGiftCard05TipAmount;
                        salesReceiptData.FQSaveToCache = salesReceipt.FQSaveToCache;

                        var response = new POSSalesReceiptGetByIdResponse
                        {
                            TxnID = salesReceipt.TxnID,
                            TimeCreated = salesReceipt.TimeCreated,
                            TimeModified = salesReceipt.TimeModified,
                            Associate = salesReceipt.Associate,
                            Cashier = salesReceipt.Cashier,
                            Comments = salesReceipt.Comments,
                            CustomerListID = salesReceipt.CustomerListID,
                            Discount = salesReceipt.Discount,
                            DiscountPercent = salesReceipt.DiscountPercent,
                            HistoryDocStatus = salesReceipt.HistoryDocStatus,
                            ItemsCount = salesReceipt.ItemsCount,
                            PriceLevelNumber = salesReceipt.PriceLevelNumber,
                            PromoCode = salesReceipt.PromoCode,
                            QuickBooksFlag = salesReceipt.QuickBooksFlag,
                            SalesOrderTxnID = salesReceipt.SalesOrderTxnID,
                            SalesReceiptNumber = salesReceipt.SalesReceiptNumber,
                            SalesReceiptType = salesReceipt.SalesReceiptType,
                            ShipDate = salesReceipt.ShipDate,
                            StoreExchangeStatus = salesReceipt.StoreExchangeStatus,
                            StoreNumber = salesReceipt.StoreNumber,
                            Subtotal = salesReceipt.Subtotal,
                            TaxAmount = salesReceipt.TaxAmount,
                            TaxCategory = salesReceipt.TaxCategory,
                            TaxPercentage = salesReceipt.TaxPercentage,
                            TenderType = salesReceipt.TenderType,
                            Total = salesReceipt.Total,
                            TrackingNumber = salesReceipt.TrackingNumber,
                            TxnDate = salesReceipt.TxnDate,
                            TxnState = salesReceipt.TxnState,
                            Workstation = salesReceipt.Workstation,
                            BillingInformationCity = salesReceipt.BillingInformationCity,
                            BillingInformationCompanyName = salesReceipt.BillingInformationCompanyName,
                            BillingInformationCountry = salesReceipt.BillingInformationCountry,
                            BillingInformationFirstName = salesReceipt.BillingInformationFirstName,
                            BillingInformationLastName = salesReceipt.BillingInformationLastName,
                            BillingInformationPhone = salesReceipt.BillingInformationPhone,
                            BillingInformationPhone2 = salesReceipt.BillingInformationPhone2,
                            BillingInformationPhone3 = salesReceipt.BillingInformationPhone3,
                            BillingInformationPhone4 = salesReceipt.BillingInformationPhone4,
                            BillingInformationPostalCode = salesReceipt.BillingInformationPostalCode,
                            BillingInformationSalutation = salesReceipt.BillingInformationSalutation,
                            BillingInformationState = salesReceipt.BillingInformationState,
                            BillingInformationStreet = salesReceipt.BillingInformationStreet,
                            ShippingInformationCity = salesReceipt.ShippingInformationCity,
                            ShippingInformationCompanyName = salesReceipt.ShippingInformationCompanyName,
                            ShippingInformationCountry = salesReceipt.ShippingInformationCountry,
                            ShippingInformationFullName = salesReceipt.ShippingInformationFullName,
                            ShippingInformationPhone = salesReceipt.ShippingInformationPhone,
                            ShippingInformationPhone2 = salesReceipt.ShippingInformationPhone2,
                            ShippingInformationPhone3 = salesReceipt.ShippingInformationPhone3,
                            ShippingInformationPhone4 = salesReceipt.ShippingInformationPhone4,
                            ShippingInformationPostalCode = salesReceipt.ShippingInformationPostalCode,
                            ShippingInformationShipBy = salesReceipt.ShippingInformationShipBy,
                            ShippingInformationShipping = salesReceipt.ShippingInformationShipping,
                            ShippingInformationState = salesReceipt.ShippingInformationState,
                            ShippingInformationStreet = salesReceipt.ShippingInformationStreet,
                            TenderAccount01TenderAmount = salesReceipt.TenderAccount01TenderAmount,
                            TenderAccount01TipAmount = salesReceipt.TenderAccount01TipAmount,
                            TenderAccount02TenderAmount = salesReceipt.TenderAccount02TenderAmount,
                            TenderAccount02TipAmount = salesReceipt.TenderAccount02TipAmount,
                            TenderAccount03TenderAmount = salesReceipt.TenderAccount03TenderAmount,
                            TenderAccount03TipAmount = salesReceipt.TenderAccount03TipAmount,
                            TenderAccount04TenderAmount = salesReceipt.TenderAccount04TenderAmount,
                            TenderAccount04TipAmount = salesReceipt.TenderAccount04TipAmount,
                            TenderAccount05TenderAmount = salesReceipt.TenderAccount05TenderAmount,
                            TenderAccount06TenderAmount = salesReceipt.TenderAccount06TenderAmount,
                            TenderAccount06TipAmount = salesReceipt.TenderAccount06TipAmount,
                            TenderAccount07TenderAmount = salesReceipt.TenderAccount07TenderAmount,
                            TenderAccount07TipAmount = salesReceipt.TenderAccount07TipAmount,
                            TenderAccount08TenderAmount = salesReceipt.TenderAccount08TenderAmount,
                            TenderAccount08TipAmount = salesReceipt.TenderAccount08TipAmount,
                            TenderAccount09TenderAmount = salesReceipt.TenderAccount09TenderAmount,
                            TenderAccount09TipAmount = salesReceipt.TenderAccount09TipAmount,
                            TenderAccount10TenderAmount = salesReceipt.TenderAccount10TenderAmount,
                            TenderAccount10TipAmount = salesReceipt.TenderAccount10TipAmount,
                            TenderCheck01CheckNumber = salesReceipt.TenderCheck01CheckNumber,
                            TenderCheck01TenderAmount = salesReceipt.TenderCheck01TenderAmount,
                            TenderCheck02CheckNumber = salesReceipt.TenderCheck02CheckNumber,
                            TenderCheck02TenderAmount = salesReceipt.TenderCheck02TenderAmount,
                            TenderCheck03CheckNumber = salesReceipt.TenderCheck03CheckNumber,
                            TenderCheck03TenderAmount = salesReceipt.TenderCheck03TenderAmount,
                            TenderCheck04CheckNumber = salesReceipt.TenderCheck04CheckNumber,
                            TenderCheck04TenderAmount = salesReceipt.TenderCheck04TenderAmount,
                            TenderCheck05CheckNumber = salesReceipt.TenderCheck05CheckNumber,
                            TenderCheck05TenderAmount = salesReceipt.TenderCheck05TenderAmount,
                            TenderCheck06CheckNumber = salesReceipt.TenderCheck06CheckNumber,
                            TenderCheck06TenderAmount = salesReceipt.TenderCheck06TenderAmount,
                            TenderCheck07CheckNumber = salesReceipt.TenderCheck07CheckNumber,
                            TenderCheck07TenderAmount = salesReceipt.TenderCheck07TenderAmount,
                            TenderCheck08CheckNumber = salesReceipt.TenderCheck08CheckNumber,
                            TenderCheck08TenderAmount = salesReceipt.TenderCheck08TenderAmount,
                            TenderCheck09CheckNumber = salesReceipt.TenderCheck09CheckNumber,
                            TenderCheck09TenderAmount = salesReceipt.TenderCheck09TenderAmount,
                            TenderCheck10CheckNumber = salesReceipt.TenderCheck10CheckNumber,
                            TenderCheck10TenderAmount = salesReceipt.TenderCheck10TenderAmount,
                            TenderCash01TenderAmount = salesReceipt.TenderCash01TenderAmount,
                            TenderCash02TenderAmount = salesReceipt.TenderCash02TenderAmount,
                            TenderCash03TenderAmount = salesReceipt.TenderCash03TenderAmount,
                            TenderCash04TenderAmount = salesReceipt.TenderCash04TenderAmount,
                            TenderCash05TenderAmount = salesReceipt.TenderCash05TenderAmount,
                            TenderCash06TenderAmount = salesReceipt.TenderCash06TenderAmount,
                            TenderCash07TenderAmount = salesReceipt.TenderCash07TenderAmount,
                            TenderCash08TenderAmount = salesReceipt.TenderCash08TenderAmount,
                            TenderCash09TenderAmount = salesReceipt.TenderCash09TenderAmount,
                            TenderCash10TenderAmount = salesReceipt.TenderCash10TenderAmount,
                            TenderCreditCard01CardName = salesReceipt.TenderCreditCard01CardName,
                            TenderCreditCard01TenderAmount = salesReceipt.TenderCreditCard01TenderAmount,
                            TenderCreditCard01TipAmount = salesReceipt.TenderCreditCard01TipAmount,
                            TenderCreditCard02CardName = salesReceipt.TenderCreditCard02CardName,
                            TenderCreditCard02TenderAmount = salesReceipt.TenderCreditCard02TenderAmount,
                            TenderCreditCard02TipAmount = salesReceipt.TenderCreditCard02TipAmount,
                            TenderCreditCard03CardName = salesReceipt.TenderCreditCard03CardName,
                            TenderCreditCard03TenderAmount = salesReceipt.TenderCreditCard03TenderAmount,
                            TenderCreditCard03TipAmount = salesReceipt.TenderCreditCard03TipAmount,
                            TenderCreditCard04CardName = salesReceipt.TenderCreditCard04CardName,
                            TenderCreditCard04TenderAmount = salesReceipt.TenderCreditCard04TenderAmount,
                            TenderCreditCard04TipAmount = salesReceipt.TenderCreditCard04TipAmount,
                            TenderCreditCard05CardName = salesReceipt.TenderCreditCard05CardName,
                            TenderCreditCard05TenderAmount = salesReceipt.TenderCreditCard05TenderAmount,
                            TenderCreditCard05TipAmount = salesReceipt.TenderCreditCard05TipAmount,
                            TenderDebitCard01Cashback = salesReceipt.TenderDebitCard01Cashback,
                            TenderDebitCard01TenderAmount = salesReceipt.TenderDebitCard01TenderAmount,
                            TenderDebitCard02Cashback = salesReceipt.TenderDebitCard02Cashback,
                            TenderDebitCard02TenderAmount = salesReceipt.TenderDebitCard02TenderAmount,
                            TenderDebitCard03Cashback = salesReceipt.TenderDebitCard03Cashback,
                            TenderDebitCard03TenderAmount = salesReceipt.TenderDebitCard03TenderAmount,
                            TenderDebitCard04Cashback = salesReceipt.TenderDebitCard04Cashback,
                            TenderDebitCard04TenderAmount = salesReceipt.TenderDebitCard04TenderAmount,
                            TenderDebitCard05Cashback = salesReceipt.TenderDebitCard05Cashback,
                            TenderDebitCard05TenderAmount = salesReceipt.TenderDebitCard05TenderAmount,
                            TenderDeposit01TenderAmount = salesReceipt.TenderDeposit01TenderAmount,
                            TenderDeposit02TenderAmount = salesReceipt.TenderDeposit02TenderAmount,
                            TenderDeposit03TenderAmount = salesReceipt.TenderDeposit03TenderAmount,
                            TenderDeposit04TenderAmount = salesReceipt.TenderDeposit04TenderAmount,
                            TenderDeposit05TenderAmount = salesReceipt.TenderDeposit05TenderAmount,
                            TenderGift01TenderAmount = salesReceipt.TenderGift01TenderAmount,
                            TenderGift01GiftCertificateNumber = salesReceipt.TenderGift01GiftCertificateNumber,
                            TenderGift02TenderAmount = salesReceipt.TenderGift02TenderAmount,
                            TenderGift02GiftCertificateNumber = salesReceipt.TenderGift02GiftCertificateNumber,
                            TenderGift03TenderAmount = salesReceipt.TenderGift03TenderAmount,
                            TenderGift03GiftCertificateNumber = salesReceipt.TenderGift03GiftCertificateNumber,
                            TenderGift04TenderAmount = salesReceipt.TenderGift04TenderAmount,
                            TenderGift04GiftCertificateNumber = salesReceipt.TenderGift04GiftCertificateNumber,
                            TenderGift05TenderAmount = salesReceipt.TenderGift05TenderAmount,
                            TenderGift05GiftCertificateNumber = salesReceipt.TenderGift05GiftCertificateNumber,
                            TenderGift06TenderAmount = salesReceipt.TenderGift06TenderAmount,
                            TenderGift06GiftCertificateNumber = salesReceipt.TenderGift06GiftCertificateNumber,
                            TenderGift07TenderAmount = salesReceipt.TenderGift07TenderAmount,
                            TenderGift07GiftCertificateNumber = salesReceipt.TenderGift07GiftCertificateNumber,
                            TenderGift08TenderAmount = salesReceipt.TenderGift08TenderAmount,
                            TenderGift08GiftCertificateNumber = salesReceipt.TenderGift08GiftCertificateNumber,
                            TenderGift09TenderAmount = salesReceipt.TenderGift09TenderAmount,
                            TenderGift10TenderAmount = salesReceipt.TenderGift10TenderAmount,
                            TenderGift10GiftCertificateNumber = salesReceipt.TenderGift10GiftCertificateNumber,
                            TenderGiftCard01TenderAmount = salesReceipt.TenderGiftCard01TenderAmount,
                            TenderGiftCard01TipAmount = salesReceipt.TenderGiftCard01TipAmount,
                            TenderGiftCard02TenderAmount = salesReceipt.TenderGiftCard02TenderAmount,
                            TenderGiftCard02TipAmount = salesReceipt.TenderGiftCard02TipAmount,
                            TenderGiftCard03TenderAmount = salesReceipt.TenderGiftCard03TenderAmount,
                            TenderGiftCard03TipAmount = salesReceipt.TenderGiftCard03TipAmount,
                            TenderGiftCard04TenderAmount = salesReceipt.TenderGiftCard04TenderAmount,
                            TenderGiftCard04TipAmount = salesReceipt.TenderGiftCard04TipAmount,
                            TenderGiftCard05TenderAmount = salesReceipt.TenderGiftCard05TenderAmount,
                            TenderGiftCard05TipAmount = salesReceipt.TenderGiftCard05TipAmount,
                            FQSaveToCache = salesReceipt.FQSaveToCache,
                        };
                        DbContext.SaveChanges();
                    }
                }
                try
                {
                    await DbContext.SaveChangesAsync();
                }
                catch (Exception e) { }

                return posSalesReceiptDemo;
            }


        }
        #endregion

        #region 2.Gives a reward based on sales receipt

        public async Task POSSalesReceiptCustomerRewards()
        {
            var topSalesTimeCreated = await DbContext.LastSalesDate.OrderByDescending(o => o.TimeCreated).Select(s => s.TimeCreated).FirstOrDefaultAsync();

            var customer = await (from S in DbContext.POSSalesReceipt
                                  join C in DbContext.POSCustomer
                                  on S.CustomerListID equals C.ListId

                                  select new POSSalesReceiptCustomerRewardsResponse
                                  {
                                      TxnID = S.TxnID,
                                      CustomerListId = S.CustomerListID,
                                      TimeCreated = S.TimeCreated,
                                      TimeModified = S.TimeModified,
                                      ListId = C.ListId,
                                      Email = C.EMail,
                                      FirstName = C.FirstName,
                                      FullName = C.FullName,
                                      Phone = C.Phone,
                                  }
                                 ).Where(S => S.TimeCreated > topSalesTimeCreated.Value).OrderByDescending(S => S.TimeCreated).ToListAsync();

            if (customer.Count > 0)
            {
                await DbContext.AddRangeAsync(new LastSalesDate
                {
                    TimeCreated = customer[0].TimeCreated
                });
            }

            foreach (var item in customer)
            {
                if (item.Email == null || item.Phone == null)
                    continue;

                var user = DbContext.User.FirstOrDefault(x => x.EmailId.ToLower() == item.Email.ToLower() && x.MobileNumber == item.Phone);

                await DbContext.AddRangeAsync(new PointsEntity
                {
                    AvailablePoints = 1,
                    UsedPoints = 0,
                    PointName = "Point added for Purchase",
                    UserId = user.Id,
                });

                user.Points = user.Points + 1;

                await DbContext.SaveChangesAsync();
            }

        }

        #endregion

        #region Dispose

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
