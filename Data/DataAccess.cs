using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using WorkTabel.Model.ObIrtish;
using MySql.Data;
using MySqlConnector;

namespace WorkTabel.Data
{
    public class DataAccess
    {
        public class DepartmentDataAccess
        {
            private readonly string _connectionString;

            public DepartmentDataAccess()
            {
                _connectionString = ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString;
            }

            public List<Department> GetDepartments()
            {
                var departments = new List<Department>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new MySqlCommand("SELECT DepartmentID, DepartmentName FROM Departments", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                departments.Add(new Department
                                {
                                    DepartmentID = reader.GetInt32(0),
                                    DepartmentName = reader.GetString(1)
                                });
                            }
                        }
                    }
                }

                return departments;
            }
        }
    }
}
