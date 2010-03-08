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
	public class PdtVerificationBinder : PostBinder<Transaction>
	{
		protected override Transaction Bind()
		{
			Transaction tx = new Transaction();
			tx.AuthToken = GetValue("at", true);
			tx.Tx = GetValue("tx", true);
			return tx;
		}
	}
}
