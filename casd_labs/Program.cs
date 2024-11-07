using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static casd_labs.Class1;
using static System.Net.WebRequestMethods;

namespace casd_labs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyArrayList<string> tags = new MyArrayList<string>();
            try
            {
                using (StreamReader reader = new StreamReader("C:\\Users\\galuz\\Desktop\\file1.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        int startInd = line.IndexOf('<');
                        while (startInd != -1)
                        {
                            int endInd = line.IndexOf('>', startInd);

                            string tag = line.Substring(startInd + 1, endInd - startInd - 1);

                            if (tag.Length >= 2 && (char.IsLetter(tag[1]) || (tag[1] == '/' && char.IsLetter(tag[2]))))
                            {
                                bool f = true;
                                for (int i = 2; i < tag.Length; i++)
                                {
                                    if (!char.IsLetterOrDigit(tag[i]))
                                    {
                                        f = false;
                                        break;
                                    }
                                }
                                string tag2;
                                if (tag[0] == '/') tag2 = tag.Substring(1, tag.Length - 1);
                                else tag2 = tag;

                                if (!tags.Contains(tag2.ToUpper()) && f == true)
                                {
                                    tags.Add(tag2.ToUpper());
                                }

                            }

                            startInd = line.IndexOf('<', endInd + 1);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e.Message);
            }
            finally
            {
                for (int i = 0; i < tags.Size(); i++) Console.WriteLine(tags.Get(i).ToString());
            }
            Console.ReadLine();
        }
    }
}
