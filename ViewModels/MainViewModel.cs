using System.Data;
using System.Collections.ObjectModel;
using WorkTabel.Model.ObIrtish;
using static WorkTabel.Model.Data.DataAccess;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WorkTabel.ViewModels.Base;
using System.Windows;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using WorkTabel.View.Windows;
using LinqToDB;
using System.Windows.Controls;
using static MaterialDesignThemes.Wpf.Theme;
using System.Globalization;
using System.Windows.Data;
using WorkTabel.Model.Data;
using WorkTabel.Model.Turniket;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace WorkTabel.ViewModels
{
    // Главная ViewModel для приложения
    public class MainViewModel : ViewModel
    {

        // Конструктор MainViewModel
        public MainViewModel()
        {
            // Загружаем данные из базы данных
            _employeeDataAccess = new EmployeeDataAccess(); //от насти!!! 05
            Departments = new ObservableCollection<Department>(new DepartmentDataAccess().GetDepartments());
            Positions = new ObservableCollection<Position>(new PositionDataAccess().GetPositions());
            Employees = new ObservableCollection<Employee>(LoadEmployeesWithDepartPositions());
            AttendanceTypes = new ObservableCollection<AttendanceType>(new AttendanceTypeDataAccess().GetAttendanceTypes());
            //Attendances = new ObservableCollection<Attendance>(new AttendanceDataAccess().GetAttendances());
            Years = new ObservableCollection<int>(Enumerable.Range(2000, DateTime.Now.Year - 1999));
            Months = new ObservableCollection<int>(Enumerable.Range(1, 12));


            // Инициализируем отфильтрованную коллекцию сотрудников
            FilteredEmployeesByDepartment = new ObservableCollection<Employee>(Employees);
            // инициализация выбранного сотрудника
            FilteredEmployee = new ObservableCollection<Employee>();
            FilteredEmployeesAttendancesByDate = new ObservableCollection<Employee>();
            // Инициализация столбцов
            GenerateColumns();
            // Покажите окно авторизации
            ShowAuthorizationWindow();

            LoadAttendanceCommand = new RelayCommand(LoadAttendance);
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
        public ObservableCollection<Position> Positions { get; set; }
        // Коллекция сотрудников
        public ObservableCollection<Employee> Employees { get; set; }
        // Коллекция типов посещаемости
        public ObservableCollection<AttendanceType> AttendanceTypes { get; set; }
        // Коллекция записей о посещаемости
        public ObservableCollection<Attendance> Attendances { get; set; }


        //----------------------------------------------------------------
        private readonly EmployeeDataAccess _employeeDataAccess; //05


        // Метод для загрузки сотрудников и установки названия отдела
        private IEnumerable<Employee> LoadEmployeesWithDepartPositions()
        {
            var employeeDataAccess = new EmployeeDataAccess();
            var departmentDataAccess = new DepartmentDataAccess();
            var positionDataAccess = new PositionDataAccess();

            var employees = employeeDataAccess.GetEmployees();
            var departments = departmentDataAccess.GetDepartments();
            var positions = positionDataAccess.GetPositions();

            // Создаем словарь для быстрого поиска названий отделов по их ID
            var departmentDict = departments.ToDictionary(d => d.DepartmentID, d => d.DepartmentName);
            var positionDict = positions.ToDictionary(d => d.PositionID, d => d.PositionName);

            // Обновляем название отдела для каждого сотрудника
            foreach (var employee in employees)
            {
                if (departmentDict.TryGetValue(employee.DepartmentID.DepartmentID, out var departmentName))
                {
                    employee.DepartmentID.DepartmentName = departmentName;
                }
                if (positionDict.TryGetValue(employee.PositionID.PositionID, out var positionName))
                {
                    employee.PositionID.PositionName = positionName;
                }
            }

            return employees;
        }


        //05 от насти!!!
        // Свойство для строки поиска
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (Set(ref _searchText, value))
                {
                    FilterEmployeesByDepartment(); // Обновляем фильтр при изменении строки поиска
                }
            }
        }
        public ICommand LoadDataCommand { get; }
        public ICommand SaveCommand => new RelayCommand(SaveChanges);

        private void SaveChanges()
        {
            foreach (var employee in Employees)
            {
                _employeeDataAccess.UpdateEmployee(employee);
            }
        }

        //06
        private void GenerateColumns()
        {
            if (SelectedYear > 0 && SelectedMonth > 0)
            {
                var daysInMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);
                Columns = new ObservableCollection<string>();

                for (int day = 1; day <= daysInMonth; day++)
                {
                    Columns.Add(day.ToString());
                }

                // Пересчитать данные после изменения столбцов
                FilterEmployeesAttendancesByDate();
            }
        }

        //-06

        // Свойство для хранения выбранного месяца
        private int _selectedMonth = DateTime.Now.Month;
        public int SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                Set(ref _selectedMonth, value);
                GenerateColumns();
            }
        }

        private int _selectedYear = DateTime.Now.Year;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                Set(ref _selectedYear, value);
                GenerateColumns();
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
                _selectedYearMonth = value;//new DateTime(Int32.Parse(SelectedYear), Int32.Parse(SelectedMonth), 1);
                FilterEmployeesAttendancesByDate();
            }
        }

        private void CalYearMonth()
        {
            // Проверьте, что SelectedYear и SelectedMonth не равны null
            if (SelectedYear != null && SelectedMonth != null)
            {
                _selectedYearMonth = new DateTime(SelectedYear, SelectedMonth, 1);
                FilterEmployeesAttendancesByDate();
                OnPropertyChanged(nameof(SelectedYearMonth)); // Уведомить о изменении свойства
            }
        }


        private void FilterEmployeesAttendancesByDate()
        {
            FilterEmployeesByDepartment();

            FilteredEmployeesAttendancesByDate = new ObservableCollection<Employee>();

            foreach (Employee employee in FilteredEmployeesByDepartment)
            {
                if (employee.Attendances != null)
                {
                    ObservableCollection<Attendance> AttendancesByDate = new ObservableCollection<Attendance>(
                       employee.Attendances.Where(e => e.AttendanceDate.Year == SelectedYear
                                                       && e.AttendanceDate.Month == SelectedMonth)
                    );

                    employee.Attendances = AttendancesByDate;
                    FilteredEmployeesAttendancesByDate.Add(employee);
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



        private void FilterEmployeesByDepartment()
        {
            if (SelectedDepartment == null && string.IsNullOrWhiteSpace(SearchText))
            {
                // Если отдел не выбран и строка поиска пуста, показываем всех сотрудников
                FilteredEmployeesByDepartment = new ObservableCollection<Employee>(Employees);
            }
            else
            {
                // Если отдел выбран или введена строка поиска, фильтруем сотрудников
                var filtered = Employees.AsEnumerable();

                if (SelectedDepartment != null)
                {
                    filtered = filtered.Where(e => e.DepartmentID.DepartmentID == SelectedDepartment.DepartmentID);
                }

                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    filtered = filtered.Where(e => e.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
                }

                FilteredEmployeesByDepartment = new ObservableCollection<Employee>(filtered);
            }
        }

       
        

        //07
        // команда для загрузки данных
        public RelayCommand LoadAttendanceCommand { get; }

        private readonly AttendanceDataAccess _attendanceDataAccess = new AttendanceDataAccess();
        private void LoadAttendance()
        {
            if (SelectedDepartment != null && SelectedYear != 0 && SelectedMonth != 0)
            {
                Attendances = _attendanceDataAccess.GetAttendances(SelectedDepartment.DepartmentID, SelectedYear, SelectedMonth);

                foreach (var employee in Employees)
                {
                    employee.Attendances = new ObservableCollection<Attendance>(Attendances.Where(a => a.EmployeeID.EmployeeID == employee.EmployeeID));
                }

                // Вызов метода для создания динамических столбцов
                GenerateDynamicColumns();
            }
        }
        private void GenerateDynamicColumns()
        {
            var dataGrid = Application.Current.MainWindow.FindName("AttendanceDataGrid") as System.Windows.Controls.DataGrid;

            if (dataGrid == null) return;

            dataGrid.Columns.Clear();

            dataGrid.Columns.Add(new DataGridTextColumn { Header = "ID", Binding = new Binding("EmployeeID"), Width = 50 });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "ФИО", Binding = new Binding("FullName"), Width = 250 });

            int daysInMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);

            for (int day = 1; day <= daysInMonth; day++)
            {
                var templateColumn = new DataGridTemplateColumn
                {
                    Header = day.ToString(),
                    CellTemplate = new DataTemplate
                    {
                        VisualTree = new FrameworkElementFactory(typeof(TextBlock))
                    }
                };

                templateColumn.CellTemplate.VisualTree.SetBinding(TextBlock.TextProperty, new Binding($"Attendances[{day - 1}].DisplayWorkedOut"));

                dataGrid.Columns.Add(templateColumn);
            }

            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Итого", Binding = new Binding("TotalWorkedOutTime"), Width = 100 });
        }
        //-07


        //  Новый метод для обновления отфильтрованных сотрудников
        private void UpdateFilteredEmployees()
        {
            if (SelectedDepartment == null || SelectedYear == null || SelectedMonth == null)
            {
                FilteredEmployeesByDepartment = new ObservableCollection<Employee>(Employees);
            }
            else
            {
                FilteredEmployeesByDepartment = new ObservableCollection<Employee>(Employees.Where(e => 
                e.DepartmentID.DepartmentID == SelectedDepartment.DepartmentID
                ));
                //  фильтруйте по году и месяцу, если нужно
                FilteredEmployeesByDepartment = new ObservableCollection<Employee>(FilteredEmployeesByDepartment.Where(e => 
                e.Attendances.Any(a => a.AttendanceDate.Year == SelectedYear && a.AttendanceDate.Month == SelectedMonth)
                ));
            }
        }



        ////27

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


