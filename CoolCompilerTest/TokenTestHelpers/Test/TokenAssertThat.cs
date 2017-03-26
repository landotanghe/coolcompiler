using CoolCompiler.Tokens;
using System.Collections.Generic;
using Xunit;

namespace CoolCompilerTest.TokenTestHelpers.Test
{
    public class TokenAssertTest
    {

        [Fact]
        public void TokenAssert_SameLexemeAndType_Passes()
        {
            var expected = new Token("test", TokenType.Identifier);
            var actual = new Token("test", TokenType.Identifier);

            TokenAssert.That(actual).IsEquivalentWith(expected);
        }

        [Fact]
        public void TokenAssert_DifferentType_Fails()
        {
            var expected = new Token("test", TokenType.Identifier);
            var actual = new Token("test", TokenType.Keyword);

            Assert.Throws<TokenAssertException>(() => TokenAssert.That(actual).IsEquivalentWith(expected));
        }

        [Fact]
        public void TokenAssert_DifferentLexeme_Fails()
        {
            var expected = new Token("test1", TokenType.Identifier);
            var actual = new Token("test2", TokenType.Identifier);

            Assert.Throws<TokenAssertException>(() => TokenAssert.That(actual).IsEquivalentWith(expected));
        }

        [Fact]
        public void TokenAssert_List_Single_SameLexemeAndType_Passes()
        {
            var actual = new List<Token> { new Token("test", TokenType.Identifier) };

            TokenAssert.That(actual)
                .FirstIs(new Token("test" , TokenType.Identifier ))
                .CountsMatch();
        }


        [Fact]
        public void TokenAssert_List_Multiple_SameLexemeAndType_Passes()
        {
            var actual = new List<Token> {
                new Token ("test", TokenType.Identifier) ,
                new Token ("if", TokenType.Keyword),
                new Token (" ", TokenType.Whitespace) };

            TokenAssert.That(actual)
                .FirstIs(new Token("test", TokenType.Identifier))
                .NextIs(new Token("if", TokenType.Keyword))
                .NextIs(new Token(" ", TokenType.Whitespace))
                .CountsMatch();
        }

        [Fact]
        public void TokenAssert_List_Single_DifferentLexeme_Fails()
        {
            var actual = new List<Token> { new Token("test", TokenType.Identifier) };
            
            Assert.Throws<TokenAssertException>(() =>
                TokenAssert.That(actual)
                    .FirstIs(new Token("fails", TokenType.Identifier))
                    .CountsMatch()
            );
        }
                
        [Fact]
        public void TokenAssert_List_Multiple_LastOneHasDifferentLexeme_Fails()
        {
            var actual = new List<Token> {
                new Token ("this", TokenType.Identifier) ,
                new Token ("test", TokenType.Keyword),
                new Token ("fails", TokenType.Whitespace) };

            Assert.Throws<TokenAssertException>(() =>
                TokenAssert.That(actual)
                    .FirstIs(new Token("this", TokenType.Identifier))
                    .NextIs(new Token("test", TokenType.Keyword))
                    .NextIs(new Token("FAILS", TokenType.Whitespace))
                    .CountsMatch()
                );
        }
        
        [Fact]
        public void TokenAssert_List_CountMismatch_Fails()
        {
            var actual = new List<Token> {
                new Token ("test", TokenType.Identifier) ,
                new Token ("if", TokenType.Keyword),
                new Token (" ", TokenType.Whitespace) };


            Assert.Throws<TokenAssertException>(() =>
                TokenAssert.That(actual)
                    .FirstIs(new Token("test", TokenType.Identifier))
                    .NextIs(new Token("if", TokenType.Keyword))
                    .CountsMatch()
            );
        }

    }
}
