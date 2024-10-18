using Task14;
namespace Sort
{
    public class DeqSorting
    {
        public int CountDigit(string line)
        {
            int cnt = 0;
            string[] str = line.Split(' ');
            for (int i = 0; i < str.Length; i++)
            {
                cnt += str[i].Length;
            }
            return cnt;
        }
        public void AddLines(int n)
        {
            MyArrayDeque<string> deque = new MyArrayDeque<string>();
            string path = "input.txt";
            string path1 = "sorted.txt";
            StreamWriter sw=new StreamWriter(path1);
            StreamReader sr = new StreamReader(path);
            string? line = sr.ReadLine();
            deque.Add(line);
            int cnt = 0;
            if (line == null) { Console.WriteLine("Строчка пуста"); }
            while (line != null)
            {
                line = sr.ReadLine();
                if (line != null) { 
                    if (CountDigit(line) > CountDigit(deque.GetFirst()))
                        deque.AddLast(line);
                    else deque.AddFirst(line);
                }
            }
            for (int i = 0; i < deque.Size(); i++)
                sw.WriteLine(deque.GetInd(i));
            sw.Close();
            for (int i = 0; i < deque.Size(); i++)
            {
                string str=deque.GetInd(i);
                string[] it = str.Split(' ');
                if (it.Length-1 > n) 
                    deque.Remove(str);
            }
            deque.Print();
        }
        static void Main(string[] args)
        {
            DeqSorting sorting = new DeqSorting();
            string n=Console.ReadLine();
            int n1=Convert.ToInt32(n);
            sorting.AddLines(n1);
        }

    }
}