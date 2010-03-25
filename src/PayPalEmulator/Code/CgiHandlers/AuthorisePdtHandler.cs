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
	public class AuthorisePdtHandler : AuthoriseHandler, ICgiHandler
	{
		private Repository<Transaction> m_txRepository;

		public AuthorisePdtHandler(Repository<Transaction> txRepository)
		{
			m_txRepository = txRepository;
		}

		public ActionResult Process(HttpRequestBase request, ModelStateDictionary modelState)
		{
			PdtVerificationBinder binder = new PdtVerificationBinder();
			Transaction tx = binder.Bind(request.Form, modelState);

			ContentResult cr = new ContentResult();
			cr.ContentEncoding = Encoding.UTF8;
			cr.ContentType = "text/html";
			cr.Content = "FAIL\n";

			if (tx != null)
			{
				Transaction dbTx = m_txRepository.GetAll().Where(x => x.Tx == tx.Tx).FirstOrDefault();
				if (dbTx != null && dbTx.AuthToken == tx.AuthToken)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append("SUCCESS\n");
					sb.Append(BuildContent(dbTx));

					cr.Content = sb.ToString();
				}
			}

			return cr;		
		}
	}
}
