using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTabel.Model.ObIrtish;
using WorkTabel.ViewModels.Base;
using static WorkTabel.ViewModels.MainViewModel;

namespace WorkTabel.ViewModels
{
    public class AuthorizationViewModel : ViewModel
    {
        public AuthorizationModel Model { get; set; } = new AuthorizationModel();
        // Команда для входа с помощью логина и пароля
        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(Login));
        // Событие, которое будет вызываться при успешной авторизации
        public event Action OnLoginSuccess;
        private void Login()
        {
            // Если авторизация прошла успешно, вызовите OnLoginSuccess.
            if (ValidateCredentials(Model.UserName, Model.Password))
            {
                OnLoginSuccess?.Invoke(); // Вызываем событие, сигнализируя об успешной авторизации
            }
            else
            {
                // Покажите сообщение об ошибке
                MessageBox.Show("Неправильное имя пользователя или пароль.");
            }
        }
        private bool ValidateCredentials(string userName, string password)
        {
            return userName == "admin" && password == "password";
        }
    }
}
