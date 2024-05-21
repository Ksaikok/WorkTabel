using System;
using System.Data;
using System.Collections.ObjectModel;
using WorkTabel.ViewModels.Base;
using WorkTabel.Model.ObIrtish;
using static WorkTabel.Data.DataAccess;

namespace WorkTabel.ViewModels 
{
    
        public class MainViewModel 
        {
            public ObservableCollection<Department> Departments { get; set; }
            public ObservableCollection<Employee> Employees { get; set; }

            public MainViewModel()
            {
                Departments = new ObservableCollection<Department>(new DepartmentDataAccess().GetDepartments());
                Employees = new ObservableCollection<Employee>(new EmployeeDataAccess().GetEmployees());
                //Employees = new EmployeeDataAccess().GetEmployees();
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
    








