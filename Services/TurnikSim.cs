using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTabel.Model.ObIrtish;
using static WorkTabel.DataAccessLayer.Data.DataAccess;

namespace WorkTabel.Services
{
    public class TurnikSim
    {
        private Random _random = new Random();
        public List<Attendance> GenerateAttendances(int departmentID, int year, int month, List<Employee> employees)
        {
            var attendances = new List<Attendance>();
            int daysInMonth = DateTime.DaysInMonth(year, month);
            foreach (var employee in employees){
                for (int day = 1; day <= daysInMonth; day++)
                {
                    var attendanceDate = new DateTime(year, month, day);
                    var attendanceType = GenerateAttendanceType();
                    var timeIn = GenerateTimeIn();
                    var timeOut = GenerateTimeOut(timeIn);
                    attendances.Add(new Attendance
                    {
                        AttendanceDate = attendanceDate,
                        TimeIn = timeIn,
                        TimeOut = timeOut,
                        WorkedOut = (int)(timeOut - timeIn).TotalHours,
                        EmployeeID = employee,
                        AttendanceTypeID = attendanceType 
                    });                    
                }
            }
            return attendances;
        }
        private AttendanceType GenerateAttendanceType()
        {
            var types = new List<AttendanceType>
        {
            new AttendanceType { AttendanceTypeID = 1, TypeName = "Явка", Abbreviation = "Я" },
            new AttendanceType { AttendanceTypeID = 2, TypeName = "Неявка (невыясненные обстоятельства)", Abbreviation = "НН" },
            new AttendanceType { AttendanceTypeID = 3, TypeName = "Больничные", Abbreviation = "Б" }
        };
            int index = _random.Next(types.Count);
            return types[index];
        }
        private DateTime GenerateTimeIn()
        {
            return DateTime.Today.AddHours(8).AddMinutes(_random.Next(0, 59));
        }
        private DateTime GenerateTimeOut(DateTime timeIn)
        {
            return timeIn.AddHours(8).AddMinutes(_random.Next(0, 59));
        }
    }


}
