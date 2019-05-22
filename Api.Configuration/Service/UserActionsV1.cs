using Api.User.DTO;
using Api.User.Repository;
using Master.Controllers;
using System.Data.SqlClient;

namespace Api.User.Service
{
    public class UserActionsV1
    {
        public ServiceError Error;
        public IUserRepository repository;

        public UserActionsV1(IUserRepository _repository)
        {
            repository = _repository;
        }

        public DTO_UserComments Comments(SqlConnection db, AuthenticatedUser au, int skip, int take)
        {
            var retComments = new DTO_UserComments();

            foreach (var item in repository.UserComments(db, au.Id, skip, take, ref retComments.total))
            {
                retComments.list.Add(new DTO_UserCommentInformation
                {
                    Comment = item.Comment,
                    Date = item.DateAdded,
                    ProductName = "x",
                    ProductCategory = "y",
                    ProductId = 1
                });
            }
            
            return retComments;
        }
    }
}
