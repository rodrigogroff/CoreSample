using Api.User.DTO;
using Api.User.Repository;
using Master.Controllers;
using System.Data.SqlClient;

namespace Api.User.Service
{
    public class UserActionsV1
    {
        public ServiceError Error;
        public AuthenticatedUser loggedUser;
        public IUserRepository repository;

        public UserActionsV1(IUserRepository _repository)
        {
            repository = _repository;
        }

        public DTO_UserComments Comments(SqlConnection db, AuthenticatedUser au)
        {
            return new DTO_UserComments();
        }

        public bool SaveComment(UserActionComment comment)
        {
            return true;
        }
    }
}
