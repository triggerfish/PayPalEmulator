﻿using System;
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
	public class BuyNowBinder : PostBinder<Transaction>
	{
		protected override Transaction Bind()
		{
			Transaction tx = new Transaction();
			tx.AuthToken = GetValue("emulator_authToken", true);
			tx.ReturnUrl = GetValue("emulator_returnUrl", true);
			tx.IpnReturnUrl = GetValue("emulator_ipnReturnUrl", true);
			tx.Amount = GetValue("amount", true);
			tx.Currency = GetValue("currency_code", true);
			tx.Custom = GetValue("custom", true);
			tx.Account = GetValue("business", true);
			return tx;
		}
	}
}
