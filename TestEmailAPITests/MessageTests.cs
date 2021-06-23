using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using TestEmailAPI.Models;
using TestEmailAPI.Validation;

namespace TestEmailAPITests
{
	public class MessageTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void ShouldPassValidationWhenOnlyRequiredFieldsArePopulated()
		{
			GivenValidMessage();
			WhenValidationOccurs();
			ThenMessageShouldPassValidation();
		}

		[Test]
		public void ShouldFailValidationIfToListIsEmpty()
		{
			GivenValidMessage();
			GivenToList(new string[0]);
			WhenValidationOccurs();
			ThenValidationResultWithErrorMessageExists(MessageValidationErrorMessages.EmailInListCannotBeNullOrEmpty);
		}

		[Test]
		public void ShouldFailValidationIfToListIsNull()
		{
			GivenValidMessage();
			GivenToList(null);
			WhenValidationOccurs();
			ThenValidationResultWithErrorMessageExists(MessageValidationErrorMessages.RecipientRequired);
		}

		[Test]
		public void ShouldFailValidationIfCCIsNotEmptyAndContainsInvalidEmail()
		{
			var invalidEmail = "invalid@email@here.com";
			GivenValidMessage();
			GivenCCHasValue("actual@email.com", invalidEmail);
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(string.Format(MessageValidationErrorMessages.ValidEmailListAttributeErrorFormat,invalidEmail));
			ThenValidationResultsCountShouldBe(1);
		}

		[Test]
		public void ShouldFailValidationIfBCCIsNotEmptyAndContainsInvalidEmail()
		{
			var invalidEmail = "invalid@email@here.com";
			GivenValidMessage();
			GivenBCCHasValue("actual@email.com", invalidEmail);
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(string.Format(MessageValidationErrorMessages.ValidEmailListAttributeErrorFormat,invalidEmail));
			ThenValidationResultsCountShouldBe(1);
		}

		[Test]
		public void ShouldFailValidationIfReplyToIsNotEmptyAndContainsInvalidEmail()
		{
			var invalidEmail = "invalid@email@here.com";
			GivenValidMessage();
			GivenReplyToListHasValue("actual@email.com", invalidEmail);
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(string.Format(MessageValidationErrorMessages.ValidEmailListAttributeErrorFormat,invalidEmail));
			ThenValidationResultsCountShouldBe(1);
		}

		[Test]
		public void ShouldFailValidationIfSenderIsNull()
		{
			GivenValidMessage();
			GivenSenderHasValue(null);
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(MessageValidationErrorMessages.SenderRequired);
		}

		[Test]
		public void ShouldFailValidationIfSenderIsEmpty()
		{
			GivenValidMessage();
			GivenSenderHasValue("");
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(MessageValidationErrorMessages.SenderRequired);
		}

		[Test]
		public void ShouldFailValidationIfSubjectIsEmpty()
		{
			GivenValidMessage();
			GivenSubjectHasValue("");
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(MessageValidationErrorMessages.SubjectRequired);
			ThenValidationResultsCountShouldBe(1);
		}

		[Test]
		public void ShoulfFailValidationIfBodyIsNull()
		{
			GivenValidMessage();
			GivenBodyHasValue(null);
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(MessageValidationErrorMessages.BodyRequired);
			ThenValidationResultsCountShouldBe(1);
		}

		[Test]
		public void ShouldFailValidationIfBodyHasNoText()
		{
			GivenValidMessage();
			GivenBodyHasValue(new MessageBodyPart());
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(MessageValidationErrorMessages.BodyPartTextRequired);
			ThenValidationResultsCountShouldBe(1);
		}

		[Test]
		public void ShouldFailValidationIfHeaderEncodingIsNotValid()
		{
			var invalidEncoding = "aldskjfkjwnla;ldn";
			GivenValidMessage();
			GivenHeaderEncodingHasValue(invalidEncoding);
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(string.Format(MessageValidationErrorMessages.ValidEncodingStringErrorFormat,invalidEncoding));
			ThenValidationResultsCountShouldBe(1);
		}

		[Test]
		public void ShouldFailValidationIfSubjectEncodingIsNotValid()
		{
			var invalidEncoding = "aldskjfkjwnla;ldn";
			GivenValidMessage();
			GivenSubjectEncodingHasValue(invalidEncoding);
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(string.Format(MessageValidationErrorMessages.ValidEncodingStringErrorFormat,invalidEncoding));
			ThenValidationResultsCountShouldBe(1);
		}

		[Test]
		public void ShouldFailValidationIfPriorityIsOutOfRange()
		{
			GivenValidMessage();
			GivenPriorityHasValue(-1);
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(MessageValidationErrorMessages.PriorityOutOfRange);
			ThenValidationResultsCountShouldBe(1);
		}

