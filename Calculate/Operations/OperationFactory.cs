using System;

namespace Calculate.Operations;

public class OperationFactory
{
    public IOperation GetOperation(char op)
    {
        return op switch
        {
            '+' => new Addition(),
            '-' => new Subtraction(),
            '*' => new Multiplication(),
            '/' => new Division(),
            _ => throw new ArgumentException("Неверная операция")
        };
    }
}