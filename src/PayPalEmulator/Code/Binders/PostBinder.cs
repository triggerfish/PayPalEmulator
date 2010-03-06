using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Linq;
using Triggerfish.Validator;
using Triggerfish;
using Triggerfish.Web.Mvc;
using System.Collections.Specialized;

namespace PayPalEmulator
{
	public abstract class PostBinder<T> where T : class
	{
		protected NameValueCollection Form { get; private set; }

		public T Bind(NameValueCollection form, ModelStateDictionary modelState)
		{
			Form = form;

			try
			{
				return Bind();
			}
			catch (ValidationException ex)
			{
				ex.ToModelErrors(modelState, "");
				return null;
			}
		}

		protected abstract T Bind();

		protected string GetValue(string name, bool required)
		{
			string value = null;
			if (null != Form)
			{
				value = Form[name];
			}
			if (required && String.IsNullOrEmpty(value))
			{
				throw new ValidationException(name, "Field is required");
			}
			return value;
		}
	}
}
