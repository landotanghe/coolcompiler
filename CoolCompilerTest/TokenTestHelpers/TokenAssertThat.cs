using CoolCompiler.Tokens;
using System;
using Xunit;

namespace CoolCompilerTest.TokenTestHelpers
{
    public class TokenAssertThat
    {
        private Token token;

        public TokenAssertThat(Token token)
        {
            this.token = token;
        }

        public void IsEquivalentWith(Token expected)
        {
            TokenFieldAssert.Equal(expected.TokenType, token.TokenType, $"TokenClass '{expected.TokenType}' expected, but found '{token.TokenType}'");
            TokenFieldAssert.Equal(expected.Lexeme, token.Lexeme, $"Lexeme '{expected.Lexeme}' expected, but found '{token.Lexeme}'");
        }
    }
}
