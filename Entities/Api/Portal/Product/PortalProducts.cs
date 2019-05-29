
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
        public long Comments;
        public string Name;        
    }

    public class ProductDto
    {
        public long Id;
        public long Views;

        public string Name;        
        public string Category;
        public string SubCategory;

        public List<NewProductComment> Comments = new List<NewProductComment>();
    }

    public class NewProductComment
    {
        public long ProductID;
        public string Comment;
        public string Date;
        public string UserName;
    }
}
