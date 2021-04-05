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
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UnlikeAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "The value of {0} cannot be the same as the value of the {1}.";

        public string OtherPropertyDisplayName { get; private set; }
        public string OtherProperty { get; private set; }

        public UnlikeAttribute(string otherProperty)
            : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            OtherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, OtherPropertyDisplayName);
        }

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
                    //OtherPropertyDisplayName = otherProperty.GetCustomAttribute<DisplayAttribute>().Name;
                    return new ValidationResult($"{validationContext.MemberName} must not match {OtherProperty}");
                }
            }

            return ValidationResult.Success;
        }

    }
}
