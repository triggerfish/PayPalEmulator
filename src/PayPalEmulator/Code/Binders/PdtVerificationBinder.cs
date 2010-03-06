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
	public class PdtVerificationBinder : PostBinder<PDT>
	{
		protected override PDT Bind()
		{
			PDT pdt = new PDT();
			pdt.AuthToken = GetValue("at", true);
			pdt.Tx = GetValue("tx", true);
			return pdt;
		}
	}
}
