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

		// can't test POST Index method as uses Ninject
	}
}
