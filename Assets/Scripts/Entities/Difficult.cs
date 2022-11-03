using System;
using Newtonsoft.Json;

namespace Entities
{
    public struct Difficult : IEquatable<Difficult>
    {
        public readonly string Value;

        [JsonConstructor]
        public Difficult(string value)
        {
            Value = value;
        }

        public bool Equals(Difficult other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Difficult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}