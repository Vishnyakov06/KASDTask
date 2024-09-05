bool CheckingSymmetry(int[][] matrix, int n)
{
    bool flag = 1;
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
void VectorLength(int[][]matrix, int[]vector, int n)
{
    int[] newmatrix;
    int sum = 0;
    newmatrix = new int[n];
    for (int i = 0; i < n; i++)
    {
        sum = 0;
        for (int j = 0; j < n; j++)
        {
            sum += vector[j] * matrix[j][i];
        }
        newmatrix[i] = sum;
    }
    sum = 0;
    for (int i = 0; i < n; i++)
    {
        sum += newmatrix[i] * vector[i];
    }
    Console.WriteLine(Math.Sqrt(sum));
}
string path = "TextFile1.txt";
string? line;
try
{
    StreamReader sr = new StreamReader(path);
    int n=Convert.ToInt32(sr.ReadLine());
    int[][] matrix=new int[n][];
    
    for (int i = 0; i < n; ++i)
    {
        line = sr.ReadLine();
        matrix[i] = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
        
    }
    line = sr.ReadLine();
    int[] vector = new int[n];
    vector = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
    for (int i = 0; i < n; i++)
    {
        Console.WriteLine(vector[i]);
    }
    sr.Close();
    if (CheckingSymmetry(matrix, n))
    {
        Console.WriteLine("Matrix Symmetry");
    }
    VectorLength(matrix, vector, n);
        
}
catch(Exception e)
{
    Console.WriteLine("exception" + e.Message);
}
