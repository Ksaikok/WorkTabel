using LinqToDB.SqlQuery;
using System.Configuration;
using System.Data;
using System.Windows;
using static LinqToDB.DataProvider.SqlServer.SqlServerProviderAdapter;
using MySqlConnector;
using Microsoft.Extensions.DependencyInjection;
using WorkTabel.Services;
using WorkTabel.View.Windows;
using WorkTabel.ViewModels;

namespace WorkTabel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    // App.xaml.cs
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<MainViewModel>();
            // Зарегистрируйте другие службы и ViewModel здесь
        }

        
    }


}
