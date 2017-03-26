using System.Collections.Generic;
using CoolCompiler.Tokens;

namespace CoolCompilerTest.TokenTestHelpers
{
    public class TokenListFirstAssertThat
    {
        private List<Token> tokens;

        public TokenListFirstAssertThat(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public TokenListAssertThat FirstIs(Token expected)
        {
            TokenAssert.That(tokens[0]).IsEquivalentWith(expected);

            var nextTokenIndexToCheck = 1;
            return new TokenListAssertThat(tokens, nextTokenIndexToCheck);
        }
    }

    public class TokenListAssertThat
    {
        private int _expectedTokensSeen;
        private List<Token> _tokens;

        public TokenListAssertThat(List<Token> tokens, int expectedTokensSeen)
        {
            _tokens = tokens;
            _expectedTokensSeen = expectedTokensSeen;
        }

        public TokenListAssertThat NextIs(Token expected)
        {
            AssertThereIsANextToken(expected);
            AssertNextTokenIsEquivalentTo(expected);

            return this;
        }

        private void AssertThereIsANextToken(Token expected)
        {
            TokenFieldAssert.True(_tokens.Count > _expectedTokensSeen, $"Not enough tokens, expected a {expected.GetType()} '{expected.Lexeme}' at postion {_expectedTokensSeen}");
        }

        private void AssertNextTokenIsEquivalentTo(Token expected)
        {
            TokenAssert.That(_tokens[_expectedTokensSeen]).IsEquivalentWith(expected);
            _expectedTokensSeen++;
        }

        public void CountsMatch()
        {
            TokenFieldAssert.Equal(_expectedTokensSeen, _tokens.Count, $"Too many tokens, expected {_expectedTokensSeen}");
        }
    }
}