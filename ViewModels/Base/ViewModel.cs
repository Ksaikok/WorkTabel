using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkTabel.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected  virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        private DataTable _departmentsData;

        public DataTable DepartmentsData
        {
            get => _departmentsData;
            set
            {
                _departmentsData = value;
                OnPropertyChanged(nameof(DepartmentsData));
            }
        }

        // Constructor
        public DepartmentsViewModel()
        {
            // Fetch data from the database
            DepartmentsData = new DatabaseService().GetDepartments();
        }



    }
}
