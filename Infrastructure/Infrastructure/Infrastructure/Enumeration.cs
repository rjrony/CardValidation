// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enumeration.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// The enumeration.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the class for Enumeration
    /// </typeparam>
    public abstract class Enumeration<T> : IComparable
        where T : IComparable<T>, new()
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Enumeration{T}" /> class.
        /// </summary>
        protected Enumeration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Enumeration{T}"/> class.
        /// </summary>
        /// <param name="typeValue">
        /// The type value.
        /// </param>
        protected Enumeration(T typeValue)
        {
            this.Value = typeValue;
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        ///     The get all.
        /// </summary>
        /// <typeparam name="T1">
        ///     The code which derives from Enumeration
        /// </typeparam>
        /// <typeparam name="T2">
        ///     The class which contains all the public static fields with enumerations
        /// </typeparam>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public static IEnumerable<T1> GetAll<T1, T2>() where T1 : Enumeration<T> where T2 : class
        {
            var type = typeof(T2);
            var fields = type.GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var fieldInfo in fields)
            {
                var typeValue = fieldInfo.GetValue(null);
                if (typeValue != null)
                {
                    yield return (T1)typeValue;
                }
            }
        }

        /// <summary>
        /// The get name.
        /// </summary>
        /// <param name="codeValue">
        /// The code value.
        /// </param>
        /// <typeparam name="T1">
        /// the type of the code
        /// </typeparam>
        /// <typeparam name="T2">
        /// The class which contains all codes
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetName<T1, T2>(T codeValue) where T1 : Enumeration<T> where T2 : class
        {
            var type = typeof(T2);
            var fields = type.GetProperties(BindingFlags.Public | BindingFlags.Static);
            foreach (var fieldInfo in fields)
            {
                T1 typeValue = (T1)fieldInfo.GetValue(null);
                if (typeValue != null && object.Equals(typeValue.Value, codeValue))
                {
                    return fieldInfo.Name;
                }
            }

            return string.Empty;
        }

        /// <summary>
        ///     The == operator
        /// </summary>
        /// <param name="first">
        ///     The first.
        /// </param>
        /// <param name="second">
        ///     The second.
        /// </param>
        /// <returns>
        ///     returns true if they are the same
        /// </returns>
        public static bool operator ==(Enumeration<T> first, Enumeration<T> second)
        {
            // If both are null, or both are same instance, return true.
            if (object.ReferenceEquals(first, second))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)first == null) || ((object)second == null))
            {
                return false;
            }

            return first.GetHashCode() == second.GetHashCode();
        }

        /// <summary>
        ///     The != override
        /// </summary>
        /// <param name="first">
        ///     The first.
        /// </param>
        /// <param name="second">
        ///     The second.
        /// </param>
        /// <returns>
        ///     Returns bool is the objects values are not equal
        /// </returns>
        public static bool operator !=(Enumeration<T> first, Enumeration<T> second)
        {
            return !(first == second);
        }

        /// <summary>
        /// The compare to.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CompareTo(object obj)
        {
            return this.Value.CompareTo(((Enumeration<T>)obj).Value);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration<T>;
            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = this.GetType() == obj.GetType();
            var valueMatches = this.Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        /// <summary>
        ///     The get hash code.
        /// </summary>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public override int GetHashCode()
        {
            return typeof(T) == typeof(Guid) ? this.Value.ToString().GetHashCode() : this.Value.GetHashCode();
        }

        /// <summary>
        ///     The to string.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Name: {0} Value: {1}", this.GetType().Name, this.Value);
        }
    }
}