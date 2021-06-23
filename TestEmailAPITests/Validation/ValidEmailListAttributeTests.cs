using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TestEmailAPI.Validation;

namespace TestEmailAPITests.Validation
{
	public class ValidEmailListAttributeTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void ShouldPassValidationForNullEnumerable()
		{
			var nullEmailList = GivenEmailEnumerableThatIsNull();
			var result = WhenEmailsAttributeValidationIsCalled(nullEmailList);
			ThenValidationShouldPass(result);
		}

		[Test]
		public void ShouldNotPassValidationForEmptyEnumerable()
		{
			var emptyEmailList = GivenEmailEnumerableThatIsEmpty();
			var result = WhenEmailsAttributeValidationIsCalled(emptyEmailList);
			ThenValidationShouldNotPass(result);
		}

		[Test]
		public void ShouldPassValidationForStringListOfProperlyFormattedEmails()
		{
			var validEmails = GivenEmailEnumerableWithOnlyValidEmails();
			var result = WhenEmailsAttributeValidationIsCalled(validEmails);
			ThenValidationShouldPass(result);
		}

		[Test]
		[TestCase("Abc.example.com")]
		[TestCase("A@b@c@example.com")]
		[TestCase("a\"b(c)d,e:f; g<h> i[j\\k]l @example.com")]
		[TestCase("just\"not\"right@example.com")]
		[TestCase("this is\"not\\allowed @example.com")]
		[TestCase("this\\ still\"not\\allowed @example.com")]
		[TestCase("QA[icon]CHOCOLATE[icon]@test.com")]
		public void ShouldFailValidationForStringListContainingAnyInvalidEmails(string invalidEmail)
		{
			var validEmails = GivenEmailEnumerableWithOnlyValidEmails();
			var emails = GivenAddingOneInvalidEmailToEnumerable(validEmails, invalidEmail);
			var result = WhenEmailsAttributeValidationIsCalled(emails);
			ThenValidationShouldNotPass(result);
		}

		private  IEnumerable<string> GivenEmailEnumerableWithOnlyValidEmails()
		{
			return ValidEmails;
		}

		private IEnumerable<string> GivenAddingOneInvalidEmailToEnumerable(IEnumerable<string> emails, string invalidEmail)
		{
			return emails.Concat(new[] {invalidEmail});
		}

		private IEnumerable<string> GivenEmailEnumerableThatIsNull()
		{
			return null;
		}

		private IEnumerable<string> GivenEmailEnumerableThatIsEmpty()
		{
			return new List<string>();
		}

		private bool WhenEmailsAttributeValidationIsCalled(IEnumerable<string> emails)
		{
			return new ValidEmailListAttribute().IsValid(emails);
		}

		private void ThenValidationShouldPass(bool result)
		{
			Assert.IsTrue(result);
		}

		private void ThenValidationShouldNotPass(bool result)
		{
			Assert.IsFalse(result);
		}

		private bool _validationResult;

		private static readonly string[] ValidEmails = new string[]
		{
			"simple@example.com",
			"very.common@example.com",
			"disposable.style.email.with+symbol@example.com",
			"other.email-with-hyphen@example.com",
			"fully-qualified-domain@example.com",
			"user.name+tag+sorting@example.com",
			"x@example.com",
			"example-indeed@strange-example.com",
			"test/test@test.com",
			"admin@mailserver1",
			"example@s.example",
			"\" \"@example.org",
			"\"john..doe\"@example.org",
			"mailhost!username@example.org",
			"user%example.com@example.org",
			"user-@example.org"
		};

		private IEnumerable<string> _emails;
	}
}
