﻿using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentNHibernate.Testing;
using NHibernate;
using Triggerfish.Validator;
using NHibernate.Exceptions;
using System.Data.SQLite;

namespace PayPalEmulator.Tests
{
	[TestClass]
	public class PDTMappingTests : DatabaseTest
	{
		[TestMethod]
		public void ShouldHaveCorrectMappings()
		{
			new PersistenceSpecification<PDT>(Session)
				.CheckProperty(x => x.Id, 1) //identity starts at 1 - can't reset to zero
				.CheckProperty(x => x.ReturnUrl, "http://www.testing.com/here")
				.CheckProperty(x => x.AuthToken, "hfsdf947rwhfsejkyr943ffh9ry943yfhsjkehgfksyf7834")
				.CheckProperty(x => x.Tx, "gkjsfd7y43hfs")
				.CheckProperty(x => x.State, "")
				.CheckProperty(x => x.Amount, "12.55")
				.CheckProperty(x => x.Currency, "GBP")
				.CheckProperty(x => x.Custom, "gfskdgfds898f9sdfdshfsd")
				.CheckProperty(x => x.ItemNumber, "")
				.VerifyTheMappings();
		}
	}
}