using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTabel.Model.Data;
using WorkTabel.Model.ObIrtish;
using static WorkTabel.Model.Data.DataAccess;

namespace WorkTabel.Model.Turniket
{
    public class TurnikSim
    {
        private Random _random = new Random();

        public List<Attendance> GenerateAttendances(int departmentID, int year, int month, List<Employee> employees)
        {
            var attendances = new List<Attendance>();
            int daysInMonth = DateTime.DaysInMonth(year, month);

            foreach (var employee in employees)
            {
                for (int day = 1; day <= daysInMonth; day++)
                {
                    var attendanceDate = new DateTime(year, month, day);
                    var attendanceTypeID = GenerateAttendanceType();
                    var timeIn = GenerateTimeIn();
                    var timeOut = GenerateTimeOut(timeIn);

                    attendances.Add(new Attendance
                    {
                        AttendanceDate = attendanceDate,
                        TimeIn = timeIn,
                        TimeOut = timeOut,
                        WorkedOut = (int)(timeOut - timeIn).TotalHours,
                        EmployeeID = employee.EmployeeID,
                        AttendanceTypeID = attendanceTypeID
                    });
                }
            }

            return attendances;
        }

        private int GenerateAttendanceType()
        {
            // Example: 1 - Явка, 2 - Неявка, 3 - Больничные и т.д.
            return _random.Next(1, 4);
        }

        private DateTime GenerateTimeIn()
        {
            // Example: employees arrive between 8:00 and 10:00
            return DateTime.Today.AddHours(8).AddMinutes(_random.Next(0, 120));
        }

        private DateTime GenerateTimeOut(DateTime timeIn)
        {
            // Example: employees leave between 16:00 and 18:00
            return timeIn.AddHours(8).AddMinutes(_random.Next(0, 120));
        }
    }

}
