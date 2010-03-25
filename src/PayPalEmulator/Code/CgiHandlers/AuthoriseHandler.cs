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
			return String.Format("transaction_subject=918562a4aa6348ba9f2fd69cfa6563c5\npayment_date=15%3A07%3A06+Mar+24%2C+2010+PDT\n" +
				"txn_type=web_accept\nlast_name=User\nresidence_country=GB\nitem_name=Climatic+Data+for+ALC\n" +
				"payment_gross=\nmc_currency={0}\nbusiness={1}\n" +
				"payment_type=instant\nprotection_eligibility=Ineligible\npayer_status=verified\ntax=0.00\n" +
				"payer_email=buyer2_1269459106_per%40triggerfishsoftware.co.uk\ntxn_id={2}\n" +
				"quantity=1\nreceiver_email=grid_1269419750_biz%40triggerfishsoftware.co.uk\nfirst_name=Test\n" +
				"payer_id=EA2FG2FSBQMUN\nreceiver_id=A79SPMGDKFXME\nitem_number={3}\nhandling_amount=0.00\n" +
				"payment_status={4}\npayment_fee=\nmc_fee={5}\nshipping=0.00\nmc_gross={6}\n" +
				"custom={7}\ncharset=windows-1252\n",
				HttpUtility.UrlEncodeUnicode(tx.Currency),
				HttpUtility.UrlEncodeUnicode(tx.Account),
				HttpUtility.UrlEncodeUnicode(tx.Tx),
				HttpUtility.UrlEncodeUnicode(tx.ItemNumber),
				tx.State,
				tx.GetFee().ToString(),
				HttpUtility.UrlEncodeUnicode(tx.Amount),
				HttpUtility.UrlEncodeUnicode(tx.Custom));
		}
	}
}
