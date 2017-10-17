using System;

namespace Calculator
{
    public class Token
    {
        public readonly String text = "";
        public readonly TokenType type;

        public Token(String text, TokenType type)
        {
            this.text = text;   
            this.type = type;
        }
        public String toString()
        {
            return "Text: " + text + " Type: " + type;
        }

    }
}

    
