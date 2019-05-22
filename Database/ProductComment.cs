
using System;

namespace Database
{
    public class ProductComment
    {
        public long Id { get; set; }

        public long? ProductID { get; set; }

        public long? UserID { get; set; }

        public string Comment { get; set; }

        public DateTime? DateAdded { get; set; }
    }
}
