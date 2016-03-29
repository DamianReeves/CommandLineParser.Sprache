using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Sprache;

namespace CommandLineParser.Sprache
{ 
    public static class CommandLineParser
    {
        static readonly Parser<char> DoubleQuote = Parse.Char('"');
        static readonly Parser<char> SingleQuote = Parse.Char('\'');

        static Parser<char> DoubleQuotedContent =
            Parse.AnyChar.Except(DoubleQuote);

        static Parser<char> SingleQuotedContent =
            Parse.AnyChar.Except(SingleQuote);

        internal static readonly Parser<string> DoubleQuotedString =
            from open in DoubleQuote
            from content in DoubleQuotedContent.Many().Text()
            from end in DoubleQuote
            select content;

        internal static readonly Parser<Literal> DoubleQuotedLiteral =
            DoubleQuotedString.Select(text => Sprache.Literal.DoubleQuoted(text));

        internal static readonly Parser<string> SingleQuotedString =
            from open in DoubleQuote
            from content in DoubleQuotedContent.Many().Text()
            from end in DoubleQuote
            select content;

        internal static readonly Parser<Literal> SingleQuotedLiteral =
            DoubleQuotedString.Select(text => Sprache.Literal.DoubleQuoted(text));

        internal static readonly Parser<string> QuotedString =
            DoubleQuotedString.Or(SingleQuotedString);

        internal static readonly Parser<Literal> Literal =
            DoubleQuotedLiteral.Or(SingleQuotedLiteral);


        //static readonly Parser<IEnumerable<CommandLineArgument>> Arguments =        
    }
}