		[Test]
		public void ShouldFailValidationIfAlternativeBodyPartHasNoText()
		{
			var invalidBodyPart = new MessageBodyPart[] {new MessageBodyPart(){ Text = null}};
			GivenValidMessage();
			GivenAlternativeBodyParts(invalidBodyPart);
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(MessageValidationErrorMessages.BodyPartTextRequired);
			ThenValidationResultsCountShouldBe(1);
		}

		[Test]
		public void ShouldFailValidationIfAlternativeBodyPartEncodingIsInvalid()
		{
			var invalidEncoding = "NotValidEncoding";
			GivenValidMessage();
			GivenAlternativeBodyParts(new MessageBodyPart[] {new MessageBodyPart(){Text = "Text", ContentEncoding = invalidEncoding}});
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(string.Format(MessageValidationErrorMessages.ValidEncodingStringErrorFormat,invalidEncoding));
			ThenValidationResultsCountShouldBe(1);
		}

		[Test]
		public void ShouldFailValidationIfHeaderNameFieldIsEmpty()
		{
			var invalidHeader = new MessageHeader[] {new MessageHeader() {Value = "TestValue"}};
			GivenValidMessage();
			GivenHeadersHaveValues(invalidHeader);
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(MessageValidationErrorMessages.HeaderNameRequired);
			ThenValidationResultsCountShouldBe(1);
		}

		[Test]
		public void ShouldFailValidationIfHeaderValueFieldIsEmpty()
		{
			var invalidHeader = new MessageHeader[] {new MessageHeader() {Name = "NameValue"}};
			GivenValidMessage();
			GivenHeadersHaveValues(invalidHeader);
			WhenValidationOccurs();
			ThenMessageShouldNotPassValidation();
			ThenValidationResultWithErrorMessageExists(MessageValidationErrorMessages.HeaderValueRequired);
			ThenValidationResultsCountShouldBe(1);
		}

		private void GivenValidMessage()
		{
			_message = new Message
			{
				To = new[] { "recipient1@gmail.com", "recipient2@gmail.com" },
				Sender = "sender@gmail.com",
				Subject = "This is the Subject",
				Body = new MessageBodyPart
				{
					Text = "This is the email body."
				},
			};
		}

		private void GivenToList(params string[] emails)
		{
			_message.To = emails;
		}

		private void GivenCCHasValue(params string[] emails)
		{
			_message.CC = emails;
		}

		private void GivenBCCHasValue(params string[] emails)
		{
			_message.BCC = emails;
		}

		private void GivenReplyToListHasValue(params string[] emails)
		{
			_message.ReplyTo = emails;
		}

		private void GivenSenderHasValue(string sender)
		{
			_message.Sender = sender;
		}

		private void GivenSubjectHasValue(string subject)
		{
			_message.Subject = subject;
		}

		private void GivenBodyHasValue(MessageBodyPart bodyPart)
		{
			_message.Body = bodyPart;
		}

		private void GivenSubjectEncodingHasValue(string subjectEncoding)
		{
			_message.SubjectEncoding = subjectEncoding;
		}

		private void GivenHeaderEncodingHasValue(string headerEncoding)
		{
			_message.HeaderEncoding = headerEncoding;
		}

		private void GivenHeadersHaveValues(MessageHeader[] messageHeaders)
		{
			_message.Headers = messageHeaders;
		}

		private void GivenPriorityHasValue(int priority)
		{
			_message.Priority = priority;
		}

		private void GivenAlternativeBodyParts(MessageBodyPart[] bodyParts)
		{
			_message.AlternativeBodyParts = bodyParts;
		}

		private void WhenValidationOccurs()
		{
			_validationContext = new ValidationContext(_message, null, null);
			_validationResults = new List<ValidationResult>();
			_validationResult = Validator.TryValidateObject(_message, _validationContext, _validationResults, true);
		}

		private void ThenMessageShouldPassValidation()
		{
			Assert.IsTrue(_validationResult);
		}

		private void ThenMessageShouldNotPassValidation()
		{
			Assert.IsFalse(_validationResult);
		}

		private void ThenValidationResultWithErrorMessageExists(string errorMessage)
		{
			Assert.True(_validationResults.Any(x => x.ErrorMessage == errorMessage));
		}

		private void ThenValidationResultsCountShouldBe(int count)
		{
			Assert.AreEqual(count, _validationResults.Count);
		}

		private bool _validationResult;
		private ValidationContext _validationContext;
		private List<ValidationResult> _validationResults;
		private Message _message;
	}
}