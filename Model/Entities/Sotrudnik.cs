using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkTabel.Model.ObIrtish
{
    //прописаны основные классы и их свойства
    public class AttendanceType : INotifyPropertyChanged
    {
        public int AttendanceTypeID { get; set; }
        public string Abbreviation { get; set; }
        public string Definition { get; set; }
        public Collection<Attendance> Attendances { get; set; }
        public string TypeName { get; internal set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    //класс отдела с атрибутами
    public class Department : INotifyPropertyChanged
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "Sotrudnik PropertyChanged")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class Position : INotifyPropertyChanged
    {
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "Sotrudnik PropertyChanged")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    // класс сотрудника с его свойствами
    public class Employee : INotifyPropertyChanged
    {
        public int EmployeeID { get; set; }
        public string? FullName { get; set; }
        public int TabelNum { get; set; }
        public Position PositionID { get; set; }
        public Department DepartmentID { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        //public string PositionName {  get; set; }
        public Collection<Attendance> Attendances { get; set; }
        public Collection<Department> Departments { get; set; }
        public Collection<Position> Positions { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        //09
        private TimeSpan totalWorkedOutTime;
        public TimeSpan TotalWorkedOutTime
        {
            get => totalWorkedOutTime;
            private set
            {
                if (totalWorkedOutTime != value)
                {
                    totalWorkedOutTime = value;
                    OnPropertyChanged(nameof(TotalWorkedOutTime));
                }
            }
        }
        private void RecalculateTotalWorkedOutTime()
        {
            TotalWorkedOutTime = new TimeSpan(Attendances.Sum(a => TimeSpan.FromHours(a.WorkedOut).Hours));
        }

        private void SubscribeToAttendanceChanges()
        {
            foreach (var attendance in Attendances)
            {
                attendance.PropertyChanged += Attendance_PropertyChanged;
            }
        }

        private void Attendance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Attendance.WorkedOut))
            {
                RecalculateTotalWorkedOutTime();
            }
        }
        // Метод для уведомления об изменении свойств
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        
        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }
    }


    // посещения сотрудником работы с расчетом отработанных часов
    public class Attendance : INotifyPropertyChanged
    {
        public int AttendanceID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public Employee EmployeeID { get; set; }
        public AttendanceType? AttendanceTypeID { get; set; }
        public AttendanceType? AttendanceType { get; set; }
        public List<int?> WorkedTime { get; set; } = new List<int?>();
        private double workedOut;
        public double WorkedOut
            {
        get => workedOut;
        set
        {
            if (workedOut != value)
            {
                workedOut = value;
                OnPropertyChanged(nameof(WorkedOut));
                OnPropertyChanged(nameof(DisplayWorkedOut));
            }
        }
    }
        

        public string DisplayWorkedOut => $"{WorkedOut} {AttendanceType?.Abbreviation ?? "\nЯ"}";

        public event PropertyChangedEventHandler? PropertyChanged;
        // Метод для уведомления о изменениях свойств
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



    //27
    //public class AuthorizationModel : INotifyPropertyChanged
    //{
    //    private string _userName;
    //    public string UserName
    //    {
    //        get => _userName;
    //        set => Set(ref _userName, value);
    //    }

    //    private string _password;
    //    public string Password
    //    {
    //        get => _password;
    //        set => Set(ref _password, value);
    //    }

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }

    //    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    //    {
    //        if (Equals(field, value)) return false;
    //        field = value;
    //        OnPropertyChanged(propertyName);
    //        return true;
    //    }
    //}


