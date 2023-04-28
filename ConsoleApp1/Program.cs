using SF2022User08Lib;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //здесь просто входные данные 
            var busySpans = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
                new TimeSpan(16, 50, 0),
            };

            var busyDurations = new int[]
            {
                60, 30, 10, 10, 40
            };
            TimeSpan start = new TimeSpan(8, 0, 0);
            TimeSpan end = new TimeSpan(18, 0, 0);

            var calc = new Calculations();
            //чтобы протестировать библиотеку - в зависимостях (references) добавим ссылку на проект SF2022USER08LIB
            //после этого мы создаем объект этого класса и вызываем метод (может ругаться, нужно будет добавить using)
            var result = calc.AvailablePeriods(busySpans, busyDurations, start, end, 30);
            //На фигурную скобку снизу вешаем брейкпоинт, меняем автозапускаемый проект на консоль и можем тестировать
            //если закончила делать либу - удали консольный проект
        }
    }
}
