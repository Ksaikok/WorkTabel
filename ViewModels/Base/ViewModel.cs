using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WorkTabel.Model.ObIrtish;

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

        // Приватное поле для хранения отфильтрованной коллекции сотрудников
        private ObservableCollection<Employee> _filteredEmployees;

        // Публичное свойство для доступа к отфильтрованной коллекции сотрудников
        public ObservableCollection<Employee> FilteredEmployees
        {
            // Получаем значение коллекции
            get => _filteredEmployees;

            // Устанавливаем значение коллекции, используя метод Set для уведомления о изменении
            set => Set(ref _filteredEmployees, value);
        }
    }
}