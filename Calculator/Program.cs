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
            String expression = "5+11";
            expression += " ";

            Program calc = new Program();
            Tokenizer tokenizer = new Tokenizer();

            Console.WriteLine("Expression: " + expression);
            Console.WriteLine("--------------------------");
            calc.tokens = tokenizer.Tokenize(expression);
            calc.PrettyPrint(calc.tokens);
            Console.WriteLine("--------------------------");

            //int result = calc.ArithmeticExpression();
            //Console.WriteLine("Expression Result: " + result);
            Console.WriteLine("Expression Result: " + calc.Expression());
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

        //Arithmetic methods

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

        public int Multiply()
        {
            MatchAndEat(TokenType.MULTIPLY);
            return Factor();
        }

        public int Divide()
         {
            MatchAndEat(TokenType.DIVIDE);
            return Factor();
         }

        public int Power()
        {
            MatchAndEat(TokenType.POWER);
            return PreFactor();
        }

        public int Mod()
        {
            MatchAndEat(TokenType.MOD);
            return Factor();
        }

        //Tokenizer methods

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

        //Boolean Methods

        public bool Relation()
        {
            int leftExpressionResult = ArithmeticExpression();
            bool result = false;
            TokenType type = CurrentToken().type;
            if (type == TokenType.EQUAL ||
            type == TokenType.LESS ||
            type == TokenType.GREATER ||
            type == TokenType.LESSEQUAL ||
            type == TokenType.GREATEREQUAL)
            {
                switch (CurrentToken().type)
                {
                    case TokenType.LESS:
                        result = Less(leftExpressionResult);
                        break;
                    case TokenType.LESSEQUAL:
                        result = LessEqual(leftExpressionResult);
                        break;
                    case TokenType.EQUAL:
                        result = Equal(leftExpressionResult);
                        break;
                    case TokenType.GREATER:
                        result = Greater(leftExpressionResult);
                        break;
                    case TokenType.GREATEREQUAL:
                        result = GreaterEqual(leftExpressionResult);
                        break;
                }
            }
            return result;
        }

        public bool Less(int leftExpressionResult)
        {
            MatchAndEat(TokenType.LESS);
            return leftExpressionResult < ArithmeticExpression();
        }

        public bool LessEqual(int leftExpressionResult)
        {
            MatchAndEat(TokenType.LESSEQUAL);
            return leftExpressionResult <= ArithmeticExpression();
        }

        public bool Equal(int leftExpressionResult)
        {
            MatchAndEat(TokenType.EQUAL);
            return leftExpressionResult == ArithmeticExpression();
        }

        public bool Greater(int leftExpressionResult)
        {
            MatchAndEat(TokenType.GREATER);
            return leftExpressionResult > ArithmeticExpression();
        }

        public bool GreaterEqual(int leftExpressionResult)
        {
            MatchAndEat(TokenType.GREATEREQUAL);
            return leftExpressionResult >= ArithmeticExpression();
        }

        public bool BooleanFactor()
        {
            return Relation();
        }

        public bool BooleanTerm()
        {
            bool result = BooleanFactor();
            while (CurrentToken().type == TokenType.AND)
            {
                MatchAndEat(TokenType.AND);
                result = result && BooleanFactor();
            }
            return result;
        }

        public bool BooleanExpression()
        {
            bool result = BooleanTerm();
            while (CurrentToken().type == TokenType.OR)
            {
                switch (CurrentToken().type)
                {
                    case TokenType.OR:
                        MatchAndEat(TokenType.OR);
                        result = result || BooleanTerm();
                        break;
                }
            }
            return result;
        }

        public bool Expression()
        {
            return BooleanExpression();
        }
    }

}

    

