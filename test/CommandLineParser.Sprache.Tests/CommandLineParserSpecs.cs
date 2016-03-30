using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Sprache;
using Xunit;

namespace CommandLineParser.Sprache
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class CommandLineParserSpecs
    {
        [Theory]
        [InlineData("Verbosity",true, "Verbosity")]
        [InlineData("Name1 Name2", true, "Name1")]
        public void Identifier_accepts_only_valid_input(string commandLine, bool accept, string expectedId)
        {
            var input = new Input(commandLine);
            var result = CommandLineParser.Identifier(input);
            var expected = new
            {
                WasSuccessful = accept,
                Value = expectedId
            };
            result.ShouldBeEquivalentTo(expected, o => o.ExcludingMissingMembers());
        }      

        [Theory]
        [InlineData(@"""Double Quoted""",true)]
        [InlineData(@"""I have a 'single quoted' string.""", true)]
        [InlineData(@"'Don't accept no double quotes.'", false)]
        [InlineData(@"'Don't accept ""quotes inside"".'", false)]
        public void DoubleQuotedString_Parser_Accepts_Only_Valid_Input(string commandLine, bool accept)
        {
            var input = new Input(commandLine);
            var result = CommandLineParser.DoubleQuotedString(input);
            var expected = new
            {
                Value = commandLine?.Trim('"'),
                WasSuccessful = accept
            };
            if (accept)
            {
                result.ShouldBeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
            }
            else
            {
                result.WasSuccessful.Should().Be(accept);
            }
            
        }

        [Theory]
        [InlineData(@"""Double Quoted""", false)]
        [InlineData(@"""I have a 'single quoted' string.""", false)]
        [InlineData(@"'Should accept.'", true)]
        [InlineData(@"'Should accept ""quotes inside"".'", true)]
        public void SingleQuotedString_Parser_Accepts_Only_Valid_Input(string commandLine, bool accept)
        {
            var input = new Input(commandLine);
            var result = CommandLineParser.SingleQuotedString(input);
            var expected = new
            {
                Value = commandLine?.Trim('\''),
                WasSuccessful = accept
            };
            if (accept)
            {
                result.ShouldBeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
            }
            else
            {
                result.WasSuccessful.Should().Be(accept);
            }

        }

        [Theory]
        [InlineData(@"""Double Quoted""", true)]
        [InlineData(@"""I have a 'single quoted' string.""", true)]        
        public void Literal_Parser_Accepts_Double_Quoted_strings(string commandLine, bool accept)
        {
            var input = new Input(commandLine);
            var result = CommandLineParser.Literal(input);
            var expected = new
            {
                Value = accept ? Literal.DoubleQuoted(commandLine?.Trim('"')) : default(Literal),
                WasSuccessful = accept
            };

            result.ShouldBeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());            
        }

        [Theory]
        [InlineData(@"'Should accept.'", true)]
        [InlineData(@"'Should accept ""quotes inside"".'", true)]
        public void Literal_Parser_Accepts_Single_Quoted_strings(string commandLine, bool accept)
        {
            var input = new Input(commandLine);
            var result = CommandLineParser.Literal(input);
            var expected = new
            {
                Value = accept ? Literal.SingleQuoted(commandLine?.Trim('\'')) : default(Literal),
                WasSuccessful = accept
            };

            result.ShouldBeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
        }

        [Fact]
        public void Arguments_and_switches_parse()
        {
            const string commandLine = "MyCommand --Enable";
            var input = new Input(commandLine);
            var args = CommandLineParser.Arguments(input);
            args.Value.Should().HaveCount(2);
        }
    }

    public class ParserLibSpecs
    {
        [Theory]
        [InlineData(@"Id1 Id2 Id3 Foo.Bar Fizz_Buzz")]
        public void Identifiers_parses_line_of_identifiers(string commandLine)
        {
            var expected = commandLine.Split(' ');
            var input = new Input(commandLine);
            var result = ParserLib.Identifiers(input);
            result.Value.Should().Equal(expected);
        }
    }
}
