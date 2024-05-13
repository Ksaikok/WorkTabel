using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkTabel.Infrastructure.Commands;
using WorkTabel.ViewModels.Base;
using static System.Net.Mime.MediaTypeNames;

namespace WorkTabel.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Команды
        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion
        #endregion

        public MainWindowViewModel()
        {
            #region Команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            #endregion
        }
        //internal class MainWindowViewModel : ViewModel
        //{
        //    //private string _Title = "Главная страница";

        //    //public string Title 
        //    //{
        //    //    get => _Title;
        //    //    set => Set(ref _Title, value);
        //    //}

        //}
    }
}
