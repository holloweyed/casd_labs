using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using casd_labs;

public class Calculator
{

    private static string[] operators = { "+", "-", "*", "/", "^", "√", "sin", "cos", "tan", "ln", "log", "exp", "min", "max", "%", "//", "trunc", "(", ")" };
    private static string numberPattern = @"^-?\d+(\.\d+)?$";

    public static void Parse(string expression, out MyStack<double> numbers, out MyStack<string> signs)
    {
        numbers = new MyStack<double>();
        signs = new MyStack<string>();

        string[] parts = expression.Split(' ');
        foreach (var part in parts)
        {
            try
            {
                if (Array.Find(operators, op => op.Equals(part)) != null)
                {
                    signs.Push(part);
                }
                else if (part == "exp")
                {
                    numbers.Push(Math.Exp(1));
                }
                else if (Regex.Matches(part, numberPattern).Count > 0)
                {
                    double a = 0;
                    double.TryParse(part, NumberStyles.Any, CultureInfo.InvariantCulture, out a);
                    numbers.Push(a);
                }
                else
                {
                    bool flag = true;
                    foreach (char c in part)
                    {
                        if (!char.IsLetter(c))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        Console.WriteLine("Введите " + part + ": ");
                        try
                        {
                            string n = Console.ReadLine();
                            numbers.Push(Convert.ToDouble(n));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(part + " не число.");
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Ошибка, повторите ввод. Разделите все операции, числа и скобки пробелами. В дробных числах используйте точку.");
            }

        }
    }

    public static void Main(string[] args)
    {
        while (true)
        {
            {
                Console.WriteLine("Введите математическое выражение, разделяя все отдельные части выражения пробелом:");
                string expression = Console.ReadLine();
                try
                {
                    Parse(expression, out MyStack<double> numbers, out MyStack<string> signs);
                    double result = Calculate(numbers, signs);
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
    private static double Calculate(MyStack<double> numbers, MyStack<string> signs)
    {
        while (!signs.Empty() && !numbers.Empty())
        {
            try
            {
                var sign = signs.Pop();
                if (sign == ")")
                {
                    numbers.Push(Calculate(numbers, signs));
                }
                else if (sign == "(")
                    break;
                else
                    numbers.Push(Switch(sign, numbers));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        return numbers.Pop();
    }

    public static double Switch(string sign, MyStack<double> numbers)
    {
        if (sign == "+")
            return numbers.Pop() + numbers.Pop();
        else if (sign == "-")
            return -1 * (numbers.Pop() - numbers.Pop());
        else if (sign == "*")
            return numbers.Pop() * numbers.Pop();
        else if (sign == "/")
        {
            var a = numbers.Pop();
            var b = numbers.Pop();
            return b / a;
        }
        else if (sign == "^")
        {
            var a = numbers.Pop();
            var b = numbers.Pop();
            return Math.Pow(b, a);
        }
        else if (sign == "√")
            return Math.Sqrt(numbers.Pop());
        else if (sign == "sin")
            return Math.Sin(numbers.Pop());
        else if (sign == "cos")
            return Math.Cos(numbers.Pop());
        else if (sign == "tan")
            return Math.Tan(numbers.Pop());
        else if (sign == "ln")
        {
            var a = numbers.Pop();
            if (a <= 0) throw new ArgumentException();
            return Math.Log(a);
        }
        else if (sign == "log")
        {
            var a = numbers.Pop();
            if (a <= 0) throw new ArgumentException();
            return Math.Log10(a);
        }
        else if (sign == "exp")
        {
            return Math.Exp(numbers.Pop());
        }
        else if (sign == "min")
            return Math.Min(numbers.Pop(), numbers.Pop());
        else if (sign == "max")
            return Math.Max(numbers.Pop(), numbers.Pop());
        else if (sign == "%")
        {
            var a = numbers.Pop();
            var b = numbers.Pop();
            return b % a;
        }

        else if (sign == "//")
        {
            var a = numbers.Pop();
            var b = numbers.Pop();
            return (int)b / a;
        }
        else if (sign == "trunc")
            return Math.Truncate(numbers.Pop());
        return 0;
    }
}

