namespace azara.models.Requests.QuickBooks;

public class POSCustomerInsertRequest
{
    [Required(ErrorMessage = "error_first_name_required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "error_last_name_required")]
    public string LastName { get; set; }

    public string EMail { get; set; }

    public string Phone { get; set; }

    public string Phone2 { get; set; }

    public string Phone3 { get; set; }

    public string Phone4 { get; set; }
}