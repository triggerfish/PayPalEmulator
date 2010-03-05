using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Database;
using Triggerfish.Ninject;

namespace PayPalEmulator
{
	public class TransactionAttribute : ActionFilterAttribute
	{
		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			base.OnResultExecuted(filterContext);

			if (filterContext.Controller.ViewData.ModelState.IsValid)
			{
				ObjectFactory.Get<IUnitOfWorkFactory>().GetCurrentUnitOfWork().Commit();
			}
		}
	}
}
