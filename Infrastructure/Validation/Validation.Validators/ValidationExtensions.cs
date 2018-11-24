// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationExtensions.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation.Validators
{
    using System;

    using SpecExpress.DSL;

    /// <summary>
    ///     The validation extensions.
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// The is email address.
        ///     This Method Name will be the name of the Rule in the DSL
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="T">
        /// The email string
        /// </typeparam>
        /// <returns>
        /// The
        ///     <see>
        ///         <cref>ActionJoinBuilder</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static ActionJoinBuilder<T, string> IsEmailAddress<T>(this IRuleBuilder<T, string> expression)
        {
            expression.RegisterValidator(new EmailAddressValidator<T>());
            return expression.JoinBuilder;
        }

        /// <summary>
        /// The is no future date.
        ///     This Method Name will be the name of the Rule in the DSL
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="T">
        /// The date DateTime
        /// </typeparam>
        /// <returns>
        /// The
        ///     <see>
        ///         <cref>ActionJoinBuilder</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static ActionJoinBuilder<T, DateTime> IsNoFutureDate<T>(this IRuleBuilder<T, DateTime> expression)
        {
            expression.RegisterValidator(new NoFutureDateValidator<T>());
            return expression.JoinBuilder;
        }

        /// <summary>
        /// The is no future date.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="T">
        /// The nullable date DateTime?
        /// </typeparam>
        /// <returns>
        /// The
        ///     <see>
        ///         <cref>ActionJoinBuilder</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static ActionJoinBuilder<T, DateTime?> IsNoFutureDate<T>(this IRuleBuilder<T, DateTime?> expression)
        {
            expression.RegisterValidator(new NoFutureNullableDateValidator<T>());
            return expression.JoinBuilder;
        }

        /// <summary>
        /// The is normal date.
        ///     This Method Name will be the name of the Rule in the DSL
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="T">
        /// The date DateTime
        /// </typeparam>
        /// <returns>
        /// The
        ///     <see>
        ///         <cref>ActionJoinBuilder</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static ActionJoinBuilder<T, DateTime> IsNormalDate<T>(this IRuleBuilder<T, DateTime> expression)
        {
            expression.RegisterValidator(new NormalDateValidator<T>());
            return expression.JoinBuilder;
        }

        /// <summary>
        /// The is normal date.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="T">
        /// The nullable date DateTime?
        /// </typeparam>
        /// <returns>
        /// The
        ///     <see>
        ///         <cref>ActionJoinBuilder</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static ActionJoinBuilder<T, DateTime?> IsNormalDate<T>(this IRuleBuilder<T, DateTime?> expression)
        {
            expression.RegisterValidator(new NormalNullableDateValidator<T>());
            return expression.JoinBuilder;
        }

        /// <summary>
        /// The is regular expression match.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="regexPattern">
        /// The regex pattern.
        /// </param>
        /// <typeparam name="T">
        /// generic
        /// </typeparam>
        /// <returns>
        /// The <see cref="ActionJoinBuilder"/>.
        /// </returns>
        public static ActionJoinBuilder<T, string> IsRegularExpressionMatch<T>(this IRuleBuilder<T, string> expression, string regexPattern)
        {
            expression.RegisterValidator(new RegularExpressionValidator<T>(regexPattern));
            return expression.JoinBuilder;
        }

        /// <summary>
        /// The is valid iban.
        ///     This Method Name will be the name of the Rule in the DSL
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="T">
        /// The name string
        /// </typeparam>
        /// <returns>
        /// The
        ///     <see>
        ///         <cref>ActionJoinBuilder</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static ActionJoinBuilder<T, string> IsValidIban<T>(this IRuleBuilder<T, string> expression)
        {
            expression.RegisterValidator(new IbanValidator<T>());
            return expression.JoinBuilder;
        }

        /// <summary>
        /// The is valid name.
        ///     This Method Name will be the name of the Rule in the DSL
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="T">
        /// The name string
        /// </typeparam>
        /// <returns>
        /// The
        ///     <see>
        ///         <cref>ActionJoinBuilder</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static ActionJoinBuilder<T, string> IsValidName<T>(this IRuleBuilder<T, string> expression)
        {
            expression.RegisterValidator(new NameValidator<T>());
            return expression.JoinBuilder;
        }

        /// <summary>
        /// The is valid uid.
        ///     This Method Name will be the name of the Rule in the DSL
        /// </summary>
        /// <typeparam name="T">
        /// The uid organisation id
        /// </typeparam>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The
        ///     <see>
        ///         <cref>ActionJoinBuilder</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static ActionJoinBuilder<T, int> IsValidUid<T>(this IRuleBuilder<T, int> expression)
        {
            expression.RegisterValidator(new UidOrganisationIdValidator<T>());
            return expression.JoinBuilder;
        }
    }
}