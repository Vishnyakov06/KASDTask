bool CheckSymmetry(int[][] matrix, int n)
{
   
    for (int i = 0; i < n; i++)
    {
        for (int j = i+1; j < n; j++)
        {
            if (matrix[i][j] != matrix[j][i])
            {
                return false;
            }
        }
    }
    return true;
}
double VectorLength(int[][]matrix, int[]vector, int n)
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
    return Math.Sqrt(sum);
}
string path = "TextFile1.txt";
string? line;
int n = 0;
int[][] matrix = new int[0][];
int[] vector = new int[0];
try
{
    StreamReader sr = new StreamReader(path);
    n=Convert.ToInt32(sr.ReadLine());
    matrix = new int[n][];
    vector = new int[n];
    for (int i = 0; i < n; ++i)
    {
        line = sr.ReadLine();
        matrix[i] = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
        
    }
    line = sr.ReadLine();
    
    vector = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
    for (int i = 0; i < n; i++)
    {
        Console.WriteLine(vector[i]);
    }
    sr.Close();
}
catch(Exception e)
{
    Console.WriteLine("exception" + e.Message);
}

if (CheckSymmetry(matrix, n))
    {
        Console.WriteLine("Matrix is symmetry");
    }
   Console.WriteLine(VectorLength(matrix, vector, n));
