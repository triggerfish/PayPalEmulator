using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Triggerfish.Validator;
using Triggerfish.Ninject;
using System.Collections.Specialized;
using System.Text;
using Triggerfish.Web;

namespace PayPalEmulator
{
	public static class TransactionExtensions
	{
		public static QueryString ToPdtQueryString(this Transaction tx)
		{
			return new QueryString()
				.Add("tx", tx.Tx ?? "")
				.Add("st", tx.State ?? "")
				.Add("amt", tx.Amount ?? "")
				.Add("cc", tx.Currency ?? "")
				.Add("cm", tx.Custom ?? "")
				.Add("item_number", tx.ItemNumber ?? "")
				.Add("business", tx.Account ?? "");
		}

		public static QueryString ToIpnQueryString(this Transaction tx)
		{
			return new QueryString()
				.Add("txn_type", "web_accept")
				.Add("txn_id", tx.Tx ?? "")
				.Add("payment_status", tx.State ?? "")
				.Add("mc_gross", tx.Amount ?? "")
				.Add("mc_fee", tx.GetFee().ToString())
				.Add("mc_currency", tx.Currency ?? "")
				.Add("custom", tx.Custom ?? "")
				.Add("item_number", tx.ItemNumber ?? "")
				.Add("business", tx.Account ?? "")
				.Add("verify_sign", tx.VerifySign ?? "");
		}

		public static string ToPdtReturnUrl(this Transaction tx)
		{
			return tx.ReturnUrl + tx.ToPdtQueryString().ToString();
		}
	}
}
