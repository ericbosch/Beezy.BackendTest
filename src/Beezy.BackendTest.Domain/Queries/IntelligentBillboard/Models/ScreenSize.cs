using System;

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
            if (String.IsNullOrEmpty(size))
                throw new ArgumentException("ScreenSize cannot be null or empty. Possible values: Big, Small.");
            return new ScreenSize(size);
        }

        public static implicit operator string(ScreenSize size) => size.Value;
    }
}
