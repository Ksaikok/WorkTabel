using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Configuration;

namespace WorkTabel.Model.ObIrtish
{
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
        public void OnPropertyChanged([CallerMemberName] string prop = "Sotrudnik PropertyChanged")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class Department : INotifyPropertyChanged
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        //public ICollection<Department> Departments { get; set; }
    }

}
