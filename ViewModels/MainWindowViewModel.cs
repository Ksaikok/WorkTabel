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
        private string _Title = "Главная страница";

        public string Title 
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

    }
}
