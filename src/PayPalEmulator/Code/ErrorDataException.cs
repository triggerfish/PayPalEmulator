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
	public class ErrorDataException : Exception
	{
		private IDictionary<string, ModelState> m_errors;

		public ErrorDataException(IDictionary<string, ModelState> errors)
			: base()
		{
			m_errors = errors;
		}

		public override string Message
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				TagBuilder blurb = new TagBuilder("p");
				blurb.InnerHtml = "The following errors occurred:";
				sb.AppendLine(blurb.ToString());
				
				TagBuilder list = new TagBuilder("ul");
				foreach (var kvp in m_errors)
				{
					if (kvp.Value.Errors.Any())
					{
						TagBuilder item = new TagBuilder("li");
						item.InnerHtml = String.Format("{0}: {1}", kvp.Key, kvp.Value.Errors.FirstOrDefault());
					}
				}

				sb.AppendLine(list.ToString());
				return sb.ToString();
			}
		}
	}
}
