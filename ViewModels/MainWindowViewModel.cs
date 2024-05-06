using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkTabel.ViewModels.Base;

namespace WorkTabel.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _Title;

        public string Title 
        {
            set => Set(ref _Title, value);
        }

    }
}
