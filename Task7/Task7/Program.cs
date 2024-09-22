using MyVector;
using System.Threading.Tasks.Dataflow;
namespace Task7
{
    public class IP
    {
        static string path = "input.txt";
        static string pathOut = "output.txt";
        static StreamReader sr = new StreamReader(path);
        static StreamWriter sw = new StreamWriter(pathOut);
        static MyVector<string> GetIP()
        {
            string line = sr.ReadLine();
            if(line== null) { Console.WriteLine("Строчка пуста"); }
            MyVector<string> ip= new MyVector<string>(10);
            while (line != null) {
                string[]ipArray = line.Split(' ');
                foreach (string adress in ipArray)
                {
                    bool isIp = true;
                    int[]ipGroup=adress.Split(".").Select(x=>Convert.ToInt32(x)).ToArray();
                    foreach(int item in ipGroup)
                    {
                        if (item>255 || item < 0)
                            isIp = false;
                    }
                    if (isIp && ipGroup.Length==4)
                        ip.Add(adress);
                }
                line = sr.ReadLine();
            }
            sr.Close();
            return ip;
        }
        static void WriteIpToFile(MyVector<string> ip)
        {
            for (int i = 0; i < ip.Count; i++) { 
                string address =ip.Get(i);
                sw.WriteLine(address);
            }
            sw.Close();
        }
        static void Main(string[] args)
        {
            MyVector<string> ip = new MyVector<string>(10);
            ip=GetIP();
            WriteIpToFile(ip);
        }
    }
}