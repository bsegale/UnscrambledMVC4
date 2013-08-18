using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Unscrambled.Areas.Coordinator.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class JoinCodeValidationAttribute : ValidationAttribute
    {
        private String BadJoinCode = "xyzxyz";

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            if ((value is string) && string.IsNullOrEmpty((string)value))
            {
                return true;
            }

            String JoinCode = (String)value;
            if (JoinCode.Equals(BadJoinCode, StringComparison.OrdinalIgnoreCase))
                return false;
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Join Code is invalid.";
        }
    }
}