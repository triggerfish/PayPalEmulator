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
	public class AuthorisePdtHandlerTests : DatabaseTest
	{
		private Repository<PDT> m_repository;
		Mock<HttpRequestBase> m_request = new Mock<HttpRequestBase>();
		private ModelStateDictionary m_modelState = new ModelStateDictionary();

		[TestMethod]
		public void ShouldReturnSuccess()
		{
			// Arrange
			AuthorisePdtHandler handler = new AuthorisePdtHandler(m_repository);

			// Act
			ContentResult result = handler.Process(m_request.Object, m_modelState) as ContentResult;

			// Assert
			Assert.AreNotEqual(null, result);
			Assert.AreEqual(Encoding.UTF8, result.ContentEncoding);
			Assert.AreEqual("text/html", result.ContentType);
			Assert.IsTrue(result.Content.StartsWith("SUCCESS"));
		}

		[TestMethod]
		public void ShouldReturnFailIfCannotBindPdt()
		{
			// Arrange
			AuthorisePdtHandler handler = new AuthorisePdtHandler(m_repository);
			m_request.Object.Form.Remove("at");

			// Act
			ContentResult result = handler.Process(m_request.Object, m_modelState) as ContentResult;

			// Assert
			Assert.AreNotEqual(null, result);
			Assert.AreEqual(Encoding.UTF8, result.ContentEncoding);
			Assert.AreEqual("text/html", result.ContentType);
			Assert.IsTrue(result.Content.StartsWith("FAIL"));
		}

		[TestMethod]
		public void ShouldReturnFailIfCannotFindPdtInDatabase()
		{
			// Arrange
			AuthorisePdtHandler handler = new AuthorisePdtHandler(m_repository);
			m_request.Object.Form["tx"] = "plibble";

			// Act
			ContentResult result = handler.Process(m_request.Object, m_modelState) as ContentResult;

			// Assert
			Assert.AreNotEqual(null, result);
			Assert.AreEqual(Encoding.UTF8, result.ContentEncoding);
			Assert.AreEqual("text/html", result.ContentType);
			Assert.IsTrue(result.Content.StartsWith("FAIL"));
		}

		[TestMethod]
		public void ShouldReturnFailIfAuthTokenInvalid()
		{
			// Arrange
			AuthorisePdtHandler handler = new AuthorisePdtHandler(m_repository);
			m_request.Object.Form["at"] = "plibble";

			// Act
			ContentResult result = handler.Process(m_request.Object, m_modelState) as ContentResult;

			// Assert
			Assert.AreNotEqual(null, result);
			Assert.AreEqual(Encoding.UTF8, result.ContentEncoding);
			Assert.AreEqual("text/html", result.ContentType);
			Assert.IsTrue(result.Content.StartsWith("FAIL"));
		}
		protected override void InitialiseData(ISession session)
		{
			NameValueCollection form = new NameValueCollection {
				{ "at", "abcdefghi" },
				{ "tx", "1234567890" }
			};
			m_request.Setup(x => x.Form).Returns(form); 
			PDT pdt = new PDT { Amount = "12.55", AuthToken = "abcdefghi", Custom = "hj8dhfdjfsh98", Tx = "1234567890" };
			m_repository = new Repository<PDT>(session);
			m_repository.Insert(pdt);
		}
	}
}
