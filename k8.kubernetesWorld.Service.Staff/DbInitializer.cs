using k8.kubernetesWorld.Service.Staff.EFModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace k8.kubernetesWorld.Service.Employee.Data
{
    public static class DbInitializer
    {
        public static bool Initialize(string connectionstring)
        {
            List<string> _response = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    Console.WriteLine("Connected successfully.");

                    StringBuilder sbDBSql = new StringBuilder();
                    sbDBSql.Append("USE master; ");
                    sbDBSql.Append("IF DB_ID('staffDB') IS NULL ");
                    sbDBSql.Append("BEGIN ");
                    sbDBSql.Append("CREATE DATABASE staffDB; ");
                    sbDBSql.Append("END ");

                    using (SqlCommand command = new SqlCommand(sbDBSql.ToString(), connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    StringBuilder sbTableSql = new StringBuilder();
                    sbTableSql.Append("IF DB_ID('staffDB') IS NOT NULL ");
                    sbTableSql.Append("BEGIN ");
                    sbTableSql.Append("USE staffDB; ");
                    sbTableSql.Append("IF(NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '[dbo].[Staff]')) ");
                    sbTableSql.Append("BEGIN ");
                    sbTableSql.Append("CREATE TABLE[dbo].[Staff]( ");
                    sbTableSql.Append("[ID][bigint] IDENTITY(1, 1) NOT NULL, ");
                    sbTableSql.Append("[FirstName] [nvarchar] (100) NULL, ");
                    sbTableSql.Append("[LastName] [nvarchar] (100) NULL, ");
                    sbTableSql.Append("[EnrollmentDate] [datetime] NULL ");
                    sbTableSql.Append(") ON[PRIMARY]; ");

                    sbTableSql.Append("ALTER TABLE[dbo].[Staff] ADD CONSTRAINT[DF_Staff_EnrollmentDate]  DEFAULT(getdate()) FOR[EnrollmentDate];");
                    sbTableSql.Append("INSERT[dbo].[Staff] ([FirstName], [LastName], [EnrollmentDate]) VALUES(N'Umar', N'Kin', CAST(N'2020-04-26T02:58:31.280' AS DateTime)); ");
                    sbTableSql.Append("INSERT[dbo].[Staff] ([FirstName], [LastName], [EnrollmentDate]) VALUES(N'Sam', N'Blake', CAST(N'2020-04-26T02:58:38.350' AS DateTime)); ");
                    sbTableSql.Append("INSERT[dbo].[Staff] ([FirstName], [LastName], [EnrollmentDate]) VALUES(N'Peter', N'Pan', CAST(N'2020-04-26T02:58:42.930' AS DateTime)); ");
                    sbTableSql.Append("INSERT[dbo].[Staff] ([FirstName], [LastName], [EnrollmentDate]) VALUES(N'Micky', N'Mouse', CAST(N'2020-04-26T02:58:48.253' AS DateTime)); ");
                    sbTableSql.Append("INSERT[dbo].[Staff] ([FirstName], [LastName], [EnrollmentDate]) VALUES(N'Ray', N'Da', CAST(N'2020-04-26T02:58:53.320' AS DateTime)); ");
                    sbTableSql.Append("INSERT[dbo].[Staff] ([FirstName], [LastName], [EnrollmentDate]) VALUES(N'Niel', N'White', CAST(N'2020-04-26T02:58:59.757' AS DateTime)); "); sbTableSql.Append("END; ");
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
