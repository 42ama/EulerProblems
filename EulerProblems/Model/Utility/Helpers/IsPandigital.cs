using System;
using System.Linq;
using System.Collections.Generic;

namespace EulerProblems.Model.Utility.Helpers
{
    public static partial class Helper
    {
        // Каждое n-значное число, которое содержит каждую цифру от 1 до n ровно один раз, будем считать пан-цифровым
        /// <summary>
        /// Проверяет является ли заданное число пан-цифровым по основанию panBase (без учета 0я)
        /// </summary>
        /// <param name="panBase">Основание пан-цифрового числа. Должно быть в пределах от 1 до 9и.</param>
        /// <returns>true - если число пан-цифровое по заданому основнию, false - если иначе</returns>
        public static bool IsPandigital(int number, int panBase)
        {
            IsPanBaseValid(panBase);

            HashSet<int> digits = new HashSet<int>();
            var panBaseValues = new int[panBase];

            for (int i = 0; i < panBase; i++)
            {
                panBaseValues[i] = i+1;
            }

            while (number != 0)
            {
                int digit = number % 10;
                if (digit == 0)
                {
                    return false;
                }
                if (digits.Contains(digit))
                {
                    return false;
                }
                digits.Add(digit);
                number /= 10;
            }

            // не учитываем 0
            digits.Remove(0);
            
            if (panBaseValues.All(digits.Contains))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Каждое n-значное число, которое содержит каждую цифру от 1 до n ровно один раз, будем считать пан-цифровым
        /// <summary>
        /// Проверяет является ли заданное число пан-цифровым по основанию panBase (без учета 0я)
        /// </summary>
        /// <param name="panBase">Основание пан-цифрового числа. Должно быть в пределах от 1 до 9и.</param>
        /// <returns>true - если число пан-цифровое по заданому основнию, false - если иначе</returns>
        public static bool IsPandigital(char[] number, int panBase)
        {
            IsPanBaseValid(panBase);

            HashSet<char> digits = new HashSet<char>();
            var panBaseValues = new char[panBase];

            // набирает массив символов начиная с 1 до panBase
            for (int i = 48; i < panBase+48; i++)
            {
                panBaseValues[i-48] = (char)(i + 1);
            }

            foreach (var digit in number)
            {
                if(digit == '0')
                {
                    return false;
                }
                if (digits.Contains(digit))
                {
                    return false;
                }
                digits.Add(digit);
            }

            if (panBaseValues.All(digits.Contains))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Проверяет основание пан-цифрового числа, подходит ли оно заданным условиям. Выбрасывает ошибку в обратном случае.
        /// </summary>
        /// <param name="panBase">Основание пан-цифрового числа</param>
        private static void IsPanBaseValid(int panBase)
        {
            if (panBase < 1)
            {
                throw new ArgumentOutOfRangeException("panBase", "Основание пан-цифрового числа должно быть больше 0.");
            }
            if (panBase > 9)
            {
                throw new ArgumentOutOfRangeException("panBase", "Основание пан-цифрового числа больше 9 не поддерживается.");
            }
        }
    }
}
