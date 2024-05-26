using System.Data;
using System.Collections.ObjectModel;
using WorkTabel.Model.ObIrtish;
using static WorkTabel.Model.Data.DataAccess;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WorkTabel.ViewModels.Base;

namespace WorkTabel.ViewModels
{
    // Главная ViewModel для приложения
    public class MainViewModel : ViewModel
    {
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

        // Конструктор MainViewModel
        public MainViewModel()
        {
            // Загружаем данные из базы данных
            Departments = new ObservableCollection<Department>(new DepartmentDataAccess().GetDepartments());
            Employees = new ObservableCollection<Employee>(new EmployeeDataAccess().GetEmployees());
            AttendanceTypes = new ObservableCollection<AttendanceType>(new AttendanceTypeDataAccess().GetAttendanceTypes());
            Attendances = new ObservableCollection<Attendance>(new AttendanceDataAccess().GetAttendances());

            // Инициализируем отфильтрованную коллекцию сотрудников
            FilteredEmployees = new ObservableCollection<Employee>(Employees);
        }

        // Свойство для хранения отфильтрованной коллекции сотрудников
        private ObservableCollection<Employee> _filteredEmployees;
        public ObservableCollection<Employee> FilteredEmployees
        {
            get => _filteredEmployees;
            set => Set(ref _filteredEmployees, value);
        }

        // Метод фильтрации сотрудников по выбранному отделу
        private void FilterEmployeesByDepartment()
        {
            // Проверяем, выбран ли отдел
            if (SelectedDepartment == null)
            {
                // Если отдел не выбран, показываем всех сотрудников
                FilteredEmployees = new ObservableCollection<Employee>(Employees);
            }
            else
            {
                // Если отдел выбран, фильтруем сотрудников по выбранному отделу
                FilteredEmployees = new ObservableCollection<Employee>(
                    // Фильтруем коллекцию Employees, оставляя сотрудников, 
                    // чей DepartmentID совпадает с DepartmentID выбранного отдела
                    Employees.Where(e => e.DepartmentID == SelectedDepartment.DepartmentID)
                );
            }
        }
    }
}

