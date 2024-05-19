using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySqlConnector;
using System.Dynamic;
using System.Data;

namespace WorkTabel.Services
{
    // строка подключения
    public class WorkTabelDB_Service
    {
        private string connectionString = "Server=localhost;Database=worktime_tabelDB;User ID=root;Password='';";

        public MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }

        public void ExecuteQuery(string query)
        {
            using (MySqlConnection connection = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("Запрос выполнен успешно.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Возникла ошибка при исполнении" + ex.Message);
                    }
                }
            }
        }
        //метод для чтения данных из БД
        public MySqlDataReader GetSqlData(string query)
        {
            MySqlDataReader reader = null;
            using (MySqlConnection connection = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        reader = command.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка при чтении данных: " + ex.Message);
                    }
                }
            }
            return reader;
        }
    public DataTable GetDepartments()
        {
            using (MySqlConnection connection = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand("SELECT Name FROM Departments", connection))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable departmentsTable = new DataTable();
                    adapter.Fill(departmentsTable);
                    return departmentsTable;
                }
            }
        }





    }
}

