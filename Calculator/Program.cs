using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        public static int currentTokenPosition = 0;
        public List<Token> tokens;

        public static void Main(string[] args)
        {
            String expression = "(600+3+2)*5";
            expression += " ";

            Program calc = new Program();
            Tokenizer tokenizer = new Tokenizer();

            Console.WriteLine("Expression: " + expression);
            Console.WriteLine("--------------------------");
            calc.tokens = tokenizer.Tokenize(expression);
            calc.PrettyPrint(calc.tokens);
            Console.WriteLine("--------------------------");

            int result = calc.ArithmeticExpression();
            Console.WriteLine("Expression Result: " + result);
        }

        public int Term()
        {
            int result = PreFactor();
            while (CurrentToken().type == TokenType.MULTIPLY ||
            CurrentToken().type == TokenType.DIVIDE ||
            CurrentToken().type == TokenType.MOD)
            {
                switch (CurrentToken().type)
                {
                    case TokenType.MULTIPLY:
                        result = result * Multiply();
                        break;
                    case TokenType.DIVIDE:
                        result = result / Divide();
                        break;
                    case TokenType.MOD:
                        result = result % Mod();
                        break;
                }
            }
            return result;
        }   

        public int ArithmeticExpression()
        {
            int result = Term();
            while (CurrentToken().type == TokenType.ADD ||
            CurrentToken().type == TokenType.SUBTRACT)
            {
                switch (CurrentToken().type)
                {
                    case TokenType.ADD:
                        result = result + Add();
                        break;
                    case TokenType.SUBTRACT:
                        result = result - Subtract();
                        break;
                }
            }
            return result;

        }

        public int PreFactor()
        {
            int result = Factor();
            while (CurrentToken().type == TokenType.POWER)
            {
                switch (CurrentToken().type)
                {
                    case TokenType.POWER:
                        result = (int) Math.Pow(result, Power());
                        break;
                }
            }
            return result;
        }

        public int Factor()
        {
            int result = 0;
            if (CurrentToken().type == TokenType.LEFT_PAREN)
            {
                MatchAndEat(TokenType.LEFT_PAREN);
                result = ArithmeticExpression();
                MatchAndEat(TokenType.RIGHT_PAREN);
            }
            else if (CurrentToken().type == TokenType.NUMBER)
            {
                result = Int32.Parse(CurrentToken().text);
                MatchAndEat(TokenType.NUMBER);
            }
            return result;
        }

        public int Add()
        {
            MatchAndEat(TokenType.ADD);
            return Term();
        }

        public int Subtract()
        {
            MatchAndEat(TokenType.SUBTRACT);
            return Term();
        }

        public int Mod()
        {
            MatchAndEat(TokenType.MOD);
            return Term();
        }

        public int Multiply()
        {
            MatchAndEat(TokenType.MULTIPLY);
            return Factor();
        }

        public int Power()
        {
            MatchAndEat(TokenType.POWER);
            return Term();
        }

        public int Divide()
        {
            MatchAndEat(TokenType.DIVIDE);
            return Factor();
        }

        public Token CurrentToken()
        {
            return GetToken(0);
        }

        public Token NextToken()
        {
            return GetToken(1);
        }

        public Token GetToken(int offset)
        {
            if(currentTokenPosition + offset >= tokens.Count())
{
                return new Token("", TokenType.EOF);
            }
            return tokens[currentTokenPosition + offset];
        }

        public void EatToken(int offset)
        {
            currentTokenPosition = currentTokenPosition + offset;
        }

        public Token MatchAndEat(TokenType type)
        {
            Token token = CurrentToken();
            if (CurrentToken().type != type)
            {
                Console.WriteLine("Saw " + token.type +
                " but " + type +
                " expected");
                Environment.Exit(0);
            }
            EatToken(1);
            return token;
        }

        public void PrettyPrint(List<Token> tokens)
        {
            int numberCount = 0;
            int opCount = 0;
            foreach(Token token in tokens)
            {
                if (token.type == TokenType.NUMBER)
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
            " different number and " + opCount + " operators.");
        }
    }

}

    

