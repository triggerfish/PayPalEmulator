using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using Triggerfish.Security;

namespace PayPalEmulator.Tests
{
	[TestClass]
	public class AddressBinderTests
	{
		private string m_auth = "jkfldfu890jdsoifjs98few8fwejfslkdfhs98hhkjh98";
		private string m_returnUrl = "http://www.testing.com/here";
		private string m_amount = "12.55";
		private string m_currency = "GBP";
		private string m_custom = "hgdsjkahda897";

		[TestMethod]
		public void ShouldBindAddress()
		{
			// Arrange
			PDTbinder binder = new PDTbinder();

			ModelBindingContext ctx = BinderHelpers.CreateModelBindingContext("pdt", new Dictionary<string, string> {
				{ "emulator_authToken", m_auth },
				{ "emulator_returnUrl", m_returnUrl },
				{ "amount", m_amount },
				{ "currency_code", m_currency },
				{ "custom", m_custom }
			});

			// Act
			PDT p = binder.BindModel(null, ctx) as PDT;

			// Assert
			Assert.AreEqual(m_auth, p.AuthToken);
			Assert.AreEqual(m_returnUrl, p.ReturnUrl);
			Assert.AreEqual(m_amount, p.Amount);
			Assert.AreEqual(m_currency, p.Currency);
			Assert.AreEqual(m_custom, p.Custom);
		}

		[TestMethod]
		public void ShouldFailIfMissingAuth()
		{
			// Arrange
			PDTbinder binder = new PDTbinder();

			ModelBindingContext ctx = BinderHelpers.CreateModelBindingContext("pdt", new Dictionary<string, string> {
				{ "emulator_returnUrl", m_returnUrl },
				{ "amount", m_amount },
				{ "currency_code", m_currency },
				{ "custom", m_custom }
			});

			// Act
			PDT p = binder.BindModel(null, ctx) as PDT;

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(ctx.ModelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailIfMissingUrl()
		{
			// Arrange
			PDTbinder binder = new PDTbinder();

			ModelBindingContext ctx = BinderHelpers.CreateModelBindingContext("pdt", new Dictionary<string, string> {
				{ "emulator_authToken", m_auth },
				{ "amount", m_amount },
				{ "currency_code", m_currency },
				{ "custom", m_custom }
			});

			// Act
			PDT p = binder.BindModel(null, ctx) as PDT;

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(ctx.ModelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailIfMissingAmount()
		{
			// Arrange
			PDTbinder binder = new PDTbinder();

			ModelBindingContext ctx = BinderHelpers.CreateModelBindingContext("pdt", new Dictionary<string, string> {
				{ "emulator_authToken", m_auth },
				{ "emulator_returnUrl", m_returnUrl },
				{ "currency_code", m_currency },
				{ "custom", m_custom }
			});

			// Act
			PDT p = binder.BindModel(null, ctx) as PDT;

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(ctx.ModelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailIfMissingCurrency()
		{
			// Arrange
			PDTbinder binder = new PDTbinder();

			ModelBindingContext ctx = BinderHelpers.CreateModelBindingContext("pdt", new Dictionary<string, string> {
				{ "emulator_authToken", m_auth },
				{ "emulator_returnUrl", m_returnUrl },
				{ "amount", m_amount },
				{ "custom", m_custom }
			});

			// Act
			PDT p = binder.BindModel(null, ctx) as PDT;

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(ctx.ModelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailIfMissingCustom()
		{
			// Arrange
			PDTbinder binder = new PDTbinder();

			ModelBindingContext ctx = BinderHelpers.CreateModelBindingContext("pdt", new Dictionary<string, string> {
				{ "emulator_authToken", m_auth },
				{ "emulator_returnUrl", m_returnUrl },
				{ "amount", m_amount },
				{ "currency_code", m_currency }
			});

			// Act
			PDT p = binder.BindModel(null, ctx) as PDT;

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(ctx.ModelState.IsValid);
		}
	}
}
