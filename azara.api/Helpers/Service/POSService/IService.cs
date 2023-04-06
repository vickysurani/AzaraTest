using azara.models.Requests.QuickBooks;

namespace azara.api.Helpers.Service.POSService
{
    public interface IService
    {
        void InsertCustomer(AzaraContext Context, POSCustomerInsertRequest request);
    }
}
