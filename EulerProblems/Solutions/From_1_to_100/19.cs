using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;

namespace EulerProblems.Solutions.From_1_to_100
{
    public enum DayOfTheWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public enum Month
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    public static class NineteenDateModelExtensions
    {
        private static Dictionary<Month, int> monthLength = new Dictionary<Month, int>
        {
            { Month.January, 31 },
            { Month.February, 28 },
            { Month.March, 31 },
            { Month.April, 30 },
            { Month.May, 31 },
            { Month.June, 30 },
            { Month.July, 31 },
            { Month.August, 31 },
            { Month.September, 30 },
            { Month.October, 31 },
            { Month.November, 30 },
            { Month.December, 31 },
        };
        public static int ToInt32(this Month month)
        {
            return month switch
            {
                Month.January => 1,
                Month.February => 2,
                Month.March => 3,
                Month.April => 4,
                Month.May => 5,
                Month.June => 6,
                Month.July => 7,
                Month.August => 8,
                Month.September => 9,
                Month.October => 10,
                Month.November => 11,
                Month.December => 12
            };
        }

        public static Month ToMonth(this int month)
        {
            return month switch
            {
                1 => Month.January,
                2 => Month.February,
                3 => Month.March,
                4 => Month.April,
                5 => Month.May,
                6 => Month.June,
                7 => Month.July,
                8 => Month.August,
                9 => Month.September,
                10 => Month.October,
                11 => Month.November,
                12 => Month.December,
                _ => throw new ArgumentException("Число должно быть в интервале от 1 до 12")
            };
        }

        public static Month NextMonth(this Month month)
        {
            return month switch
            {
                Month.December => Month.January,
                Month.January => Month.February,
                Month.February => Month.March,
                Month.March => Month.April,
                Month.April => Month.May,
                Month.May => Month.June,
                Month.June => Month.July,
                Month.July => Month.August,
                Month.August => Month.September,
                Month.September => Month.October,
                Month.October => Month.November,
                Month.November => Month.December
            };
        }

        public static int ToInt32(this DayOfTheWeek dayOfTheWeek)
        {
            return dayOfTheWeek switch
            {
                DayOfTheWeek.Monday => 0,
                DayOfTheWeek.Tuesday => 1,
                DayOfTheWeek.Wednesday => 2,
                DayOfTheWeek.Thursday => 3,
                DayOfTheWeek.Friday => 4,
                DayOfTheWeek.Saturday => 5,
                DayOfTheWeek.Sunday => 6
            };
        }

        public static DayOfTheWeek ToDayOfTheWeek(this int dayOfTheWeek)
        {
            return dayOfTheWeek switch
            {
                0 => DayOfTheWeek.Monday,
                1 => DayOfTheWeek.Tuesday,
                2 => DayOfTheWeek.Wednesday,
                3 => DayOfTheWeek.Thursday,
                4 => DayOfTheWeek.Friday,
                5 => DayOfTheWeek.Saturday,
                6 => DayOfTheWeek.Sunday,
                _ => throw new ArgumentException("Число должно быть в интервале от 0 до 6")
            };
        }

        public static int GetLength(this Month month)
        {
            return monthLength[month];
        }
    }
    public class Nineteen
    {
        const string content =
            "Дана следующая информация (однако, вы можете проверить ее самостоятельно): " +
            "1 января 1900 года - понедельник. " +
            "В апреле, июне, сентябре и ноябре 30 дней. " +
            "В феврале 28 дней, в високосный год - 29. В остальных месяцах по 31 дню. " +
            "Високосный год - любой год, делящийся нацело на 4, однако последний год века(ХХ00) " +
            "является високосным в том и только том случае, если делится на 400. " +
            "Сколько воскресений выпадает на первое число месяца в двадцатом веке " +
            "(с 1 января 1901 года до 31 декабря 2000 года)?";

