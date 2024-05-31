using System.Data;
using System.Collections.ObjectModel;
using WorkTabel.Model.ObIrtish;
using static WorkTabel.Model.Data.DataAccess;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WorkTabel.ViewModels.Base;
using System.Windows;
using GalaSoft.MvvmLight.CommandWpf;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using WorkTabel.View.Windows;
using LinqToDB;
using System.Windows.Controls;
using static MaterialDesignThemes.Wpf.Theme;
using System.Globalization;
using System.Windows.Data;

namespace WorkTabel.ViewModels
{
    // Главная ViewModel для приложения
    public class MainViewModel : ViewModel
    {

        // Конструктор MainViewModel
        public MainViewModel()
        {
            // Загружаем данные из базы данных
            Departments = new ObservableCollection<Department>(new DepartmentDataAccess().GetDepartments());
            Employees = new ObservableCollection<Employee>(new EmployeeDataAccess().GetEmployees());
            AttendanceTypes = new ObservableCollection<AttendanceType>(new AttendanceTypeDataAccess().GetAttendanceTypes());
            Attendances = new ObservableCollection<Attendance>(new AttendanceDataAccess().GetAttendances());

            // Инициализируем отфильтрованную коллекцию сотрудников
            FilteredEmployeesByDepartment = new ObservableCollection<Employee>(Employees);
            // инициализация выбранного сотрудника
            FilteredEmployee = new ObservableCollection<Employee>();
            FilteredEmployeesAttendancesByDate = new ObservableCollection<Employee>();


            // Покажите окно авторизации
            ShowAuthorizationWindow();
        }

        // Событие PropertyChanged, наследуемое от ViewModel
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод для уведомления об изменении свойств, наследуемый от ViewModel
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // Свойства для хранения данных
        // Коллекция отделов
        public ObservableCollection<Department> Departments { get; set; }
        // Коллекция сотрудников
        public ObservableCollection<Employee> Employees { get; set; }
        // Коллекция типов посещаемости
        public ObservableCollection<AttendanceType> AttendanceTypes { get; set; }
        // Коллекция записей о посещаемости
        public ObservableCollection<Attendance> Attendances { get; set; }

        //----------------------------------------------------------------

        // Свойство для хранения выбранного отдела
        private string _selectedMonth;
        public string SelectedMonth
        {
            // Получаем значение выбранного отдела
            get => _selectedMonth;

            // Устанавливаем значение выбранного отдела, используя метод Set для уведомления об изменении
            set
            {
                Set(ref _selectedMonth, value); // Используем метод Set из ViewModelBase
                OnPropertyChanged("SelectedYearMonth");

            }
        }

        private string _selectedYear;
        public string SelectedYear
        {
            // Получаем значение выбранного отдела
            get => _selectedYear;

            // Устанавливаем значение выбранного отдела, используя метод Set для уведомления об изменении
            set
            {
                Set(ref _selectedYear, value); // Используем метод Set из ViewModelBase
                OnPropertyChanged("SelectedYearMonth");
                CalYearMonth();


            }
        }

        private DateTime _selectedYearMonth;
        public DateTime SelectedYearMonth
        {
            // Получаем значение выбранного отдела
            get => _selectedYearMonth;

            // Устанавливаем значение выбранного отдела, используя метод Set для уведомления об изменении
            set
            {
                _selectedYearMonth = new DateTime(Int32.Parse(SelectedYear), Int32.Parse(SelectedMonth), 1);
                FilterEmployeesAttendancesByDate();
            }
        }
        private void CalYearMonth()
        {
            _selectedYearMonth = new DateTime(Int32.Parse(SelectedYear), Int32.Parse(SelectedMonth), 1);
            FilterEmployeesAttendancesByDate();
        }

        private void FilterEmployeesAttendancesByDate()
        {
            FilterEmployeesByDepartment();
            // Проверяем, выбран ли отдел


            FilteredEmployeesAttendancesByDate = new ObservableCollection<Employee>();
            foreach (Employee employee in FilteredEmployeesByDepartment)
            {

                if (employee.Attendances != null)
                {
                    ObservableCollection<Attendance> AttendancesByDate = new ObservableCollection<Attendance>(
                       // Фильтруем коллекцию Employees, оставляя сотрудников, 
                       // чей DepartmentID совпадает с DepartmentID выбранного отдела
                       employee.Attendances.Where(e => e.AttendanceDate.Month == SelectedYearMonth.Month

                   ));
                    employee.Attendances = AttendancesByDate;
                    FilteredEmployeesAttendancesByDate.Add(employee);
                    string str = " ";
                    foreach (Attendance attendance in AttendancesByDate)
                    {
                        str += attendance.AttendanceDate;
                        str += "\n";
                    }
                    MessageBox.Show(str);

                }

            }
        }

        // конвертирпование DateTime в Date
        public class DateTimeToDateConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                Collection<Attendance> attendances = (Collection<Attendance>)value;
                string str = " ";
                foreach (Attendance attendance in attendances)
                {
                    str += attendance.AttendanceDate;
                    str += "\n";
                }

