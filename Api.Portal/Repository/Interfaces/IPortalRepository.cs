﻿using System.Collections.Generic;
using System.Data.SqlClient;

namespace Api.Configuration.Repository
{
    public interface IPortalRepository
    {
        List<Entities.Database.ProductCategory> CategoryList(SqlConnection db, int skip, int take, ref int total);
    }
}
