using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.NHibernate;
using System.Text;
using Triggerfish.Ninject;
using Ninject;

namespace PayPalEmulator.Controllers
{
	[HandleError]
	public class CGiController : Controller
	{
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index()
		{
			// process get commands

			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[Transaction]
		public ActionResult Index(string cmd)
		{
			ICgiHandler handler = ObjectFactory.Kernel.TryGet<ICgiHandler>(cmd);

			if (handler != null)
			{
				return handler.Process(Request, ModelState);
			}

			return RedirectToAction("Index");
		}
	}
}
