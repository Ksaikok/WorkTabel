// ReportService.cs
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WorkTabel.Model.ObIrtish;
using WorkTabel.Services;
using Xceed.Document.NET;
using Xceed.Words.NET;

public class ReportService : IReportService
{
    [Obsolete]
    public void GenerateAttendanceReport(string templatePath, string outputPath, ObservableCollection<Employee> employees, int year, int month)
    {
        using (var document = DocX.Load(templatePath))
        {
            var periodStart = new DateTime(year, month, 1);
            var periodEnd = periodStart.AddMonths(1).AddDays(-1);
            // Replace basic placeholders
            document.ReplaceText("#Организация#", "ФБУ «Администрация Обь-Иртышского бассейна»");
            document.ReplaceText("#ОКПО#", "12345678");
            document.ReplaceText("#Отдел#", "Отдел договорных отн. и орг. торгов");
            document.ReplaceText("#№Док#", "123");
            document.ReplaceText("#ДатаДок#", DateTime.Now.ToString("dd.MM.yyyy"));
            document.ReplaceText("#ПериодС#", periodStart.ToString("dd.MM.yyyy"));
            document.ReplaceText("#ПериодПо#", periodEnd.ToString("dd.MM.yyyy"));
            document.ReplaceText("#ДолжностьОтвЛ#", "Секретарь");
            document.ReplaceText("#ФИООтвЛ#", "Егоров Василий Иванович");
            document.ReplaceText("#ДолжностьРукП#", "Директор");
            document.ReplaceText("#ФИОРукП#", "Воронов Давид Алексеевич");
            document.ReplaceText("#ДолжностьРабК#", "HR");
            document.ReplaceText("#ФИОРабК#", "Чернова Анна Васильевна");
            document.ReplaceText("#День#", DateTime.Now.Day.ToString());
            document.ReplaceText("#Месяц#", DateTime.Now.ToString("MMMM"));
            document.ReplaceText("#Год#", DateTime.Now.Year.ToString());
            // Find the table for employees data
            var table = document.Tables.FirstOrDefault(t => t.Paragraphs[0].Text.Contains("Номер по по-рядку"));
            if (table != null)
            {
                int rowIndex = 1;
                foreach (var employee in employees)
                {
                    var row = table.InsertRow();
                    row.Cells[0].Paragraphs[0].Append(rowIndex.ToString());
                    row.Cells[1].Paragraphs[0].Append(employee.FullName);
                    row.Cells[2].Paragraphs[0].Append(employee.TabelNum.ToString());

                    for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                    {
                        var attendance = employee.Attendances.FirstOrDefault(a => a.AttendanceDate.Day == day);
                        if (attendance != null)
                        {
                            var abbreviation = attendance.AttendanceType?.Abbreviation ?? "Я";
                            row.Cells[3 + day - 1].Paragraphs[0].Append(abbreviation);
                        }
                        else
                        {
                            row.Cells[3 + day - 1].Paragraphs[0].Append("Н");
                        }
                    }
                    row.Cells[34].Paragraphs[0].Append(employee.Attendances.Sum(a => a.WorkedOut).ToString());
                    rowIndex++;
                }
            }
            document.SaveAs(outputPath);
        }
    }
}
