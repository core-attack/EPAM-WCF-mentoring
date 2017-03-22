namespace CalculatorService
{
    using System;

    public class CalculatorService : ICalculatorService
    {
        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Diff(double a, double b)
        {
            return a - b;
        }

        public double Mult(double a, double b)
        {
            return a * b;
        }

        public double Div(double a, double b)
        {
            throw new Exception("some exception");
            return a / b;
        }

        public double PI()
        {
            return Math.PI;
        }
    }
}
