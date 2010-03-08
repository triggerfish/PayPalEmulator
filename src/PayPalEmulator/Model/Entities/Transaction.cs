using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Triggerfish.Database;
using System.Text;

namespace PayPalEmulator
{
	public class Transaction : Entity<int>
	{
		public virtual string ReturnUrl { get; set; }
		public virtual string IpnReturnUrl { get; set; }
		public virtual string AuthToken { get; set; }
		public virtual string Tx { get; set; }
		public virtual string State { get; set; }
		public virtual string Amount { get; set; }
		public virtual string Currency { get; set; }
		public virtual string Custom { get; set; }
		public virtual string ItemNumber { get; set; }
		public virtual string Account { get; set; }
		public virtual string VerifySign { get; set; }

		public virtual decimal GetFee()
		{
			decimal amount = Decimal.Parse(Amount);
			return Math.Round((amount * 0.034m), 2, MidpointRounding.AwayFromZero) + 0.2m;
		}
	}
}
