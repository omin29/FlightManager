using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Data.Context;
using Data.Models;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Data.Attributes
{
    /// <summary>
    /// A custom attribute which ensures that the value of a certain property does not match the value of another property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UnlikeAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "The value of {0} cannot be the same as the value of the {1}.";

        /// <summary>
        /// The name of the other property which will be used for validation.
        /// </summary>
        public string OtherProperty { get; private set; }

        /// <summary>
        /// The constructor of the UnlikeAttribute class which prepares the data for validation.
        /// </summary>
        /// <param name="otherProperty">The name of the other property</param>
        public UnlikeAttribute(string otherProperty)
            : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            OtherProperty = otherProperty;
        }

        /// <summary>
        /// Performs a validation check on the value of the current property to ensure that it is different from the other property's value.
        /// </summary>
        /// <param name="value">The value of the current property which should not match the other property's value.</param>
        /// <param name="validationContext">Describes the context in which a validation check is performed.</param>
        /// <returns>A container for the results of a validation request.</returns>
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType()
                    .GetProperty(OtherProperty);

                var otherPropertyValue = otherProperty
                    .GetValue(validationContext.ObjectInstance, null);

                if (value.Equals(otherPropertyValue))
                {
                    return new ValidationResult($"{validationContext.MemberName} must not match {OtherProperty}");
                }
            }

            return ValidationResult.Success;
        }

    }
}
