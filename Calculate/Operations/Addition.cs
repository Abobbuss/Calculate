using System;

namespace Calculate.Operations
{
    public class Addition : IOperation
    {
        public double Execute(double a, double b) => a + b;
    }
}
