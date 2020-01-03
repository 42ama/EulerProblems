using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EulerProblems.Model.Utility.Helpers;
using EulerProblems.Model.Utility.Interface;
using EulerProblems.Model.Utility.DataContainers;
using EulerProblems.Model.Utility.Services;
using EulerProblems.Model.Utility.Extensions;

namespace EulerProblems.Model.Solutions.From_1_to_100
{
    public class ThirtyEight
    {
        const string content =
            "Возьмем число 192 и умножим его по очереди на 1, 2 и 3:" +
            "\n192 × 1 = 192" +
            "\n192 × 2 = 384" +
            "\n192 × 3 = 576" +
            "\nОбъединяя все три произведения, получим девятизначное число 192384576 из цифр от 1 до 9 (пан-цифровое число). Будем называть число 192384576 объединенным произведением 192 и (1,2,3) Таким же образом можно начать с числа 9 и по очереди умножать его на 1, 2, 3, 4 и 5, что в итоге дает пан-цифровое число 918273645, являющееся объединенным произведением 9 и (1,2,3,4,5). Какое самое большое девятизначное пан-цифровое число можно образовать как объединенное произведение целого числа и (1,2, ... , n), где n > 1?";

        private int Calculate()
        {
            int max = 0;
            // начинаем с самого большого массива (1..9) и спускаемся до (1, 2)
            for (int i = 9; i > 1; i--)
            {
                // формируем массив
                int[] digits = new int[i];
                for (int j = 0; j < i; j++)
                {
                    digits[j] = j+1;
                }

                //проверяем все произведения начиная с числа 1 до того момента как длина произведения не станет больше 9и
                int number = 1;
                var product = new StringBuilder();
                while (true)
                {
                    product = new StringBuilder();
                    foreach (var digit in digits)
                    {
                        product.Append(number * digit);
                    }
                    
                    // если число не 9изначное, пропускаем его
                    if(product.Length < 9)
                    {
                        number++;
                        continue;
                    }
                    else if(product.Length > 9)
                    {
                        break;
                    }
                    

                    if(Helper.IsPandigital(product.ToString(), 9))
                    {
                        int productNum = Int32.Parse(product.ToString());
                        if (productNum > max)
                        {
                            max = productNum;
                        }
                    }
                    number++;
                }
            }
            return max;
        }

        private void CalculateBench()
        {
            Calculate();
        }


        public ProblemContainer CompleteProblem()
        {
            ProblemContainer pc = new ProblemContainer()
            {
                Number = 38,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Какое самое большое девятизначное пан-цифровое число можно образовать как объединенное произведение целого числа и (1,2, ... , n), где n > 1?",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench)
            });

            return pc;
        }
    }
}

