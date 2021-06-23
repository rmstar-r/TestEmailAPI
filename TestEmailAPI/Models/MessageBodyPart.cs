using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestEmailAPI.Validation;

namespace TestEmailAPI.Models
{
	public class MessageBodyPart
	{
		[Required(ErrorMessage = MessageValidationErrorMessages.BodyPartTextRequired)]
		public string Text { get; set; }

		[ValidEncodingString]
		public string ContentEncoding { get; set; }
		
		public bool IsHtml { get; set; }
	}
}
