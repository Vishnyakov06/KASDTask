bool CheckingSymmetry(int[][] matrix, int n)
{
    int flag = 1;
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (i == j) continue;
            if (matrix[i][j] != matrix[j][i])
            {
                flag = 0;
            }
        }
    }
    if (flag == 1)
    {
        return true;
    }
    else return false;
}
void rez(int[][]matr, int[]vect, int n)
{
    int[] newm;
    int sum = 0;
    newm = new int[n];
    for (int i = 0; i < n; i++)
    {
        sum = 0;
        for (int j = 0; j < n; j++)
        {
            sum += vect[j] * matr[j][i];
        }
        newm[i] = sum;
    }
    sum = 0;
    for (int i = 0; i < n; i++)
    {
        sum += newm[i] * vect[i];
    }
    Console.WriteLine(Math.Sqrt(sum));
}
string path = "TextFile1.txt";
string? line;
try
{
    StreamReader sr = new StreamReader(path);
    int n=Convert.ToInt32(sr.ReadLine());
    int[][] matr=new int[n][];
    
    for (int i = 0; i < n; ++i)
    {
        line = sr.ReadLine();
        matr[i] = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
        
    }
    line = sr.ReadLine();
    int[] vect = new int[n];
    vect = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
    for (int i = 0; i < n; i++)
    {
        Console.WriteLine(vect[i]);
    }
    sr.Close();
    if (CheckingSymmetry(matr, n))
    {
        Console.WriteLine("jd");
    }
    rez(matr, vect, n);
        
}
catch(Exception e)
{
    Console.WriteLine("ex" + e.Message);
}