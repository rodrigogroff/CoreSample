using Api.Portal.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;
using Entities.Api.Portal;

namespace Api.Portal.Service
{
    public class PortalProductCommentV1
    {
        public ServiceError Error;
        public IPortalRepository repository;        

        public PortalProductCommentV1(IPortalRepository _repository)
        {
            repository = _repository;
        }

        public bool Exec(SqlConnection db, AuthenticatedUser au, NewProductComment comment)
        {
            return repository.ProductAddComment(db, au.Id, comment) > 0 ? true : false;
        }
    }
}
