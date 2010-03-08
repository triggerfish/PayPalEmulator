using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Linq;
using Triggerfish.Validator;
using Triggerfish;
using System.Collections.Specialized;

namespace PayPalEmulator
{
	public class IpnVerificationBinder : PostBinder<Transaction>
	{
		protected override Transaction Bind()
		{
			Transaction tx = new Transaction();
			tx.Tx = GetValue("txn_id", true);
			tx.State = GetValue("payment_status", true);
			tx.Amount = GetValue("mc_gross", true);
			tx.Currency = GetValue("mc_currency", true);
			tx.Custom = GetValue("custom", true);
			tx.ItemNumber = GetValue("item_number", true);
			tx.Account = GetValue("business", true);
			tx.VerifySign = GetValue("verify_sign", true);
			return tx;
		}
	}
}
