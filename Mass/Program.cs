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

            var strCon = "Data Source=DESKTOP-6JMR2NF;Initial Catalog=VortigoServicePlatform;Integrated Security=SSPI;";

            using (var db = new SqlConnection(strCon))
            {
                db.Query("truncate table [Admin]");
                db.Query("truncate table [User]");
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
                        Console.WriteLine(j);
                        db.Query(sb.ToString());
                        sb.Clear();
                    }                        
                }
            }
            
        }
    }
}
