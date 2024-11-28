using System.Text.RegularExpressions;
using Task18;
public class Var
{
    enum TypeEnum
    {
        Integer,
        Double,
        Float
    }
    static void Main(string[] args)
    {
        /*Это переменная типа MatchCollection, которая будет содержать все найденные совпадения. 
        MatchCollection — это коллекция объектов Match, каждый из которых представляет одно совпадение с регулярным выражением.
         */
        try
        {
            MyHashMap<string, string> variable = new MyHashMap<string, string>();
            string pattern = @"(double|int|float) \S* ?(?:=) ?(\S)+?(?=;)";
            string path = "input.txt";
            StreamReader sr = new StreamReader(path);
            string? line = sr.ReadLine();
            if (line == null) Console.WriteLine("Строчка пуста");
            while (line != null)
            {
                MatchCollection matches = Regex.Matches(line, pattern);
                foreach (Match match in matches)
                {
                    string[] parts = match.Value.Split(' ');
                    string type = parts[0].Trim();
                    string valuable = parts[3].Trim();
                    string name = parts[1].Trim();
                    string tv = type +" "+ valuable;
                    if (variable.ContainsKey(name)) Console.WriteLine("повтор"+"    "+$"{type} {name}={valuable}");
                    else variable.Put(name,tv);
                }
                line = sr.ReadLine();           
            }
            sr.Close();
            var pair = variable.EntrySet();
            foreach (var pai in pair)
                Console.WriteLine(pai.Value + " " + pai.Key);
        }
        catch (Exception ex) { Console.WriteLine("Exception : " + ex.Message); }

    }
}