using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TestEmailAPI.Validation;

namespace TestEmailAPITests.Validation
{
	class ValidEmailAttributeTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void ShouldPassValidationForProperlyFormattedEmails()
		{
			foreach (var email in ValidEmails)
			{
				GivenEmail(email);
				WhenEmailsAttributeValidationIsCalled();
				ThenValidationShouldPass();
			}
		}

		[Test]
		public void ShouldPassValidationForNullEmailString()
		{
			GivenEmail(null);
			WhenEmailsAttributeValidationIsCalled();
			ThenValidationShouldPass();
		}

		[Test]
		public void ShouldPassValidationForEmptyEmailString()
		{
			GivenEmail("");
			WhenEmailsAttributeValidationIsCalled();
			ThenValidationShouldPass();
		}

		[Test]
		[TestCase("Abc.example.com")]
		[TestCase("A@b@c@example.com")]
		[TestCase("a\"b(c)d,e:f; g<h> i[j\\k]l @example.com")]
		[TestCase("just\"not\"right@example.com")]
		[TestCase("this is\"not\\allowed @example.com")]
		[TestCase("this\\ still\"not\\allowed @example.com")]
		[TestCase("QA[icon]CHOCOLATE[icon]@test.com")]
		public void ShouldFailValidationForImporperlyFormattedEmails(string invalidEmail)
		{
			GivenEmail(invalidEmail);
			WhenEmailsAttributeValidationIsCalled();
			ThenValidationShouldNotPass();
		}

		private void GivenEmail(string email)
		{
			_email = email;
		}

		private void WhenEmailsAttributeValidationIsCalled()
		{
			_validationResult = new ValidEmailAttribute().IsValid(_email);
		}

		private void ThenValidationShouldPass()
		{
			Assert.IsTrue(_validationResult);
		}

		private void ThenValidationShouldNotPass()
		{
			Assert.IsFalse(_validationResult);
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

		private string _email;
	}
}
