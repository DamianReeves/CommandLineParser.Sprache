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
        
        internal static readonly Parser<string> Identifier = ParserLib.Identifier;
        internal static readonly Parser<string> Option =
            from switchIndicator in Parse.Chars('-', '/').Once().Or(Parse.Char('-').Repeat(2))
            from id in Identifier
            select id;

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
            from open in SingleQuote
            from content in SingleQuotedContent.Many().Text()
            from end in SingleQuote
            select content;

        internal static readonly Parser<Literal> SingleQuotedLiteral =
            SingleQuotedString.Select(text => Sprache.Literal.SingleQuoted(text));

        internal static readonly Parser<string> QuotedString =
            DoubleQuotedString.XOr(SingleQuotedString);

        //internal static readonly Parser<string> UnquotedString =
        //    from initial in Parse.CharExcept(new[] { '"','\''})
        //    from content in Parse.Not(Parse.WhiteSpace)

        internal static readonly Parser<Literal> QuotedLiteral =
            DoubleQuotedLiteral.XOr(SingleQuotedLiteral);

        internal static readonly Parser<Literal> Literal =
            QuotedLiteral;


        internal static readonly Parser<IEnumerable<CommandLineArgument>> Arguments =
            Option.Select(o => (CommandLineArgument)new CommandLineArgument.CommandLineOption(o))
                .Or(Identifier.Select(c => new CommandLineArgument.CommandLineArg(c)))
                .Many();
                                
    }

    public static class ParserLib
    {
        static readonly Parser<char> ValidIdentifierCharacter =
            Parse.Char(c => char.IsLetterOrDigit(c) || c == '.' || c == '_', "valid identifier character");

        internal static readonly Parser<string> Identifier =
            //from leading in Parse.WhiteSpace.Many()
            from first in Parse.Letter.Once()
            from rest in ValidIdentifierCharacter.Many()
            from trailing in Parse.WhiteSpace.Many()
            select new string(first.Concat(rest).ToArray());

        internal static readonly Parser<IEnumerable> Identifiers =
            Identifier.Many();
    }
}
