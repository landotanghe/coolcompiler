using CoolCompiler.Tokens;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CoolCompiler.Lexers
{
    public class Lexer
    {
        private string _code;
        
        private LexemeFinder _whiteSpace = new LexemeFinder("[\t\n ]+", TokenType.Whitespace);
        private LexemeFinder _identifier = new LexemeFinder("[a-zA-Z_][a-zA-Z0-9_]*", TokenType.Identifier);
        private LexemeFinder _keyWord = new LexemeFinder("(int)|(string)", TokenType.Keyword);
        private LexemeFinder _openBracket = new LexemeFinder("\\(", TokenType.OpenBracket);
        private LexemeFinder _closeBracket = new LexemeFinder("\\)", TokenType.CloseBracket);

        public List<Token> GetTokens(string code)
        {
            _code = code;
            var tokens = new List<Token>();

            while (HasInputRemaining())
            {
                var token = GetNextToken();
                FailOnEmptyLexeme(token);
                RemoveTokenFromCode(token);
                tokens.Add(token);
            }

            return tokens;
        }

        private void FailOnEmptyLexeme(Token token)
        {
            if (String.IsNullOrEmpty(token.Lexeme))
            {
                throw new Exception("Empty lexeme results in an infinte loop");
            }
        }

        private bool HasInputRemaining()
        {
            return !String.IsNullOrEmpty(_code);
        }

        private void RemoveTokenFromCode(Token token)
        {
            var startIndex = token.Lexeme.Length;
            _code = _code.Substring(startIndex);
        }

        private Token GetNextToken()
        {
            var token = GetLowPriorityToken();

            if (_keyWord.IsFullMatch(token.Lexeme))
            {
                return (new Token ( token.Lexeme, TokenType.Keyword ));
            }else
            {
                return token;
            }
        }

        private Token GetLowPriorityToken()
        {
            var lowPriorityLexemeFinders = new List<LexemeFinder>()
            {
                _whiteSpace,
                _openBracket, _closeBracket,
                _identifier
            };

            if (_openBracket.IsPrefixOf(_code))
            {
                var openBracketLexeme = _openBracket.GetLexeme(_code);
                return (new Token(openBracketLexeme, TokenType.OpenBracket));
            }


            if (_closeBracket.IsPrefixOf(_code))
            {
                var openBracketLexeme = _closeBracket.GetLexeme(_code);
                return (new Token(openBracketLexeme, TokenType.CloseBracket));
            }

            foreach (var lexemeFinder in lowPriorityLexemeFinders)
            {
                if (lexemeFinder.IsPrefixOf(_code))
                {
                    var lexeme = lexemeFinder.GetLexeme(_code);

                    return new Token(lexeme, lexemeFinder.TokenType);
                }
            }

            return new Token(_code, TokenType.LexicalError);
        }

        private class LexemeFinder
        {
            private Regex _regex;

            public TokenType TokenType { get; }

            public LexemeFinder(string pattern, TokenType tokenType)
            {
                _regex = new Regex($"^{pattern}");
                TokenType = tokenType;
            }

            public bool IsPrefixOf(string code)
            {
                return _regex.IsMatch(code);
            }

            public bool IsFullMatch(string code)
            {
                var match = _regex.Match(code);
                return match.Length == code.Length;
            }

            public string GetLexeme(string code)
            {
                return _regex.Match(code).Value;
            }
            
        }
    }
}
