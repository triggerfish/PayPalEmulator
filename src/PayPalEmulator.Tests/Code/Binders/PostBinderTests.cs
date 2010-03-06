using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using Triggerfish.Security;
using System.Collections.Specialized;
using Triggerfish.Validator;

namespace PayPalEmulator.Tests
{
	[TestClass]
	public class PostBinderTests
	{
		private ModelStateDictionary m_modelState = new ModelStateDictionary();

		[TestMethod]
		public void ShouldReturnObjectIfSuccessful()
		{
			// Arrange
			FakeBinderSuccess binder = new FakeBinderSuccess();

			// Act
			string s = binder.Bind(null, m_modelState);

			// Assert
			Assert.AreEqual("testing", s);
		}

		[TestMethod]
		public void ShouldReturnNullIfFailed()
		{
			// Arrange
			FakeBinderThrows binder = new FakeBinderThrows();

			// Act
			string s = binder.Bind(null, m_modelState);

			// Assert
			Assert.AreEqual(null, s);
		}

		[TestMethod]
		public void ShouldSetModelStateInvalidIfFailed()
		{
			// Arrange
			FakeBinderThrows binder = new FakeBinderThrows();

			// Act
			string s = binder.Bind(null, m_modelState);

			// Assert
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldGetFormValueIfExists()
		{
			// Arrange
			FakeBinderUsingGetNotRequired binder = new FakeBinderUsingGetNotRequired();

			NameValueCollection form = new NameValueCollection {
				{ "test", "testing" }
			};

			// Act
			string s = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual("testing", s);
			Assert.IsTrue(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldReturnNullIfFormValueDoesNotExist()
		{
			// Arrange
			FakeBinderUsingGetNotRequired binder = new FakeBinderUsingGetNotRequired();

			// Act
			string s = binder.Bind(null, m_modelState);

			// Assert
			Assert.AreEqual(null, s);
			Assert.IsTrue(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldThrowIfFormValueDoesNotExistAndIsRequired()
		{
			// Arrange
			FakeBinderUsingGetRequired binder = new FakeBinderUsingGetRequired();

			// Act
			string s = binder.Bind(null, m_modelState);

			// Assert
			Assert.AreEqual(null, s);
			Assert.IsFalse(m_modelState.IsValid);
		}
	}

	internal class FakeBinderSuccess : PostBinder<string>
	{
		protected override string Bind()
		{
			return "testing";
		}
	}

	internal class FakeBinderThrows : PostBinder<string>
	{
		protected override string Bind()
		{
			throw new ValidationException("Test", "Error");
		}
	}

	internal class FakeBinderUsingGetNotRequired : PostBinder<string>
	{
		protected override string Bind()
		{
			return GetValue("test", false);
		}
	}
	
	internal class FakeBinderUsingGetRequired : PostBinder<string>
	{
		protected override string Bind()
		{
			return GetValue("test", true);
		}
	}
}
