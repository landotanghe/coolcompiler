using CoolCompiler.Tokens;
using CoolCompiler.Lexers;
using CoolCompilerTest.TokenTestHelpers;
using Xunit;
using System;

namespace CoolCompilerTest
{
    public class CoolLexerTest
    {
        private Lexer _lexer;
        
        public CoolLexerTest()
        {
            _lexer = new Lexer();
        }
        
        [Theory]
        [InlineData("int", TokenType.Keyword)]
        [InlineData("string", TokenType.Keyword)]
        [InlineData("int1", TokenType.Identifier)]
        [InlineData("\n\t ", TokenType.Whitespace)]
        [InlineData("(", TokenType.OpenBracket)]
        [InlineData(")", TokenType.CloseBracket)]
        [InlineData("$", TokenType.LexicalError)]
        [InlineData("@@@@", TokenType.LexicalError)]
        public void CoolLexer_SingleToken(string lexeme, TokenType tokenType)
        {
            var tokens = _lexer.GetTokens(lexeme);

            Assert.Equal(1, tokens.Count);            

            TokenAssert.That(tokens[0]).IsEquivalentWith(new Token(lexeme, tokenType));
        }
        
        [Fact]
        public void CoolLexer_DeclareVariable_KeyWordWhiteSpaceIdentifier()
        {
            var code = "int int1";

            var tokens = _lexer.GetTokens(code);
            
            TokenAssert.That(tokens)
                .FirstIs(new Token( "int", TokenType.Keyword ))
                .NextIs(new Token(" ", TokenType.Whitespace))
                .NextIs(new Token( "int1", TokenType.Identifier))
                .CountsMatch();
        }


    }
}
