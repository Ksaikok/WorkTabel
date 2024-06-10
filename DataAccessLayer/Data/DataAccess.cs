using System.Configuration;
using WorkTabel.Model.ObIrtish;
using MySqlConnector;
using System.Collections.ObjectModel;
using System.Windows;

namespace WorkTabel.DataAccessLayer.Data
{
    //слой допуска к данным. считываем данные из бд и записываем их в класс переменной в Model/ObIrtish/Sotrudnik.cs
    public class DataAccess
    {
        internal T ExecuteScalar<T>(string query, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public class EmployeeDataAccess //загрузка инфы о сотрудниках
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

                        using (var command = new MySqlCommand("SELECT * FROM Employees", connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        employees.Add(new Employee
                                        {
                                            EmployeeID = reader.GetInt32(0),
                                            FullName = reader.GetString(1),
                                            TabelNum = reader.GetInt32(2),
                                            PositionID = new Position
                                            {
                                                PositionID = reader.GetInt32(3),
                                            },
                                            DepartmentID = new Department
                                            {
                                                DepartmentID = reader.GetInt32(4)
                                            },
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
            public void UpdateEmployee(Employee employee)
            {
                try
                {
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();
                        var query = "UPDATE Employees SET FullName = @FullName," +
                            " TabelNum = @TabelNum, PositionID = @PositionID, " +
                            "DepartmentID = @DepartmentID, PhoneNumber = @PhoneNumber, " +
                            "Email = @Email, Birthday = @Birthday WHERE EmployeeID = @EmployeeID";
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                            command.Parameters.AddWithValue("@FullName", employee.FullName);
                            command.Parameters.AddWithValue("@TabelNum", employee.TabelNum);
                            command.Parameters.AddWithValue("@PositionID", employee.PositionID.PositionID);
                            command.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID.DepartmentID);
                            command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                            command.Parameters.AddWithValue("@Email", employee.Email);
                            command.Parameters.AddWithValue("@Birthday", employee.Birthday);

                            command.ExecuteNonQuery();
                        }
                        connection.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Не удалось обновить данные сотрудника. " + ex.Message);
                }
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
                                    Application.Current.Dispatcher.Invoke(() =>
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

        public class PositionDataAccess
        {
            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString;
            public List<Position> GetPositions()
            {
                var positions = new List<Position>();
                try
                {
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (var command = new MySqlCommand("SELECT * FROM Positions", connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        positions.Add(new Position
                                        {
                                            PositionID = reader.GetInt32(0),
                                            PositionName = reader.GetString(1)
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
                    MessageBox.Show("Не удалось подключиться к базе данных. Не удалось подключиться к таблице Positions. " + ex.Message);
                }
                return positions;
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
                                    Application.Current.Dispatcher.Invoke(() =>
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
            public void SaveAttendances(List<Attendance> attendances)
            {
                using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach (var attendance in attendances)
                            {
                                try
                                {
                                    using (var command = new MySqlCommand("INSERT INTO Attendance (AttendanceDate, EmployeeID, AttendanceTypeID, TimeIn, TimeOut, WorkedOut) VALUES (@AttendanceDate, @EmployeeID, @AttendanceTypeID, @TimeIn, @TimeOut, @WorkedOut)", connection))
                                    {
                                        command.Parameters.AddWithValue("@AttendanceDate", attendance.AttendanceDate);
                                        command.Parameters.AddWithValue("@TimeIn", attendance.TimeIn ?? (object)DBNull.Value);
                                        command.Parameters.AddWithValue("@TimeOut", attendance.TimeOut ?? (object)DBNull.Value);
                                        command.Parameters.AddWithValue("@WorkedOut", attendance.WorkedOut);
                                        command.Parameters.AddWithValue("@EmployeeID", attendance.EmployeeID.EmployeeID);
                                        command.Parameters.AddWithValue("@AttendanceTypeID", attendance.AttendanceTypeID?.AttendanceTypeID ?? (object)DBNull.Value);

                                        // Логирование параметров перед выполнением команды
                                        Console.WriteLine($"AttendanceDate: {attendance.AttendanceDate}, TimeIn: {attendance.TimeIn}, TimeOut: {attendance.TimeOut}, WorkedOut: {attendance.WorkedOut}, EmployeeID: {attendance.EmployeeID.EmployeeID}, AttendanceTypeID: {attendance.AttendanceTypeID?.AttendanceTypeID}");

                                        command.ExecuteNonQuery();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // Логирование исключения для отдельной записи
                                    Console.WriteLine($"Ошибка при выполнении команды для EmployeeID: {attendance.EmployeeID.EmployeeID}, Error: {ex.Message}");
                                    // Прервать выполнение транзакции, если требуется
                                    transaction.Rollback();
                                    return;
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            // Логирование исключения транзакции
                            Console.WriteLine($"Ошибка при выполнении транзакции: {ex.Message}");
                            transaction.Rollback();
                        }
                    }
                    connection.Close();
                }
            }

            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString;

            public ObservableCollection<Attendance> GetAttendances(int departmentId, int year, int month)
            {
                var attendances = new ObservableCollection<Attendance>();
                try
                {
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();
                        var query = @"
                SELECT a.AttendanceID, a.AttendanceDate, a.TimeIn, a.TimeOut, a.WorkedOut, a.EmployeeID, a.AttendanceTypeID
                FROM Attendance a
                JOIN Employees e ON a.EmployeeID = e.EmployeeID
                WHERE e.DepartmentID = @DepartmentID AND YEAR(a.AttendanceDate) = @Year AND MONTH(a.AttendanceDate) = @Month";

                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@DepartmentID", departmentId);
                            command.Parameters.AddWithValue("@Year", year);
                            command.Parameters.AddWithValue("@Month", month);

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        attendances.Add(new Attendance
                                        {
                                            AttendanceID = reader.GetInt32(0),
                                            AttendanceDate = reader.GetDateTime(1),
                                            TimeIn = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                                            TimeOut = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
                                            WorkedOut = reader.GetInt32(4),
                                            EmployeeID = new Employee { EmployeeID = reader.GetInt32(5) },
                                            AttendanceTypeID = new AttendanceType { AttendanceTypeID = reader.GetInt32(6) }
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
                    MessageBox.Show("Не удалось подключиться к базе данных. Не удалось подключиться к таблице Attendance. " + ex.Message);
                }
                return attendances;
            }
        }

        //10
        public class AddEmpDataAccess
        {
            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString;

            public bool AddEmployee(Employee employee)
            {
                try
                {
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (var command = new MySqlCommand("INSERT INTO Employees (FullName, TabelNum, PositionID, DepartmentID, PhoneNumber, Email, Birthday) VALUES (@FullName, @TabelNum, @PositionID, @DepartmentID, @PhoneNumber, @Email, @Birthday)", connection))
                        {
                            command.Parameters.AddWithValue("@FullName", employee.FullName);
                            command.Parameters.AddWithValue("@TabelNum", employee.TabelNum);
                            command.Parameters.AddWithValue("@PositionID", employee.PositionID.PositionID); // Используем PositionID из объекта Position
                            command.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID.DepartmentID); // Используем DepartmentID из объекта Department
                            command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                            command.Parameters.AddWithValue("@Email", employee.Email);
                            command.Parameters.AddWithValue("@Birthday", employee.Birthday);

                            command.ExecuteNonQuery();
                            return true;
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    // Обработка исключения
                    MessageBox.Show("Не удалось добавить сотрудника. " + ex.Message);
                    return false;
                }
            }
        }

        public class DelEmpDataAccess
        {
            private readonly string _connectionString = ConfigurationManager.ConnectionStrings["WorkTabelDB"].ConnectionString;

            public bool DeleteEmployee(Employee employee)
            {
                try
                {
                    using (var connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (var command = new MySqlCommand("DELETE FROM Employees WHERE EmployeeID = @EmployeeID", connection))
                        {
                            command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Не удалось удалить сотрудника. " + ex.Message);
                    return false;
                }
            }
        }
        //-10

    }
}
