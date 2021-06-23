using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEmailAPI.Validation
{
	public class ValidEncodingStringAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var encodingString = (string) value;

			if(string.IsNullOrEmpty(encodingString))
				return ValidationResult.Success;
			try
			{
				Encoding.GetEncoding(encodingString);
			}
			catch
			{
				return new ValidationResult(string.Format(MessageValidationErrorMessages.ValidEncodingStringErrorFormat, encodingString));
			}
			return ValidationResult.Success;
		}
	}
}
