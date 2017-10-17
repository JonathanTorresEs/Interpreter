using System;

namespace Calculator
{
    public enum TokenType
    {
        NUMBER, NEWLINE, OPERATOR, EOF, UNKNOWN,
        ADD, SUBTRACT, MULTIPLY, DIVIDE, MOD, POWER, LEFT_PAREN, RIGHT_PAREN
    }
}


