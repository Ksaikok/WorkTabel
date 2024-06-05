using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WorkTabel.Model.Data;
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
        private ObservableCollection<Attendance> _attendance;

        // Публичное свойство для доступа к коллекции  посещаемости
        public ObservableCollection<Attendance> Attendance
        {
            // Получаем значение коллекции
            get => _attendance;

            // Устанавливаем значение коллекции, используя метод Set для уведомления о изменении
            set => Set(ref _attendance, value);
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
        private ObservableCollection<KeyValuePair<int, string>> _months;
        public ObservableCollection<KeyValuePair<int, string>> Months
        {
            get
            {
                if (_months == null)
                {
                    _months = new ObservableCollection<KeyValuePair<int, string>>
            {
                        new KeyValuePair<int, string>(1, "Январь"),
                        new KeyValuePair<int, string>(2, "Февраль"),
                        new KeyValuePair<int, string>(3, "Март"),
                        new KeyValuePair<int, string>(4, "Апрель"),
                        new KeyValuePair<int, string>(5, "Май"),
                        new KeyValuePair<int, string>(6, "Июнь"),
                        new KeyValuePair<int, string>(7, "Июль"),
                        new KeyValuePair<int, string>(8, "Август"),
                        new KeyValuePair<int, string>(9, "Сентябрь"),
                        new KeyValuePair<int, string>(10, "Октябрь"),
                        new KeyValuePair<int, string>(11, "Ноябрь"),
                        new KeyValuePair<int, string>(12, "Декабрь")
            };
                }
                return _months;
            }
            set => Set(ref _months, value);
        }

        // Свойство для хранения выбранного посещения
        private Attendance _selectedAttendance;
        public Attendance SelectedAttendance
        {
            get => _selectedAttendance;
            set => Set(ref _selectedAttendance, value);
        }

        private System.Collections.IEnumerable filterEmployeesAttendancesByDate;

        public System.Collections.IEnumerable FilterEmployeesAttendancesByDate { get => filterEmployeesAttendancesByDate; set => Set(ref filterEmployeesAttendancesByDate, value); }



    }
}