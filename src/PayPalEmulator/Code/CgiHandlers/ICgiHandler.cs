using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Validator;
using Triggerfish.Ninject;
using System.Collections.Specialized;
using System.Text;

namespace PayPalEmulator
{
	public interface ICgiHandler
	{
		ActionResult Process(HttpRequestBase request, ModelStateDictionary modelState);
	}
}
