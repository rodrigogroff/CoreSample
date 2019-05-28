using System.Collections.Generic;

namespace Entities.Api.Configuration
{
    public class AdminCategories
    {
        public int total;
        public List<AdminCategory> list = new List<AdminCategory>();
    }

    public class AdminCategory
    {
        public long Id;
        public string Name;        
    }

    public class NewCategoryData
    {
        public long Id;
        public string Name;
    }
}
