namespace CoolCompiler.Tokens
{
    public sealed class Token
    {
        public string Lexeme { get; set; }
        public TokenType TokenType { get; set; }   
        
        public Token(string lexeme, TokenType tokenType)
        {
            Lexeme = lexeme;
            TokenType = tokenType;
        }     
    }
}
