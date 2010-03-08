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
	public class BuyNowClickHandlerTests : DatabaseTest
	{
		private Repository<PDT> m_repository;
		Mock<HttpRequestBase> m_request = new Mock<HttpRequestBase>();
		private ModelStateDictionary m_modelState = new ModelStateDictionary();

		[TestMethod]
		public void ShouldRedirectIfSuccessful()
		{
			// Arrange
			BuyNowClickHandler handler = new BuyNowClickHandler(m_repository);

			// Act
			RedirectToRouteResult result = handler.Process(m_request.Object, m_modelState) as RedirectToRouteResult;

			// Assert
			Assert.AreNotEqual(null, result);
			Assert.AreEqual("BuyNow", result.RouteValues["action"]);
			Assert.AreEqual("BuyNow", result.RouteValues["controller"]);
		}

		[TestMethod]
		public void ShouldInsertPDTIfSuccessful()
		{
			// Arrange
			BuyNowClickHandler handler = new BuyNowClickHandler(m_repository);

			// Act
			RedirectToRouteResult result = handler.Process(m_request.Object, m_modelState) as RedirectToRouteResult;

			// Assert
			Assert.AreEqual(1, m_repository.Count);
			Assert.AreEqual("hj8dhfdjfsh98", m_repository.Get(1).Custom);
		}

		[TestMethod]
		public void ShouldThrowIfBindingFailed()
		{
			// Arrange
			BuyNowClickHandler handler = new BuyNowClickHandler(m_repository);
			m_request.Object.Form.Remove("emulator_returnUrl");

			// Act
			try
			{
				RedirectToRouteResult result = handler.Process(m_request.Object, m_modelState) as RedirectToRouteResult;
			}
			catch (ErrorDataException)
			{
				return;
			}

			// Assert
			Assert.Fail("Should throw");
		}

		protected override void InitialiseData(ISession session)
		{
			NameValueCollection form = new NameValueCollection {
				{ "emulator_authToken", "abcdefghi" },
				{ "amount", "12.55" },
				{ "currency_code", "GBP" },
				{ "custom", "hj8dhfdjfsh98" },
				{ "emulator_returnUrl", "http://www.testing.com/here" },
				{ "business", "hfdsahfk" }
			};
			m_request.Setup(x => x.Form).Returns(form);
			m_repository = new Repository<PDT>(session);
		}
	}
}
