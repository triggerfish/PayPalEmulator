using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Web.Mvc;
using Triggerfish.Ninject;

namespace PayPalEmulator
{
	public class CgiHandlerBinder : ModelBinder<ICgiHandler>
	{
		protected override object Bind()
		{
			string cmd = GetValue("cmd", true);
			return ObjectFactory.TryGet<ICgiHandler>(cmd);
		}
	}
}
