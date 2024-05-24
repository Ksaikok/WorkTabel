using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTabel.ViewModels
{
    //class TurniketViewModel
    //{
    //    private readonly Random _random = new Random();
    //    private readonly List<int> _employeeIDs; // Список ID сотрудников

    //    public ObservableCollection<AttendanceEntry> AttendanceLog { get; } = new ObservableCollection<AttendanceEntry>();

    //    // Конструктор ViewModel
    //    public TurniketViewModel(List<int> employeeIDs)
    //    {
    //        _employeeIDs = employeeIDs;
    //    }

    //    // Метод для симуляции дня
    //    public void SimulateDay(DateTime date)
    //    {
    //        AttendanceLog.Clear();
    //        foreach (var employeeID in _employeeIDs)
    //        {
    //            if (_random.NextDouble() < 0.8) // 80% вероятность явки
    //            {
    //                var arrivalTime = new DateTime(date.Year, date.Month, date.Day,
    //                                               _random.Next(8, 10), _random.Next(0, 60), 0); // Приход с 8 до 10
    //                var departureTime = new DateTime(date.Year, date.Month, date.Day,
    //                                                 _random.Next(17, 20), _random.Next(0, 60), 0); // Уход с 17 до 20

    //                AttendanceLog.Add(new AttendanceEntry
    //                {
    //                    EntryID = AttendanceLog.Count + 1,
    //                    EmployeeID = employeeID,
    //                    Date = date,
    //                    ArrivalTime = arrivalTime,
    //                    DepartureTime = departureTime,
    //                    AttendanceType = "Я" // Явка
    //                });
    //            }
    //        }
    //    }
    //}
}
