using System.Collections.Generic;

namespace Entities.Api.Configuration
{
    public class AdminSubCategories
    {
        public int total;
        public List<AdminSubCategory> list = new List<AdminSubCategory>();
    }

    public class AdminSubCategory
    {
        public long Id;
        public string Name;        
    }

    public class NewSubCategoryData
    {
        public long Id;
        public long ProductCategoryID;
        public string Name;
    }
}
