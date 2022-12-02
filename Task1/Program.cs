using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Сформировать массив случайных целых чисел (размер  задается пользователем).
// Вычислить сумму чисел массива и максимальное число в массиве.
// Реализовать  решение  задачи  с  использованием  механизма  задач продолжения.


namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размерность массива чисел: ");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(GetSum);
            Task task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(GetMax);
            Task task3 = task1.ContinueWith<int>(func3);

            task1.Start();
            Console.ReadKey();
        }
        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random r = new Random();
            Console.Write("Массив: ");
            for (int i = 0; i < n; i++)
            {
                array[i] = r.Next(0, 100);
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
            return array;
        }

        static int GetSum(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                sum += array[i];
            }
            Console.WriteLine($"Сумма чисел массива = {sum}");
            return sum;
        }

        static int GetMax(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                if (max < array[i])
                    max = array[i];
            }
            Console.WriteLine($"Максимальное число = {max}");
            return max;
        }
    }
}
