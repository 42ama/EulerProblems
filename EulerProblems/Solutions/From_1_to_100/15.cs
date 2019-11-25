using System.Linq;
using System;
using System.Numerics;
using System.Collections.Generic;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;

namespace EulerProblems.Solutions.From_1_to_100
{
    struct PossibleDirections
    {
        public const string Down = "down";
        public const string Right = "right";
    }
    public class Fiveteen
    {
        const string content =
            "Начиная в левом верхнем углу сетки 2×2 и имея возможность двигаться только вниз или вправо, " +
            "существует ровно 6 маршрутов до правого нижнего угла сетки. " +
            "Сколько существует таких маршрутов в сетке 20×20?";
        string[] possibleDirections = new string[2] { PossibleDirections.Down, PossibleDirections.Right };

        // сгенерировать список всех возможных маршрутов
        private IEnumerable<IEnumerable<string>> GenerateAllRoutes(int routeLength)
        {
            List<string[]> routes = new List<string[]>();

            for (long i = 0; i < Math.Pow(2, routeLength); i++)
            {
                string[] route = new string[routeLength];
                long changebleI = i;
                for (int j = 0; j < routeLength; j++)
                {
                    if (changebleI % 2 == 1)
                    {
                        route[j] = PossibleDirections.Down;
                    }
                    else
                    {
                        route[j] = PossibleDirections.Right;
                    }
                    changebleI /= 2;
                }
                routes.Add(route);
            }

            return routes;
        }

        // проверяем маршрут на валидность
        // проверяем проходя маршрут из начала в конец, если финальные координаты совпадают с необходимыми
        // считаем, что маршрут валиден.
        private bool IsRouteValidAlg1(IEnumerable<string> route, int dimensionSide)
        {
            int x = 0, y = 0;
            foreach (var point in route)
            {
                switch (point)
                {
                    case PossibleDirections.Down:
                        y++;
                        break;
                    case PossibleDirections.Right:
                        x++;
                        break;
                }
            }
            if (x == dimensionSide && y == dimensionSide)
            {
                return true;
            }
            return false;
        }

        // проверяем маршрут на валидность
        // если в маршруте количество проходов вниз и вправо равно, то он считается валидным
        private bool IsRouteValidAlg2(IEnumerable<string> route)
        {
            if(route.Count(i => i == PossibleDirections.Down) == route.Count(i => i == PossibleDirections.Right))
            {
                return true;
            }
            return false;
        }

        // собираем список всех возможных маршрутов и проверяем каждый на то является ли он решающим задачу
        private long CalculateSlow(int dimensionSide)
        {
            // длина любого маршрута
            int routeLength = dimensionSide * 2;

            // собираем все маршруты
            var routes = GenerateAllRoutes(routeLength);

            // посчитать количество маршрутов которые доведут до цели
            int routesValid = 0;
            foreach (var route in routes)
            {
                if(IsRouteValidAlg1(route, dimensionSide))
                {
                    routesValid++;
                }
            }
            
            return routesValid;
        }

        // считаем по формуле перестановок с повторениями
        private BigInteger CalculateFast(int dimensionSide)
        {
            int routeLength = dimensionSide * 2;
            return Helper.Factorial(routeLength) / (Helper.Factorial(dimensionSide) * Helper.Factorial(dimensionSide));
        }

        private void CalculateSlow2()
        {
            CalculateSlow(2);
        }

        private void CalculateSlow10()
        {
            CalculateSlow(10);
        }
        private void CalculateFast2()
        {
            CalculateFast(2);
        }

        private void CalculateFast10()
        {
            CalculateFast(10);
        }

        private void CalculateFast20()
        {
            CalculateFast(20);
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 15,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько существует таких маршрутов в сетке 2×2? Быстрый алгоритм",
                Result = CalculateFast(2).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateFast2)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько существует таких маршрутов в сетке 10×10? Быстрый алгоритм",
                Result = CalculateFast(10).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateFast10)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько существует таких маршрутов в сетке 20×20? Быстрый алгоритм",
                Result = CalculateFast(20).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateFast20)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько существует таких маршрутов в сетке 2×2? Медленный алгоритм",
                Result = CalculateSlow(2).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateSlow2)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько существует таких маршрутов в сетке 10×10? Медленный алгоритм",
                Result = CalculateSlow(10).ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateSlow10, 2, 3)
            });

            return pc;
        }
    }
}
