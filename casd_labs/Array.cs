using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casd_labs
{
    internal class ArrayGeneration
    {
        static Random rand = new Random();
        public static int[] ArrayFirst(int size)
        {
            int modulus = 1001;
            int[] array = new int[size];

            for (int i = 0; i < size; i++) array[i] = rand.Next(modulus);
            return array;
        }

        public static int[] ArraySecond(int size)
        {
            int size2 = 0;
            int modulus = 1001;
            while (Math.Pow(10, size2) != size) size2++;
            int[] array = new int[size];
            int[] array2 = new int[size];
            for (int i = 0; i < size; i++) array[i] = rand.Next(modulus);

            int curSize = 0;
            do
            {
                int preSubArrSize = rand.Next(1, size2 + 1);
                while (size - curSize < Math.Pow(10, preSubArrSize)) preSubArrSize = rand.Next(1, size2 + 1);
                int subArrSize = Convert.ToInt32(Math.Pow(10, preSubArrSize));
                int[] subArray = new int[subArrSize];
                for (int i = curSize; i < subArrSize; i++) subArray[i] = array[i];
                curSize += subArrSize;
                Array.Sort(subArray);
                Array.Copy(subArray, 0, array2, 0, subArrSize);
            } while (curSize < size);
            return array2;
        }

        public static int[] ArrayThird(int size)
        {
            int modulus = 1001;
            int firstIndex;
            int secondIndex;
            int[] array = new int[size];
            for (int i = 0; i < size; i++) array[i] = rand.Next(modulus);
            Array.Sort(array);
            int j = rand.Next(0, size);
            for (int i = 0; i < j; i++)
            {
                do
                {
                    firstIndex = rand.Next(0, size);
                    secondIndex = rand.Next(0, size);
                } while (firstIndex == secondIndex);
                int ind = array[firstIndex];
                array[firstIndex] = array[secondIndex];
                array[secondIndex] = ind;
            }
            return array;
        }

        public static int[] ArrayForth1(int size)
        {
            int modulus = 1001;
            int[] array = new int[size];
            for (int i = 0; i < size; i++) array[i] = rand.Next(modulus);
            Array.Sort(array);
            return array;
        }

        public static int[] ArrayForth2(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++) array[i] = rand.Next();
            Array.Sort(array);
            Array.Reverse(array);
            return array;
        }

        public static int[] ArrayForth3(int size)
        {
            int modulus = 1001;
            int[] array = new int[size];
            for (int i = 0; i < size; i++) array[i] = rand.Next(modulus);
            Array.Sort(array);
            int j = rand.Next(0, size);
            for (int i = 0; i < j; i++)
            {
                int ind = rand.Next(0, size);
                array[ind] = rand.Next();
            }
            return array;
        }

        public static int[] ArrayForth4(int size)
        {
            int modulus = 1001;
            int[] array = new int[size];
            Array.Sort(array);
            int elem = rand.Next(modulus);
            int[] arrPercent = { 10, 25, 50, 75, 90 };
            int ind = rand.Next(5);
            int percent = (size / 100) * arrPercent[ind];
            for (int i = 0; i < percent; i++) array[i] = elem;
            for (int i = percent; i < size; i++) array[i] = rand.Next(modulus);
            return array;
        }
    }
}
