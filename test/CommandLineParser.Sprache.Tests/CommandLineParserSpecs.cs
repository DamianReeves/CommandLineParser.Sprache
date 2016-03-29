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
        public CommandLineParserSpecs()
        {
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
    }
}
