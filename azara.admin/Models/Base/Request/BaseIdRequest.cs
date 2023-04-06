namespace azara.admin.Models.Base.Request;

public class BaseIdRequest
{
    public Guid? Id { get; set; } = Guid.NewGuid();
    public int? IntID { get; set; } = 0; 
}