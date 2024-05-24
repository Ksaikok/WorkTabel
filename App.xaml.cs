using LinqToDB.SqlQuery;
using System.Configuration;
using System.Data;
using System.Windows;
using static LinqToDB.DataProvider.SqlServer.SqlServerProviderAdapter;
using MySqlConnector;

namespace WorkTabel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ///// <summary>
        ///// Test that the server is connected
        ///// </summary>
        ///// <param name="connectionString">The connection string</param>
        ///// <returns>true if the connection is opened</returns>
        //private static bool IsServerConnected(string connectionString)
        //{
        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            connection.Open();
        //            return true;
        //        }
        //        catch (MySqlException)
        //        {
        //            return false;
        //        }
        //        finally
        //        {
        //            // not really necessary
        //            connection.Close();
        //        }
        //    }
        //}
    }

}
