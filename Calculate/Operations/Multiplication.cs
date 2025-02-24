using System;

namespace Calculate.Operations
{
    public class Multiplication : IOperation
    {
        public double Execute(double a, double b) => a * b;
    }
}
