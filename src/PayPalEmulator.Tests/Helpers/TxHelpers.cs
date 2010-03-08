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
using System.Collections.Specialized;

namespace PayPalEmulator.Tests
{
	public class TxHelpers
	{
		private string m_txn_id = "fsdfswe";
		private string m_payment_status = "Completed";
		private string m_mc_gross = "41.06";
		private string m_mc_currency = "GBP";
		private string m_custom = "fdsjfsuy8w34yfsj";
		private string m_item_number = "";
		private string m_business = "fhdshjsd78787dfsyf";
		private string m_verify_sign = "ouoqo98838ejueu";

		public NameValueCollection CreateForm()
		{
			return new NameValueCollection {
				{ "txn_type", "web_accept"},
				{ "txn_id", m_txn_id},
				{ "payment_status", m_payment_status},
				{ "mc_gross", m_mc_gross},
				{ "mc_fee", "1.60"},
				{ "mc_currency", m_mc_currency},
				{ "custom", m_custom},
				{ "item_number", m_item_number},
				{ "business", m_business},
				{ "verify_sign", m_verify_sign}
			};
		}

		public Transaction CreateTx()
		{
			return new Transaction {
				Tx = m_txn_id,
				State = m_payment_status,
				Amount = m_mc_gross,
				Currency = m_mc_currency,
				Custom = m_custom,
				ItemNumber = m_item_number,
				Account = m_business,
				VerifySign = m_verify_sign
			};
		}
	}
}
