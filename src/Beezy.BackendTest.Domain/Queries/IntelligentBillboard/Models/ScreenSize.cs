using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models
{
    public class ScreenSize
    {
        public string Value { get; }

        private ScreenSize(string size)
        {
            Value = size;
        }

        public static ScreenSize Create(string size)
        {
            if (string.IsNullOrEmpty(size))
                throw new ArgumentException("ScreenSize cannot be null or empty. Possible values: Big, Small.");
            return new ScreenSize(size);
        }

        protected bool Equals(ScreenSize other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ScreenSize)obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(ScreenSize x, ScreenSize y)
        {
            if (x is null) return y is null;
            return x.Equals(y);
        }

        public static bool operator !=(ScreenSize x, ScreenSize y)
        {
            return !(x == y);
        }
    }
}
