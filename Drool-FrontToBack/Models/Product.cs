using Drool_FrontToBack.Models.Common;

namespace Drool_FrontToBack.Models;

public class Product : BaseEntity
{
    public string ImagePath { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }

}