using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WorkTabel.DataAccessLayer.Data;
using WorkTabel.Model.ObIrtish;
using static WorkTabel.ViewModels.MainViewModel;

namespace WorkTabel.ViewModels.Base
{
    // Базовый класс для всех ViewModels в приложении
    public abstract class ViewModel : INotifyPropertyChanged
    {
        // Событие, которое вызывается при изменении свойства
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод для уведомления о изменении свойства
        // Использует атрибут [CallerMemberName] для автоматического определения имени свойства
        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            // Проверяем, есть ли подписчики на событие PropertyChanged
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        // Метод для установки значения свойства и уведомления о его изменении
        // Использует атрибут [CallerMemberName] для автоматического определения имени свойства
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            // Проверяем, изменилось ли значение
            if (Equals(field, value)) return false;

            // Устанавливаем новое значение
            field = value;

            // Уведомляем о изменении свойства
            OnPropertyChanged(PropertyName);

            // Возвращаем true, если значение изменилось
            return true;
        }

        // Приватное поле для хранения коллекции отделов
        private ObservableCollection<Department> _departments;

        // Публичное свойство для доступа к коллекции отделов
        public ObservableCollection<Department> Departments
        {
            // Получаем значение коллекции
            get => _departments;

            // Устанавливаем значение коллекции, используя метод Set для уведомления о изменении
            set => Set(ref _departments, value);
        }

        // Приватное поле для хранения коллекции сотрудников
        private ObservableCollection<Employee> _employees;

        // Публичное свойство для доступа к коллекции сотрудников
        public ObservableCollection<Employee> Employees
        {
            // Получаем значение коллекции
            get => _employees;

            // Устанавливаем значение коллекции, используя метод Set для уведомления о изменении
            set => Set(ref _employees, value);
        }

        // Приватное поле для хранения коллекции типов посещаемости
        private ObservableCollection<AttendanceType> _attendanceTypes;

        // Публичное свойство для доступа к коллекции типов посещаемости
        public ObservableCollection<AttendanceType> AttendanceTypes
        {
            // Получаем значение коллекции
            get => _attendanceTypes;

            // Устанавливаем значение коллекции, используя метод Set для уведомления о изменении
            set => Set(ref _attendanceTypes, value);
        }

        // Приватное поле для хранения коллекции  посещаемости
        private ObservableCollection<Attendance> _attendances;

        // Публичное свойство для доступа к коллекции  посещаемости
        public ObservableCollection<Attendance> Attendances
        {
            // Получаем значение коллекции
            get => _attendances;

            // Устанавливаем значение коллекции, используя метод Set для уведомления о изменении
            set => Set(ref _attendances, value);
        }

        // Приватное поле для хранения коллекции отделов
        private ObservableCollection<Position> _positions;

        // Публичное свойство для доступа к коллекции отделов
        public ObservableCollection<Position> Positions
        {
            // Получаем значение коллекции
            get => _positions;

            // Устанавливаем значение коллекции, используя метод Set для уведомления о изменении
            set => Set(ref _positions, value);
        }

        //07
        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set => Set(ref _selectedDepartment, value);
        }

        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set => Set(ref _selectedYear, value);
        }

        private int _selectedMonth;
        public int SelectedMonth
        {
            get => _selectedMonth;
            set => Set(ref _selectedMonth, value);
        }
        //-07


        // Приватное поле для хранения отфильтрованной коллекции сотрудников
        private ObservableCollection<Employee> _FilteredEmployeesByDepartment;

        // Публичное свойство для доступа к отфильтрованной коллекции сотрудников
        public ObservableCollection<Employee> FilteredEmployeesByDepartment
        {
            // Получаем значение коллекции
            get => _FilteredEmployeesByDepartment;

            // Устанавливаем значение коллекции, используя метод Set для уведомления о изменении
            set => Set(ref _FilteredEmployeesByDepartment, value);
        }
        // Приватное поле для хранения отфильтрованной коллекции сотрудников
        private ObservableCollection<Employee> _filteredEmployeesAttendancesByDate;

        // Публичное свойство для доступа к отфильтрованной коллекции сотрудников
        public ObservableCollection<Employee> FilteredEmployeesAttendancesByDate
        {
            // Получаем значение коллекции
            get => _filteredEmployeesAttendancesByDate;

            // Устанавливаем значение коллекции, используя метод Set для уведомления о изменении
            set => Set(ref _filteredEmployeesAttendancesByDate, value);
        }

        // Свойство для хранения списка годов
        private ObservableCollection<int> _years;
        public ObservableCollection<int> Years
        {
            get
            {
                if (_years == null)
                {
                    // Создаем список годов, например, начиная с текущего года и назад 10 лет
                    _years = new ObservableCollection<int>(Enumerable.Range(DateTime.Now.Year, 5).Reverse());
                }
                return _years;
            }
            set => Set(ref _years, value);
        }

        // Свойство для хранения списка месяцев (необязательно класс)
        private ObservableCollection<int> _months;
        public ObservableCollection<int> Months { get; set; } = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        

        // Свойство для хранения выбранного посещения
        private Attendance _selectedAttendance;
        public Attendance SelectedAttendance
        {
            get => _selectedAttendance;
            set => Set(ref _selectedAttendance, value);
        }

        private System.Collections.IEnumerable filterEmployeesAttendancesByDate;

        public System.Collections.IEnumerable FilterEmployeesAttendancesByDate { get => filterEmployeesAttendancesByDate; set => Set(ref filterEmployeesAttendancesByDate, value); }

        private ObservableCollection<string> _columns;
        public ObservableCollection<string> Columns
        {
            get => _columns;
            set => Set(ref _columns, value);
        }



    }
}