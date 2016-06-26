using System;
using System.Collections.Generic;
using System.Linq;

namespace CircularPrimeNumbers
{
    class Program
    {
        static int upTo = 1000000; //верхняя граница чисел для проверки

        static int amountOfCircularPrime = 0; //количество circular prime numbers в диапазоне [1, upTo]

        static bool isPrime(int test_number) //проверка простоты числа test_number
        {
            if (test_number == 1)
                return false;
            else if (test_number == 2)
                return true;
            else
            {
                int min_range = 3;
                int max_range = Convert.ToInt32(Math.Truncate(Math.Sqrt(test_number))) + 1; //округленное значение квадратного корня (test_number)
                int r = test_number % 10; //остаток от деления test_number на 10
                if (r == 0 || r == 2 || r == 4 || r == 6 || r == 8) //проверяем test_number на четность
                    return false;
                for (int i = min_range; i <= max_range; i += 2)
                    if (test_number % i == 0)
                        return false;
                return true;
            }
        }

        static int[] parseNumber(int number) //разбиваем number на цифры
        {
            int[] figures = new int[number.ToString().Length]; //массив цифр числа number
            for (int i = figures.Length - 1; i > -1; i--)
            {
                figures[i] = number % 10;
                number = number / 10;
            }
            return figures;
        }

        static int[] shiftOf(int number) //сдвиг числа number
        {
            int[] figures = parseNumber(number); //массив цифр числа number
            int[] shiftNumbers = new int[figures.Length - 1]; //массив смещенных чисел
            int temp; //временное хранение первого элемента массива figures

            for (int i = 0; i < figures.Length - 1; i++)
            {
                temp = figures[0];
                for (int j = 0; j < figures.Length - 1; j++)
                {
                    figures[j] = figures[j + 1];
                    shiftNumbers[i] += figures[j] * Convert.ToInt32(Math.Pow(10, figures.Length - j - 1));
                }
                figures[figures.Length - 1] = temp;
                shiftNumbers[i] += temp;
            }
            return shiftNumbers;
        }

        static bool check(int number, string s) //проверяем, входит ли хотя бы одна из цифр 0, 2, 4, 6, 8 в number
        {
            foreach (char c in s)
            {
                if (number.ToString().Contains(c))
                    return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            List<int> primeNumb = new List<int>(); //список простых чисел в диапазоне [1, upTo]
            for (int i = 1; i <= upTo; i++)
                if (isPrime(i))
                    primeNumb.Add(i);

            int counter; //счетчик кол-ва простых чисел для массива сдвинутых чисел
            for (int i = 0; i < primeNumb.Count; i++)
            {
                counter = 1;
                if (check(primeNumb[i], "02468"))
                    continue;
                else
                {
                    int[] shiftPrimeNumb = shiftOf(primeNumb[i]);
                    for (int j = 0; j < shiftPrimeNumb.Length; j++)
                        if (primeNumb.Contains(shiftPrimeNumb[j]))
                            counter++;
                        else break;
                    if (counter == primeNumb[i].ToString().Length)
                        amountOfCircularPrime++;
                }
            }
            if (upTo != 1)
                amountOfCircularPrime++; //т.к. ф-ция check пропускает число 2, которое является circular prime number
            Console.WriteLine("amountOfCircularPrime: {0}", amountOfCircularPrime);
            Console.ReadLine();
        }
    }
}
