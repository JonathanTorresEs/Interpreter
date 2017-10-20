﻿using System;

namespace Calculator
{
    public enum TokenType
    {
        NUMBER, NEWLINE, OPERATOR, EOF, UNKNOWN,
        ADD, SUBTRACT, MULTIPLY, DIVIDE, MOD, POWER, LEFT_PAREN,
        RIGHT_PAREN, LESS, GREATER, EQUAL, NOTEQUAL,
        LESSEQUAL, GREATEREQUAL, ASSIGNMENT, NOT, OR, AND,
        KEYWORD, PRINT, PRINTLN, WAIT, END, SCRIPT
    }
}


