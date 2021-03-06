﻿using Api.Portal.Repository;
using Entities.Api;
using Entities.Api.Configuration;
using Entities.Api.Portal;
using System.Data.SqlClient;

namespace Api.Portal.Service
{
    public class UserCommentsV1
    {
        public ServiceError Error;
        public IUserRepository repository;

        public UserCommentsV1(IUserRepository _repository)
        {
            repository = _repository;
        }

        public UserComments Exec(SqlConnection db, AuthenticatedUser au, int skip, int take)
        {
            var retComments = new UserComments();

            foreach (var item in repository.UserComments(db, au.Id, skip, take, ref retComments.total))
            {
                retComments.list.Add(new UserCommentInformation
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
