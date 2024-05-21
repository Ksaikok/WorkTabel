using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace WorkTabel.Model.ObIrtish
{
    internal class Employee : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private DateTime _enterTime;
        private DateTime _leaveTime;
        private readonly int _workedout;
        private string _attendanceType;


        public int EmployeeID
        {
            get { return _id; }
            set 
            {
                _id = value;
                OnPropertyChanged("EmployeeID");
            }
        }
        public string FullName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("FullName");
            }
        }
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
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        //public ICollection<Department> Departments { get; set; }
    }

}
