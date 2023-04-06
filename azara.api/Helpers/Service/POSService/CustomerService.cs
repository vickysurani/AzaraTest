using azara.models.Requests.QuickBooks;
using System.Data;

namespace azara.api.Helpers.Service.POSService
{
    public class CustomerService : IService
    {
        public void InsertCustomer(AzaraContext Context, POSCustomerInsertRequest request)
        {
            try
            {
                SqlConnection con = (SqlConnection)(Context.Database.GetDbConnection());
                SqlCommand cmd = new SqlCommand("[dbo].[Pos_Customer_Insert]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = request.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = request.LastName;
                cmd.Parameters.Add("@EMail", SqlDbType.VarChar).Value = request.EMail;
                cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = request.Phone;
                cmd.Parameters.Add("@Phone2", SqlDbType.VarChar).Value = request.Phone2;
                cmd.Parameters.Add("@Phone3", SqlDbType.VarChar).Value = request.Phone3;
                cmd.Parameters.Add("@Phone4", SqlDbType.VarChar).Value = request.Phone4;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex) { }
        }

    }
}
