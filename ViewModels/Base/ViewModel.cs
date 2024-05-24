using System.ComponentModel;
using System.Runtime.CompilerServices;
using WorkTabel.Model.ObIrtish;

namespace WorkTabel.ViewModels.Base
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
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
        //-------------------------------------------------
        private System.Collections.IEnumerable departments;

        public System.Collections.IEnumerable Departments { get => departments; set => Set(ref departments, value); }

        //-------------------------------------------------
        private System.Collections.IEnumerable employees;

        public System.Collections.IEnumerable Employees { get => employees; set => Set(ref employees, value); }

        //-------------------------------------------------
        private System.Collections.IEnumerable attendanceTypes;

        public System.Collections.IEnumerable AttendanceTypes { get => attendanceTypes; set => Set(ref attendanceTypes, value); }
        //-------------------------------------------------


    }




}
