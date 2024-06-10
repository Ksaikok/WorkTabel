using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTabel.Model.ObIrtish;
using WorkTabel.ViewModels.Base;
using static WorkTabel.DataAccessLayer.Data.DataAccess;

namespace WorkTabel.ViewModels
{
    public class DelEmployeeViewModel : ViewModel
    {
        //удаление
        private readonly DelEmpDataAccess _deleteEmpDataAccess = new DelEmpDataAccess();
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set => Set(ref _selectedEmployee, value);
        }

        private RelayCommand _deleteEmployeeCommand;
        public RelayCommand DeleteEmployeeCommand => _deleteEmployeeCommand ?? (_deleteEmployeeCommand = new RelayCommand(DeleteEmployee, CanDeleteEmployee));

        private void DeleteEmployee()
        {
            if (SelectedEmployee != null)
            {
                // Здесь вызывайте метод для удаления выбранного сотрудника из базы данных
                // Например, можно использовать метод из вашего DataAccess

                bool isDeleted = _deleteEmpDataAccess.DeleteEmployee(SelectedEmployee);
                if (isDeleted)
                {
                    MessageBox.Show("Сотрудник успешно удален.");
                    // После удаления обновите список сотрудников, чтобы отразить изменения в интерфейсе
                    // Например, можно перезагрузить список сотрудников
                    // После удаления сбросьте выбранного сотрудника на null
                    SelectedEmployee = null;
                }
                else
                {
                    MessageBox.Show("Не удалось удалить сотрудника.");
                }
            }
        }

        private bool CanDeleteEmployee()
        {
            // Проверяем, выбран ли какой-то сотрудник для удаления
            return SelectedEmployee != null;
        }
        //private RelayCommand _deleteEmployeeCommand;
        //public RelayCommand DeleteEmployeeCommand => _deleteEmployeeCommand ?? (_deleteEmployeeCommand = new RelayCommand(DeleteEmployee));

        //private void DeleteEmployee()
        //{
        //    // Проверяем, есть ли выбранный сотрудник для удаления
        //    if (SelectedEmployee != null)
        //    {
        //        // Вызываем метод удаления из DataAccess
        //        bool isDeleted = _addEmpDataAccess.DeleteEmployee(SelectedEmployee);
        //        if (isDeleted)
        //        {
        //            MessageBox.Show("Сотрудник удалён успешно.");
        //            // Обновляем список сотрудников после удаления
        //            Employees.Remove(SelectedEmployee);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Ошибка при удалении сотрудника.");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Выберите сотрудника для удаления.");
        //    }
        //}
    }
}