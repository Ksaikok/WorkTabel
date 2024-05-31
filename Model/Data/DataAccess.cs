using System.Configuration;
using WorkTabel.Model.ObIrtish;
using MySqlConnector;
using System.Collections.ObjectModel;
using System.Windows;

namespace WorkTabel.Model.Data
{
    //слой допуска к данным. считываем данные из бд и записываем их в класс переменной в Model/ObIrtish/Sotrudnik.cs
    // подключение к БД прописано в app.config
    public class DataAccess
    {
        //загрузка инфы о сотрудниках
        public class EmployeeDataAccess
        {

            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString;

            public ObservableCollection<Employee> GetEmployees()
            {
                var employees = new ObservableCollection<Employee>();
                try
                {

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
                                            TabelNum = reader.GetInt32(2),
                                            PositionID = reader.GetInt32(3),
                                            DepartmentID = reader.GetInt32(4),
                                            PhoneNumber = reader.GetString(5),
                                            Email = reader.GetString(6),
                                            Birthday = reader.GetDateTime(7),
                                        });
                                    });

                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    // Обработка исключения
                    MessageBox.Show("Не удалось подключиться к базе данных. Не удалось подключиться к таблице Employee. " + ex.Message);
                }
                return employees;
            }
        }

        // загрузка инфы о отделах
        public class DepartmentDataAccess
        {
           
            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString;


            public List<Department> GetDepartments()
            {
                var departments = new List<Department>();
                try
                {

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
                }
                catch (MySqlException ex)
                {
                    // Обработка исключения
                    MessageBox.Show("Не удалось подключиться к базе данных. Не удалось подключиться к таблице Department. " + ex.Message);
                }
                return departments;
            }
        }

        // инфа о типах посещения (в табелях тип посещения проставляется определённой буквой, каждая имеет свою расшифровку)
        public class AttendanceTypeDataAccess
        {

            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString;

            public ObservableCollection<AttendanceType> GetAttendanceTypes()
            {
                var attendanceTypes = new ObservableCollection<AttendanceType>();
                try
                {

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
                }
                catch (MySqlException ex)
                {
                    // Обработка исключения
                    MessageBox.Show("Не удалось подключиться к базе данных. Не удалось подключиться к таблице AttendanceType. " + ex.Message);
                }

                return attendanceTypes;
            }
        }

        // инфа о посещении (кто когда и сколько отработал)
        public class AttendanceDataAccess
        {

            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString;

            public ObservableCollection<Attendance> GetAttendances()
            {
                var attendances = new ObservableCollection<Attendance>();
                try
                {

                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (var command = new MySqlCommand("SELECT *  FROM Attendance", connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        attendances.Add(new Attendance
                                        {
                                            AttendanceID = reader.GetInt32(0),
                                            AttendanceDate = reader.GetDateTime(1),
                                            TimeIn = reader.GetDateTime(2),
                                            TimeOut = reader.GetDateTime(3),
                                            EmployeeID = new Employee
                                            {
                                                EmployeeID = reader.GetInt32(4),
                                            },
                                            AttendanceTypeID = new AttendanceType
                                            {
                                                AttendanceTypeID = reader.GetInt32(4),
                                            }

                                        });
                                    });

                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch(MySqlException ex) 
                {
                    // Обработка исключения
                    MessageBox.Show("Не удалось подключиться к базе данных. Не удалось подключиться к таблице Attendance. " + ex.Message);

                }
                catch(Exception ex) 
                {
                    Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
                }

                return attendances;
            }
        }
    }
}