        private class EulerDate
        {
            public readonly DateTime UpperBound;
            public DayOfTheWeek DayOfTheWeek { get; protected set; }
            public int Year { get; protected set; }
            public Month Month { get; protected set; }
            public int Day { get; protected set; }

            public bool IsUpperBoundReached 
            { 
                get
                {
                    if (Year >= UpperBound.Year)
                    {
                        return true;
                    }
                    return false;
                } 
            }



            public EulerDate(int year, int month, int day, DayOfTheWeek dayOfTheWeek, DateTime upperBound)
            {
                Day = day;
                Month = month.ToMonth();
                Year = year;
                DayOfTheWeek = dayOfTheWeek;
                UpperBound = upperBound;
            }

            public EulerDate(int year, int month, int day, DayOfTheWeek dayOfTheWeek, int upperBoundYear, int upperBoundMonth, int upperBoundDay)
                : this(year, month, day, dayOfTheWeek, new DateTime(upperBoundYear, upperBoundMonth, upperBoundDay)) { }


            public EulerDate(DateTime startDate, DayOfTheWeek startDateDayOfTheWeek, DateTime upperBound)
                : this(startDate.Year, startDate.Month, startDate.Day, startDateDayOfTheWeek, upperBound) { }



            /// <summary>
            /// Устанавливает для данного экземляра следующий месяц
            /// </summary>
            /// <returns>Возвращает день недели первого числа месяца</returns>
            public DayOfTheWeek NextMonth()
            {
                int daysToAdd = 0;
                if(Month == Month.February && IsYearLeap())
                {
                    daysToAdd++;
                }
                if(Month == Month.December)
                {
                    Year++;
                }
                daysToAdd += Month.GetLength();

                // добавляем к количеству добавляемых дней текущей день недели 
                // для поправки дня неделя относительно её начала
                DayOfTheWeek = ((daysToAdd + DayOfTheWeek.ToInt32()) % 7 ).ToDayOfTheWeek();
                Month = Month.NextMonth();

                return DayOfTheWeek;
            }
            
            public bool IsYearLeap()
            {
                //Високосный год - любой год, делящийся нацело на 4, однако последний год века (ХХ00)
                //является високосным в том и только том случае, если делится на 400.
                if ((Year % 4 == 0 && Year % 100 != 0) || (Year % 100 == 0 && Year % 400 == 0))
                {
                    return true;
                }
                return false;

            }
        }

        /// <summary>
        /// Считает количество воскресений выпавших на первое число месяца начиная с 
        /// <c>startYear</c> включительно и до <c>endYear</c> невключительно 
        /// </summary>
        /// <param name="startYear">Год начала отсчета, должен быть больше 1900</param>
        /// <param name="endYear">Год конца отсчета, невключительно</param>
        /// <returns></returns>
        private int Calculate(int startYear = 1901, int endYear = 2001)
        {
            if(startYear < 1900)
            {
                throw new ArgumentException("Поддерживаются лишь года начиная с 1900");
            }
            // начинаем отсчет с первой даты для которой известен день недели
            var date = new EulerDate(1900, 1, 1, DayOfTheWeek.Monday, endYear, 1, 1);

            // увеличиваем дату до необходимой нам
            while(date.Year < startYear)
            {
                date.NextMonth();
            }

            // проверяем первое число, первого месяца нужного нам года
            int summ = 0;
            if (date.DayOfTheWeek == DayOfTheWeek.Sunday)
            {
                summ++;
            }

            // собираем информацию о воскресеньях приходящихся на первое число месяца за данный период
            do
            {
                if (date.NextMonth() == DayOfTheWeek.Sunday)
                {
                    summ++;
                }
                //Console.WriteLine($"{date.Year}/{date.Month.ToInt32()}/{date.Day} {date.DayOfTheWeek}");
            }
            while (!date.IsUpperBoundReached);

            return summ;
        }

        private void CalculateBench()
        {
            Calculate();
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 19,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько воскресений выпадает на первое число месяца в двадцатом веке?",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

