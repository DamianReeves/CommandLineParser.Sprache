using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandLineParser.Sprache
{
    public abstract class CommandLineArgument
    {
        private CommandLineArgument(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public class CommandLineProperty : CommandLineArgument
        {
            public CommandLineProperty(string name, string value) : base(value)
            {
                Name = name;
            }

            public string Name { get; }
        }

        public class CommandLineArg : CommandLineArgument
        {
            public CommandLineArg(string value) : base(value)
            {
            }
        }
    }
}
