using System;

namespace CommandLineParser.Sprache
{
    public abstract class CommandLineArgument : IEquatable<CommandLineArgument>
    {
        private CommandLineArgument(string value)
        {
            Value = value;
        }

        public string Value
        {
            get;
        }

        public bool Equals(CommandLineArgument other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CommandLineArgument) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(CommandLineArgument left, CommandLineArgument right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CommandLineArgument left, CommandLineArgument right)
        {
            return !Equals(left, right);
        }

        public class CommandLineProperty : CommandLineArgument
        {
            public CommandLineProperty(string name, string value): base (value)
            {
                Name = name;
            }

            public string Name
            {
                get;
            }
        }

        public class CommandLineOption: CommandLineArgument, IEquatable<CommandLineOption>
        {
            public CommandLineOption(string name): base (name)
            {
                Name = name;
            }

            public string Name
            {
                get;
            }

            public bool Equals(CommandLineOption other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return base.Equals(other) && string.Equals(Name, other.Name);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((CommandLineOption) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (base.GetHashCode()*397) ^ (Name != null ? Name.GetHashCode() : 0);
                }
            }

            public static bool operator ==(CommandLineOption left, CommandLineOption right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(CommandLineOption left, CommandLineOption right)
            {
                return !Equals(left, right);
            }
        }

        public class CommandLineArg : CommandLineArgument
        {
            public CommandLineArg(string value): base (value)
            {
            }
        }        
    }
}