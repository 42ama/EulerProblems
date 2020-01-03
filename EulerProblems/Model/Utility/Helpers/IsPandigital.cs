using System;
using System.Linq;
using System.Collections.Generic;

namespace EulerProblems.Model.Utility.Helpers
{
    public static partial class Helper
    {
        // Каждое n-значное число, которое содержит каждую цифру от 0 или 1 до n ровно один раз, будем считать пан-цифровым

        /// <summary>
        /// Проверяет является ли заданное число пан-цифровым по основанию panBase
        /// </summary>
        /// <param name="panBase">Основание пан-цифрового числа. Должно быть в пределах от 1 до 9и.</param>
        /// <param name="startFromZero">Если true, то первым числом считается 0 иначе 1.</param>
        /// <returns>true - если число пан-цифровое по заданому основнию, false - если иначе</returns>
        public static bool IsPandigital(int number, int panBase, bool startFromZero = false)
        {
            return IsPandigital(number.ToString(), panBase, startFromZero);
        }

        /// <summary>
        /// Проверяет является ли заданное число пан-цифровым по основанию panBase
        /// </summary>
        /// <param name="panBase">Основание пан-цифрового числа. Должно быть в пределах от 1 до 9и.</param>
        /// <param name="startFromZero">Если true, то первым числом считается 0 иначе 1.</param>
        /// <returns>true - если число пан-цифровое по заданому основнию, false - если иначе</returns>
        public static bool IsPandigital(long number, int panBase, bool startFromZero = false)
        {
            return IsPandigital(number.ToString(), panBase, startFromZero);
        }

        /// <summary>
        /// Проверяет является ли заданное число пан-цифровым по основанию panBase
        /// </summary>
        /// <param name="panBase">Основание пан-цифрового числа. Должно быть в пределах от 1 до 9и.</param>
        /// <param name="startFromZero">Если true, то первым числом считается 0 иначе 1.</param>
        /// <returns>true - если число пан-цифровое по заданому основнию, false - если иначе</returns>
        public static bool IsPandigital(string number, int panBase, bool startFromZero = false)
        {
            return IsPandigital(number.ToCharArray(), panBase, startFromZero);
        }

        /// <summary>
        /// Проверяет является ли заданное число пан-цифровым по основанию panBase
        /// </summary>
        /// <param name="panBase">Основание пан-цифрового числа. Должно быть в пределах от 1 до 9и.</param>
        /// <param name="startFromZero">Если true, то первым числом считается 0 иначе 1.</param>
        /// <returns>true - если число пан-цифровое по заданому основнию, false - если иначе</returns>
        public static bool IsPandigital(char[] number, int panBase, bool startFromZero = false)
        {
            IsPanBaseValid(panBase);

            HashSet<char> digits = new HashSet<char>();
            char[] panBaseValues;
            int startingCharValue;
            int endingCharValue;

            if(startFromZero)
            {
                startingCharValue = 47;
                endingCharValue = panBase + startingCharValue + 1;
                panBaseValues = new char[panBase + 1];
            }
            else
            {
                startingCharValue = 48;
                endingCharValue = panBase + startingCharValue;
                panBaseValues = new char[panBase];
            }

            // набирает массив символов начиная с 0 или 1 до panBase
            for (int i = startingCharValue; i < endingCharValue; i++)
            {
                panBaseValues[i - startingCharValue] = (char)(i + 1);
            }

            foreach (var digit in number)
            {
                if(digit == '0' && !startFromZero)
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
