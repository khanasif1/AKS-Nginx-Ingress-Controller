using k8.kubernetesWorld.Service.Product.EFModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace k8.kubernetesWorld.Service.Product.Data
{
    public static class DbInitializer
    {
        public static bool Initialize(string connectionString)
        {
            List<string> _response = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected successfully.");

                    StringBuilder sbDBSql = new StringBuilder();
                    sbDBSql.Append("USE master; ");
                    sbDBSql.Append("IF DB_ID('productDB') IS NULL ");
                    sbDBSql.Append("BEGIN ");
                    sbDBSql.Append("CREATE DATABASE productDB; ");
                    sbDBSql.Append("END ");

                    using (SqlCommand command = new SqlCommand(sbDBSql.ToString(), connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    StringBuilder sbTableSql = new StringBuilder();
                    sbTableSql.Append("IF DB_ID('productDB') IS NOT NULL ");
                    sbTableSql.Append("BEGIN ");
                    sbTableSql.Append("USE productDB; ");
                    sbTableSql.Append("IF(NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '[dbo].[Product]')) ");
                    sbTableSql.Append("BEGIN ");
                    sbTableSql.Append("CREATE TABLE[dbo].[Product]( ");
                    sbTableSql.Append("[ID][bigint] IDENTITY(1, 1) NOT NULL, ");
                    sbTableSql.Append("[Name] [nvarchar] (100) NULL, ");
                    sbTableSql.Append("[Description] [nvarchar] (100) NULL, ");
                    sbTableSql.Append("[EnrollmentDate] [datetime] NULL ");
                    sbTableSql.Append(") ON[PRIMARY]; ");

                    sbTableSql.Append("ALTER TABLE[dbo].[Product] ADD CONSTRAINT[DF_Product_EnrollmentDate]  DEFAULT(getdate()) FOR[EnrollmentDate];");
                    sbTableSql.Append("INSERT[dbo].[Product] ([Name], [Description], [EnrollmentDate]) VALUES(N'Car', N'Jeep', CAST(N'2020-04-26T02:58:31.280' AS DateTime)); ");
                    sbTableSql.Append("INSERT[dbo].[Product] ([Name], [Description], [EnrollmentDate]) VALUES(N'Bike', N'Ducati', CAST(N'2020-04-26T02:58:38.350' AS DateTime)); ");
                    sbTableSql.Append("INSERT[dbo].[Product] ([Name], [Description], [EnrollmentDate]) VALUES(N'Plane', N'Boing', CAST(N'2020-04-26T02:58:42.930' AS DateTime)); ");
                    sbTableSql.Append("INSERT[dbo].[Product] ([Name], [Description], [EnrollmentDate]) VALUES(N'Watch', N'Rado', CAST(N'2020-04-26T02:58:48.253' AS DateTime)); ");
                    sbTableSql.Append("INSERT[dbo].[Product] ([Name], [Description], [EnrollmentDate]) VALUES(N'Pen', N'Ball', CAST(N'2020-04-26T02:58:53.320' AS DateTime)); ");
                    sbTableSql.Append("INSERT[dbo].[Product] ([Name], [Description], [EnrollmentDate]) VALUES(N'TV', N'LG', CAST(N'2020-04-26T02:58:59.757' AS DateTime)); ");
                    sbTableSql.Append("END; ");
                    sbTableSql.Append("END; ");



                    using (SqlCommand command = new SqlCommand(sbTableSql.ToString(), connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand("USE master; Select Name from sys.Databases;", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _response.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                //return _employeeContext.Employees.Select(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                //_response.Add(configuration.GetConnectionString("DefaultConnection"));
                _response.Add(ex.Message.ToString());

            }
            return true;
        }
    }
}
