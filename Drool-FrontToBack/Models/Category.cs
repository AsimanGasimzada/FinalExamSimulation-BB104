using Drool_FrontToBack.Models.Common;

namespace Drool_FrontToBack.Models;

public class Category:BaseEntity
{
    public string Name { get; set; }
    public IEnumerable<Product> Products { get; set; }
}
