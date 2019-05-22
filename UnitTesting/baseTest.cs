using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace UnitTesting
{
    public class BaseTest 
    {
        public string strCon = "Data Source=DESKTOP-6JMR2NF;Initial Catalog=VortigoServicePlatform;Integrated Security=SSPI;";
        
        public string GetValidClientGuid()
        {
            using (var db = new SqlConnection(strCon))
            {
                return db.QueryFirstOrDefault<string>
                                ("select Guid from [Client] (nolock) where Id=@Client", new
                                {
                                    Client = 1
                                });
            }
        }

    }
}
