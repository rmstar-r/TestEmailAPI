using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEmailAPI.Validation
{
	public static class MessageValidationErrorMessages
	{
		public const string RecipientRequired = "Message 'To' field cannot be null or empty.";
		public const string SenderRequired = "Message 'Sender' field cannot be null or empty.";
		public const string SubjectRequired = "Message 'Subject' field cannot be null or empty.";
		public const string BodyRequired = "Message 'Body' field cannot be null or empty.";
		public const string EmailInListCannotBeNullOrEmpty = "Email list may not contain null or empty values.";

		public const string BodyPartTextRequired = "The 'Text' field of a Message Body Part cannot be null or empty.";

		public const string PriorityOutOfRange =
			"Message 'Priority' field be one of the following integers: 0 (Normal priority), 1 (Low priority), 2 (High priority).";

		public const string HeaderInListCannotBeNull = "Header list may not contain null members.";
		public const string HeaderNameRequired = "The 'Name' field of a Message Header cannot be null or empty.";
		public const string HeaderValueRequired = "The 'Value' field of a Message Header cannot be null or empty.";

		public const string ValidEmailListAttributeErrorFormat = "List contains invalid email address: {0}";
		public const string ValidEmailAttributeErrorFormat = "Invalid email address: {0}";
		public const string ValidEncodingStringErrorFormat = "Failed to parse Encoding string: {0}";
	}
}
