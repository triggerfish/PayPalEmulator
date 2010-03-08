using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Validator;
using Triggerfish.Ninject;
using System.Collections.Specialized;
using System.Text;
using Triggerfish.NHibernate;
using System.Web.Routing;

namespace PayPalEmulator
{
	public abstract class AuthoriseHandler
	{
		protected string BuildContent(Transaction tx)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(String.Format("{0}={1}", "txn_type", "web_accept"));
			sb.AppendLine(String.Format("{0}={1}", "txn_id", HttpUtility.UrlEncodeUnicode(tx.Tx)));
			sb.AppendLine(String.Format("{0}={1}", "payment_status", tx.State));
			sb.AppendLine(String.Format("{0}={1}", "mc_gross", HttpUtility.UrlEncodeUnicode(tx.Amount)));
			sb.AppendLine(String.Format("{0}={1}", "mc_fee", tx.GetFee().ToString()));
			sb.AppendLine(String.Format("{0}={1}", "mc_currency", HttpUtility.UrlEncodeUnicode(tx.Currency)));
			sb.AppendLine(String.Format("{0}={1}", "custom", HttpUtility.UrlEncodeUnicode(tx.Custom)));
			sb.AppendLine(String.Format("{0}={1}", "item_number", HttpUtility.UrlEncodeUnicode(tx.ItemNumber)));
			sb.AppendLine(String.Format("{0}={1}", "business", HttpUtility.UrlEncodeUnicode(tx.Account)));

			return sb.ToString();
		}
	}
}
