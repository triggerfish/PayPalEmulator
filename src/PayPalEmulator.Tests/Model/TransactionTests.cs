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
using Triggerfish.Web;

namespace PayPalEmulator.Tests
{
	[TestClass]
	public class TransactionTests : DatabaseTest
	{
		[TestMethod]
		public void ShouldHaveCorrectMappings()
		{
			new PersistenceSpecification<Transaction>(Session)
				.CheckProperty(x => x.Id, 1) //identity starts at 1 - can't reset to zero
				.CheckProperty(x => x.ReturnUrl, "http://www.testing.com/here")
				.CheckProperty(x => x.IpnReturnUrl, "http://www.testing.com/there")
				.CheckProperty(x => x.AuthToken, "hfsdf947rwhfsejkyr943ffh9ry943yfhsjkehgfksyf7834")
				.CheckProperty(x => x.Tx, "gkjsfd7y43hfs")
				.CheckProperty(x => x.State, "")
				.CheckProperty(x => x.Amount, "12.55")
				.CheckProperty(x => x.Currency, "GBP")
				.CheckProperty(x => x.Custom, "gfskdgfds898f9sdfdshfsd")
				.CheckProperty(x => x.ItemNumber, "")
				.CheckProperty(x => x.Account, "dhsjkhdk")
				.CheckProperty(x => x.VerifySign, "67843287ydh")
				.VerifyTheMappings();
		}

		[TestMethod]
		public void ShoulBuildQueryString()
		{
			// arrange
			Transaction tx = new Transaction { Tx = "ABC", Amount = "123" };

			// act
			QueryString qs = tx.ToPdtQueryString();

			// assert
			Assert.AreEqual(7, qs.Count);
			Assert.AreEqual("?tx=ABC&st=&amt=123&cc=&cm=&item_number=&business=", qs.ToString());
		}
	
		[TestMethod]
		public void ShoulBuildFullUrl()
		{
			// arrange
			Transaction tx = new Transaction { Tx = "ABC", Amount = "123", ReturnUrl = "http://test.com" };

			// assert
			Assert.AreEqual("http://test.com?tx=ABC&st=&amt=123&cc=&cm=&item_number=&business=", tx.ToPdtReturnUrl());
		}
	}
}
