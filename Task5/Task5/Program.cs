using MyArrayList;
using System.Security.Cryptography;
using System.Text;
namespace Task5
{
    public class Program
    {
        static string path = "input.txt";
        static StreamReader sr=new StreamReader(path);
        static MyArrayList<string> GetTeg()
        {
            bool isOpen=false;
            bool isClose= false;
            string? line=sr.ReadLine();
            if (line == null) { Console.WriteLine("Строчка пуста"); }
            var arrayTeg = new MyArrayList<string>(10);
            string teg = "";
            while (line != null)
            {
                isOpen = false;
                teg = "";
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '<' && line[i + 1] != null)
                    {
                        if ((line[i + 1] == '/' || char.IsLetter(line[i + 1])) && !isOpen) {isOpen = true; }
                        else
                        {
                            i++;
                            while (i < line.Length)
                            {
                                if (line[i] == '<') break;
                                i++;
                            }
                            teg = "";
                        }
                        if (i == line.Length) break;
                    }
                    if (line[i] == '>' && isOpen) { isOpen = false; teg += line[i]; isClose = true; }
                    if (isOpen && (line[i] == '<' || line[i] == '>' || line[i] == '/'|| line[i] == '/' || char.IsLetter(line[i]))) teg+= line[i];
                    if (isClose) {  arrayTeg.Add(teg);teg = "";isClose = false; }
                }
                line = sr.ReadLine();
            }
            return arrayTeg;
        }
        static MyArrayList<string> DeleteDublicate(MyArrayList<string> arrayTeg) {
            MyArrayList<string> tegDublicate=new MyArrayList<string>(10);
            MyArrayList<string> uniqueTeg = new MyArrayList<string>(10);
            string lowerCaseTeg;
            string dublicate;
            for (int i = 0; i < arrayTeg.Size; i++) {
                lowerCaseTeg = arrayTeg.Element(i);
                dublicate=lowerCaseTeg.ToLower();
                tegDublicate.Add(dublicate);
            }
            for (int i = 0; i < arrayTeg.Size; i++) {
                for (int j = i + 1; j < arrayTeg.Size; j++)
                {
                    if (tegDublicate.Element(i) == tegDublicate.Element(j))
                        arrayTeg.Set(j, "false");
                }
            }
            for (int i = 0; i < arrayTeg.Size; i++)
            {
                bool hasSlashInFirst = tegDublicate.Element(i).Contains("/");
                for (int j = i + 1; j < arrayTeg.Size; j++)
                {
                    bool hasSlashInSecond = tegDublicate.Element(j).Contains("/");
                    if (hasSlashInFirst && !hasSlashInSecond)
                     arrayTeg.Set(j, "false");
                }
            }
            for (int i = 0; i < arrayTeg.Size; i++)
            {
                if (arrayTeg.Element(i) == "false") continue;
                uniqueTeg.Add(arrayTeg.Element(i));
            }
            return uniqueTeg;
        }
        static void Main(string[] args)
        {
            var array = new MyArrayList<string>(10);
            var teg = new MyArrayList<string>(10);
            array =GetTeg();
            teg = DeleteDublicate(array);
            for (int i = 0; i < teg.Size; i++)
            {   
                Console.WriteLine(teg.Element(i));
            }
        }
    }
}
