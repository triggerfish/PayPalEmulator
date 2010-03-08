using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject.Web.Mvc;
using Triggerfish.Ninject;
using System.Web.Configuration;
using Triggerfish.Database;
using Ninject;
using Triggerfish.Web.Mvc;

namespace PayPalEmulator
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : NinjectHttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"",												// Route name
				"cgi-bin/webscr",								// URL with parameters
				new { controller = "Cgi", action = "Index" }	// Parameter defaults
			);

			//routes.MapRoute(
			//    "Default",                                              // Route name
			//    "{controller}/{action}/{id}",                           // URL with parameters
			//    new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
			//);
		}

		public override void Init()
		{
			base.Init();

			this.EndRequest += AppEndRequest;
		}

		protected override void OnApplicationStarted()
		{
			ObjectFactory.Load(new PayPalEmulator.StandardModule(WebConfigurationManager.AppSettings["SQLiteDatabaseFilename"], UnitOfWorkStorageType.Web));

			RegisterAllControllersIn("PayPalEmulator");

			RegisterRoutes(RouteTable.Routes);

			ModelBinders.Binders.DefaultBinder = new BinderResolver(ObjectFactory.TryGet<IModelBinder>);
			TransactionAttribute.FactoryResolver = ObjectFactory.TryGet<IUnitOfWorkFactory>;
		}

		protected override IKernel CreateKernel()
		{
			return ObjectFactory.Kernel;
		}

		private void AppEndRequest(object sender, EventArgs args)
		{
			ObjectFactory.Get<IUnitOfWorkFactory>().CloseCurrentUnitOfWork();
		}
	}
}