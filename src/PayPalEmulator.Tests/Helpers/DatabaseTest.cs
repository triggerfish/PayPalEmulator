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
	public class DatabaseTest : Triggerfish.NHibernate.Testing.DatabaseTest
	{
		[TestInitialize]
		public void Setup()
		{
 			SetupTest<PDT>();
		}

		[TestCleanup]
		public void TearDown()
		{
 			 TearDownTest();
		}
	}
}
