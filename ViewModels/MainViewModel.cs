using System;
using System.Data;
using System.Collections.ObjectModel;
using WorkTabel.ViewModels.Base;
using WorkTabel.Model.ObIrtish;
using static WorkTabel.Model.Data.DataAccess;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using WorkTabel.Model.Data;
using System.ComponentModel;
using System.Windows.Data;

namespace WorkTabel.ViewModels
{

    public class MainViewModel 
        {
            public ObservableCollection<Department> Departments { get; set; }
            public ObservableCollection<Employee> Employees { get; set; }
            public ObservableCollection<AttendanceType> AttendanceTypes { get; set; }


            public MainViewModel()
            {
                    Departments = new ObservableCollection<Department>(new DepartmentDataAccess().GetDepartments());
                    Employees = new ObservableCollection<Employee>(new EmployeeDataAccess().GetEmployees());
                    AttendanceTypes = new ObservableCollection<AttendanceType>(new AttendanceTypeDataAccess().GetAttendanceTypes());

                    //Employees = new EmployeeDataAccess().GetEmployees();
            }



        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
                // Обновляем список сотрудников при изменении выбранного отдела
                Employees.Refresh();
            }
        }

        // Коллекция всех сотрудников (загружается из базы данных)
        private ObservableCollection<Employee> _allEmployees;

        // Отфильтрованная коллекция сотрудников для DataGrid
        private ICollectionView _employees;
        public ICollectionView Employees
        {
            get
            {
                if (_employees == null && _allEmployees != null)
                {
                    _employees = CollectionViewSource.GetDefaultView(_allEmployees);
                    _employees.Filter = EmployeeFilter; // Устанавливаем фильтр
                }
                return _employees;
            }
        }

        // Метод фильтрации сотрудников по выбранному отделу
        private bool EmployeeFilter(object item)
        {
            if (SelectedDepartment == null)
                return true; // Если отдел не выбран, показываем всех

            var employee = item as Employee;
            return employee.DepartmentID == SelectedDepartment.DepartmentID;
        }


    }
 }   
    
    
    //internal class MainWindowViewModel : ViewModel
    //{



        //#region Команды
        //#region CloseApplicationCommand
        //public ICommand CloseApplicationCommand { get; }
        //private bool CanCloseApplicationCommandExecute(object p) => true;
        //private void OnCloseApplicationCommandExecuted(object p)
        //{
        //    System.Windows.Application.Current.Shutdown();
        //}
        //#endregion



        //#endregion

        //public MainWindowViewModel()
        //{
        //    #region Команды
        //    CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

        //    #endregion


        //}
    //}
    








