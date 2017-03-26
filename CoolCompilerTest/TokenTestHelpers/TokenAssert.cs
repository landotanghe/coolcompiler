using CoolCompiler.Tokens;
using System.Collections.Generic;

namespace CoolCompilerTest.TokenTestHelpers
{
    public static class TokenAssert
    {
        public static TokenAssertThat That(Token token)
        {
            return new TokenAssertThat(token);
        }

        public static TokenListFirstAssertThat That(List<Token> tokens)
        {
            return new TokenListFirstAssertThat(tokens);
        }
    }
}
