using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Linq;
using Triggerfish.Validator;
using Triggerfish;

namespace PayPalEmulator
{
	public class PDTbinder : ModelBinder<PDT>
	{
		protected override object Bind()
		{
			PDT pdt = new PDT();
			pdt.AuthToken = GetValue("emulator_authToken", true);
			pdt.ReturnUrl = GetValue("emulator_returnUrl", true);
			pdt.Amount = GetValue("amount", true);
			pdt.Currency = GetValue("currency_code", true);
			pdt.Custom = GetValue("custom", true);
			return pdt;
		}
	}
}
