using System.ComponentModel;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyCalculator mycalculator = new MyCalculator();

            Console.WriteLine("For Continueing To Work In Program Please Enter  y  And For Ending  x ");
            string contin = Console.ReadLine();
            while (contin == "y" || contin == "Y")
            {
                try
                {
                    Console.WriteLine("Enter First Number: ");
                    mycalculator.a = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Second Number: ");
                    mycalculator.b = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter One Of Them Operator + * / -  ");
                    string oper = Console.ReadLine();

                    if (oper == "+") Console.WriteLine($"{mycalculator.a} {oper} {mycalculator.b} = {mycalculator.Sum()}");
                    else if (oper == "*") Console.WriteLine($"{mycalculator.a} {oper} {mycalculator.b} = {mycalculator.Multiplication()}");
                    else if (oper == "/") Console.WriteLine($"{mycalculator.a} {oper} {mycalculator.b} = {mycalculator.Divide()}");
                    else if (oper == "-") Console.WriteLine($"{mycalculator.a} {oper} {mycalculator.b} = {mycalculator.Subtract()}");
                    else Console.WriteLine("Enter Correct Operator + * / - ");
                }
                catch
                {
                    Console.WriteLine("Incorrect Number - Data Not Received");
                }
                Console.WriteLine("For Continueing To Work In Program Please Enter  y  And For Ending  x ");
                contin = (Console.ReadLine());
            }
            if (contin == "x" || contin == "X") Console.WriteLine("The End");
            else Console.WriteLine("Enter y or x");
        }
    }

    class MyCalculator
    {
        public double a { get; set; }
        public double b { get; set; }

        public double Sum() { return a + b; }
        public double Divide() { return a / b; }
        public double Subtract() { return a - b; }
        public double Multiplication() { return a * b; }
    }
}

