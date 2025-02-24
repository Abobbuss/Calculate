using System.Collections.Generic;

namespace Calculate.Expression;

public class Tokenizer
{
    public static List<string> Tokenize(string expression)
    {
        List<string> tokens = new List<string>();
        string numberBuffer = "";

        foreach (char c in expression)
        {
            if (char.IsDigit(c) || c == '.')
            {
                numberBuffer += c;
            }
            else
            {
                if (numberBuffer.Length > 0)
                {
                    tokens.Add(numberBuffer);
                    numberBuffer = "";
                }
                tokens.Add(c.ToString());
            }
        }
        if (numberBuffer.Length > 0)
        {
            tokens.Add(numberBuffer);
        }
        return tokens;
    }
}