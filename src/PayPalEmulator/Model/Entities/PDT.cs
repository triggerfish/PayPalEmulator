using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Triggerfish.Database;
using System.Text;
using Triggerfish.Web;

namespace PayPalEmulator
{
	public class PDT : Entity<int>
	{
		public virtual string ReturnUrl { get; set; }
		public virtual string AuthToken { get; set; }
		public virtual string Tx { get; set; }
		public virtual string State { get; set; }
		public virtual string Amount { get; set; }
		public virtual string Currency { get; set; }
		public virtual string Custom { get; set; }
		public virtual string ItemNumber { get; set; }
		public virtual string Account { get; set; }

		public virtual QueryString ToQueryString()
		{
			return new QueryString()
				.Add("tx", Tx ?? "")
				.Add("st", State ?? "")
				.Add("amt", Amount ?? "")
				.Add("cc", Currency ?? "")
				.Add("cm", Custom ?? "")
				.Add("item_number", ItemNumber ?? "")
				.Add("business", Account ?? "");
		}

		public virtual string ToFullReturnUrl()
		{
			return ReturnUrl + ToQueryString().ToString();
		}
	}
}
