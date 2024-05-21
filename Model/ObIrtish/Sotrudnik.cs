using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace WorkTabel.Model.ObIrtish
{
    public class Employee : INotifyPropertyChanged
    {
        private DateTime _enterTime;
        private DateTime _leaveTime;
        private string _attendanceType;

        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public int PositionID { get; set; }
        public int DepartmentID { get; set; }
        public DateTime EnterTime
        {
            get { return _enterTime; }
            set
            {
                _enterTime = value;
                OnPropertyChanged("EnterTime");
            }
        }
        public DateTime ExitTime
        {
            get { return _leaveTime; }
            set
            {
                _leaveTime = value;
                OnPropertyChanged("ExitTime");
            }
        }
        public int WorkedOut { get; set; }
        public string AttendanceType
        {
            get { return _attendanceType; }
            set
            {
                _attendanceType = value;
                OnPropertyChanged("AttendanceType");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
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
