using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Configuration;
using WorkTabel.ViewModels.Base;

namespace WorkTabel.Model.ObIrtish
{
    public class AttendanceType : INotifyPropertyChanged
    {
        public int AttendanceTypeID {get; set;}
        public string Abbreviation { get; set;}
        public string Definition { get; set;}

        public event PropertyChangedEventHandler? PropertyChanged;
    }
    // класс сотрудника с его атрибутами
    public class Employee : INotifyPropertyChanged 
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public int PositionID { get; set; }
        public int DepartmentID { get; set; }

        //код ниже будет представлен лругим классом
        //public DateTime EnterTime { get; set; }
        //public DateTime ExitTime { get; set; }
        //public int WorkedOut { get; set; }
        //public string AttendanceType { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        
    }
    //класс отдела с атрибутами
    public class Department : INotifyPropertyChanged
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        

       

        public event PropertyChangedEventHandler? PropertyChanged;
        //public ICollection<Department> Departments { get; set; }

        public void OnPropertyChanged([CallerMemberName] string prop = "Sotrudnik PropertyChanged")
        {
             if (PropertyChanged != null)
             PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class AttendanceEntry
    {
        public int EntryID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string AttendanceType { get; set; }
        public TimeSpan? WorkedTime
        {
            get
            {
                if (ArrivalTime.HasValue && DepartureTime.HasValue)
                    return DepartureTime.Value - ArrivalTime.Value;
                else
                    return null;
            }
        }
    }






}
