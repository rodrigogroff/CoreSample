using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Dapper;

namespace Mass
{
    class Program
    {
        static string GetNome(ref List<string> lstPrimeirosNomes, ref List<string> lstSobrenomes)
        {
            var str = "";

            var r = new Random();

            int its = 1;
            if (r.Next(1, 10) > 8) its = 2;

            for (int i = 0; i < its; i++)
                str += lstPrimeirosNomes[r.Next(0, lstPrimeirosNomes.Count)] + " ";

            its = 1;

            if (r.Next(1, 10) > 6) its = 2;
            if (r.Next(1, 10) > 9) its = 3;

            for (int i = 0; i < its; i++)
                str += lstSobrenomes[r.Next(0, lstSobrenomes.Count)] + " ";

            return str.TrimEnd();
        }

        static string GetSobreNome(ref List<string> lstSobrenomes)
        {
            var r = new Random();
            return lstSobrenomes[r.Next(0, lstSobrenomes.Count)];
        }

        static void Main(string[] args)
        {
            List<string> lstPrimeirosNomes = new List<string>();
            List<string> lstSobrenomes = new List<string>();

            using (var sr = new StreamReader("nomes.txt"))
            {
                var arrNomes = sr.ReadToEnd().Replace("\r\n", "¨").Replace("\n", "¨").Split('¨');

                foreach (var item in arrNomes)
                {
                    if (item.StartsWith("-") || item == "")
                        continue;

                    int iPos = 0;
                    foreach (var iNome in item.Split(' '))
                    {
                        if (iNome.Length > 2)
                            if (!Char.IsNumber(iNome[0]))
                            {
                                if (iPos < 2)
                                    lstPrimeirosNomes.Add(iNome);
                                else
                                    lstSobrenomes.Add(iNome);                               
                            }
                        iPos++;
                    }                    
                }
            }

            var strCon = "Data Source=WIN-VC8RCO6KBEK\\SQLEXPRESS;Initial Catalog=VortigoServicePlatform;Integrated Security=SSPI;";

            using (var db = new SqlConnection(strCon))
            {
                db.Query("truncate table [Admin]");
                db.Query("truncate table [User]");
                db.Query("truncate table [ProductCategory]");
                db.Query("truncate table [ProductSubCategory]");
                db.Query("truncate table [Product]");
            }

            using (var db = new SqlConnection(strCon))
            {
                db.Query("insert into [Admin] (Name,Email,Password) values (@Name,@Email,@Password)", 
                    new { Name = GetNome(ref lstPrimeirosNomes, ref lstSobrenomes), Email = "dba@client.com", Password = "123456" });

                StringBuilder sb = new StringBuilder();

                for (int j = 1; j <= 100; j++)
                {
                    sb.AppendLine ( "insert into [User] (Name,Email,Password) values ('" + 
                                    GetNome(ref lstPrimeirosNomes, ref lstSobrenomes) + "', 'user" + j + "@client.com', '123456');");

                    if (j % 100 == 0)
                    {
                        db.Query(sb.ToString());
                        sb.Clear();
                    }                        
                }

                sb.Clear();

                int totCat = 20;
                int totSubPerCat = 100;
                int totProds = 100;

                for (int j = 1; j <= totCat; j++)
                {
                    sb.AppendLine("insert into [ProductCategory] (Name) values ('" + GetSobreNome(ref lstSobrenomes) + j.ToString() + "');");

                    if (j % 10 == 0)
                    {
                        db.Query(sb.ToString());
                        sb.Clear();
                    }
                }

                sb.Clear();

                for (int cat = 1; cat <= totCat; cat++)
                    for (int sub = 1; sub <= totSubPerCat; sub++)
                {
                    sb.AppendLine("insert into [ProductSubCategory] (Name,ProductCategoryID) values ('sub" + GetSobreNome(ref lstSobrenomes) + sub.ToString() + "'," + cat + ");");

                    if (sub % 10 == 0)
                    {                        
                        db.Query(sb.ToString());
                        sb.Clear();
                    }
                }

                sb.Clear();

                for (int cat = 1; cat <= totCat; cat++)
                {
                    Console.WriteLine(cat + "/" + totCat);

                    for (int sub = 1; sub <= totSubPerCat; sub++)
                    {
                        int indexSub = (cat -1) * totSubPerCat + sub;

                        for (int i = 0; i < totProds; i++)
                        {
                            sb.AppendLine("insert into [Product] (Name,ProductCategoryID,ProductSubCategoryID,CreatedByAdminID,DateAdded,LastEditByAdminID,DateEdit) values ('prod" + 
                                GetSobreNome(ref lstSobrenomes) + "'," + cat + "," + indexSub + ", 1, '2019-01-01',1,'2019-01-01');");

                            if (i % 100 == 0)
                            {
                                db.Query(sb.ToString());
                                sb.Clear();
                            }
                        }
                    }                        
                }
            }
            
        }
    }
}
