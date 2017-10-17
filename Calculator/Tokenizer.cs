using System;
using System.Collections;
using System.Collections.Generic;
using Calculator;

namespace Calculator
{
    public class Tokenizer
    {
        public String expression = "";
        public int currentCharPosition = 0;
        public char Look;

        public void Init()
        {
            GetChar();
        }

        public void GetChar()
        {
            if (currentCharPosition < expression.Length)
                Look = expression[currentCharPosition];
            currentCharPosition++;
        }

        public bool IsOp(char chr)
        {
            bool aritOp = chr == '+' || chr == '-' ||
            chr == '*' || chr == '/';
            return aritOp;
        }

        public TokenType FindOpType(char firstOperator)
        {
            TokenType type = TokenType.UNKNOWN;
            switch (firstOperator)
            {
                case '+':
                    type = TokenType.ADD;
                    break;
                case '-':
                    type = TokenType.SUBTRACT;
                    break;
                case '*':
                    type = TokenType.MULTIPLY;
                    break;
                case '/':
                    type = TokenType. DIVIDE;
                    break;
                case '%':
                    type = TokenType.MOD;
                    break;
                case '^':
                    type = TokenType.POWER;
                    break;
            }
            return type;
        }

        public bool IsParen(char chr)
        {
            bool prntOp = chr == '(' || chr == ')';
            return prntOp;
        }

        public TokenType FindParenType(char chr)
        {
            TokenType type = TokenType.UNKNOWN;
            switch (chr)
            {
                case '(':
                    type = TokenType.LEFT_PAREN;
                    break;
                case ')':
                    type = TokenType.RIGHT_PAREN;
                    break;
            }
            return type;
        }

        public List<Token> Tokenize(String source)
        {
            List<Token> tokens = new List<Token>();
            String token = "";
            TokenizeState state = TokenizeState.DEFAULT;
            for (int i = 0; i < source.Length; i++)
            {
                char chr = source[i];
                switch (state)
                {
                    case TokenizeState.DEFAULT:
                        TokenType opType = FindOpType(chr);
                        if (IsOp(chr))
                        {
                            tokens.Add(new Token(Char.ToString(chr), opType));
                        }
                        else if (IsParen(chr))
                        {
                            TokenType parenType = FindParenType(chr);
                            tokens.Add(new Token(Char.ToString(chr), parenType));
                        }
                        else if (Char.IsDigit(chr))
                        {
                            token += chr;
                            state = TokenizeState.NUMBER;
                        }
                        break;
                    case TokenizeState.NUMBER:
                        if (Char.IsDigit(chr))
                        {
                            token += chr;
                        }
                        else
                        {
                            tokens.Add(new Token(token, TokenType.NUMBER));
                            token = "";
                            state = TokenizeState.DEFAULT;
                            i--;
                        }
                        break;
                }
            }
            return tokens;
        }

        public void PrettyPrint(List<Token> tokens)
        {
            int numberCount = 0;
            int opCount = 0;
            foreach(Token token in tokens)
            {
                if (token.type.Equals("NUMBER"))
                {
                    Console.WriteLine("Number....: " + token.text);
                    numberCount++;
                }
                else
                {
                    Console.WriteLine("Operator..: " + token.type);
                    opCount++;
                }
            }
            Console.WriteLine("You have got " + numberCount +
            " different number and " + opCount
            + " operators.");
        }

        public static void main(string[] args)
        {
            String expression = "219%341^19";
            expression += " ";
            Tokenizer tokenizer = new Tokenizer();
            List<Token> tokens = tokenizer.Tokenize(expression);
            Console.WriteLine("--------------");
            tokenizer.PrettyPrint(tokens);
        }
    }
}



