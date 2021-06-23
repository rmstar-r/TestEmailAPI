using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestEmailAPI.Validation;

namespace TestEmailAPI.Models
{
	public class MessageHeader
	{
		[Required(ErrorMessage = MessageValidationErrorMessages.HeaderNameRequired)]
		public string Name { get; set; }

		[Required(ErrorMessage = MessageValidationErrorMessages.HeaderValueRequired)]
		public string Value { get; set; }

	}
}
