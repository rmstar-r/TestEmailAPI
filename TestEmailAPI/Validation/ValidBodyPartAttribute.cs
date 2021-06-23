using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestEmailAPI.Models;

namespace TestEmailAPI.Validation
{
	public class ValidBodyPartAttribute: ValidationAttribute
	{
		protected readonly List<ValidationResult> ValidationResults = new List<ValidationResult>();
		public bool ValidateMultiple { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if(value == null)
				return ValidationResult.Success;
			
			var bodyParts = ValidateMultiple ? (IEnumerable<MessageBodyPart>) value : new[] {(MessageBodyPart) value};

			foreach (var bodyPart in bodyParts)
			{
				if (!Validator.TryValidateObject(bodyPart, new ValidationContext(bodyPart), ValidationResults, true))
				{
					return new ValidationResult(ValidationResults.First().ErrorMessage);
				}
			}
			return ValidationResult.Success;
		}
	}
}
