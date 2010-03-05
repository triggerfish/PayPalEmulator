using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Validator;
using Triggerfish.Ninject;

namespace PayPalEmulator
{
	public abstract class ModelBinder<T> : Triggerfish.Web.Mvc.ModelBinder<T> where T : class
	{
	}
}
