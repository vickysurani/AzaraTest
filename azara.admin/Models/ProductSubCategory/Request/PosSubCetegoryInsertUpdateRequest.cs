namespace azara.admin.Models.ProductSubCategory.Request
{
    public class PosSubCetegoryInsertUpdateRequest
    {
        public int? ProductSubCategoryId { get; set; }

        [Required(ErrorMessageResourceName = "error_product_category_name_required", ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Name { get; set; }

        public string? Image { get; set; }

        public List<string>? ImageList { get; set; }
    }
}
