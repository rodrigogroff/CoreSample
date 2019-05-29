
using System;

namespace Entities.Database
{
    public class ProductView
    {
        public long Id { get; set; }

        public long? ProductID { get; set; }

        public long? UserID { get; set; }

        public DateTime? DateAdded { get; set; }
    }
}
