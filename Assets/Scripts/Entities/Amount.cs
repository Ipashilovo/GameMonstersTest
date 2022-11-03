using System;
using Newtonsoft.Json;

namespace Entities
{
    public struct Amount : IEquatable<Amount>
    {
        public readonly int Value;

        public static Amount Zero => new Amount(0);

        [JsonConstructor]
        public Amount(int value)
        {
            Value = value;
        }

        public static Amount operator +(Amount a, Amount b)
        {
            return new Amount(a.Value + b.Value);
        }
        
        public static bool operator >=(Amount a, Amount b)
        {
            return a.Value >= b.Value;
        }

        public static bool operator <=(Amount a, Amount b)
        {
            return a.Value <= b.Value;
        }

        public bool Equals(Amount other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Amount other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }
    }
}