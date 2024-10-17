using T13;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Runtime;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.Generic;
using System.Drawing;
using ZedGraph;
namespace Task13Graphs
{

    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

        }
        public void InsertPath()
        {
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            pathToArray = appDir + @"\arrayy.txt";
            pathToTime = appDir + @"\timee.txt";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.Items.Add("Первая группа");
            this.comboBox1.Items.Add("Вторая группа");
            this.comboBox1.Items.Add("Третья группа");
            this.comboBox1.SelectedText = "Выберите группу";
            this.comboBox2.SelectedText = "Выберете тип данных";
            this.comboBox2.Items.Add("Int");
            this.comboBox2.Items.Add("Double");
            this.comboBox2.Items.Add("Char");

        }
        int flag = 1;

        private void button1_Click_1(object sender, EventArgs e)
        {
            Sorting sort = new Sorting();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    flag = 1;
                    switch (comboBox2.SelectedIndex)
                    {
                        case 0:
                            for (int length = 10; length <= 10000; length *= 10)
                                sort.SpeedTime(sort.GenerateIntArray, length, sort.BubbleSort, sort.InsertionSort, sort.SelectionSort, sort.ShakerSort, sort.GnomeSort);
                            break;
                        case 1:
                            for (int length = 10; length <= 10000; length *= 10)
                                sort.SpeedTime(sort.GenerateDoubleArray, length, sort.BubbleSort, sort.InsertionSort, sort.SelectionSort, sort.ShakerSort, sort.GnomeSort);
                            break;
                        case 2:
                            for (int length = 10; length <= 10000; length *= 10)
                                sort.SpeedTime(sort.GenerateCharArray, length, sort.BubbleSort, sort.InsertionSort, sort.SelectionSort, sort.ShakerSort, sort.GnomeSort);
                            break;
                        
                    }
                    break;
                case 1:
                    flag = 2;
                    switch (comboBox2.SelectedIndex)
                    {
                        case 0:
                            for (int length = 10; length <= 10000; length *= 10)
                                sort.SpeedTime(sort.GenerateIntArray, length, sort.ShellSort, sort.BitonicSort, sort.TreeSort);

                            break;
                        case 1:
                            for (int length = 10; length <= 10000; length *= 10)
                                sort.SpeedTime(sort.GenerateDoubleArray, length, sort.ShellSort, sort.BitonicSort, sort.TreeSort);
                            break;
                        case 2:
                            for (int length = 10; length <= 10000; length *= 10)
                                sort.SpeedTime(sort.GenerateCharArray, length, sort.ShellSort, sort.BitonicSort, sort.TreeSort);
                            break;
                        
                    }
                    break;
                case 2:
                    flag = 3;
                    switch (comboBox2.SelectedIndex)
                    {
                        case 0:
                            for (int length = 10; length <= 10000; length *= 10)
                                sort.SpeedTime(sort.GenerateIntArray, length, sort.CombSort,sort.QuickSort, sort.MergeSort);
                            break;
                        case 1:
                            for (int length = 10; length <= 10000; length *= 10)
                                sort.SpeedTime(sort.GenerateDoubleArray, length, sort.CombSort, sort.QuickSort, sort.MergeSort);
                            break;
                        case 2:
                            for (int length = 10; length <= 10000; length *= 10)
                                sort.SpeedTime(sort.GenerateCharArray, length, sort.CombSort, sort.QuickSort, sort.MergeSort);
                            break;
                       
                    }
                    break;
            }
        }
        string pathToArray;
        string pathToTime;
        private void button2_Click(object sender, EventArgs e)
        {
            Sorting sort = new Sorting();
            InsertPath();
            List<List<double>> time = new List<List<double>>();
            try
            {
                StreamReader sr = new StreamReader(pathToTime);
                string line = sr.ReadLine();

                while (line != null)
                {
                    List<double> speed = new List<double>();
                    string[] lineArray = line.Split(' ');
                    foreach (string str in lineArray)
                    {
                        speed.Add(Convert.ToDouble(str));
                    }
                    time.Add(speed);
                    line = sr.ReadLine();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Scale.Max = 10000;
            pane.XAxis.Title.Text = "Размер массива";
            pane.YAxis.Title.Text = "Время выполнения";
            pane.Title.Text = "Исследование скорости работы сортировок";
            for (int i = 0; i < time[0].Count; i++)
            {
                PointPairList pointList = new PointPairList();
                int x = 10;

                for (int j = 0; j < time.Count; j++)
                {

                    pointList.Add(x, time[j][i]);
                    x *= 10;
                }
                switch (i)
                {
                    case 0:
                        if (flag == 1)
                        {
                            pane.AddCurve("BubbleSort: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.AddCurve("ShellSort: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("CombSort: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 1:
                        if (flag == 1)
                        {
                            pane.AddCurve("InsertSort: " + i, pointList, Color.Red, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.AddCurve("TreeSort: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("HeapSort: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 2:
                        if (flag == 1)
                        {
                            pane.AddCurve("SelectionSort: " + i, pointList, Color.Pink, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.AddCurve("BitonicSort: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("QuickSort: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 3:
                        if (flag == 1)
                        {
                            pane.AddCurve("ShakerSort: " + i, pointList, Color.Yellow, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("MergeSort: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 4:
                        if (flag == 1)
                        {
                            pane.AddCurve("GnomeSort: " + i, pointList, Color.Green, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("CountingSort: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 5:
                        pane.AddCurve("RadixSort: " + i, pointList, Color.Aquamarine, SymbolType.Default);
                        break;
                }
            }

            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
