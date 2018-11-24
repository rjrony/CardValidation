// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DummySpecification.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation
{
    using SpecExpress;

    /// <summary>
    ///     Only required so that the scan returns one spec and does not throw an exception
    /// </summary>
    internal class DummySpecification : Validates<string>
    {
    }
}