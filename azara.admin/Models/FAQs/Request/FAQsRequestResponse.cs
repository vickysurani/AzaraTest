namespace azara.admin.Models.FAQs.Request;

public class FAQsRequestResponse
{
    public string? Id { get; set; }

    [Required(ErrorMessageResourceName = "error_question_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Question { get; set; }

    [Required(ErrorMessageResourceName = "error_answer_required", ErrorMessageResourceType = typeof(ErrorMessage))]
    public string Answer { get; set; }
}