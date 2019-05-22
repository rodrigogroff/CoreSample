﻿using Api.User.Repository;
using Database;
using Master.Controllers;
using System.Data.SqlClient;

namespace UnitTesting
{
    public class mockUserRepositoryUserExists : IUserRepository
    {
        public bool ClientExists(SqlConnection db, string client_guid) { return true; }

        public long AddUser(SqlConnection db, NewUserData user)  { return 1; }
        
        public bool UserExists(SqlConnection db, string email, string client_guid) { return true; }

        public bool UserLogin(SqlConnection db, string email, string password, string clientGuid, ref User user) { return true; }
    }

    public class mockUserRepositoryUserNotExists : IUserRepository
    {
        public bool ClientExists(SqlConnection db, string client_guid) { return true; }

        public long AddUser(SqlConnection db, NewUserData user) { return 1; }

        public bool UserExists(SqlConnection db, string email, string client_guid) { return false; }

        public bool UserLogin(SqlConnection db, string email, string password, string clientGuid, ref User user) { return false; }
    }
}