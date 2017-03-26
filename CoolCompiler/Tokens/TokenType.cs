using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolCompiler.Tokens
{
    public enum TokenType
    {
        // Arithmetic
        Multiplicator,
        Divider,
        Plus,
        Minus,

        Assignment,
        Identifier,
        Keyword,
        LexicalError,
        LogicalOperator,
        Whitespace,

        OpenBracket,
        CloseBracket
    }
}
