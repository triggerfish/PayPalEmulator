using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPalEmulator;
using PayPalEmulator.Controllers;
using Triggerfish.NHibernate;

namespace PayPalEmulator.Tests.Controllers
{
	[TestClass]
	public class CgiControllerTest
	{
		[TestMethod]
		public void ShouldDisplayIndexView()
		{
			// Arrange
			CGiController controller = new CGiController();

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.AreEqual("", result.ViewName);
		}

		[TestMethod]
		public void ShouldPerformAction()
		{
			// Arrange
			CGiController controller = new CGiController();
			FakeHandler handler = new FakeHandler();

			// Act
			ActionResult result = controller.Index(handler);

			// Assert
			Assert.IsTrue(result is JsonResult);
		}

		[TestMethod]
		public void ShouldRedirectToActionIfNoHandler()
		{
			// Arrange
			CGiController controller = new CGiController();

			// Act
			RedirectToRouteResult result = controller.Index(null) as RedirectToRouteResult;

			// Assert
			Assert.AreNotEqual(null, result);
			Assert.AreEqual("Index", result.RouteValues["action"]);
		}
	}

	internal class FakeHandler : ICgiHandler
	{
		public ActionResult Process(System.Web.HttpRequestBase request, ModelStateDictionary modelState)
		{
			return new JsonResult();
		}
	}
}
