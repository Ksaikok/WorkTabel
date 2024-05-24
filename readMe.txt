ПРивет
Далее будет описана инфраструктура проекта

1.	Data						- папка для всего что связанно с данными
	1.1 DataAccess.cs			- в этом файле происходит чтение таблиц и использование Базы Данных
2. Infrastructure				- инфраструктура :)
	2.1 Base					- это база
		2.1.1 Commands.cs		- класс, содержащий class Command : ICommand и EventHandler
	2.2 LambdaCommands.cs		- что-то из видео урока, можно не обращать внимания (позже удалю)
3. Model						- слой модели
	3.1 ObIrtish				- Обь-Иртышь
		3.1.1 Sotrudnik.cs		- класс в котором объявляется класс Employee (сотрудник) и Department (отдел) со всеми переменными + PropertyChangedEventHandler
4. Services						- сервисы (пустые)
5. View							- слой View
	5.1	Windows					- окна приложения
		5.1.1 MainWindow.xaml	- главное окно
6. ViewModels					- слой ViewModel
	6.1 Base					- снова база
		6.1.1 ViewModel.cs		- класс ViewModel'и и System.Collections.IEnumerable для departments и employees
	6.2 MainViewModel.cs		- 
7. gitattributes				- гитовская всячина
8. gitignore					- гитовская всячина
9. App.config					- файл конфигурации приложения. В нём находится строка подключения к БД (с логином, паролем, локалхостом и тд)
10.App.xaml						- xaml файл приложения с ссылками на словари ресурсов
11. readme						- *вы находитесь тут*
		