using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;
using System.Text;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class FiftyFive
    {
        const string content =
            "Если взять число 47, перевернуть его и прибавить к исходному, т.е. найти 47 + 74 = 121, получится палиндром." +
            "Не из всех чисел таким образом сразу получается палиндром. К примеру," +
            "\n349 + 943 = 1292" +
            "\n1292 + 2921 = 4213" +
            "\n4213 + 3124 = 7337" +
            "\nТ.е., понадобилось 3 итерации для того, чтобы превратить число 349 в палиндром. " +
            "Хотя никто еще этого не доказал, считается, что из некоторых чисел, таких как 196, невозможно получить палиндром.Такое число, которое не образует палиндром путем переворачивания и сложения с самим собой, называется числом Личрэла.Ввиду теоретической природы таких чисел, а также цели этой задачи, мы будем считать, что число является числом Личрэла до тех пор, пока не будет доказано обратное.Помимо этого дано, что любое число меньше десяти тысяч либо (1) станет палиндромом меньше, чем за 50 итераций, либо(2) никто, с какой бы-то ни было вычислительной мощностью, не смог получить из него палиндром.Между прочим, 10677 является первым числом, для которого необходимо более 50 итераций, чтобы получить палиндром: 4668731596684224866951378664 (53 итерации, 28-значное число). " +
            "\nНа удивление, есть такие палиндромы, которые одновременно являются и числами Личрэла; первое такое число - 4994.  " +
            "Сколько существует чисел Личрэла меньше десяти тысяч?";


        private int Calculate()
        {
            int count = 0;
            for (int i = 10; i < 10000; i++)
            {
                if(IsLychrel(i))
                {
                    count++;
                }
            }
            return count;
        }

        private bool IsLychrel(int number, int counterBound = 50)
        {
            int counter = 1;
            var bigNumber = new BigInteger(number);
            // согласно условию проходимся n раз, алгоритмом преобразования в палиндромы
            // если палиндромов за это время не обнаружено, данное число - число Лирчэла
            while(counter <= counterBound)
            {
                bigNumber = bigNumber + BigInteger.Parse(Reverse(bigNumber.ToString()));
                if(Helper.IsPalindrome(bigNumber.ToString().ToCharArray()))
                {
                    return false;
                }
                counter++;
            }
            return true;
        }

        /// <summary>
        /// Переворачивает строку, возвращает перевернутую версию
        /// </summary>
        /// <param name="line">Строка, которую необходимо перевернуть</param>
        /// <returns>Перевернутная исходная сторка</returns>
        private string Reverse(string line)
        {
            var sb = new StringBuilder();
            for (int i = line.Length - 1; i >= 0; i--)
            {
                sb.Append(line[i]);
            }
            return sb.ToString();
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 55,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сколько существует чисел Личрэла меньше десяти тысяч?",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

