using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using WorkTabel.Model.ObIrtish;
using MySql.Data;
using MySqlConnector;
using System.Collections.ObjectModel;

namespace WorkTabel.Data
{
    public class DataAccess
    {
        
        public class EmployeeDataAccess
        {
            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString;


            public ObservableCollection<Employee> GetEmployees()
            {
                var employees = new ObservableCollection<Employee>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new MySqlCommand("SELECT EmployeeID, FullName, PositionID, DepartmentID  FROM Employees", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                App.Current.Dispatcher.Invoke(() =>
                                {
                                    employees.Add(new Employee
                                    {
                                        //EmployeeID = reader.GetInt32(0),
                                        FullName = reader.GetString(1),
                                        PositionID = reader.GetInt32(2),
                                        DepartmentID = reader.GetInt32(3)
                                    });
                                });

                            }
                        }
                    }
                }

                return employees;
            }
        }

        public class DepartmentDataAccess
        {
            //private readonly string _connectionString;

            //public DepartmentDataAccess()
            //{}
            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString;


            public List<Department> GetDepartments()
            {
                var departments = new List<Department>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new MySqlCommand("SELECT DepartmentID, Name FROM Departments", connection))
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
