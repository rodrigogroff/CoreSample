using System;

namespace Entities.Database
{
    public class Product
    {
        public long Id { get; set; }

        public long ProductCategoryID { get; set; }

        public long ProductSubCategoryID { get; set; }

        public string Name { get; set; }

        public long CreatedByAdminID { get; set; }

        public DateTime DateAdded { get; set; }

        public long LastEditByAdminID { get; set; }

        public DateTime DateEdit { get; set; }
    }
}
