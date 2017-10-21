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
            bool addOp = chr == '+' || chr == '-';
            bool mulOp = chr == '*' || chr == '/' || chr == '%' || chr == '^';

            bool compOp = chr == '<' || chr == '>' || chr == '=';
            bool lgicOp = chr == '!' || chr == '|' || chr == '&';

            return addOp || mulOp || compOp || lgicOp;
        }

        public TokenType FindOpType(char firstOperator, char nextChar)
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
                    type = TokenType.DIVIDE;
                    break;
                case '%':
                    type = TokenType.MOD;
                    break;
                case '^':
                    type = TokenType.POWER;
                    break;
                case '<':
                    type = TokenType.LESS;
                    if (nextChar == '=') type = TokenType.LESSEQUAL;
                    break;
                case '>':
                    type = TokenType.GREATER;
                    if (nextChar == '=') type = TokenType.GREATEREQUAL;
                    break;
                case '=':
                    type = TokenType.ASSIGNMENT;
                    if (nextChar == '=') type = TokenType.EQUAL;
                    break;
                case '!':
                    type = TokenType.NOT;
                    if (nextChar == '=') type = TokenType.NOTEQUAL;
                    break;
                case '|':
                    type = TokenType.OR;
                    break;
                case '&':
                    type = TokenType.AND;
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
            Token token = null;
            String tokenText = "";
            char firstOperator = '\0';
            TokenizeState state = TokenizeState.DEFAULT;
            for (int index = 0; index < source.Length; index++)
            {
                char chr = source[index];
                switch (state)
                {
                    case TokenizeState.DEFAULT:
                        if (IsOp(chr))
                        {
                            firstOperator = chr;
                            TokenType opType = FindOpType(firstOperator, '\0');
                            token = new Token(Char.ToString(chr), opType);
                            state = TokenizeState.OPERATOR;
                        }
                        else if (Char.IsDigit(chr))
                        {
                            tokenText += chr;
                            state = TokenizeState.NUMBER;
                        }
                        else if (IsParen(chr))
                        {
                            TokenType parenType = FindParenType(chr);
                            tokens.Add(new Token(Char.ToString(chr), parenType));
                        }
                        else if (Char.IsLetter(chr))
                        {
                            tokenText += chr;
                            state = TokenizeState.KEYWORD;
                        }
                        else if (chr == '"')
                        {
                            state = TokenizeState.STRING;
                        }
                        else if (chr == '#')
                        {
                            state = TokenizeState.COMMENT;
                        }
                        break;
                    case TokenizeState.OPERATOR:
                        if (IsOp(chr))
                        {
                            TokenType opType = FindOpType(firstOperator, chr);
                            token = new Token(Char.ToString(firstOperator)
                            + Char.ToString(chr), opType);
                        }
                        else
                    {
                            tokens.Add(token);
                            state = TokenizeState.DEFAULT;
                            index--;
                        }
                        break;
                    case TokenizeState.NUMBER:
                        if (Char.IsDigit(chr))
                        {
                            tokenText += chr;
                        }
                        else
                        {
                            tokens.Add(new Token(tokenText, TokenType.NUMBER));
                            tokenText = "";
                            state = TokenizeState.DEFAULT;
                            index--;
                        }
                        break;
                    case TokenizeState.KEYWORD:
                        if (Char.IsLetterOrDigit(chr))
                        {
                            tokenText += chr;
                        }
                        else
                        {
                            TokenType type = FindStatementType(tokenText);
                            tokens.Add(new Token(tokenText, type));
                            tokenText = "";
                            state = TokenizeState.DEFAULT;
                            index--;
                        }
                        break;
                    case TokenizeState.STRING:
                        if (chr == '"')
                        {
                            tokens.Add(new Token(tokenText, TokenType.STRING));
                            tokenText = "";
                            state = TokenizeState.DEFAULT;
                        }
                        else
                        {
                            tokenText += chr;
                        }
                        break;
                    case TokenizeState.COMMENT:
                        if (chr == '\n')
                            state = TokenizeState.DEFAULT;
                        break;
                }
            }
            return tokens;
        }

        public TokenType FindStatementType(String str)
        {
            TokenType type = TokenType.UNKNOWN;
            switch (str)
            {
                case "script":
                    type = TokenType.SCRIPT;
                    break;
                case "end":
                    type = TokenType.END;
                    break;
                case "while":
                    type = TokenType.WHILE;
                    break;
                case "print":
                    type = TokenType.PRINT;
                    break;
                case "println":
                    type = TokenType.PRINTLN;
                    break;
                case "wait":
                    type = TokenType.WAIT;
                    break;
                default:
                    type = TokenType.KEYWORD;
                    break;
            }
            return type;
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



