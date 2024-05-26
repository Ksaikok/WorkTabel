using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkTabel.Model.ObIrtish
{
    //прописаны основные классы и их свойства
    public class AttendanceType : INotifyPropertyChanged
    {
        public int AttendanceTypeID {get; set;}
        public string Abbreviation { get; set;}
        public string Definition { get; set;}
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
    // класс сотрудника с его атрибутами
    public class Employee : INotifyPropertyChanged 
    {
        public int EmployeeID { get; set; }
        public string? FullName { get; set; }
        public int TabelNum { get; set; }
        public int PositionID { get; set; }
        public int DepartmentID { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        // Метод для уведомления об изменении свойств
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    // посещения сотрудником работы с расчетом отработанных часов
    public class Attendance
    {
        public int AttendanceID { get; set; }
        public DateTime AttendanceDate { get; set; }        
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public Employee EmployeeID { get; set; }
        public AttendanceType? AttendanceTypeID { get; set; }
        public TimeSpan? WorkedTime
        {
            get
            {
                if (TimeIn.HasValue && TimeOut.HasValue)
                    return TimeOut.Value - TimeIn.Value;
                else
                    return null;
            }
        }
    }


}
