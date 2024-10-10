using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace casd_labs
{
    public partial class Form2 : Form
    {
        int n;
        int s;
        List<int[]> listArr;
        List<int[]> listArr2 = new List<int[]>();
        public delegate int[] SortDelegat(int[] a);
        public delegate void SortDelegat2(int[] a);

        private void DisplayMessageBoxText()
        {
            MessageBox.Show("Выберите группу тестовых данных и группу сортировок и повторите генерацию.");
        }

        public Form2()
        {
            InitializeComponent();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();

            pane.XAxis.Title.Text = "Элементы массива";
            pane.YAxis.Title.Text = "Время";
            pane.Title.Text = "Сравнение сортировок";
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                n = 1;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                n = 2;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                n = 3;
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                n = 4;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                s = 1;
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                s = 2;
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                s = 3;
            }
        }
        public int[] GetSize(int s)
        {
            if (s == 1)
            {
                int[] size = { 10, 100, 1000, 10000 };
                return size;
            }
            else if (s == 2)
            {
                int[] size = { 10, 100, 1000, 10000, 100000 };
                return size;
            }
            else if (s == 3)
            {
                int[] size = { 10, 100, 1000, 10000, 100000, 1000000 };
                return size;
            }
            return null;
        }

        public List<int[]> ArrFinalGeneration(int s)
        {
            if (s == 0 || n == 0)
            {
                DisplayMessageBoxText();
                return null;
            }
            int[] size = GetSize(s);
            List<int[]> arrs = new List<int[]>();
            for (int i = 0; i < size.Length; i++)
            {
                if (n == 1) arrs.Add(ArrayGeneration.ArrayFirst(size[i]));
                else if (n == 2) arrs.Add(ArrayGeneration.ArraySecond(size[i]));
                else if (n == 3) arrs.Add(ArrayGeneration.ArrayThird(size[i]));
                else if (n == 4)
                {
                    arrs.Add(ArrayGeneration.ArrayForth1(size[i]));
                    arrs.Add(ArrayGeneration.ArrayForth2(size[i]));
                    arrs.Add(ArrayGeneration.ArrayForth3(size[i]));
                    arrs.Add(ArrayGeneration.ArrayForth4(size[i]));
                }
            }
            return arrs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listArr = ArrFinalGeneration(s);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listArr == null)
            {
                DisplayMessageBoxText();
                return;
            }
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();

            if (s == 1 && n != 4)
            {
                PointPairList list1 = new PointPairList();
                PointPairList list2 = new PointPairList();
                PointPairList list3 = new PointPairList();
                PointPairList list4 = new PointPairList();
                PointPairList list5 = new PointPairList();
                long time1 = 0, time2 = 0, time3 = 0, time4 = 0, time5 = 0;
                for (int i = 0; i < 4; i++)
                {
                    int k = 1;
                    int[] a = listArr[i];
                    for (int j = 0; j < i + 1; j++) k *= 10;
                    for (int l = 0; l < 20; l++)
                    {
                        time1 += GetSortTime(a, new SortDelegat(Program.BubbleSort));
                        time2 += GetSortTime(a, new SortDelegat(Program.InsertionSort));
                        time3 += GetSortTime(a, new SortDelegat(Program.SelectionSort));
                        time4 += GetSortTime(a, new SortDelegat(Program.ShakerSort));
                        time5 += GetSortTime(a, new SortDelegat(Program.GnomeSort));
                    }
                    list1.Add(k, time1/20);
                    list2.Add(k, time2/20);
                    list3.Add(k, time3/20);
                    list4.Add(k, time4/20);
                    list5.Add(k, time5/20);
                }
                pane.AddCurve("Bubble", list1, Color.Red, SymbolType.None);
                pane.AddCurve("Insertion", list2, Color.Blue, SymbolType.None);
                pane.AddCurve("Selection", list3, Color.Green, SymbolType.None);
                pane.AddCurve("Shaker", list4, Color.Purple, SymbolType.None);
                pane.AddCurve("Gnome", list5, Color.Yellow, SymbolType.None);
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
            else if (s == 2 && n != 4)
            {
                PointPairList list1 = new PointPairList();
                PointPairList list2 = new PointPairList();
                PointPairList list3 = new PointPairList();
                long time1 = 0, time2 = 0, time3 = 0;
                for (int i = 0; i < 5; i++)
                {
                    int k = 1;
                    for (int j = 0; j < i + 1; j++) k *= 10;
                    int[] a = listArr[i];
                    for (int l = 0; l < 20; l++)
                    {
                        time1 += GetSortTime(listArr[i], new SortDelegat(Program.ShellSort));
                        time2 += GetSortTime(listArr[i], new SortDelegat(Program.TreeSort));
                        time3 += GetSortTime(a, new SortDelegat2(Program.BiSort));
                    }
                    list1.Add(k, time1 / 20);
                    list2.Add(k, time2 / 20);
                    list3.Add(k, time3 / 20);
                }
                pane.AddCurve("Shell", list1, Color.Red, SymbolType.None);
                pane.AddCurve("Tree", list2, Color.Blue, SymbolType.None);
                pane.AddCurve("Bitonic", list3, Color.Green, SymbolType.None);
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
            else if (s == 3 && n != 4)
            {
                PointPairList list1 = new PointPairList();
                PointPairList list2 = new PointPairList();
                PointPairList list3 = new PointPairList();
                PointPairList list4 = new PointPairList();
                PointPairList list5 = new PointPairList();
                PointPairList list6 = new PointPairList();
                long time1 = 0, time2 = 0, time3 = 0, time4 = 0, time5 = 0, time6 = 0;
                for (int i = 0; i < 6; i++)
                {
                    int k = 1;
                    for (int j = 0; j < i + 1; j++) k *= 10;
                    int[] a = listArr[i];
                    for (int l = 0; l < 20; l++)
                    {
                        time1 += GetSortTime(a, new SortDelegat(Program.CombSort));
                        time2 += GetSortTime(a, new SortDelegat(HeapSort.PerformHeapSort));
                        time3 += GetSortTime(a, new SortDelegat(Program.QuickSort));
                        time4 += GetSortTime(a, new SortDelegat(Program.MergeSort));
                        time5 += GetSortTime(a, new SortDelegat(Program.CountingSort));
                        time6 += GetSortTime(a, new SortDelegat2(Program.RadixSort));
                    }
                    list1.Add(k, time1 / 20);
                    list2.Add(k, time2 / 20);
                    list3.Add(k, time3 / 20);
                    list4.Add(k, time4 / 20);
                    list5.Add(k, time5 / 20);
                    list6.Add(k, time6 / 20);
                }
                pane.AddCurve("Comb", list1, Color.Red, SymbolType.None);
                pane.AddCurve("Heap", list2, Color.Blue, SymbolType.None);
                pane.AddCurve("Quick", list3, Color.Green, SymbolType.None);
                pane.AddCurve("Merge", list4, Color.Purple, SymbolType.None);
                pane.AddCurve("Counting", list5, Color.Yellow, SymbolType.None);
                pane.AddCurve("Radix", list6, Color.Orange, SymbolType.None);
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
            else if (s == 1 && n == 4)
            {
                int c = 0;
                PointPairList list1 = new PointPairList();
                PointPairList list2 = new PointPairList();
                PointPairList list3 = new PointPairList();
                PointPairList list4 = new PointPairList();
                PointPairList list5 = new PointPairList();
                long[] time = new long[5];
                long time1 = 0, time2 = 0, time3 = 0, time4 = 0, time5 = 0;
                for (int i = 0; i < 4; i++)
                {
                    int k = 1;
                    for (int j = 0; j < i + 1; j++) k *= 10;
                    for (int l = 0; l < 20; l++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            int[] a = listArr[c];
                            long y = GetSortTime(a, new SortDelegat(Program.BubbleSort));
                            time[0] += y;
                            y = GetSortTime(a, new SortDelegat(Program.InsertionSort));
                            time[1] += y;
                            y = GetSortTime(a, new SortDelegat(Program.SelectionSort));
                            time[2] += y;
                            y = GetSortTime(a, new SortDelegat(Program.ShakerSort));
                            time[3] += y;
                            y = GetSortTime(a, new SortDelegat(Program.GnomeSort));
                            time[4] += y;
                            c++;
                        }
                        time1 += time[0] / 4;
                        time2 += time[1] / 4;
                        time3 += time[2] / 4;
                        time4 += time[3] / 4;
                        time5 += time[4] / 4;
                        Array.Clear(time, 0, time.Length - 1);
                    }
                    list1.Add(k, time1 / 20);
                    list2.Add(k, time2 / 20);
                    list3.Add(k, time3 / 20);
                    list4.Add(k, time4 / 20);
                    list5.Add(k, time5 / 20);
                }
                pane.AddCurve("Bubble", list1, Color.Red, SymbolType.None);
                pane.AddCurve("Insertion", list2, Color.Blue, SymbolType.None);
                pane.AddCurve("Selection", list3, Color.Green, SymbolType.None);
                pane.AddCurve("Shaker", list4, Color.Purple, SymbolType.None);
                pane.AddCurve("Gnome", list5, Color.Yellow, SymbolType.None);
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
            else if (s == 2 && n == 4)
            {
                int c = 0;
                PointPairList list1 = new PointPairList();
                PointPairList list2 = new PointPairList();
                PointPairList list3 = new PointPairList();
                long[] time = new long[3];
                long time1 = 0, time2 = 0, time3 = 0;
                for (int i = 0; i < 5; i++)
                {
                    int k = 1;
                    for (int j = 0; j < i + 1; j++) k *= 10;
                    for (int l = 0; l < 20; l++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            int[] a = listArr[c];
                            long y = GetSortTime(listArr[i], new SortDelegat(Program.ShellSort));
                            time[0] += y;
                            y = GetSortTime(listArr[i], new SortDelegat(Program.TreeSort));
                            time[1] += y;
                            y = GetSortTime(a, new SortDelegat2(Program.BiSort));
                            time[2] += y;
                            c++;
                        }
                        time1 += time[0] / 4;
                        time2 += time[1] / 4;
                        time3 += time[2] / 4;
                        Array.Clear(time, 0, time.Length - 1);
                    }
                    list1.Add(k, time1 / 20);
                    list2.Add(k, time2 / 20);
                    list3.Add(k, time3 / 20);
                }
                pane.AddCurve("Shell", list1, Color.Red, SymbolType.None);
                pane.AddCurve("Tree", list2, Color.Blue, SymbolType.None);
                pane.AddCurve("Bitonic", list3, Color.Green, SymbolType.None);
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
            else if (s == 3 && n == 4)
            {
                int c = 0;
                PointPairList list1 = new PointPairList();
                PointPairList list2 = new PointPairList();
                PointPairList list3 = new PointPairList();
                PointPairList list4 = new PointPairList();
                PointPairList list5 = new PointPairList();
                PointPairList list6 = new PointPairList();
                long[] time = new long[6];
                long time1 = 0, time2 = 0, time3 = 0, time4 = 0, time5 = 0, time6 = 0;
                for (int i = 0; i < 6; i++)
                {
                    int k = 1;
                    for (int j = 0; j < i + 1; j++) k *= 10;
                    for (int l = 0; l < 20; l++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            int[] a = listArr[c];
                            long y = GetSortTime(a, new SortDelegat(Program.CombSort));
                            time[0] += y;
                            y = GetSortTime(a, new SortDelegat(HeapSort.PerformHeapSort));
                            time[1] += y;
                            y = GetSortTime(a, new SortDelegat(Program.QuickSort));
                            time[2] += y;
                            y = GetSortTime(a, new SortDelegat(Program.MergeSort));
                            time[3] += y;
                            y = GetSortTime(a, new SortDelegat(Program.CountingSort));
                            time[4] += y;
                            y = GetSortTime(a, new SortDelegat2(Program.RadixSort));
                            time[5] += y;
                            c++;
                        }
                        time1 += time[0] / 4;
                        time2 += time[1] / 4;
                        time3 += time[2] / 4;
                        time4 += time[3] / 4;
                        time5 += time[4] / 4;
                        time6 += time[5] / 4;
                        Array.Clear(time, 0, time.Length - 1);
                    }
                    list1.Add(k, time1 / 20);
                    list2.Add(k, time2 / 20);
                    list3.Add(k, time3 / 20);
                    list4.Add(k, time4 / 20);
                    list5.Add(k, time5 / 20);
                    list6.Add(k, time6 / 20);
                }
                pane.AddCurve("Comb", list1, Color.Red, SymbolType.None);
                pane.AddCurve("Heap", list2, Color.Blue, SymbolType.None);
                pane.AddCurve("Quick", list3, Color.Green, SymbolType.None);
                pane.AddCurve("Merge", list4, Color.Purple, SymbolType.None);
                pane.AddCurve("Counting", list5, Color.Yellow, SymbolType.None);
                pane.AddCurve("Radix", list6, Color.Orange, SymbolType.None);
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
            }
        }

        public long GetSortTime(int[] a, SortDelegat sort)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            listArr2.Add(sort(a));
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
        public long GetSortTime(int[] a, SortDelegat2 sort)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            sort(a);
            stopwatch.Stop();
            listArr2.Add(a);
            return stopwatch.ElapsedMilliseconds;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("C:\\Users\\galuz\\Downloads\\TaskSort.txt");
            sw.WriteLine("Сгенерированные массивы:");
            foreach (int[] g in listArr)
            {
                string line = string.Join(" ", g.Select(num => num.ToString()));
                sw.WriteLine(line);
                sw.WriteLine();
            }
            sw.WriteLine("Отсортированные массивы:");
            foreach (int[] g in listArr2)
            {
                string line = string.Join(" ", g.Select(num => num.ToString()));
                sw.WriteLine(line);
                sw.WriteLine();
            }
            sw.Close();
        }
      
    }
}
