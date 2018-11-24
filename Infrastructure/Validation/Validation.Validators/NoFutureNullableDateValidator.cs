// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoFutureNullableDateValidator.cs">
//   Copyright © 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Validation.Validators
{
    using System;

    

    using SpecExpress;
    using SpecExpress.Rules;

    /// <summary>
    /// The no future date validator.
    /// </summary>
    /// <typeparam name="T">
    /// The DateTime date
    /// </typeparam>
    public class NoFutureNullableDateValidator<T> : RuleValidator<T, DateTime?>
    {
        private readonly DateTime startDate = new DateTime(1800, 1, 1);

        /// <summary>
        ///     Initializes a new instance of the <see cref="NoFutureNullableDateValidator{T}" /> class.
        ///     Initializes a new instance of the <see cref="NoFutureDateValidator{T}" /> class.
        /// </summary>
        public NoFutureNullableDateValidator()
        {
            //Define either a Message Store Name or a default Message
            //MessageStoreName = "MyCompanyValidationMessages";
            this.Message = Properties.Resources.InvalidNoFutureDate;
        }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="specificationContainer">
        /// The specification container.
        /// </param>
        /// <param name="notification">
        /// The notification.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Validate(
            RuleValidatorContext<T, DateTime?> context,
            SpecificationContainer specificationContainer,
            ValidationNotification notification)
        {
            var isValid = context.PropertyValue.ToDateOnly() >= this.startDate && context.PropertyValue.ToDateOnly() <= DateTime.Today;
            return this.Evaluate(isValid, context, notification);
        }
    }
}