using System;

namespace Infrastructure.Host.Contracts
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ExcludeSanitizationAttribute : Attribute
    {
    }
}
