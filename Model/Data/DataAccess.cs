using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using WorkTabel.Model.ObIrtish;
using MySql.Data;
using MySqlConnector;
using System.Collections.ObjectModel;

namespace WorkTabel.Model.Data
{
    //слой допуска к данным. считываем данные из бд и записываем их в класс переменной в Model/ObIrtish/Sotrudnik.cs
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

                    using (var command = new MySqlCommand("SELECT *  FROM Employees", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                                {
                                    employees.Add(new Employee
                                    {
                                        EmployeeID = reader.GetInt32(0),
                                        FullName = reader.GetString(1),
                                        PositionID = reader.GetInt32(2),
                                        DepartmentID = reader.GetInt32(3)
                                    });
                                });

                            }
                        }
                    }
                    connection.Close();
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
                                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                                {
                                    departments.Add(new Department
                                    {
                                        DepartmentID = reader.GetInt32(0),
                                        DepartmentName = reader.GetString(1)
                                    });
                                });
                            }
                        }
                    }
                    connection.Close();
                }

                return departments;
            }
        }

        public class AttendanceTypeDataAccess
        {

            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString;

            public ObservableCollection<AttendanceType> GetAttendanceTypes()
            {
                var attendanceTypes = new ObservableCollection<AttendanceType>();

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new MySqlCommand("SELECT *  FROM AttendanceType", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                                {
                                    attendanceTypes.Add(new AttendanceType
                                    {
                                        AttendanceTypeID = reader.GetInt32(0),
                                        Abbreviation = reader.GetString(1),
                                        Definition = reader.GetString(2),
                                    });
                                });

                            }
                        }
                    }
                    connection.Close();
                }

                return attendanceTypes;
            }
        }
    }
}
