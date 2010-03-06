using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using Triggerfish.Security;
using System.Collections.Specialized;

namespace PayPalEmulator.Tests
{
	[TestClass]
	public class BuyNowBinderTests
	{
		private string m_auth = "jkfldfu890jdsoifjs98few8fwejfslkdfhs98hhkjh98";
		private string m_returnUrl = "http://www.testing.com/here";
		private string m_amount = "12.55";
		private string m_currency = "GBP";
		private string m_custom = "hgdsjkahda897";
		private ModelStateDictionary m_modelState = new ModelStateDictionary();

		[TestMethod]
		public void ShouldBindPdt()
		{
			// Arrange
			BuyNowBinder binder = new BuyNowBinder();

			NameValueCollection form = new NameValueCollection {
				{ "emulator_authToken", m_auth },
				{ "emulator_returnUrl", m_returnUrl },
				{ "amount", m_amount },
				{ "currency_code", m_currency },
				{ "custom", m_custom }
			};

			// Act
			PDT p = binder.Bind(form, m_modelState);

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
			BuyNowBinder binder = new BuyNowBinder();

			NameValueCollection form = new NameValueCollection {
				{ "emulator_returnUrl", m_returnUrl },
				{ "amount", m_amount },
				{ "currency_code", m_currency },
				{ "custom", m_custom }
			};

			// Act
			PDT p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailIfMissingUrl()
		{
			// Arrange
			BuyNowBinder binder = new BuyNowBinder();

			NameValueCollection form = new NameValueCollection {
				{ "emulator_authToken", m_auth },
				{ "amount", m_amount },
				{ "currency_code", m_currency },
				{ "custom", m_custom }
			};

			// Act
			PDT p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailIfMissingAmount()
		{
			// Arrange
			BuyNowBinder binder = new BuyNowBinder();

			NameValueCollection form = new NameValueCollection {
				{ "emulator_authToken", m_auth },
				{ "emulator_returnUrl", m_returnUrl },
				{ "currency_code", m_currency },
				{ "custom", m_custom }
			};

			// Act
			PDT p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailIfMissingCurrency()
		{
			// Arrange
			BuyNowBinder binder = new BuyNowBinder();

			NameValueCollection form = new NameValueCollection {
				{ "emulator_authToken", m_auth },
				{ "emulator_returnUrl", m_returnUrl },
				{ "amount", m_amount },
				{ "custom", m_custom }
			};

			// Act
			PDT p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailIfMissingCustom()
		{
			// Arrange
			BuyNowBinder binder = new BuyNowBinder();

			NameValueCollection form = new NameValueCollection {
				{ "emulator_authToken", m_auth },
				{ "emulator_returnUrl", m_returnUrl },
				{ "amount", m_amount },
				{ "currency_code", m_currency }
			};

			// Act
			PDT p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}
	}
}
