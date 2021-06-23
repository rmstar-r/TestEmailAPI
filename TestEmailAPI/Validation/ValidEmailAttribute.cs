using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TestEmailAPI.Validation
{
	public class ValidEmailAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var email = (string) value;

			if(string.IsNullOrEmpty(email))
				return ValidationResult.Success;

			try
			{
				var address = new MailAddress(email).Address;
			}
			catch
			{
				return new ValidationResult(string.Format(MessageValidationErrorMessages.ValidEmailAttributeErrorFormat, email));
			}

			return ValidationResult.Success;
		}
	}
}
