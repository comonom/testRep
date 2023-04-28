using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF2022User08Lib
{
    //не забывай правильное название класса, проекта, метода, возвращаемое значение, входные данные (с такими же именами!)
    public class Calculations
    {
        public string[] AvailablePeriods(
            TimeSpan[] startTimes,
            int[] durations,
            TimeSpan beginWorkingTime,
            TimeSpan endWorkingTime,
            int consultationTime)
        {
            //сюда будем складывать строки свободных промежутков. в конце конвертируем в массив
            List<string> freeTimes = new List<string>();

            //Время консультации из int перебиваем в timespan (30 -> 00:30:00)
            TimeSpan consultationSpan = TimeSpan.FromMinutes(consultationTime);
            TimeSpan currentSpan = beginWorkingTime; //это наша переменная, которая будет обозначать ТЕКУЩЕЕ значение которое
            //мы проверям на свободный промежуток. По началу это будет начало рабочего дня.
            var busyRanges = GetBusyRanges(startTimes, durations); // получем занятые промежутки (из startTime и durations)
            //перекидываем в объекты класса Range

            //Пока просматриваемый спан меньше чем конец рабочего дня
            while(currentSpan < endWorkingTime)
            {
                //перебиваем спан и время консультации в Range
                var currentRange = new Range
                {
                    Start = currentSpan,
                    End = currentSpan + consultationSpan,
                };

                //проходимся по листу busyRanges, проверяя каждый на то, что они пересекаются
                //Вернется первый объект Range, который пересекся с проверяемым промежутком (текущим)
                //Не вернется ничего (null) если пересечений нету
                var intersect = busyRanges.FirstOrDefault(f => IsInRange(f, currentRange));
                //Если пересечений нет - значит мы можем как минимум прибавить к текущему промежутку (currentSpan)
                //прибавить время консультации (это ещё служит выходом из цикла while, т.к. смотри условие)
                if(intersect == null)
                {
                    //ВНИМАНИЕ. МОЖЕТ ПРОИЗОЙТИ ТАКАЯ СИТУАЦИЯ ЧТО У НАС ПРОМЕЖУТОК НАПРИМЕР 17:50-18:20
                    //НО КОНЕЦ РАБОЧЕГО ДНЯ ТО В 18 00, ТОГДА НАМ ПРОСТО НАДО ПРИБАВИТЬ К ТЕКУЩЕМУ СПАНУ ВРЕМЯ КОНСУЛЬТАЦИИ
                    //И В WHILE ЦИКЛЕ УСЛОВИЕ БУДЕТ FALSE, ЧТО ПОЗВОЛИТ ЗАКОНЧИТЬ ЦИКЛ.
                    //НО если время в норме - просто добавляем в список
                    if(currentRange.End <= endWorkingTime)
                        freeTimes.Add($"{currentRange.Start.ToString(@"hh\:mm")}-{currentRange.End.ToString(@"hh\:mm")}");

                    //в любом случае если промежуток не пересекся ни с чем, мы добавим время консультации к текущему спану
                    currentSpan += consultationSpan;
                }
                else
                {
                    //ЕСЛИ пересечение есть, то мы ставим текущий спан как КОНЕЦ промежутка, с которым случилось пересечение
                    //он нам вернулся благодаря firstOrDefault и записан в intersect переменную
                    currentSpan = intersect.End;
                }
            }

            //перебиваем в массив
            return freeTimes.ToArray();
        }

        //Наш метод, который из массива таймспанов и массива количества минут делает список range
        private List<Range> GetBusyRanges(TimeSpan[] startTimes, int[] durations)
        {
            // select - формирует новый лист, туда мы засовываем объекты (new Range), где start - сам таймСпан (s)
            //который содержится в startTimes и сейчас его смотрим. i - индекс просматриваемого объекта в листе
            //end вычисляем как текущий просматриваемый таймспан + таймспан из минут durations по просматриваемому индексу.
            return startTimes.Select((s, i) => new Range
            {
                Start = s,
                End = s + TimeSpan.FromMinutes(durations[i])
            }).OrderBy(o => o.Start).ToList(); // не забудь перекинуть в лист. на всякий случай ещё отсортируем их
        }

        private bool IsInRange(Range r1, Range r2)
        {
            //ФУНКЦИЯ ПРОВЕРКИ НА ТО ЧТО РЕНДЖИ R1 И R2 ПЕРЕСЕКАЮТСЯ!!!!!!!!!! ЗАПОМНИТЬ
            //ОЧЕНЬ ЛЕГКОЕ УСЛОВИЕ. ЕСЛИ КАЖДЫЙ СТАРТ РЕНДЖА МЕНЬШЕ КОНЦА ДРУГОГО - ОНИ ПЕРЕСЕКЛИСЬ.
            //TRUE - ЕСЛИ ПЕРЕСЕКЛИСЬ, FALSE - нЕТ.
            return r1.Start < r2.End && r2.Start < r1.End;
        }
    }
}
