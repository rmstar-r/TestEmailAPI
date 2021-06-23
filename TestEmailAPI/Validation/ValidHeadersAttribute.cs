using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestEmailAPI.Models;

namespace TestEmailAPI.Validation
{
	public class ValidHeadersAttribute: ValidationAttribute
	{
		protected readonly List<ValidationResult> ValidationResults = new List<ValidationResult>();

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var headers = (IEnumerable<MessageHeader>) value;
			if(headers == null)
				return ValidationResult.Success;
			foreach (var header in headers)
			{
				if(header == null)
					return new ValidationResult(MessageValidationErrorMessages.HeaderInListCannotBeNull);

				if (!Validator.TryValidateObject(header, new ValidationContext(header), ValidationResults, true))
				{
					return new ValidationResult(ValidationResults.First().ErrorMessage);
				}
			}
			return ValidationResult.Success;
		}
	}
}
