using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Parser
    {
        public Dictionary<string, Object> symbolTable = new Dictionary<string, object>();

        public static int currentTokenPosition = 0;
        public List<Token> tokens;

        //Constructor

        public Parser() { }

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public static void main(string[] args)
        {
            Tokenizer tokenizer = new Tokenizer();
            Parser calc = new Parser();
            List<String> expresssionList = new List<String>();

            // Remember, we use a trailing white space as a terminating character!

            expresssionList.Add("(100*2+2)*2+5>=500 ");
            expresssionList.Add("((5+1)*100-2+3) ");
            expresssionList.Add("100-30/2+13>=10 ");
            expresssionList.Add("(853+92*5)*10-20/2+771 ");
            expresssionList.Add("(5)*2 ");

            List<Node> commandList = new List<Node>();
            foreach(String expression in expresssionList)
            {
                Console.WriteLine("Expression: " + expression);
                currentTokenPosition = 0;
                calc.tokens = tokenizer.Tokenize(expression);
                Node node = calc.BooleanExpression();

                if (node != null)
                {

                commandList.Add(node);
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Now, lets calculate expressions...");

            foreach (Node command in commandList)
                Console.WriteLine("Expression Result: " + command.Eval());
        }

        public Node Term()
        {
            Node node = SignedFactor();

            while (IsMulOp(CurrentToken().type))
            {
                switch (CurrentToken().type)
                {
                    case TokenType.MULTIPLY:
                        node = new BinOpNode(TokenType.MULTIPLY, node, Multiply());
                        break;
                    case TokenType.DIVIDE:
                        node = new BinOpNode(TokenType.DIVIDE, node, Divide());
                        break;
                    case TokenType.MOD:
                        node = new BinOpNode(TokenType.MOD, node, Mod());
                        break;
                }
            }
            return node;
        }   

        public Node ArithmeticExpression()
        {
            Node node = Term();
            while (IsAddOp(CurrentToken().type))
            {
                switch (CurrentToken().type)
                {
                    case TokenType.ADD:
                        node = new BinOpNode(TokenType.ADD, node, Add());
                        break;
                    case TokenType.SUBTRACT:
                        node = new BinOpNode(TokenType.SUBTRACT, node, Subtract());
                        break;
                }
            }
            return node;

        }

        public Node PreFactor()
        {
            Node node = Factor();

            while (IsPowOp(CurrentToken().type))
            {
                switch (CurrentToken().type)
                {
                    case TokenType.POWER:
                        node = new BinOpNode(TokenType.POWER, node, Power());
                        break;
                }
            }
            return node;
        }

        public Node Factor()
        {
            Node result = null;
            if (CurrentToken().type == TokenType.LEFT_PAREN)
            {
                MatchAndEat(TokenType.LEFT_PAREN);
                result = Expression();
                MatchAndEat(TokenType.RIGHT_PAREN);
            }
            else if (IsNumber())
            {
                Token token = MatchAndEat(TokenType.NUMBER);
                result = new NumberNode(Int32.Parse(token.text));
            }
            else if (IsString())
            {
                Token token = MatchAndEat(TokenType.STRING);
                result = new StringNode((token.text).ToString());
            }
            else if (IsKeyWord())
            {
                result = Variable();
            }
            return result;
        }

        public Node SignedFactor()
        {
            if (CurrentToken().type == TokenType.SUBTRACT)
            {
                MatchAndEat(TokenType.SUBTRACT);
                Node node = new NegOpNode(PreFactor());
                return node;
            }
            return PreFactor();
        }

        public Node NotFactor()
        {
            if (CurrentToken().type == TokenType.NOT)
            {
                MatchAndEat(TokenType.NOT);
                Node node = BooleanFactor();
                return new NotOpNode(node);
            }
            return BooleanFactor();
        }

        //Arithmetic methods

        public Node Add()
        {
            MatchAndEat(TokenType.ADD);
            return Term();
        }

        public Node Subtract()
        {
            MatchAndEat(TokenType.SUBTRACT);
            return Term();
        }

        public Node Multiply()
        {
            MatchAndEat(TokenType.MULTIPLY);
            return Factor();
        }

        public Node Divide()
         {
            MatchAndEat(TokenType.DIVIDE);
            return Factor();
         }

        public Node Power()
        {
            MatchAndEat(TokenType.POWER);
            return PreFactor();
        }

        public Node Mod()
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

        public Node Relation()
        {
            Node node = ArithmeticExpression();
            if (IsRelOp(CurrentToken().type))
            {
                switch (CurrentToken().type)
                {
                    case TokenType.LESS:
                        node = Less(node);
                        break;
                    case TokenType.GREATER:
                        node = Greater(node);
                        break;
                    case TokenType.EQUAL:
                        node = Equal(node);
                        break;
                    case TokenType.NOTEQUAL:
                        node = NotEqual(node);
                        break;
                    case TokenType.LESSEQUAL:
                        node = LessEqual(node);
                        break;
                    case TokenType.GREATEREQUAL:
                        node = GreaterEqual(node);
                        break;
                }
            }
            return node;
        }

        public Node Less(Node node)
        {
            MatchAndEat(TokenType.LESS);
            return new BinOpNode(TokenType.LESS, node, ArithmeticExpression());
        }

        public Node Greater(Node node)
        {
            MatchAndEat(TokenType.GREATER);
            return new BinOpNode(TokenType.GREATER, node, ArithmeticExpression());
        }

        public Node Equal(Node node)
        {
            MatchAndEat(TokenType.EQUAL);
            return new BinOpNode(TokenType.EQUAL, node, ArithmeticExpression());
        }

        public Node NotEqual(Node node)
        {
            MatchAndEat(TokenType.NOTEQUAL);
            return new BinOpNode(TokenType.NOTEQUAL, node, ArithmeticExpression());
        }

        public Node LessEqual(Node node)
        {
            MatchAndEat(TokenType.LESSEQUAL);
            return new BinOpNode(TokenType.LESSEQUAL, node, ArithmeticExpression());
        }

        public Node GreaterEqual(Node node)
        {
            MatchAndEat(TokenType.GREATEREQUAL);
            return new BinOpNode(TokenType.GREATEREQUAL, node, ArithmeticExpression());
        }

        public Node BooleanFactor()
        {
            return Relation();
        }

        public Node BooleanTerm()
        {
            Node node = NotFactor();
            while (CurrentToken().type == TokenType.AND)
            {
                MatchAndEat(TokenType.AND);
                node = new BinOpNode(TokenType.AND, node, NotFactor());
            }
            return node;
        }

        public Node BooleanExpression()
        {
            Node node = BooleanTerm();
            while (IsLogicalOp(CurrentToken().type))
            {
                switch (CurrentToken().type)
                {
                    case TokenType.OR:
                        MatchAndEat(TokenType.OR);
                        node = new BinOpNode(TokenType.OR, node, BooleanTerm());
                        break;
                }
            }
            return node;
        }

        public Node Expression()
        {
            return BooleanExpression();
        }

        public Node FunctionDefinition()
        {
            MatchAndEat(TokenType.DEF);
            String functionName = MatchAndEat(TokenType.KEYWORD).text;
            MatchAndEat(TokenType.LEFT_PAREN);

            List<Parameter> parameters = FunctionDefParameters();
            MatchAndEat(TokenType.RIGHT_PAREN);
            Node functionBody = Block();

            Function function = new Function(functionName, functionBody, parameters);
            Node functionVariable = new AssignmentNode(functionName, function, this);
            return functionVariable;
        }

        public List<Parameter> FunctionDefParameters()
        {
            List<Parameter> parameters = null;
            if (CurrentToken().type == TokenType.KEYWORD)
            {
                parameters = new List<Parameter>();
                parameters.Add(new Parameter(MatchAndEat(TokenType.KEYWORD).text));
                while (CurrentToken().type == TokenType.COMMA)
                {
                    MatchAndEat(TokenType.COMMA);
                    parameters.Add(new Parameter(MatchAndEat(TokenType.KEYWORD).text));
                }
            }
            return parameters;
        }

        //Parser Methods

        public bool IsPowOp(TokenType type)
        {
            return type == TokenType.POWER;
        }

        public bool IsMulOp(TokenType type)
        {
            return type == TokenType.MULTIPLY || type == TokenType.DIVIDE || type == TokenType.MOD;
        }

        public bool IsAddOp(TokenType type)
        {
            return type == TokenType.ADD || type == TokenType.SUBTRACT;
        }

        public bool IsMultiDigitOp(TokenType type)
        {
            return type == TokenType.LESSEQUAL || type == TokenType.GREATEREQUAL;
        }

        public bool IsRelOp(TokenType type)
        {
            bool lgOps = type == TokenType.LESS || type == TokenType.GREATER;
            bool eqOps = type == TokenType.EQUAL || type == TokenType.NOTEQUAL;
            return eqOps || lgOps || IsMultiDigitOp(type);
        }

        public bool IsLogicalOp(TokenType type)
        {
            return type == TokenType.OR || type == TokenType.AND;
        }

        public bool IsNumber()
        {
            return CurrentToken().type == TokenType.NUMBER;
        }

        public bool IsString()
        {
            return CurrentToken().type == TokenType.STRING;
        }

        public bool IsKeyWord()
        {
            return CurrentToken().type == TokenType.KEYWORD;
        }

        public bool IsAssignment()
        {
            TokenType type = CurrentToken().type;
            return type == TokenType.KEYWORD &&
            NextToken().type == TokenType.ASSIGNMENT;
        }

        public bool IsWhile()
        {
            return CurrentToken().type == TokenType.WHILE;
        }

        public bool IsIfElse()
        {
            TokenType type = CurrentToken().type;
            return type == TokenType.IF || type == TokenType.ELSE;
        }

        public bool IsArrayAccess()
        {
            TokenType type = CurrentToken().type;
            return type == TokenType.KEYWORD &&
            NextToken().type == TokenType.LEFT_BRACKET;
        }

        public bool IsFunctionDef()
        {
            TokenType type = CurrentToken().type;
            return type == TokenType.DEF && NextToken().type == TokenType.KEYWORD;
        }

        public bool IsFunctionCall()
        {
            TokenType type = CurrentToken().type;
            return type == TokenType.KEYWORD && NextToken().type == TokenType.LEFT_PAREN;
        }

        public List<Token> getTokens()
        {
            return tokens;
        }

        public Node Statement()
        {
            Node node = null;
            TokenType type = CurrentToken().type;
            if (IsAssignment())
            {
                node = Assignment();
            }
            else if (IsArrayAccess())
            {
                node = ArrayUpdate();
            }
            else if (IsWhile())
            {
                node = While();
            }
            else if (IsIfElse())
            {
                node = If();
            }
            else if (IsFunctionDef())
            {
                node = FunctionDefinition();
            }
            else if (IsFunctionCall())
            {
                node = FunctionCall();
            }
            else if (type == TokenType.PRINT)
            {
                MatchAndEat(TokenType.PRINT);
                node = new PrintNode(Expression(), "sameline"); 
            }
            else if (type == TokenType.PRINTLN)
            {
                MatchAndEat(TokenType.PRINTLN);
                node = new PrintNode(Expression(), "newline");
            }
            else if (type == TokenType.WAIT)
            {
                MatchAndEat(TokenType.WAIT);
                node = new WaitNode(Expression());
            }
            else 
            {
                Console.WriteLine("Unknown language construct: "
                + CurrentToken().text);
                Environment.Exit(0);
            }
            return node;
        }

        public Node Variable()
        {
            Node node = null;
            if (NextToken().type == TokenType.LEFT_PAREN)
            {
                node = FunctionCall();
            }
            else
            {
                Token token = MatchAndEat(TokenType.KEYWORD);
                Node varNode = new VariableNode(token.text, this);
                // Handle array access here
                if (CurrentToken().type == TokenType.LEFT_BRACKET)
                {
                    MatchAndEat(TokenType.LEFT_BRACKET);
                    Node key = Expression();
                    MatchAndEat(TokenType.RIGHT_BRACKET);
                    return new LookupNode((VariableNode)varNode, key);
                }
                else return varNode;
            }
            return node;
        }

        public BlockNode Block()
        {
            List<Node> statements = new List<Node>();
            while (!(CurrentToken().type.Equals(TokenType.END)))
            {
                statements.Add(Statement());
                Console.WriteLine("=============== Statement found! =================");
            }
            MatchAndEat(TokenType.END);
            Console.WriteLine("END TOKEN FOUND");
            return new BlockNode(statements);
        }

        public Node Assignment()
        {
            Node node = null;
            String name = MatchAndEat(TokenType.KEYWORD).text;

            MatchAndEat(TokenType.ASSIGNMENT);

            if (CurrentToken().type == TokenType.LEFT_BRACKET)
            {
                node = ArrayDefinition(name);
            }
            else
            {
                Node value = Expression();
                node = new AssignmentNode(name, value, this);
            }
            return node;
        }

        public Node While()
        {
            Node condition, body;
            MatchAndEat(TokenType.WHILE);
            condition = Expression();
            body = Block();
            return new WhileNode(condition, body);
        }

        public Node If()
        {
            Node condition = null, thenPart = null, elsePart = null;
            MatchAndEat(TokenType.IF);
            condition = Expression();
            thenPart = Block();
            if (CurrentToken().type == TokenType.ELSE)
            {
                MatchAndEat(TokenType.ELSE);
                if (CurrentToken().type == TokenType.IF) elsePart = If();
                else elsePart = Block();
            }
            return new IfNode(condition, thenPart, elsePart);
        }

        public Node ArrayDefinition(String name)
        {
            List<Node> elements = new List<Node>();
            MatchAndEat(TokenType.LEFT_BRACKET);
            if (CurrentToken().type != TokenType.RIGHT_BRACKET)
            {
                elements.Add(Expression());
                while (CurrentToken().type != TokenType.RIGHT_BRACKET)
                {
                    MatchAndEat(TokenType.COMMA);
                    elements.Add(Expression());
                }
            }
            MatchAndEat(TokenType.RIGHT_BRACKET);
            return new AssignmentNode(name, new ArrayNode(elements), this);
        }

        public Node ArrayUpdate()
        {
            String arrayName = MatchAndEat(TokenType.KEYWORD).text;
            Node array = new VariableNode(arrayName, this);
            MatchAndEat(TokenType.LEFT_BRACKET);
            Node indexExpr = Expression();
            MatchAndEat(TokenType.RIGHT_BRACKET);
            MatchAndEat(TokenType.ASSIGNMENT);
            Node rightSideExpr = Expression();

            return new ArrayUpdateNode(array, indexExpr, rightSideExpr);
        }

        public Node FunctionCall()
        {
            String functionName = MatchAndEat(TokenType.KEYWORD).text;
            Node calleeFunctionName = new VariableNode(functionName, this);

            MatchAndEat(TokenType.LEFT_PAREN);
            List<Parameter> actualParameters = FunctionCallParameters();

            MatchAndEat(TokenType.RIGHT_PAREN);
            Node functionCallNode = new FunctionCallNode(calleeFunctionName,actualParameters, this);

            return functionCallNode;
        }

        public List<Parameter> FunctionCallParameters()
        {
            List<Parameter> actualParameters = null;
            Node expression = Expression();
            if (expression != null)
            {
                actualParameters = new List<Parameter>();
                actualParameters.Add(new Parameter(expression));
                while (CurrentToken().type == TokenType.COMMA)
                {
                    MatchAndEat(TokenType.COMMA);
                    actualParameters.Add(new Parameter(Expression()));
                }
            }
            return actualParameters;
        }

        //Symbol Methods

        public Object setVariable(String name, Object value)
        {
            if (symbolTable.ContainsKey(name))
            {
                if ((name.Equals("PI")) || (name.Equals("EULER")))
                {
                    Console.WriteLine("Reserved Word: " + name);
                    System.Environment.Exit(1);
                }
                symbolTable[name] = value;
            }
            else
                symbolTable.Add(name, value);
            return value;
        }

        public Object getVariable(String name)
        {
            Object value;
            symbolTable.TryGetValue(name, out value);
            if (value != null) return value;
            return null;
        }

        public Object ExecuteFunction(Function function, List<BoundParameter> boundParameters)
        {
            // Save the symbolTable
            Dictionary<String, Object> savedSymbolTable = new Dictionary<String, Object>(symbolTable);
            // Get bound parameters
            for (int index = 0; index < boundParameters.Count; index++)
            {
                BoundParameter param = (BoundParameter)boundParameters[index];
                setVariable(param.getName(), param.getValue());
            }
            // Eval function
            Object ret = function.Eval();
            // Restore symbolTable
            symbolTable = savedSymbolTable;
            return ret;
        }
    }

}

    

