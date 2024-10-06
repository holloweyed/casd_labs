using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static casd_labs.Program;

namespace casd_labs
{
    internal class Program
    {
        internal class Complex
        {
            private double Re;
            private double Im;

            public void CreateComplex()
            {
                string real, imaginary;
                do
                {
                    Console.WriteLine("Введите вещественную часть: ");
                    real = Console.ReadLine();

                    Console.WriteLine("Введите мнимую часть: ");
                    imaginary = Console.ReadLine();

                    if ((real.StartsWith("-") && !real.Substring(1).All(char.IsDigit)) || (!real.StartsWith("-") && !real.All(char.IsDigit)) || (imaginary.StartsWith("-") && !imaginary.Substring(1).All(char.IsDigit)) || (!imaginary.StartsWith("-") && !imaginary.All(char.IsDigit)))
                        Console.WriteLine("Ошибка, повторите ввод.");
                }
                while ((real.StartsWith("-") && !real.Substring(1).All(char.IsDigit)) || (!real.StartsWith("-") && !real.All(char.IsDigit)) || (imaginary.StartsWith("-") && !imaginary.Substring(1).All(char.IsDigit)) || (!imaginary.StartsWith("-") && !imaginary.All(char.IsDigit)));
                SetComplex(Convert.ToDouble(real), Convert.ToDouble(imaginary));
            }

            public void SetComplex(double real, double imaginary)
            {
                Re = real;
                Im = imaginary;
            }

            public double RePart() { return Re; }
            public double ImPart() { return Im; }

            public Complex Add(Complex digit)
            {
                Complex res = new Complex();
                res.Re = Re + digit.Re;
                res.Im = Im + digit.Im;
                return res;
            }

            public Complex Mul(Complex digit)
            {
                Complex res = new Complex();
                res.Re = Re * digit.Re - Im * digit.Im;
                res.Im = Re * digit.Im + Im * digit.Re;
                return res;
            }

            public Complex Dif(Complex digit)
            {
                Complex res = new Complex();
                res.Re = Re - digit.Re;
                res.Im = Im - digit.Im;
                return res;
            }

            public Complex Div(Complex digit)
            {
                Complex res = new Complex();
                res.Re = (Re * digit.Re + Im * digit.Im) / (digit.Re * digit.Re + digit.Im * digit.Im);
                res.Im = (Im * digit.Re - Re * digit.Im) / (digit.Re * digit.Re + digit.Im * digit.Im);
                return res;
            }

            public double Abs()
            {
                return Math.Sqrt(Re * Re + Im * Im);
            }

            public double Arg()
            {
                if (Re > 0)
                {
                    return Math.Tan(Im / Re);
                }
                else if (Re < 0 && Im >= 0)
                {
                    return Math.Tan(Im / Re) + Math.PI;
                }
                else if (Re < 0 && Im < 0)
                {
                    return Math.Tan(Im / Re) - Math.PI;
                }
                else if (Re == 0 && Im > 0)
                {
                    return Math.PI / 2;
                }
                else if (Re == 0 && Im < 0)
                {
                    return Math.PI / (-2);
                }
                else
                {
                    return 0;
                }
            }
        }
        static void Main(string[] args)
        {
            Complex complex1 = new Complex();
            while (true)
            {
                bool f = true;
                Console.WriteLine("Выберите операцию по её номеру:");
                Console.WriteLine("1. Задать комплесное число");
                Console.WriteLine("2. Сложение");
                Console.WriteLine("3. Вычитание");
                Console.WriteLine("4. Умножение");
                Console.WriteLine("5. Деление");
                Console.WriteLine("6. Нахождение модуля");
                Console.WriteLine("7. Нахождение аргумента");
                Console.WriteLine("8. Вывод комплексного числа");
                Console.WriteLine("Для выхода введите Q.");

                string command = Console.ReadLine();

                Complex res = new Complex();
                Complex complex2 = new Complex();

                switch (command)
                {
                    case "1":
                        complex1.CreateComplex();
                        break;
                    case "2":
                        Console.WriteLine("Введите второе комплексное число:");
                        complex2.CreateComplex();
                        res = complex1.Add(complex2);
                        complex1.SetComplex(res.RePart(), res.ImPart());
                        if (complex1.ImPart() < 0)
                            Console.WriteLine("Сумма двух комплексных чисел: " + complex1.RePart() + " - " + Math.Abs(complex1.ImPart()) + 'i');
                        else Console.WriteLine("Сумма двух комплексных чисел: " + complex1.RePart() + " + " + complex1.ImPart() + 'i');
                        break;
                    case "3":
                        Console.WriteLine("Введите второе комплексное число:");
                        complex2.CreateComplex();
                        res = complex1.Dif(complex2);
                        complex1.SetComplex(res.RePart(), res.ImPart());
                        if (complex1.ImPart() < 0)
                            Console.WriteLine("Разность двух комплексных чисел: " + complex1.RePart() + " - " + Math.Abs(complex1.ImPart()) + 'i');
                        else Console.WriteLine("Разность двух комплексных чисел: " + complex1.RePart() + " + " + complex1.ImPart() + 'i');
                        break;
                    case "4":
                        Console.WriteLine("Введите второе комплексное число:");
                        complex2.CreateComplex();
                        res = complex1.Mul(complex2);
                        complex1.SetComplex(res.RePart(), res.ImPart());
                        if (complex1.ImPart() < 0)
                            Console.WriteLine("Произведение двух комплексных чисел: " + complex1.RePart() + " - " + Math.Abs(complex1.ImPart()) + 'i');
                        else Console.WriteLine("Произведение двух комплексных чисел: " + complex1.RePart() + " + " + complex1.ImPart() + 'i');
                        break;
                    case "5":
                        Console.WriteLine("Введите второе комплексное число:");
                        complex2.CreateComplex();
                        res = complex1.Div(complex2);
                        complex1.SetComplex(res.RePart(), res.ImPart());
                        if (complex1.ImPart() < 0)
                            Console.WriteLine("Частное двух комплексных чисел: " + complex1.RePart() + " - " + Math.Abs(complex1.ImPart()) + 'i');
                        else Console.WriteLine("Частное двух комплексных чисел: " + complex1.RePart() + " + " + complex1.ImPart() + 'i');
                        break;
                    case "6":
                        Console.WriteLine("Модуль комплексного числа: " + complex1.Abs());
                        break;
                    case "7":
                        Console.WriteLine("Аргумент комплексного числа: " + complex1.Arg());
                        break;
                    case "8":
                        Console.WriteLine("Вещественная часть комплексного числа: " + complex1.RePart());
                        Console.WriteLine("Мнимая часть комплексного числа: " + complex1.ImPart() + 'i');
                        break;
                    case "q":
                    case "Q":
                        f = false; break;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
                if (!f) break;
                Console.WriteLine("\t");
            }
        }
    }
}