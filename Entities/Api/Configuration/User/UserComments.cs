
using System;
using System.Collections.Generic;

namespace Entities.Api.Configuration
{
    public class UserComments
    {
        public int total;
        public List<UserCommentInformation> list = new List<UserCommentInformation>();
    }

    public class UserCommentInformation
    {
        public string Comment;
        public long ProductId;
        public string ProductName;
        public string ProductCategory;
        public DateTime? Date;
    }
}
