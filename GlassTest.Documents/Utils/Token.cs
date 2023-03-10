using System;

namespace GlassTest.Documents.Utils
{
    public class Token
    {
        public string Value { get; private set; }
        public Token(string value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if(obj is Token)
            {
                return (obj as Token).Value.Equals(Value);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}