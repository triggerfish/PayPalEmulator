using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using Triggerfish.NHibernate;
using Triggerfish.Database;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Event;

namespace PayPalEmulator.Tests
{
	public abstract class DatabaseTest
	{
		protected UnitOfWork UnitOfWork { get; private set; }
		protected ISession Session { get; private set; }

		[TestInitialize]
		public void Setup()
		{
			FluentConfiguration configuration =
				Fluently.Configure()
					.Database(SQLiteConfiguration.Standard
						.InMemory()
						.ProxyFactoryFactory("NHibernate.ByteCode.LinFu.ProxyFactoryFactory, NHibernate.ByteCode.LinFu")
						.ShowSql())
					.Mappings(x => x.FluentMappings.AddFromAssemblyOf<PDT>());

			configuration.BuildConfiguration();

			SingleConnectionSessionSourceForSQLiteInMemoryTesting ss = new SingleConnectionSessionSourceForSQLiteInMemoryTesting(configuration);
			ss.BuildSchema();

			Session = ss.CreateSession();
			UnitOfWork = new UnitOfWork(Session);
			UnitOfWork.Begin();

			InitialiseData(Session);
			UnitOfWork.Commit();

			Session.Clear();
		}

		[TestCleanup]
		public void TearDown()
		{
			UnitOfWork.End();
		}

		protected virtual void InitialiseData(ISession session)
		{
		}
	}
}
