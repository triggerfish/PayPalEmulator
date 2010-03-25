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
using Triggerfish.Web;

namespace PayPalEmulator
{
	public class AuthoriseIpnHandler : AuthoriseHandler, ICgiHandler
	{
		private Repository<Transaction> m_txRepository;

		public AuthoriseIpnHandler(Repository<Transaction> txRepository)
		{
			m_txRepository = txRepository;
		}

		public ActionResult Process(HttpRequestBase request, ModelStateDictionary modelState)
		{
			IpnVerificationBinder binder = new IpnVerificationBinder();
			Transaction tx = binder.Bind(request.Form, modelState);

			ContentResult cr = new ContentResult();
			cr.ContentEncoding = Encoding.UTF8;
			cr.ContentType = "text/html";
			cr.Content = "INVALID";

			if (tx != null)
			{
				Transaction dbTx = m_txRepository.GetAll().Where(x => x.Tx == tx.Tx).FirstOrDefault();
				if (dbTx != null)
				{
					string expected = dbTx.ToIpnQueryString().ToString();
					QueryString actualQs = new QueryString();
					actualQs.Add(request.Form);
					actualQs.Remove("cmd");
					string actual = actualQs.ToString();

					if (expected == actual)
					{
						cr.Content = "VERIFIED";
					}
				}
			}

			return cr;
		}
	}
}
