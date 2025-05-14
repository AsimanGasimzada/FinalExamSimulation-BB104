namespace Drool_FrontToBack.ViewModels.ProductViewModels;

public class ProductUpdateVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile? Image { get; set; }
    public int CategoryId { get; set; }
}
