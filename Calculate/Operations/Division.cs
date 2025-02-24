using System;

namespace Calculate.Operations
{
    public class Division : IOperation
    {
        public double Execute(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Деление на ноль недопустимо");

            return a / b;
        }
    }
}
