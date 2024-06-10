using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTabel.Model.ObIrtish;

namespace WorkTabel.Services
{
    // IReportService.cs
    public interface IReportService
    {
        void GenerateAttendanceReport(string templatePath, string outputPath, ObservableCollection<Employee> employees, int year, int month);
    }

}
