using CommunityToolkit.Mvvm.Input;
using static WorkTabel.DataAccessLayer.Data.DataAccess;
using System.Collections.ObjectModel;
using System.Windows;
using WorkTabel.Model.ObIrtish;
using WorkTabel.ViewModels.Base;

namespace WorkTabel.ViewModels
{
    public class AddEmployeeViewModel : ViewModel
    {
        private readonly PositionDataAccess _positionDataAccess = new PositionDataAccess();
        private readonly DepartmentDataAccess _departmentDataAccess = new DepartmentDataAccess();
        private readonly AddEmpDataAccess _addEmpDataAccess = new AddEmpDataAccess();

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => Set(ref _fullName, value);
        }

        private int _tabelNum;
        public int TabelNum
        {
            get => _tabelNum;
            set => Set(ref _tabelNum, value);
        }

        private Position _selectedPosition;
        public Position SelectedPosition
        {
            get => _selectedPosition;
            set
            {
                if (Set(ref _selectedPosition, value))
                {
                    OnPropertyChanged(nameof(PositionID));
                }
            }
        }

        public int PositionID => SelectedPosition?.PositionID ?? 0;

        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                if (Set(ref _selectedDepartment, value))
                {
                    OnPropertyChanged(nameof(DepartmentID));
                }
            }
        }

        public int DepartmentID => SelectedDepartment?.DepartmentID ?? 0;

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => Set(ref _phoneNumber, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        private DateTime _birthday;
        public DateTime Birthday
        {
            get => _birthday;
            set => Set(ref _birthday, value);
        }

        private ObservableCollection<Position> _positions;
        public ObservableCollection<Position> Positions
        {
            get => _positions;
            set => Set(ref _positions, value);
        }

        private ObservableCollection<Department> _departments;
        public ObservableCollection<Department> Departments
        {
            get => _departments;
            set => Set(ref _departments, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => Set(ref _errorMessage, value);
        }

        private RelayCommand _addEmployeeCommand;
        public RelayCommand AddEmployeeCommand => _addEmployeeCommand ?? (_addEmployeeCommand = new RelayCommand(AddEmployee));

        public AddEmployeeViewModel()
        {
            // Инициализация коллекций
            Positions = new ObservableCollection<Position>(_positionDataAccess.GetPositions());
            Departments = new ObservableCollection<Department>(_departmentDataAccess.GetDepartments());
        }

        private void AddEmployee()
        {
            if (ValidateInputs())
            {
                var employee = new Employee
                {
                    FullName = FullName,
                    TabelNum = TabelNum,
                    PositionID = SelectedPosition,
                    DepartmentID = SelectedDepartment,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    Birthday = Birthday,
                };

                bool isAdded = _addEmpDataAccess.AddEmployee(employee);
                if (isAdded)
                {
                    MessageBox.Show("Успешно! Перезапустите приложение для отображения нового сотрудника.");
                    // Закрыть окно после успешного добавления
                    CloseWindow();
                }
            }
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                ErrorMessage = "Введите ФИО.";
                return false;
            }

            if (TabelNum == null)
            {
                ErrorMessage = "Введите табельный номер.";
                return false;
            }

            if (SelectedPosition == null)
            {
                ErrorMessage = "Выберите должность.";
                return false;
            }

            if (SelectedDepartment == null)
            {
                ErrorMessage = "Выберите отдел.";
                return false;
            }

            if (PhoneNumber == null)
            {
                ErrorMessage = "Введите номер телефона.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Введите еmail.";
                return false;
            }



            ErrorMessage = string.Empty;
            return true;
        }
        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}