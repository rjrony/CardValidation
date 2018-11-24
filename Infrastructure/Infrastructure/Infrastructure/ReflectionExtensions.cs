// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionExtensions.cs">
//   Copyright © 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;

    /// <summary>
    ///     The reflection extensions.
    /// </summary>
    public static class ReflectionExtensions
    {
        #region Methods

        private static string GetPropertyNameInternal(LambdaExpression lambda)
        {
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member.Name;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get message type. This method is required because event are dynamic types
        ///     and we need to figure out what interface represents the event
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        public static Type GetMessageType(this object message)
        {
            if (message == null)
            {
                return null;
            }

            if (message.GetType().FullName.EndsWith("__impl"))
            {
                var name = message.GetType().FullName.Replace("__impl", string.Empty).Replace("\\", string.Empty);
                foreach (var i in message.GetType().GetInterfaces())
                {
                    if (i.FullName == name)
                    {
                        return i;
                    }
                }
            }

            return message.GetType();
        }

        /// <summary>
        /// The copy properties to. Only first level of properties are copied. If they have no setter on target, these will be
        ///     ignored.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="deepCopy">
        /// The deep copy. Serializes properties and deserializes them.
        /// </param>
        public static void CopyPropertiesTo(this object source, object target, bool deepCopy = true)
        {
            var propsSource = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var propsTarget = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(c => c.Name);
            foreach (var propSource in propsSource)
            {
                if (!propsTarget.ContainsKey(propSource.Name))
                {
                    continue;
                }

                var foundTargetProp = propsTarget[propSource.Name];
                if (!foundTargetProp.CanWrite || !foundTargetProp.PropertyType.IsAssignableFrom(propSource.PropertyType))
                {
                    continue;
                }

                var sourceInstance = propSource.GetValue(source);
                object targetPropInstance;
                if (foundTargetProp.PropertyType.IsValueType || foundTargetProp.PropertyType == typeof(string) || !deepCopy)
                {
                    targetPropInstance = sourceInstance;
                }
                else
                {
                    var serializedData = sourceInstance.SerializeToJsonIncludingObjectNames();
                    targetPropInstance = serializedData.DeserializeFromJson(foundTargetProp.PropertyType);
                }

                foundTargetProp.SetValue(target, targetPropInstance);
            }
        }

        /// <summary>
        /// The get property name.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <typeparam name="TProperty">
        /// property to return
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetPropertyName<TProperty>(Expression<Func<TProperty>> property)
        {
            var lambda = (LambdaExpression)property;

            return GetPropertyNameInternal(lambda);
        }

        /// <summary>
        /// The get property name.
        /// </summary>
        /// <param name="this">
        /// The this.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TObject">
        /// instance of the object
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// property to return
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetPropertyName<TObject, TProperty>(this TObject @this, Expression<Func<TObject, TProperty>> expression)
        {
            return GetPropertyNameInternal(expression);
        }

        /// <summary>
        /// The is generic enumerable.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsGenericEnumerable(this Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsGenericTypeDefintionOf(typeof(IEnumerable<>));
        }

        /// <summary>
        /// The is generic list.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsGenericList(this Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsGenericTypeDefintionOf(typeof(List<>));
        }

        /// <summary>
        /// The is generic type defintion of. List of Car is GenericTypeDefinition of IList
        /// </summary>
        /// <param name="firstType">
        /// The first type. Example is List of Car
        /// </param>
        /// <param name="secondType">
        /// The second type. Example is IList
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsGenericTypeDefintionOf(this Type firstType, Type secondType)
        {
            if (firstType == null || firstType == typeof(object) || secondType == null || secondType == typeof(object))
            {
                return false;
            }

            var typeInterfaces = firstType.GetInterfaces();
            if (typeInterfaces.Any(typeInterface => typeInterface.IsGenericType && IsSameGenericTypeDefintion(typeInterface, secondType)))
            {
                return true;
            }

            bool isSame = IsSameGenericTypeDefintion(firstType, secondType);
            if (isSame)
            {
                return true;
            }

            return IsSameGenericTypeDefintion(firstType.BaseType, secondType);
        }

        /// <summary>
        /// The is same generic type defintion.
        /// </summary>
        /// <param name="firstType">
        /// The first type.
        /// </param>
        /// <param name="secondType">
        /// The second type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsSameGenericTypeDefintion(Type firstType, Type secondType)
        {
            if (firstType == null || secondType == null)
            {
                return false;
            }

            if (firstType.IsGenericType && secondType.IsGenericType)
            {
                return firstType.GetGenericTypeDefinition() == secondType.GetGenericTypeDefinition();
            }

            return false;
        }

        /// <summary>
        /// The to detailed type load exception.
        /// </summary>
        /// <param name="typeLoadException">
        /// The type load exception.
        /// </param>
        /// <returns>
        /// The <see cref="ReflectionTypeLoadException"/>.
        /// </returns>
        public static ReflectionTypeLoadException ToDetailedTypeLoadException(this ReflectionTypeLoadException typeLoadException)
        {
            if (typeLoadException == null)
            {
                return null;
            }

            var sb = new StringBuilder();
            sb.AppendLine(typeLoadException.Message);
            foreach (var loaderException in typeLoadException.LoaderExceptions)
            {
                sb.AppendLine(string.Format("{0} {1}", loaderException.GetType().FullName, loaderException.Message));
            }

            sb.AppendLine("-".PadRight(10, '-'));
            sb.AppendLine(typeLoadException.StackTrace);

            var result = new ReflectionTypeLoadException(typeLoadException.Types, typeLoadException.LoaderExceptions, sb.ToString());
            return result;
        }

        #endregion
    }
}