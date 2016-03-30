namespace CommandLineParser.Sprache
{
    public abstract class Literal
    {
        private Literal(string text, QuoteType quoteType)
        {
            Text = text;
            QuoteType = quoteType;
        }

        public string Text
        {
            get;
        }

        public QuoteType QuoteType
        {
            get;
        }

        public static Literal Unquoted(string text)
        {
            return new UnquotedLiteral(text);
        }

        public static Literal DoubleQuoted(string text)
        {
            return new DoubleQuotedLiteral(text);
        }

        public static Literal SingleQuoted(string text)
        {
            return new SingleQuotedLiteral(text);
        }

        public sealed class UnquotedLiteral : Literal
        {
            public UnquotedLiteral(string text): base (text, QuoteType.None)
            {
            }
        }

        public sealed class DoubleQuotedLiteral : Literal
        {
            public DoubleQuotedLiteral(string text): base (text, QuoteType.DoubleQuoted)
            {
            }
        }

        public sealed class SingleQuotedLiteral : Literal
        {
            public SingleQuotedLiteral(string text): base (text, QuoteType.SingleQuoted)
            {
            }
        }
    }
}