                return str;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return DependencyProperty.UnsetValue;
            }
        }

        //31
        public class DynamicDayColumnGenerator : DataTemplateSelector
        {
            public int Year { get; set; }
            public int Month { get; set; }

            public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                var dataGrid = container as System.Windows.Controls.DataGrid;

                // Генерируем столбцы для каждого дня месяца
                var days = Enumerable.Range(1, DateTime.DaysInMonth(Year, Month));
                foreach (var day in days)
                {
                    var header = new DateTime(Year, Month, day).ToString("dd");
                    var column = new DataGridTextColumn
                    {
                        Header = header,
                        Binding = new Binding($"WorkedTime[{day - 1}]") { StringFormat = @"hh\:mm" }, // Доступ к WorkedTime по индексу дня
                        Width = DataGridLength.Auto,
                        IsReadOnly = false, // Можно сделать редактируемым
                    };
                    dataGrid.Columns.Add(column);
                }

                return null; //  Возвращаем null, так как шаблон не используется
            }
        }
        //--31



        // Свойство для хранения отфильтрованной коллекции сотрудников
        private ObservableCollection<Employee> _filteredEmployee;
        public ObservableCollection<Employee> FilteredEmployee
        {
            get => _filteredEmployee;
            set => Set(ref _filteredEmployee, value);
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set {
                Set(ref _selectedEmployee, value);
                FilterSelectedEmployee();
                }
        }
        // Метод фильтрации сотрудников по выбранному отделу
        private void FilterSelectedEmployee()
        {
            // Проверяем, выбран ли отдел
            if (SelectedEmployee == null)
            {
                //// Если отдел не выбран, показываем всех сотрудников
                //SelectedEmployee = new ObservableCollection<Employee>(Employees);
            }
            else
            {
                // Если отдел выбран, фильтруем сотрудников по выбранному отделу
                FilteredEmployee = new ObservableCollection<Employee>(
                    // Фильтруем коллекцию Employees, оставляя сотрудников, 
                    // чей DepartmentID совпадает с DepartmentID выбранного отдела
                    Employees.Where(e => e.EmployeeID == SelectedEmployee.EmployeeID)
                );
            }
        }


        //________________________________________________________________
        // Свойство для хранения выбранного отдела
        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            // Получаем значение выбранного отдела
            get => _selectedDepartment;

            // Устанавливаем значение выбранного отдела, используя метод Set для уведомления об изменении
            set
            {
                Set(ref _selectedDepartment, value); // Используем метод Set из ViewModelBase
                // Вызываем метод фильтрации сотрудников при изменении выбранного отдела
                FilterEmployeesByDepartment();
            }
        }

        // Свойство для хранения отфильтрованной коллекции сотрудников
        private ObservableCollection<Employee> _FilteredEmployeesByDepartment;
        public ObservableCollection<Employee> FilteredEmployeesByDepartment
        {
            get => _FilteredEmployeesByDepartment;
            set => Set(ref _FilteredEmployeesByDepartment, value);
        }



        // Метод фильтрации сотрудников по выбранному отделу
        private void FilterEmployeesByDepartment()
        {
            // Проверяем, выбран ли отдел
            if (SelectedDepartment == null)
            {
                // Если отдел не выбран, показываем всех сотрудников
                FilteredEmployeesByDepartment = new ObservableCollection<Employee>(Employees);
            }
            else
            {
                // Если отдел выбран, фильтруем сотрудников по выбранному отделу
                FilteredEmployeesByDepartment = new ObservableCollection<Employee>(
                    // Фильтруем коллекцию Employees, оставляя сотрудников, 
                    // чей DepartmentID совпадает с DepartmentID выбранного отдела
                    Employees.Where(e => e.DepartmentID == SelectedDepartment.DepartmentID)
                );
            }
        }

        

        //27

        // Метод для показа окна авторизации
        private void ShowAuthorizationWindow()
        {
            // Создайте экземпляр AuthorizationViewModel
            var authorizationViewModel = new AuthorizationViewModel();

            // Подпишитесь на событие OnLoginSuccess
            authorizationViewModel.OnLoginSuccess += OnLoginSuccess;

            // Создайте окно авторизации
            var authorizationWindow = new AuthorizationWindow() { DataContext = authorizationViewModel };

            // Покажите окно
            authorizationWindow.ShowDialog();
        }
        // Метод, который вызывается при успешной авторизации
        private void OnLoginSuccess()
        {
            //// Проверьте, была ли выбрана роль гостя
            //if (AuthorizationViewModel.IsGuestMode)
            //{
            //    // Если да, то установите IsAuthenticated в true и выполните действия для режима "Гость"
            //    IsAuthenticated = true;
            //    // ... (Логика для режима "Гость", например, ограничение доступа к некоторым функциям)
            //}
            //else
            //{
            //    // Если нет, то установите IsAuthenticated в true и выполните действия для авторизованного пользователя
            //    IsAuthenticated = true;
            //    // ... (Логика для авторизованного пользователя, например, предоставление полного доступа к функциям)
            //}
        }

        public class AuthorizationModel : INotifyPropertyChanged
        {
            private string _userName;
            public string UserName
            {
                get => _userName;
                set => Set(ref _userName, value);
            }

            private string _password;
            public string Password
            {
                get => _password;
                set => Set(ref _password, value);
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
            {
                if (Equals(field, value)) return false;
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }


        }
    }
}


