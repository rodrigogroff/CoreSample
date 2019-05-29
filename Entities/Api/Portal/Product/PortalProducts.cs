
using System.Collections.Generic;

namespace Entities.Api.Portal
{
    public class PortalProducts
    {
        public int total;
        public List<ProductSimpleDto> list = new List<ProductSimpleDto>();
    }
    
    public class ProductSimpleDto
    {
        public long Id;
        public long Views;
        public string Name;        
    }

    public class ProductDto
    {
        public long Id;
        public long Views;

        public string Name;        
        public string Category;
        public string SubCategory;
    }
}
