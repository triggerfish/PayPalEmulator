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
using Triggerfish.NHibernate;
using NHibernate;
using Moq;
using System.Web;

namespace PayPalEmulator.Tests
{
	[TestClass]
	public class AuthoriseIpnHandlerTests : DatabaseTest
	{
		private Repository<Transaction> m_repository;
		Mock<HttpRequestBase> m_request = new Mock<HttpRequestBase>();
		private ModelStateDictionary m_modelState = new ModelStateDictionary();

		[TestMethod]
		public void ShouldReturnVerified()
		{
			// Arrange
			AuthoriseIpnHandler handler = new AuthoriseIpnHandler(m_repository);

			// Act
			ContentResult result = handler.Process(m_request.Object, m_modelState) as ContentResult;

			// Assert
			Assert.AreNotEqual(null, result);
			Assert.AreEqual(Encoding.UTF8, result.ContentEncoding);
			Assert.AreEqual("text/html", result.ContentType);
			Assert.IsTrue(result.Content.StartsWith("VERIFIED"));
		}

		[TestMethod]
		public void ShouldFailIfBindingError()
		{
			// Arrange
			AuthoriseIpnHandler handler = new AuthoriseIpnHandler(m_repository);
			m_request.Object.Form.Remove("txn_id");

			// Act
			ContentResult result = handler.Process(m_request.Object, m_modelState) as ContentResult;

			// Assert
			Assert.AreNotEqual(null, result);
			Assert.AreEqual(Encoding.UTF8, result.ContentEncoding);
			Assert.AreEqual("text/html", result.ContentType);
			Assert.IsTrue(result.Content.StartsWith("INVALID"));
		}

		[TestMethod]
		public void ShouldFailIfTxDoesNotExist()
		{
			// Arrange
			AuthoriseIpnHandler handler = new AuthoriseIpnHandler(m_repository);
			m_request.Object.Form["txn_id"] = "plibble";

			// Act
			ContentResult result = handler.Process(m_request.Object, m_modelState) as ContentResult;

			// Assert
			Assert.IsTrue(result.Content.StartsWith("INVALID"));
		}

		[TestMethod]
		public void ShouldFailIfFormDataDoesNotMatch()
		{
			// Arrange
			AuthoriseIpnHandler handler = new AuthoriseIpnHandler(m_repository);
			m_request.Object.Form["payment_status"] = "plibble";

			// Act
			ContentResult result = handler.Process(m_request.Object, m_modelState) as ContentResult;

			// Assert
			Assert.IsTrue(result.Content.StartsWith("INVALID"));
		}

		protected override void InitialiseData(ISession session)
		{
			TxHelpers txh = new TxHelpers();
			NameValueCollection form = txh.CreateForm();
			m_request.Setup(x => x.Form).Returns(form);
			Transaction tx = txh.CreateTx();
			m_repository = new Repository<Transaction>(session);
			m_repository.Insert(tx);
		}
	}
}
