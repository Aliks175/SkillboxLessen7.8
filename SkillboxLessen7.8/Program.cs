using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using static System.Console;
using File = System.IO.File;

namespace SkillboxLessen7._8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Repository repository = new Repository();
            Worker[] workers;
            bool isOpen = true;
            string inputText;
            Worker fiendWorker;
            while (isOpen)
            {
                WriteLine("Добрый день выберите один из следующих пунктов :\n");
                WriteLine("1)Просмотр всех записей.\n2)Просмотр одной записи по индексу\n" +
                    "3)Создание записи.\n4)Удаление записи.\n5)Загрузка записей в выбранном диапазоне дат.\n" +
                    "6)Выберите сортировку");
                Write("Введите номер желаемого пункта ");
                inputText = ReadLine();
                WriteLine("\n");
                if (int.TryParse(inputText, out int inputNumber)) { }
                else
                {
                    WriteLine("Ошибка ввода такой функции нет");
                    ReadLine();
                    Clear();
                    continue;
                }
                switch (inputNumber)
                {
                    case 1:
                        WriteLine("Список всех рабочих :\n ");
                        workers = repository.GetAllWorkers();

                        foreach (var worker in workers)
                            worker.ShowInfo();
                        break;
                    case 2:
                        Write("Введите искомый индекс рабочего : ");
                        inputText = ReadLine();
                        WriteLine("\n");
                        if (int.TryParse(inputText, out int inputIndex))
                        {
                            fiendWorker = repository.GetWorkerById(inputIndex);
                            if (fiendWorker.Equals(new Worker())) { }
                            else
                            {
                                fiendWorker.ShowInfo();
                            }
                        }
                        else
                        {
                            WriteLine("Ошибка ввода такого индекса нет");
                            break;
                        }
                        break;
                    case 3:
                        string[] employeeData = new string[6];
                        if (repository.DataCompletion(ref employeeData))
                        {
                            repository.CreateNewWorker(employeeData);
                        }
                        else
                        {
                            WriteLine("Ошибка ввода, Данные не полные");
                        }
                        break;
                    case 4:
                        Clear();
                        WriteLine("Список всех рабочих :\n ");
                        foreach (var worker in repository.GetAllWorkers())
                        {
                            worker.ShowInfo();
                        }
                        WriteLine("\n");
                        Write("Удалние работника.\nВведите индекс :");
                        inputText = ReadLine();
                        if (int.TryParse(inputText, out int deleteIndex))
                        {
                            fiendWorker = repository.GetWorkerById(deleteIndex);
                            if (fiendWorker.Equals(new Worker())) { }
                            else
                            {
                                repository.DeleteWorker(deleteIndex);
                            }
                        }
                        else
                        {
                            WriteLine("Ошибка ввода такого индекса нет");
                            break;
                        }
                        break;
                    case 5:
                        Clear();
                        if (repository.ChooseSortingMethod(out int sortValue)) { }
                        else
                        {
                            continue;
                        }
                        repository.DataCompletionSerchBetweenTwoDates(out DateTime dateFrom, out DateTime dateTo);
                        if (dateFrom == DateTime.MinValue && dateTo == DateTime.MinValue)
                        {
                            WriteLine("Ошибка Ввод данных не коректен ");
                        }
                        else if (dateFrom > dateTo)
                        {
                            WriteLine("Дата начала поиска больше чем дата его окончания проверьте корректность ввода");
                        }
                        else
                        {
                            workers = repository.GetWorkersBetweenTwoDates(dateFrom, dateTo, sortValue);
                            WriteLine("Все успешно ");
                            foreach (var worker in workers)
                            {
                                WriteLine('\n');
                                WriteLine(new string('_', 48) + "\n");
                                worker.ShowInfo();
                            }
                        }
                        break;
                    case 6:
                        int _chooseNumberFilter = 1;
                        bool _isForwardFilter=true;
                        bool _isdataSuccessfullyCompleted=false;
                        workers = repository.GetAllWorkers();
                        bool isDataEntry = true;
                        while (isDataEntry)
                        {
                            Clear();
                            WriteLine("Введите новую сортеровку ");
                            WriteLine("\n1)По возростанию\n2)По убыванию ");
                            Write("Ваш выбор :");
                            if (int.TryParse(ReadLine(), out int numberSort))
                            {
                                if (numberSort > 0 && numberSort < 3)
                                    _isForwardFilter = numberSort == 1;
                                else
                                {
                                    WriteLine("Ошибка нет ");
                                    ReadLine();
                                    Clear();
                                    continue;
                                }
                            }
                            else
                            {
                                WriteLine("Ошибка нет ");
                                ReadLine();
                                Clear();
                                continue;
                            }
                            WriteLine("По какому критерию сортировать ");
                            WriteLine("\n1)По Id \n2)По дате создания\n3)По имени\n4)по возросту\n" +
                                "5)По росту\n6)По дате рождения\n7)По месту рождения ");
                            Write("Ваш выбор :");
                            if (int.TryParse(ReadLine(), out int numberChooseSort))
                            {
                                if (numberChooseSort > 0 && numberChooseSort < 8)
                                {
                                    _chooseNumberFilter = numberChooseSort;
                                }
                                else
                                {
                                    WriteLine("Ошибка нет ");
                                    ReadLine();
                                    Clear();
                                    continue;
                                }
                            }
                            else
                            {
                                WriteLine("Ошибка нет ");
                                ReadLine();
                                Clear();
                                continue;
                            }
                            Write("\nФильтр сортировки установлен");
                            isDataEntry = false;
                            _isdataSuccessfullyCompleted = true;
                        }
                        if (_isdataSuccessfullyCompleted)
                        {
                            if (_isForwardFilter)
                            {
                                if (_chooseNumberFilter == 1)
                                {
                                    workers = workers.OrderBy(worker => worker.Id).ToArray();
                                }
                                else if (_chooseNumberFilter == 2)
                                {
                                    workers = workers.OrderBy(worker => worker.DateAndTimeCreate).ToArray();
                                }
                                else if (_chooseNumberFilter == 3)
                                {
                                    workers = workers.OrderBy(worker => worker.FullName).ToArray();
                                }
                                else if (_chooseNumberFilter == 4)
                                {
                                     workers = workers.OrderBy(worker => worker.Age).ToArray();
                                }
                                else if (_chooseNumberFilter == 5)
                                {
                                     workers = workers.OrderBy(worker => worker.Height).ToArray();
                                }
                                else if (_chooseNumberFilter == 6)
                                {
                                    workers =  workers.OrderBy(worker => worker.DateOfBirth).ToArray();
                                }
                                else if (_chooseNumberFilter == 7)
                                {
                                    workers =  workers.OrderBy(worker => worker.PlaceOfBirth).ToArray();
                                }
                            }
                            else
                            {
                                if (_chooseNumberFilter == 1)
                                {
                                    workers = workers.OrderByDescending(worker => worker.Id).Select(worker => worker).ToArray();
                                }
                                else if (_chooseNumberFilter == 2)
                                {
                                    workers = workers.OrderByDescending(worker => worker.DateAndTimeCreate).Select(worker => worker).ToArray();
                                }
                                else if (_chooseNumberFilter == 3)
                                {
                                    workers = workers.OrderByDescending(worker => worker.FullName).Select(worker => worker).ToArray();
                                }
                                else if (_chooseNumberFilter == 4)
                                {
                                    workers = workers.OrderByDescending(worker => worker.Age).Select(worker => worker).ToArray();
                                }
                                else if (_chooseNumberFilter == 5)
                                {
                                    workers = workers.OrderByDescending(worker => worker.Height).Select(worker => worker).ToArray();
                                }
                                else if (_chooseNumberFilter == 6)
                                {
                                    workers = workers.OrderByDescending(worker => worker.DateOfBirth).Select(worker => worker).ToArray();
                                }
                                else if (_chooseNumberFilter == 7)
                                {
                                    workers = workers.OrderByDescending(worker => worker.PlaceOfBirth).Select(worker => worker).ToArray();
                                }
                            }
                        }
                        break;
                    default:
                        WriteLine("Данные не корректы");
                        break;
                }
                ReadLine();
                Clear();
            }
        }

        struct Worker
        {
            public int Id { get; set; }
            public DateTime DateAndTimeCreate { get; private set; }
            public string FullName { get; private set; }
            public int Age { get; private set; }
            public double Height { get; private set; }
            public DateTime DateOfBirth { get; private set; }
            public string PlaceOfBirth { get; private set; }

            public Worker(DateTime dateCreate, string fullName, int age, double height, DateTime dateOfBirth, string placeOfBirth)
            {
                Id = 0;
                DateAndTimeCreate = dateCreate;
                FullName = fullName;
                Age = age;
                Height = height;
                DateOfBirth = dateOfBirth;
                PlaceOfBirth = placeOfBirth;
            }

            public void ShowInfo()
            {
                WriteLine($"{Id} {DateAndTimeCreate} {FullName} {Age} {Height} {DateOfBirth} {PlaceOfBirth}");
            }
        }

        class Repository
        {
            private string _pathFile = @"PersonalAffairsForEmployees.txt";
            private List<Worker> _workers = new List<Worker>();

            public Repository()
            {
                if (File.Exists(_pathFile))
                {
                    WriteLine($"Загружен файл {_pathFile}");
                    SaveWorkersDate();
                }
                else
                {
                    WriteLine($"Создан файл {_pathFile}");
                    File.Create(_pathFile).Close();
                }
                WriteLine($"\n");
            }

            public Worker[] GetAllWorkers()
            {
                SaveWorkersDate();
                return _workers.ToArray();
            }

            public bool ChooseSortingMethod(out int sortValue)
            {
                bool isDataEntry = true;
                sortValue = 0;
                while (isDataEntry)
                {
                    WriteLine("По какой дате вы хотели бы сортировать");
                    WriteLine("1) Create - дате создания");
                    WriteLine("2) Birth - дате рождения");
                    Write("Введите желаемую сортеровку :");
                   string inputText = ReadLine();
                    WriteLine("\n");
                    if (int.TryParse(inputText, out int inputSortValue))
                    {
                        if (inputSortValue > 0 && inputSortValue < 3)
                        {
                            sortValue = inputSortValue;
                            return true;
                        }
                    }
                    else
                    {
                        Clear();
                        isDataEntry = EndingDataCompletion();
                        continue;
                    }
                }
                return false;
            }

            public void DataCompletionSerchBetweenTwoDates(out DateTime dateFrom, out DateTime dateTo)
            {
                dateFrom = DateTime.MinValue;
                dateTo = DateTime.MinValue;
                bool isDataEntry = true;
                while (isDataEntry)
                {
                    WriteLine("Поиск в выбранном диапазоне дат");
                    WriteLine("\nПример: 01.01.2000\n");
                    Write("Введите от какой даты начать поиск : ");
                    if (DateTime.TryParse(ReadLine(), out DateTime inputDateFrom))
                    {
                        dateFrom = inputDateFrom;
                    }
                    else
                    {
                        WriteLine("Неверная дата рождения");
                        Clear();
                        isDataEntry = EndingDataCompletion();
                        continue;
                    }
                    Write("\n Введите на какой дате закончить поиск :");
                    if (DateTime.TryParse(ReadLine(), out DateTime inputDateTo))
                    {
                        dateTo = inputDateTo;
                    }
                    else
                    {
                        WriteLine("Неверная дата рождения");
                        Clear();
                        isDataEntry = EndingDataCompletion();
                        continue;
                    }
                    isDataEntry = false;
                }
            }

            public bool DataCompletion(ref string[] strings)
            {
                bool isDataFilledInCompletely = false;
                bool isDataEntry = true;
                while (isDataEntry)
                {
                    strings[0] = DateTime.Now.ToString();
                    Clear();
                    WriteLine("\nВ ведите Ф.И.О работника.\nПример: Иванов Иван Иваныч\n");
                    Write("Ф.И.О работника :");
                    string tryInputFull = ReadLine();
                    if (tryInputFull == "")
                    {
                        WriteLine("Неверный формат Ф.И.О работника");
                        ReadKey();
                        isDataEntry = EndingDataCompletion();
                        continue;
                    }
                    else
                    {
                        strings[1] = tryInputFull;
                    }
                    Clear();
                    WriteLine("\nВ ведите Возраст работника.\nПример: 32\n");
                    Write("Возраст :");
                    if (int.TryParse(ReadLine(), out int age))
                    {
                        strings[2] = age.ToString();
                    }
                    else
                    {
                        WriteLine("Неверный возраст");
                        ReadKey();
                        isDataEntry = EndingDataCompletion();
                        continue;
                    }
                    Clear();
                    WriteLine("\nВ ведите Рост работника.\nПример: 176\n");
                    Write("Рост :");
                    if (double.TryParse(ReadLine(), out double height))
                    {
                        strings[3] = height.ToString();
                    }
                    else
                    {
                        WriteLine("Неверный Рост");
                        ReadKey();
                        isDataEntry = EndingDataCompletion();
                        continue;
                    }
                    Clear();
                    WriteLine("\nВ ведите дата рождения работника.\nПример: 05.05.1992\n");
                    Write("Дата рождения : ");
                    if (DateTime.TryParse(ReadLine(), out DateTime dateOfBirth))
                    {
                        strings[4] = dateOfBirth.ToString();
                    }
                    else
                    {
                        WriteLine("Неверная дата рождения");
                        ReadKey();
                        isDataEntry = EndingDataCompletion();
                        continue;
                    }
                    Clear();
                    WriteLine("\nВ ведите место рождения работника.\nПример: город Москва\n");
                    Write("В ведите место рождения :");
                    strings[5] = ReadLine();
                    isDataEntry = false;
                    isDataFilledInCompletely = true;
                }
                return isDataFilledInCompletely;
            }

            public Worker GetWorkerById(int index)
            {
                foreach (Worker worker in _workers)
                {
                    if (worker.Id == index)
                    {
                        return worker;
                    }
                }
                WriteLine("Сотрудника с данным индексом нет");
                return new Worker();
            }

            public void DeleteWorker(int id)
            {
                SaveWorkersDate();
                for (int i = 0; i < _workers.Count; i++)
                {
                    if (_workers[i].Id == id)
                    {
                        _workers.Remove(_workers[i]);
                        WriteLine("Удаление прошло успешно");
                    }
                }
                WriteWorkersDate();
            }

            public void AddWorker(Worker worker)
            {
                if (worker.Equals(new Worker()))
                {
                    Clear();
                    WriteLine("Ошибка добавления нового сотрудника");
                }
                else
                {
                    worker.Id = CheckMaxIdWorkerAtList() + 1;
                    _workers.Add(worker);
                    WriteWorkersDate();
                    Clear();
                    WriteLine(new string('_', 48) + "\n");
                    WriteLine("Добавление нового сотрудника прошло успешно");
                    WriteLine(new string('_', 48) + "\n");
                    WriteLine('\n');
                    WriteLine(new string('_', 48) + "\n");
                    worker.ShowInfo();
                    WriteLine(new string('_', 48) + "\n");
                }
            }

            public void CreateNewWorker(string[] employeeData)
            {
                AddWorker(ContentDateRead(employeeData));
            }

            public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo, int sortValue)
            {
                List<Worker> dataRangeWorkers = new List<Worker>();
                SaveWorkersDate();
                IEnumerable<Worker> FiltersOfDate;
                if (sortValue == 1)
                {
                    FiltersOfDate = from Worker work in _workers where work.DateAndTimeCreate > dateFrom && work.DateAndTimeCreate < dateTo select work;
                }
                else
                {
                    FiltersOfDate = from Worker work in _workers where work.DateOfBirth > dateFrom && work.DateOfBirth < dateTo select work;
                }
                return FiltersOfDate.ToArray();
            }

            private bool EndingDataCompletion()
            {
                string yes = "Yes", no = "No";
                string inputDecision;
                while (true)
                {
                    Clear();
                    WriteLine("Вы хотите продолжить создание новой записи Yes or No");
                    inputDecision = ReadLine();
                    if (yes.ToUpper() == inputDecision.ToUpper())
                    {
                        Clear();
                        return true;
                    }
                    else if (no.ToUpper() == inputDecision.ToUpper())
                    {
                        Clear();
                        return false;
                    }
                }
            }

            private void WriteWorkersDate()
            {
                using (StreamWriter streamWriter = new StreamWriter(_pathFile))
                {
                    foreach (var worker in _workers)
                    {
                        streamWriter.WriteLine($"{worker.Id}#{worker.DateAndTimeCreate}#{worker.FullName}" +
                            $"#{worker.Age}#{worker.Height}#{worker.DateOfBirth}#{worker.PlaceOfBirth}");
                    }
                }
            }

            private void SaveWorkersDate()
            {
                Worker newWorker;
                _workers.Clear();
                string[] strings = File.ReadAllLines(_pathFile);
                for (int i = 0; i < strings.Length; i++)
                {
                    newWorker = ContentVerification(strings[i].Split('#'));
                    if (newWorker.Equals(new Worker()))
                    {
                        continue;
                    }
                    else
                    {
                        newWorker.Id = _workers.Count + 1;
                        _workers.Add(newWorker);
                    }
                }
                var filtersWorker = from work in _workers orderby work.Id select work;
                _workers = filtersWorker.ToList();
            }

            private Worker ContentDateRead(string[] employeeData)
            {
                DateTime _dateAndTimeCreate;
                string _fullName;
                int _age;
                double _height;
                DateTime _dateOfBirth;
                string _placeOfBirth;
                _dateAndTimeCreate = DateTime.Parse(employeeData[0]);
                _fullName = employeeData[1];
                _age = int.Parse(employeeData[2]);
                _height = double.Parse(employeeData[3]);
                _dateOfBirth = DateTime.Parse(employeeData[4]);
                _placeOfBirth = employeeData[5];
                return new Worker(_dateAndTimeCreate, _fullName, _age, _height, _dateOfBirth, _placeOfBirth);
            }

            private Worker ContentVerification(string[] employeeData)
            {
                bool isNotError = true;
                if (int.TryParse(employeeData[0], out int id)){}
                else
                {
                    isNotError = false;
                    WriteLine("При чтении допущена ошибка в блоке ID Не верный");
                }
                if (DateTime.TryParse(employeeData[1], out DateTime dateAndTimeCreate)){}
                else
                {
                    isNotError = false;
                    WriteLine("При чтении допущена ошибка в блоке время добовления записи");
                }
               string _fullName = employeeData[2];
                if (int.TryParse(employeeData[3], out int age)){}
                else
                {
                    isNotError = false;
                    WriteLine("При чтении допущена ошибка в блоке Возраст");
                }
                if (double.TryParse(employeeData[4], out double height)){}
                else
                {
                    isNotError = false;
                    WriteLine("При чтении допущена ошибка в блоке Рост");
                }
                if (DateTime.TryParse(employeeData[5], out DateTime dateOfBirth)){}
                else
                {
                    isNotError = false;
                    WriteLine("При чтении допущена ошибка в блоке день рождения");
                }
               string _placeOfBirth = employeeData[6];
                if (isNotError)
                {
                    return new Worker(dateAndTimeCreate, _fullName, age, height, dateOfBirth, _placeOfBirth);
                }
                else
                {
                    WriteLine("При чтении допущена ошибка файл пропущен");
                    return new Worker();
                }
            }

            private int CheckMaxIdWorkerAtList()
            {
                int maxId = 0;
                if (_workers.Count > 0)
                {
                    maxId = _workers[0].Id;
                    for (int i = 1; i < _workers.Count; i++)
                    {
                        if (_workers[i].Id > maxId)
                            maxId = _workers[i].Id;
                    }
                }
                return maxId;
            }
        }
    }
}

