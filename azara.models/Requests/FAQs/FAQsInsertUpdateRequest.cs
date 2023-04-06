namespace azara.models.Requests.FAQs;

public class FAQsInsertUpdateRequest
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "error_question_required")]
    public string Question { get; set; }

    [Required(ErrorMessage = "error_answer_required")]
    public string Answer { get; set; }
}