using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPalEmulator
{
	public class PaymentViewData
	{
		public Transaction Tx { get; set; }
		public bool IsIpnEnabled { get { return !String.IsNullOrEmpty(Tx.IpnReturnUrl); } }
	}
}
