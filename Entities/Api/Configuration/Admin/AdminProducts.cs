using System.Collections.Generic;

namespace Entities.Api.Configuration
{
    public class AdminProducts
    {
        public int total;
        public List<AdminProductData> list = new List<AdminProductData>();
    }

    public class NewProductData
    {
        public long Id;
        public long ProductCategoryID;
        public long ProductSubCategoryID;
        public string Name;
    }

    public class AdminProductData
    {
        public long Id;
        public long ProductCategoryID;
        public long ProductSubCategoryID;

        public string Name;
        public string CategName;
        public string SubCategName;
        public string DateCreated;
        public string DateLastEdit;
        public string AdminCreation;
        public string AdminEdit;
    }
}
