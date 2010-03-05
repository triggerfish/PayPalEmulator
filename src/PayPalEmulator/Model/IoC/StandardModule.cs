using System.Reflection;
using Ninject.Modules;
using NHibernate;
using Triggerfish.NHibernate;
using Triggerfish.Database;

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

			config.Create<PDT>();

			UnitOfWorkFactory.Initialise(config.Config, m_storage);

			// session
			Bind<ISession>()
				.ToMethod(x => UnitOfWorkFactory.GetCurrentSession());
			Bind<IUnitOfWorkFactory>()
				.To<UnitOfWorkFactory>()
				.InTransientScope();

			// repositories
			Bind<Repository<PDT>>()
				.ToSelf();

			// binders
			Bind<ModelBinder<PDT>>()
				.To<PDTbinder>();
		}
	}
}
