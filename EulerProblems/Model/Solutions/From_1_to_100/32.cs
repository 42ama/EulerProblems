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
    public class ThirtyTwo
    {
        const string content =
            "Каждое n-значное число, которое содержит каждую цифру от 1 до n ровно один раз, будем считать пан-цифровым; к примеру, 5-значное число 15234 является пан-цифровым, т.к. содержит цифры от 1 до 5. Произведение 7254 является необычным, поскольку равенство 39 × 186 = 7254, состоящее из множимого, множителя и произведения является пан-цифровым, т.е.содержит цифры от 1 до 9. Найдите сумму всех пан-цифровых произведений, для которых равенство \"множимое × множитель = произведение\" можно записать цифрами от 1 до 9, используя каждую цифру только один раз. ПОДСКАЗКА: Некоторые произведения можно получить несколькими способами, поэтому убедитесь, что включили их в сумму лишь единожды.";
        
        private int Calculate()
        {
            var numbersToIgnore = new HashSet<int>();
            int summ = 0;
            // любое подходящее нам число будет иметь 9 знаков

            // нижняя граница ограничивается первым 4х значным числом, потому что 
            // 3знач. * 2знач. = 4знач. может существовать, 
            // в то время как 3знач * 3знач = 3знач. или 4знач. * 2знач. = 3знач. нет, а значит
            // и для всех чисел меньше 3знач. (2знач. и 1знач.) правило тоже не будет выполняться

            // по тому же принципу ограничиваем верхнюю границу первым 5и значным числом, так как 
            // 2знач. * 2знач. = 5 знач. или 3знач. * 1знач. = 5 знач. чисел не существует
            // и также для всех чисел больше 5знач. (6знач. и т.д.)

            for (int number = 1000; number < 10000; number++)
            {
                // пропускаем все числа в которых есть одинаковые символы
                if(!Helper.IsAllCharsUnique(number.ToString()))
                {
                    continue;
                }

                var divisors = Helper.DivisorsWithoutTypical(number);
                if (divisors.Count == 0)
                {
                    continue;
                }

                foreach(var divisor1 in divisors)
                {
                    var numberSB = new StringBuilder(number.ToString());
                    int divisor2 = number / divisor1;
                    numberSB.Append(divisor1);
                    numberSB.Append(divisor2);

                    // итоговое число должно иметь длину 9, не содержаться в уже отобранных и быть пан-цифровым
                    if (numberSB.Length == 9)
                    {
                        if (!numbersToIgnore.Contains(number))
                        {
                            if (Helper.IsPandigital(numberSB.ToString().ToCharArray(), 9))
                            {
                                summ += number;
                                numbersToIgnore.Add(number);
                            }
                        }
                        
                    }
                }
            }
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
                Number = 32,
                Task = content,
                Cases = new List<CaseContainer>()
            };

            pc.Cases.Add(new CaseContainer
            {
                Task = "Сумма всех пан-цифровых произведений, для которых равенство \"множимое × множитель = произведение\" можно записать цифрами от 1 до 9.",
                Result = Calculate().ToString(),
                BenchmarkResult = WatchstopBenchmark.Benchmark(CalculateBench,3,3)
            });

            return pc;
        }
    }
}

