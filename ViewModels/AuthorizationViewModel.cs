using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTabel.Model.ObIrtish;
using WorkTabel.ViewModels.Base;

namespace WorkTabel.ViewModels
{
    //public class AuthorizationViewModel : ViewModel
    //{
    //    public AuthorizationModel Model { get; set; } = new AuthorizationModel();

    //    // Флаг, указывающий, выбрана ли роль "Гость"
    //    private bool _isGuestMode;
    //    public bool IsGuestMode
    //    {
    //        get => _isGuestMode;
    //        set => Set(ref _isGuestMode, value);
    //    }

    //    // Команда для входа в качестве гостя
    //    private RelayCommand _enterGuestModeCommand;
    //    public RelayCommand EnterGuestModeCommand => _enterGuestModeCommand ?? (_enterGuestModeCommand = new RelayCommand(EnterGuestMode));

    //    // Метод для входа в качестве гостя
    //    private void EnterGuestMode()
    //    {
    //        // Устанавливаем флаг IsGuestMode в true
    //        IsGuestMode = true;

    //        // Вызываем событие OnLoginSuccess, чтобы сигнализировать об успешной авторизации
    //        OnLoginSuccess?.Invoke();
    //    }

    //    // Команда для входа с помощью логина и пароля
    //    private RelayCommand _loginCommand;
    //    public RelayCommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(Login));

    //    // Событие, которое будет вызываться при успешной авторизации
    //    public event Action OnLoginSuccess;

    //    private void Login()
    //    {
    //        // Проверьте введенные данные (имя пользователя и пароль).
    //        // Выполните проверку аутентификации (например, обратитесь к серверу или базе данных).
    //        // Если авторизация прошла успешно, вызовите OnLoginSuccess.
    //        if (ValidateCredentials(Model.UserName, Model.Password))
    //        {
    //            OnLoginSuccess?.Invoke(); // Вызываем событие, сигнализируя об успешной авторизации
    //        }
    //        else
    //        {
    //            // Покажите сообщение об ошибке
    //            MessageBox.Show("Неправильное имя пользователя или пароль.");
    //        }
    //    }

    //    private bool ValidateCredentials(string userName, string password)
    //    {
    //        // Здесь реализуйте логику проверки учетных данных
    //        // Например, можно проверить введенные данные с данными из базы данных или конфигурационного файла
    //        // Для простоты, можно просто проверить, совпадают ли имя пользователя и пароль с жестко заданными значениями
    //        return userName == "admin" && password == "password";
    //    }
    //}
}
