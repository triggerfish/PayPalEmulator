using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Globalization;

namespace PayPalEmulator.Tests
{
	[TestClass]
	public class BinderResolverTests
	{
		[TestMethod]
		public void ShouldResolveCustomBinder()
		{
			// Arrange
			Mock<IModelBinder> mock = new Mock<IModelBinder>();
			ModelBindingContext context = new ModelBindingContext();
			context.ModelType = typeof(string);

			BinderResolver resolver = new BinderResolver(mb => { return mock.Object; });

			// Act
			resolver.BindModel(null, context);

			// Assert
			mock.Verify(x => x.BindModel(null, context));
		}

		[TestMethod]
		public void ShouldUseDefaultModelBinder()
		{
			// Arrange
			ModelBindingContext context = new ModelBindingContext();
			context.ModelType = typeof(string);

			BinderResolver resolver = new BinderResolver(mb => { return null; });

			// Act
			try
			{
				resolver.BindModel(null, context);
			}
			catch (NullReferenceException)
			{
				return; // success - DefaultModelBinder will throw because the ControllerContext arg is null
			}

			// Assert
			Assert.Fail();
		}

		[TestMethod]
		public void ShouldUseDefaultModelBinderForValueType()
		{
			// Arrange
			ModelBindingContext context = new ModelBindingContext();
			context.ModelType = typeof(int);

			BinderResolver resolver = new BinderResolver(mb => { return null; });

			// Act
			try
			{
				resolver.BindModel(null, context);
			}
			catch (NullReferenceException)
			{
				return; // success - DefaultModelBinder will throw because the ControllerContext arg is null
			}

			// Assert
			Assert.Fail();
		}
	}
}
