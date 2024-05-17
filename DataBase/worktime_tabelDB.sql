-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Апр 29 2024 г., 13:15
-- Версия сервера: 8.0.30
-- Версия PHP: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `worktime_tabelDB`
--

-- --------------------------------------------------------

--
-- Структура таблицы `Attendance`
--

CREATE TABLE `Attendance` (
  `AttendanceID` int UNSIGNED NOT NULL,
  `Date` date DEFAULT NULL,
  `TimeIn` time DEFAULT NULL,
  `TimeOut` time DEFAULT NULL,
  `EmployeeID` int UNSIGNED DEFAULT NULL,
  `AttendanceTypeID` int UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `AttendanceType`
--

CREATE TABLE `AttendanceType` (
  `AttendanceTypeID` int UNSIGNED NOT NULL,
  `Abbreviation` text NOT NULL,
  `Definition` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `AttendanceType`
--

INSERT INTO `AttendanceType` (`AttendanceTypeID`, `Abbreviation`, `Definition`) VALUES
(1, 'Я', 'Продолжительность работы в дневное время'),
(2, 'Н', 'Продолжительность работы в ночное время'),
(3, 'РВ', 'Продолжительность работы в выходные и нерабочие, праздничные дни'),
(4, 'С', 'Продолжительность сверхурочной работы'),
(5, 'ВМ', 'Продолжительность работы вахтовым методом'),
(6, 'К', 'Служебная командировка'),
(7, 'ПК', 'Повышение квалификации с отрывом от работы'),
(8, 'ПМ', 'Повышение квалификации с отрывом от работы в другой местности'),
(9, 'ОТ', 'Ежегодный основной оплачиваемый отпуск'),
(10, 'ОД', 'Ежегодный дополнительный оплачиваемый отпуск'),
(11, 'У', 'Дополнительный отпуск в связи с обучением с сохранением среднего заработка работникам, совмещающим работу с обучением'),
(12, 'УФ', 'Сокращенная продолжительность рабочего времени для обучающихся без отрыва от производства с частичным сохранением заработной платы'),
(13, 'УД', 'Дополнительный отпуск, в связи с обучением без сохранения заработной платы'),
(14, 'Р', 'Отпуск по беременности и родам (отпуск в связи с усыновлением новорожденного ребенка)'),
(15, 'ОЖ', 'Отпуск по уходу за ребенком до достижения им возраста трех лет'),
(16, 'ДО', 'Отпуск без сохранения заработной платы, предоставленный работнику по разрешению работодателя'),
(17, 'ОЗ', 'Отпуск без сохранения заработной платы в случаях, предусмотренных законодательством'),
(18, 'ДБ', 'Ежегодный дополнительный отпуск без сохранения заработной платы'),
(19, 'Б', 'Временная нетрудоспособность (кроме случаев, предусмотренных кодом «Т») с назначением пособия согласно законодательству  '),
(20, 'Т', 'Временная нетрудоспособность без назначения пособия в случаях, предусмотренных законодательством  '),
(21, 'ЛЧ', 'Сокращенная продолжительность рабочего времени против нормальной продолжительности рабочего дня в случаях, предусмотренных законодательством'),
(22, 'ПВ ', 'Время вынужденного прогула в случае признания увольнения, перевода на другую работу или отстранения от работы незаконными с восстановлением на прежней работе'),
(23, 'Г', 'Невыходы на время исполнения государственных или общественных обязанностей согласно законодательству'),
(24, 'ПР', 'Прогулы (отсутствие на рабочем месте без уважительной причины в течение времени, установленного законодательством)'),
(25, 'НС', 'Продолжительность работы в режиме неполного рабочего времени по инициативе работодателя в случаях, предусмотренных законодательством'),
(26, 'В', 'Выходные дни (еженедельный отпуск) и нерабочие праздничные дни'),
(27, 'ОВ', 'Дополнительные выходные дни (оплачиваемые)'),
(28, 'НВ', 'Дополнительные выходные дни (без сохранения заработной платы)'),
(29, 'ЗБ', 'Забастовка (при условиях и в порядке, предусмотренных законом)'),
(30, 'НН', 'Неявки по невыясненным причинам (до выяснения обстоятельств)'),
(31, 'РП', 'Время простоя по вине работодателя'),
(32, 'НП', 'Время простоя по причинам, не зависящим от работодателя и работника'),
(33, 'ВП', 'Время простоя по вине работника'),
(34, 'НО', 'Отстранение от работы (недопущение к работе) с оплатой (пособием) в соответствии с законодательством'),
(35, 'НБ', 'Отстранение от работы (недопущение к работе) по причинам, предусмотренным законодательством, без начисления заработной платы'),
(36, 'НЗ', 'Время приостановки работы в случае задержки выплаты заработной платы');

-- --------------------------------------------------------

--
-- Структура таблицы `Departments`
--

CREATE TABLE `Departments` (
  `DepartmentID` int UNSIGNED NOT NULL,
  `Name` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Departments`
--

INSERT INTO `Departments` (`DepartmentID`, `Name`) VALUES
(1, 'Отдел договорных отн. и орг. торгов'),
(2, 'Отдел бух. учёта и отчётности'),
(3, 'Отдел налогообложения и расчетов с перс.'),
(4, 'Отдел содержания и развития инфраструктуры'),
(5, 'Отдел компьютерного обеспечения и связи'),
(6, 'Отдел транспортной безопасности ГО и ЧС'),
(7, 'Отдел правовых и имущественных отношений'),
(8, 'Отдел хоз. обеспечения'),
(9, 'Отдел судового хоз-ства и промдеятельности'),
(10, 'Технический отдел'),
(11, 'Отдел промышленной безопасности'),
(13, 'Заместители');

-- --------------------------------------------------------

--
-- Структура таблицы `Employees`
--

CREATE TABLE `Employees` (
  `EmployeeID` int UNSIGNED NOT NULL,
  `FullName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `PositionID` int UNSIGNED DEFAULT NULL,
  `DepartmentID` int UNSIGNED DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Employees`
--

INSERT INTO `Employees` (`EmployeeID`, `FullName`, `PositionID`, `DepartmentID`) VALUES
(1, 'Вандакурова Анастасия Константиновна', 7, 13),
(2, 'Япончик Владимир Михайлович', 16, 8),
(3, 'Геливера Вячеслав Владимирович\r\n', 11, 13),
(4, 'Чаюн Семён Андреевич', 1, 5),
(5, 'Окулов Игорь Владимирович', 1, 5),
(6, 'Богатырёв Добрыня Никитич', 4, 2),
(7, 'Виноградова Кира Марковна', 2, 10),
(8, 'Гусева Виктория Вячеславовна', 1, 10),
(9, 'Алексеев Герман Петрович', 1, 10),
(10, 'Ермаков Роман Константинович', 2, 10),
(11, 'Назарова Софья Никитична', 18, 10),
(12, 'Фирсов Владислав Михайлович', 2, 10),
(13, 'Новиков Платон Ильич', 4, 2),
(14, 'Воронов Леонид Глебович', 4, 2),
(15, 'Яковлева Елизавета Лукинична', 4, 2),
(16, 'Громов Павел Янович', 4, 2),
(17, 'Королева Вероника Александровна', 4, 2),
(18, 'Золотарева Ксения Артёмовна', 4, 2),
(19, 'Шестаков Максим Тимофеевич', 4, 3),
(20, 'Кузнецова Алёна Степановна', 4, 3),
(21, 'Михайлов Богдан Кириллович', 4, 3),
(22, 'Мальцева Дарья Матвеевна', 4, 3),
(23, 'Кузьмина Виктория Арсентьевна', 4, 3),
(24, 'Пантелеева Дарья Ивановна', 3, 7),
(25, 'Золотов Владимир Адамович', 3, 7),
(26, 'Виноградов Марк Маратович', 3, 7),
(27, 'Коновалов Матвей Давидович', 3, 7),
(28, 'Воронов Давид Алексеевич', 3, 7),
(29, 'Орехов Александр Александрович', 4, 7);

-- --------------------------------------------------------

--
-- Структура таблицы `Positions`
--

CREATE TABLE `Positions` (
  `PositionID` int UNSIGNED NOT NULL,
  `Name` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `Positions`
--

INSERT INTO `Positions` (`PositionID`, `Name`) VALUES
(1, 'Разработчик'),
(2, 'Конструктор'),
(3, 'Юрист'),
(4, 'Бухгалтер'),
(5, 'Завхоз'),
(6, 'Руководитель'),
(7, 'Первый замемтитель руководителя'),
(8, 'Первый замрук-капитан Обь-Ирт. бассейна'),
(9, 'Замрук по экономике и финансам'),
(10, 'Замрук по флоту'),
(11, 'Замрук по развитию и обеспечению'),
(12, 'Главный бухгалтер'),
(13, 'Советник'),
(14, 'Помощник'),
(15, 'Методист'),
(16, 'Уборщик помещений'),
(17, 'Инженер'),
(18, 'Секретарь');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `Attendance`
--
ALTER TABLE `Attendance`
  ADD PRIMARY KEY (`AttendanceID`),
  ADD KEY `AttendanceTypeID` (`AttendanceTypeID`),
  ADD KEY `EmployeeID` (`EmployeeID`);

--
-- Индексы таблицы `AttendanceType`
--
ALTER TABLE `AttendanceType`
  ADD PRIMARY KEY (`AttendanceTypeID`);

--
-- Индексы таблицы `Departments`
--
ALTER TABLE `Departments`
  ADD PRIMARY KEY (`DepartmentID`);

--
-- Индексы таблицы `Employees`
--
ALTER TABLE `Employees`
  ADD PRIMARY KEY (`EmployeeID`),
  ADD KEY `PositionID` (`PositionID`),
  ADD KEY `DepartmentID` (`DepartmentID`);

--
-- Индексы таблицы `Positions`
--
ALTER TABLE `Positions`
  ADD PRIMARY KEY (`PositionID`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `Attendance`
--
ALTER TABLE `Attendance`
  MODIFY `AttendanceID` int UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `AttendanceType`
--
ALTER TABLE `AttendanceType`
  MODIFY `AttendanceTypeID` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT для таблицы `Departments`
--
ALTER TABLE `Departments`
  MODIFY `DepartmentID` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT для таблицы `Employees`
--
ALTER TABLE `Employees`
  MODIFY `EmployeeID` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT для таблицы `Positions`
--
ALTER TABLE `Positions`
  MODIFY `PositionID` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `Attendance`
--
ALTER TABLE `Attendance`
  ADD CONSTRAINT `attendance_ibfk_1` FOREIGN KEY (`AttendanceTypeID`) REFERENCES `AttendanceType` (`AttendanceTypeID`),
  ADD CONSTRAINT `attendance_ibfk_2` FOREIGN KEY (`EmployeeID`) REFERENCES `Employees` (`EmployeeID`);

--
-- Ограничения внешнего ключа таблицы `Employees`
--
ALTER TABLE `Employees`
  ADD CONSTRAINT `employees_ibfk_1` FOREIGN KEY (`PositionID`) REFERENCES `Positions` (`PositionID`),
  ADD CONSTRAINT `employees_ibfk_2` FOREIGN KEY (`DepartmentID`) REFERENCES `Departments` (`DepartmentID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
