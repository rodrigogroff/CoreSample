
using System;
using System.Collections.Generic;

namespace Api.Configuration.DTO
{
    public class DTO_UserComments
    {
        public int total;
        public List<DTO_UserCommentInformation> list = new List<DTO_UserCommentInformation>();
    }

    public class DTO_UserCommentInformation
    {
        public string Comment;
        public long ProductId;
        public string ProductName;
        public string ProductCategory;
        public DateTime? Date;
    }
}
