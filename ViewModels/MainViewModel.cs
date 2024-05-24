using System.Data;
using System.Collections.ObjectModel;
using WorkTabel.Model.ObIrtish;
using static WorkTabel.Model.Data.DataAccess;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WorkTabel.ViewModels.Base;

namespace WorkTabel.ViewModels
{

    public class MainViewModel : ViewModel
        {

        // PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<AttendanceType> AttendanceTypes { get; set; }
        //----------------------------------------------------------------
        private Department _selectedDepartment;
        public Department SelectedDepartment
            {
            get => _selectedDepartment;
            set
            {
                Set(ref _selectedDepartment, value); // Используем метод Set из ViewModelBase
                FilterEmployeesByDepartment();
            }
        }

        // Отфильтрованная коллекция сотрудников
        private ObservableCollection<Employee> _filteredEmployees;
        public ObservableCollection<Employee> FilteredEmployees
        {
            get => _filteredEmployees;
            set => Set(ref _filteredEmployees, value);
        }



        public MainViewModel()
        {
             // Загрузка данных
             Departments = new ObservableCollection<Department>(new DepartmentDataAccess().GetDepartments());
             Employees = new ObservableCollection<Employee>(new EmployeeDataAccess().GetEmployees());
             AttendanceTypes = new ObservableCollection<AttendanceType>(new AttendanceTypeDataAccess().GetAttendanceTypes());

            //Employees = new EmployeeDataAccess().GetEmployees();

            // Инициализация FilteredEmployees
            FilteredEmployees = new ObservableCollection<Employee>(Employees);


        }
        //-------------------------------------------------

        // Метод фильтрации сотрудников
        private void FilterEmployeesByDepartment()
        {
            if (SelectedDepartment == null)
            {
                // Если отдел не выбран, показываем всех сотрудников
                FilteredEmployees = new ObservableCollection<Employee>(Employees);
            }
            else
            {
                // Фильтруем сотрудников по выбранному отделу
                FilteredEmployees = new ObservableCollection<Employee>(
                    Employees.Where(e => e.DepartmentID == SelectedDepartment.DepartmentID)
                );
            }
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
    








