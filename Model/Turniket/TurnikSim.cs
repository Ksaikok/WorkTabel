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
        // Метод для генерации посещений
        public static List<Attendance> GenerateAttendances(int year, int month, Department department, List<AttendanceType> attendanceTypes)
        {
            // 1. Получить список сотрудников из выбранного отдела
            var turEmployeeDataAccess = new EmployeeDataAccess(); // Создайте экземпляр
            var turEmployees = turEmployeeDataAccess.GetEmployees().Where(e => e.DepartmentID == department.DepartmentID).ToList();
            // 2. Создать список посещений
            var attendances = new List<Attendance>();
            foreach (var turEmployee in turEmployees)
            {
                // 3. Генерировать случайные даты для посещений
                var random = new Random();
                var daysInMonth = DateTime.DaysInMonth(year, month);
                for (int i = 1; i <= daysInMonth; i++)
                {
                    // 4. Проверить, нужно ли генерировать посещение для этого дня
                    //    (например, с вероятностью 80% для рабочего дня)
                    if (random.Next(100) < 80)
                    {
                        // 5. Создать посещение
                        var attendance = new Attendance
                        {
                            AttendanceDate = new DateTime(year, month, i),
                            EmployeeID = turEmployee,
                            AttendanceTypeID = attendanceTypes[random.Next(attendanceTypes.Count)],
                            WorkedTime = Enumerable.Range(1, DateTime.DaysInMonth(year, month)).Select(day => (int?)random.Next(300, 600)).ToList()
                        };// Случайное значение от 300 до 600 минут

                        // 6. Генерировать случайное время входа и выхода
                        //    (обратите внимание на ограничения по времени для каждого типа посещения)
                        attendance.TimeIn = new DateTime(year, month, i).Date + new TimeSpan(random.Next(7, 10), random.Next(0, 59), 0);
                        attendance.TimeOut = attendance.TimeIn.Value.Date + new TimeSpan(random.Next(16, 17), random.Next(0, 59), 0);
                        // Рассчитываем отработанное время
                        attendance.WorkedTime.Add(random.Next(300, 600)); // Случайное значение от 300 до 600 минут

                        // 7. Добавить посещение в список
                        attendances.Add(attendance);
                    }
                }
            }

            return attendances;
        }
    }

}
