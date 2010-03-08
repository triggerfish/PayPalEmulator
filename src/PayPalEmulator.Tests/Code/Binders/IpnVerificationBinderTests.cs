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
	public class IpnVerificationBinderTests
	{
		private string m_txn_id = "fsdfswe";
		private string m_payment_status = "Completed";
		private string m_mc_gross = "12.22";
		private string m_mc_currency = "GBP";
		private string m_custom = "fdsjfsuy8w34yfsj";
		private string m_item_number = "";
		private string m_business = "fhdshjsd78787dfsyf";
		private string m_verify_sign = "ouoqo98838ejueu";

		private ModelStateDictionary m_modelState = new ModelStateDictionary();

		[TestMethod]
		public void ShouldBindTx()
		{
			// Arrange
			IpnVerificationBinder binder = new IpnVerificationBinder();

			NameValueCollection form = new NameValueCollection {
				{ "txn_id", m_txn_id},
				{ "payment_status", m_payment_status},
				{ "mc_gross", m_mc_gross},
				{ "mc_currency", m_mc_currency},
				{ "custom", m_custom},
				{ "item_number", m_item_number},
				{ "business", m_business},
				{ "verify_sign", m_verify_sign}
			};

			// Act
			Transaction p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(m_txn_id, p.Tx);
			Assert.AreEqual(m_payment_status, p.State);
			Assert.AreEqual(m_mc_gross, p.Amount);
			Assert.AreEqual(m_mc_currency, p.Currency);
			Assert.AreEqual(m_custom, p.Custom);
			Assert.AreEqual(m_item_number, p.ItemNumber);
			Assert.AreEqual(m_business, p.Account);
			Assert.AreEqual(m_verify_sign, p.VerifySign);
		}

		[TestMethod]
		public void ShouldFailToBindIfTxMissing()
		{
			// Arrange
			IpnVerificationBinder binder = new IpnVerificationBinder();

			NameValueCollection form = new NameValueCollection {
				{ "payment_status", m_payment_status},
				{ "mc_gross", m_mc_gross},
				{ "mc_currency", m_mc_currency},
				{ "custom", m_custom},
				{ "item_number", m_item_number},
				{ "business", m_business},
				{ "verify_sign", m_verify_sign}
			};

			// Act
			Transaction p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailToBindIfStausMissing()
		{
			// Arrange
			IpnVerificationBinder binder = new IpnVerificationBinder();

			NameValueCollection form = new NameValueCollection {
				{ "txn_id", m_txn_id},
				{ "mc_gross", m_mc_gross},
				{ "mc_currency", m_mc_currency},
				{ "custom", m_custom},
				{ "item_number", m_item_number},
				{ "business", m_business},
				{ "verify_sign", m_verify_sign}
			};

			// Act
			Transaction p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailToBindIfAmountMissing()
		{
			// Arrange
			IpnVerificationBinder binder = new IpnVerificationBinder();

			NameValueCollection form = new NameValueCollection {
				{ "txn_id", m_txn_id},
				{ "payment_status", m_payment_status},
				{ "mc_currency", m_mc_currency},
				{ "custom", m_custom},
				{ "item_number", m_item_number},
				{ "business", m_business},
				{ "verify_sign", m_verify_sign}
			};

			// Act
			Transaction p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailToBindIfCurrencyMissing()
		{
			// Arrange
			IpnVerificationBinder binder = new IpnVerificationBinder();

			NameValueCollection form = new NameValueCollection {
				{ "txn_id", m_txn_id},
				{ "payment_status", m_payment_status},
				{ "mc_gross", m_mc_gross},
				{ "custom", m_custom},
				{ "item_number", m_item_number},
				{ "business", m_business},
				{ "verify_sign", m_verify_sign}
			};

			// Act
			Transaction p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailToBindIfCustomMissing()
		{
			// Arrange
			IpnVerificationBinder binder = new IpnVerificationBinder();

			NameValueCollection form = new NameValueCollection {
				{ "txn_id", m_txn_id},
				{ "payment_status", m_payment_status},
				{ "mc_gross", m_mc_gross},
				{ "mc_currency", m_mc_currency},
				{ "item_number", m_item_number},
				{ "business", m_business},
				{ "verify_sign", m_verify_sign}
			};

			// Act
			Transaction p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailToBindIfItemNumberMissing()
		{
			// Arrange
			IpnVerificationBinder binder = new IpnVerificationBinder();

			NameValueCollection form = new NameValueCollection {
				{ "txn_id", m_txn_id},
				{ "payment_status", m_payment_status},
				{ "mc_gross", m_mc_gross},
				{ "mc_currency", m_mc_currency},
				{ "custom", m_custom},
				{ "business", m_business},
				{ "verify_sign", m_verify_sign}
			};

			// Act
			Transaction p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailToBindIfBusinessMissing()
		{
			// Arrange
			IpnVerificationBinder binder = new IpnVerificationBinder();

			NameValueCollection form = new NameValueCollection {
				{ "txn_id", m_txn_id},
				{ "payment_status", m_payment_status},
				{ "mc_gross", m_mc_gross},
				{ "mc_currency", m_mc_currency},
				{ "custom", m_custom},
				{ "item_number", m_item_number},
				{ "verify_sign", m_verify_sign}
			};

			// Act
			Transaction p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailToBindIfVerifySignMissing()
		{
			// Arrange
			IpnVerificationBinder binder = new IpnVerificationBinder();

			NameValueCollection form = new NameValueCollection {
				{ "txn_id", m_txn_id},
				{ "payment_status", m_payment_status},
				{ "mc_gross", m_mc_gross},
				{ "mc_currency", m_mc_currency},
				{ "custom", m_custom},
				{ "item_number", m_item_number},
				{ "business", m_business}
			};

			// Act
			Transaction p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

	}
}
