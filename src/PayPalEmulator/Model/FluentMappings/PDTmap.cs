using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using Triggerfish.Database;

namespace PayPalEmulator
{
	public class PDTmap : ClassMap<PDT>
	{
		public PDTmap()
		{
			Table("PDT");
			Id(x => x.Id).GeneratedBy.Native();
			Map(x => x.ReturnUrl);
			Map(x => x.AuthToken);
			Map(x => x.Tx);
			Map(x => x.State);
			Map(x => x.Amount);
			Map(x => x.Currency);
			Map(x => x.Custom);
			Map(x => x.ItemNumber);
			Map(x => x.Account);
		}
	}
}
