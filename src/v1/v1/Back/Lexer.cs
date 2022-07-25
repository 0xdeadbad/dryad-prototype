﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v1.Back
{
    public class Token 
    {
        public string _type = "";
        public string _value = "";
    
        public Token(string type, string value)
        {
            _type = type;
            _value = value;
        }
    }

    public class TokenTypes
    {
        public readonly string DIGITS = "0123456789";
        // Types
        public readonly string TInt = "INT";
        public readonly string TFloat = "FLOAT";
        public readonly string TPlus = "MATH_PLUS";
        public readonly string TMinus = "MATH_MINUS";
        public readonly string TMultiply = "MATH_MULTIPLY";
        public readonly string TDivide = "MATH_DIVIDE";
        public readonly string TLParen = "L_PAREN";
        public readonly string TRParen = "R_PAREN";
    }
    
    
    public class Tokenizer
    {
        private string text { get; set; }
        private int pos { get; set; }
        private int line { get; set; }
        private int col { get; set; }
        private char current { get; set; }
        private List<Token> tokens { get; set; }
        public TokenTypes types = new();

        public Tokenizer(string text)
        {
            tokens = new List<Token>();
            this.text = text;
            this.pos = 0;
            this.line = 1;
            this.col = 1;
            this.current = text[pos];
        }

        public void Next()
        {
            if (pos < text.Length - 1)
            {
                pos++;
                current = text[pos];
                col++;
            }
            else
            {
                current = '\0';
            }
        }

        public void SkipComment()
        {
            while (current != '\0' && current != '\n')
            {
                Next();
            }
        }

        public void NextToken()
        {
            while (current != '\0')
            {
                if(current == ' ' || current == '\t' || current == '\n')
                {
                    Next();
                    continue;
                }

                if(current == '\n')
                {
                    line++;
                    col = 1;
                }
                
                if(current == '#')
                {
                    SkipComment();
                    continue;
                }

                if(current == '+')
                {
                    tokens.Add(new Token(types.TPlus, "+"));
                    Next();
                    continue;
                }

                if (current == '-')
                {
                    tokens.Add(new Token(types.TMinus, "-"));
                    Next();
                    continue;
                }

                if (current == '*')
                {
                    tokens.Add(new Token(types.TMultiply, "*"));
                    Next();
                    continue;
                }

                if (current == '/')
                {
                    tokens.Add(new Token(types.TDivide, "/"));
                    Next();
                    continue;
                }

                if (current == '(')
                {
                    tokens.Add(new Token(types.TLParen, "("));
                    Next();
                    continue;
                }

                if (current == ')')
                {
                    tokens.Add(new Token(types.TRParen, ")"));
                    Next();
                    continue;
                }

                if (types.DIGITS.Contains(current))
                {
                    string value = "";
                    while (types.DIGITS.Contains(current))
                    {
                        value += current;
                        Next();
                    }
                    tokens.Add(new Token(types.TInt, value));
                    continue;
                }
            }
        }
    }

    public class Lexer
    {
    }
}
