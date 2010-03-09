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
using Triggerfish.Web;

namespace PayPalEmulator.Tests.Controllers
{
	[TestClass]
	public class BuyNowControllerTest : DatabaseTest
	{
		private Repository<Transaction> m_repository;
		private Mock<IHttpRequest> m_httpRequest = new Mock<IHttpRequest>();

		[TestMethod]
		public void ShouldDisplayBuyNow()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository, m_httpRequest.Object);

			// Act
			ViewResult result = controller.BuyNow(1);

			// Assert
			Assert.AreEqual("", result.ViewName);
			Assert.IsTrue(result.ViewData.Model is PaymentViewData);
			Assert.AreNotEqual(null, ((PaymentViewData)result.ViewData.Model).Tx);
		}

		[TestMethod]
		public void ShouldThrowFromBuyNowIfInvalidId()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository, m_httpRequest.Object);

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
			BuyNowController controller = new BuyNowController(m_repository, m_httpRequest.Object);

			// Act
			ActionResult result = controller.PayNow(1, BuyNowAction.Succeed);

			// Assert
			Assert.IsTrue(result is RedirectToRouteResult);
			Assert.AreEqual("Paid", ((RedirectToRouteResult)result).RouteValues["action"]);
			Assert.AreEqual(1, (int)((RedirectToRouteResult)result).RouteValues["txId"]);
		}

		[TestMethod]
		public void ShouldCreateTxAndVerifyNumbers()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository, m_httpRequest.Object);

			// Act
			controller.PayNow(1, BuyNowAction.Succeed);

			Transaction tx = m_repository.Get(1);

			// Assert
			Assert.IsFalse(String.IsNullOrEmpty(tx.Tx));
			Assert.IsFalse(String.IsNullOrEmpty(tx.VerifySign));
			Assert.AreEqual("Completed", tx.State);
		}

		[TestMethod]
		public void ShouldSendIpnMessage()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository, m_httpRequest.Object);
			Transaction tx = m_repository.Get(1);
			tx.IpnReturnUrl = "http://test.com/";
			UnitOfWork.Commit();

			// Act
			controller.PayNow(1, BuyNowAction.Succeed);

			// Assert
			m_httpRequest.Verify(x => x.BeginSend(null, null));
		}

		[TestMethod]
		public void ShouldNotSendIpnMessageIfNoUrl()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository, m_httpRequest.Object);

			// Act
			controller.PayNow(1, BuyNowAction.Succeed);

			// Assert
			m_httpRequest.Verify(x => x.BeginSend(null, null), Times.Never());
		}

		[TestMethod]
		public void ShouldSetStatusToFailed()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository, m_httpRequest.Object);

			// Act
			controller.PayNow(1, BuyNowAction.Fail);

			Transaction tx = m_repository.Get(1);

			// Assert
			Assert.AreEqual("Failed", tx.State);
		}

		[TestMethod]
		public void ShouldThrowFromPayNowIfInvalidId()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository, m_httpRequest.Object);

			// Act
			try
			{
				controller.PayNow(99, BuyNowAction.Succeed);
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
			Transaction tx = m_repository.Get(1);
			tx.Tx = "hfjdhfsk";
			tx.State = "Completed";
			UnitOfWork.Commit();

			BuyNowController controller = new BuyNowController(m_repository, m_httpRequest.Object);

			// Act
			ViewResult result = controller.Paid(1) as ViewResult;

			// Assert
			Assert.AreEqual("", result.ViewName);
			Assert.IsTrue(result.ViewData.Model is PaymentViewData);
			Assert.AreNotEqual(null, ((PaymentViewData)result.ViewData.Model).Tx);
		}

		[TestMethod]
		public void ShouldThrowFromPaidIfInvalidId()
		{
			// Arrange
			BuyNowController controller = new BuyNowController(m_repository, m_httpRequest.Object);

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
			TxHelpers txh = new TxHelpers();
			Transaction tx = txh.CreateTx();
			m_repository = new Repository<Transaction>(session);
			m_repository.Insert(tx);
		}
	}
}
