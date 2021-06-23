using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestEmailAPI.Validation;

namespace TestEmailAPI.Models
{
	public class Message
	{
		[ValidEmailList]
		[Required(ErrorMessage = MessageValidationErrorMessages.RecipientRequired)]
		public string[] To { get; set; }

		[ValidEmailList]
		public string[] CC { get; set; }

		[ValidEmailList]
		public string[] BCC { get; set; }

		[ValidEmailList]
		public string[] ReplyTo { get; set; }


		[ValidEmail]
		[Required(ErrorMessage = MessageValidationErrorMessages.SenderRequired)]
		public string Sender { get; set; }

		[Required(ErrorMessage = MessageValidationErrorMessages.SubjectRequired)]
		public string Subject { get; set; }

		[ValidEncodingString]
		public string SubjectEncoding { get; set; }

		[Range(0,2,ErrorMessage = MessageValidationErrorMessages.PriorityOutOfRange)]
		public int Priority { get; set; }

		[ValidHeaders]
		public MessageHeader[] Headers { get; set; }

		[ValidEncodingString]
		public string HeaderEncoding { get; set; }

		[Required(ErrorMessage = MessageValidationErrorMessages.BodyRequired)]
		[ValidBodyPart]
		public MessageBodyPart Body { get; set; }

		[ValidBodyPart(ValidateMultiple = true)]
		public MessageBodyPart[] AlternativeBodyParts { get; set; }
	}
}
