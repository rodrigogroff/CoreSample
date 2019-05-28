using Api.Configuration.Repository;
using Entities.Api.Configuration;
using Entities.Api;
using System.Data.SqlClient;
using System;

namespace Api.Configuration.Service
{
    public class AdminEditProductV1
    {
        public ServiceError Error;
        public IAdminRepository repository;        

        public AdminEditProductV1(IAdminRepository _repository)
        {
            repository = _repository;
        }

        public bool Exec(SqlConnection db, AuthenticatedUser au, NewProductData obj)
        {
            if (string.IsNullOrEmpty(obj.Name))
            {
                Error = new ServiceError { Message = "Name is empty!" };
                return false;
            }

            if (!repository.ProductExistsId(db, obj.Id))
            {
                Error = new ServiceError { Message = "Id invalid!" };
                return false;
            }

            var prod = repository.ProductById(db, obj.Id);

            prod.DateEdit = DateTime.Now;
            prod.LastEditByAdminID = au.Id;
            prod.Name = obj.Name;
            
            repository.ProductEdit(db, prod);
            
            return true;
        }
    }
}
