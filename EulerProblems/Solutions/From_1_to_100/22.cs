using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using EulerProblems.Utility.Helpers;
using EulerProblems.Utility.Interface;
using EulerProblems.Utility.DataContainers;
using EulerProblems.Utility.Services;

namespace EulerProblems.Solutions.From_1_to_100
{
    public class TwentyTwo
    {
        const string content =
            "Используйте 22_names.txt. Начните с сортировки в алфавитном порядке. " +
            "Затем подсчитайте алфавитные значения каждого имени и умножьте это значение на " +
            "порядковый номер имени в отсортированном списке для получения количества очков имени. " +
            "Например, если список отсортирован по алфавиту, имя COLIN (алфавитное значение " +
            "которого 3 + 15 + 12 + 9 + 14 = 53) является 938-ым в списке. Поэтому, имя " +
            "COLIN получает 938 × 53 = 49714 очков. Какова сумма очков имен в файле?";

        static string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources\22_names.txt");

        static IDictionary<char, int> alphabets = new Dictionary<char, int>
        {
            {'A', 1}, {'B', 2}, {'C', 3}, {'D', 4}, {'E', 5}, {'F', 6}, {'G', 7}, {'H', 8}, {'I', 9},
            {'J', 10}, {'K', 11}, {'L', 12}, {'M', 13}, {'N', 14}, {'O', 15}, {'P', 16}, {'Q', 17}, {'R', 18},
            {'S', 19}, {'T', 20}, {'U', 21}, {'V', 22}, {'W', 23}, {'X', 24}, {'Y', 25}, {'Z', 26}
        };

        private string[] Parse(string pathToFile)
        {
            var notParsedText = File.ReadAllText(pathToFile);
            var s = notParsedText.Split(',');
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = s[i].Substring(1, (s[i].Length - 2));
            }
            return s;
        }

        private T[] QSort<T>(T[] collection) where T : IComparable
        {
            if (collection.Count() < 2)
            {
                return collection;
            }
            else
            {
                T pivot = collection[0];

                var less = new List<T>();
                var greater = new List<T>();

                for (int i = 1; i < collection.Length; i++)
                {
                    if(collection[i].CompareTo(pivot) < 0)
                    {
                        less.Add(collection[i]);
                    }
                    else
                    {
                        greater.Add(collection[i]);
                    }
                }

                var lessSorted = QSort(less.ToArray());
                var greaterSorted = QSort(greater.ToArray());
                T[] sorted = new T[lessSorted.Length + 1 + greaterSorted.Length];

                Array.Copy(lessSorted, sorted, lessSorted.Length);
                sorted[lessSorted.Length] = pivot;
                Array.Copy(greaterSorted, 0, sorted, lessSorted.Length + 1, greaterSorted.Length);

                return sorted;
            }
        }

        private T[] QSortLinq<T>(T[] collection) where T : IComparable
        {
            if (collection.Count() < 2)
            {
                return collection;
            }
            else
            {
                T pivot = collection[0];

                var less = collection[1..].Where(i => i.CompareTo(pivot) < 0);
                var greater = collection[1..].Where(i => i.CompareTo(pivot) >= 0);

                var lessSorted = QSortLinq(less.ToArray());
                var greaterSorted = QSortLinq(greater.ToArray());
                T[] sorted = new T[lessSorted.Length + 1 + greaterSorted.Length];

                Array.Copy(lessSorted, sorted, lessSorted.Length);
                sorted[lessSorted.Length] = pivot;
                Array.Copy(greaterSorted, 0, sorted, lessSorted.Length + 1, greaterSorted.Length);

                return sorted;
            }
        }

        private T[] MergeSort<T>(T[] collection) where T : IComparable
        {
            if (collection.Count() < 2)
            {
                return collection;
            }
            else
            {
                int pivot = collection.Length / 2;

                var arrayA = collection[..pivot];
                var arrayB = collection[pivot..];

                var sortedArrayA = MergeSort(arrayA);
                var sortedArrayB = MergeSort(arrayB);

                int i = 0; // sortedArrayA increment
                int j = 0; // sortedArrayB increment
                int k = 0; // result increment
                var sorted = new T[collection.Length];

                while(i != sortedArrayA.Length && j != sortedArrayB.Length)
                {
                    if (sortedArrayA[i].CompareTo(sortedArrayB[j]) < 0)
                    {
                        sorted[k] = sortedArrayA[i];
                        i++;
                        k++;
                    }
                    else
                    {
                        sorted[k] = sortedArrayB[j];
                        j++;
                        k++;
                    }

                    if(i == sortedArrayA.Length && j != sortedArrayB.Length)
                    {
                        Array.Copy(sortedArrayB, j, sorted, k, sortedArrayB.Length - j);
                        break;
                    }
                    else if(i != sortedArrayA.Length && j == sortedArrayB.Length)
                    {
                        Array.Copy(sortedArrayA, i, sorted, k, sortedArrayA.Length - i);
                        break;
                    }
                }

                return sorted;
            }
        }

        private int CountPoints(string[] sortedNames)
        {
            int summ = 0;
            for (int i = 0; i < sortedNames.Length; i++)
            {
                int littleSum = 0;
                foreach (char letter in sortedNames[i])
                {
                    littleSum += alphabets[letter];
                }
                summ += littleSum * (i + 1);
            }
            return summ;
        }


        private int CalculateQSort()
        {
            var names = Parse(path);
            var sortedNames = QSort(names);
            return CountPoints(sortedNames);
        }

        private int CalculateQSortLinq()
        {
            var names = Parse(path);
            var sortedNames = QSortLinq(names);
            return CountPoints(sortedNames);
        }

        private long CalculateDefaultSort()
        {
            var names = Parse(path);
            Array.Sort(names);
            return CountPoints(names);
        }

        private int CalculateMergeSort()
        {
            var names = Parse(path);
            var sortedNames = MergeSort(names);
            return CountPoints(sortedNames);
        }


        private void CalculateQSortBench()
        {
            CalculateQSort();
        }

        private void CalculateQSortLinqBench()
        {
            CalculateQSortLinq();
        }

        private void CalculateDefaultSortBench()
        {
            CalculateDefaultSort();
        }

        private void CalculateMergeSortBench()
        {
            CalculateMergeSort();
        }

        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 22,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какова сумма очков имен в файле? quicksort",
                Result = CalculateQSort().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateQSortBench)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какова сумма очков имен в файле? quicksort с linq",
                Result = CalculateQSortLinq().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateQSortLinqBench)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какова сумма очков имен в файле? mergesort",
                Result = CalculateMergeSort().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateMergeSortBench)
            });

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какова сумма очков имен в файле? Array.Sort()",
                Result = CalculateDefaultSort().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateDefaultSortBench)
            });

            return pc;
        }
    }
}

