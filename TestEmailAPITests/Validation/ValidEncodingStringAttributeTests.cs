using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TestEmailAPI.Validation;

namespace TestEmailAPITests.Validation
{
	public class ValidEncodingStringAttributeTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void ShouldPassValidationForAllEncodingStrings()
		{
			foreach (var encoding in Encoding.GetEncodings())
			{
				GivenEncodingString(encoding.Name);
				WhenEmailsAttributeValidationIsCalled();
				ThenValidationShouldPass();
			}
		}

		[Test]
		[TestCase("")]
		[TestCase(null)]
		public void ShouldPassValidationIfNullOrEmpty(string encodingString)
		{
			GivenEncodingString(encodingString);
			WhenEmailsAttributeValidationIsCalled();
			ThenValidationShouldPass();
		}

		[Test]
		[TestCase("Random Junk")]
		[TestCase("3jan2as0d2b")]
		[TestCase(" ")]
		[TestCase("\t")]
		[TestCase("1")]
		[TestCase("2343")]
		[TestCase("\n")]
		public void ShouldNotPassValidationForUnrecognizedStrings(string encodingString)
		{
			GivenEncodingString(encodingString);
			WhenEmailsAttributeValidationIsCalled();
			ThenValidationShouldNotPass();
		}

		private void GivenEncodingString(string encodingString)
		{
			_givenEncodingString = encodingString;
		}

		private void WhenEmailsAttributeValidationIsCalled()
		{
			_validationResult = new ValidEncodingStringAttribute().IsValid(_givenEncodingString);
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
		private string _givenEncodingString;
	}
}
