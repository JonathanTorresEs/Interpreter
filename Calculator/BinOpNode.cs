﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class BinOpNode : Node
    {

        public TokenType op;
        public Node left;
        public Node right;

        public BinOpNode() { }

        public BinOpNode(TokenType op, Node left, Node right)
        {
            this.op = op;
            this.left = left;
            this.right = right;
        }

        public double ToDouble(Node node)
        {
            Object res = node.Eval();
            return Double.Parse(res.ToString());
        }

        public bool ToBoolean(Node node)
        {
            Object res = node.Eval();
            return (bool)res;
        }

        public Object ToObject(Node node)
        {
            return node.Eval();
        }

        public override Object Eval()
        {
            Object result = null;
            switch (op)
            {
                case TokenType.ADD:
                    result = (double)(ToDouble(left) + ToDouble(right));
                    break;
                case TokenType.SUBTRACT:
                    result = (double)(ToDouble(left) - ToDouble(right));
                    break;
                case TokenType.MULTIPLY:
                    result = (double)(ToDouble(left) * ToDouble(right));
                    break;
                case TokenType.MOD:
                    result = (double)(ToDouble(left)) % (ToDouble(right));
                    break;
                case TokenType.POWER:
                    result = (double)(Math.Pow(ToDouble(left), ToDouble(right)));
                    break;
                case TokenType.DIVIDE:
                    if (ToDouble(right) == 0)
                    {
                        Console.WriteLine("Error: Division by Zero!");
                        Environment.Exit(0);
                    }
                    result = (double)(ToDouble(left) / ToDouble(right));
                    break;
                case TokenType.LESS:
                    result = (bool)(ToDouble(left) < ToDouble(right));
                    break;
                case TokenType.GREATER:
                    result = (bool)(ToDouble(left) > ToDouble(right));
                    break;
                // != and == work as equal and !equal for strings
                case TokenType.EQUAL:
                    result = (bool)(ToObject(left).Equals(ToObject(right)));
                    break;
                case TokenType.NOTEQUAL:
                    result = (bool)(!ToObject(left).Equals(ToObject(right)));
                    break;
                case TokenType.LESSEQUAL:
                    result = (bool)(ToDouble(left) <= ToDouble(right));
                    break;
                case TokenType.GREATEREQUAL:
                    result = (bool)(ToDouble(left) >= ToDouble(right));
                    break;
                case TokenType.OR:
                    result = (bool)(ToBoolean(left) || ToBoolean(right));
                    break;
                case TokenType.AND:
                    result = (bool)(ToBoolean(left) && ToBoolean(right));
                    break;
            }
            return result;
        }
        public static void main(string[] args)
        {
            NumberNode firstNumber = new NumberNode(100);
            NumberNode secondNumber = new NumberNode(200);

            Node sumNode = new BinOpNode(TokenType.ADD, firstNumber, secondNumber);

            Console.WriteLine("100 + 200 = " + sumNode.Eval());
            Node compareNode = new BinOpNode(TokenType.LESS, firstNumber, secondNumber);

            Console.WriteLine("100 < 200 = " + compareNode.Eval());
        }
    }

}
