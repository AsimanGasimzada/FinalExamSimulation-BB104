using Drool_FrontToBack.ViewModels.CategoryViewModels;
using Drool_FrontToBack.ViewModels.ProductViewModels;

namespace Drool_FrontToBack.ViewModels;

public class HomeVM
{
    public List<ProductGetVM> Products { get; set; }
    public List<CategoryGetVM> Categories { get; set; }
}
