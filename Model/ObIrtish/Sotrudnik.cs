using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTabel.Model.ObIrtish
{
    internal class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public DateTime EnterTime { get; set; }
        public DateTime ExitTime { get; set; }
        public int WorkedOut {  get; set; }
        public string AttendanceType { get; set; } = "П";
    }
    internal class Department
    {
        public int DepartmentID {  get; set; }
        public string DepartmentName { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }

}
