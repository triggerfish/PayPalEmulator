﻿using System.Reflection;
using Ninject.Modules;
using NHibernate;
using Triggerfish.NHibernate;
using Triggerfish.Database;
using Triggerfish.Web.Mvc;
using Triggerfish.Web;

namespace PayPalEmulator
{
	public class StandardModule : NinjectModule
	{
		private string m_sqliteFilename;
		private UnitOfWorkStorageType m_storage;

		public StandardModule(string filename, UnitOfWorkStorageType storage)
		{
			m_sqliteFilename = filename;
			m_storage = storage;
		}

		public override void Load()
		{
			Configuration config = new Configuration(new SqliteDatabase(m_sqliteFilename));

			config.Create<Transaction>();

			UnitOfWorkFactory.Initialise(config.Config, m_storage);

			// session
			Bind<ISession>()
				.ToMethod(x => UnitOfWorkFactory.GetCurrentSession());
			Bind<IUnitOfWorkFactory>()
				.To<UnitOfWorkFactory>()
				.InTransientScope();

			// entities
			Bind<Repository<Transaction>>()
				.ToSelf();

			// binders
			Bind<ModelBinder<ICgiHandler>>()
				.To<CgiHandlerBinder>();
			Bind<ICgiHandler>()
				.To<BuyNowClickHandler>()
				.Named("_xclick");
			Bind<ICgiHandler>()
				.To<AuthorisePdtHandler>()
				.Named("_notify-synch");
			Bind<ICgiHandler>()
				.To<AuthoriseIpnHandler>()
				.Named("_notify-validate");

			// misc
			Bind<IHttpRequest>()
				.To<HttpRequest>();
		}
	}
}
