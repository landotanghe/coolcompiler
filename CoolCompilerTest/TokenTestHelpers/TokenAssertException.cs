using System;
using Xunit;

namespace CoolCompilerTest.TokenTestHelpers
{
    public class TokenAssertException : Exception
    {
        public TokenAssertException(string msg): base(msg) { }        
    }

    public class TokenFieldAssert
    {
        public static void Equal(object expected, object actual, string message)
        {
            try
            {
                Assert.Equal(expected, actual);
            }
            catch (Exception)
            {
                throw new TokenAssertException(message);
            }
        }

        public static void True(bool result, string message)
        {
            if (!result)
            {
                throw new TokenAssertException(message);
            }
        }
    }
}
