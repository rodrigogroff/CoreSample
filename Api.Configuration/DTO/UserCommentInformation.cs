
using System.Collections.Generic;

namespace Api.User.DTO
{
    public class DTO_UserComments
    {
        public List<DTO_UserCommentInformation> list = new List<DTO_UserCommentInformation>();
    }

    public class DTO_UserCommentInformation
    {
        public string Comment;
        public string ProductId;
        public string ProductName;
        public string ProductCategory;
        public string Date;
    }
}
