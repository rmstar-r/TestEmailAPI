using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TestEmailAPI.Validation
{
	public class ValidEmailListAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var emails = (IEnumerable<string>) value;

			if(emails == null)
				return ValidationResult.Success;

			if(emails.Count() == 0)
				return new ValidationResult(MessageValidationErrorMessages.EmailInListCannotBeNullOrEmpty);

			foreach (var email in emails)
			{
				if(string.IsNullOrEmpty(email))
					return new ValidationResult(MessageValidationErrorMessages.EmailInListCannotBeNullOrEmpty);
				try
				{
					var address = new MailAddress(email).Address;
				}catch{
					return new ValidationResult(string.Format(MessageValidationErrorMessages.ValidEmailListAttributeErrorFormat, email));
				}
			}

			return ValidationResult.Success;
		}
	}
}
