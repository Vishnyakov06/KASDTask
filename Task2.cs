using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


ComplexNumbers numbers=new ComplexNumbers();
ComplexNumbers numbers1 = new ComplexNumbers();
ComplexNumbers result=new ComplexNumbers();
numbers1.imaginary = 0;
numbers1.real = 0;
numbers.real = 0;
numbers.imaginary = 0;
while (true)
{
    
    Console.WriteLine("1. Create a complex number\n" +
        "2. Addition of complex numbers\n" +
        "3. Subtraction of complex numbers\n" +
        "4. Multiplication of complex numbers\n" +
        "5. Division of complex numbers\n" +
        "6. Finding the module of a complex number\n" +
        "\ta First number\n" +
        "\tb Second number\n"+
        "7. Finding the argument of a complex number\n" +
        "\ta First number\n" +
        "\tb Second number\n"+
        "8. Print of complex number\n" +
        "\ta First number\n" +
        "\tb Second number\n"
        ); char option;
    
         option=Convert.ToChar(Console.ReadLine());
    if ((option == 'Q' || option == 'q')) break;
    if (option > '8') { Console.WriteLine("Unknown character"); }
    
    switch (option)
    {
        case '1':
            Console.WriteLine("Enter real number:");
            double real1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter imaginary number:");
            double imaginary1 = Convert.ToDouble(Console.ReadLine());
            numbers.CreatureComplexNumbers(ref real1, ref imaginary1,ref numbers);
            Console.WriteLine("Enter real number:");
            double real2 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter imaginary number:");
            double imaginary2 = Convert.ToDouble(Console.ReadLine());
            numbers1.CreatureComplexNumbers(ref real2, ref imaginary2,ref numbers1);
            break;
        case '2':
            result.AdditionComplexNumbers(numbers, numbers1,ref result);
            break;
        case '3':
            result.SubtractionComlexNumbers(numbers, numbers1,ref result);
            break;
        case '4':
            result.MultiplicationComplexNumbers(numbers, numbers1,ref result);
            break;
        case '5':
            result.DivisionComplexNumbers(numbers, numbers1, ref result);
            break;
        case '6':
            char option2;
            option2 = Convert.ToChar(Console.ReadLine());
            if ((option2 == 'Q' || option2 == 'q')) break;
            if (option2 != 'a' && option2 != 'b') { Console.WriteLine("unknown character"); break; }
            switch (option2)
            {
                case 'a':
                    Console.WriteLine(numbers.ModuleComplexNumbers(numbers));
                    break;
                case 'b':
                    Console.WriteLine(numbers1.ModuleComplexNumbers(numbers1));
                    break;
            }
            break;
        case '7':
            
            char option3;
            option3 = Convert.ToChar(Console.ReadLine());
            if ((option3 == 'Q' || option3 == 'q')) break;
            if (option3 != 'a' && option3 != 'b') { Console.WriteLine("unknown character"); break; }
            switch (option3)
            {
                case 'a':
                    Console.WriteLine(numbers.ArgumentComplexNumbers(numbers));
                    break;
                case 'b':
                    Console.WriteLine(numbers1.ArgumentComplexNumbers(numbers1));
                    break;
            }
            break;
        case '8':
            char option1;
            option1 = Convert.ToChar(Console.ReadLine());
            if ((option1 == 'Q' || option1 == 'q')) break;
            if (option1 != 'a' && option1 != 'b') { Console.WriteLine("unknown character"); break; }
            switch (option1)
            {
                case 'a':
                    numbers.PrintComplexNumbers(numbers);
                    break;
                case 'b':
                    numbers1.PrintComplexNumbers(numbers1);
                    break;
            }
            break;
    }
}
struct ComplexNumbers
{
    public double imaginary;
    public double real;
    public ComplexNumbers CreatureComplexNumbers(ref double re,ref double im,ref ComplexNumbers numbers1)
{
    numbers1.imaginary = im;
    numbers1.real = re;
    return numbers1;
}
public void AdditionComplexNumbers(ComplexNumbers numbers,ComplexNumbers numbers1,ref ComplexNumbers result)
    { 
    result.real= numbers.real+numbers1.real;
    result.imaginary = numbers.imaginary + numbers1.imaginary;
    if (result.imaginary >= 0)
    {
        Console.WriteLine($"{result.real} + {result.imaginary}*i");
    }
    else Console.WriteLine($"{result.real} - {result.imaginary * -1}*i");
}
public void SubtractionComlexNumbers(ComplexNumbers numbers,ComplexNumbers numbers1,ref ComplexNumbers result)
{
    result.real = numbers.real - numbers1.real;
    result.imaginary = numbers.imaginary - numbers1.imaginary;
    if (result.imaginary >= 0)
    {
        Console.WriteLine($"{result.real} + {result.imaginary}*i");
    }
    else Console.WriteLine($"{result.real} - {result.imaginary * -1}*i");
}
public void MultiplicationComplexNumbers(ComplexNumbers numbers,ComplexNumbers numbers1, ref ComplexNumbers result)
{
    result.real=(numbers.real*numbers1.real - numbers.imaginary*numbers1.imaginary);
    result.imaginary=numbers.real*numbers1.imaginary+numbers1.real*numbers.imaginary;
    if (result.imaginary >= 0)
    {
        Console.WriteLine($"{result.real} + {result.imaginary}*i");
    }
    else Console.WriteLine($"{result.real} - {result.imaginary * -1}*i");
}
public void DivisionComplexNumbers(ComplexNumbers numbers,ComplexNumbers numbers1, ref ComplexNumbers result)
{
    result.real = (numbers.real * numbers1.real + numbers.imaginary * numbers1.imaginary)/(Math.Pow(numbers1.real,2)+ Math.Pow(numbers1.imaginary, 2));
    result.imaginary = (numbers1.real * numbers.imaginary - numbers.real * numbers1.imaginary) / (Math.Pow(numbers1.real, 2) + Math.Pow(numbers1.imaginary, 2));
    if (result.imaginary >= 0)
    {
        Console.WriteLine($"{result.real} + {result.imaginary}*i");
    }
    else Console.WriteLine($"{result.real} - {result.imaginary * -1}*i");
}
public double ArgumentComplexNumbers(ComplexNumbers numbers)
{
    if ((numbers.real > 0 && numbers.imaginary>=0)||(numbers.real > 0 && numbers.imaginary < 0))
    {
        return Math.Atan(numbers.imaginary / numbers.real);
    }
    else if(numbers.imaginary >= 0 && numbers.real < 0)
    {
        return Math.PI+ Math.Atan(numbers.imaginary / numbers.real);
    }
    else if(numbers.real < 0 && numbers.imaginary < 0)
    {
        return -Math.PI + Math.Atan(numbers.imaginary / numbers.real);
    }
    else if(numbers.real==0 && numbers.imaginary > 0)
    {
        return Math.PI / 2;
    }
    else
    {
        return -Math.PI / 2;
    }
}
public void PrintComplexNumbers(ComplexNumbers numbers)
{
    if (numbers.imaginary >= 0)
    {
        Console.WriteLine($"{numbers.real} + {numbers.imaginary}*i");
    }
    else Console.WriteLine($"{numbers.real} - {numbers.imaginary * -1}*i");
}
public double ModuleComplexNumbers(ComplexNumbers numbers)
{
    return Math.Sqrt(Math.Pow(numbers.real, 2) + Math.Pow(numbers.imaginary, 2));

}
}

