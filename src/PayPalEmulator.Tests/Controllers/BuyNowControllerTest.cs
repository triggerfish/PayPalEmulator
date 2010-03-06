using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPalEmulator;
using PayPalEmulator.Controllers;
using Moq;
using Triggerfish.NHibernate;

namespace PayPalEmulator.Tests.Controllers
{
	[TestClass]
	public class BuyNowControllerTest : DatabaseTest
	{
		private Repository<PDT> m_repository;

		[TestMethod]
		public void ShouldDisplayBuyNow()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository);

			// Act
			ViewResult result = controller.BuyNow(1);

			// Assert
			Assert.AreEqual("", result.ViewName);
			Assert.IsTrue(result.ViewData.Model is PaymentViewData);
			Assert.AreNotEqual(null, ((PaymentViewData)result.ViewData.Model).PDT);
		}

		[TestMethod]
		public void ShouldThrowFromBuyNowIfInvalidId()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository);

			// Act
			try
			{
				controller.BuyNow(99);
			}
			catch (ErrorDataException)
			{
				return;
			}

			// Assert
			Assert.Fail("Should Throw");
		}

		[TestMethod]
		public void ShouldRedirectToPaidFromPayNow()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository);

			// Act
			ActionResult result = controller.PayNow(1);

			// Assert
			Assert.IsTrue(result is RedirectToRouteResult);
			Assert.AreEqual("Paid", ((RedirectToRouteResult)result).RouteValues["action"]);
			Assert.AreEqual(1, (int)((RedirectToRouteResult)result).RouteValues["pdtId"]);
		}

		[TestMethod]
		public void ShouldCreateTxNumber()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository);

			// Act
			controller.PayNow(1);

			PDT pdt = m_repository.Get(1);

			// Assert
			Assert.IsFalse(String.IsNullOrEmpty(pdt.Tx));
			Assert.AreEqual("Completed", pdt.State);
		}

		[TestMethod]
		public void ShouldThrowFromPayNowIfInvalidId()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository);

			// Act
			try
			{
				controller.PayNow(99);
			}
			catch (ErrorDataException)
			{
				return;
			}

			// Assert
			Assert.Fail("Should have thrown");
		}

		[TestMethod]
		public void ShouldDisplayPaid()
		{
			// Arrange
			PDT pdt = m_repository.Get(1);
			pdt.Tx = "hfjdhfsk";
			pdt.State = "Completed";
			UnitOfWork.Commit();

			BuyNowController controller = new BuyNowController(m_repository);

			// Act
			ViewResult result = controller.Paid(1) as ViewResult;

			// Assert
			Assert.AreEqual("", result.ViewName);
			Assert.IsTrue(result.ViewData.Model is PaymentViewData);
			Assert.AreNotEqual(null, ((PaymentViewData)result.ViewData.Model).PDT);
		}

		[TestMethod]
		public void ShouldThrowFromPaidIfInvalidId()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository);

			// Act
			try
			{
				controller.BuyNow(99);
			}
			catch (ErrorDataException)
			{
				return;
			}

			// Assert
			Assert.Fail("Should Throw");
		}

		protected override void InitialiseData(NHibernate.ISession session)
		{
			PDT pdt = new PDT { Amount = "12.55", AuthToken = "fsd7ds876786fsd86", Currency = "GBP", Custom = "hj8dhfdjfsh98", ReturnUrl = "http://www.testing.com/here" };
			m_repository = new Repository<PDT>(session);
			m_repository.Insert(pdt);
		}
	}
}
