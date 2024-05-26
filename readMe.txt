Привет
Далее будет описана инфраструктура проекта
	
1. Infrastructure				- инфраструктура :)
	1.1 Base					- это база
		1.1.1 Commands.cs		- класс, содержащий class Command : ICommand и EventHandler
	1.2 LambdaCommands.cs		- что-то из видео урока
2. Model						- слой модели
	2.1 ObIrtish				- Обь-Иртышь
		2.1.1 Sotrudnik.cs		- класс в котором объявляется класс Employee (сотрудник) и Department (отдел) со всеми переменными + PropertyChangedEventHandler
	2.2 Data					- папка для всего что связанно с данными
		2.2.1 DataAccess.cs		- в этом файле происходит чтение таблиц и использование Базы Данных
3. Services						- сервисы (пустые)
4. View							- слой View
	4.1	Windows					- окна приложения
		4.1.1 MainWindow.xaml	- главное окно
5. ViewModels					- слой ViewModel
	5.1 Base					- снова база
		5.1.1 ViewModel.cs		- класс ViewModel'и и System.Collections.IEnumerable для departments и employees
	5.2 MainViewModel.cs		- 
6. gitattributes				- гитовская всячина
7. gitignore					- гитовская всячина
8. App.config					- файл конфигурации приложения. В нём находится строка подключения к БД (с логином, паролем, локалхостом и тд)
9.App.xaml						- xaml файл приложения с ссылками на словари ресурсов
10. readme						- *вы находитесь тут*
		