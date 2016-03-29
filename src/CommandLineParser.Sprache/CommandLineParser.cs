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

        internal static Parser<char> SingleQuotedContent =
            Parse.AnyChar.Except(SingleQuote);

        internal static readonly Parser<string> DoubleQuotedString =
            from open in DoubleQuote
            from content in DoubleQuotedContent.Many().Text()
            from end in DoubleQuote
            select content;


        //static readonly Parser<IEnumerable<CommandLineArgument>> Arguments =        
    }
}
