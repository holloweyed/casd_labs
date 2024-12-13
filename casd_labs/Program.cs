using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casd_labs
{
    public class Program
    {
        static void Main(string[] args)
        {
            MyArrayDeque<string> deque = new MyArrayDeque<string>();
            string[] lines = File.ReadAllLines("C:\\Users\\galuz\\Documents\\input.txt");

            foreach (var line in lines)
            {
                int digitCount = line.Count(char.IsDigit);
                int referenceDigitCount = deque.IsEmpty() ? 0 : deque.Element().Count(char.IsDigit);

                if (digitCount > referenceDigitCount)
                {
                    deque.Add(line);
                }
                else
                {
                    deque.Push(line);
                }
            }

            using (StreamWriter writer = new StreamWriter("sorted.txt"))
            {
                var resultArray = deque.ToArray();
                foreach (var str in resultArray)
                {
                    writer.WriteLine(str);
                }
            }

            Console.Write("Введите максимально доступное количество пробелов в строке: ");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Некорректный ввод. Введите число.");
            }

            string[] elems = deque.ToArray();

            for (int i = 0; i < elems.Length; i++)
            {
                int a = elems[i].Count(c => c == ' ');
                if (a > n)
                {
                    deque.Remove(elems[i]);
                }
            }

            Console.WriteLine("Строки, содержащие не более n пробелов: ");
            while (!deque.IsEmpty())
            {
                Console.WriteLine(deque.RemoveFirst());
            }
            Console.WriteLine("Данные записаны в файл: " + Directory.GetCurrentDirectory() + "\\sorted.txt");
            Console.Read();
        }
    }
}