using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        private static int numberRequest = 1;
        private static string filePath = "log.txt";
        private static StreamWriter writer = new StreamWriter(filePath);

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите количество шагов:");
                int n = Convert.ToInt32(Console.ReadLine());
                MyPriorityQueue<Request> queue = new MyPriorityQueue<Request>();
                for (int i = 1; i < n + 1; i++)
                {
                    Random rnd = new Random();
                    int countRequests = rnd.Next(1, 11);
                    for (int j = 0; j < countRequests; j++)
                    {
                        GenerateRequest(queue, i, countRequests);
                    }
                    Request maxRequest = queue.Element();
                    queue.Remove(maxRequest);
                    writer.WriteLine("REMOVE " + maxRequest.Number + " " + maxRequest.Priority + " " + maxRequest.Step);
                }
                string s = "";
                System.TimeSpan time = new TimeSpan();
                while (!queue.IsEmpty())
                {
                    Request req = queue.Element();
                    time = req.Stopwatch.Elapsed;
                    queue.Remove(req);
                    writer.WriteLine("REMOVE " + req.Number + " " + req.Priority + " " + req.Step);
                    s = "Приоритет заявки: " + req.Priority + ", номер заявки: " + req.Number + ", номер шага: " + req.Step;
                }
                queue = null;
                Console.WriteLine("Максимальное время ожидания: " + time);
                Console.WriteLine(s);
                writer.Close();
                Console.WriteLine("Данные записаны в файл: " + Directory.GetCurrentDirectory() + "\\log.txt");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadLine();
            }
        }
        private static void GenerateRequest(MyPriorityQueue<Request> queue, int step, int countRequests)
        {

            Random rnd = new Random();
            
            for (int i = 0; i <= countRequests; i++)
            {
                int priority = rnd.Next(1, 5);
                Request request = new Request(priority, numberRequest, step);
                numberRequest++;
                queue.Add(request);
                writer.WriteLine("ADD " + request.Number + " " + request.Priority + " " + request.Step);
            }
        }
    }
}