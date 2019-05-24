
namespace Entities.Database
{
    public class ProductSubCategory
    {
        public long Id { get; set; }
        public long ProductCategoryID { get; set; }
        public string Name { get; set; }
    }
}
