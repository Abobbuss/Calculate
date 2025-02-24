using System;
using System.Collections.Generic;
using Calculate.Operations;

namespace Calculate.Expression;

public class ExpressionEvaluator
{
    private readonly OperationFactory _operationFactory;

    public ExpressionEvaluator()
    {
        _operationFactory = new OperationFactory();
    }

    public double Evaluate(string expression)
    {
        if (!IsValidExpression(expression))
            throw new ArgumentException("Неверное выражение!");
        
        var tokens = Tokenizer.Tokenize(expression);
        var rpnQueue = ConvertToRpn(tokens);
        return ComputeRPN(rpnQueue);
    }

    private Queue<string> ConvertToRpn(List<string> tokens)
    {
        Stack<string> operatorStack = new Stack<string>();
        Queue<string> outputQueue = new Queue<string>();

        Dictionary<string, int> precedence = new()
        {
            {"+", 1}, {"-", 1},
            {"*", 2}, {"/", 2}
        };

        foreach (var token in tokens)
        {
            if (double.TryParse(token, out _))
            {
                outputQueue.Enqueue(token);
            }
            else if (token == "(")
            {
                operatorStack.Push(token);
            }
            else if (token == ")")
            {
                while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }
                operatorStack.Pop();
            }
            else if (precedence.ContainsKey(token))
            {
                while (operatorStack.Count > 0 && precedence.ContainsKey(operatorStack.Peek()) &&
                       precedence[operatorStack.Peek()] >= precedence[token])
                {
                    outputQueue.Enqueue(operatorStack.Pop());
                }
                operatorStack.Push(token);
            }
        }

        while (operatorStack.Count > 0)
        {
            outputQueue.Enqueue(operatorStack.Pop());
        }

        return outputQueue;
    }

    private double ComputeRPN(Queue<string> rpnQueue)
    {
        Stack<double> stack = new Stack<double>();
            
        while (rpnQueue.Count > 0)
        {
            var token = rpnQueue.Dequeue();
                
            if (double.TryParse(token, out double number))
            {
                stack.Push(number);
            }
            else
            {
                double b = stack.Pop();
                double a = stack.Pop();
                stack.Push(_operationFactory.GetOperation(token[0]).Execute(a, b));
            }
        }
            
        return stack.Pop();
    }
    
    private bool IsValidExpression(string expression)
    {
        int balance = 0;
        
        foreach (char c in expression)
        {
            if (c == '(') 
                balance++;
            
            if (c == ')') 
                balance--;
            
            if (balance < 0) 
                return false;
        }
        
        return balance == 0;
    }
}