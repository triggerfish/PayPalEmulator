using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;

namespace PayPalEmulator.Tests
{
	public static class BinderHelpers
	{
		public static ModelBindingContext CreateModelBindingContext(string argName)
		{
			return new ModelBindingContext() {
				FallbackToEmptyPrefix = true,
				ModelName = argName,
				ModelState = new ModelStateDictionary(),
				ValueProvider = new Dictionary<string, ValueProviderResult>(),
			};
		}

		public static ModelBindingContext CreateModelBindingContext(string argName, string argValue)
		{
			ModelBindingContext ctx = CreateModelBindingContext(argName);
			ctx.ValueProvider.Add(argName, new ValueProviderResult(argValue, argValue, CultureInfo.CurrentCulture));
			return ctx;
		}

		public static ModelBindingContext CreateModelBindingContext(string argName, IDictionary<string, string> argValues)
		{
			ModelBindingContext ctx = CreateModelBindingContext(argName);
			foreach (KeyValuePair<string, string> kvp in argValues)
			{
				ctx.ValueProvider.Add(kvp.Key, new ValueProviderResult(kvp.Value, kvp.Value, CultureInfo.CurrentCulture));
			}
			return ctx;
		}
	}
}
