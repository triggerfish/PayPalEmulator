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
	public class PdtVerificationBinderTests
	{
		private string m_at = "jkfldfu890jdsoifjs98few8fwejfslkdfhs98hhkjh98";
		private string m_tx = "hjdfkshf888fsdyfsdu";
		private ModelStateDictionary m_modelState = new ModelStateDictionary();

		[TestMethod]
		public void ShouldBindPdt()
		{
			// Arrange
			PdtVerificationBinder binder = new PdtVerificationBinder();

			NameValueCollection form = new NameValueCollection {
				{ "at", m_at },
				{ "tx", m_tx }
			};

			// Act
			PDT p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(m_at, p.AuthToken);
			Assert.AreEqual(m_tx, p.Tx);
		}

		[TestMethod]
		public void ShouldFailIfMissingAt()
		{
			// Arrange
			PdtVerificationBinder binder = new PdtVerificationBinder();

			NameValueCollection form = new NameValueCollection {
				{ "tx", m_tx }
			};

			// Act
			PDT p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}

		[TestMethod]
		public void ShouldFailIfMissingTx()
		{
			// Arrange
			PdtVerificationBinder binder = new PdtVerificationBinder();

			NameValueCollection form = new NameValueCollection {
				{ "at", m_at },
			};

			// Act
			PDT p = binder.Bind(form, m_modelState);

			// Assert
			Assert.AreEqual(null, p);
			Assert.IsFalse(m_modelState.IsValid);
		}
	}
}
