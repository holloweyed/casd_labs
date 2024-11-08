using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casd_labs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "Z:\\input.txt";
            string outputFilePath = "Z:\\output.txt";

            string[] lines = File.ReadAllLines(inputFilePath);
            MyVector<string> inputLines = new MyVector<string>();
            MyVector<string> validIps = new MyVector<string>();

            foreach (string line in lines) inputLines.Add(line);

            for (int i = 0; i < inputLines.Size(); i++)
            {
                string[] parts = inputLines.Get(i).Split(' ');
                foreach (string part in parts)
                {
                    if (IsValidIp(part))
                    {
                        if (!validIps.Contains(part))
                        {
                            validIps.Add(part);
                        }
                    }
                }
            }
            StreamWriter sw = new StreamWriter(outputFilePath);
            for (int i = 0; i < inputLines.Size(); i++)
                sw.WriteLine(validIps.Get(i));
            sw.Close();
            Console.WriteLine($"IP адреса записаны в {outputFilePath}");
            Console.ReadLine();
        }
        public static bool IsValidIp(string ip)
        {
            string[] parts = ip.Split('.');
            if (parts.Length != 4) return false;

            foreach (string part in parts)
            {
                if (!int.TryParse(part, out int num) || num < 0 || num > 255 || part[0] == '0')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
